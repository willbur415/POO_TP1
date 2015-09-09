using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    public class Bullet : MovableObject
    {
        private Boolean isShooted;
        private const int BULLET_SPAWN_POS = -100;
        private const int BULLET_LIFE_TIME = 100;
        private int lifeTime = BULLET_LIFE_TIME;

        public Bullet(Texture2D image, Vector2 position)
            : base(image, position)
        {
            FillObject2DInfo();
        }

        public Bullet(Texture2D image)
        {
            this.position = new Vector2(BULLET_SPAWN_POS, BULLET_SPAWN_POS);
            this.image = image;
            this.velocity = new Vector2(0, 0);
            FillObject2DInfo();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            base.Move();

            if (isShooted)
            {
                if (lifeTime < 0)
                {
                    isShooted = false;
                }
                lifeTime--;
            }
            else
            {
                resetBullet();
            }
        }

        /// <summary>
        /// Checks the collision box that is hit by the bullet.
        /// </summary>
        /// <param name="theOther">The other.</param>
        public override void CheckCollisionBox(Objet2D theOther)
        {
            if (this.isShooted)
            {
                if (boiteCollision.Intersects(theOther.BoiteCollision))
                {
                    if (base.IsAsteroid(theOther))
                    {
                        (theOther as Asteroid).Split(this.rotationAngle);
                    }
                    if (IsEnemyShip(theOther))
                    {
                        (theOther as EnemyShip).LoseLife();
                    }
                    resetBullet();
                }
            }
        }

        public Boolean IsShooted
        {
            get
            {
                return isShooted;
            }
            set
            {
                isShooted = value;
            }
        }

        public int LifeTime
        {
            get
            {
                return lifeTime;
            }
            set 
            {
                lifeTime = value;
            }
        }

        /// <summary>
        /// Resets the bullet.
        /// </summary>
        private void resetBullet()
        {
            this.velocity = Vector2.Zero;
            this.position.X = Vector2.Zero.X + BULLET_SPAWN_POS;
            this.position.Y = Vector2.Zero.Y + BULLET_SPAWN_POS;
            lifeTime = BULLET_LIFE_TIME;
            isShooted = false;
        }
    }
}
