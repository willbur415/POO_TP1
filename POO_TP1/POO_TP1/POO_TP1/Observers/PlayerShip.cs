﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using POO_TP1;


namespace POO_TP1
{
    class PlayerShip : MovableObject, Observer
    {
        private static PlayerShip ship;
        private Bullet[] bullets;
        private Bonus currentBonus;

        private const double MAXSPEED = 5.0;
        private const float SLOWFACTOR = 20.0f;
        private const int MAX_BULLETS = 5;
        private const int COOLDOWN_TIME = 20;
        private const int RESPAWN_TIME = 70;
        private const int BULLET_SPAWN_POS = -100;
        private bool alive;
        private bool invincible;
        private int numberOfLifes = 3;
        private int playerTotalLife = 3;
        
        private bool firstShot = false;
        private int shotCooldown;
        private int respawnTime;

        public const int TIME_BETWEEN_SHOTS_MILI = 3000;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static PlayerShip GetInstance()
        {
            if (ship == null)
            {
                ship = new PlayerShip();
            }
            return ship;
        }
        /// <summary>
        /// Prevents a default instance of the <see cref="PlayerShip"/> class from being created.
        /// </summary>
        private PlayerShip()     
        {
            alive = true;
        }

        /// <summary>
        /// Initializes the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="position">The position.</param>
        public void Initialize(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
            FillObject2DInfo();
            
            this.currentBonus = new Bonus(image, Vector2.Zero, BonusType.none);
            bullets = new Bullet[MAX_BULLETS];
        }

        /// <summary>
        /// Resets the position.
        /// </summary>
        public void ResetPosition()
        {
            base.position.X = Game1.SCREENWIDTH / 2;
            base.position.Y = Game1.SCREENHEIGHT / 2;
            base.velocity = Vector2.Zero;
        }

        /// <summary>
        /// Gets or sets the bullets.
        /// </summary>
        /// <value>
        /// The bullets.
        /// </value>
        public Bullet[] Bullets
        {
            get
            {
                return bullets;
            }
            set
            {
                bullets = value;
            }
        }

