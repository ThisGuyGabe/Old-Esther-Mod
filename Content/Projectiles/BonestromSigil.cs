using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Projectiles {
	public class BonestromSigil : ModProjectile {
		public override void SetStaticDefaults() {
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults() {
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 200;
		}
		private int manaCost = 0;
		public override void AI() {
			Player player = Main.player[Projectile.owner];
			if (player.channel == true) {
				Projectile.timeLeft = 3;
				if (Projectile.ai[0] % 10 == 0) {
					manaCost += 1;
				}
			}
			else {
				player.statMana -= manaCost;
				Projectile.Kill();
			}
			if (manaCost >= player.statMana) {
				player.statLife -= manaCost / 10;
				player.statMana -= manaCost;
				CombatText.NewText(player.getRect(), Color.Red, -manaCost / 10);
				Projectile.Kill();
			}
			if (Projectile.ai[0] % 50 == 25) {
				CombatText.NewText(player.getRect(), Color.CornflowerBlue, -manaCost);
			}

			Projectile.ai[0]++;
			Projectile.velocity = Vector2.Zero;
			if (Projectile.ai[0] % 25 == 0) {
				Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)) * 5, ModContent.ProjectileType<BonestromBolt>(), Projectile.damage, 2, Projectile.owner);
			}
		}
		public override void Kill(int timeLeft) {
			for (int i = 0; i < 90; i++) {
				double deg = i * 4;
				double rad = (deg) * (Math.PI / 180);
				double dist = 30;

				float x2 = (Projectile.Center.X + Projectile.width / 2) - (int)(Math.Cos(rad) * dist);
				float y2 = (Projectile.Center.Y + Projectile.height / 2) - (int)(Math.Sin(rad) * dist);
				int dust = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.CrimsonTorch, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}