using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using ProjectTracker.Domain.Interface;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Application.Features.Project.Command
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                return Result.Fail<Guid>("Invalid project creation request");

            if (request.Deadline < DateTime.UtcNow.AddDays(1))
                return Result.Fail<Guid>("Deadline must be at least 1 day in the future");


            var project = _mapper.Map<ProjectTracker.Domain.Entities.Project>(request);


            await _projectRepository.AddAsync(project);
            return Result.Ok<Guid>(project.Id); // Assuming vehicleId is the created project entity
        }
    }
}
