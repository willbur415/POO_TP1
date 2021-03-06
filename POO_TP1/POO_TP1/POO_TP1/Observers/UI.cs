﻿using System;
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
        private const int STARTING_LIFE = 3;
        private int numberOfLife;
        private int score;
        private int scoreMultiplier;
        private string bonusMessage;
        private Texture2D playerLifeImage;
        private Texture2D backGroundUI;
        private SpriteFont scoreFont;
        private Vector2 origin;
        private Vector2 textPos;
        private int nbLifeUP = 1;

        public UI()
        {
            this.numberOfLife = STARTING_LIFE;
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

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
           
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGroundUI, Vector2.Zero, Color.White);

            textPos.X = 650;
            textPos.Y = 0;

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

        /// <summary>
        /// Updates the score.
        /// </summary>
        /// <param name="score">The score.</param>
        private void UpdateScore(int score)
        {
            this.score += score * scoreMultiplier;
            if (this.score > 10000 * nbLifeUP)
            {
                nbLifeUP++;
                UpdateLife(PlayerShip.GetInstance().NumberOfLifes + 1);
            }
        }

        /// <summary>
        /// Updates the life.
        /// </summary>
        /// <param name="life">The life.</param>
        private void UpdateLife(int life)
        {
            this.numberOfLife = life;
            this.NotifyAllObservers();
        }

        /// <summary>
        /// Notifies the specified subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
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
                }
                else if ((subject as PlayerShip).NumberOfLifes != this.numberOfLife)
                {
                    this.numberOfLife = PlayerShip.GetInstance().NumberOfLifes;
            }
            else if (subject is Bonus)
            {
                if ((subject as Bonus).Type == BonusType.invincible)
                {
                    bonusMessage = "Invincible";
                }
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
                else if ((subject as Bonus).Type == BonusType.none)
                {
                    if ((subject as Bonus).BonusTime == -1)
                    {
                        scoreMultiplier = 1;
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
