# NetChat

A simple chat client created using ASP.Net and SignalR for real time communication using a web interface.

## Current Features
- Real-time chat functionality
- Storage of messages in SQLite DB

## ToDo
- Improve database experience with display chats
- Change script 
- Clean up script
- Improve UI to be less generic
- Add unit tests

## Tech
- ASP.NET Core (Framework for the web application)
- SignalR (Library for real-time web functionality)
- Sqlite (Lightweight database for message storage)

## Usage Instructions
Ensure you have the .NET SDK installed on your machine. (https://dotnet.microsoft.com/download).
Packages required are the "Microsoft.EntityFrameworkCore", "Microsoft.EntityFrameworkCore.Tools" and "Microsoft.EntityFrameworkCore.Sqlite".

You will need to install SQLite (https://sqlite.org/download.html). Run commands to apply migrations "dotnet ef migrations add InitialCreate" and
"dotnet ef database update". DB should be named NetChat.db


Clone the repository, build the application and run.

Note that you may have to allow localhost to use https in your browser. This can be done through trusting dotnet to use https through the command line. You may also need to
change your browser settings to enable localhost to use https.

## License
This project is licensed under the MIT License.