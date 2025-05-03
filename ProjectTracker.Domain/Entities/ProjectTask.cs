using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectTracker.Domain.Enum.Enums;
using TaskStatus = ProjectTracker.Domain.Enum.Enums.TaskStatus;

namespace ProjectTracker.Domain.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatus Status { get; private set; } // Enum
        public TaskType Type { get; private set; } // Enum (Feature/Bug/Improvement)
        public DateTime CreatedDate { get; private set; }
        public DateTime? DueDate { get; private set; }

        // Relationships
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        public Guid? AssigneeId { get; private set; }
        public virtual TeamMember? Assignee { get; private set; }
        public string? ExternalTaskId { get; private set; } // Jira/DevOps ID
        public string? ExternalSystem { get; private set; } // "Jira"/"AzureDevOps"
        public DateTime? LastSynced { get; private set; }
        public virtual ICollection<TaskComment> Comments { get; private set; } = new List<TaskComment>();
    }
}
