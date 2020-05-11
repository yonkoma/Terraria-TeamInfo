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
			int steps = (int)((healthRight - healthLeft) * lifePercent);

			spriteBatch.Draw(Main.magicPixel, new Rectangle(x, y, width, height), Color.Lerp(Color.Black, teamColor, 0.3f));
			Color healthColorStart = Color.Lerp(Color.Black, teamColor, 0.7f);
			for (int i = 0; i < steps; i += 1)
			{
				float percent = (float)i / (healthRight - healthLeft);
				spriteBatch.Draw(Main.magicPixel, new Rectangle(healthLeft + i, y + 2, 1, height - 4), Color.Lerp(healthColorStart, teamColor, percent));
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
