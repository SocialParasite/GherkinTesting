namespace ToDoApp.Domain
{
    public class Category : BaseEntity<Category>
    {
        public Category() { }

        public Category(IRepository<Category> categoryRepository) : base(categoryRepository) { }
    }
}
