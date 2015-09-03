using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    class GameMenu
    {
        private static GameMenu menu;
        private SpriteFont font;

        private string playButton;
        private string exitButton;
        private string optionsButton;

        private Vector2 fontPos;
        private Vector2 cursorPosition;
        private Vector2 playButtonPosition;
        private Vector2 exitButtonPosition;
        private Vector2 optionsButtonPosition;

        public static GameMenu GetInstance()
        {
            if (menu == null)
            {
                menu = new GameMenu();
            }

            return menu;
        }

        public void Initialize(SpriteFont font,ref GraphicsDeviceManager graphics)
        {
            this.font = font;
            fontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                     graphics.GraphicsDevice.Viewport.Height / 2);
            playButton = "Play";
            exitButton = "Exit";
            optionsButton = "Options";
        }

        public void CallMenu(ref SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font,playButton,fontPos,Color.LightGreen,0,font.MeasureString(playButton) / 2, 1.0f, SpriteEffects.None, 0,5f);
        }
    }
}
