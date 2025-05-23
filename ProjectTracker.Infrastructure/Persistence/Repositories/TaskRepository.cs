﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Domain.Interface;
using ProjectTracker.Infrastructure.Data;

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

        Task<ProjectTask> IRepository<ProjectTask>.FindAsync(Expression<Func<ProjectTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
