using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Domain.Interface;
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

        public async Task<IReadOnlyList<Project>> GetProjectsWithTasksAsync()
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .ToListAsync();
        }
    }
}
