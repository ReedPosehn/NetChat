# NetChat

A simple chat client created using ASP.Net and SignalR for real time communication using a web interface.

## Current Features
- Real-time chat functionality
- Storage of messages in SQLite DB
- Login page checks whether user has an existing login

## ToDo
### UI
- Change UI to darker mode. Customize fonts
- Look into displaying a more proper amount of messages; perhaps pulling last 50, with a scroll bar for previous.
- Improve UI of the chatroom; functionality should be ironed out before UI changes made.

### User authentication
- Checks credentials against database for authentication
- Registration piece needs to be built next

### Testing
- Add unit tests

## Clean up
- Eliminate unneccesary script

### Investigation work
- Look at changing Javascript to TypeScript

## Tech
- ASP.NET Core (Framework for the web application)
- SignalR (Library for real-time web functionality)
- BCrypt (Password hashing)
- Sqlite (Lightweight database for message storage)

## Usage Instructions
Ensure you have the .NET SDK installed on your machine. (https://dotnet.microsoft.com/download).
Packages required are the "Microsoft.EntityFrameworkCore", "Microsoft.EntityFrameworkCore.Tools" and "Microsoft.EntityFrameworkCore.Sqlite".

You will need to install SQLite (https://sqlite.org/download.html). Run commands to apply migrations "dotnet ef migrations add InitialCreate" and
"dotnet ef database update". DB should be named NetChat.db

BCrypt is used for password hashing - you can install the BCrypt package using the .NET CLI. Run the following command in your project directory:
dotnet add package BCrypt.Net-Next

Clone the repository, build the application and run.

Note that you may have to allow localhost to use https in your browser. This can be done through trusting dotnet to use https through the command line. You may also need to
change your browser settings to enable localhost to use https.

## License
This project is licensed under the MIT License.