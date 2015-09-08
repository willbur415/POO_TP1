using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POO_TP1.Subjects.BonusType
{
    public class DoublePoints : Bonus
    {
        private const int SCORE_MULTIPLIER = 2;
        private const int BONUS_DURATION = 150;

        public DoublePoints(Texture2D image, Vector2 position)
            : base(image, position)
        {
            
        }

        public override void StartEffect()
        {
            UI.GetInstance().ScoreMultiplier = SCORE_MULTIPLIER;
            base.bonusTime = BONUS_DURATION;
        }

        public override void Update()
        {
            if (bonusTime > 0)
            {
                bonusTime--;
            }
            else
            {
                this.NotifyAllObservers();
            }
        }
    }
}
