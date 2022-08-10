# Project Description
Design and implement a **Tasks Management** console application that will help a small team of developers to organize and manage their work tasks.

# Functional Requirements
The application must support multiple **teams**.

Each team must have a **name**, **members,** and **boards**.

- The name must be unique in the application.
- The name is a string between 5 and 15 symbols.

Each member must have a **name**, list of **tasks** and **activity history**.

- The name must be unique in the application.
- The name is a string between 5 and 15 symbols.

Each board must have a **name**, list of **tasks** and **activity history**.

- Name must be unique in the team.
- Name is a string between 5 and 10 symbols. 

There are 3 types of tasks: **bug**, **story,** and **feedback**. 

## Bug
Bugs must have an ID, a title, a description, a list of steps to reproduce it, a priority, a severity, a status, an assignee, a list of comments and a list of changes history.

- Title is a string between 10 and 50 symbols.
- Description is a string between 10 and 500 symbols.
- Steps to reproduce is a list of strings.
- Priority is one of the following: **High**, **Medium**, or **Low**.
- Severity is one of the following: **Critical**, **Major**, or **Minor**.
- Status is one of the following: **Active** or **Fixed**.
- Assignee is a member from the team.
- Comments is a list of comments (string messages with an author).
- History is a list of all changes (string messages) that were done to the bug.
## Story
Stories must have an ID, a title, a description, a priority, a size, a status, an assignee, a list of comments and a list of changes history.

- Title is a string between 10 and 50 symbols.
- Description is a string between 10 and 500 symbols.

- Priority is one of the following: **High**, **Medium**, or **Low**.
- Size is one of the following: **Large**, **Medium**, or **Small**.
- Status is one of the following: **Not Done**, **InProgress**, or **Done**.
- Assignee is a member from the team.
- Comments is a list of comments (string messages with author).
- History is a list of all changes (string messages) that were done to the story.
## Feedback
Feedbacks must have an ID, a title, a description, a rating, a status, a list of comments and a list of changes history.

- Title is a string between 10 and 50 symbols.
- Description is a string between 10 and 500 symbols.
- Rating is an integer.
- Status is one of the following: **New**, **Unscheduled**, **Scheduled**, or **Done**.
- Comments is a list of comments (string messages with author).
- History is a list of all changes (string messages) that were done to the feedback.

**Important**

**Each task must have a unique ID.** For example, if there is a bug with ID = 1 then a story or feedback with ID = 1 cannot exist.


## Operations
The application must support the following operations:

- Create a new person.
- Show all people.
- Show person's activity.
- Create a new team.
- Show all teams.
- Show team's activity.
- Add person to team.
- Show all team members.
- Create a new board in a team.
- Show all team boards.
- Show board's activity.
- Create a new Bug/Story/Feedback in a board.
- Change the Priority/Severity/Status of a bug.
- Change the Priority/Size/Status of a story.
- Change the Rating/Status of a feedback.

- Assign/Unassign a task to a person.
- Add comment to a task.

- Listing:
  - List all tasks (display the most important info).
    - Filter by title
    - Sort by title
  - List bugs/stories/feedback only.
    - Filter by status and/or assignee
    - Sort by title/priority/severity/size/rating (depending on the task type)
  - List tasks with assignee.
    - Filter by status and/or assignee
    - Sort by title



# Use cases
## Use case #1
One of the developers has noticed a bug in the company’s product. He starts the application and goes on to create a new Task for it. He creates a new Bug and gives it the title "The program freezes when the Log In button is clicked." For the description he adds "This needs to be fixed quickly!", he marks the Bug as High priority and gives it Critical severity. Since it is a new bug, it gets the Active status.

