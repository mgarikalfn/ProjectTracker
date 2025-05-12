using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Interface;

namespace ProjectTracker.Application.Features.Project.Command
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<string>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<Result<string>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.projectId);
            if (project == null)
            {
                return Result.Fail<string>("Project not found");
            }

            _mapper.Map(request, project);
            try
            {
                var updateResult = await _projectRepository.UpdateAsync(project);
                return updateResult ? Result.Ok(project.ProjectId) : Result.Fail<string>("Update failed");
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Update failed: {ex.Message}");
            }
        }
    }
}
