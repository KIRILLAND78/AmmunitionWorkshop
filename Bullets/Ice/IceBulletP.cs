using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace AmmunitionWorkshop.Bullets.Ice
{
	public class IceBulletP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ice Bullet");
		}
		public override bool IsLoadingEnabled(Mod mod)
		{
			return false;
			//return !ModContent.GetInstance<AMWClientConfig>().disableIce;
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
			Projectile.light = 0.5f;
			AIType = ProjectileID.Bullet; // Act exactly like default Bullet
			Projectile.extraUpdates = 1;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			//UniFreezeLib.UniFreezeLib.FreezeNPC(target.whoAmI, 0.03f*MathF.Sqrt(hit.Damage), 400);
			//Mod FreezeLib = ModLoader.GetMod("UniFreezeLib");
			//if (FreezeLib != null)
			//{
			//	Main.NewText("loaded");
				//FreezeLib.Call("FreezePlayer", Projectile.owner, 0.99f, 400);
				//FreezeLib.Call("FreezeNPC", target.whoAmI, 0.99f, 400);
			//}
			//else
			//{
			//	target.velocity = 0.98f * target.velocity;
			//}

			//target.GetGlobalNPC<AmmWorkshopModNpc>().Freeze(105, damage);
            base.OnHitNPC(target, hit, damageDone);
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