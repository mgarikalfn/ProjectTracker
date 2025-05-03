using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Entities
{
    public class Team:BaseEntity
    {
        public string Name { get; private set; }
        public string Department { get; private set; }

        // Relationships
        public Guid? TeamLeadId { get; private set; }
        public virtual TeamMember? TeamLead { get; private set; }

        public virtual ICollection<TeamMember> Members { get; private set; } = new List<TeamMember>();
        public virtual ICollection<Project> Projects { get; private set; } = new List<Project>();
    }
}
