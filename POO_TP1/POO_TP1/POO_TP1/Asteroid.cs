using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    enum AsteroidSize { small, medium, large}

    class Asteroid : MovableObject
    {
        private AsteroidSize size;
        private Random rand;
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

        public Asteroid(Texture2D image, Vector2 position, float rotationAngle) : base(image, position)
        {
            rand = new Random();
            velocity.X = (float)(Math.Sin((double)rotationAngle) * ASTEROIDS_SPEED);
            velocity.Y = (float)(Math.Cos((double)rotationAngle) * ASTEROIDS_SPEED);
        }

        public new void Move()
        {
            base.move();
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
    }
}
