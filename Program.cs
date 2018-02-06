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

        private string ReadToken()
        {
            string token="";
            string[] strf = new string[2];
            token = System.IO.File.ReadAllText("config.txt");
            strf[0] = token.Split('=')[0];
            strf[1] = token.Split('=')[1];
            return strf[1];
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            string token = ReadToken();
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