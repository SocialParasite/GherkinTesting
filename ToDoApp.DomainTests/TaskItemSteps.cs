using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ToDoApp.Domain;

namespace ToDoApp.DomainTests
{
    [Binding]
    public class TaskItemSteps
    {
        private TaskItem _item;
        private Action _action;
        ToDoList _toDoList;
        Subtask _subtask;
        Mock<TaskItem> _mock;

        [Given(@"Jill has a ToDo-item open")]
        public void GivenJillNamesAToDo_Item()
        {
            _item = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
        }

        [When(@"she sets the name of the ToDo-item as (.*)")]
        public void WhenSheSetsTheNameOfTheToDo_ItemAsMyToDo_Item(string name)
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

        [When(@"she enters a name that is too long for the item")]
        public void WhenSheEntersANameThatIsTooLongForTheItem(string multilineText)
        {
            _action = () => _item.SetName(multilineText);
        }

        [Then(@"Jill should be informed that the name is too long for the item")]
        public void ThenJillShouldBeInformedThatTheNameIsTooLongForTheItem()
        {
            Assert.Throws<ArgumentOutOfRangeException>(_action.Invoke);
        }


        [Given(@"Jill wants to add a new task item")]
        public void GivenJillWantsToAddANewToDo_Item()
        {
            _item = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
        }

        [When(@"she enters a name for the task item as (.*)")]
        public void WhenSheEntersANameForTheToDo_ItemAsMyToDo_Item(string name)
        {
            _action = () => _item.SetName(name);
        }

        [Then(@"ToDo-item (.*) be saved")]
        public void ThenToDo_ItemMayBeSaved(string expectation)
        {
            bool expected = expectation == "may" ? true : false;

            if (expected)
            {
                Action action = async () => await _item.SaveItemAsync();
                Assert.DoesNotThrow(action.Invoke);
            }
            else
            {
                Func<Task> action = async () => await _item.SaveItemAsync();
                Assert.ThrowsAsync<ArgumentNullException>(action.Invoke);
            }
        }

        [When(@"she adds a subtask to the item")]
        public void WhenSheAddsATaskToTheItem()
        {
            _subtask = new Subtask(new Mock<IRepository<Subtask>>().Object);
        }

        [Then(@"subtask may be added to the ToDo-item")]
        public void ThenTaskMayBeAddedToTheToDo_Item()
        {
            _item.AddSubtask(_subtask);
            Assert.IsNotEmpty(_item.Subtasks);
        }

        [Given(@"Jill wants to add a task to ToDo-list")]
        public void GivenJillWantsToAddATaskToToDo_List()
        {
            _item = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
        }

        [When(@"she has a task selected")]
        public void WhenSheHasATaskSelected()
        {
            _item.SetName("Something to do.");
        }

        [When(@"she chooses a todo-list")]
        public void WhenSheChoosesATodo_List()
        {
            _toDoList = new ToDoList(new Mock<IRepository<ToDoList>>().Object);
            _toDoList.SetName("My todo");
        }

        [Then(@"the selected task is set as an item on the the chosen list")]
        public void ThenTheSelectedTaskIsSetAsAnItemOnTheTheChosenList()
        {
            _item.SetParentToDoList(_toDoList);
            Assert.That(_item.ParentList.Equals(_toDoList));
        }

        [Given(@"Jill adds a new ToDo-item")]
        public void GivenJillAddsANewToDo_Item()
        {
            _item = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
            _item.SetName("New Item 1");
        }

        [When(@"she saves it")]
        public async Task WhenHeSavesIt()
        {
            await _item.SaveItemAsync();
        }

        [Then(@"tasks creation date and time are logged")]
        public void ThenTasksCreationDateAndTimeAreLogged()
        {
            var creationDate = _item.CreationDate;
            Assert.That(creationDate, Is.EqualTo(DateTime.Now).Within(1).Minutes);
        }

        [Given(@"Jill has a task open")]
        public void GivenJillHasATaskOpen()
        {
            _item = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
            _item.SetName("Parent task");
        }

        [Given(@"Jill modifies a ToDo-item")]
        public void GivenJillModifiesAToDo_Item()
        {
            var repo = new Mock<IRepository<TaskItem>>().Object;
            _mock = new Mock<TaskItem>(repo);
            _mock.Object.SetName("Old item");
            
            _mock.SetupGet(x => x.CreationDate).Returns(DateTime.Now.AddDays(-1));
        }

        [When(@"she saves the modified item")]
        public async Task WhenSheSavesTheModifiedItem()
        {
            await _mock.Object.SaveItemAsync();
        }

        [Then(@"tasks creation date and time are not changed")]
        public void ThenTasksCreationDateAndTimeAreNotChanged()
        {
            Assert.That(_mock.Object.CreationDate, Is.Not.EqualTo(DateTime.Now).Within(1).Minutes);
        }

        //[Given(@"she has another task she wants to set as a subtask")]
        //public void GivenSheHasAnotherTaskSheWantsToSetAsASubtask()
        //{
        //    _subtask = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
        //    _subtask.SetName("Convert to subtask");
        //}

        //[Then(@"task should be added as a subtask to parent task")]
        //public void ThenTaskShouldBeAddedAsASubtaskToParentTask()
        //{
        //    _item.AddSubtask(_subtask);
        //    Assert.That(_item.Subtasks.Last(), Is.TypeOf<Subtask>());
        //}

    }
}
