using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Shroomite
{
	public class ShroomiteBulletP : ModProjectile
	{
		NPC target = null;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroomite Bullet");
		}

		public override void SetDefaults()
		{
			Projectile.width = 1;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged; 
			Projectile.penetrate = 2;
			Projectile.timeLeft = 600;
			Projectile.alpha = 0;
			Projectile.extraUpdates = 2;
			Projectile.light = 0.3f;
			AIType = ProjectileID.Bullet; // Act exactly like default Bullet
		}
        public override void AI()
        {
			if (Projectile.timeLeft <= 595)
			{
				int s = Dust.NewDust(Projectile.Center, 1, 1, DustID.BlueTorch, default, default, default, Color.Blue);
				Main.dust[s].velocity = Vector2.Zero;
				Main.dust[s].noGravity = true;
			}
			if ((Main.netMode == NetmodeID.Server) || (Main.netMode == NetmodeID.SinglePlayer))
			{
				if (target != null && !target.active)
				{
					target = null;
				}
				if (target != null)
				{
					//Main.NewText(MathHelper.ToDegrees(Projectile.velocity.ToRotation()));
					float angle = ((-Projectile.Center + target.Center).ToRotation() + ((-Projectile.Center + target.Center).ToRotation() < 0 ? MathHelper.TwoPi : 0) - Projectile.velocity.ToRotation() - (Projectile.velocity.ToRotation() < 0 ? MathHelper.TwoPi : 0) * -1) % MathHelper.TwoPi;
					angle = angle > MathHelper.Pi ? -(MathHelper.TwoPi - angle) : angle;
					Projectile.velocity = Projectile.velocity.RotatedBy(Math.Clamp(angle, (-MathHelper.Pi / 36), (MathHelper.Pi / 36)));

				}
				else
				{
					NPC buffer = null;
					float g = 0;
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						if (Vector2.Distance(Main.npc[i].Center, Projectile.Center) <= 200 && Main.npc[i].active && !Main.npc[i].friendly && Main.npc[i].type != NPCID.TargetDummy)
							if (g < Vector2.Distance(Main.npc[i].Center, Projectile.Center)) buffer = Main.npc[i];

					}
					target = buffer;
				}
			}


            base.AI();
        }

        public override bool PreDraw(ref Color lightColor)
		{

			return false;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}