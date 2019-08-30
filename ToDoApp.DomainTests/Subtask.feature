Feature: Subtask

Scenario Outline: Valid name should be given for a subtask
	Given Jeff has a subtask open
	When he sets a name of the subtask as <value>
	Then subtask name <may?> be set

	Examples: Name should be required
		| value        | may?    |
		| My subtask 1 | may     |
		|              | may not |

Scenario: Detect a too long subtask name
	Given Jeff has a subtask open
	When he enters a name that is too long for the subtask
		"""
		I don’t know how I should name this subtask, so I’m just typing some random stuff here.
		"""
	Then Jeff should be informed that the name is too long for the subtask

Scenario Outline: Required data should be provided when adding a new subtask
	Given Jeff wants to add a new subtask
	When he enters a name for the subtask as <value>
	Then subtask <may?> be saved

	Examples: Name should be required
		| value      | may?    |
		| My subtask | may     |
		|            | may not |

Scenario: Adding a subtask to Task item
	Given Jeff wants to add a subtask to task item
	When she has a subtask selected
	And she chooses a task item
	Then the selected subtask is set as an child task on the the chosen task item

Scenario: Subasks creation date and time
	Given Jeff adds a new subtask
	When he saves the subtask
	Then subtasks creation date and time are logged
