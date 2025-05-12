using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Jira
{
    public class JiraProject
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string ProjectTypeKey { get; set; }
        public string ProjectCategory { get; set; }
        public string LeadDisplayName { get; set; }

        public List<JiraUser> Developers { get; set; } = new();
        public List<JiraUser> Admins { get; set; } = new();
    }


}
