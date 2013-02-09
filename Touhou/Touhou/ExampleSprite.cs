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

namespace Touhou.ExampleSprite
{
    public class ExampleSprite : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public ExampleSprite()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            
        }

        Texture2D myTexture;

        Vector2 spritePosition = Vector2.Zero;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myTexture = Content.Load<Texture2D>("reimu");
        }

        protected override void UnloadContent()
        {

        }

        KeyboardState keystate;
        Vector2 spriteSpeed;

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            keystate = Keyboard.GetState();
            spriteSpeed = Vector2.Zero;
            if (keystate.IsKeyDown(Keys.Left)) {spriteSpeed.X = -100;}
            if (keystate.IsKeyDown(Keys.Right)) {spriteSpeed.X = 100;}
            if (keystate.IsKeyDown(Keys.Up)) {spriteSpeed.Y = -100;}
            if (keystate.IsKeyDown(Keys.Down)) {spriteSpeed.Y = 100;}

            this.UpdateSprite(gameTime);

            base.Update(gameTime);
        }

        void UpdateSprite(GameTime gameTime)
        {
            // Move the sprite by speed, scaled by elapsed time.
            spritePosition +=
                spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int MaxX = graphics.GraphicsDevice.Viewport.Width - myTexture.Width;
            int MinX = 0;
            int MaxY = graphics.GraphicsDevice.Viewport.Height - myTexture.Height;
            int MinY = 0;

            // Check for edges.
            if (spritePosition.X > MaxX) {spritePosition.X = MaxX;}
            else if (spritePosition.X < MinX) {spritePosition.X = MinX;}
            if (spritePosition.Y > MaxY) {spritePosition.Y = MaxY;}
            else if (spritePosition.Y < MinY) {spritePosition.Y = MinY;}
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // Draw the sprite.
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(myTexture, spritePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}