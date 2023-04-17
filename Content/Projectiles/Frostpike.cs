using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace EstherMod.Content.Projectiles
{
    public class Frostpike : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frostpike");
		}

		public override void SetDefaults()
		{
			Projectile.penetrate = 2;
			Projectile.width = 14;
			Projectile.height = 20;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			AIType = ProjectileID.Bone;
		}
		public override void AI()
		{
			if (Main.rand.NextBool(5))
			{
				Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.ApprenticeStorm, 0, 0, 0, default, Scale: 1);
			}
		}
	}
}