using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Domain.Interface;

namespace ProjectTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JiraTestController : ControllerBase
    {
        private readonly IJiraService _jiraService;

        public JiraTestController(IJiraService jiraService)
        {
            _jiraService = jiraService;
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> Fetch()
        {
            var issues = await _jiraService.GetIssuesAsync("project = FDA");
            return Ok(issues);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _jiraService.GetAllProjectsAsync();
            return Ok(projects);
        }
    }

}
