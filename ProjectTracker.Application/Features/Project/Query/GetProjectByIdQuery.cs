using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProjectTracker.Application.Dtos.Project;

namespace ProjectTracker.Application.Features.Project.Query
{
    public class GetProjectByIdQuery :IRequest<Result<ProjectDto>>
    {
        public string ProjectId { get; set; }
    }
}
