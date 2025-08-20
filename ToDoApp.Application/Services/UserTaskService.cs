using AutoMapper;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services
{
    public class UserTaskService : ITaskService
    {
        private readonly IGenericRepository<UserTask> _taskRepository;
        private readonly IMapper _mapper;

        public UserTaskService(IGenericRepository<UserTask> taskRepository, IMapper mappper)
        {
            _taskRepository = taskRepository;
            _mapper = mappper;
        }

        public async Task<bool> CreateAsync(CreateTasksDto taskToCreate)
        {
            var newTask = _mapper.Map<UserTask>(taskToCreate);
            await _taskRepository.CreateAsync(newTask);
            return newTask.Id != Guid.Empty;
        }

        public async Task<bool> DeleteAsync(TaskDto taskToDelete)
        {
            var task = _mapper.Map<UserTask>(taskToDelete);
            await _taskRepository.DeleteAsync(task);
            var isDeleted = await _taskRepository.GetByIdAsync(task.Id);
            return isDeleted == null;
        }

        public async Task<IEnumerable<TaskDto>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return result;
        }

        public Task<TaskDto> GetByIdAsync(string idTask)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> GetByUserAsync(string idUser)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(UpdateTaskDto taskToUpdate)
        {
            var newTask = _mapper.Map<UserTask>(taskToUpdate);
            newTask.LastUpdatedDate = DateTime.Now;
            await _taskRepository.UpdateAsync(newTask);
            return newTask.Id != Guid.Empty;
        }

        public async Task<bool> CompleteTaskAsync(CompleteTaskDto completeTask)
        {
            var task = await _taskRepository.GetByIdAsync(completeTask.Id);
            task.IsCompleted = completeTask.IsCompleted;
            task.CompletedDate = task.IsCompleted ? DateTime.Now : null;
            await _taskRepository.UpdateAsync(task);
            return true;
        }
    }
}
