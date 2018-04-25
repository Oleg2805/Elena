using Discord.WebSocket;
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Elena_rewrite
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        SocketGuild _guild;

        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult(); //по сути это означает что вместо синхронного метода main мы лишь используем его как точку входа а сами запускаем из него асинхронную новую программу 

        

        public async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null) return; //если токена нет просто выход
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            }); //создаем новую инстанцию клиента
            _client.Log += Log; //логгинг
           
            await _client.LoginAsync(TokenType.Bot, Config.bot.token); //ждем входа с токеном и json
            await _client.StartAsync();
            _handler = new CommandHandler(); //новый обработчик комманд
            await _handler.InitializeAsync(_client); //ждем его инициилизацию в клиенте

           

                await Task.Delay(-1);   //не выключать программу
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
