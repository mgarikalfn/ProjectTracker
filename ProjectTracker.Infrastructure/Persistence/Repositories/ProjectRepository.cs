using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Domain.Interface;
using ProjectTracker.Infrastructure.Data;
using ProjectTracker.Infrastructure.Extension;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository: Repository<Project>,IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Project>> GetProjectsByStatusAsync(ProjectStatus status)
        {
            return await _context.Projects
                .Where(p => p.Status == status)
                .ToListAsync();
        }

       

        Task<Project> IProjectRepository.GetProjectByIdAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        Task<Project> IRepository<Project>.FindAsync(Expression<Func<Project, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Project>> IProjectRepository.GetProjectsWithTasksAsync(string projectId)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.ProjectId == projectId)
                .ToListAsync();
        }

        async Task<PagedResult<Project>> IProjectRepository.GetFilteredProjectsAsync(string? name, string? description, ProjectStatus? status, ProjectPriority? priority, DateTime? startDateFrom, DateTime? startDateTo, DateTime? deadlineFrom, DateTime? deadlineTo, bool? isCompleted, string? sortBy, bool sortDescending, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Projects.AsQueryable();

            // Apply filters using extensions
            query = query
                .ApplyFilter(name, description, status,priority,startDateFrom,startDateTo,deadlineFrom,deadlineTo,isCompleted)
                .ApplySorting(sortBy);

            // Pagination
           
            var items = await query
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToListAsync(cancellationToken);

            return new  PagedResult<Project>
            {
                Items = items,
                TotalCount = await query.CountAsync(cancellationToken),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
