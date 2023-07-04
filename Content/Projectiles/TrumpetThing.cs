using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace EstherMod.Content.Projectiles {
	public class TrumpetThing : ModProjectile {
		public override void SetDefaults() {
			Projectile.width = 16;
			Projectile.height = 36;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.aiStyle = 21;
			Projectile.timeLeft = 180;
			Projectile.penetrate = 2;
			AIType = ProjectileID.QuarterNote;
		}
	}
}