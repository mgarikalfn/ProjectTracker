using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Domain.Entities
{
    public class Project:BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ProjectStatus Status { get; private set; } 
        public ProjectPriority Priority { get; private set; } 
        public DateTime StartDate { get; private set; }
        public DateTime Deadline { get; private set; }
        public DateTime? CompletedDate { get; private set; }

        // Navigation Properties
        public virtual Team LeadTeam { get; private set; }
        public Guid? LeadTeamId { get; private set; }

        public virtual ICollection<ProjectTask> Tasks { get; private set; } = new List<ProjectTask>();
        public virtual ICollection<TeamMember> AssignedMembers { get; private set; } = new List<TeamMember>();
        public virtual ICollection<ProjectUpdate> Updates { get; private set; } = new List<ProjectUpdate>();

        public string? ExternalProjectId { get; private set; }
        public string? ExternalSystem { get; private set; }

        // Modify assignments to use join table
        public virtual ICollection<ProjectAssignment> Assignments { get; private set; } = new List<ProjectAssignment>();
        // Domain Methods
        public void MarkAsCompleted()
        {
            Status = ProjectStatus.Completed;
            CompletedDate = DateTime.UtcNow;
        }
    }
}
