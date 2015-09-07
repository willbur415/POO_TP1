using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_TP1
{    
    public class LevelManager
    {
        private const int NB_MAX_ASTEROIDS = 12;
        private const int NB_ASTEROIDS_INCR = 3;
        private int nbAsteroids;
        private int currentLevel;

        public LevelManager()
        {
            currentLevel = 1;
            ChangeNbAsteroids();
        }

        public int NbAsteroids
        {
            get
            {
                return nbAsteroids;
            }
            set
            {
                nbAsteroids = value;
            }
        }

        public void ChangeLevel()
        {
            currentLevel++;
            ChangeNbAsteroids();
        }

        private void ChangeNbAsteroids()
        {
            nbAsteroids = currentLevel + NB_ASTEROIDS_INCR;
        }
    }
}
