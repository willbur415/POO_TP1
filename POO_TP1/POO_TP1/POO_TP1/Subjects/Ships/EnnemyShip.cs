using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    public enum TypeShip {littleShip, bigShip, bigBossShip}

    public abstract class EnnemyShip : MovableObject
    {
        protected TypeShip type;

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

        public EnnemyShip(Texture2D image, Vector2 position, TypeShip type)
            : base(image, position)
        {
            Type = type;
        }

        public override void CheckCollisionBox(Objet2D theOther)
        {

        }

        public new void Move()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
        }
    }
}
