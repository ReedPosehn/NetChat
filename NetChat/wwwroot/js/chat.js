"use strict";

// Create a connection to the SignalR hub
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")  // Ensure this matches the hub URL in Startup.cs
    .build();

// Disable the send button until the connection is established
document.getElementById("sendButton").disabled = true;

// Handle incoming messages
connection.on("ReceiveMessage", function (user, message, timestamp) {
    var li = document.createElement("li");
    // Format message with timestamp
    li.textContent = `${timestamp}: ${user} - ${message}`;
    document.getElementById("messagesList").appendChild(li);
});

// Start the connection
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// Send a message when the button is clicked
document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    // Prevent the default form submission
    event.preventDefault();
});
