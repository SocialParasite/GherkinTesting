using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ToDoApp.Domain;

namespace ToDoApp.DomainTests
{
    [Binding]
    public class SubtaskSteps
    {
        private Subtask _subtask;
        private Action _action;
        TaskItem _taskItem;

        [Given(@"Jeff has a subtask open")]
        public void GivenJeffHasASubtaskOpen()
        {
            _subtask = new Subtask(new Mock<IRepository<Subtask>>().Object);
        }

        [When(@"he sets a name of the subtask as (.*)")]
        public void WhenHeSetsANameOfTheSubtaskAsMySubtask(string name)
        {
            _action = () => _subtask.SetName(name);
        }

        [Then(@"subtask name (.*) be set")]
        public void ThenSubtaskNameMayBeSet(string expectation)
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

        [When(@"he enters a name that is too long for the subtask")]
        public void WhenHeEntersANameThatIsTooLongForTheSubtask(string multilineText)
        {
            _action = () => _subtask.SetName(multilineText);
        }

        [Then(@"Jeff should be informed that the name is too long for the subtask")]
        public void ThenJeffShouldBeInformedThatTheNameIsTooLongForTheSubtask()
        {
            Assert.Throws<ArgumentOutOfRangeException>(_action.Invoke);
        }

        [Given(@"Jeff wants to add a new subtask")]
        public void GivenJeffWantsToAddANewSubtask()
        {
            _subtask = new Subtask(new Mock<IRepository<Subtask>>().Object);
        }

        [When(@"he enters a name for the subtask as (.*)")]
        public void WhenHeEntersANameForTheSubtaskAsMySubtask(string name)
        {
            _action = () => _subtask.SetName(name);
        }

        [Then(@"subtask (.*) be saved")]
        public void ThenSubtaskMayBeSaved(string expectation)
        {
            bool expected = expectation == "may" ? true : false;

            if (expected)
            {
                Action action = async () => await _subtask.SaveItemAsync();
                Assert.DoesNotThrow(action.Invoke);
            }
            else
            {
                Func<Task> action = async () => await _subtask.SaveItemAsync();
                Assert.ThrowsAsync<ArgumentNullException>(action.Invoke);
            }
        }

        [Given(@"Jeff wants to add a subtask to task item")]
        public void GivenJeffWantsToAddASubtaskToTaskItem()
        {
            _subtask = new Subtask(new Mock<IRepository<Subtask>>().Object);
        }

        [When(@"she has a subtask selected")]
        public void WhenSheHasASubtaskSelected()
        {
            _subtask.SetName("Name of the subtask");
        }

        [When(@"she chooses a task item")]
        public void WhenSheChoosesATaskItem()
        {
            _taskItem = new TaskItem(new Mock<IRepository<TaskItem>>().Object);
            _taskItem.SetName("Parent task");
        }

        [Then(@"the selected subtask is set as an child task on the the chosen task item")]
        public void ThenTheSelectedSubtaskIsSetAsAnChildTaskOnTheTheChosenTaskItem()
        {
            _subtask.SetParentTask(_taskItem);
            Assert.That(_subtask.ParentTask.Equals(_taskItem));
        }

        [Given(@"Jeff adds a new subtask")]
        public void GivenJeffAddsANewSubtask()
        {
            _subtask = new Subtask(new Mock<IRepository<Subtask>>().Object);
            _subtask.SetName("subtask x");
        }

        [When(@"he saves the subtask")]
        public async Task WhenHeSavesTheSubtask()
        {
            await _subtask.SaveItemAsync();
        }

        [Then(@"subtasks creation date and time are logged")]
        public void ThenSubtasksCreationDateAndTimeAreLogged()
        {
            var creationDate = _subtask.CreationDate;
            Assert.That(creationDate, Is.EqualTo(DateTime.Now).Within(1).Minutes);
        }
    }
}
