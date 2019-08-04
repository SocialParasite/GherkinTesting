using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public class Project
    {
        private readonly IProjectRepository _projectRepository;

        public Project(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public string Name { get; private set; }
        public ICollection<ToDoItem> ToDoItems { get; private set; }

        public void SetName(string name)
        {
            if (name is null || name == String.Empty)
                throw new ArgumentNullException(nameof(name), "Name can't be null or empty.");

            Name = name;
        }

        public void AddTodoItem(ToDoItem item)
        {
            if (ToDoItems is null)
            {
                ToDoItems = new List<ToDoItem>();
            }

            ToDoItems.Add(item);
        }

        public async Task SaveAsync()
        {
            if (Name != null && Name != String.Empty)
            {
                await _projectRepository.SaveAsync();
            }
            else
                throw new ArgumentNullException(nameof(Name), "Name is not set! Enter a name for the new Todo-list before saving!");
        }
    }
}
