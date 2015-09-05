using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Factory
    {
        private static ContentManager content;

        public Factory(ContentManager content)
        {
            Factory.content = content;
        }

        public static EnnemyShip createEnnemyShip(TypeShip typeShip)
        {
            if (typeShip == TypeShip.littleShip)
            {
                return new LittleShip(content.Load<Texture2D>("Graphics\\sprites\\LittleShip"), new Vector2(150, 150), TypeShip.littleShip);
            }
            else
            {
                return new BigShip(content.Load<Texture2D>("Graphics\\sprites\\BigShip"), new Vector2(150, 150), TypeShip.littleShip);
            }
        }

        public static Bonus createBonus(BonusType bonusType)
        {
            if (bonusType == BonusType.invincible)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(150, 150), BonusType.invincible);
            }
            if (bonusType == BonusType.doublePoints)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\doublePoints"), new Vector2(150, 150), BonusType.doublePoints);
            }
            if (bonusType == BonusType.extraLife)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\extraLife"), new Vector2(150, 150), BonusType.extraLife);
            }
            if (bonusType == BonusType.extraPoints)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\extraPoints"), new Vector2(150, 150), BonusType.extraPoints);
            }
            if (bonusType == BonusType.slowDown)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\slowDown"), new Vector2(150, 150), BonusType.slowDown);
            }
            return null;
        }
    }
}
