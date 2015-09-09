using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    public enum TypeShip {littleShip, bigShip, bigBossShip}

    public abstract class EnemyShip : MovableObject
    {
        private const int MAX_BULLETS = 3;
        private const int BULLET_SPAWN_POS = -100;

        protected TypeShip type;
        private const double ENEMY_SPEED = 0.7;
        private  int enemyHP = 5;
        private bool isAlive = true;
        private Bullet[] bullets;
        
        public TypeShip Type
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

        public EnemyShip(Texture2D image, Vector2 position, TypeShip type)
            : base(image, position)
        {
            double angle = 1.6;
            rotationAngle = (float) angle ;
            velocity.X = (float)(Math.Sin((double)rotationAngle) * ENEMY_SPEED);
            velocity.Y = (float)(Math.Cos((double)rotationAngle) * ENEMY_SPEED);
            Type = type;
            bullets = new Bullet[MAX_BULLETS];
            AddObserver(UI.GetInstance());
        }

        public override void CheckCollisionBox(Objet2D theOther)
        {
            
        }

        private void InitializeEnemyBullet()
        {
            
        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        public new void Move()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
            UpdateCollision();
        }

        /// <summary>
        /// decrements life when called.
        /// </summary>
        public void LoseLife()
        {
            enemyHP--;
            if (enemyHP <= 0)
            {
                isAlive = false;
            }
            NotifyAllObservers();
        }
        /// <summary>
        /// Initializes the bullets.
        /// </summary>
        /// <param name="content">The content.</param>
        public void InitBullets(ContentManager content)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Bullet(content.Load<Texture2D>("Graphics\\sprites\\EnemyBullet"), new Vector2(BULLET_SPAWN_POS, BULLET_SPAWN_POS));
                bullets[i].IsEnemyBullet = true;
            }
        }

        public int EnemyHP
        {
            get { return enemyHP;}
        }

        public bool IsAlive
        {
            get { return isAlive;}
        }
        /// <summary>
        /// Gets or sets the bullets.
        /// </summary>
        /// <value>
        /// The bullets.
        /// </value>
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
    }
}
