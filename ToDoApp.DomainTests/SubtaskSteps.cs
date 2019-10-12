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
        Mock<Subtask> _mock;

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

        [When(@"he has a subtask selected")]
        public void WhenHeHasASubtaskSelected()
        {
            _subtask.SetName("Name of the subtask");
        }

        [When(@"he chooses a task item")]
        public void WhenHeChoosesATaskItem()
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
            var creationDate = _subtask.GetCreationDate();
            Assert.That(creationDate, Is.EqualTo(DateTime.Now).Within(1).Minutes);
        }

        [Given(@"Jeff modifies a subtask")]
        public void GivenJeffModifiesASubtask()
        {
            var repo = new Mock<IRepository<Subtask>>().Object;
            _mock = new Mock<Subtask>(repo);
            _mock.Object.SetName("Old subtask");

            _mock.SetupGet(x => x.CreationDate).Returns(DateTime.Now.AddDays(-1));
        }

        [When(@"he saves the modified subtask")]
        public async Task WhenHeSavesTheModifiedSubtask()
        {
            await _mock.Object.SaveItemAsync();
        }

        [Then(@"subtasks creation date and time are not changed")]
        public void ThenSubtasksCreationDateAndTimeAreNotChanged()
        {
            Assert.That(_mock.Object.CreationDate, Is.Not.EqualTo(DateTime.Now).Within(1).Minutes);
        }

        [Given(@"Jeff wants to set a deadline for a subtask")]
        public void GivenJeffWantsToSetADeadlineForASubtask()
        {
            _subtask = new Subtask(new Mock<IRepository<Subtask>>().Object);
        }

        [Then(@"he should be able to select a deadline date for the subtask")]
        public void ThenHeShouldBeAbleToSelectADeadlineDateForTheSubtask()
        {
            var myDeadline = DateTime.Today.AddDays(10);
            _subtask.SetDeadline(myDeadline);
            Assert.That(_subtask.GetDeadline(), Is.EqualTo(myDeadline));
        }
    }
}
