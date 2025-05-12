using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Application.Dtos.Project;
using ProjectTracker.Domain.Entities;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Infrastructure.Extension
{
    public static  class ProjectQueryExtension
    {
        public static IQueryable<Project> ApplyFilter(this IQueryable<Project> query , string? name = null,
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
            int pageSize = 10)
        {
            //basic filter
            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (!string.IsNullOrEmpty(description))
                query = query.Where(p => p.Description.Contains(description));

            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(p => p.Priority == priority.Value);

            if (startDateFrom.HasValue)
                query = query.Where(p => p.StartDate >= startDateFrom.Value);

            if (startDateTo.HasValue)
                query = query.Where(p => p.StartDate <= startDateTo.Value);

            if (deadlineFrom.HasValue)
                query = query.Where(p => p.Deadline >= deadlineFrom.Value);

            if (deadlineTo.HasValue)
                query = query.Where(p => p.Deadline <= deadlineTo.Value);

            if (isCompleted.HasValue)
                query = query.Where(p => isCompleted.Value
                    ? p.Status == ProjectStatus.Completed
                    : p.Status != ProjectStatus.Completed);

            return query;
        }

        //Sorting

        public static IQueryable<Project> ApplySorting(this IQueryable<Project> query, string? name = null,
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
            int pageSize = 10)
        {
            return sortBy.ToLower() switch
            {
                "deadline" => sortDescending
                    ? query.OrderByDescending(p => p.Deadline)
                    : query.OrderBy(p => p.Deadline),
                "priority" => sortDescending
                    ? query.OrderByDescending(p => p.Priority)
                    : query.OrderBy(p => p.Priority),
                "status" => sortDescending
                    ? query.OrderByDescending(p => p.Status)
                    : query.OrderBy(p => p.Status),
                _ => sortDescending  // Default sort by Name
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name)
            };
        }

        //Pagination

        public static IQueryable<Project> ApplyPagination(this IQueryable<Project> query, string? name = null,
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
            int pageSize = 10)
        {
            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