The developer also assigns it to the senior developer in the team. To be able to fix the bug, the senior developer needs to know how to reproduce it, so the developer who logged the bug adds a list of steps to reproduce: "1. Open the application; 2. Click "Log In"; 3. The application freezes!" The bug is saved to the application and is ready to be fixed.
## Use case #2
A new developer has joined the team. One of the other developers starts the application and creates a new team member. After that, he adds the new team member to one of the existing teams and assigns all Critical bugs to him.
## Use case #3
One of the developers has fixed a bug that was assigned to him. He adds a comment to that bug, saying "This one took me a while, but it is fixed now!", and then changes the status of the bug to Done. Just to be sure, he checks the changes history list of the bug and sees that the last entry in the list says, "The status of item with ID 42 switched from Active to Done."

# Technical Requirements
- Follow the **OOP best practices**:
  - Use data encapsulation.
  - Use inheritance and polymorphism properly.
  - Use interfaces and abstract classes properly.
  - Use static members properly.
  - Use enumerations properly.
  - Aim for strong cohesion and loose coupling.
- Follow guidelines for writing **clean code**:
  - Proper naming of classes, methods, and fields.
  - Small classes and methods.
  - Well formatted and consistent code.
  - No duplicate code.
- Implement user **input validations** and display meaningful messages.
- Implement proper **exception handling**.
- Prefer using LINQ when working with collections.
- Cover the core functionality with unit tests (**at least 80%** code coverage of the models and commands).
  - There is no need to test the printing commands.
- Use **Git** to keep your source code and for team collaboration.

# Teamwork Guidelines
Please see the Teamwork Guidelines document.

### Sample Input

```
CreateTeam Team
CreateTeam ThisIsPrettyLongTeamName
CreateTeam bestTeam
CreateMember Memb
CreateMember ThisIsPrettyLongMemberName
CretaeMember pesho
CreateMember pesho
CreateBoardInteam Board1 teamName
CreateBoardInTeam Boar bestTeam
CreateBoardInTeam Board1 bestTeam
CreateFeedback FeedbackTitle Feed 10
CreateFeedback Feed FeedbackDescription 10
CreateFeedback FeedbackName FeedbackDescription 10
CreateStory StoryTitle Stor High Medium
CreateStory Stor Stor High Medium
CreateStory StoryName StoryDescription priority Medium
CreateStory StoryName StoryDescription High Med
CreateStory StoryName StoryDescription High Medium
CreateStory StoryName11 StoryDescription High Medium
CreateStory StoryName12 StoryDescription Low Medium
CreateBug BugTitle123 BugDesc High Critical
step1
step2
end
CreateBug Bug BugDescription High Critical
step1
end
CreateBug BugTitle123 BugDescription High Critical
step1
step2
end
AddMemberToTeam bestTeam pesho
AddMemberToTeam TeamName pesho
AddMembertoTeam bestTeam pesho
AddComment 1 pesho "This is a nice task"
AddComment 2 pesho "This is a tough one!"
AddTask 2 bestTeam Board1
AddTask 2 bestTeam Board1
AddTask 4 bestTeam Board2
AddTask 4 teamName Board1
CreateTeam bestTeam
CreateMember pesho
AssignTask 2 pesho bestTeam
AssignTask 3 pesho bestTeam
ShowAllTeamBoards bestTeam
FilterTasksByTitle bug
FilterTasksByTitle Bug
FilterTasksByTitle Title
FilterTasksByTitle Name
CreateMember gosho
AddMembertoTeam bestTeam gosho
AssignTask 1 gosho bestTeam
AssignTask 4 gosho bestTeam
FilterAssignedTasksBy status NotDone
FilterAssignedTasksBy assignee pesho
UnassignTask 3 pesho
FilterAssignedTasksBy assignee pesho
CreateBug BugTitleNumber2 BugDescription2 Low Major
step1
step2
end
ChangeBug 5 status fixed
SortBugBy priority
ChangeStory 2 priority Major
None of the enums in PriorityTypes match the value Major.
ChangeStory 2 priotiry Low
Parameter with name priotiry does not exist!
ChangeStory 2 priority Low
FilterStoryBy priority
SortStoryBy priority
changestory 3 priority Medium
SortStoryBy priority
ShowallMembers
ShowAllTeams
ShowTeamsActivity bestTeam
ShowBoardActivity Board1 bestTeam
ShowTeamMembers bestTeam
ShowTaskActivity 3
ChangeStory 3 priority Low
ShowtaskActivity 3
ShowTeamMembers bestTeam
RemoveMemberFromTeam bestTeam pesho
ShowTeamMembers bestTeam
AssignTask 2 gosho bestTeam
ShowTeamMembers bestTeam
```


