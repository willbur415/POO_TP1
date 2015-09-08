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
using POO_TP1.Subjects.BonusType;


namespace POO_TP1
{
    class Factory
    {

        public Factory()
        {
        }

        public static EnnemyShip createEnnemyShip(TypeShip typeShip)
        {
            if (typeShip == TypeShip.littleShip)
            {
                return new LittleShip(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\LittleShip"), new Vector2(150, 150), TypeShip.littleShip);
            }
            else
            {
                return new BigShip(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\BigShip"), new Vector2(150, 150), TypeShip.littleShip);
            }
        }

        public static Bonus createBonus(BonusType bonusType)
        {
            Bonus bonus;
            if (bonusType == BonusType.invincible)
            {
                bonus = new Invincible(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(150, 150));
            }
            else if (bonusType == BonusType.doublePoints)
            {
                bonus = new DoublePoints(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\doublePoints"), new Vector2(150, 150));
            }
            else if (bonusType == BonusType.extraLife)
            {
                bonus = new ExtraLife(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\extraLife"), new Vector2(150, 150));
            }
            else if (bonusType == BonusType.extraPoints)
            {
                bonus = new ExtraPoints(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\extraPoints"), new Vector2(150, 150));
            }
            else
            {
                bonus = new SlowDown(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\slowDown"), new Vector2(150, 150));
            }
            return bonus;
        }
    }
}
