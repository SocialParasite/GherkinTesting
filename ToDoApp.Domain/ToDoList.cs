using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public class ToDoList
    {
        internal ToDoList() { }

        public ToDoList(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository ?? throw new ArgumentNullException(nameof(toDoListRepository));
        }

        private readonly IToDoListRepository _toDoListRepository;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "ToDo-list name should be at minimum 1 character long.")]
        [MaxLength(64, ErrorMessage = "ToDo-list name should be maximum of 64 characters long.")]
        public string Name { get; private set; }

        public ICollection<TaskItem> TaskItems { get; private set; }

        public void SetName(string name)
        {
            if (name is null || name == String.Empty)
                throw new ArgumentNullException(nameof(name), "Name can't be null or empty.");

            if (name.Length > 64)
                throw new ArgumentOutOfRangeException(nameof(name), "Name can not be longer than 64 characters.");

            Name = name;
        }

        public void AddTaskItem(TaskItem item)
        {
            if (TaskItems is null)
            {
                TaskItems = new List<TaskItem>();
            }

            TaskItems.Add(item);
        }

        public async Task SaveAsync()
        {
            if (Name != null && Name != String.Empty)
            {
                await _toDoListRepository.SaveAsync();
            }
            else
                throw new ArgumentNullException(nameof(Name), "Name is not set! Enter a name for the new Todo-list before saving!");
        }
    }
}
