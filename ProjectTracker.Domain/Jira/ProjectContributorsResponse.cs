using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Jira
{
    public class ProjectContributorsResponse
    {
        public string ProjectKey { get; set; }
        public List<JiraContributorDto> Developers { get; set; }
        public List<JiraContributorDto> Admins { get; set; }
    }
}
