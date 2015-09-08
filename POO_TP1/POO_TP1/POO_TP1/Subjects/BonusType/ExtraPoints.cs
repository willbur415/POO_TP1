using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1.Subjects.BonusType
{
    public class ExtraPoints : Bonus
    {
        public ExtraPoints(Texture2D image, Vector2 position)
            : base(image, position)
        {
            
        }

        public override void StartEffect()
        {
            UI.GetInstance().Score += 300;
        }

        public override void Update()
        {

        }
    }
}
