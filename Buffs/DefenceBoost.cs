using Terraria;
using Terraria.ModLoader;
using AmmunitionWorkshop;
namespace AmmunitionWorkshop.Buffs
{
class DefenceBoost: ModBuff
{
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Current damage reduction: "+System.Convert.ToString(Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().defBoost*100)+"%";
            base.ModifyBuffTip(ref tip, ref rare);
        }

    }
}