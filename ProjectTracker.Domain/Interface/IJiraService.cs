using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Jira;

namespace ProjectTracker.Domain.Interface
{
    public interface IJiraService
    {
        Task<List<JiraIssues>> GetIssuesAsync(string jql, CancellationToken ct = default);
        Task<List<JiraProject>> GetAllProjectsAsync(CancellationToken ct = default);
    }
}
