using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using POO_TP1;

namespace POO_TP1
{

    public enum BonusType
    {
        invincible, extraLife, doublePoints, slowDown, extraPoints
    }

    public class Bonus : ObservedSubject
    {
        private Texture2D image;
        private Vector2 position;
        private Vector2 posCenter;
        private Vector2 offset;
        private BonusType type;
        private BoundingBox boiteCollision;
        private BoundingSphere sphereCollision;

        public BonusType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Texture2D Image
        {
            get { return image; }
        }

        public Vector2 Position
        {
            get { return position;}
        }

        public Bonus(Texture2D image, Vector2 position, BonusType type)
        {
            this.image = image;
            this.position = position;
            this.type = type;
            offset = new Vector2(image.Width / 2, image.Height / 2);
            posCenter = new Vector2(position.X + offset.X, position.Y + offset.Y);
            boiteCollision = new BoundingBox(new Vector3(position.X - offset.X, position.Y - offset.Y, 0), new Vector3(position.X + offset.X, position.Y + +offset.Y, 0));
            sphereCollision = new BoundingSphere(new Vector3(position.X, position.Y, 0), offset.X);
        }
    }
}
