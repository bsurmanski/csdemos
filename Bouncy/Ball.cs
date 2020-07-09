using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncy
{
	class Ball {
		public Vector2 position;
		public Vector2 velocity;
		public Color color;
		public static float DIAMETER = 32;
		public Ball(Vector2 p, Vector2 v) {
			position = p;
			velocity = v;
			color = Color.White;
		}
		
		public void Update(Rectangle bounds) {
			position += velocity;
			
			if (position.X < 0) velocity.X = Math.Abs(velocity.X);
			if (position.Y < 0) velocity.Y = Math.Abs(velocity.Y);
			if (position.X > bounds.Width - DIAMETER) velocity.X = -Math.Abs(velocity.X);
			if (position.Y > bounds.Height - DIAMETER) velocity.Y = -Math.Abs(velocity.Y);
			
			Vector2 gravity_point = new Vector2(200, 200);
			Vector2 gravity_vector =  gravity_point - position;
			gravity_vector.Normalize();
			velocity += gravity_vector * 0.25f;
		}
		
		public Vector2 GetCenter() {
			return new Vector2(position.X + DIAMETER / 2.0f, 
							   position.Y + DIAMETER / 2.0f);
		}
		
		public void CycleColor() {
			if (color == Color.White) {
				color = Color.Blue;
			} else if (color == Color.Blue) {
				color = Color.Red;
			} else if (color == Color.Red) {
				color = Color.White;
			}
		}
		
		public bool Collides(Ball o) {
			Vector2 c1 = GetCenter();
			Vector2 c2 = o.GetCenter();
			
			bool ret = Vector2.Distance(c1, c2) <= DIAMETER;
			if (ret) CycleColor();
			return ret;
		}
	}
}