using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace Tortellio.BuilderMode.Commands
{
    public class CommandCheckBuilder : IRocketCommand
    {
        public string Name => "checkbuilder";
        public string Help => "Checks if a player is in builder mode or not";
        public string Syntax => "<playername>";
        public List<string> Aliases => new List<string>() { "cb" };
        public List<string> Permissions => new List<string>() { "builder.check" };
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public void Execute(IRocketPlayer caller, string[] command)
        {
            if(command.Length == 0)
            {
                UnturnedChat.Say(caller, BuilderMode.Instance.Translate("cb_usage"));
                return;
            }
            else if (command.Length > 0)
            {
                UnturnedPlayer cplayer = UnturnedPlayer.FromName(command[0]);
                BuilderMode.Instance.CheckBuilder(cplayer, caller);
            }
        }
    }
}
