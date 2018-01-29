using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Elena
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            string token = "NDAzMjM0NjY4MDU4NzcxNDU2.DVD6aw.XYhe8g9wZE02_nlsFctwsyhr-qw"; // Remember to keep this private!
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            _client.MessageReceived += MessageReceived;

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "!onichan")
            {
                await message.Channel.SendMessageAsync("WrongHole!");
            }
        }

    }
}
 

//static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello World!");
        //}