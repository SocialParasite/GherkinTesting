using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ToDoApp.Domain;

namespace ToDoApp.DomainTests
{
    [Binding]
    public class AddANewProjectSteps
    {
        private Mock<IProjectRepository> projectRepositoryMock;
        Project _project;
        private ToDoItem _todoItem;
        private Action _action;

        [Given(@"Eddie names a ToDo-list")]
        public void GivenEddieNamesAToDo_List()
        {
            projectRepositoryMock = new Mock<IProjectRepository>();

            _project = new Project(projectRepositoryMock.Object);
        }

        [When(@"he sets the name of the ToDo-list as (.*)")]
        public void WhenHeSetsTheNameOfTheToDo_List(string name)
        {
            _action = () =>_project.SetName(name);
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

        [Given(@"Eddie wants to add a new ToDo-list")]
        public void GivenEddieWantsToAddANewProject()
        {
            projectRepositoryMock = new Mock<IProjectRepository>();

            _project = new Project(projectRepositoryMock.Object);    
        }

        [When(@"he enters a name for the ToDo-list as (.*)")]
        public void WhenHeSetsTheNameOfTheProject(string name)
        {
            if (name != null && name != String.Empty)
            {
                _project.SetName(name);
            }
        }

        [Then(@"ToDo-list (.*) be saved")]
        public void ThenTaskBeSaved(string p0)
        {
            bool expected = p0 == "may" ? true : false;

            if (expected)
            {
                Action action = async () => await _project.SaveAsync();
                Assert.DoesNotThrow(action.Invoke);
            }
            else
            {
                Func<Task> action = async () => await _project.SaveAsync();
                Assert.ThrowsAsync<ArgumentNullException>(action.Invoke);
            }
        }

        [Given(@"Eddie has a ToDo-list open")]
        public void GivenEddieHasAToDo_ListOpen()
        {
            projectRepositoryMock = new Mock<IProjectRepository>();

            _project = new Project(projectRepositoryMock.Object);
        }

        [When(@"he adds an item to the list")]
        public void WhenHeAddsAnItemToTheList()
        {
            _todoItem = new ToDoItem();
        }

        [Then(@"item may be added to the ToDo-list")]
        public void ThenItemMayBeAddedToTheToDo_List()
        {
            _project.AddTodoItem(_todoItem);
            Assert.IsNotEmpty(_project.ToDoItems);
        }




    }
}
