using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Entities;

namespace ProjectTracker.Domain.Interface
{
    public interface ITaskRepository:IRepository<ProjectTask>
    {
        Task<IReadOnlyList<ProjectTask>> GetTasksByAssigneeAsync(string assigneeId);
        Task<IReadOnlyList<ProjectTask>> GetOverdueTasksAsync();
    }
}
