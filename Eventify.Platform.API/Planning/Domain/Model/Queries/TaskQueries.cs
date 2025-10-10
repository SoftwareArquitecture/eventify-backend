namespace Eventify.Platform.API.Planning.Domain.Model.Queries;

public record GetAllTasksQuery();

public record GetTaskByIdQuery(int Id);

public record GetTasksByColumnIdQuery(int ColumnId);

public record GetTaskBoardQuery(int BoardId);
