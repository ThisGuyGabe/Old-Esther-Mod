using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles
{
	public class StellarRainProjectile : ModProjectile
	{
		public override void SetDefaults() 
		{
			Projectile.CloneDefaults(ProjectileID.StarWrath); 
			AIType = ProjectileID.StarWrath;
			Projectile.width = 5;
			Projectile.height = 12;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.light = 1.5f;
		}

		public override Color? GetAlpha(Color lightColor) {
			return new Color(151, 98, 23, 127);
		}

		public override void AI()
		{
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.YellowTorch, 0f, 0f, 5, default(Color), 0.8f);
			Main.dust[dust].noGravity = false;
		}


		public override void Kill(int timeLeft) 
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}
