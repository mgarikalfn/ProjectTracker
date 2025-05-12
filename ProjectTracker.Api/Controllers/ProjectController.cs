using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Application.Dtos.Project;
using ProjectTracker.Application.Features.Project.Command;
using ProjectTracker.Application.Features.Project.Query;
using ProjectTracker.Domain.Entities;
using static ProjectTracker.Domain.Enum.Enums;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProjectsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Create([FromBody] CreateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
        //return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery { ProjectId = id });
        return Ok(result);
    }

    //[HttpGet("filter")]
    //[AllowAnonymous]
    //public async Task<ActionResult<PagedResult<Project>>> FilterProject([FromBody] ProjectFilterDto query)
    //{
    //    var command = _mapper.Map<GetProjectsQuery>(query);
    //    var result = await _mediator.Send(command);
    //    return Ok(result);
    //}

    [HttpPut("{id}/{status}")]
    [Authorize(Roles = "Admin,ProjectManager")]
    public async Task<IActionResult> UpdateStatus(string id, ProjectStatus status)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _mediator.Send(new UpdateProjectStatusCommand
        {
            ProjectId = id,
            Status = status,
            ChangedByUserId = userId
        });
        return Ok(result);
    }

    [HttpPost("{id}/assign")]
    [Authorize(Roles = "Admin,ProjectManager")]
    public async Task<IActionResult> AssignUser(string id, [FromBody] AssignDeveloperToProjectDto request)
    {
        var command = _mapper.Map<AssignProjectToDeveloperCommand>(request);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        command.ProjectId = id;
        command.AssignedBy = userId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProjectDto request)
    {
        var command = _mapper.Map<UpdateProjectCommand>(request);
        command.projectId = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}