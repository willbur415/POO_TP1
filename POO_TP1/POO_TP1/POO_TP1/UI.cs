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
        private const int LIFE_ORIGIN_POS = 7;
        private const int LIFE_SPACING = 5;
        private static UI ui;
        private int numberOfLife;
        private int score;
        private Texture2D playerLifeImage;
        private Texture2D backGroundUI;
        private SpriteFont scoreFont;
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
            this.score = 10;
            playerLifeImage = Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\PlayerShipLife");
            backGroundUI = Game1.contentManager.Load<Texture2D>("Graphics\\UI\\UI");
            origin = new Vector2(LIFE_ORIGIN_POS, LIFE_ORIGIN_POS);
            scoreFont = Game1.contentManager.Load<SpriteFont>("kootenay");
        }

        public void draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGroundUI, Vector2.Zero, Color.White);
            string scoreText = Convert.ToString(score);
            Vector2 pos = new Vector2(650, 0);
            spriteBatch.DrawString(scoreFont, scoreText, pos, Color.White, 0.0f , Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);

            for (int i = 0; i < numberOfLife; i++)
            {
                spriteBatch.Draw(playerLifeImage, origin, Color.White);
                    origin.X += playerLifeImage.Width + LIFE_SPACING;
            }
            origin.X = LIFE_ORIGIN_POS;
        }

        public void updateScore(int score)
        {
            this.score += score;
        }

        public void updateLife(int life)
        {
            this.numberOfLife += life;
        }
    }
}
