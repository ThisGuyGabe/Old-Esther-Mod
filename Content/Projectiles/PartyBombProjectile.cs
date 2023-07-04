using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles {
	public class PartyBombProjectile : ModProjectile {
		private const int pulseCount = 3;
		private const int rippleSize = 15;
		private const int speed = 45;
		private const float gravity = 0.15f;
		private const float vibrationScalarMax = 12f;
		private int dustType = 61;

		private float rotationSpeed = 0.03f;

		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults() {
			Projectile.width = 20;
			Projectile.height = 24;
			Projectile.timeLeft = 420;
			Projectile.penetrate = -1;
		}

		public override void AI() {

			if (Projectile.timeLeft <= 180) {
				if (Projectile.ai[0] != 2) {
					Projectile.ai[0] = 2;
					Projectile.alpha = 255;

					if (!Filters.Scene["HydraShockwave"].IsActive()) {
						Filters.Scene.Activate("HydraShockwave", Projectile.Center).GetShader().UseColor(pulseCount, rippleSize, speed).UseTargetPosition(Projectile.Center);
					}

					Main.player[Projectile.owner].GetModPlayer<EstherPlayer>().partyBombActive = true;

					SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
					SoundEngine.PlaySound(SoundID.NPCDeath58.WithPitchOffset(-0.4f), Projectile.position);

					Vector2 velocity = Utils.ToRotationVector2(Projectile.rotation);

					for (var i = 0; i < 40; i++) {
						Vector2 dustVelocity = velocity * Main.rand.NextFloat();

						if (i % 2 == 0) {
							dustVelocity = -dustVelocity;
						}

						Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType, velocity.X, velocity.Y, 0, default(Color), 4f);
						dust.velocity = dustVelocity * 40f;
						dust.noGravity = true;
					}

					Vector2 goreVelocity = Utils.RotatedBy(velocity, 0.25 * Math.PI);

					for (var i = 0; i < 4; i++) {
						goreVelocity = new Vector2(-goreVelocity.Y, goreVelocity.X);

						Gore gore = Gore.NewGoreDirect(Projectile.GetSource_NaturalSpawn(), Projectile.Center, Vector2.Zero, Main.rand.Next(61, 64), 2f);
						gore.velocity = goreVelocity * 10;
					}
				}

				var progress = (180f - Projectile.timeLeft) / 60f;
				Filters.Scene["HydraShockwave"].GetShader().UseProgress(progress).UseOpacity(100f);

				return;
			}
			if (Projectile.timeLeft <= 330) {
				if (Projectile.ai[0] == 0) {
					Projectile.ai[0] = 1;
					Projectile.frame++;
					SoundEngine.PlaySound(SoundID.Item61.WithPitchOffset(0.7f), Projectile.position);
					rotationSpeed *= 0.5f;
				}

				Lighting.AddLight(Projectile.Center, new Vector3(0.5f, 1, 0.5f));

				Projectile.velocity *= 0.9f;

				var vibrationScalar = (1 - (Projectile.timeLeft - 180) / 150f) * vibrationScalarMax;

				var vibrationX = Main.rand.NextFloat() * vibrationScalar;
				var direction = ((Projectile.timeLeft % 2) - 0.5f) * 2;
				Projectile.position += new Vector2(vibrationX * direction, 0);

				if (Main.rand.NextFloat() > (Projectile.timeLeft - 180) / 150f) {
					float xPos;
					float yPos;

					do {
						xPos = Main.rand.Next(-32, 33);
						yPos = Main.rand.Next(-32, 33);
					}
					while (Math.Abs(xPos) + Math.Abs(yPos) < 16f);

					Vector2 spawnPos = Projectile.Center + Projectile.velocity * 14 + new Vector2(xPos, yPos);
					Vector2 velocity = (Projectile.Center + Projectile.velocity * 14 - spawnPos) * 0.075f;

					Dust dust = Dust.NewDustPerfect(spawnPos, dustType, velocity, 0, default(Color), 2);
					dust.noGravity = true;
				}
			}
			else {
				Lighting.AddLight(Projectile.Center, new Vector3(0.5f, 1, 0.5f));

				if (Projectile.timeLeft % 4 == 0) {
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0, 0, 0, default(Color), 2f);
				}
			}

			Projectile.rotation += (float)(Math.PI * rotationSpeed) * Projectile.direction;
			Projectile.velocity.Y += gravity;
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			return false;
		}

		public override void Kill(int timeLeft) {
			Filters.Scene["HydraShockwave"].Deactivate();
		}
	}
}