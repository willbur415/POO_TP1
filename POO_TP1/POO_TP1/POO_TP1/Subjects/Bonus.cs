using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using POO_TP1;

namespace POO_TP1
{

    public enum BonusType
    {
        none, invincible, extraLife, doublePoints, slowDown, extraPoints
    }

    public class Bonus : MovableObject
    {
        private const int BONUS_15_SECONDS = 900;
        private int bonusTime;
        private bool isFinished;
        private BonusType type;

        public int BonusTime
        {
            get
            {
                return bonusTime;
            }
            set
            {
                bonusTime = value;
            }
        }

        public bool IsFinished
        {
            get
            {
                return isFinished;
            }
            set
            {
                isFinished = value;
            }
        }

        public BonusType Type
        {
            get
            {
                return type;
        }
            set
            {
                type = value;
            }
        }

        public Bonus(Texture2D image, Vector2 position, BonusType type)
            : base(image, position)
        {
            this.AddObserver(PlayerShip.GetInstance());
            this.AddObserver(UI.GetInstance());
            this.type = type;
            bonusTime = BONUS_15_SECONDS;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            if (this.type != BonusType.none)
            {
                if (this.type == BonusType.extraLife)
                {
                    PlayerShip.GetInstance().AddLife();
                    this.type = BonusType.none;
                }
                if (this.type == BonusType.extraPoints)
                {
                    bonusTime = 0;
                    this.NotifyAllObservers();
                    this.type = BonusType.none;
                }
                UpdateTimer();
            }
        }

        /// <summary>
        /// Checks the collision box.
        /// </summary>
        /// <param name="theOther">The other.</param>
        public override void CheckCollisionBox(Objet2D theOther)
        {
            if (this.boiteCollision.Intersects(theOther.BoiteCollision))
            {
                this.NotifyAllObservers();
            }
        }

        /// <summary>
        /// Updates the timer for the bonus.
        /// </summary>
        private void UpdateTimer()
        {
            if (bonusTime > 0)
            {
                bonusTime--;
            }
            else
            {
                if (type != BonusType.none)
                {
                    this.NotifyAllObservers();
            }
                type = BonusType.none;
                bonusTime = BONUS_15_SECONDS;
            }
        }
    }
}
