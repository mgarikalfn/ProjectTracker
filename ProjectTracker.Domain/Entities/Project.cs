using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Exception;
using static ProjectTracker.Domain.Enum.Enums;
using TaskStatus = ProjectTracker.Domain.Enum.Enums.TaskStatus;

namespace ProjectTracker.Domain.Entities
{
    public class Project:BaseEntity
    {
        public string ProjectId { get; private set; } // Unique identifier for the project
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ProjectStatus Status { get; private set; } 
        public ProjectPriority Priority { get; private set; } 
        public DateTime StartDate { get; private set; }
        public DateTime Deadline { get; private set; }
        public DateTime? CompletedDate { get; private set; }

        // Navigation Properties
        public virtual ICollection<ProjectTask> Tasks { get; private set; } = new List<ProjectTask>();
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

        public void SetStatus(ProjectStatus newStatus)
        {
            if (Status == ProjectStatus.Completed && newStatus != ProjectStatus.Completed)
            {
                throw new DomainException("Cannot change status from Completed");
            }

            if (newStatus == ProjectStatus.Completed && !Tasks.All(t => t.Status == TaskStatus.Done))
            {
                throw new DomainException("All tasks must be completed before completing project");
            }

            Status = newStatus;
            if(newStatus == ProjectStatus.Completed)
            {
                CompletedDate = DateTime.UtcNow;
            }
        }
    }
}
