using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Application.Dtos.ProjectAssignment;
using ProjectTracker.Application.Dtos.ProjectTask;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Application.Dtos.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public bool IsOverdue { get; set; }
        public ICollection<TaskSummaryDto> Tasks { get; set; }
        public ICollection<AssignmentDto> Assignments { get; set; }
    }
}
