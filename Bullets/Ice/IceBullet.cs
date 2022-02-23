using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Ice
{
	public class IceBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fired bullets reduce target's velocity.\r\nFired bullets will penetrate 1 enemy.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 11; 
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true; 
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<IceBulletP>();
			Item.shootSpeed = 6; 
			Item.ammo = AmmoID.Bullet;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(333)
				.AddIngredient(ItemID.FrostCore)
				.Register();
		}
	}
}