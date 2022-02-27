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
        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            bool listempty = true;
            for (int a = 0; a < 21; a++)
            {
                if (player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, a].type != 0) listempty = false;
            }

            if (!listempty)
            {
                while (player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type == 0)
                {
                    player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo++;
                    if (player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo == 21)
                    {
                        player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo = 0;
                    }
                }
                if (ammo.type != player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type)
                {
                    dontwasteammo = ammo.type;
                }
                else
                {
                    dontwasteammo = 0;
                }


                    if (player.HasItem(player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type))
                {
                    int a = 0;
                    for (a = 0; a < player.inventory.Length; a++)
                    {
                        if (player.inventory[a].type == (player.GetModPlayer<AmmWorkhopModPl>().bullets[player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo].type))
                        {
                            //ammo = player.inventory[a];
                            break;
                        }
                    }
                    player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo++;
                    if (player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo == 21)
                    {
                        player.GetModPlayer<AmmWorkhopModPl>().CurrentAmmo = 0;
                    }
                    //at this point i don't know what am i doing with my life honestly
                    if ((ammo.type!= player.inventory[a].type)&& CanBeConsumedAsAmmo(player.inventory[a], player)&& CanConsumeAmmo(weapon, player))
                    {
                        player.inventory[a].stack -= 1;
                    }
                    type = player.inventory[a].shoot;
                    base.PickAmmo(weapon, player.inventory[a], player, ref type, ref speed, ref damage, ref knockback);
                    return;
                }
            }

            base.PickAmmo(weapon, ammo, player, ref type, ref speed, ref damage, ref knockback);
        }
        public override bool CanBeConsumedAsAmmo(Item ammo, Player player)
        {
            if (ammo.type!=dontwasteammo)
            {
                return base.CanBeConsumedAsAmmo(ammo, player);//poorly designed shit.
            }
            return false;
        }
    }
}
