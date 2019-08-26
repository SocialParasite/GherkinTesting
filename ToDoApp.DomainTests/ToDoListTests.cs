using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Domain;

namespace ToDoApp.DomainTests
{
    [TestFixture]
    public class ToDoListTests
    {
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ToDoListName(string name)
        {
            var todoListRepositoryMock = new Mock<IToDoListRepository>();
            var todoList = new ToDoList(todoListRepositoryMock.Object);

            Assert.Throws<ArgumentNullException>(() => todoList.SetName(name));
        }

        [Test]
        public void TooLongToDoListName()
        {
            var todoListRepositoryMock = new Mock<IToDoListRepository>();
            var todoList = new ToDoList(todoListRepositoryMock.Object);

            Action action = () => todoList.SetName("I don’t know what I should name this list, so I’m just typing some random stuff here.");

            Assert.Throws<ArgumentOutOfRangeException>(action.Invoke);
        }
    }
}
