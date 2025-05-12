using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Entities;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Domain.Interface
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IReadOnlyList<Project>> GetProjectsByStatusAsync(ProjectStatus status);
        Task<IReadOnlyList<Project>> GetProjectsWithTasksAsync(string projectId);
        Task<Project> GetProjectByIdAsync(string projectId);

        Task<PagedResult<Project>> GetFilteredProjectsAsync(
            string? name = null,
            string? description = null,
            ProjectStatus? status = null,
            ProjectPriority? priority = null,
            DateTime? startDateFrom = null,
            DateTime? startDateTo = null,
            DateTime? deadlineFrom = null,
            DateTime? deadlineTo = null,
            bool? isCompleted = null,
            string? sortBy = "Name",
            bool sortDescending = false,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

    }

}
