using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Jester
{
	public class JesterBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fired bullets will split in three on shot.\r\nGuns will deal only 83% of original damage.\r\nI genuinely don't know how to use those.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !ModContent.GetInstance<AMWClientConfig>().disableOPstar;
		}

		public override void SetDefaults()
		{
			Item.damage = 0; 
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true; 
			Item.knockBack = 0.2f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<JesterBulletP>();
			Item.shootSpeed = 1; 
			Item.ammo = AmmoID.Bullet;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(50)
				.AddIngredient(ItemID.Star)
				.AddIngredient(ItemID.MusketBall, 50)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}