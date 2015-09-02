using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    enum AsteroidSize { small, medium, large}

    class Asteroid : Objet2D
    {
        private Vector2 velocity;
        private AsteroidSize size;

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set 
            {
                velocity = value;
            }
        }

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

        public Asteroid(Texture2D image, Vector2 position, Vector2 velocity) : base(image, position)
        {
            Velocity = velocity;
        }

        public void Move()
        {
            position += Velocity;

            //OtherSide(ref position.X, ref boiteCollision.Min.X, ref boiteCollision.Max.X, ref sphereCollision.Center.X, Game1.SCREENWIDTH, image.Width);
            //OtherSide(ref position.Y, ref boiteCollision.Min.Y, ref boiteCollision.Max.Y, ref sphereCollision.Center.Y, Game1.SCREENHEIGHT, image.Height);
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


        private void OtherSide(ref float position, ref float minBox, ref float maxBox, ref float sphere, int screenSize, int imageSize)
        {
            if (position > screenSize + imageSize / 2)
            {
                position -= screenSize + imageSize;
                minBox -= screenSize + imageSize;
                maxBox -= screenSize + imageSize;
                sphere -= screenSize + imageSize;
            }
            else if (position < -imageSize / 2)
            {
                position += screenSize + imageSize;
                minBox += screenSize + imageSize;
                maxBox += screenSize + imageSize;
                sphere += screenSize + imageSize;
            }
        }

    }
}
