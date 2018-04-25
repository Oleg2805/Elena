using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using ShikiApiLib;

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

        [Command("getUserId")]       
        [RequireUserPermission(GuildPermission.Administrator)] //необходимое разрешение для использования комманды
        public async Task GetUIdTask(SocketGuildUser user)
        {
            string result = user.ToString() + " ID is " + user.Id;
            await Context.Channel.SendMessageAsync(result);
        }

        [Command("shiki")]
        public async Task ShowShikiLink(SocketGuildUser user)
        {
            string sConnStr = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "Elena",
                IntegratedSecurity = true
            }.ConnectionString;

            using (SqlConnection conn = new SqlConnection(sConnStr))
            {
                conn.Open();
                string result = "no results";
                var sCommand = new SqlCommand(@"select Shikimori from Users where ID=@uID", conn); 
                sCommand.Parameters.AddWithValue("@uID", user.Id.ToString());
                string link = (string) sCommand.ExecuteScalar();
                if (link.Length > 0)
                {
                     result = user.ToString() + " Shikimori profile: https://shikimori.org/" + link;
                }              
                await Context.Channel.SendMessageAsync(result);
            }

            
        }

        [Command("shiki.top")]
        public async Task ShowShikiTop10(SocketGuildUser user)
        {
            string sConnStr = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "Elena",
                IntegratedSecurity = true
            }.ConnectionString;

            using (SqlConnection conn = new SqlConnection(sConnStr))
            {
                conn.Open();
                string result = "no results";
                var sCommand = new SqlCommand(@"select shiki_id from Users where ID=@uID", conn);
                sCommand.Parameters.AddWithValue("@uID", user.Id.ToString());
                string sID = (string)sCommand.ExecuteScalar();
                if (sID.Length > 0)
                {
                    UserInfo info = ShikiApiStatic.GetUserInfo(Convert.ToInt32(sID));
                    string res = info.ToString();

                }
                await Context.Channel.SendMessageAsync(result);
            }

            
        }


    }
}
