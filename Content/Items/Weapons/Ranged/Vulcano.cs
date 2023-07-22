using EstherMod.Content.Projectiles;
using EstherMod.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static EstherMod.Content.Items.Weapons.Ranged.Vulcano.VulcanoArrow;

namespace EstherMod.Content.Items.Weapons.Ranged;

public sealed class Vulcano : BaseItem {

	int shoot;
	public sealed override string Texture => "EstherMod/Assets/Weapons/Ranged/Vulcano";
	public override void SetDefaults() {
		Item.damage = 71;
		Item.rare = ItemRarityID.Pink;
		Item.width = 28;
		Item.height = 70;
		Item.useAnimation = 20;
		Item.useTime = 20;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 4.5f;
		Item.shootSpeed = 16f;
		Item.DamageType = DamageClass.Ranged;
		Item.autoReuse = true;
		Item.noMelee = true;
		Item.shoot = ModContent.ProjectileType<VulcanoArrow>();
		Item.value = Item.sellPrice(gold: 15, silver: 67);
		Item.UseSound = SoundID.Item5;
		Item.useAmmo = AmmoID.Arrow;
		Item.noMelee = true;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<VulcanoArrow>(), ModContent.ProjectileType<VulcanoHomingArrow>(), ModContent.ProjectileType<VulcanoBowProj>() });
		}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		shoot++;
		if (shoot >= 3) {
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VulcanoBowProj>(), damage, knockback, player.whoAmI);
			Item.noUseGraphic = true;
			shoot = 0;
		} else {
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VulcanoArrow>(), damage, knockback, player.whoAmI);
			Item.noUseGraphic = false;
		}
		return false;
	}
	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.SoulofFright, 10)
			.AddIngredient<ForgedFury>()
			.AddIngredient(ItemID.DaedalusStormbow, 1)
			.AddIngredient(ItemID.HallowedBar, 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public sealed class VulcanoBowProj : ModProjectile {
		public sealed override string Texture => "EstherMod/Assets/Weapons/Ranged/Vulcano";
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
		}
		public override void SetDefaults() {
			Projectile.width = 28;
			Projectile.height = 70;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.aiStyle = 2;
			Projectile.ignoreWater = true;
			AIType = 48;
			Projectile.tileCollide = true;
			Projectile.penetrate = -1;
		}
		public override void AI() {
			if (Projectile.ai[0] > 60 && Projectile.ai[0] < 120) {
				Projectile.velocity = Vector2.Zero;
				Projectile.rotation = MathHelper.ToDegrees(360);

				if (Main.rand.NextBool(8)) {
					for (int i = 0; i < 2; i++) {
						var proj = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(0, -6).RotatedByRandom(MathHelper.ToRadians(10)), ModContent.ProjectileType<VulcanoHomingArrow>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
						Main.projectile[proj].extraUpdates = 2;
					}
				}
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Projectile.velocity.X = -Projectile.velocity.X;
			Projectile.velocity.Y = -Projectile.velocity.Y;

			target.AddBuff(BuffID.Confused, 180); // 3 seconds also funny since the enemy is confused on why u threw the bow at them
		}

		public override void Kill(int timeLeft) {
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		}

		public override bool PreDraw(ref Color lightColor) {
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}
	}

	public sealed class VulcanoArrow : ModProjectile {
		public sealed override string Texture => "EstherMod/Assets/Weapons/Ranged/VulcanoArrow";
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
		}
		public override void SetDefaults() {
			Projectile.width = 22;
			Projectile.height = 46;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.aiStyle = 1;
			AIType = ProjectileID.WoodenArrowFriendly;
			Projectile.ignoreWater = false; // because it's a fire arrow
			Projectile.tileCollide = true;
			Projectile.penetrate = 5;
		}

		public override void AI() {
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 0, default, 0.8f);
			Main.dust[dust].velocity *= 1.5f;
			Main.dust[dust].noGravity = true;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.AddBuff(BuffID.OnFire, 300); // 5 seconds
		}
		public override bool PreDraw(ref Color lightColor) {
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			for (int i = 0; i < 5; i++) {
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 0, default, 0.8f);
				Main.dust[dust].noGravity = true;
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			return true;
		}

		public sealed class VulcanoHomingArrow : ModProjectile {
			public sealed override string Texture => "EstherMod/Assets/Weapons/Ranged/VulcanoHomingArrow";
			public override void SetStaticDefaults() {
				ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
				ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
			}

			public override void SetDefaults() {
				Projectile.width = 22;
				Projectile.height = 46;
				Projectile.aiStyle = 1;
				AIType = ProjectileID.WoodenArrowFriendly;
				Projectile.ignoreWater = false; // because it's a fire arrow
				Projectile.tileCollide = true;
				Projectile.friendly = true;
				Projectile.hostile = false;
			}

			public override void AI() 
			{
				Player owner = Main.player[Projectile.owner];

				SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC target);
				// code from flora gale :)
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

			public override bool OnTileCollide(Vector2 oldVelocity) {
				for (int i = 0; i < 5; i++) {
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 0, default, 0.8f);
					Main.dust[dust].noGravity = true;
				}
				SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
				return true;
			}

			public override bool PreDraw(ref Color lightColor) {
				Main.instance.LoadProjectile(Projectile.type);
				Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

				Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < Projectile.oldPos.Length; k++) {
					Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
				}
				return true;
			}
			public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
				target.AddBuff(BuffID.OnFire, 300); // 5 seconds
			}
		}
	}
}
