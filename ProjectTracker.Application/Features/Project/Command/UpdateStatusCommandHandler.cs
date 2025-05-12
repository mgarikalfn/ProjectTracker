using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProjectTracker.Domain.Interface;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Application.Features.Project.Command
{
    public class UpdateStatusCommandHandler:IRequestHandler<UpdateProjectStatusCommand,Result>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _repository;
        public async Task<Result> Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.ProjectId);
            if (project == null) return Result.Fail("Project not found");

            if (request.Status == ProjectStatus.Completed)
            {
                project.MarkAsCompleted();
            }
            else
            {
                project.SetStatus(request.Status);
            }

           // await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
