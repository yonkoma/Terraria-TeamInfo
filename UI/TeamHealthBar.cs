using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace TeamInfo.UI
{
	internal class TeamHealthBar : UIElement
	{
		private const int TextHeight = 15;

		internal Player player;
		internal UIText nameText;
		internal UIText healthText;

		public TeamHealthBar(Player player)
		{
			this.player = player;

			nameText = new UIText("Player", 0.6f);
			nameText.Top.Set(0, 0f);
			nameText.HAlign = 0f;

			healthText = new UIText("100/100", 0.6f);
			healthText.Top.Set(0, 0f);
			healthText.HAlign = 1f;

			this.Append(nameText);
			this.Append(healthText);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			int currentLife = this.player.statLife;
			int maxLife = this.player.statLifeMax2;
			Color teamColor = Main.teamColor[this.player.team];

			float lifePercent = (float)currentLife / maxLife;
			lifePercent = Utils.Clamp(lifePercent, 0f, 1f);

			Rectangle dimensions = this.GetInnerDimensions().ToRectangle();
			int x = dimensions.X;
			int y = dimensions.Y + TextHeight;
			int width = dimensions.Width;
			int height = dimensions.Height - TextHeight;
			int healthLeft = x + 2;
			int healthRight = x + width - 2;

			spriteBatch.Draw(Main.magicPixel, new Rectangle(x + 1, y, width - 2, height), Color.Lerp(Color.Black, teamColor, 0.6f));
			spriteBatch.Draw(Main.magicPixel, new Rectangle(x, y + 1, width, height - 2), Color.Lerp(Color.Black, teamColor, 0.6f));
			Color healthColorFullLeft = Color.Lerp(Color.White, teamColor, 1f);
			Color healthColorFullRight = Color.Lerp(Color.White, teamColor, 0.7f);
			Color healthColorEmptyLeft = Color.Lerp(Color.Black, teamColor, 0.2f);
			Color healthColorEmptyRight = Color.Lerp(Color.Black, teamColor, 0.3f);
			int innerWidth = healthRight - healthLeft;
			for (int i = 0; i < innerWidth; i += 1)
			{
				float percent = (float)(i + 1) / innerWidth;
				Color color;
				if(percent <= lifePercent)
				{
					color = Color.Lerp(healthColorFullLeft, healthColorFullRight, percent);
				} else
				{
					color = Color.Lerp(healthColorEmptyLeft, healthColorEmptyRight, percent);
				}
				int roundEdgeAdjust = 0;
				if(i == 0 || i == innerWidth - 1)
				{
					roundEdgeAdjust = 1;
				}
				Rectangle healthColumn = new Rectangle(healthLeft + i, y + 2 + roundEdgeAdjust, 1, height - 4 - 2 * roundEdgeAdjust);
				spriteBatch.Draw(Main.magicPixel, healthColumn, color);
			}
			base.DrawSelf(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			nameText.SetText($"{player.name}:");
			healthText.SetText($"{player.statLife}/{player.statLifeMax2}");
			base.Update(gameTime);
		}
	}
}
