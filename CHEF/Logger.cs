﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CHEF
{
    internal static class Logger
    {
        private const string LogPrefix = "[CHEF]";

        private static DiscordSocketClient _client;
        private static IGuildUser _reportToUser;
        
        internal static void Init(DiscordSocketClient client)
        {
            const long ror2ServerId = 562704639141740588;
            const long iDeathHdId = 125598628310941697;

            _client = client;
            _reportToUser = client.GetGuild(ror2ServerId)?.GetUser(iDeathHdId) ?? (IGuildUser) _client.Rest.GetGuildUserAsync(ror2ServerId, iDeathHdId).Result;
        }

        internal static void Log(string msg)
        {
            var log = $"{LogPrefix} {msg}";

            Console.WriteLine(log);

            Task.Run(async () => { await _reportToUser.SendMessageAsync(log); });
        }

        internal static void LogClassInit([CallerFilePath]string filePath = "")
        {
            Log($"Initializing {Path.GetFileNameWithoutExtension(filePath)}");
        }
    }
}
