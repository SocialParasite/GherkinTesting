using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public interface IToDoListRepository : IRepository<ToDoList>
    {
        //Task SaveAsync();
    }
}
