using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using ToDoApp.Domain;

namespace ToDoApp.DomainTests
{
    [Binding]
    public class CategorySteps
    {
        private Category _category;

        [Given(@"Stone wants to add a new category")]
        public void GivenStoneWantsToAddANewCategory()
        {
            _category = new Category(new Mock<IRepository<Category>>().Object);
        }

        [When(@"he enters a name for the category")]
        public void WhenHeEntersANameForTheCategory()
        {
            _category.SetName("My Category");
        }

        [Then(@"category may be added")]
        public void ThenCategoryMayBeAdded()
        {
            Action action = async () => await _category.SaveItemAsync();
            Assert.DoesNotThrow(action.Invoke);

            var creationDate = _category.GetCreationDate();
            Assert.That(creationDate, Is.EqualTo(DateTime.Now).Within(1).Minutes);
        }
    }
}
