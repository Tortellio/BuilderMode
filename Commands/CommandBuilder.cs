using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace Tortellio.BuilderMode.Commands
{
    public class CommandBuilder : IRocketCommand
    {
        public string Name => "builder";
        public string Help => "Enter builder mode";
        public string Syntax => "";
        public List<string> Aliases => new List<string>() { "b" };
        public List<string> Permissions => new List<string>() { "builder" };
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (caller == null) return;
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (player.HasPermission("builder.freecam") || player.HasPermission("builder.builder") || player.HasPermission("builder.name"))
            {
                BuilderMode.Instance.DoBuilder(player);
                return;
            }

            if (caller.HasPermission("duty")) UnturnedChat.Say(caller, BuilderMode.Instance.Translate("no_permission"));
        }
    }
}
