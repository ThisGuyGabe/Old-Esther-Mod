using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace EstherMod.Content.Projectiles {
	public class CogProjectile : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.aiStyle = 2;
			Projectile.timeLeft = 380;
			Projectile.penetrate = 10;
			AIType = ProjectileID.BoulderStaffOfEarth;
		}
	}
}