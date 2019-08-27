Feature: ToDo-List
	TODO-LIST consists of todo-items, which in turn may contain zero or more task-items.
		User may have more than one todo-lists.

Scenario: Adding new ToDo-list
	Given Eddie wants to create a new ToDo-list
	When he enters a valid name for the list
	Then the list may be created

Scenario Outline: Valid name should be given for the ToDo-list
	Given Eddie wants to name a ToDo-list
	When he sets the name of the ToDo-list to <value>
	Then name <may?> be set

	Examples: Name should be required
		| value        | may?    |
		| My ToDo-list | may     |
		|              | may not |

Scenario: Detect too long name
	Given Eddie wants to name a ToDo-list
	When he enters a name that is too long
	  """
	  I don’t know how I should name this list, so I’m just typing some random stuff here.
	  """
	Then Eddie should be informed that the name is too long

Scenario Outline: Required data should be provided when adding a new project
	Given Eddie wants to create a new ToDo-list
	When he enters a name for the ToDo-list as <value>
	Then ToDo-list <may?> be saved

	Examples: Name should be required
		| value        | may?    |
		| My ToDo-list | may     |
		|              | may not |

Scenario: Adding ToDo-items to ToDo-list
	Given Eddie has a ToDo-list open
	When he wants to add a new or existing ToDo-item to the list
	Then item may be added to the ToDo-list
