using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AmmunitionWorkshop.Bullets.Topaz
{
	public class TopazBullet : GemBullet
    {

        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModContent.GetInstance<AMWClientConfig>().disableGems;
        }
        public override string gem_name => "topaz";
    }
}