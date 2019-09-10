using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public class Subtask : BaseEntity<Subtask>
    {
        internal Subtask() { }

        public Subtask(IRepository<Subtask> subtaskRepository) : base(subtaskRepository)
        {
        }

        public Guid TaskItemId { get; private set; }
        public TaskItem ParentTask { get; private set; }

        public DateTime Deadline { get; private set; }

        public void SetParentTask(TaskItem parent)
        {
            if (parent != null)
            {
                ParentTask = parent;
            }
        }

        public void SetDeadline(DateTime deadline)
        {
            Deadline = deadline;
        }
    }
}
