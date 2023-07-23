using CascadeMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Weapons.Magic;

public sealed class FloraGaleStaff : BaseItem {
	public sealed override string Texture => "CascadeMod/Assets/Weapons/Magic/FloraGaleStaff";

	public override void SetStaticDefaults() {
		Item.staff[Type] = true;
	}

	public override void SetDefaults() {
		Item.damage = 7;
		Item.DefaultToStaff(ModContent.ProjectileType<FloraGale>(), 5f, 25, 8);
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.RichMahogany, 15)
			.AddIngredient(ItemID.JungleSpores, 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		Projectile.NewProjectile(
			source,
			position,
			velocity.RotatedByRandom(MathHelper.ToRadians(20)),
			type,
			damage,
			knockback,
			player.whoAmI,
			-1
		);
		return false;
	}
}
public sealed class FloraLeaf : ModProjectile {
	public sealed override string Texture => "CascadeMod/Assets/Weapons/Magic/FloraLeaf";
	public override void SetDefaults() {
		Projectile.width = 14;
		Projectile.height = 14;

		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.ArmorPenetration = 10;
		Projectile.timeLeft = 210;
		Projectile.CountsAsClass(DamageClass.Magic);
	}

	public override void AI() {
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		Projectile.ai[1]++;
		Player owner = Main.player[Projectile.owner];
		SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC target);

		if (foundTarget) {
			float moveSpeed = 7f;
			float velMultiplier = 1f;
			var dist = targetCenter - Projectile.Center;
			float length = dist == Vector2.Zero ? 0f : dist.Length();
			if (length < moveSpeed) {
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			Vector2 changevelocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
			changevelocity *= moveSpeed;
			changevelocity *= velMultiplier;
			Projectile.velocity = (changevelocity + (Projectile.velocity * 14)) / 15;
		}
		else {
			Projectile.velocity.Y += 0.25f;
		}
	}

	private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC target) {
		distanceFromTarget = 100f;
		targetCenter = Projectile.position;
		foundTarget = false;
		if (!foundTarget) {
			for (int i = 0; i < Main.maxNPCs; i++) {
				target = Main.npc[i];

				if (target.CanBeChasedBy()) {
					float between = Vector2.Distance(target.Center, Projectile.Center);
					bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
					bool inRange = between < distanceFromTarget;
					bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height);
					bool closeThroughWall = between < 100f;

					if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall)) {
						distanceFromTarget = between;
						targetCenter = target.Center;
						foundTarget = true;
					}
				}
			}
		}
		target = null;

	}
}
public sealed class FloraGale : ModProjectile {
	public sealed override string Texture => "CascadeMod/Assets/Weapons/Magic/FloraGale";
	public override void SetDefaults() {
		Projectile.width = 30;
		Projectile.height = 30;

		Projectile.hostile = false;
		Projectile.friendly = false;
		Projectile.timeLeft = 100;
	}
	public override void AI() {
		Player player = Main.player[Projectile.owner];
		Projectile.velocity = Projectile.velocity * 0.99f;
		Projectile.ai[1]++;
		if (Projectile.ai[1] % 25 == 12){
			Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), ModContent.ProjectileType<FloraLeaf>(), Projectile.damage, 1, player.whoAmI);
		}
	}
	int i = 0;
	int icount = Main.rand.Next(3,5);
	public override void Kill(int timeLeft) {
		Player player = Main.player[Projectile.owner];
		if (Projectile.owner == Main.myPlayer) {
			while (i < icount) {
				i++;
				Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<FloraLeaf>(), (int)(Projectile.damage * 1.25f), 1, player.whoAmI);
			}
		}
	}
}
