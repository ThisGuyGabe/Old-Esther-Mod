using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Magic;

public sealed class FloraGaleStaff : BaseItem {
	public sealed override string Texture => "EstherMod/Assets/Weapons/Magic/FloraGaleStaff";

	public override void SetStaticDefaults() {
		Item.staff[Type] = true;
	}

	public override void SetDefaults() {
		Item.damage = 4;
		Item.DefaultToStaff(ModContent.ProjectileType<FloraLeaf>(), 3.5f, 25, 8);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		for (int i = 0; i < 3; i++) {
			Projectile.NewProjectile(
				source,
				position,
				velocity.RotatedByRandom(MathHelper.ToRadians(45)),
				type,
				damage,
				knockback,
				player.whoAmI,
				-1
			);
		}
		return false;
	}
}
public sealed class FloraLeaf : ModProjectile {
	public sealed override string Texture => "EstherMod/Assets/Weapons/Magic/FloraLeaf";

	public int TargetIndex {
		get => (int)Projectile.ai[0];
		set => Projectile.ai[0] = value;
	}

	public NPC Target {
		get {
			if (TargetIndex == -1)
				return null;
			return Main.npc[TargetIndex];
		}
	}

	public override void SetDefaults() {
		Projectile.width = 14;
		Projectile.height = 14;

		Projectile.hostile = false;
		Projectile.friendly = true;

		Projectile.timeLeft = 6400;
	}

	public override void AI() {
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		Projectile.ai[1]++;

		if (TargetIndex == -1 || !Target.active) {
			NPC closestNpc = null;
			for (int i = 0; i < Main.maxNPCs; i++) {
				var npc = Main.npc[i];
				if (npc.active && !npc.friendly && !npc.townNPC && !npc.immortal && npc.defense >= (closestNpc?.defense ?? -1) && Projectile.Center.DistanceSQ(closestNpc?.Center ?? npc.Center) <= 350f * 350f && Collision.CanHit(Projectile, npc)) {
					closestNpc = npc;
				}
			}
			TargetIndex = closestNpc?.whoAmI ?? -1;
		}

		if (Projectile.ai[1] < 30f || Target == null)
			return;

		float moveSpeed = 3.5f;
		float velMultiplier = 1f;

		var dist = Target.Center - Projectile.Center;
		float length = dist == Vector2.Zero ? 0f : dist.Length();
		if (length < moveSpeed) {
			velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
		}
		Projectile.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
		Projectile.velocity *= moveSpeed;
		Projectile.velocity *= velMultiplier;
	}
}