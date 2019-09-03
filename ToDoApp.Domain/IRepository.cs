using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public interface IRepository<T> 
    {
        Task SaveItemAsync();
    }
}
