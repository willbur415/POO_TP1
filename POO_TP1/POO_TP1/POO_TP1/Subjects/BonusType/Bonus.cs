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
        invincible, extraLife, doublePoints, slowDown, extraPoints
    }

    public abstract class Bonus : MovableObject
    {
        protected int bonusTime;
        protected bool isFinished;

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

        public Bonus(Texture2D image, Vector2 position)
            : base(image, position)
        {
            this.AddObserver(PlayerShip.GetInstance());
        }

        public override void CheckCollisionBox(Objet2D theOther)
        {
            if (this.boiteCollision.Intersects(theOther.BoiteCollision))
            {
                this.NotifyAllObservers();
            }
        }

        public abstract void StartEffect();
        public abstract void Update();
    }
}
