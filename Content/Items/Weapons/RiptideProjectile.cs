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

	public class RiptideProjectile : ModProjectile 
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Riptide Blast");
		}

		public override void SetDefaults() {

			Projectile.width = 60;
			Projectile.height = 60;

			Projectile.friendly = true;
			Projectile.ignoreWater = true;

			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 30;
			Projectile.light = 1f;
		}

		public override Color? GetAlpha(Color lightColor) {
			return new Color(200, 236, 255, 63f);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.AddBuff(BuffID.Wet, 60 * 10);
		}

		public override void AI() {
			for (int i = 0; i < 30; i++)
            {

                float x2 = Projectile.Center.X - 5;
                float y2 = Projectile.Center.Y;
                int dust = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.BlueTorch, 0.5f, 0.5f, 0, default(Color), 1f);
                Main.dust[dust].noGravity = false;
            }

		}
	}
}