using ToDoApp.Application.DTOs;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllAsync();
        Task<TaskDto> GetByIdAsync(string idTask);
        Task<TaskDto> GetByUserAsync(string idUser);
        Task<bool> CreateAsync(CreateTasksDto taskToCreate);
        Task<bool> UpdateAsync(UpdateTaskDto taskToUpdate);
        Task<bool> CompleteTaskAsync(CompleteTaskDto taskToComplete);
        Task<bool> DeleteAsync(TaskDto taskToDelete);
    }
}
