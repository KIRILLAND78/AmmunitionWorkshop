using AmmunitionWorkshop.Bullets.Amber;
using AmmunitionWorkshop.Bullets.Amethyst;
using AmmunitionWorkshop.Bullets.Diamond;
using AmmunitionWorkshop.Bullets.Emerald;
using AmmunitionWorkshop.Bullets.Ruby;
using AmmunitionWorkshop.Bullets.Sapphire;
using AmmunitionWorkshop.Bullets.Topaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmunitionWorkshop.Bullets
{

    public class GemBullet : ModItem
    {
        static object[,] bullets;
        static GemBullet()
        {
            bullets = new object[,]{
                { 4, 1f, 0.062f, 3, ItemID.Amethyst, 0 },//ame
                { 4, 1f, 0.06f, 2, ItemID.Topaz, 0 },//topaz
                { 4, 1f, 0.052f, 1, ItemID.Sapphire, 0 },//sapphire
                { 4, 1f, 0.12f,  1, ItemID.Emerald, 0 },//emer
                { 4, 1f, 0.16f, 2, ItemID.Ruby, 0 },//ruby
                { 5, 1f, 0.03f, 1, ItemID.Amber, 1 },//amber
                { 5, 1f, 0.20f, 1, ItemID.Diamond, 0 },//diamond
                
                { 5, 1f, 0.050f, 1, ItemID.Diamond, 0 },//opal
                { 5, 1f, 0.045f, 2, ItemID.Diamond, 0 },//aqua
                { 5, 1f, 0, 1, ItemID.Diamond, 0 },//onyx
            };
        }
        public static object GetInfo(string gem, string stat)
        {
            if (stat == "shootid")
            {
                switch (gem)
                {
                    case "amethyst": return ModContent.ProjectileType<AmethystBulletP>();
                    case "topaz": return ModContent.ProjectileType<TopazBulletP>();
                    case "sapphire": return ModContent.ProjectileType<SapphireBulletP>();
                    case "emerald": return ModContent.ProjectileType<EmeraldBulletP>();
                    case "ruby": return ModContent.ProjectileType<RubyBulletP>();
                    case "amber": return ModContent.ProjectileType<AmberBulletP>();
                    case "diamond": return ModContent.ProjectileType<DiamondBulletP>();

                    //thorium
                    case "aquamarine": return ModContent.ProjectileType<DiamondBulletP>();
                    case "opal": return ModContent.ProjectileType<DiamondBulletP>();
                    case "onyx": return ModContent.ProjectileType<DiamondBulletP>();
                }
            }
            int x = 0;
            int y = 0;


            switch (gem)
            {
                case "amethyst": x = 0; break;
                case "topaz": x = 1; break;
                case "sapphire": x = 2; break;
                case "emerald": x = 3; break;
                case "ruby": x = 4; break;
                case "amber": x = 5; break;
                case "diamond": x = 6; break;
                //thorium
                case "aquamarine": x = 7; break;
                case "opal": x = 8; break;
                case "onyx": x = 9; break;
            }

            switch (stat)
            {
                case "damage": y = 0; break;
                case "knockback": y = 1; break;
                case "damagetomana": y = 2; break;
                case "onein": y = 3; break;
                case "craftitemid": y = 4; break;
                case "effect": y = 5; break;
            }
            return bullets[x, y];
        }

        public virtual string gem_name {get;} = "none";
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Fired will restore your mana by 7.5% of damage with 50% chance.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Bullet effect", "When hit will restore your " + (((int)GetInfo(gem_name, "effect") == 1)?"health":"mana")+" by " + (int)(100 * (float)GetInfo(gem_name, "damagetomana")) + "% of damage dealt with " + 100 / (int)GetInfo(gem_name, "onein") + "% chance."));
            base.ModifyTooltips(tooltips);
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public override void SetDefaults()
        {
            Item.damage = (int)GetInfo(gem_name, "damage");
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = (float)GetInfo(gem_name, "knockback");
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.shoot = (int)GetInfo(gem_name, "shootid");
            Item.shootSpeed = 2;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe(40)
                .AddIngredient((short)GetInfo(gem_name, "craftitemid"))
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
