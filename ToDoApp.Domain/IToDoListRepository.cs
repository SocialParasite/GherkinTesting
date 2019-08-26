using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public interface IToDoListRepository
    {
        Task SaveAsync();
    }
}
