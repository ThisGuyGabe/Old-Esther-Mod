using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles {
	public class BloomingBaneProjectile : ModProjectile {
		public override void SetDefaults() {
			Projectile.CloneDefaults(595);
			AIType = 595;
			Main.projFrames[Projectile.type] = 28;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			if (Main.rand.NextBool(2)) {
				target.AddBuff(BuffID.Poisoned, 60, false);
			}
		}
	}
}
