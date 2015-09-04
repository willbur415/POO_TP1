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

        public void Update()
        {
            this.position += this.velocity;

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

        public override void CheckCollisionBox(Objet2D theOther)
        {
            if(boiteCollision.Intersects(theOther.BoiteCollision))
            {
                resetBullet();
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

        private void resetBullet()
        {
            this.velocity = Vector2.Zero;
            this.position.X = BULLET_SPAWN_POS;
            this.position.Y = BULLET_SPAWN_POS;
            lifeTime = BULLET_LIFE_TIME;
        }
    }
}
