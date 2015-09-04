﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using POO_TP1;


namespace POO_TP1
{
    class PlayerShip : MovableObject, Observer
    {
        private const double MAXSPEED = 100.0;
        private const float SLOWFACTOR = 20.0f;
        private const int MAX_BULLETS = 10;
        private const int COOLDOWN_TIME = 20;
        private bool alive;
        private int playerTotalLife = 3;
        private static PlayerShip ship;
        private Bullet[] bullets;
        private bool firstShot = false;
        private int cooldown;

        public const int TIME_BETWEEN_SHOTS_MILI = 3000;

        public static PlayerShip GetInstance()
        {
            if (ship == null)
            {
                ship = new PlayerShip();
            }
            return ship;
        }
        private PlayerShip()     
        {
            alive = true;
        }

        public void Initialize(Texture2D image, Vector2 position)
        {
            base.image = image;
            base.position = position;
            bullets = new Bullet[MAX_BULLETS];
            FillObject2DInfo();
        }

        public Bullet[] Bullets
        {
            get
            {
                return bullets;
            }
            set
            {
                bullets = value;
            }
        }

        public bool FirstShot
        {
            get
            {
                return firstShot;
            }
            set
            {
                firstShot = value;
            }
        }

        public bool Alive
        {
            get
            {
                return alive;
            }
        }


        public int Cooldown
        {
            get
            {
                return cooldown;
            }
            set
            {
                cooldown = value;
            }
        }

        public int PlayerTotalLife
        {
            get { return playerTotalLife; }
            set { playerTotalLife = value; }
        }

        public void CheckCollisionSphere(Objet2D theOther)
        {
            if (sphereCollision.Intersects(theOther.SphereCollision))
            {
                alive = false;
            }
        }

        public override void CheckCollisionBox(Objet2D theOther)
        {
            if (boiteCollision.Intersects(theOther.BoiteCollision))
            {
                alive = false;
            }
        }

        public void MoveShip(float newThrust)
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            //Rappel, le thrust arrière doit être plus lent
            if (newThrust < 0)
                newThrust /= 2;

            //Angle 0 est un vecteur qui pointe vers le haut, et on augmente l'angle dans le sens des aiguilles d'une montre

            velocity.X += (float)(Math.Sin((double)rotationAngle) * newThrust) / SLOWFACTOR;
            velocity.Y -= (float)(Math.Cos((double)rotationAngle) * newThrust) / SLOWFACTOR;

            //Dans bien des vieux jeux la vitesse maximum semble être par axe, mais comme on a de la puissance de calcul, on va la faire totale.
            MaxThrust();

            //Il faut aussi déplacer les poly de collision
            move();
            bulletsFollow();
        }

        private void bulletsFollow()
        {
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.IsShooted)
                    bullet.Position = this.position;
            }
        }

        private void MaxThrust()
        {
            //Calcul de la vitesse maximum actuelle (pythagore)
            double totalSpeed = Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));

            //Si plus grande que la vitesse actuelle, on se fait un ratio
            if (totalSpeed > MAXSPEED)
            {
                float ratio = (float)(MAXSPEED / totalSpeed);
                velocity.X *= ratio;
                velocity.Y *= ratio;
            }
        }

        public bool Shoot()
        {
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.IsShooted)
                {
                    bullet.Velocity = new Vector2((float)Math.Sin((double)rotationAngle) * 10, -(float)Math.Cos((double)rotationAngle) * 10);
                    bullet.IsShooted = true;
                    cooldown = COOLDOWN_TIME;
                    return true;
                }
            }
            return false;
        }

        public void InitBullets(ContentManager content)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Bullet(content.Load<Texture2D>("Graphics\\sprites\\Bullet"));
            }
        }

        public void Notify(ObservedSubject subject)
        {

        }
    }
}
