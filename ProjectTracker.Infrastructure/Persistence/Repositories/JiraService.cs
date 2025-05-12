using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectTracker.Application.Dtos;
using ProjectTracker.Domain.Interface;
using ProjectTracker.Domain.Jira;

namespace ProjectTracker.Infrastructure.Persistence.Repositories
{
    public class JiraService : IJiraService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<JiraService> _logger;

        public JiraService(HttpClient httpClient, IConfiguration config, ILogger<JiraService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            var email = config["Jira:Email"];
            var token = config["Jira:ApiToken"];
            var base64Creds = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{email}:{token}"));

            _httpClient.BaseAddress = new Uri($"{config["Jira:BaseUrl"]}/rest/api/3/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Creds);
        }

        public async Task<List<JiraProject>> GetAllProjectsAsync(CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync("project/search", ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to fetch projects: {response.StatusCode}");
                return new List<JiraProject>();
            }

            var content = await response.Content.ReadAsStringAsync(ct);
            var data = JsonConvert.DeserializeObject<JiraProjectSearchResult>(content);

            var result = new List<JiraProject>();

            foreach (var p in data.Values)
            {
                var project = new JiraProject
                {
                    Id = p.Id,
                    Key = p.Key,
                    Name = p.Name,
                    ProjectTypeKey = p.ProjectTypeKey,
                    ProjectCategory = p.ProjectCategory?.Name ?? "Uncategorized",
                    LeadDisplayName = p.Lead?.DisplayName ?? "Unknown"
                };

                // Fetch project roles (URLs to roles like Admin/Member)
                var roleResp = await _httpClient.GetAsync($"project/{p.Id}/role", ct);
                if (roleResp.IsSuccessStatusCode)
                {
                    var roleContent = await roleResp.Content.ReadAsStringAsync(ct);
                    var roles = JsonConvert.DeserializeObject<Dictionary<string, string>>(roleContent);

                    if (roles.TryGetValue("Administrator", out var adminUrl))
                    {
                        project.Admins = await GetUsersInRoleAsync(adminUrl, ct);
                    }

                    if (roles.TryGetValue("Member", out var memberUrl))
                    {
                        project.Developers = await GetUsersInRoleAsync(memberUrl, ct);
                    }
                }

                result.Add(project);
            }

            return result;
        }



        async Task<List<JiraIssues>> IJiraService.GetIssuesAsync(string jql, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"search?jql={Uri.EscapeDataString(jql)}", ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to fetch issues: {response.StatusCode}");
                return new List<JiraIssues>();
            }

            var json = await response.Content.ReadAsStringAsync(ct);
            var data = JsonConvert.DeserializeObject<JiraSearchResult>(json);

            return data.Issues.Select(i => new JiraIssues
            {
                Key = i.Key,
                Summary = i.Fields.Summary,
                Status = i.Fields.Status.Name
            }).ToList();
        }



        private async Task<List<JiraUser>> GetUsersInRoleAsync(string roleUrl, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync(roleUrl, ct);
            if (!response.IsSuccessStatusCode)
                return new List<JiraUser>();

            var content = await response.Content.ReadAsStringAsync(ct);
            dynamic roleData = JsonConvert.DeserializeObject(content);

            var users = new List<JiraUser>();

            if (roleData.actors != null)
            {
                foreach (var actor in roleData.actors)
                {
                    if ((string)actor.type == "atlassian-user-role-actor")
                    {
                        users.Add(new JiraUser
                        {
                            AccountId = actor.accountId,
                            DisplayName = actor.displayName
                        });
                    }
                }
            }

            return users;
        }




    }
}
