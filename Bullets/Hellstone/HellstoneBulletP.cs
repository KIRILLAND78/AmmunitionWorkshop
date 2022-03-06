using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Hellstone
{
	public class HellstoneBulletP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellstone Bullet");
		}
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !ModContent.GetInstance<AMWClientConfig>().disableHellstone;
		}

		public override void SetDefaults()
		{
			Projectile.width = 1;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged; 
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 0;
			Projectile.extraUpdates = 1;
			Projectile.light = 0.3f;
			AIType = ProjectileID.Bullet; // Act exactly like default Bullet
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.OnFire,120);
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.OnFire, 90);
			base.OnHitPvp(target, damage, crit);
        }

        public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
				Vector2 drawPos = (Projectile.position - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			

			return true;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}