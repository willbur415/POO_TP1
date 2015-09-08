using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    public enum TypeShip {littleShip, bigShip, bigBossShip}

    public abstract class EnemyShip : MovableObject
    {
        protected TypeShip type;
        private const double ENEMY_SPEED = 0.7;

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
            AddObserver(UI.GetInstance());
        }

        public override void CheckCollisionBox(Objet2D theOther)
        {

        }

        public new void Move()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
            UpdateCollision();
        }
    }
}
