using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Cobalt
{
	public class CobaltBullet : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !ModContent.GetInstance<AMWClientConfig>().disableHM1;
		}
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 12; 
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true; 
			Item.knockBack = 2f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<CobaltBulletP>();
			Item.shootSpeed = 1; 
			Item.ammo = AmmoID.Bullet;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(300)
				.AddIngredient(ItemID.CobaltBar)
				.AddIngredient(ItemID.MusketBall, 300)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}