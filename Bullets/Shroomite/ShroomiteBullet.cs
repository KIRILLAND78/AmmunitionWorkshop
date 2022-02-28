using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Shroomite
{
	public class ShroomiteBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fired bullets will chase after your enemies and penetrate them once.");
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
			Item.knockBack = 1f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<ShroomiteBulletP>();
			Item.shootSpeed = 1; 
			Item.ammo = AmmoID.Bullet;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(60)
				.AddIngredient(ItemID.ShroomiteBar)
				.AddIngredient(ItemID.MusketBall,60)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}