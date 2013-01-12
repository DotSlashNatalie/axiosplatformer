using GameStateManagement;
using Microsoft.Xna.Framework;
using AxiosPlatformer.GameScreens;

namespace AxiosPlatformer.MenuScreens
{
    class MainMenu : MenuScreen
    {
        public MainMenu() : base("Axios Platformer") { }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            MenuEntries.Add(new MenuEntry("New Game"));
            //MenuEntries.Add(new MenuEntry("Load Game"));
            //MenuEntries.Add(new MenuEntry("Options"));
            MenuEntries.Add(new MenuEntry("Exit Game"));
        }

        protected override void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            base.OnSelectEntry(entryIndex, playerIndex);

            switch (entryIndex)
            {
                case 0:
                    ScreenManager.AddScreen(new LevelLoader(), PlayerIndex.One);
                    break;
                case 1:
                    break;
                case 3:
                    this.ScreenManager.Game.Exit();
                    break;
                default:
                    break;
            }
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            base.OnCancel(playerIndex);

            ScreenManager.Game.Exit();
        }
    }
}
