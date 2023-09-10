using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Config;

namespace AmmunitionWorkshop
{
	//public class AMWServerConfig : ModConfig
	//{
	//	public override ConfigScope Mode => ConfigScope.ClientSide;

	//	[Header("Placement and visibility")]
	//	[Label("Hide UI if disease is not present?")]
	//	[DefaultValue(true)]
	//	public bool hideZero;
	//	[Label("Placement on UI")]
	//	public Vector2 placement;
	//}
	public class AMWClientConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Header("Ammunition")]
		[Label("Disable Gem bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableGems;
		[Label("Disable Gold/Platinum bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disablePHM;
		[Label("Disable tier 1 hardmode ore bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableHM1;
		[Label("Disable tier 2 hardmode ore bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableHM2;
		[Label("Disable tier 3 hardmode ore bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableHM3;
		[Label("Disable Demonite/Crimtane ore bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableDC;
		[Label("Disable Jester bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableOPstar;
		[Label("Disable Honey bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableHoney;
		[Label("Disable Sting bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableSting;
		[Label("Disable Shroomite bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableShroom;
		[Label("Disable Hallowed bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableHallowed;
		[Label("Disable Hellstone bullets?")]
		[DefaultValue(false)]
		[ReloadRequired]
		public bool disableHellstone;
        [Label("Disable Mecha bullets?")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool disableMecha;


    }
}