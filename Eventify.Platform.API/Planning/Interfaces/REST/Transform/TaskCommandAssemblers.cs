using Eventify.Platform.API.Planning.Domain.Model.Commands;
using Eventify.Platform.API.Planning.Interfaces.REST.Resources;

namespace Eventify.Platform.API.Planning.Interfaces.REST.Transform;

public static class CreateTaskCommandFromResourceAssembler
{
    public static CreateTaskCommand ToCommandFromResource(CreateTaskResource resource)
    {
        return new CreateTaskCommand(
            resource.Title,
            resource.Description,
            resource.ColumnId,
            resource.Order
        );
    }
}

public static class UpdateTaskCommandFromResourceAssembler
{
    public static UpdateTaskCommand ToCommandFromResource(int taskId, UpdateTaskResource resource)
    {
        return new UpdateTaskCommand(
            taskId,
            resource.Title,
            resource.Description,
            resource.ColumnId,
            resource.Order
        );
    }
}

public static class MoveTaskCommandFromResourceAssembler
{
    public static MoveTaskCommand ToCommandFromResource(int taskId, MoveTaskResource resource)
    {
        return new MoveTaskCommand(
            taskId,
            resource.TargetColumnId,
            resource.Order
        );
    }
}
