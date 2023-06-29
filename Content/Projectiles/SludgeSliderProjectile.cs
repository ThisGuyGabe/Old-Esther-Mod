using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles
{
	public class SludgeSliderProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
		
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 5.5f;

			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 250f;

			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13.5f;
		}

		public override void SetDefaults() {
			Projectile.width = 14; 
			Projectile.height = 14;
			Projectile.scale = 1.3f;

			Projectile.aiStyle = ProjAIStyleID.Yoyo;

			Projectile.friendly = true; 
			Projectile.DamageType = DamageClass.MeleeNoSpeed; 
			Projectile.penetrate = -1; 
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
				target.AddBuff(BuffID.Slimed, 60 * 5);
		}

		public override void PostAI() {
 		int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueFairy, 0f, 0f, 5);// the 56 means slimy saddle dusts.
 		Main.dust[dust].noGravity = false; 
			
		}
	}
}
