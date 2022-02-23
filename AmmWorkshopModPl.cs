using System;
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
        public Item[,] bullets = new Item[3,30];
        public int CurrentMode =0;

        public Item[] GetArrayItems()
        {
            Item[] res = new Item[30];
            for (int a=0; a < 30; a++)
            {
                if (bullets[CurrentMode, a] == null)
                {
                    bullets[CurrentMode, a]=new Item();
                }
                //Main.NewText(bullets[CurrentMode, a]);
                res[a] = bullets[CurrentMode, a];
            }
            return res;
        }

        public float defBoost = 0;
       
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
