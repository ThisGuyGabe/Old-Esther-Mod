using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace EstherMod.Content.Projectiles
{

	public class Ruby : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ruby");

			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.aiStyle = 29;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 800;
			Projectile.penetrate = 5;
			AIType = ProjectileID.RubyBolt;
		}

        public override void AI()
        {
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y - Projectile.Center.Y;
                float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                if (distance < 480f && !target.friendly && target.active)
                {
                    if (Projectile.ai[0] > 10f)
                    {
                        distance = 3f / distance;
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;
                        int proj = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, shootToX, shootToY, Mod.Find<ModProjectile>("Ruby").Type, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                        Main.projectile[proj].timeLeft = 300;
                        Main.projectile[proj].netUpdate = true;
                        Projectile.netUpdate = true;
                        SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
                        Projectile.ai[0] = -50f;
                    }
                }
            }
            Projectile.ai[0] += 1f;
        }
    }
}