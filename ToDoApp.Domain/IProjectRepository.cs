using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public interface IProjectRepository
    {
        Task SaveAsync();
    }
}
