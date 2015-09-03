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
        private const int BULLET_SPAWN_POS = -100;

        public Bullet(Texture2D image, Vector2 position)
            : base(image, position)
        {
            FillObject2DInfo();
        }

        public Bullet(Texture2D image)
        {
            this.position = new Vector2(BULLET_SPAWN_POS, BULLET_SPAWN_POS);
            this.image = image;
            this.velocity = new Vector2(0, 0);
            FillObject2DInfo();
        }

        public void Update()
        {
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
