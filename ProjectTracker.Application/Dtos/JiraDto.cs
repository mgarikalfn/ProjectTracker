using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Application.Dtos
{
    public class JiraSearchResult
    {
        public List<JiraRawIssue> Issues { get; set; }
    }

    public class JiraRawIssue
    {
        public string Key { get; set; }
        public JiraFields Fields { get; set; }
    }

    public class JiraFields
    {
        public string Summary { get; set; }
        public JiraStatus Status { get; set; }
    }

    public class JiraStatus
    {
        public string Name { get; set; }
    }

    public class JiraProjectSearchResult
    {
        public List<JiraProjectRaw> Values { get; set; }
    }

    public class JiraProjectRaw
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string ProjectTypeKey { get; set; }
        public JiraProjectCategory ProjectCategory { get; set; }
        public JiraLead Lead { get; set; }
    }

    public class JiraProjectCategory
    {
        public string Name { get; set; }
    }

    public class JiraLead
    {
        public string DisplayName { get; set; }
    }

    public class JiraProjectRoleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<JiraActor> Actors { get; set; }
    }

    public class JiraActor
    {
        public string DisplayName { get; set; }
        public string AccountId { get; set; }
        public string Type { get; set; }
    }

    public class JiraContributorDto
    {
        public string AccountId { get; set; }
        public string DisplayName { get; set; }
    }

    public class ProjectContributorsResponse
    {
        public string ProjectKey { get; set; }
        public List<JiraContributorDto> Developers { get; set; }
        public List<JiraContributorDto> Admins { get; set; }
    }


}
