﻿namespace PolisProReminder.Application.TodoTasks.Dtos;

public class TodoTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int Order { get; set; }
}
