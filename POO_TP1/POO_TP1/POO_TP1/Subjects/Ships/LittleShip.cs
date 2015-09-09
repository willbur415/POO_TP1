using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    public class LittleShip : EnemyShip
    {
        public LittleShip(Texture2D image, Vector2 position, TypeShip type)
            : base(image, position, type)
        {
            
        }

        
    }
}
