using System.Collections.Generic;
using TeamInfo.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace TeamInfo
{
	public class TeamInfo : Mod
	{
		internal TeamInfoBox InfoBox;
		private UserInterface _infoBoxUI;

		public TeamInfo()
		{
		}

		public override void Load()
		{
			if (!Main.dedServ)
			{
				InfoBox = new TeamInfoBox();
				InfoBox.Activate();
				_infoBoxUI = new UserInterface();
				_infoBoxUI.SetState(InfoBox);
			}
		}

		public override void UpdateUI(GameTime gameTime)
		{
			_infoBoxUI?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1)
			{
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"TeamInfo: Team Infobox",
					delegate
					{
						_infoBoxUI.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}