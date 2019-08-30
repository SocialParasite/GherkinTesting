Feature: ToDo-item
	Item is a task on todo-list to be done. An item may be divided to smaller units of work, subtasks. 
	All subtasks need to be completed before parent item may be marked as completed.

Scenario Outline: Valid name should be given for the ToDo-item
	Given Jill has a ToDo-item open
	When she sets the name of the ToDo-item as <value>
	Then item name <may?> be set

	Examples: Name should be required
		| value          | may?    |
		| My ToDo-item 1 | may     |
		|                | may not |

Scenario: Detect a too long name
	Given Jill has a ToDo-item open
	When she enters a name that is too long for the item
		"""
		I don’t know how I should name this list, so I’m just typing some random stuff here.
		"""
	Then Jill should be informed that the name is too long for the item

Scenario Outline: Required data should be provided when adding a new task item
	Given Jill wants to add a new task item
	When she enters a name for the task item as <value>
	Then ToDo-item <may?> be saved

	Examples: Name should be required
		| value        | may?    |
		| My ToDo-item | may     |
		|              | may not |

Scenario: It shall be possible to add tasks to ToDo-item
	Given Jill has a ToDo-item open
	When she adds a subtask to the item
	Then subtask may be added to the ToDo-item

Scenario: Adding a task to ToDo-list
	Given Jill wants to add a task to ToDo-list
	When she has a task selected
	And she chooses a todo-list
	Then the selected task is set as an item on the the chosen list

Scenario: Tasks creation date and time 
	Given Jill adds a new ToDo-item
	When she saves it
	Then tasks creation date and time are logged


Scenario: Adding task as a subtask
	Given Jill has a task open
	And she has another task she wants to set as a subtask
	Then task should be added as a subtask to parent task
#
#	Given Eddie wants to add a new task
#	And he leaves title empty
#	Then task may not be saved
#
#Scenario: Setting a deadline
#
#	Given Eddie wants to set a deadline for the task
#	When he sets a dealine date
#	Then task should get a deadline
#
#Scenario: Setting a category for the task
#
#	Given Eddie wants to set a category for the task
#	When he selects a task
#	And selects a category
#	Then task should be added to that category
#
#	Given Eddie wants to add a new category
#	When he enters a category name
#	And sets a identifying color for the category
#	Then category may be created