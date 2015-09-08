using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1.Subjects.BonusType
{
    public class Invincible : Bonus
    {
        public Invincible(Texture2D image, Vector2 position)
            : base(image, position)
        {
            
        }

        public override void StartEffect()
        {
            PlayerShip.GetInstance().IsInvincible = true;
        }

        public override void Update()
        {
            if (bonusTime > 0)
            {
                bonusTime--;
            }
            else
            {
                PlayerShip.GetInstance().IsInvincible = false;
            }
        }
    }
}
