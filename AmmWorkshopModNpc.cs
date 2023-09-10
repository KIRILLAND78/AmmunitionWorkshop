using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
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
        
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if ((npc.type == NPCID.SkeletronPrime)||(npc.type == NPCID.TheDestroyer) || (npc.type == NPCID.Spazmatism) || (npc.type == NPCID.Retinazer))
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bullets.Energy.EnergyBullet>(), 1, 300,300));
            base.ModifyNPCLoot(npc, npcLoot);
        }
        public bool CanBeHitByProjectileB(NPC npc, Projectile projectile)
        {
            bool invuln = false;
            if (base.CanBeHitByProjectile(npc, projectile) == true)
            {
                invuln = true;
            }
            return invuln;
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
