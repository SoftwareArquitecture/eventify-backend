using Eventify.Platform.API.Planning.Domain.Model.Aggregates;
using Eventify.Platform.API.Planning.Domain.Model.Queries;

namespace Eventify.Platform.API.Planning.Domain.Services;

public interface ITaskQueryService
{
    Task<IEnumerable<TaskItem>> Handle(GetAllTasksQuery query);
    Task<TaskItem?> Handle(GetTaskByIdQuery query);
    Task<IEnumerable<TaskItem>> Handle(GetTasksByColumnIdQuery query);
    Task<object> Handle(GetTaskBoardQuery query);
}
