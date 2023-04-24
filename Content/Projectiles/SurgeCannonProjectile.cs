using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace EstherMod.Content.Projectiles 
{

	public class SurgeCannonProjectile : ModProjectile 
	{
		public override void SetStaticDefaults() {
		}

		public override void SetDefaults() {

			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;

			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 60 * 3;
		}

		public override Color? GetAlpha(Color lightColor) {
			return new Color(200, 236, 255, 63f);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.AddBuff(BuffID.Wet, 60 * 10);
		}

		public override void AI() {

			Projectile.ai[0] += 1f; // 15 ticks (1/4 of a second) before applying gravity.
			if (Projectile.ai[0] >= 15f)
			{
				Projectile.ai[0] = 15f;
				Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
			}
			if (Projectile.velocity.Y > 20f)
			{
				Projectile.velocity.Y = 20f; 
			}

			int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.SnowflakeIce, 0f, 0f, 0, default(Color), 1f);
			Main.dust[dust].noGravity = true;

		}
	}
}