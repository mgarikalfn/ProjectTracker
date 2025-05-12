namespace ProjectTracker.Domain.Entities
{
    public class ProjectAssignment:BaseEntity
    {
        public string ProjectId { get; set; }
        public string DeveloperId { get; set; }
        public DateTime AssignedDate { get; set; }
        public decimal AllocatedHoursPerWeek { get; set; } // e.g., 20h/week

        // Navigation
        public virtual Project Project { get; set; }
        public virtual TeamMember Developer { get; set; }
    }
}