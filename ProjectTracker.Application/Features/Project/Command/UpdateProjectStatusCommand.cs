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
    public class UpdateProjectStatusCommand:IRequest<Result>
    {
        public string ProjectId { get; set; }
        public ProjectStatus Status { get; set; }
        public string ChangedByUserId { get; set; }
    }
}
