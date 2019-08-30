using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public class TaskItem : IToDoItem
    {
        private readonly IRepository<TaskItem> _toDoItemRepository;

        internal TaskItem() { }

        public TaskItem(IRepository<TaskItem> toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository ?? throw new ArgumentNullException(nameof(toDoItemRepository));
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "Items name should be at minimum 1 character long.")]
        [MaxLength(64, ErrorMessage = "Items name should be maximum of 64 characters long.")]
        public string Name { get; private set; }

        public DateTime CreationDate { get; private set; }

        public ICollection<Subtask> Subtasks { get; private set; }
        public ToDoList ParentList { get; private set; }

        public void SetName(string name)
        {
            if (name is null || name == String.Empty)
                throw new ArgumentNullException(nameof(name), "Name can't be null or empty.");

            if (name.Length > 64)
                throw new ArgumentOutOfRangeException(nameof(name), "Name can not be longer than 64 characters.");

            Name = name;
        }

        public void AddSubtask(IToDoItem item)
        {
            if (Subtasks is null)
            {
                Subtasks = new List<Subtask>();
            }

            if (item is TaskItem)
            {
                Subtasks.Add(ConvertToSubtask(item as TaskItem));
            }
            else
                Subtasks.Add(item as Subtask);
        }

        public void SetParentToDoList(ToDoList parent)
        {
            if (parent != null)
            {
                ParentList = parent;
            }
        }

        public async Task SaveItemAsync()
        {
            if (Name != null && Name != String.Empty)
            {
                CreationDate = DateTime.Now;
                await _toDoItemRepository.SaveAsync();
            }
            else
                throw new ArgumentNullException(nameof(Name), "Name is not set! Enter a name for the new Todo-item before saving!");
        }

        public Subtask ConvertToSubtask(TaskItem taskItem)
        {
            return new Subtask(taskItem.Id, Name = taskItem.Name, taskItem.CreationDate, this.Id);
        }
    }
}
