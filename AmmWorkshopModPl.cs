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
    internal class AmmWorkhopModPl : ModPlayer
    {
        public Item[,] bullets = new Item[3,21];
        public int CurrentMode =0;
        public int CurrentAmmo = 0;

        
        //public override bool CanConsumeAmmo(Item weapon, Item ammo)
        //{
        //    if (weapon.ammo == AmmoID.Bullet)
        //    {
        //        bool listempty = true;
        //        for (int a = 0; a < 21; a++)
        //        {
        //            if (bullets[CurrentAmmo, a].type != 0) listempty = false;
        //        }
        //        if (!listempty)
        //        {
        //            while (bullets[CurrentAmmo, CurrentAmmo].type == 0) CurrentAmmo++;

        //            if (ammo.type == bullets[CurrentAmmo, CurrentAmmo].type)
        //            {
        //                CurrentAmmo++;
        //                return base.CanConsumeAmmo(weapon, ammo);
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }

            

        //    return base.CanConsumeAmmo(weapon, ammo);
        //}
        //public Item[] GetArrayItems()
        //{
        //    Item[] res = new Item[21];
        //    for (int a=0; a < 21; a++)
        //    {
        //        if (bullets[CurrentMode, a] == null)
        //        {
        //            bullets[CurrentMode, a]=new Item();

        //            //bullets[CurrentMode, a].SetDefaults(0);
        //        }
        //        //Main.NewText(bullets[CurrentMode, a]);
        //        res[a] = bullets[CurrentMode, a];
        //    }
        //    return res;
        //}

        public float defBoost = 0;
        public override void SaveData(TagCompound tag)
        {
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 21; b++)
                {
                    if (bullets[a, b] != null&& bullets[a, b].type!=0 )
                    {
                        Logging.PublicLogger.Debug($"Saved a key: bullet{a}_{b}. It's: {bullets[a, b].type}.");

                        tag.Add($"bullet{a}_{b}", bullets[a,b].type);
                    }
                }
            }
            base.SaveData(tag);
        }
        public override void LoadData(TagCompound tag)
        {
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 21; b++)
                {
                    bullets[a, b] = new Item();
                    if (tag.ContainsKey($"bullet{a}_{b}"))
                    {
                        bullets[a, b]= new Item((tag.GetAsInt($"bullet{a}_{b}")));
                        
                    }
                }
            }
            base.LoadData(tag);
        }

        public void AddDefBoost(float damage)
        {
            if (!Player.HasBuff<DefenceBoost>())
            {
                defBoost = 0;
            }
            Player.AddBuff(ModContent.BuffType<DefenceBoost>(),600);
            defBoost = Math.Clamp(defBoost+damage / 3000, 0, 0.45f);
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            damage =(int)(damage * (1 - defBoost));
            defBoost = 0;
            Player.ClearBuff(ModContent.BuffType<DefenceBoost>());
            base.ModifyHitByNPC(npc, ref damage, ref crit);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            damage = (int)(damage * (1 - defBoost));
            defBoost = 0;
            Player.ClearBuff(ModContent.BuffType<DefenceBoost>());
            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }

        //public override void SaveData(TagCompound tag)
        //{
        //    base.SaveData(tag);
        //}



    }
}
