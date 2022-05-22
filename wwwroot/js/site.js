// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";
var connected = false;
var nickname = "";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("send-message-button").disabled = true;

connection.on("ReceiveMessage", function (response) {
    var li = document.createElement("li");
    li.classList.add("message-li");
    document.getElementById("message-list").appendChild(li);
    let json = JSON.parse(response);
    let time = new Date(json.Date);
    li.textContent = `${time.getHours()}:${time.getMinutes()}:${time.getSeconds()} ${json.Username}: ${json.Text}`;
});

connection.on("LoadMessages", function (response) {
    let messages = JSON.parse(response);
    for(const message of messages)
    {
        var li = document.createElement("li");
        li.classList.add("message-li");
        document.getElementById("message-list").appendChild(li);
        let time = new Date(message.Date);
        li.textContent = `${time.getHours()}:${time.getMinutes()}:${time.getSeconds()} ${message.Username}: ${message.Text}`;
    }
});

connection.start().then(function () {
    connected = true;
    connection.invoke("Connected").catch(function (err) {
        return console.error(err.toString());
    })
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
                document.getElementById("overlay").style.display = "none";
                document.getElementById("message-input-box").focus();
            }
        }
    }
})

document.getElementById("guest-button").addEventListener("click", function (event) {
    if(connected)
    {
        nickname = document.getElementById("guest-button").getAttribute("data");
        if(typeof nickname === "string")
        {
            if(nickname !== "")
            {
                document.getElementById("send-message-button").disabled = false;
                document.getElementById("nickname-dialog").style.visibility = "hidden";
                document.getElementById("overlay").style.display = "none";
                document.getElementById("message-input-box").focus();
            }
        }
    }
})

try {
    document.getElementById("nickname-input").addEventListener("keypress", function (event)
    {
        if(event.key === "Enter")
        {
            event.preventDefault();
            document.getElementById("nickname-button").click();
        }
    })
}
catch (e)
{
    console.log(e);
}

try {
    document.getElementById("message-input-box").addEventListener("keypress", function (event)
    {
        if(event.key === "Enter")
        {
            event.preventDefault();
            document.getElementById("send-message-button").click();
        }
    })
}
catch (e)
{
    console.log(e);
}
