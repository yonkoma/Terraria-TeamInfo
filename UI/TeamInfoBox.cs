using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace TeamInfo.UI
{
	internal class TeamInfoBox : UIState
	{
		public const int HealthBarWidth = 150;
		public const int HealthBarHeight = 20;
		public const int HealthBarSpacing = 5;

		private UIElement area;
		private Player[] teamMembers = new Player[0];
		private List<int> teamMemberIds = new List<int>();
		private TeamHealthBar[] teamHealthBars = new TeamHealthBar[0];

		public override void OnInitialize()
		{
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement(); 
			area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(HealthBarWidth, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(HealthBarHeight, 0f);

			// TeamHealthBar bar = new TeamHealthBar(60, 100, Main.teamColor[1]);
			// bar.Width.Set(HealthBarWidth, 0f);
			// bar.Height.Set(HealthBarHeight, 0f);
			// bar.Left.Set(0, 0f);
			// area.Append(bar);
			Append(area);
		}

		public override void Update(GameTime gameTime)
		{
			Player player = Main.LocalPlayer;
			Color teamColor = Main.teamColor[player.team];
			if(player.team == 0)
			{
				teamMembers = new Player[0];
				teamMemberIds = new List<int>();
				area.RemoveAllChildren();
			} else
			{
				List<int> newTeamMemberIds = new List<int>();
				for(int i = 0; i < Main.player.Length; i++)
				{
					if(Main.player[i].team == player.team)
					{
						newTeamMemberIds.Add(i);
					}
				}
				bool teamChanged = Enumerable.SequenceEqual(newTeamMemberIds, this.teamMemberIds);
				if(!teamChanged)
				{
					teamMemberIds = newTeamMemberIds;
					int teamCount = teamMemberIds.Count; 
					teamMembers = new Player[teamCount];
					teamHealthBars = new TeamHealthBar[teamCount];
					area.RemoveAllChildren();
					area.Height.Set(teamCount * HealthBarHeight + (teamCount - 1) * HealthBarSpacing, 0f);
					for(int i = 0; i < teamCount; i++)
					{
						teamMembers[i] = Main.player[i];
						teamHealthBars[i] = new TeamHealthBar(teamMembers[i].statLife, teamMembers[i].statLifeMax2, teamColor);
						teamHealthBars[i].Width.Set(HealthBarWidth, 0f);
						teamHealthBars[i].Height.Set(HealthBarHeight, 0f);
						teamHealthBars[i].Left.Set(0, 0f);
						teamHealthBars[i].Top.Set(i * (HealthBarHeight + HealthBarSpacing), 0f);
						area.Append(teamHealthBars[i]);
					}
				} else
				{
					for (int i = 0; i < teamMembers.Length; i++)
					{
						teamHealthBars[i].currentLife = teamMembers[i].statLife;
						teamHealthBars[i].maxLife = teamMembers[i].statLifeMax2;
						teamHealthBars[i].teamColor = teamColor;
					}
				}
			}
			// text.SetText($"{player.name}: {player.statLife} / {player.statLifeMax2} ({player.statManaMax})");
			base.Update(gameTime);
		}
	}
}
