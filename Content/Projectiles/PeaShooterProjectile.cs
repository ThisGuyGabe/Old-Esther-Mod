using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles
{

	public class PeaShooterProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = 8;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.penetrate = 3;
			AIType = ProjectileID.CursedFlameFriendly;
		}
    }
}