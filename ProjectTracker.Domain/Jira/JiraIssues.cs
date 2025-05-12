using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Jira
{
    public class JiraIssues
    {
        public string Status { get; set; }
        public string Key { get; set; }
        public string Summary { get; set; }
    }
}
