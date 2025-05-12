using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using ProjectTracker.Application.Dtos.Project;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Domain.Interface;

namespace ProjectTracker.Application.Features.Project.Query
{
    
public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, Result<PagedResult<ProjectDto>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

       

        async Task<Result<PagedResult<ProjectDto>>> IRequestHandler<GetProjectsQuery, Result<PagedResult<ProjectDto>>>.Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var query = await _projectRepository.GetFilteredProjectsAsync(request.Name, request.Description, request.Status, request.Priority, request.StartDateFrom, request.StartDateTo, request.DeadlineFrom, request.DeadlineTo, request.IsCompleted, request.SortBy, request.SortDescending, request.PageNumber, request.PageSize
, cancellationToken);

            var mappedProjects = query.Items.Select(project => _mapper.Map<ProjectDto>(project)).ToList();

            return new PagedResult<ProjectDto>
            {
                Items = mappedProjects,
                TotalCount = query.TotalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }
    }
}
