using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles
{
	public class Sinewave : ModProjectile
	{
		public Vector2 initialCenter;

		public int sineTimer;

		public float waveOffset;

		public override void SetStaticDefaults() 
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults() {
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true; 
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
		}
		public override bool ShouldUpdatePosition() {
			return false;
		}

		public override void OnSpawn(IEntitySource source) {
			initialCenter = Projectile.Center;
		}

		public override void AI() {
			Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();

			float velocityLength = Projectile.velocity.Length();

			if (velocityLength > 0) {
				float waveStride = 300f;

				float waveProgress = sineTimer * velocityLength / waveStride + waveOffset;
				float radians = waveProgress * MathHelper.TwoPi;
				float sine = MathF.Sin(radians) * Projectile.direction;

				Vector2 offset = Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.PiOver2 * -1);

				float waveAmplitude = 32;

				if (sineTimer < 20) {
					// Up to 1/3rd of a second (20/60 = 1/3), make the amplitude grow to the intended size
					float factor = 1f - sineTimer / 20f;
					waveAmplitude *= 1f - factor * factor;
				}

				offset *= sine * waveAmplitude;

				initialCenter += Projectile.velocity;
				Projectile.Center = initialCenter + offset;

				float cosine = MathF.Cos(radians) * Projectile.direction;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathF.Atan(-1 * cosine * waveAmplitude * velocityLength / waveStride);

				const float sineOf60Degrees = 0.866025404f;
				if (sine > sineOf60Degrees) {
					Projectile.frame = Projectile.direction == 1 ? 0 : 2;
				}
				else if (sine < -sineOf60Degrees) {
					Projectile.frame = Projectile.direction == 1 ? 2 : 0;
				}
				else {
					Projectile.frame = 1;
				}
			}
			else {
				// Failsafe for when the velocity is 0
				Projectile.frame = 1;
				Projectile.rotation = 0;
			}
			sineTimer++;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = ModContent.Request<Texture2D>("EstherMod/Content/Projectiles/GlowBall").Value;
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				float scale = Projectile.scale * (Projectile.oldPos.Length - k) / Projectile.oldPos.Length * .45f;
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(Color.Aquamarine) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(texture2D, drawPos, null, color, Projectile.rotation, TextureAssets.Projectile[Projectile.type].Value.Size(), scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