### Expected Output

```
Type help to see all of the commands

CreateTeam Team
Team's name must be between 5 and 15 characters!
CreateTeam ThisIsPrettyLongTeamName
Team's name must be between 5 and 15 characters!
CreateTeam bestTeam
Team with name bestTeam was created.
####################
CreateMember Memb
Member name must be between 5 and 15 characters!
CreateMember ThisIsPrettyLongMemberName
Member name must be between 5 and 15 characters!
CretaeMember pesho
Command with name: CretaeMember doesn't exist!
CreateMember pesho
Member with name pesho was created.
####################
CreateBoardInteam Board1 teamName
There is no team with name teamName!
CreateBoardInTeam Boar bestTeam
Board name must be between 5 and 10 characters!
CreateBoardInTeam Board1 bestTeam
Board with name Board1 created in team bestTeam!
####################
CreateFeedback FeedbackTitle Feed 10
Description must be between 10 and 500 characters!
CreateFeedback Feed FeedbackDescription 10
Title must be between 10 and 50 characters!
CreateFeedback FeedbackName FeedbackDescription 10
Feedback with title FeedbackName and id 1 was created.
####################
CreateStory StoryTitle Stor High Medium
Description must be between 10 and 500 characters!
CreateStory Stor Stor High Medium
Title must be between 10 and 50 characters!
CreateStory StoryName StoryDescription priority Medium
None of the enums in PriorityTypes match the value priority.
CreateStory StoryName StoryDescription High Med
None of the enums in SizeType match the value Med.
CreateStory StoryName StoryDescription High Medium
Title must be between 10 and 50 characters!
CreateStory StoryName11 StoryDescription High Medium
Story with id 2 was created!
####################
CreateStory StoryName12 StoryDescription Low Medium
Story with id 3 was created!
####################
CreateBug BugTitle123 BugDesc High Critical
Please enter step to reproduce the bug without enumerating it and press <Enter> to add new step.
When you are ready just type <end>
step1
step2
end
Description must be between 10 and 500 characters!
CreateBug Bug BugDescription High Critical
Please enter step to reproduce the bug without enumerating it and press <Enter> to add new step.
When you are ready just type <end>
step1
end
Title must be between 10 and 50 characters!
CreateBug BugTitle123 BugDescription High Critical
Please enter step to reproduce the bug without enumerating it and press <Enter> to add new step.
When you are ready just type <end>
step1
step2
end
Bug with Id: 4 was created!
####################
AddMemberToTeam bestTeam pesho
Member with name : pesho was successfully added in team bestTeam!
####################
AddMemberToTeam TeamName pesho
There is no team with name TeamName!
AddMembertoTeam bestTeam pesho
Member with name pesho is already in team bestTeam!
AddComment 1 pesho "This is a nice task"
Comment added to task with id 1 by pesho
####################
AddComment 2 pesho "This is a tough one!"
Comment added to task with id 2 by pesho
####################
AddTask 2 bestTeam Board1
Task with id 2 added to board Board1 in team bestTeam
####################
AddTask 2 bestTeam Board1
Task: StoryName11 with ID: 2  is already on board Board1.
AddTask 4 bestTeam Board2
There is no board with name Board2 in team bestTeam!
AddTask 4 teamName Board1
There is no team with name teamName!
CreateTeam bestTeam
Team's name should be unique in the aplication!
CreateMember pesho
Members's name should be unique in the aplication!
AssignTask 2 pesho bestTeam
Task with id 2 was assigned to pesho
####################
AssignTask 3 pesho bestTeam
Task with Id: 3 is not attached to any of team bestTeam boards!
ShowAllTeamBoards bestTeam
-BOARD NAME: Board1-
--BOARD--
  --TASKS--

    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: High
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--


  --TASKS--

--BOARD--

--------
####################
FilterTasksByTitle bug

####################
FilterTasksByTitle Bug
--BUG--
    Id: 4
    Title: BugTitle123
    Description: BugDescription
    Assignee: No assignee
    Priority: High
    Severity: Critical
    Status: Active
    Steps to reproduce:
     1.Step1
     2.Step2

    --NO COMMENTS--


####################
####################
FilterTasksByTitle Title
--BUG--
    Id: 4
    Title: BugTitle123
    Description: BugDescription
    Assignee: No assignee
    Priority: High
    Severity: Critical
    Status: Active
    Steps to reproduce:
     1.Step1
     2.Step2

    --NO COMMENTS--


####################
####################
FilterTasksByTitle Name
--FEEDBACK--
    Id: 1
    Title: FeedbackName
    Description: FeedbackDescription
    Status: New
    Rating: 10
    --COMMENTS--
    ----------
    This is a nice task
      User: pesho
    ----------
    --COMMENTS--


####################
    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: High
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--


####################
    --STORY--
    Id: 3
    Title: StoryName12
    Description: StoryDescription
    Assignee: No assignee
    Priority: Low
    Size: Medium
    Status: NotDone
    --NO COMMENTS--


####################
####################
CreateMember gosho
Member with name gosho was created.
####################
AddMembertoTeam bestTeam gosho
Member with name : gosho was successfully added in team bestTeam!
####################
AssignTask 1 gosho bestTeam
Task with Id: 1 is not attached to any of team bestTeam boards!
AssignTask 4 gosho bestTeam
Task with Id: 4 is not attached to any of team bestTeam boards!
FilterAssignedTasksBy status NotDone
--STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: High
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--
####################
FilterAssignedTasksBy assignee pesho
--STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: High
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--
####################
UnassignTask 3 pesho
Task with id 3 was not found in the task list of member pesho
FilterAssignedTasksBy assignee pesho
--STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: High
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--
####################
CreateBug BugTitleNumber2 BugDescription2 Low Major
Please enter step to reproduce the bug without enumerating it and press <Enter> to add new step.
When you are ready just type <end>
step1
step2
end
Bug with Id: 5 was created!
####################
ChangeBug 5 status fixed
Status of bug with Id: 5 was changed!
####################
SortBugBy priority
--BUG--
    Id: 4
    Title: BugTitle123
    Description: BugDescription
    Assignee: No assignee
    Priority: High
    Severity: Critical
    Status: Active
    Steps to reproduce:
     1.Step1
     2.Step2

    --NO COMMENTS--


    --BUG--
    Id: 5
    Title: BugTitleNumber2
    Description: BugDescription2
    Assignee: No assignee
    Priority: Low
    Severity: Major
    Status: Fixed
    Steps to reproduce:
     1.Step1
     2.Step2

    --NO COMMENTS--
####################
ChangeStory 2 priority Major
None of the enums in PriorityTypes match the value Major.
None of the enums in PriorityTypes match the value Major.
Command with name: None doesn't exist!
ChangeStory 2 priotiry Low
Parameter with name priotiry does not exist!
Parameter with name priotiry does not exist!
Command with name: Parameter doesn't exist!
ChangeStory 2 priority Low
Priority of story with id 2 was changed!
####################
FilterStoryBy priority
You can only filter list of stories by assignee or status!
SortStoryBy priority
--STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: Low
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--

####################
    --STORY--
    Id: 3
    Title: StoryName12
    Description: StoryDescription
    Assignee: No assignee
    Priority: Low
    Size: Medium
    Status: NotDone
    --NO COMMENTS--

####################
####################
changestory 3 priority Medium
Priority of story with id 3 was changed!
####################
SortStoryBy priority
--STORY--
    Id: 3
    Title: StoryName12
    Description: StoryDescription
    Assignee: No assignee
    Priority: Medium
    Size: Medium
    Status: NotDone
    --NO COMMENTS--

####################
    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: Low
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--

####################
####################
ShowallMembers
Number of all members: 2
--MEMBER--
  Member Name: pesho
  --TASKS--

    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: Low
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--

  --TASKS--

--MEMBER--

####################
--MEMBER--
  Member Name: gosho
  --NO TASKS--

--MEMBER--

####################
####################
ShowAllTeams
Number of all teams: 1
--TEAM--
Team name: bestTeam
Members of the team: 2
--MEMBER--
  Member Name: pesho
  --TASKS--

    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: Low
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--

  --TASKS--

--MEMBER--

--MEMBER--
  Member Name: gosho
  --NO TASKS--

--MEMBER--

--BOARD--
  --TASKS--

    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: Low
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--


  --TASKS--

--BOARD--

--TEAM--

####################
####################
ShowTeamsActivity bestTeam
[2022:08:10|14:17:34]Team with bestTeam was created!
[2022:08:10|14:17:34]Board with name Board1 added to team bestTeam.
[2022:08:10|14:17:34]Member pesho joined bestTeam team!
[2022:08:10|14:17:35]Member gosho joined bestTeam team!
####################
ShowBoardActivity Board1 bestTeam
[2022:08:10|14:17:34]Successfuly created Board with name Board1!
[2022:08:10|14:17:34]Successfuly added Story with ID: 2 to board Board1!
####################
ShowTeamMembers bestTeam
--MEMBER--
  Member Name: pesho
  --TASKS--

    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: Low
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--

  --TASKS--

--MEMBER--

--MEMBER--
  Member Name: gosho
  --NO TASKS--

--MEMBER--
####################
ShowTaskActivity 3
[2022:08:10|14:17:34]Successfuly created Story with ID: 3!
[2022:08:10|14:17:35]Priority of Story with ID 3 StoryName12 was changed from Low to Medium.
####################
ChangeStory 3 priority Low
Priority of story with id 3 was changed!
####################
ShowtaskActivity 3
[2022:08:10|14:17:34]Successfuly created Story with ID: 3!
[2022:08:10|14:17:35]Priority of Story with ID 3 StoryName12 was changed from Low to Medium.
[2022:08:10|14:17:35]Priority of Story with ID 3 StoryName12 was changed from Medium to Low.
####################
ShowTeamMembers bestTeam
--MEMBER--
  Member Name: pesho
  --TASKS--

    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: pesho
    Priority: Low
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--

  --TASKS--

--MEMBER--

--MEMBER--
  Member Name: gosho
  --NO TASKS--

--MEMBER--
####################
RemoveMemberFromTeam bestTeam pesho
Member with name : pesho was successfully removed from team bestTeam!
Task with id 2 is unassigned from pesho.
####################
ShowTeamMembers bestTeam
--MEMBER--
  Member Name: gosho
  --NO TASKS--

--MEMBER--
####################
AssignTask 2 gosho bestTeam
Task with id 2 was assigned to gosho
####################
ShowTeamMembers bestTeam
--MEMBER--
  Member Name: gosho
  --TASKS--

    --STORY--
    Id: 2
    Title: StoryName11
    Description: StoryDescription
    Assignee: gosho
    Priority: Low
    Size: Medium
    Status: NotDone
    --COMMENTS--
    ----------
    This is a tough one!
      User: pesho
    ----------
    --COMMENTS--

  --TASKS--

--MEMBER--
####################
```
