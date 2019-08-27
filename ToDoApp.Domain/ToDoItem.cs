﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToDoApp.Domain
{
    public class ToDoItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "Items name should be at minimum 1 character long.")]
        [MaxLength(64, ErrorMessage = "Items name should be maximum of 64 characters long.")]
        public string Name { get; private set; }

        public ICollection<ToDoItem> TaskItems { get; private set; }
        public ToDoItem ParentTask { get; private set; }

        public void SetName(string name)
        {
            if (name is null || name == String.Empty)
                throw new ArgumentNullException(nameof(name), "Name can't be null or empty.");

            if (name.Length > 64)
                throw new ArgumentOutOfRangeException(nameof(name), "Name can not be longer than 64 characters.");

            Name = name;
        }

        public void AddTaskItem(ToDoItem item)
        {
            if (TaskItems is null)
            {
                TaskItems = new List<ToDoItem>();
            }

            TaskItems.Add(item);
        }

        public void SetParentTask(ToDoItem parent)
        {
            if (parent != null)
            {
                ParentTask = parent;
            }
        }

    }
}
