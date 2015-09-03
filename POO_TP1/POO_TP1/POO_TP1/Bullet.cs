using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1
{
    public class Bullet : Objet2D
    {
        public Bullet(Texture2D image, Vector2 position)
            : base(image, position)
        {

        }
    }
}
