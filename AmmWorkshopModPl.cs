using System;
using Terraria.ID;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using AmmunitionWorkshop.Buffs;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.Default;

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
        public float powBoost = 0;
        public override void SaveData(TagCompound tag)
        {
            tag.Add("amworkshop_bullet", CurrentMode);
            List<Item> bl_ls = new();
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 21; b++)
                {
                    if (bullets[a, b] == null) bullets[a, b] = new Item(ItemID.None);
                    if (bullets[a, b].ModItem is UnloadedItem) bullets[a, b] = new Item(ItemID.None);
                    bl_ls.Add(bullets[a, b]);
                }
            }
            

            tag["amworkshop_bullets_array"] = bl_ls;
            /**
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 21; b++)
                {
                    if (bullets[a, b] != null && bullets[a, b].type!=0 )
                    {
                        //Logging.PublicLogger.Debug($"Saved a key: bullet{a}_{b}. It's: {bullets[a, b].type}.");

                        try
                        {
                            tag[$"bullet{a}_{b}"] = ItemIO.Save(bullets[a, b]);
                            //tag.Add($"bullet{a}_{b}", bullets[a, b].type);//i understand this is wrong, but i don't understand how to make it right way.
                        }
                        catch { }
                    }
                }
            }**/
            base.SaveData(tag);
        }
        public override void Initialize()
        {
            bullets = new Item[3, 21];

            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 21; b++)
                {
                    bullets[a, b] = new Item(ItemID.None);
                }
            }
            base.Initialize();
        }
        public override void LoadData(TagCompound tag)
        {
            bullets = new Item[3, 21];//issue was, LoadData for some reason have not created array of items. like wth.
            if (tag.ContainsKey("amworkshop_bullet"))
            {
                CurrentMode = tag.GetAsInt("amworkshop_bullet");
            }
            if (tag.ContainsKey("amworkshop_bullets_array"))
            {
                var list = tag.GetList<TagCompound>("amworkshop_bullets_array");
                for (int a = 0; a < 3; a++)
                {
                    for (int b = 0; b < 21; b++)
                    {
                        bullets[a, b] = new Item(ItemID.None);
                        /*if (tag.ContainsKey($"bullet{a}_{b}"))
                        {
                            try//does try..catch counts as defensive programming?
                            {

                                //Logging.PublicLogger.Debug($"loaded a key: bullet{a}_{b}. It's: {bullets[a, b].type}.");
                                bullets[a, b] = ItemIO.Load()//new Item(tag.GetAsInt($"bullet{a}_{b}"));
                            }
                            catch { }
                        }*/
                        try
                        {
                            ItemIO.Load(bullets[a, b], list[a * 21 + b]);
                        }
                        catch
                        {

                        }
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
        public void AddPowBoost(float damage)
        {
            if (!Player.HasBuff<PowerBoost>())
            {
                powBoost = 0;
            }
            Player.AddBuff(ModContent.BuffType<PowerBoost>(), 400);
            powBoost = Math.Clamp(powBoost + damage / 8000, 0, 0.30f);
        }
        public override void PostUpdate()
        {
            powBoost -= 0.0002f;
            if (powBoost < 0)
            {
                powBoost = 0;
                Player.ClearBuff(ModContent.BuffType<PowerBoost>());
            }
            base.PostUpdate();
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (!Player.HasBuff<PowerBoost>())
            {
                powBoost = 0;
            }
            damage += powBoost;
            base.ModifyWeaponDamage(item, ref damage);
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (!Player.HasBuff<DefenceBoost>())
            {
                defBoost = 0;
            }
            modifiers.FinalDamage *= (1 - defBoost);
            defBoost = 0;
            Player.ClearBuff(ModContent.BuffType<DefenceBoost>());
            base.ModifyHitByNPC(npc, ref modifiers);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (!Player.HasBuff<DefenceBoost>())
            {
                defBoost = 0;
            }
            modifiers.FinalDamage *= (1 - defBoost);
            defBoost = 0;
            Player.ClearBuff(ModContent.BuffType<DefenceBoost>());
            base.ModifyHitByProjectile(proj, ref modifiers);
        }

        //public override void SaveData(TagCompound tag)
        //{
        //    base.SaveData(tag);
        //}



    }
}
