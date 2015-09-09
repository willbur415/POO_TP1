using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace POO_TP1
{
    public class Objet2D : ObservedSubject
    {
        protected Texture2D image;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 posCenter;
        protected Vector2 offset;

        protected BoundingBox boiteCollision;
        protected BoundingSphere sphereCollision;
        protected float rotationAngle;

        public Objet2D(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
            FillObject2DInfo();
        }

        protected Objet2D()
        {
        }

        protected void FillObject2DInfo()
        {
            this.offset = new Vector2(image.Width / 2, image.Height / 2);
            this.posCenter = new Vector2(position.X + offset.X, position.Y + offset.Y);
            setBoundings();
        }

        public Texture2D Image
        {
            get
            {
                return image;
            }
        }
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public Vector2 PosCenter
        {
            get
            {
                return posCenter;
            }
            set
            {
                posCenter = value;
            }
        }
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
        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }
        public Rectangle Boundings
        {
            get
            {
                return new Rectangle((int)this.position.X, (int)this.position.Y, this.image.Width, this.image.Height);
            }
        }
        public BoundingBox BoiteCollision
        {
            get
            {
                return boiteCollision;
            }
        }
        public BoundingSphere SphereCollision
        {
            get
            {
                return sphereCollision;
            }
        }

        public float RotationAngle
        {
            get
            {
                return rotationAngle;
            }
            set
            {
                rotationAngle = value;
            }
        }
        /// <summary>
        /// Sets the boundings of the object for the collision.
        /// </summary>
        private void setBoundings()
        {
            this.boiteCollision.Min.X = this.position.X;
            this.boiteCollision.Min.Y = this.position.Y;
            this.boiteCollision.Max.X = this.position.X + image.Width;
            this.boiteCollision.Max.Y = this.position.Y + image.Height;

            //this.sphereCollision = new BoundingSphere(new Vector3(position.X - offset.X, position.Y - offset.Y, 0), offset.X);
        }
    }
}
