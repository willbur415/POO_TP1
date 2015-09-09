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
using POO_TP1.Subjects.Ships;


namespace POO_TP1
{
    public class Factory
    {
        public Factory()
        {
        }

        public static EnemyShip CreateEnemyShip(TypeShip typeShip,Vector2 screenSize)
        {
            Random r = new Random();
            int random = r.Next(0, (int)screenSize.Y);
            int startPosInX = -100;

            if (typeShip == TypeShip.littleShip)
            {
                return new LittleShip(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\LittleShip"), new Vector2(startPosInX,random), TypeShip.littleShip);
            }
            if (typeShip == TypeShip.bigShip)
            {
                return new BigShip(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\BigShip"), new Vector2(startPosInX, random), TypeShip.littleShip);
            }

            return new BigBossShip(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\BigBossShip"), new Vector2(startPosInX, random), TypeShip.bigBossShip);
        }

        public static Bonus createBonus()
        {
            Random rand = new Random();
            int bonusNumber = rand.Next(5);
            int posX = rand.Next(0, Game1.SCREENWIDTH);
            int posY = rand.Next(0, Game1.SCREENHEIGHT);

            Bonus bonus = null;
            switch (bonusNumber)
            {
                case 1:
                    bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.invincible);
                    break;
                case 2:
                    bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.doublePoints);
                    break;
                case 3:
                    bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.extraLife);
                    break;
                case 4:
                    bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.extraPoints);
                    break;
                case 5:
                    bonus = new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.slowDown);
                    break;                    
            }
            
            return bonus;
        }
    }
}
