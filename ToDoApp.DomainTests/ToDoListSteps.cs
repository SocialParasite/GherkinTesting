using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ToDoApp.Domain;

namespace ToDoApp.DomainTests
{
    [Binding]
    public class ProjectSteps
    {
        private Mock<IToDoListRepository> _toDoListRepositoryMock;
        ToDoList _toDoList;
        private ToDoItem _todoItem;
        private Action _action;

        [Given(@"Eddie wants to create a new ToDo-list")]
        public void GivenEddieWantsToCreateANewToDo_List()
        {
            _toDoListRepositoryMock = new Mock<IToDoListRepository>();
            _toDoList = new ToDoList(_toDoListRepositoryMock.Object);
        }

        [When(@"he enters a valid name for the list")]
        public void WhenHeEntersAValidNameForTheList()
        {
            _toDoList.SetName("My ToDo-list");
        }

        [Then(@"the list may be created")]
        public void ThenTheListMayBeSaved()
        {
            Action _action = async () => await _toDoList.SaveAsync();
            Assert.DoesNotThrow(_action.Invoke);
        }

        [Given(@"Eddie wants to name a ToDo-list")]
        public void GivenEddieNamesAToDo_List()
        {
            _toDoListRepositoryMock = new Mock<IToDoListRepository>();

            _toDoList = new ToDoList(_toDoListRepositoryMock.Object);
        }

        [When(@"he sets the name of the ToDo-list to (.*)")]
        public void WhenHeSetsTheNameOfTheToDo_List(string name)
        {
            _action = () => _toDoList.SetName(name);
        }

        [Then(@"name (.*) be set")]
        public void ThenNameMayBeSet(string p0)
        {
            bool expected = p0 == "may" ? true : false;

            if (expected)
            {
                Assert.DoesNotThrow(_action.Invoke);
            }
            else
            {
                Assert.Throws<ArgumentNullException>(_action.Invoke);
            }
        }

        [When(@"he enters a name that is too long")]
        public void WhenHeEntersANameThatIsTooLong(string multilineText)
        {
            _action = () => _toDoList.SetName(multilineText);
        }

        [Then(@"Eddie should be informed that the name is too long")]
        public void ThenEddieShouldBeInformedThatTheNameIsTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(_action.Invoke);
        }

        [When(@"he enters a name for the ToDo-list as (.*)")]
        public void WhenHeSetsTheNameOfTheProject(string name)
        {
            if (name != null && name != String.Empty)
            {
                _toDoList.SetName(name);
            }
        }

        [Then(@"ToDo-list (.*) be saved")]
        public void ThenTaskBeSaved(string p0)
        {
            bool expected = p0 == "may" ? true : false;

            if (expected)
            {
                Action action = async () => await _toDoList.SaveAsync();
                Assert.DoesNotThrow(action.Invoke);
            }
            else
            {
                Func<Task> action = async () => await _toDoList.SaveAsync();
                Assert.ThrowsAsync<ArgumentNullException>(action.Invoke);
            }
        }

        [Given(@"Eddie has a ToDo-list open")]
        public void GivenEddieHasAToDo_ListOpen()
        {
            _toDoListRepositoryMock = new Mock<IToDoListRepository>();

            _toDoList = new ToDoList(_toDoListRepositoryMock.Object);
        }

        [When(@"he wants to add a new or existing ToDo-item to the list")]
        public void WhenHeAddsAnItemToTheList()
        {
            _todoItem = new ToDoItem();
        }

        [Then(@"item may be added to the ToDo-list")]
        public void ThenItemMayBeAddedToTheToDo_List()
        {
            _toDoList.AddTodoItem(_todoItem);
            Assert.IsNotEmpty(_toDoList.ToDoItems);
        }
    }
}
