using Eventify.Platform.API.Planning.Domain.Model.Aggregates;
using Eventify.Platform.API.Shared.Domain.Repositories;

namespace Eventify.Platform.API.Planning.Domain.Repositories;

public interface ITaskRepository : IBaseRepository<TaskItem>
{
    Task<IEnumerable<TaskItem>> FindAllAsync();
    new Task<TaskItem?> FindByIdAsync(int id);
    Task<IEnumerable<TaskItem>> FindByColumnIdAsync(int columnId);
    Task<IEnumerable<TaskItem>> FindByColumnIdOrderedAsync(int columnId);
}
