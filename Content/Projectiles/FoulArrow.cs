using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles {
	public class FoulArrow : ModProjectile {
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // trail length
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
			ProjectileID.Sets.TrailingMode[Type] = 0;
		}

		public override void SetDefaults() {
			Projectile.width = 14;
			Projectile.height = 34;
			Projectile.aiStyle = 1;
			Projectile.arrow = true;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 2;
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			for (var i = 0; i < 4; i++) {
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptionThorns, 0f, 0f, 0, default(Color), 1f);
				SoundEngine.PlaySound(SoundID.Dig.WithVolumeScale(0.5f).WithPitchOffset(0.8f), Projectile.position);
			}
			return true;
		}

		public override void AI() {
			float num231 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
			float num232 = Projectile.localAI[0];
			if (num232 == 0f) {
				Projectile.localAI[0] = num231;
				num232 = num231;
			}
			float projX = Projectile.position.X;
			float projY = Projectile.position.Y;
			float num235 = 800f;
			bool flag6 = false;
			int num236 = 0;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 20f) {
				Projectile.ai[0] -= 1f;
				if (Projectile.ai[1] == 0f) {
					for (int num237 = 0; num237 < 200; num237++) {
						if (Main.npc[num237].CanBeChasedBy(Projectile) && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(num237 + 1))) {
							float num238 = Main.npc[num237].position.X + (float)(Main.npc[num237].width / 2);
							float num239 = Main.npc[num237].position.Y + (float)(Main.npc[num237].height / 2);
							float num240 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num238) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num239);
							if (num240 < num235 && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[num237].position, Main.npc[num237].width, Main.npc[num237].height)) {
								num235 = num240;
								projX = num238;
								projY = num239;
								flag6 = true;
								num236 = num237;
							}
						}
					}
					if (flag6) {
						Projectile.ai[1] = num236 + 1;
					}
					flag6 = false;
				}
				if (Projectile.ai[1] != 0f) {
					int num241 = (int)(Projectile.ai[1] - 1f);
					if (Main.npc[num241].active && Main.npc[num241].CanBeChasedBy(Projectile, ignoreDontTakeDamage: true)) {
						float num242 = Main.npc[num241].position.X + (float)(Main.npc[num241].width / 2);
						float num243 = Main.npc[num241].position.Y + (float)(Main.npc[num241].height / 2);
						if (Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num242) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num243) < 1000f) {
							flag6 = true;
							projX = Main.npc[num241].position.X + (float)(Main.npc[num241].width / 2);
							projY = Main.npc[num241].position.Y + (float)(Main.npc[num241].height / 2);
						}
					}
				}
				if (flag6) {
					float num244 = num232;
					Vector2 val42 = default(Vector2);
					val42 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
					float num245 = projX - val42.X;
					float num246 = projY - val42.Y;
					float num247 = (float)Math.Sqrt(num245 * num245 + num246 * num246);
					num247 = num244 / num247;
					num245 *= num247;
					num246 *= num247;
					int num248 = 8;
					Projectile.velocity.X = (Projectile.velocity.X * (float)(num248 - 1) + num245) / (float)num248;
					Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num248 - 1) + num246) / (float)num248;
				}
			}
		}

		public override bool PreDraw(ref Color lightColor) {
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
