﻿using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ToDoApp.Domain;

namespace ToDoApp.DomainTests
{
    [Binding]
    public class ItemSteps
    {
        private ToDoItem _item;
        private Action _action;
        ToDoList _toDoList;
        TaskItem _taskItem;
        
        [Given(@"Eddie names a ToDo-item")]
        public void GivenEddieNamesAToDo_Item()
        {
            _item = new ToDoItem();
        }

        [When(@"he sets the name of the ToDo-item as (.*)")]
        public void WhenHeSetsTheNameOfTheToDo_ItemAsMyToDo_Item(string name)
        {
            _action = () => _item.SetName(name);
        }

        [Then(@"item name (.*) be set")]
        public void ThenItemNameMayBeSet(string expectation)
        {
            bool expected = expectation == "may" ? true : false;

            if (expected)
            {
                Assert.DoesNotThrow(_action.Invoke);
            }
            else
            {
                Assert.Throws<ArgumentNullException>(_action.Invoke);
            }
        }

        [Given(@"Eddie wants to add a new ToDo-item")]
        public void GivenEddieWantsToAddANewToDo_Item()
        {
            _item = new ToDoItem();
            var toDoListRepositoryMock = new Mock<IToDoListRepository>();
            _toDoList = new ToDoList(toDoListRepositoryMock.Object);
        }

        [When(@"he enters a name for the ToDo-item as (.*)")]
        public void WhenHeEntersANameForTheToDo_ItemAsMyToDo_Item(string name)
        {
            _action = () => _item.SetName(name);
        }

        [Then(@"ToDo-item (.*) be saved as part of selected ToDo-list")]
        public void ThenToDo_ItemMayBeSavedAsPartOfSelectedToDo_List(string expectation)
        {
            bool expected = expectation == "may" ? true : false;

            if (expected)
            {
                _toDoList.AddTodoItem(_item);
                Action action = async () => await _toDoList.SaveAsync();
                Assert.DoesNotThrow(action.Invoke);
            }
            else
            {
                Func<Task> action = async () => await _toDoList.SaveAsync();
                Assert.ThrowsAsync<ArgumentNullException>(action.Invoke);
            }
        }

        [Given(@"Eddie has a ToDo-item open")]
        public void GivenEddieHasAToDo_ItemOpen()
        {
            _item = new ToDoItem();
        }

        [When(@"he adds a task to the item")]
        public void WhenHeAddsATaskToTheItem()
        {
            _taskItem = new TaskItem();
        }

        [Then(@"task may be added to the ToDo-item")]
        public void ThenTaskMayBeAddedToTheToDo_Item()
        {
            _item.AddTaskItem(_taskItem);
            Assert.IsNotEmpty(_item.TaskItems);
        }


    }
}