using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Elena_rewrite.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("serverinfo")]
        public async Task giveContext()
        {
            var embed = new EmbedBuilder();
            string description = "Server name: "+Context.Guild.Name;
            description += "\nNumber of channels: " + Context.Guild.Channels.Count;
            description += "\nServerOwner: " + Context.Guild.Owner;
            description += "\nUserCount: " + Context.Guild.Users.Count;
            embed.WithTitle("Информация о текущем сервере");
            embed.WithDescription(description);
            embed.WithColor(new Color(155,48,216));
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("pick")]
        public async Task PickOne([Remainder]string msg)
        {
            string[] options = msg.Split('|', StringSplitOptions.RemoveEmptyEntries);
            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];
            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username); //локальный ник??? 
            embed.WithDescription(selection);
            embed.WithThumbnailUrl("https://flatis.moe/uploads/comment_faces/animated/aliens.gif");
            embed.WithColor(new Color(155, 48, 216));
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("sendnudes")]
        //[RequireUserPermission(GuildPermission.Administrator)] //необходимое разрешение для использования комманды
        public async Task RevealSecret([Remainder]string args = "")
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync("nudes sent xD");
        }


    }
}
