using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AmmunitionWorkshop.Bullets.Diamond
{
	public class DiamondBullet : GemBullet
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModContent.GetInstance<AMWClientConfig>().disableGems;
        }
        public override string gem_name => "diamond";
    }
}