using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    class UI
    {
        private const int LIFE_ORIGIN_POS = 15;
        private const int LIFE_SPACING = 5;
        private static UI ui;
        private int numberOfLife;
        private int score;
        private Texture2D playerLifeImage;
        private Vector2 origin;

        public static UI GetInstance()
        {
            if (ui == null)
            {
                ui = new UI();
            }
            return ui;
        }

        public void Initialize()
        {
            this.numberOfLife = 3;
            this.score = 3;
            playerLifeImage = Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\PlayerShipLife");
            origin = new Vector2(LIFE_ORIGIN_POS, LIFE_ORIGIN_POS);
        }

        public void draw(ref SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberOfLife; i++)
            {
                spriteBatch.Draw(playerLifeImage, origin, Color.Pink);
                    origin.X += playerLifeImage.Width + LIFE_SPACING;
            }
            origin.X = LIFE_ORIGIN_POS;
        }
    }
}
