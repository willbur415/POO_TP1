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

        /// <summary>
        /// Creates the enemy ship.
        /// </summary>
        /// <param name="typeShip">The type ship.</param>
        /// <param name="screenSize">Size of the screen.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates the bonus.
        /// </summary>
        /// <returns></returns>
        public static Bonus createBonus()
        {
            Random rand = new Random();
            int bonusNumber = rand.Next(4);
            int posX = rand.Next(0, Game1.SCREENWIDTH);
            int posY = rand.Next(0, Game1.SCREENHEIGHT);

            
            switch (bonusNumber)
            {
                case 1:
                    return new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.invincible);
                case 2:
                    return new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.doublePoints);
                case 3:
                    return new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.extraLife);
                case 4:
                    return new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.extraPoints);
                case 5:
                    return new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.slowDown);
            }
            return new Bonus(Game1.contentManager.Load<Texture2D>("Graphics\\sprites\\Bonus\\invincible"), new Vector2(posX, posY), BonusType.none);
        }
    }
}
