using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace TeamInfo
{
	public class Config : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Label("Show the team info box")]
		[DefaultValue(true)]
		public bool ShowTeamInfo;

		public override void OnChanged()
		{
			UI.TeamInfoBox.Visible = ShowTeamInfo;
		}
	}
}
