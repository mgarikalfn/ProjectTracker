using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Domain.Entities
{
    public class ProjectUpdate:BaseEntity
    {
        public string Description { get; private set; }
        public UpdateType Type { get; private set; } // Enum: StatusChange, PriorityChange, etc.
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // Relationships
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        public Guid CreatedById { get; private set; }
        public virtual TeamMember CreatedBy { get; private set; }
    }
}
