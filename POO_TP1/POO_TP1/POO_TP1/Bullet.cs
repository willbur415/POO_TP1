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
        private Boolean isShooted;

        public Bullet(Texture2D image, Vector2 position)
            : base(image, position)
        {
            FillObject2DInfo();
        }

        public Bullet(Texture2D image)
        {
            this.position = new Vector2(600, 600);
            this.image = image;
            this.velocity = new Vector2(0, 0);
            FillObject2DInfo();
        }

        public void update()
        {
            this.velocity.X *= 1.1f;
            this.velocity.Y *= 1.1f;
            this.position += this.velocity;
        }

        public Boolean IsShooted
        {
            get
            {
                return isShooted;
            }
            set
            {
                isShooted = value;
            }
        }
    }
}
