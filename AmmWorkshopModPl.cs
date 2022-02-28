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

        public Item[,] bullets;
        public int CurrentMode =0;
        public int CurrentAmmo = 0;



        public override void PreUpdate()
        {
            if (AmmunitionWorkshop.changeleft.JustPressed)
            {
                Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode--;
                if (Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode < 0)
                {
                    Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode = 2;
                }
            }
            if (AmmunitionWorkshop.changeright.JustPressed)
            {
                Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode++;
                if (Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode > 2)
                {
                    Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode = 0;
                }
            }
                base.PreUpdate();
        }

        public float defBoost = 0;
        public override void SaveData(TagCompound tag)
        {
            tag.Add("amworkshop_bullet", CurrentMode);
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 21; b++)
                {
                    if (bullets[a, b] != null && bullets[a, b].type!=0 )
                    {
                        //Logging.PublicLogger.Debug($"Saved a key: bullet{a}_{b}. It's: {bullets[a, b].type}.");

                        tag.Add($"bullet{a}_{b}", bullets[a,b].type);//i understand this is wrong, but i don't understand how to make it right way.
                    }
                }
            }
            base.SaveData(tag);
        }
        public override void LoadData(TagCompound tag)
        {
            bullets = new Item[3, 21];
            if (tag.ContainsKey("amworkshop_bullet"))
            {
                CurrentMode = tag.GetAsInt("amworkshop_bullet");
            }
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 21; b++)
                {
                    bullets[a, b] = new Item();
                    if (tag.ContainsKey($"bullet{a}_{b}"))
                    {
                        //Logging.PublicLogger.Debug($"loaded a key: bullet{a}_{b}. It's: {bullets[a, b].type}.");
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
