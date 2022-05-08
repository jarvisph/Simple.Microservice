const signalR = require("@microsoft/signalr")
const chanenl = "TEST";
const token = "a123456";
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/hub", {
        headers: {
            "appkey": token
        }
    })
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("连接成功");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();

connection.on(chanenl, (message) => {
    console.log(message)
});