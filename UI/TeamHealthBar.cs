using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace TeamInfo.UI
{
	internal class TeamHealthBar : UIElement
	{
		internal int currentLife;
		internal int maxLife;
		internal Color teamColor;

		public TeamHealthBar(int currentLife, int maxLife, Color teamColor)
		{
			this.currentLife = currentLife;
			this.maxLife = maxLife;
			this.teamColor = teamColor;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			float lifePercent = (float)this.currentLife / this.maxLife;
			lifePercent = Utils.Clamp(lifePercent, 0f, 1f);

			Rectangle dimensions = this.GetInnerDimensions().ToRectangle();
			int x = dimensions.X;
			int y = dimensions.Y;
			int width = dimensions.Width;
			int height = dimensions.Height;
			int left = x + 2;
			int right = x + width - 2;
			int steps = (int)((right - left) * lifePercent);

			spriteBatch.Draw(Main.magicPixel, new Rectangle(x, y, width, height), Color.Lerp(Color.Black, teamColor, 0.3f));
			Color healthColorStart = Color.Lerp(Color.Black, teamColor, 0.7f);
			for (int i = 0; i < steps; i += 1) {
				float percent = (float)i / (right - left);
				spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, y + 2, 1, height - 4), Color.Lerp(healthColorStart, teamColor, percent));
			}
		}
	}
}
