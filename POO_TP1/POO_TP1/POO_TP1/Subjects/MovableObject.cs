using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POO_TP1.Subjects.Ships;

namespace POO_TP1
{
    public abstract class MovableObject : Objet2D
    {
        public MovableObject(Texture2D image, Vector2 position)
            : base(image, position)
        {
        }
        public MovableObject()
        {
        }

        protected void Move()
        {
            this.position.X += this.velocity.X;
            this.position.Y += this.velocity.Y;

            UpdateCollision();

            OtherSide(ref position.X, ref boiteCollision.Min.X, ref boiteCollision.Max.X, ref sphereCollision.Center.X, Game1.SCREENWIDTH, image.Width);
            OtherSide(ref position.Y, ref boiteCollision.Min.Y, ref boiteCollision.Max.Y, ref sphereCollision.Center.Y, Game1.SCREENHEIGHT, image.Height);
        }

        protected bool IsAsteroid(Objet2D objet)
        {
            if (objet.GetType() == typeof(Asteroid))
            {
                return true;
            }
            return false;
        }
        protected bool IsEnemyShip(Objet2D objet)
        {
            if (objet.GetType() == typeof(LittleShip) || objet.GetType() == typeof(BigShip) || objet.GetType() == typeof(BigBossShip))
            {
                return true;
            }
            return false;
        }

        public abstract void CheckCollisionBox(Objet2D theOther);

        protected void UpdateCollision()
        {
            this.boiteCollision.Min.X = position.X;
            this.boiteCollision.Min.Y = position.Y;
            this.boiteCollision.Max.X = position.X + image.Width;
            this.boiteCollision.Max.Y = position.Y + image.Height;

            this.sphereCollision.Center.X = position.X;
            this.sphereCollision.Center.Y = position.Y;
        }

        /// <summary>
        /// Allows the object to appear on the other side of the screen
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="minBox">The minimum box.</param>
        /// <param name="maxBox">The maximum box.</param>
        /// <param name="sphere">The sphere.</param>
        /// <param name="screenSize">Size of the screen.</param>
        /// <param name="imageSize">Size of the image.</param>
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
