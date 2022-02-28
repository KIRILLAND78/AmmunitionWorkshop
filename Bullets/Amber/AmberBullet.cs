using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Amber
{
	public class AmberBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fired bullets will heal you by 5% of damage on hit.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 4; 
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true; 
			Item.knockBack = 1f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<AmberBulletP>();
			Item.shootSpeed = 2; 
			Item.ammo = AmmoID.Bullet;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(40)
				.AddIngredient(ItemID.Amber)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}