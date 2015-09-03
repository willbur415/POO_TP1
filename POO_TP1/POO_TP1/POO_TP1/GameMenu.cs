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

        private int selectedItemIndex;
        private int nbMenuItems;
        private string[] menuItems = new string[3];
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
            menuItems[0] = "Play";
            menuItems[1] = "Options";
            menuItems[2] = "Exit";
            nbMenuItems = 3;
            selectedItemIndex = 0;
            this.font = font;
            playButtonPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                     graphics.GraphicsDevice.Viewport.Height / 2);
            optionsButtonPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                     graphics.GraphicsDevice.Viewport.Height / 2 + 75);
            exitButtonPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                     graphics.GraphicsDevice.Viewport.Height / 2 + 150);
            fontOrigin = font.MeasureString(menuItems[0]) / 2;

            
        }

        public void choiceUp()
        {
            if (selectedItemIndex - 1 >= 0)
            {
                
            }
        }

        public void choiceDown()
        {
        }

        public int SelectedItemIndex
        {
            get { return selectedItemIndex;}
            set { selectedItemIndex = value; }
        }

        public String PlayButton
        {
            get {return menuItems[0]; }
        }
        public String ExitButton
        {
            get { return menuItems[2]; }
        }
        public String OptionsButton
        {
            get { return menuItems[1]; }
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
