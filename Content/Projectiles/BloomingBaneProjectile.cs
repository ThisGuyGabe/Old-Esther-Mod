using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EstherMod.Content.Projectiles
{
	public class BloomingBaneProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(595);

			AIType = 595;
			Main.projFrames[Projectile.type] = 28;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("BloomingBaneProjectile");

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(BuffID.Poisoned, 60, false);
			}
		}
	}
}
