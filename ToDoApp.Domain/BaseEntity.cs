using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain
{
    public class BaseEntity<T> : IRepository<T>
    {
        protected readonly IRepository<T> _repository;

        public BaseEntity(IRepository<T> repository = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; protected set; }

        [Required]
        [MinLength(1, ErrorMessage = "ToDo-list name should be at minimum 1 character long.")]
        [MaxLength(64, ErrorMessage = "ToDo-list name should be maximum of 64 characters long.")]
        public string Name { get; protected set; }

        public virtual DateTime CreationDate { get; protected set; }

        public void SetName(string name)
        {
            if (name is null || name == String.Empty)
                throw new ArgumentNullException(nameof(name), "Name can't be null or empty.");

            if (name.Length > 64)
                throw new ArgumentOutOfRangeException(nameof(name), "Name can not be longer than 64 characters.");

            Name = name;
        }

        public async Task SaveItemAsync()
        {
            if (Name != null && Name != String.Empty)
            {
                if(CreationDate == default)
                    CreationDate = DateTime.Now;

                await _repository.SaveItemAsync();
            }
            else
                throw new ArgumentNullException(nameof(Name), "Name is not set! Enter a name for the new Todo-list before saving!");
        }
    }
}
