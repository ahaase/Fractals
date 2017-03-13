using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Numerics;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pixelTexture;

        Complex C = new Complex();
        Complex Z = new Complex();


        Color[,] pixels;

        double xmin, xmax;
        double ymin, ymax;

        int iterations;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            xmin = ymin = -2;
            xmax = ymax = 2;
            xmin = -2.5f;
            xmax = 1.5f;


            C = new Complex(xmin, ymin);
            Z = new Complex(xmin, ymin);

            iterations = 50;

            pixelTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            pixels = new Color[graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight];

            Color[] data = new Color[1]; 
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            pixelTexture.SetData(data);

            base.Initialize();
        }

        Complex calculate(Complex c, Complex z)
        {
            return Complex.Add(c, Complex.Multiply(z, z));
        }

        Color DoesDiverge(double xValue, double yValue)
        {
            Complex c = new Complex(xValue, yValue);
            Complex z = new Complex(xValue, yValue);
            int runs = 0;
            for(int i = 0; i < iterations; i++)
            {
                z = calculate(c, z);

                if (Complex.Abs(z) > 2)
                {
                    return new Color(runs*20, runs * 10, runs*5);
                }
                runs = i;
            }

            return Color.Black; // färgvärde
        }
        void Scan()
        {
            for (int y = 0; y < graphics.PreferredBackBufferHeight; y++)
            {
                for (int x = 0; x < graphics.PreferredBackBufferWidth; x++)
                {
                    double xValue = x * (xmax - xmin) / graphics.PreferredBackBufferWidth + xmin;
                    double yValue = y * (ymax - ymin) / graphics.PreferredBackBufferHeight + ymin;
                    pixels[x, y] = DoesDiverge(xValue, yValue);
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Scan();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            for (int y = 0; y < graphics.PreferredBackBufferHeight; y++) {
                for(int x = 0; x < graphics.PreferredBackBufferWidth; x++)
                {
                    spriteBatch.Draw(pixelTexture, new Vector2(x, y), pixels[x, y]);
                }
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
