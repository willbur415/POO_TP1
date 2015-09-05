using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using POO_TP1;

namespace POO_TP1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static ContentManager contentManager;
        private SpriteFont font;
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;
        private const float TIME_BETWEEN_SHOTS_SEC= 3;
        private bool paused = true;
        private bool pauseKeyDown;
        private int menuCounter;


        private Factory facto;
        private Texture2D spacefield;
        private EnnemyShip eShip;
        private Bonus bonus;
        public static List<Asteroid> Asteroids;
        public static List<Asteroid> DeadAsteroids;
        private Random rand;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO : ajouter la logique d’initialisation ici
            InitGraphicsMode(SCREENWIDTH, SCREENHEIGHT, false);
            base.Initialize();
            
        }

        private bool InitGraphicsMode(int width, int height, bool fullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (fullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    //if ((dm.Width == width) && (dm.Height == height))
                    //{
                    // The mode is supported, so set the buffer formats, apply changes and return
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                    //}
                }
            }
            return false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Kootenay");
            GameMenu.GetInstance().Initialize(font, ref graphics);
            UI.GetInstance().Initialize();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            facto = new Factory(Content);
            spacefield = Content.Load<Texture2D>("Graphics\\background\\stars");
            PlayerShip.GetInstance().Initialize(Content.Load<Texture2D>("Graphics\\sprites\\PlayerShip"), new Vector2(SCREENWIDTH / 4, SCREENHEIGHT / 2));
            PlayerShip.GetInstance().InitBullets(Content);
            eShip = Factory.createEnnemyShip(TypeShip.bigShip);
            bonus = Factory.createBonus(BonusType.slowDown);
            loadAsteroids();
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            menuCounter++;
            GamePadState padOneState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState(PlayerIndex.One);

            CheckPauseKey(padOneState, keyboardState);

            if (!paused)
            {
                if (PlayerShip.GetInstance().IsAlive)
                {
                    if (padOneState.IsConnected) CheckPadInputs(padOneState);
                    else CheckKeyboardKeys(keyboardState);
                }
                
                updateBullets();
                updateAsteroids();
                DeadAsteroids.Clear();
            }
            else
            {
                if (padOneState.DPad.Down == ButtonState.Pressed || padOneState.DPad.Up == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.Up))
                {
                    if (menuCounter > 10)
                    {
                        GameMenu.GetInstance().UpdateMenu(ref padOneState, ref keyboardState);
                        menuCounter = 0;
                    } 
                }
                if (padOneState.Buttons.A == ButtonState.Pressed)
                {
                    CheckGameMenuChoice();
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(spacefield, Vector2.Zero, Color.White);

            if (!paused)
            {
                
                spriteBatch.Draw(eShip.Image, eShip.Position, Color.White);

                drawAsteroids(spriteBatch);

                if (PlayerShip.GetInstance().Alive)
                {
                    drawPlayer(spriteBatch);
                }
                UI.GetInstance().draw(ref spriteBatch);
            }
            else
            {
                GameMenu.GetInstance().DrawMenu(ref spriteBatch);     
            }

            spriteBatch.End();
            base.Draw(gameTime);
            
        }

        private void CheckGameMenuChoice()
        {
            if (GameMenu.GetInstance().SelectedItemIndex == 0)
            {
                paused = false;
            }
            if (GameMenu.GetInstance().SelectedItemIndex == 1)
            {

            }
            if (GameMenu.GetInstance().SelectedItemIndex == 2)
            {
                Exit();
            }
        }

        //Code for game pausing taking in microsoft documentation
        private void BeginPause()
        {
            paused = true;

        }
        private void EndPause()
        {
            paused = false;
        }
        private void CheckPauseKey(GamePadState gamePadState, KeyboardState keyboardState)
        {
            bool pauseKeyDownThisFrame = (gamePadState.Buttons.Start == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Enter));
            // If key was not down before, but is down now, we toggle the
            // pause setting
            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (!paused)
                    BeginPause();
                else
                    EndPause();
            }
            pauseKeyDown = pauseKeyDownThisFrame;
        }

        private void CheckPadInputs(GamePadState padState)
        {
            PlayerShip.GetInstance().RotationAngle += padState.ThumbSticks.Right.X / 16.0f;
            PlayerShip.GetInstance().MoveShip(padState.ThumbSticks.Left.Y);
            if (padState.IsButtonDown(Buttons.RightTrigger)) playerShoot();
        }

        private void CheckKeyboardKeys(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.W))
            {
                PlayerShip.GetInstance().MoveShip(1.0f);
            }
            else
            {
                PlayerShip.GetInstance().MoveShip(0);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                PlayerShip.GetInstance().MoveShip(-1.0f);
            }
            else
            {
                PlayerShip.GetInstance().MoveShip(0);
            }
            if (keyboardState.IsKeyDown(Keys.A)) PlayerShip.GetInstance().RotationAngle -= 0.05f;
            if (keyboardState.IsKeyDown(Keys.D)) PlayerShip.GetInstance().RotationAngle += 0.05f;
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                playerShoot();
            }
        }

        private void loadAsteroids()
        {
            Asteroids = new List<Asteroid>();
            DeadAsteroids = new List<Asteroid>();
            rand = new Random();
            for (int i = 0; i < rand.Next(2, 6); i++)
            {
                Asteroid ast = new Asteroid(Content.Load<Texture2D>("Graphics\\sprites\\asteroid_big"), Vector2.Zero, i, AsteroidSize.large);
                Asteroids.Add(ast);
                ast.AddObserver(UI.GetInstance());
            }
        }

        private void playerShoot()
        {
            if (PlayerShip.GetInstance().Cooldown == 0)
            {
                PlayerShip.GetInstance().Shoot();
            }
        }

        private void updateAsteroids()
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                Asteroids[i].Move();
                PlayerShip.GetInstance().CheckCollisionBox(Asteroids[i]);
            }
        }

        private void updateBullets()
        {
            foreach (Bullet bullet in PlayerShip.GetInstance().Bullets)
            {
                bullet.Update();
                for (int i = 0; i < Asteroids.Count; i++)
                {
                    bullet.CheckCollisionBox(Asteroids[i]);
                }
                for (int i = 0; i < DeadAsteroids.Count; i++)
                {
                    if (Asteroids.Contains(DeadAsteroids[i]))
                    {
                        Asteroids.Remove(DeadAsteroids[i]);
                    }
                }
            }
        }

        private void drawAsteroids(SpriteBatch spriteBatch)
        {
            foreach (Asteroid ast in Asteroids)
            {
                spriteBatch.Draw(ast.Image, ast.Position, Color.White);
            }
        }

        private void drawPlayer(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in PlayerShip.GetInstance().Bullets)
            {
                spriteBatch.Draw(bullet.Image, bullet.Position, Color.White);
            }
            spriteBatch.Draw(PlayerShip.GetInstance().Image, PlayerShip.GetInstance().Position, null, Color.White, PlayerShip.GetInstance().RotationAngle, PlayerShip.GetInstance().Offset, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
