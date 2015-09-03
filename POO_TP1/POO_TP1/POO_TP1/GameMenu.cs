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
        private Vector2 playButtonPos;
        private Vector2 optionsButtonPos;
        private Vector2 exitButtonsPos;
        private Vector2 fontOrigin;
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
            playButtonPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                     graphics.GraphicsDevice.Viewport.Height / 2);
            optionsButtonPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                     graphics.GraphicsDevice.Viewport.Height / 2 + 75);
            exitButtonPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                     graphics.GraphicsDevice.Viewport.Height / 2 + 150);
            playButton = "Play";
            exitButton = "Exit";
            optionsButton = "Options";
            fontOrigin = font.MeasureString(playButton) / 2;
        }

        public String PlayButton
        {
            get {return playButton; }
        }
        public String ExitButton
        {
            get { return exitButton; }
        }
        public String OptionsButton
        {
            get { return optionsButton; }
        }

        public Vector2 PlayButtonPosition
        {
            get { return playButtonPosition;}
        }

        public SpriteFont Font
        {
            get { return font;}
        }

        public Vector2 PlayButtonPos
        {
            get { return playButtonPos;}
        }
        public Vector2 OptionsButtonPos
        {
            get { return optionsButtonPos; }
        }
        public Vector2 ExitButtonPos
        {
            get { return exitButtonPosition; }
        }

        public Vector2 FontOrigin
        {
            get { return fontOrigin;}
        }
    }
}
