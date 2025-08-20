using AutoMapper;
using ToDoApp.Application.DTOs;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Api.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskDto, UserTask>();
            CreateMap<UserTask, TaskDto>();
            CreateMap<CreateTasksDto, UserTask>();
            CreateMap<UserTask, CreateTasksDto>();
            CreateMap<UserTask, CompleteTaskDto>();
            CreateMap<CompleteTaskDto, UserTask>();
            CreateMap<UserTask, UpdateTaskDto>();
            CreateMap<UpdateTaskDto, UserTask>();
        }
    }
}
