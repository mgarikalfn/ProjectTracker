using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Entities;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Domain.Interface
{
    public interface IProjectRepository:IRepository<Project>
    {
        Task<IReadOnlyList<Project>> GetProjectsByStatusAsync(ProjectStatus status);
        Task<IReadOnlyList<Project>> GetProjectsWithTasksAsync();
    }
}
