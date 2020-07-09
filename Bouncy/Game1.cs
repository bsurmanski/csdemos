using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Bouncy
{
    public class Game1 : Game
    {
        protected GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
		
		List<Ball> balls;
        Texture2D image;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
			balls = new List<Ball>();
			balls.Add(new Ball(new Vector2(0, 200), new Vector2(2.5f,1)));
			balls.Add(new Ball(new Vector2(400, 400), new Vector2()));
			balls.Add(new Ball(new Vector2(70, 300), new Vector2(1.0f,1)));
			balls.Add(new Ball(new Vector2(100, 400), new Vector2(5.0f, 2.0f)));
			balls.Add(new Ball(new Vector2(300, 200), new Vector2(0.0f, 0.0f)));
			balls.Add(new Ball(new Vector2(600, 100), new Vector2(0.0f, 0.0f)));
			balls.Add(new Ball(new Vector2(500, 200), new Vector2(0.0f, 0.0f)));
			balls.Add(new Ball(new Vector2(500, 300), new Vector2(0.0f, 0.0f)));
			balls.Add(new Ball(new Vector2(500, 400), new Vector2(0.0f, 0.0f)));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            image = Content.Load<Texture2D>("ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
			
			for(int i = 0; i < balls.Count; i++) {
				for (int j = i + 1; j < balls.Count; j++) {
					if (balls[i].Collides(balls[j])) {
						Vector2 normal = balls[i].GetCenter() - balls[j].GetCenter();
						normal.Normalize();
						
						// Move balls so they no longer overlap
						float overlap = Ball.DIAMETER - Vector2.Distance(balls[i].GetCenter(), balls[j].GetCenter());				
						balls[i].position += overlap * normal;
						balls[j].position -= overlap * normal;

						// relative velocity along the normal vector.
						Vector2 relative_v = Vector2.Dot(balls[i].velocity - balls[j].velocity, normal) * normal;
						balls[i].velocity -= relative_v * 0.98f;
						balls[j].velocity += relative_v * 0.98f;
					}
				}
			}

            foreach (Ball b in balls) {
				b.Update(_graphics.GraphicsDevice.Viewport.Bounds);
			}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
			foreach (Ball b in balls) {
				_spriteBatch.Draw(image, b.position, b.color);
			}
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
