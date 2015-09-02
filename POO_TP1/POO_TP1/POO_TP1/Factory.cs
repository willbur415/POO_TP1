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
    }
}
