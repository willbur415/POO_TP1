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
            this.type = type;
            bonusTime = 900;
        }

        public void Update()
        {
            if (this.type != BonusType.none)
            {
                if (bonusTime > 0)
                {
                    bonusTime--;
                }
                else
                {
                    type = BonusType.none;
                }
            }
        }

        public override void CheckCollisionBox(Objet2D theOther)
        {
            if (this.boiteCollision.Intersects(theOther.BoiteCollision))
            {
                this.NotifyAllObservers();
            }
        }
    }
}
