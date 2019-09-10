using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Domain
{
    public class Category : BaseEntity<Category>
    {
        public Category() { }

        public Category(IRepository<Category> categoryRepository) : base(categoryRepository) { }
    }
}
