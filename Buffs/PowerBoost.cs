using Terraria;
using Terraria.ModLoader;
using AmmunitionWorkshop;
namespace AmmunitionWorkshop.Buffs
{
class PowerBoost: ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.lightPet[Type] = false;
            Main.debuff[Type] = false;
            // DisplayName.SetDefault("Energy Amplifier");
            base.SetStaticDefaults();
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = "Current damage amplification: "+System.Convert.ToString(Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().powBoost*100)+"%";
            base.ModifyBuffText(ref buffName,ref tip, ref rare);
        }

    }
}