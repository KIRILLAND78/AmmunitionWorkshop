using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AmmunitionWorkshop
{
    internal class AmmWorkshopModNpc : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        int frozentime = 0;
        float frozenpower = 0;
        float frozenpowerfly = 0;
        public void Freeze(int dur, float dmg)
        {
            dmg = (float)Math.Sqrt(dmg)+3;
            frozentime = dur;
            frozenpower = 1 - (dmg / 100);
            frozenpowerfly = 1 - (dmg / 520);
        }

        public override void PostAI(NPC npc)
        {
            if (frozentime > 1)
            {
                npc.color = Color.LightBlue;
                frozentime -= 1;
                if (npc.collideY == true)
                {
                    npc.velocity = npc.velocity * frozenpower;
                }else
                {
                    npc.velocity = npc.velocity * frozenpowerfly;
                }
            }
            if (frozentime == 1)
            {
                npc.color = Color.White;
            }
            base.PostAI(npc);
        }

    }
}
