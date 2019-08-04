Feature: Project
	PROJECT is a todo-list, which consists of todo-items, which in turn consist of task-items.

Scenario Outline: Valid name should be given for the ToDo-list
	Given Eddie names a ToDo-list
	When he sets the name of the ToDo-list as <value>
	Then name <may?> be set

	Examples: Name should be required
		| value        | may?    |
		| My ToDo-list | may     |
		|              | may not |

Scenario Outline: Required data should be provided when adding a new project
	Given Eddie wants to add a new ToDo-list
	When he enters a name for the ToDo-list as <value>
	Then ToDo-list <may?> be saved

	Examples: Name should be required
		| value        | may?    |
		| My ToDo-list | may     |
		|              | may not |

Scenario: It shall be possible to add items to ToDo-list
	Given Eddie has a ToDo-list open
	When he adds an item to the list
	Then item may be added to the ToDo-list
