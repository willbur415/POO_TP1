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
        private List<Asteroid> asteroids;
        private List<Asteroid> deadAsteroids;

        private static LevelManager levelManager;

        public static LevelManager GetInstance()
        {
            if (levelManager == null)
            {
                levelManager = new LevelManager();
            }
            return levelManager;
        }

        private LevelManager()
        {
            
        }

        public void Initialize()
        {
            asteroids = new List<Asteroid>();
            deadAsteroids = new List<Asteroid>();
            currentLevel = 1;
            ChangeNbAsteroids();
        }

        public List<Asteroid> Asteroids
        {
            get
            {
                return asteroids;
            }
        }

        public List<Asteroid> DeadAsteroids
        {
            get
            {
                return deadAsteroids;
            }
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