        /// <summary>
        /// Gets or sets the current bonus.
        /// </summary>
        /// <value>
        /// The current bonus.
        /// </value>
        public Bonus CurrentBonus
        {
            get
            {
                return currentBonus;
            }
            set
            {
                currentBonus = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is alive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is alive; otherwise, <c>false</c>.
        /// </value>
        public bool IsAlive
        {
            get
            {
                return alive;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [first shot].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [first shot]; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvincible
        {
            get
            {
                return invincible;
            }
            set
            {
                invincible = value;
            }
        }

        public bool FirstShot
        {
            get
            {
                return firstShot;
            }
            set
            {
                firstShot = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="PlayerShip"/> is alive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if alive; otherwise, <c>false</c>.
        /// </value>
        public bool Alive
        {
            get
            {
                return alive;
            }
        }



        /// <summary>
        /// Gets or sets the cooldown.
        /// </summary>
        /// <value>
        /// The cooldown.
        /// </value>
        public int Cooldown
        {
            get
            {
                return shotCooldown;
            }
            set
            {
                shotCooldown = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of lifes.
        /// </summary>
        /// <value>
        /// The number of lifes.
        /// </value>
        public int NumberOfLifes
        {
            get
            {
                return numberOfLifes;
            }
            set 
            {
                numberOfLifes = value;
            }
        }

        /// <summary>
        /// Gets or sets the player total life.
        /// </summary>
        /// <value>
        /// The player total life.
        /// </value>
        public int PlayerTotalLife
        {
            get 
            {
                return playerTotalLife; 
            }
            set 
            {
                playerTotalLife = value; 
            }
        }

        /// <summary>
        /// Adds the life.
        /// </summary>
        public void AddLife()
        {
            this.numberOfLifes++;
            this.NotifyAllObservers();
        }

        /// <summary>
        /// Checks the collision sphere.
        /// </summary>
        /// <param name="theOther">The other.</param>
        public void CheckCollisionSphere(Objet2D theOther)
        {
            if (sphereCollision.Intersects(theOther.SphereCollision))
            {
                alive = false;
            }
        }

        /// <summary>
        /// Checks the collision box.
        /// </summary>
        /// <param name="theOther">The other.</param>
        public override void CheckCollisionBox(Objet2D theOther)
        {
            if ((IsAsteroid(theOther) && currentBonus.Type != BonusType.invincible) || (IsEnemyShip(theOther) && currentBonus.Type != BonusType.invincible) || (IsEnemyBullet(theOther) && currentBonus.Type != BonusType.invincible))
            {
                if (boiteCollision.Intersects(theOther.BoiteCollision) && alive)
                {
                    playerDead();
                    ship.NotifyAllObservers();
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(ref SpriteBatch spriteBatch)
        {
            Vector2 drawPos = this.position + this.offset;

            if (this.currentBonus.Type != BonusType.invincible)
            {
                spriteBatch.Draw(this.image, drawPos, null, Color.White, this.rotationAngle, this.offset, 1.0f, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.Draw(this.image, drawPos, null, Color.Green, this.rotationAngle, this.offset, 1.0f, SpriteEffects.None, 1);
            }
            

            //if (PlayerShip.GetInstance().CurrentBonus.Type != BonusType.invincible)
            //{
            //    PlayerShip.GetInstance().Draw(ref spriteBatch);
            //}
            //else
            //{
            //    spriteBatch.Draw(PlayerShip.GetInstance().Image, PlayerShip.GetInstance().Position, null, Color.Green, PlayerShip.GetInstance().RotationAngle, PlayerShip.GetInstance().Offset, 1.0f, SpriteEffects.None, 1);
            //}


        }

        /// <summary>
        /// Updates the specified new thrust.
        /// </summary>
        /// <param name="newThrust">The new thrust.</param>
        public void Update(float newThrust)
        {
            if (shotCooldown > 0)
            {
                shotCooldown--;
            }
            
            currentBonus.Update();

            //Rappel, le thrust arrière doit être plus lent
            if (newThrust < 0)
                newThrust /= 2;

            //Angle 0 est un vecteur qui pointe vers le haut, et on augmente l'angle dans le sens des aiguilles d'une montre

            velocity.X += (float)(Math.Sin((double)rotationAngle) * newThrust) / SLOWFACTOR;
            velocity.Y -= (float)(Math.Cos((double)rotationAngle) * newThrust) / SLOWFACTOR;

            //Dans bien des vieux jeux la vitesse maximum semble être par axe, mais comme on a de la puissance de calcul, on va la faire totale.
            MaxThrust();

            //Il faut aussi déplacer les poly de collision
            Move();
            bulletsFollow();
        }

        /// <summary>
        /// Checks the respawn time.
        /// </summary>
        public void CheckRespawnTime()
        {
            if (respawnTime > 0)
            {
                respawnTime--;
            }
            else
            {
                respawn();
            }
        }

        /// <summary>
        /// Players the dead.
        /// </summary>
        private void playerDead()
        {
            alive = false;
            numberOfLifes--;
            respawnTime = RESPAWN_TIME;
        }

        /// <summary>
        /// Respawns this instance.
        /// </summary>
        private void respawn()
        {
            if (currentBonus.Type != BonusType.none)
            {
                currentBonus.StopBonus();
            }
            currentBonus.Type = BonusType.invincible;
            currentBonus.BonusTime = 300;
            this.alive = true;
            this.velocity = Vector2.Zero;
            this.position.X = Game1.SCREENWIDTH / 2;
            this.position.Y = Game1.SCREENHEIGHT / 2;
        }

        /// <summary>
        /// Bulletses the follow.
        /// </summary>
        private void bulletsFollow()
        {
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.IsShooted)
                    bullet.Position = this.position;
            }
        }

        /// <summary>
        /// Maximums the thrust.
        /// </summary>
        private void MaxThrust()
        {
            //Calcul de la vitesse maximum actuelle (pythagore)
            double totalSpeed = Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));

            //Si plus grande que la vitesse actuelle, on se fait un ratio
            if (totalSpeed > MAXSPEED)
            {
                float ratio = (float)(MAXSPEED / totalSpeed);
                velocity.X *= ratio;
                velocity.Y *= ratio;
            }
        }

        /// <summary>
        /// Shoots this instance.
        /// </summary>
        /// <returns></returns>
        public bool Shoot()
        {
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.IsShooted)
                {
                    bullet.RotationAngle = this.rotationAngle;
                    bullet.Velocity = new Vector2((float)Math.Sin((double)rotationAngle) * 10, -(float)Math.Cos((double)rotationAngle) * 10);
                    bullet.IsShooted = true;
                    shotCooldown = COOLDOWN_TIME;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Initializes the bullets.
        /// </summary>
        /// <param name="content">The content.</param>
        public void InitBullets(ContentManager content)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Bullet(content.Load<Texture2D>("Graphics\\sprites\\Bullet"), new Vector2(BULLET_SPAWN_POS, BULLET_SPAWN_POS));
            }
        }

        /// <summary>
        /// Notifies the specified subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        public void Notify(ObservedSubject subject)
        {
            if (subject is UI)
            {
                if ((subject as UI).NumberOfLife != this.numberOfLifes)
                {
                    this.numberOfLifes = (subject as UI).NumberOfLife;
                }
            }
            else if (subject is Bullet)
            {
                numberOfLifes -= 1;
                this.NotifyAllObservers();
            }
            else if (subject is Bonus)
            {
                if ((subject as Bonus).Type != BonusType.none)
                {
                    if ((subject as Bonus).Type == BonusType.invincible)
                    {
                        if ((subject as Bonus).BonusTime > 0)
                        {
                            this.invincible = true;
                        }
                        else
                        {
                            this.invincible = false;
                        }
                    }
                    Game1.bonusList.Remove(subject as Bonus);
                    currentBonus = (subject as Bonus);
                }
            }
        }
    }
}
