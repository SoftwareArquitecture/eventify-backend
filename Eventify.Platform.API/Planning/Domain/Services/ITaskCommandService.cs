using Eventify.Platform.API.Planning.Domain.Model.Aggregates;
using Eventify.Platform.API.Planning.Domain.Model.Commands;

namespace Eventify.Platform.API.Planning.Domain.Services;

public interface ITaskCommandService
{
    Task<TaskItem?> Handle(CreateTaskCommand command);
    Task<TaskItem?> Handle(UpdateTaskCommand command);
    Task<TaskItem?> Handle(DeleteTaskCommand command);
    Task<TaskItem?> Handle(MoveTaskCommand command);
}
