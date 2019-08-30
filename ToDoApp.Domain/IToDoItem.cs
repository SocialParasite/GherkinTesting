using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public interface IToDoItem
    {
        DateTime CreationDate { get; }
        Guid Id { get; }
        string Name { get; }

        Task SaveItemAsync();
        void SetName(string name);
    }
}
