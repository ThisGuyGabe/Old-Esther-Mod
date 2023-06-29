using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Projectiles
{

	public class Ruby : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.aiStyle = 1;
			Projectile.width = 14;
			Projectile.height = 18;
			Projectile.ignoreWater = true;
			Projectile.friendly = true;
			Projectile.timeLeft = 200;
			Projectile.DamageType = DamageClass.Melee;
			Main.projFrames[Projectile.type] = 5;
			AIType = ProjectileID.Bullet;
			Projectile.penetrate = 3;
		}
		public override bool PreAI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 10)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 5;
			}
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			for (int i = 0; i < 1; i++)
			{
				int proj = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ProjectileID.RubyBolt, (int)(Projectile.damage * 0.4f), 0, Projectile.owner);
				Main.projectile[proj].velocity = new Vector2((float)(Projectile.velocity.X + Main.rand.Next(-1, 1)), (float)(Projectile.velocity.Y + Main.rand.Next(-1, 1)));
			}
			Projectile.Kill();
		}

		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 90, Projectile.oldVelocity.X * 0.4f, Projectile.oldVelocity.Y * 0.4f);
		}
	}
}