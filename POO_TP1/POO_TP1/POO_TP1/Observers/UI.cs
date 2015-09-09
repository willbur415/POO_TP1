using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    class UI : ObservedSubject, Observer
    {
        private const int LIFE_ORIGIN_POS = 7;
        private const int LIFE_SPACING = 5;
        private static UI ui;
        private int numberOfLife;
        private int score;
        private int scoreMultiplier;
        private Texture2D playerLifeImage;
        private Texture2D backGroundUI;
        private SpriteFont scoreFont;
        private Vector2 origin;
        private Vector2 textPos;
        private int nbLifeUP = 1;
        private string bonusMessage;

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
            this.score = 0;
            this.scoreMultiplier = 1;
            this.AddObserver(PlayerShip.GetInstance());
            playerLifeImage = Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\PlayerShipLife");
            backGroundUI = Game1.contentManager.Load<Texture2D>("Graphics\\UI\\UI");
            origin = new Vector2(LIFE_ORIGIN_POS, LIFE_ORIGIN_POS);
            scoreFont = Game1.contentManager.Load<SpriteFont>("kootenay");
            textPos = Vector2.Zero;
            this.bonusMessage = "";
        }

        public void draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGroundUI, Vector2.Zero, Color.White);

            textPos.X = 650;

            spriteBatch.DrawString(scoreFont, score.ToString(), textPos, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            textPos.X = 1050;
            spriteBatch.DrawString(scoreFont, "Level " + LevelManager.GetInstance().CurrentLevel, textPos, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            textPos.X = 1000;
            textPos.Y = 700;
            if (bonusMessage != "")
            {
                spriteBatch.DrawString(scoreFont, bonusMessage, textPos, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            }

            for (int i = 0; i < numberOfLife; i++)
            {
                spriteBatch.Draw(playerLifeImage, origin, Color.White);
                    origin.X += playerLifeImage.Width + LIFE_SPACING;
            }
            origin.X = LIFE_ORIGIN_POS;
            textPos = Vector2.Zero;
        }

        public int NumberOfLife
        {
            get
            {
                return numberOfLife;
            }
            set
            {
                numberOfLife = value;
            }
        }

        public int ScoreMultiplier
        {
            get
            {
                return scoreMultiplier;
            }
            set
            {
                scoreMultiplier = value;
            }
        }

        private void UpdateScore(int score)
        {
            this.score += score * scoreMultiplier;
            if (this.score > 10000 * nbLifeUP)
            {
                nbLifeUP++;
                UpdateLife(PlayerShip.GetInstance().NumberOfLifes + 1);
            }
        }

        private void UpdateLife(int life)
        {
            this.numberOfLife = life;
            this.NotifyAllObservers();
        }

        public void Notify(ObservedSubject subject)
        {
            if (subject is Asteroid)
            {
                Asteroid ast = subject as Asteroid;
                if (ast.Size == AsteroidSize.large)
                {
                    UpdateScore(20);
                }
                else if (ast.Size == AsteroidSize.medium)
                {
                    UpdateScore(50);
                }
                else
                {
                    UpdateScore(100);
                }

            }
            else if (subject is EnemyShip && (subject as EnemyShip).IsAlive == false)
            {
                EnemyShip ship = subject as EnemyShip;
                if (ship.Type == TypeShip.bigBossShip)
                {
                    UpdateScore(200);
                }
            }
            else if (subject is PlayerShip)
            {
                if (!(subject as PlayerShip).IsAlive)
                {
                    UpdateLife(PlayerShip.GetInstance().NumberOfLifes);
                    NotifyAllObservers();
                }
                else if ((subject as PlayerShip).NumberOfLifes != this.numberOfLife)
                {
                    this.numberOfLife = PlayerShip.GetInstance().NumberOfLifes;
                }
            }
            else if (subject is Bonus)
            {
                if ((subject as Bonus).Type == BonusType.doublePoints)
                {
                    if ((subject as Bonus).BonusTime > 0)
                    {
                        bonusMessage = "Double Points";
                        scoreMultiplier = 2;
                    }
                    else
                    {
                        bonusMessage = "";
                        scoreMultiplier = 1;
                    }
                }
                else if ((subject as Bonus).Type == BonusType.invincible)
                {
                    if ((subject as Bonus).BonusTime > 0)
                    {
                        bonusMessage = "Invincible";
                    }
                    else
                    {
                        bonusMessage = "";
                    }
                }
                else if ((subject as Bonus).Type == BonusType.extraPoints)
                {
                    if ((subject as Bonus).BonusTime > 0)
                    {
                        score += 1000;
                    }
                }
                else if ((subject as Bonus).Type == BonusType.slowDown)
                {
                    if ((subject as Bonus).BonusTime > 0)
                    {
                        bonusMessage = "SlowMotio";
                    }
                    else
                    {
                        bonusMessage = "";
                    }
                }
            }
        }

        public int Score
        {
            get 
            {
                return score; 
            }
            set
            {
                score = value;
            }
        }
    }
}
