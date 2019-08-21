using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Domain
{
    public class ToDoItem
    {
        public string Name { get; private set; }

        public ICollection<TaskItem> TaskItems { get; private set; }

        public void SetName(string name)
        {
            if (name is null || name == String.Empty)
                throw new ArgumentNullException(nameof(name), "Name can't be null or empty.");

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

    }
}
