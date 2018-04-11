using System;
using SwinGameSDK;
using static SwinGameSDK.SwinGame;

namespace Battleship
{
    class GameLogic
    {

        public static void Main()
        {

            // Opens a new Graphics Window
            SwinGame.OpenGraphicsWindow("Battle Ships", 800, 600);
            // Load Resources
            GameResources.LoadResources();
            SwinGame.PlayMusic(GameResources.GameMusic("Background"));
            // Game Loop
            while ((((SwinGame.WindowCloseRequested() == true) || (GameResources.CurrentState == GameState.Quitting)) == false))
            {
                HandleUserInput();
                DrawScreen();
            }

            SwinGame.StopMusic();
            // Free Resources and Close Audio, to end the program.
            FreeResources();
        }
    }
}

