using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public class TaskItem : BaseEntity<TaskItem>
    {
        internal TaskItem() { }

        public TaskItem(IRepository<TaskItem> toDoItemRepository) : base(toDoItemRepository) { }

        public ICollection<Subtask> Subtasks { get; private set; }
        public ToDoList ParentList { get; private set; }

        public DateTime Deadline { get; private set; }

        public ICollection<Category> Categories { get; private set; }

        public void AddSubtask(Subtask item)
        {
            if (Subtasks is null)
            {
                Subtasks = new List<Subtask>();
            }

            Subtasks.Add(item);
        }

        public void SetParentToDoList(ToDoList parent)
        {
            if (parent != null)
            {
                ParentList = parent;
            }
        }

        public void SetDeadline(DateTime deadline)
        {
            Deadline = deadline;
        }

        public void AddCategory(Category category)
        {
            if (Categories is null)
            {
                Categories = new List<Category>();
            }

            if (category != null)
            {
                Categories.Add(category);
            }
        }
    }
}
