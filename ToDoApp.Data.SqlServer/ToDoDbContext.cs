using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Domain;

namespace ToDoApp.Data.SqlServer
{
    public class ToDoDbContext : DbContext
    {

        public ToDoDbContext() { }

        public ToDoDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string connectionString;
        public static IConfigurationRoot Configuration { get; private set; }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<TaskItem> ToDoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString is null)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("connectionString.json");

                Configuration = builder.Build();

                connectionString = Configuration.GetConnectionString("ToDoAppDEV");
                optionsBuilder.UseSqlServer(connectionString);
            }
            else
                optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
