using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public class TaskItem : BaseEntity<TaskItem>, IToDoItem
    {
        internal TaskItem() { }

        public TaskItem(IRepository<TaskItem> toDoItemRepository) : base(toDoItemRepository) { }

        public ICollection<Subtask> Subtasks { get; private set; }
        public ToDoList ParentList { get; private set; }

        public void AddSubtask(IToDoItem item)
        {
            if (Subtasks is null)
            {
                Subtasks = new List<Subtask>();
            }

            if (item is Subtask)
            {
                Subtasks.Add(item as Subtask);
            }
            //else
            //    Subtasks.Add(ConvertToSubtask(item as TaskItem));
        }

        public void SetParentToDoList(ToDoList parent)
        {
            if (parent != null)
            {
                ParentList = parent;
            }
        }

        ////public Subtask ConvertToSubtask(TaskItem taskItem)
        ////{
        ////    return new Subtask(taskItem.Id, Name = taskItem.Name, taskItem.CreationDate, this.Id);
        ////}
    }
}
