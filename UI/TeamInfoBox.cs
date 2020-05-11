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
		public const int HealthBarWidth = 160;
		public const int HealthBarHeight = 40;
		public const int HealthBarSpacing = 10;

		private UIElement area;
		private Player[] teamMembers = new Player[0];
		private List<int> teamMemberIds = new List<int>();
		private TeamHealthBar[] teamHealthBars = new TeamHealthBar[0];

		public override void OnInitialize()
		{
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement(); 
			area.Left.Set(-320 - HealthBarWidth, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(100, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(HealthBarWidth, 0f);
			area.Height.Set(HealthBarHeight, 0f);

			this.Append(area);
		}

		public override void Update(GameTime gameTime)
		{
			Player player = Main.LocalPlayer;
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
						teamHealthBars[i] = new TeamHealthBar(teamMembers[i]);
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
						teamHealthBars[i].player = teamMembers[i];
					}
				}
			}
			base.Update(gameTime);
		}
	}
}
