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

Scenario Outline: Required data should be provided when adding a new ToDo-item
	Given Jill wants to add a new ToDo-item
	When she enters a name for the ToDo-item as <value>
	Then ToDo-item <may?> be saved as part of selected ToDo-list

	Examples: Name should be required
		| value        | may?    |
		| My ToDo-item | may     |
		|              | may not |

Scenario: It shall be possible to add tasks to ToDo-item
	Given Jill has a ToDo-item open
	When she adds a subtask to the item
	Then subtask may be added to the ToDo-item

Scenario: Setting task as a subtask
	Given Jill wants to set a task as a subtask to another task
	When she has a subtask selected
	And she chooses a parent task
	Then the selected task is set as a child task of the chosen parent