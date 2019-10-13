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
        private TaskItem _childTask;
        private Action _action;
        ToDoList _toDoList;
        Mock<TaskItem> _mock;
        private Category _category;

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
            _childTask = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
        }

        [Then(@"subtask may be added to the ToDo-item")]
        public void ThenTaskMayBeAddedToTheToDo_Item()
        {
            _item.AddSubtask(_childTask);
            Assert.IsNotEmpty(_item.Subtasks);
        }

        [Given(@"Jill wants to add a subtask to task item")]
        public void GivenJillWantsToAddASubtaskToTaskItem()
        {
            _childTask = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
        }

        [When(@"she has a task selected that she wants to set as subtask")]
        public void WhenSheHasATaskSelectedThatSheWantsToSetAsSubtask()
        {
            _childTask.SetName("childtask");
        }

        [When(@"she chooses a parent task item")]
        public void WhenSheChoosesAParentTaskItem()
        {
            _item = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
        }

        [Then(@"the selected task is set as an child task to the the chosen task item")]
        public void ThenTheSelectedTaskIsSetAsAnChildTaskToTheTheChosenTaskItem()
        {
            _childTask.AddSubtask(_childTask);
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
            var creationDate = _item.GetCreationDate();
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
            Assert.That(_mock.Object.GetCreationDate, Is.Not.EqualTo(DateTime.Now).Within(1).Minutes);
        }

        [Given(@"Eddie wants to set a deadline for the task")]
        public void GivenEddieWantsToSetADeadlineForTheTask()
        {
            _item = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
        }

        [When(@"he has a task selected")]
        public void WhenHeHasATaskSelected()
        {
            _item.SetName("Task with a deadline");
        }

        [Then(@"he should be able to select a deadline date for the task")]
        public void ThenHeShouldBeAbleToSelectADeadlineDateForTheTask()
        {
            Assert.That(_item.GetDeadline(), Is.EqualTo(default(DateTime)));
            var myDeadline = DateTime.Today.AddDays(10);
            _item.SetDeadline(myDeadline);
            Assert.That(_item.GetDeadline(), Is.Not.EqualTo(default(DateTime)));
            Assert.That(_item.GetDeadline(), Is.EqualTo(myDeadline));
        }

        [Given(@"Eddie has a task with (.*) categories")]
        public void GivenEddieHasATaskWithCategories(int count)
        {
            _item = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
            _item.SetName("Task with categories");

            if (count > 0)
            {
                var cat = new Category(new Mock<IRepository<Category>>().Object);
                cat.SetName("Category 1");
                _item.AddCategory(cat);
                Assert.That(_item.Categories, Is.Not.Empty);
            }
            else
                Assert.That(_item.Categories, Is.Null);
        }

        [When(@"he sets a category for the task")]
        public void WhenHeSetsACategoryForTheTask()
        {
            var newCat = new Category(new Mock<IRepository<Category>>().Object);
            newCat.SetName("New Category");
            _item.AddCategory(newCat);
        }

        [Then(@"task should be added to that category")]
        public void ThenTaskShouldBeAddedToThatCategory()
        {
            Assert.That(_item.Categories.Any(c => c.Name == "New Category"));
        }
    }
}
