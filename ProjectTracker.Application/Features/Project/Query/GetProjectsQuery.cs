using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProjectTracker.Application.Dtos.Project;
using ProjectTracker.Domain.Entities;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Application.Features.Project.Query
   
{
    public class GetProjectsQuery:IRequest<Result<PagedResult<ProjectDto>>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExternalProjectId { get; set; }
        public ProjectStatus? Status { get; set; }
        public ProjectPriority? Priority { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? DeadlineFrom { get; set; }
        public DateTime? DeadlineTo { get; set; }
        public bool? IsCompleted { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;

    }

}
