using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles
{
	public class RiptideExplosion : ModProjectile
	{
		private const int DefaultWidthHeight = 40;
		private const int ExplosionWidthHeight = 120;

		public override void SetDefaults() {
			Projectile.height = 100;
			Projectile.scale = 5f;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1;
			DrawOffsetX = -2;
			DrawOriginOffsetY = -5;
			Projectile.damage = 45;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.AddBuff(BuffID.Wet, 30 * 10);
		}

		public override void AI() {
			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3) {
				Projectile.tileCollide = false;
				Projectile.alpha = 255;

				Projectile.Resize(ExplosionWidthHeight, ExplosionWidthHeight);
			}
			else {
				if (Main.rand.NextBool()) {
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f, 100, default, 1f);
					dust.scale = 0.1f + Main.rand.Next(5) * 0.1f;
					dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
					dust.noGravity = true;
					dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation - 2.1f, default) * 10f;
				}
			}
			Projectile.ai[0] += 1f;
			Projectile.rotation += Projectile.velocity.X * 0.1f;
		}

		public override void Kill(int timeLeft) {
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int i = 0; i < 50; i++) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f, 100, default, 2f);
				dust.velocity *= 1.4f;
			}
			Projectile.Resize(DefaultWidthHeight, DefaultWidthHeight);
		}
	}
}
