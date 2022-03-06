using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Crimtane
{
	public class CrimtaneBullet : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !ModContent.GetInstance<AMWClientConfig>().disableDC;
		}
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 10; 
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true; 
			Item.knockBack = 2.6f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<CrimtaneBulletP>();
			Item.shootSpeed = 6; 
			Item.ammo = AmmoID.Bullet;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(70)
				.AddIngredient(ItemID.CrimtaneBar)
				.AddIngredient(ItemID.MusketBall, 70)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}