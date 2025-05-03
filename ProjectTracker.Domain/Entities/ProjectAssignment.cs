namespace ProjectTracker.Domain.Entities
{
    public class ProjectAssignment:BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid TeamMemberId { get; set; }
        public DateTime AssignedDate { get; set; }
        public decimal AllocationPercentage { get; set; } // 0-100%

        // Navigation
        public virtual Project Project { get; set; }
        public virtual TeamMember TeamMember { get; set; }
    }
}