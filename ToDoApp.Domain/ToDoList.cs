using System.Collections.Generic;

namespace ToDoApp.Domain
{
    public class ToDoList : BaseEntity<ToDoList>
    {
        internal ToDoList() { }

        public ToDoList(IRepository<ToDoList> toDoListRepository) : base(toDoListRepository)
        {
        }

        public ICollection<TaskItem> TaskItems { get; private set; }

        public void AddTaskItem(TaskItem item)
        {
            if (TaskItems is null)
            {
                TaskItems = new List<TaskItem>();
            }

            TaskItems.Add(item);
        }
    }
}
