using System;
using Terraria.ID;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using AmmunitionWorkshop.Buffs;
using Terraria.ModLoader.IO;
namespace AmmunitionWorkshop
{
    internal class AmmunitionGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => false;
        static int dontwasteammo = 0;
        static int sa = 0;
        //static bool trash=false;
        public override void OnConsumedAsAmmo(Item ammo, Player player)
        {
            if (ammo.type == dontwasteammo)
            {
                ammo.stack += 1;
                //if (trash)
                //{
                //    player.trashItem.stack -= 1;
                //}
                //else
                //{
                    player.inventory[sa].stack -= 1;
                //}
            }
            base.OnConsumedAsAmmo(ammo, player);
        }
        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            dontwasteammo = 0;
            bool listempty = true;
            for (int a = 0; a < 21; a++)
            {
                if (player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, a].type != 0 && player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, a].ammo==weapon.useAmmo) listempty = false;
            }

            if (!listempty)
            {
                while (player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type == 0 || player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].ammo != weapon.useAmmo)
                {
                    player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo++;
                    if (player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo == 21)
                    {
                        player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo = 0;
                    }
                }
                if (ammo.type != player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type && player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type != 0)
                {
                    dontwasteammo = ammo.type;
                }
                else
                {
                    dontwasteammo = 0;
                }
                //if (ammo.type != player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type)
                //{
                //    dontwasteammo = ammo.type;
                //}
                //else
                //{
                //    dontwasteammo = 0;
                //}


                if (player.HasItem(player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type))
                {
                    int a = 0;
                    //trash = false;
                    for (a = 0; a < player.inventory.Length; a++)
                    {
                        if (player.inventory[a].type == (player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type))
                        {
                            //ammo = player.inventory[a];
                            break;
                        }
                        //if (a == player.inventory.Length)
                        //{

                        //    trash = true;
                        //}
                    }
                    player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo++;
                    if (player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo == 21)
                    {
                        player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo = 0;
                    }sa = a;

                    //at this point i don't know what am i doing with my life honestly
                    if ((weapon.useAmmo != player.inventory[a].ammo))
                    {

                        dontwasteammo = 0;
                        //player.inventory[a].TurnToAir();
                    }
                    else
                    {

                        type = player.inventory[a].shoot;
                    }
                    base.PickAmmo(weapon, player.inventory[a], player, ref type, ref speed, ref damage, ref knockback);
                    return;
                }
                else
                {
                    dontwasteammo = 0;
                }
            }

            base.PickAmmo(weapon, ammo, player, ref type, ref speed, ref damage, ref knockback);
        }

    }
}
