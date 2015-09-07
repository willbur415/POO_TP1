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

        public Asteroid(Texture2D image, Vector2 position, float rotationAngle, AsteroidSize size) : base(image, position)
        {
            this.rotationAngle = rotationAngle;
            velocity.X = (float)(Math.Sin((double)rotationAngle) * ASTEROIDS_SPEED);
            velocity.Y = (float)(Math.Cos((double)rotationAngle) * ASTEROIDS_SPEED);
            this.size = size;
            this.AddObserver(UI.GetInstance());
        }

        public void Move()
        {
            base.move();
        }

        public override void CheckCollisionBox(Objet2D theOther)
        {
            // override if needed
        }

        public void Split()
        {
            if (size == AsteroidSize.large)
            {
                addSmallerAsteroid(AsteroidSize.medium);
            }
            else if (size == AsteroidSize.medium)
            {
                addSmallerAsteroid(AsteroidSize.small);
            }
            else
            {
                LevelManager.GetInstance().DeadAsteroids.Add(this);
            }
            NotifyAllObservers();
        }

        public float CheckAsteroidSize()
        {
            if (size == AsteroidSize.small)
            {
                return 0.08f;
            }
            else if (size == AsteroidSize.medium)
            {
                return 0.1f;
            }
            else
            {
                return 0.3f;
            }
        }

        private void addSmallerAsteroid(AsteroidSize size)
        {
            float newRotationAngle = (float)((Math.PI * 2) / 3);
            Asteroid newAst1;
            Asteroid newAst2;

            LevelManager.GetInstance().DeadAsteroids.Add(this);
            if (size == AsteroidSize.medium)
            {
                newAst1 = new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_medium"), this.position, this.rotationAngle + newRotationAngle, size);
                newAst2 = new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_medium"), this.position, this.rotationAngle - newRotationAngle, size);
                LevelManager.GetInstance().Asteroids.Add(new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_medium"), this.position, this.rotationAngle - newRotationAngle, size));
                LevelManager.GetInstance().Asteroids.Add(new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_medium"), this.position, this.rotationAngle + newRotationAngle, size));
            }
            else
            {
                LevelManager.GetInstance().Asteroids.Add(new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_small"), this.position, this.rotationAngle + newRotationAngle, size));
                LevelManager.GetInstance().Asteroids.Add(new Asteroid(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\asteroid_small"), this.position, this.rotationAngle - newRotationAngle, size));
            }
            
        }
    }
}
