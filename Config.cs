using Rocket.API;

namespace Tortellio.BuilderMode
{
    public class Config : IRocketPluginConfiguration
    {
        public bool EnableServerAnnouncer;
        public string MessageColor;
        public void LoadDefaults()
        {
            EnableServerAnnouncer = true;
            MessageColor = "Yellow";
        }
    }
}