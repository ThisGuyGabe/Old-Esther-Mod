using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ModLoader;
using static CascadeMod.Content.Items.Weapons.Ranged.VulcanoBowProj;

namespace CascadeMod.Content.Items.Weapons.Ranged;

public sealed class Vulcano : ModItem {

	int shoot;
	public sealed override string Texture => "CascadeMod/Assets/Weapons/Ranged/Vulcano";
	public override void SetDefaults() {
		Item.damage = 78;
		Item.rare = ItemRarityID.Pink;
		Item.width = 28;
		Item.height = 70;
		Item.useAnimation = 10;
		Item.useTime = 12;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 4.5f;
		Item.shootSpeed = 16f;
		Item.DamageType = DamageClass.Ranged;
		Item.autoReuse = true;
		Item.shoot = ModContent.ProjectileType<VulcanoArrow>();
		Item.value = Item.sellPrice(gold: 15, silver: 67);
		Item.UseSound = SoundID.Item5;
		Item.useAmmo = AmmoID.Arrow;
		Item.noMelee = true;
		Item.useLimitPerAnimation = 1;
	}
	public override bool CanUseItem(Player player) {
		if (!Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<VulcanoBowProj>())) {
			return true;
		}
		return false;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		if (!Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<VulcanoBowProj>())) {
			shoot++;
			if (shoot >= 5) {
				Projectile.NewProjectile(source, position - new Vector2(0, 15), velocity, ModContent.ProjectileType<VulcanoBowProj>(), damage, knockback, player.whoAmI);
				shoot = 0;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.noUseGraphic = true;
				Item.UseSound = SoundID.Item5;
				SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, player.Center + (velocity * 3));
				Item.useAmmo = AmmoID.Arrow;

			}
			else {
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VulcanoArrow>(), damage, knockback, player.whoAmI);
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.noUseGraphic = false;

				if (shoot == 4) {
					Item.UseSound = null;
					Item.useAmmo = AmmoID.None;
				}
			}
		}
		return false;
	}
	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient<ForgedFury>()
			.AddIngredient(ItemID.ChlorophyteBar, 12)
			.AddIngredient(ItemID.SoulofFright, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
	public sealed class VulcanoBowProj : ModProjectile {
		public sealed override string Texture => "CascadeMod/Assets/Weapons/Ranged/VulcanoProjectile";
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
		}
		public override void SetDefaults() {
			Projectile.width = 70;
			Projectile.height = 28;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.penetrate = -1;
		}
		public override void AI() {
		Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.ToRadians(90);
			Projectile.ai[0]++;
			if (Projectile.ai[0] < 20) {
				if (Projectile.ai[0] % 2 == 1) {
					Projectile.velocity.Y += 1;
				}
				if (Projectile.ai[0] < 10)
				{
					if (Main.player[Projectile.owner].direction == 1)
					{
						Main.player[Projectile.owner].SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.ToRadians(120 + (Projectile.ai[0] * (21 - Projectile.ai[1]))));
					}
					if (Main.player[Projectile.owner].direction == -1)
					{
						Main.player[Projectile.owner].SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.ToRadians(300 - (Projectile.ai[0] * (21 - Projectile.ai[1]))));
					}
				}
			}
			if (Projectile.ai[0] == 41) {
			FadingParticle flameParticle = ParticleOrchestrator._poolFading.RequestParticle();
				flameParticle.SetBasicInfo(TextureAssets.Projectile[540], new Rectangle?(), Vector2.Zero, Projectile.Center);
				flameParticle.SetTypeInfo(30);
				flameParticle.FadeInNormalizedTime = 0.005f;
				flameParticle.FadeOutNormalizedTime = 0.25f;
				flameParticle.ScaleAcceleration = Vector2.One * -0.01666667f / 5;
				flameParticle.Scale = new Vector2(0.4f, 0.4f);
				flameParticle.ColorTint = new Color(1, 0.85f, 0.15f);
				flameParticle.ScaleVelocity = new Vector2(0.2f, 0.2f);
				flameParticle._texture = TextureAssets.Projectile[540];
				Main.ParticleSystem_World_OverPlayers.Add(flameParticle);
			}
			
			Lighting.AddLight(Projectile.Center, new Vector3(1, 0.65f, 0.25f));
			if (Projectile.ai[0] == 10)
			{
			}
			if (Projectile.ai[0] > 40 && Projectile.ai[0] < 110) {
				Projectile.velocity = ((Projectile.velocity * 4) + Vector2.Zero) / 53;
				if (Projectile.ai[0] > 47) {
					Projectile.rotation = MathHelper.ToRadians(-180);
				}

				if (Projectile.ai[0] > 50) {
					if (Main.rand.NextBool(8)) {
						SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
						for (int i = 0; i < 2; i++) {
							
							Projectile.NewProjectile(Terraria.Entity.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(0, -6).RotatedByRandom(MathHelper.ToRadians(10)), ModContent.ProjectileType<VulcanoHomingArrow>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
						}
					}
				}

			}
			else {
				if (Projectile.ai[0] <= 40) {
					Projectile.velocity = ((Projectile.velocity * 9) + (Projectile.Center.DirectionTo(Main.MouseWorld) * 21)) / 10;
				}	
				if (Projectile.ai[0] >= 110) {
					Projectile.velocity = ((Projectile.velocity * 9) + (Projectile.Center.DirectionTo(Main.player[Projectile.owner].Center) * 27)) / 10;
					Projectile.tileCollide = false;
					if (Projectile.Center.Distance(Main.player[Projectile.owner].Center) < 15) {
						Projectile.Kill();
					}
				}	
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
            Projectile.Center -= Projectile.velocity * 2;
            Projectile.velocity *= -1;
            return false;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			if (Projectile.ai[0] < 110) {
				Projectile.velocity *= -1.5f;
			}
			

			target.AddBuff(BuffID.Confused, 180); // 3 seconds also funny since the enemy is confused on why u threw the bow at them
		}

		public override void Kill(int timeLeft) {
			SoundEngine.PlaySound(SoundID.MaxMana, Projectile.position);
			if (Projectile.owner == Main.myPlayer)
			{
                for (int i = 0; i < 5; i++)
                {
                    Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Top - new Vector2(Main.rand.Next(-6,6), Main.rand.Next(-32,-20)), DustID.ManaRegeneration, Vector2.Zero, 125, Scale: 1.5f);
                    d.noGravity = true;
                }
            }

        }

		public override bool PreDraw(ref Color lightColor) {
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.FlipVertically, 0);
			}
			return false;
		}
        public override void PostDraw(Color lightColor)
        {
			Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center, null, Color.White, Projectile.rotation, Vector2.Zero, Projectile.scale, SpriteEffects.FlipVertically, 0);;
    }

		public sealed class VulcanoArrow : ModProjectile
		{
			public sealed override string Texture => "CascadeMod/Assets/Weapons/Ranged/VulcanoArrow";
			public override void SetStaticDefaults()
			{
				ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
				ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
			}
			public override void SetDefaults()
			{
				Projectile.width = 26;
				Projectile.height = 22;
				Projectile.hostile = false;
				Projectile.friendly = true;
				Projectile.aiStyle = 1;
				AIType = ProjectileID.WoodenArrowFriendly;
				Projectile.ignoreWater = false; // because it's a fire arrow
				Projectile.tileCollide = true;
				Projectile.penetrate = 5;
			}

			public override void AI()
			{
				Lighting.AddLight(Projectile.Center, new Vector3(1, 0.25f, 0.1f));
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1), 0, default, 0.8f);
				Main.dust[dust].velocity *= 1.5f;
				Main.dust[dust].noGravity = true;
			}
			public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
			{
				target.AddBuff(BuffID.OnFire, 300); // 5 seconds
			}
			public override bool PreDraw(ref Color lightColor)
			{
				Main.instance.LoadProjectile(Projectile.type);
				Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

				Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < Projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
				}

				return true;
			}
			public override void Kill(int timeLeft)
			{
				SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
				FadingParticle flameParticle = ParticleOrchestrator._poolFading.RequestParticle();
				flameParticle.SetBasicInfo(TextureAssets.Projectile[540], new Rectangle?(), Vector2.Zero, Projectile.Center + (Projectile.velocity.SafeNormalize(Vector2.Zero) * 10));
				flameParticle.SetTypeInfo(10);
				flameParticle.Rotation = Projectile.rotation + 1.5708f;
				flameParticle.FadeInNormalizedTime = 0.0025f;
				flameParticle.FadeOutNormalizedTime = 0.125f;
				flameParticle.ScaleAcceleration = Vector2.One * -0.01666667f / 2;
				flameParticle.Scale = new Vector2(0.2f, 0.2f);
				flameParticle.ColorTint = new Color(1, 0.25f, 0.15f);
				flameParticle.ScaleVelocity = new Vector2(0.3f, 0.3f);
				flameParticle._texture = TextureAssets.Projectile[540];
				Main.ParticleSystem_World_OverPlayers.Add(flameParticle);
			}
			public override bool OnTileCollide(Vector2 oldVelocity)
			{
				for (int i = 0; i < 5; i++)
				{
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 0, default, 0.8f);
					Main.dust[dust].noGravity = true;
				}
				SoundEngine.PlaySound(SoundID.DD2_GoblinBomb, Projectile.position);
				FadingParticle flameParticle = ParticleOrchestrator._poolFading.RequestParticle();
				flameParticle.SetBasicInfo(TextureAssets.Projectile[540], new Rectangle?(), Vector2.Zero, Projectile.Center + (Projectile.velocity.SafeNormalize(Vector2.Zero) * 10));
				flameParticle.SetTypeInfo(10);
				flameParticle.Rotation = Projectile.rotation + 1.5708f;
				flameParticle.FadeInNormalizedTime = 0.0025f;
				flameParticle.FadeOutNormalizedTime = 0.125f;
				flameParticle.ScaleAcceleration = Vector2.One * -0.01666667f / 2;
				flameParticle.Scale = new Vector2(0.2f, 0.2f);
				flameParticle.ColorTint = new Color(1, 0.25f, 0.15f);
				flameParticle.ScaleVelocity = new Vector2(0.3f, 0.3f);
				flameParticle._texture = TextureAssets.Projectile[540];
				Main.ParticleSystem_World_OverPlayers.Add(flameParticle);
				return true;

			}
		}
		public sealed class VulcanoHomingArrow : ModProjectile {
			public sealed override string Texture => "CascadeMod/Assets/Textures/Glow";
			public override void SetStaticDefaults() {
				ProjectileID.Sets.TrailCacheLength[Projectile.type] = 17;
				ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
			}

			public override void SetDefaults() {
				Projectile.width = 64;
				Projectile.height = 64;
				Projectile.aiStyle = 1;
				AIType = ProjectileID.WoodenArrowFriendly;
				Projectile.ignoreWater = false; // because it's a lava ball
				Projectile.tileCollide = true;
				Projectile.friendly = true;
				Projectile.scale = 0.5f;
				Projectile.hostile = false;
				Projectile.penetrate = 3;
			}

			public override void AI() 
			{

				Lighting.AddLight(Projectile.Center, new Vector3(1, 0.85f, 0.15f));
				Player owner = Main.player[Projectile.owner];
				if (Main.rand.Next(1, 4) == 3) {
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 0, default, 0.8f);
					Main.dust[dust].velocity *= 1.5f;
					Main.dust[dust].noGravity = true;
				}
				SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC target);
				// code from flora gale :)
				if (foundTarget) {
					float moveSpeed = 11f;
					float velMultiplier = 1f;
					var dist = targetCenter - Projectile.Center;
					float length = dist == Vector2.Zero ? 0f : dist.Length();
					if (length < moveSpeed) {
						velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
					}
					Vector2 changevelocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
					changevelocity *= moveSpeed;
					changevelocity *= velMultiplier;
					Projectile.velocity = (changevelocity + (Projectile.velocity * 9)) / 10;
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
			public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
				SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.position); ;
				FadingParticle flameParticle = ParticleOrchestrator._poolFading.RequestParticle();
				flameParticle.SetBasicInfo(TextureAssets.Projectile[540], new Rectangle?(), Vector2.Zero, Projectile.Center);
				flameParticle.SetTypeInfo(10);
				flameParticle.FadeInNormalizedTime = 0.005f;
				flameParticle.FadeOutNormalizedTime = 0.1f;
				flameParticle.ScaleAcceleration = Vector2.One * -0.01666667f / 2;
				flameParticle.Scale = new Vector2(0.05f, 0.05f);
				flameParticle.ColorTint = new Color(1, 0.35f, 0.15f);
				flameParticle.ScaleVelocity = new Vector2(0.3f, 0.3f);
				flameParticle._texture = TextureAssets.Projectile[540];
				Main.ParticleSystem_World_OverPlayers.Add(flameParticle);
				target.AddBuff(BuffID.OnFire, 420);
				target.AddBuff(BuffID.OnFire3, 300);
				target.AddBuff(BuffID.Burning, 120);
			}
			public override bool OnTileCollide(Vector2 oldVelocity) {
				for (int i = 0; i < 5; i++) {
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 0, default, 0.8f);
					Main.dust[dust].noGravity = true;
				}
				SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.position);
				FadingParticle flameParticle = ParticleOrchestrator._poolFading.RequestParticle();
				flameParticle.SetBasicInfo(TextureAssets.Projectile[540], new Rectangle?(), Vector2.Zero, Projectile.Center);
				flameParticle.SetTypeInfo(10);
				flameParticle.FadeInNormalizedTime = 0.0005f;
				flameParticle.FadeOutNormalizedTime = 0.05f;
				flameParticle.ScaleAcceleration = Vector2.One * -0.01666667f / 2; 
				flameParticle.Scale = new Vector2(0.05f, 0.05f);
				flameParticle.ColorTint = new Color(1, 0.55f, 0.15f);
				flameParticle.ScaleVelocity = new Vector2(0.3f, 0.3f);
				flameParticle._texture = TextureAssets.Projectile[540];
				Main.ParticleSystem_World_OverPlayers.Add(flameParticle);
				return true;
			}

			public override bool PreDraw(ref Color lightColor) {

				for (int k = 0; k < Projectile.oldPos.Length; k++) {
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition;
					Color color = new Color(1, 0.85f / (k / 3.7f), 0.15f) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, 0, Vector2.Zero, Projectile.scale, SpriteEffects.None, 0);

				}
				return false;
			}
			public override void PostDraw(Color lightColor) {
				Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center, null, new Color(1, 0.85f, 0.15f), 0, Vector2.Zero, Projectile.scale + 0.5f, SpriteEffects.None, 0);
			}
		}
	}
