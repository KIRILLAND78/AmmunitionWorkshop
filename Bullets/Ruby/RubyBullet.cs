using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Ruby
{
	public class RubyBullet : GemBullet
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModContent.GetInstance<AMWClientConfig>().disableGems;
        }
        public override string gem_name => "ruby";
    }
}