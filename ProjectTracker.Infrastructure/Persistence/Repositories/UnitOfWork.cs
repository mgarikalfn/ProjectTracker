using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectTracker.Domain.Interface;
using ProjectTracker.Infrastructure.Data;

namespace ProjectTracker.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(
            AppDbContext context,
            ILogger<UnitOfWork> logger,
            IProjectRepository projectRepository,
            ITaskRepository taskRepository
        )
        {
            _context = context;
            _logger = logger;
            Projects = projectRepository;
            Tasks = taskRepository;
        }

        public IProjectRepository Projects { get; }
        public ITaskRepository Tasks { get; }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await CompleteAsync(cancellationToken);
            return true;
        }
    }
}
