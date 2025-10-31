using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class TaskService
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public TaskService()
        {
            // Add some sample tasks for testing
            _tasks.Add(new TaskItem
            {
                Id = _nextId++,
                Description = "Complete the assignment",
                IsCompleted = false,
                CreatedAt = DateTime.Now
            });
            _tasks.Add(new TaskItem
            {
                Id = _nextId++,
                Description = "Submit the code",
                IsCompleted = false,
                CreatedAt = DateTime.Now
            });
        }

        // Get all tasks
        public List<TaskItem> GetAllTasks()
        {
            return _tasks;
        }

        // Get task by ID
        public TaskItem? GetTaskById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        // Add a new task
        public TaskItem AddTask(TaskItem task)
        {
            task.Id = _nextId++;
            task.CreatedAt = DateTime.Now;
            _tasks.Add(task);
            return task;
        }

        // Update a task
        public TaskItem? UpdateTask(int id, TaskItem updatedTask)
        {
            var task = GetTaskById(id);
            if (task == null) return null;

            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;
            return task;
        }

        // Delete a task
        public bool DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task == null) return false;

            _tasks.Remove(task);
            return true;
        }
    }
}