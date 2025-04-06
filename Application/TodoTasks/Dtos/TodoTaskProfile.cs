using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.TodoTasks.Dtos;

internal class TodoTaskProfile : Profile
{
    public TodoTaskProfile()
    {
        CreateMap<TodoTask, TodoTaskDto>();
    }
}
