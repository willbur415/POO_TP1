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
            content.RootDirectory = "Content";
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

        public static Bonus createBonus(Bonus.BonusType bonusType)
        {
            if (bonusType == Bonus.BonusType.invincible)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(150, 150), Bonus.BonusType.invincible);
            }
            if (bonusType == Bonus.BonusType.doublePoints)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\doublePoints"), new Vector2(150, 150), Bonus.BonusType.doublePoints);
            }
            if (bonusType == Bonus.BonusType.extraLife)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\extraLife"), new Vector2(150, 150), Bonus.BonusType.extraLife);
            }
            if (bonusType == Bonus.BonusType.extraPoints)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\extraPoints"), new Vector2(150, 150), Bonus.BonusType.extraPoints);
            }
            if (bonusType == Bonus.BonusType.slowDown)
            {
                return new Bonus(content.Load<Texture2D>("Graphics\\sprites\\Bonus\\slowDown"), new Vector2(150, 150), Bonus.BonusType.slowDown);
            }

            return null;
        }
    }
}
