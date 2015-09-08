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

        public Factory()
        {
        }

        public static EnnemyShip createEnnemyShip(TypeShip typeShip)
        {
            Random r = new Random();
            int random = r.Next(0, 500);
            if (typeShip == TypeShip.littleShip)
            {
                return new LittleShip(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\LittleShip"), new Vector2(-50,random), TypeShip.littleShip);
            }
            else
            {
                return new BigShip(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\BigShip"), new Vector2(-50, random), TypeShip.littleShip);
            }
        }

        public static Bonus createBonus(BonusType bonusType)
        {
            Bonus bonus;
            if (bonusType == BonusType.invincible)
            {
                bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(150, 150), BonusType.invincible);
            }
            else if (bonusType == BonusType.doublePoints)
            {
                bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\doublePoints"), new Vector2(150, 150), BonusType.doublePoints);
            }
            else if (bonusType == BonusType.extraLife)
            {
                bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\extraLife"), new Vector2(150, 150), BonusType.extraLife);
            }
            else if (bonusType == BonusType.extraPoints)
            {
                bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\extraPoints"), new Vector2(150, 150), BonusType.extraPoints);
            }
            else
            {
                bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\slowDown"), new Vector2(150, 150), BonusType.slowDown);
            }
            return bonus;
        }
    }
}
