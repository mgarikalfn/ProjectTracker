using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Application.Dtos.Project
{
    public class ProjectFilterDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectStatus? Status { get; set; }
        public ProjectPriority? Priority { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? DeadlineFrom { get; set; }
        public DateTime? DeadlineTo { get; set; }
        public bool? IsCompleted { get; set; }

        public int PageNumber { get; set; } = 1;  // Default to first page
        public int PageSize { get; set; } = 10;   // Default page size

        // Sorting properties (optional but recommended)
        public string SortBy { get; set; } = "Name"; // Default sort field
        public bool SortDescending { get; set; } = false;
    }
}
