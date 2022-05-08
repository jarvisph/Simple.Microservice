using Microsoft.AspNetCore.SignalR.Client;

string chanenl = "TEST";
string token = "b123456";

HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/hub", options =>
                {
                   options.Headers = new Dictionary<string, string>
                   {
                       {"appkey",token }
                   };
                })
                .Build();

connection.Closed += async (error) =>
{
    await Task.Delay(new Random().Next(0, 5) * 1000);
    await connection.StartAsync();
};
connection.On<string>(chanenl, (message) =>
{
    Console.WriteLine(message);
});



await connection.StartAsync();


await connection.InvokeAsync("SendMessage", chanenl, $"[{DateTime.Now}]你好啊");

await Task.Delay(new Random().Next(0, 5) * 1000);