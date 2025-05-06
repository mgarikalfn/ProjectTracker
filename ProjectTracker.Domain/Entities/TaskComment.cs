using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Entities
{
    public class TaskComment:BaseEntity
    {
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // Relationships
        public Guid TaskId { get; private set; }
        public virtual ProjectTask Task { get; private set; }

        public Guid AuthorId { get; private set; }
        public virtual TeamMember Author { get; private set; }

        // Domain Methods
        public void UpdateContent(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                //throw new DomainException("Comment cannot be empty");

            Content = newContent;
        }
    }
}
