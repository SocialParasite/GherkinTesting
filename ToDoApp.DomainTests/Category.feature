Feature: Category
	Tasks may belong to one or more categories.
	Categories make it easier to sort your tasks.

Scenario: Add Categories
	Given Stone wants to add a new category
	When he enters a name for the category
	Then category may be added
