using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToDoApp.Domain
{
    public class TaskItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        //public string Title { get; private set; }

        //public void SetTitle(string newTitle)
        //{
        //    Title = newTitle;
        //}

        //public void Save()
        //{

        //}
    }
}
