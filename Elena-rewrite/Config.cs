using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Elena_rewrite
{
    class Config
    {
        public static BotConfig bot;
        private const string configFolder = "Data";
        private const string configFile = "config.json";
        private const string path = configFolder + "/" + configFile;
        static Config()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
            else
            {
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(path, json);
            }
        }
    }

    public struct BotConfig
    {
        public string token;
        public string cmdPrefix;
    }
}
