using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        ITaskRepository Tasks { get; }
       
        // Add other repositories as needed

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
