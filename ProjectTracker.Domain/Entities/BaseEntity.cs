using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Entities
{
    public abstract class BaseEntity
    {
        // Primary Key
        public Guid Id { get; protected set; } = Guid.NewGuid();

        // Auditing
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; protected set; }
        public string? LastModifiedBy { get; protected set; }

        // Soft Delete (Optional)
       // public bool IsDeleted { get; protected set; }

        // Domain Events (Advanced)
        //private readonly List<IDomainEvent> _domainEvents = new();
        //public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        // Methods
        public void UpdateModified(string modifiedByUserId)
        {
            ModifiedAt = DateTime.UtcNow;
            LastModifiedBy = modifiedByUserId;
        }

       /* public void SoftDelete()
        {
            IsDeleted = true;
            ModifiedAt = DateTime.UtcNow;
        }

        public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);
        public void ClearDomainEvents() => _domainEvents.Clear();
       */
    }
}
