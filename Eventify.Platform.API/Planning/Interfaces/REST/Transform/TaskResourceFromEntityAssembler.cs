using Eventify.Platform.API.Planning.Domain.Model.Aggregates;
using Eventify.Platform.API.Planning.Interfaces.REST.Resources;

namespace Eventify.Platform.API.Planning.Interfaces.REST.Transform;

public static class TaskResourceFromEntityAssembler
{
    public static TaskResource ToResourceFromEntity(TaskItem task)
    {
        return new TaskResource(
            task.Id,
            task.Title.Title,
            task.Description.Description,
            task.ColumnId.ColumnId,
            task.Order.Order,
            task.CreatedAt
        );
    }
}
