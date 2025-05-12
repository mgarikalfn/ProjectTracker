using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Application.Features.Project.Command
{
    public class CreateProjectCommand:IRequest<Result<Guid>>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ProjectStatus Status { get; private set; }
        public ProjectPriority Priority { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime Deadline { get; private set; }
        public DateTime? CompletedDate { get; private set; }
        public string? ExternalProjectId { get; private set; }
    }
}
