using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Domain.Interface;

namespace ProjectTracker.Infrastructure.Persistence.Repositories
{
    public class TaskRepository:Repository<ProjectTask> , ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyList<ProjectTask>> GetTasksByAssigneeAsync(string assigneeId)
        {
            return await _context.Tasks
                .Where(t => t.AssigneeId == assigneeId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProjectTask>> GetOverdueTasksAsync()
        {
            return await _context.Tasks
                .Where(t => t.DueDate < DateTime.UtcNow && t.Status != ProjectTracker.Domain.Enum.Enums.TaskStatus.Done)
                .ToListAsync();
        }
    }
}
