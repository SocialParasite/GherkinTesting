Feature: Add a new item
Item is an item on todo-list to be done. An item may be divided to smaller units of work. 
These subitems are called tasks.
All tasks need to be completed before parent item may be marked as completed.

Scenario Outline: Valid name should be given for the ToDo-item
	Given Eddie names a ToDo-item
	When he sets the name of the ToDo-item as <value>
	Then item name <may?> be set

	Examples: Name should be required
		| value          | may?    |
		| My ToDo-item 1 | may     |
		|                | may not |


Scenario Outline: Required data should be provided when adding a new ToDo-item
	Given Eddie wants to add a new ToDo-item
	When he enters a name for the ToDo-item as <value>
	Then ToDo-item <may?> be saved as part of selected ToDo-list

	Examples: Name should be required
		| value        | may?    |
		| My ToDo-item | may     |
		|              | may not |

Scenario: It shall be possible to add tasks to ToDo-item
	Given Eddie has a ToDo-item open
	When he adds a task to the item
	Then task may be added to the ToDo-item