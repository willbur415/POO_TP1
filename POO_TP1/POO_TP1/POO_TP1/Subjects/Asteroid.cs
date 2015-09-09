using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace POO_TP1
{
    public enum AsteroidSize { small, medium, large}

    public class Asteroid : MovableObject
    {
        private AsteroidSize size;
        private const float ASTEROIDS_SPEED = 2;
        private bool isSlow;
        private bool isUIObserved;

        public AsteroidSize Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public bool IsUIObserver
        {
            get
            {
                return isUIObserved;
            }
            set
            {
                isUIObserved = value;
            }
        }


        public Asteroid(Texture2D image, Vector2 position, float rotationAngle, AsteroidSize size) : base(image, position)
        {
            this.rotationAngle = rotationAngle;
            velocity.X = (float)(Math.Sin((double)rotationAngle) * ASTEROIDS_SPEED);
            velocity.Y = (float)(Math.Cos((double)rotationAngle) * ASTEROIDS_SPEED);
            this.size = size;
            this.isSlow = false;
            IsUIObserver = false;
        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        public void Move()
        {
            if (PlayerShip.GetInstance().CurrentBonus.Type == BonusType.slowDown)
            {
                if (!this.isSlow)
                {
                    this.isSlow = true;
                    this.velocity.X /= 2;
                    this.velocity.Y /= 2;
                }
                if (PlayerShip.GetInstance().CurrentBonus.BonusTime < 0)
                {
                    this.isSlow = false;
                    this.velocity.X *= 2;
                    this.velocity.Y *= 2;
                }
            }
            base.Move();
        }

        public override void CheckCollisionBox(Objet2D theOther)
        {
            // override if needed
        }

        /// <summary>
        /// Splits the asteroid based on bullet angle.
        /// </summary>
        /// <param name="bulletRotationAngle">The bullet rotation angle.</param>
        public void Split(float bulletRotationAngle)
        {
            if (size == AsteroidSize.large)
            {
                AddSmallerAsteroid(AsteroidSize.medium, bulletRotationAngle);
            }
            else if (size == AsteroidSize.medium)
            {
                AddSmallerAsteroid(AsteroidSize.small, bulletRotationAngle);
            }
            else
            {
                LevelManager.GetInstance().DeadAsteroids.Add(this);
            }
            NotifyAllObservers();
        }

        /// <summary>
        /// Checks the size of the asteroid.
        /// </summary>
        /// <returns></returns>
        public float CheckAsteroidSize()
        {
            if (size == AsteroidSize.small)
            {
                return 0.08f;
            }
            if (size == AsteroidSize.medium)
            {
                return 0.1f;
            }
            return 0.3f;   
        }

        /// <summary>
        /// transforms bigger asteroids into smaller asteroids
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="bulletRotationAngle">The bullet rotation angle.</param>
        private void AddSmallerAsteroid(AsteroidSize size, float bulletRotationAngle)
        {
            float newRotationAngle = (float)((Math.PI * 2) / 3);
            Asteroid newAst1;
            Asteroid newAst2;

            LevelManager.GetInstance().DeadAsteroids.Add(this);
            if (size == AsteroidSize.medium)
            {
                newAst1 = new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_medium"), this.position, bulletRotationAngle + newRotationAngle, size);
                newAst2 = new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_medium"), this.position, bulletRotationAngle - newRotationAngle, size);
            }
            else
            {
                newAst1 = new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_small"), this.position, bulletRotationAngle + newRotationAngle, size);
                newAst2 = new  Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_small"), this.position, bulletRotationAngle - newRotationAngle, size);
            }
            LevelManager.GetInstance().Asteroids.Add(newAst1);
            LevelManager.GetInstance().Asteroids.Add(newAst2);
        }
    }
}
