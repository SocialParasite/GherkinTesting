Feature: ToDo-item
	ToDo-item is a task on todo-list to be done. An item may be divided to smaller units of work, subtasks. 
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

Scenario: It shall be possible to add tasks as child tasks to other tasks
	Given Jill has a ToDo-item open
	When she adds a subtask to the item
	Then subtask may be added to the ToDo-item

Scenario: Adding another task as a subtask to Task item
	Given Jill wants to add a subtask to task item
	When she has a task selected that she wants to set as subtask
	And she chooses a parent task item
	Then the selected task is set as an child task to the the chosen task item

Scenario: Adding a task to ToDo-list
	Given Jill wants to add a task to ToDo-list
	When she has a task selected
	And she chooses a todo-list
	Then the selected task is set as an item on the the chosen list

Scenario: Tasks creation date and time
	Given Jill adds a new ToDo-item
	When she saves it
	Then tasks creation date and time are logged

Scenario: Tasks modification
	Given Jill modifies a ToDo-item
	When she saves the modified item
	Then tasks creation date and time are not changed

Scenario: Setting a deadline
	Given Eddie wants to set a deadline for the task
	When he has a task selected
	Then he should be able to select a deadline date for the task

Scenario Outline: Setting a categories for the task
	Given Eddie has a task with <value> categories
	When he sets a category for the task
	Then task should be added to that category

	Examples: Task could belong to more than one categories
		| value |
		| 0     |
		| 1     |