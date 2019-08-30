using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public class Subtask : IToDoItem
    {
        private readonly IRepository<Subtask> _subtaskRepository;

        internal Subtask() { }
        internal Subtask(Guid id, string name, DateTime creationDate, Guid parentId)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            TaskItemId = parentId;
        }

        public Subtask(IRepository<Subtask> subtaskRepository)
        {
            this._subtaskRepository = subtaskRepository ?? throw new ArgumentNullException(nameof(subtaskRepository));
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "Items name should be at minimum 1 character long.")]
        [MaxLength(64, ErrorMessage = "Items name should be maximum of 64 characters long.")]
        public string Name { get; private set; }

        public DateTime CreationDate { get; private set; }

        public Guid TaskItemId { get; private set; }
        public TaskItem ParentTask { get; private set; }

        public void SetName(string name)
        {
            if (name is null || name == String.Empty)
                throw new ArgumentNullException(nameof(name), "Name can't be null or empty.");

            if (name.Length > 64)
                throw new ArgumentOutOfRangeException(nameof(name), "Name can not be longer than 64 characters.");

            Name = name;
        }
        
        public void SetParentTask(TaskItem parent)
        {
            if (parent != null)
            {
                ParentTask = parent;
            }
        }

        public async Task SaveItemAsync()
        {
            if (Name != null && Name != String.Empty)
            {
                CreationDate = DateTime.Now;
                await _subtaskRepository.SaveAsync();
            }
            else
                throw new ArgumentNullException(nameof(Name), "Name is not set! Enter a name for the new Todo-item before saving!");
        }
    }
}
