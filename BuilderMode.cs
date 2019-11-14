using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Tortellio.BuilderMode
{
    public class BuilderMode : RocketPlugin<Config>
    {
        public static BuilderMode Instance;
        public static string PluginName = "BuilderMode";
        public static string PluginVersion = "1.0.0";
        protected override void Load()
        {
            Instance = this;
            Logger.Log("BuilderMode has been loaded!");
            Logger.Log(PluginName + PluginVersion, ConsoleColor.Yellow);
            Logger.Log("Made by Tortellio", ConsoleColor.Yellow);
        }
        protected override void Unload()
        {
            Instance = null;
            Logger.Log("BuilderMode has been unloaded!");
            Logger.Log("Visit Tortellio Discord for more! https://discord.gg/pzQwsew", ConsoleColor.Yellow);
        }
        public void DoBuilder(UnturnedPlayer caller)
        {
            if (caller.Player.look.canUseWorkzone || caller.Player.look.canUseFreecam || caller.Player.look.canUseSpecStats)
            {
                caller.Player.look.sendFreecamAllowed(false);
                caller.Player.look.sendSpecStatsAllowed(false);
                caller.Player.look.sendWorkzoneAllowed(false);
                if (Configuration.Instance.EnableServerAnnouncer) UnturnedChat.Say(Instance.Translate("b_off_message", caller.CharacterName), UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Color.yellow));
            }
            else
            {
                if (caller.HasPermission("builder.freecam")) { caller.Player.look.sendFreecamAllowed(true); UnturnedChat.Say(caller, Instance.Translate("has_freecam")); }
                if (caller.HasPermission("builder.builder")) { caller.Player.look.sendWorkzoneAllowed(true); UnturnedChat.Say(caller, Instance.Translate("has_builder")); }
                if (caller.HasPermission("builder.name")) { caller.Player.look.sendSpecStatsAllowed(true); UnturnedChat.Say(caller, Instance.Translate("has_name")); }
                if (Configuration.Instance.EnableServerAnnouncer) UnturnedChat.Say(Instance.Translate("b_on_message", caller.CharacterName), UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Color.yellow));
            }
        }
        public void CheckBuilder(UnturnedPlayer cplayer, IRocketPlayer caller)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (cplayer != null && (cplayer.Player.look.canUseWorkzone || cplayer.Player.look.canUseFreecam || cplayer.Player.look.canUseSpecStats))
            {
                if (caller is ConsolePlayer)
                {
                    UnturnedChat.Say(Instance.Translate("cb_on_message", "Console", cplayer.DisplayName));
                }
                else if (player is UnturnedPlayer)
                {
                    UnturnedChat.Say(Instance.Translate("cb_on_message", caller.DisplayName, cplayer.DisplayName));
                }
                return;
            }
            else if (cplayer == null)
            {
                UnturnedChat.Say(caller, Translate("cb_not_found"));
                return;
            }
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "has_freecam", "Free Camera enabled!" },
            { "has_builder", "Builder enabled!" },
            { "has_name", "Player Names enabled!" },
            { "no_permission", "You do not have the correct permissions to use /builder" },
            { "b_on_message", "{0} entered builder mode." },
            { "b_off_message", "{0} exit builder mode." },
            { "cb_on_message", "{0} has confirmed that {1} has builder role." },
            { "cb_off_message", "{0} has confirmed that {1} doesn't have builder role." },
            { "cb_not_found", "Player is not online or invalid." },
            { "cb_usage", "No argument was specified. Please use \"cb <playername>\" to check on a player." }
        };
    }
}
