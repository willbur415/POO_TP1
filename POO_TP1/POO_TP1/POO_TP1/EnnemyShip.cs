using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    public abstract class EnnemyShip : Objet2D
    {
        public EnnemyShip(Texture2D image, Vector2 position)
            : base(image, position)
        {
            
        }
    }
}
