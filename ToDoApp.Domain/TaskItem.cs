﻿using System;
using System.Collections.Generic;

namespace ToDoApp.Domain
{
    public class TaskItem : BaseEntity<TaskItem>
    {
        internal TaskItem() { }

        private DateTime _deadline;

        public TaskItem(IRepository<TaskItem> toDoItemRepository) : base(toDoItemRepository) { }

        public ICollection<TaskItem> Subtasks { get; private set; }
        public ToDoList ParentList { get; private set; }


        public ICollection<Category> Categories { get; private set; }

        public void AddSubtask(TaskItem item)
        {
            if (Subtasks is null)
            {
                Subtasks = new List<TaskItem>();
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
            => _deadline = deadline.ToUniversalTime();

        public DateTime GetDeadline() 
            => _deadline == default ? default : _deadline.ToLocalTime();

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
