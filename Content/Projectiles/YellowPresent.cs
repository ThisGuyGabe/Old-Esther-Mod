using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace EstherMod.Content.Projectiles
{
	public class YellowPresent : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 20;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.aiStyle = 2;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 800;
			Projectile.penetrate = 5;
            AIType = ProjectileID.CursedFlameFriendly;
        }

		int bounce = 0;
		int maxBounces = 2;

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Dig.WithVolumeScale(0.5f).WithPitchOffset(0.8f), Projectile.position);
			for (int i = 0; i < 1; i++)
			{
				Vector2 newVelocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(180));
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, newVelocity, Mod.Find<ModProjectile>("Coal").Type, 8, 4, Projectile.owner);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			bounce++;
			for (var i = 0; i < 4; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7, 0f, 0f, 0, default(Color), 1f);
			}
			if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X;
			if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = -oldVelocity.Y;

			if (bounce >= maxBounces) return true;
			else return false;
		}
	}
}