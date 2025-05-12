using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using ProjectTracker.Application.Dtos.Project;
using ProjectTracker.Domain.Interface;

namespace ProjectTracker.Application.Features.Project.Query
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public GetProjectByIdQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _projectRepository.GetProjectsWithTasksAsync(request.ProjectId);
            if (vehicle == null)
                return Result.Fail<ProjectDto>("Vehicle not found");
            var vehicleDto = _mapper.Map<ProjectDto>(vehicle);
            return Result.Ok(vehicleDto);
        }
    }
}
