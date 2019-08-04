Feature: Add a new task

Task is a small piece of work that belongs to a larger unit of work, called item.
Task cannot exist without a parent item.

#Scenario: Adding a new task
#	Given Eddie wants to add a new task
#	When he enters a title
#	Then task may be saved

#Scenario: Setting task as a subtask
#	Given Eddie wants to set a task as a subtask to another task
#	When selects a task
#	And he sets a parent task
#	Then the selected task is set as a child task of a parent

#Scenario Outline: Required data should be provided when adding a new task
#	
#	Given Eddie wants to add a new task
#	When he enters <propertyName> 
#	Then task <should?> be added to tasklist
#
#	Examples: Title should be required
#	| propertyName | should? |
#	| Title        | should  |

#	Given Eddie wants to save the new task
#	When he saves it
#	And task has a title
#	Then task is added to tasklist
#	And tasks creation date and time are logged
#	And task is added to the task list
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