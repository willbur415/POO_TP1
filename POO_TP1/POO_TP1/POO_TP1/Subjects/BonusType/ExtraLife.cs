using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1.Subjects.BonusType
{
    public class ExtraLife : Bonus
    {
        public ExtraLife(Texture2D image, Vector2 position)
            : base(image, position)
        {

        }

        public override void StartEffect()
        {
            PlayerShip.GetInstance().NumberOfLifes += 1;
            UI.GetInstance().NumberOfLife += 1;
        }

        public override void Update()
        {

        }
    }
}
