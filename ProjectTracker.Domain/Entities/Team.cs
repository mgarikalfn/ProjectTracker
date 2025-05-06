using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Identity;

namespace ProjectTracker.Domain.Entities
{
    public class Team:BaseEntity
    {
        public string Name { get; private set; }
        public string Department { get; private set; }

        // Relationships
        public string? TeamLeadId { get; private set; }
        public virtual AppUser? TeamLead { get; private set; }

        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
        public ICollection<ProjectAssignment> ProjectAssignments { get; set; } = new List<ProjectAssignment>();
    }
}
