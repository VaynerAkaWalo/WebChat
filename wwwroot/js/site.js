// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";
var connected = false;
var nickname = "";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("send-message-button").disabled = true;

connection.on("ReceiveMessage", function (user, message, date) {
    var li = document.createElement("li");
    li.classList.add("message-li");
    document.getElementById("message-list").appendChild(li);
    var time = new Date(date);
    li.textContent = `${time.getHours()}:${time.getMinutes()} ${user}: ${message}`;
});

connection.start().then(function () {
    connected = true;
}).catch(function (err) {
    return console.error(err.toString());
})

document.getElementById("send-message-button").addEventListener("click", function (event) {
    var message = document.getElementById("message-input-box").value;
    document.getElementById("message-input-box").value = "";
    connection.invoke("SendMessage", nickname, message).catch(function (err) {
        return console.error(err.toString());
    })
})

document.getElementById("nickname-button").addEventListener("click", function (event) {
    if(connected)
    {
        nickname = document.getElementById("nickname-input").value;
        if(typeof nickname === "string")
        {
            if(nickname !== "")
            {
                document.getElementById("send-message-button").disabled = false;
                document.getElementById("nickname-dialog").style.visibility = "hidden";
            }
        }
    }
})
