**ToDoDonely Console Application**

**Introduction**
ToDoDonely is a console-based task management application built with C#. It allows users to efficiently manage their to-do lists, including viewing, adding, editing, and saving tasks.

**Features**
Display tasks by date or project
Add new tasks
Edit existing tasks (update, mark as done, remove)
Save tasks to a JSON file for persistent storage
User-friendly menu interface with color-coded prompts and instant feedback concerning Editing and Adding a Task/Project.

**Prerequisites**
To run this application, ensure you have the following installed:
.NET SDK (version 6.0 or later)
A text editor or IDE (such as Visual Studio or VS Code)

**Installation**
Clone the repository or download the source code.
git clone https://github.com/KalleVictor/ToDoDonely.git
cd ToDoDonely

Upon launch, the main menu is displayed with the following options:
1: Show Task List (by Date or Project)
2: Add New Task
3: Edit Task (Update, Mark as Done, Remove)
4: Save & Quit

Select an option by entering the corresponding number.
Follow the on-screen instructions to manage your tasks.
When finished, choose option 4 to save and exit.

**File Storage**
The application saves tasks to a JSON file named tasks.json. Ensure this file is accessible for data persistence.

**Code Structure**
Main Program: Handles menu navigation and user interaction.
Taskmanager.cs: Manages loading, displaying.
TaskEditor.cs: Handles adding and editing tasks.
MenuInterface.cs: Provides the visual structure and formatting for the console UI.
Files.cs: Handles the saving procedure. 

**License**
????

**Contact**
KalleVictor over at GitHub
