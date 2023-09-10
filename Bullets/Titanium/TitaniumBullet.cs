using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Titanium
{
	public class TitaniumBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Fired bullets give you stacking damage resistance buff.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !ModContent.GetInstance<AMWClientConfig>().disableHM3;
		}
		public override void SetDefaults()
		{
			Item.damage = 15; 
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true; 
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<TitaniumBulletP>();
			Item.shootSpeed = 2; 
			Item.ammo = AmmoID.Bullet;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(100)
				.AddIngredient(ItemID.TitaniumBar)
				.AddIngredient(ItemID.MusketBall, 100)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}