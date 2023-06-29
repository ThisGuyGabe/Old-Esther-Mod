using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria.DataStructures;
using EstherMod.Core;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent;

namespace EstherMod.Content.Items.Weapons.Melee
{
	public sealed class Needle : ModItem
	{
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.width = 52;
            Item.height = 52;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 0;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<NeedleProj>();
            Item.shootSpeed = 30f;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.channel = true;
            Item.value = Item.sellPrice(silver: 45);
            Item.rare = ItemRarityID.Blue;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<NeedleProj>());
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item156, player.Center);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
	}

	public sealed class NeedleProj : ModProjectile {
		private Player Owner => Main.player[Projectile.owner];
		private bool Retracting => Projectile.timeLeft < 40;

		private Vector2 startPos;
		private readonly List<NPC> alreadyHit = new();

		public override void SetDefaults() {
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.tileCollide = true;
			Projectile.friendly = true;
			Projectile.timeLeft = 80;
			Projectile.penetrate = -1;
		}

		public override void AI() {
			Owner.heldProj = Projectile.whoAmI;
			Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Owner.DirectionTo(Projectile.Center).ToRotation() - 1.57f);
			Owner.itemAnimation = Owner.itemTime = 2;
			Owner.direction = Math.Sign(Owner.DirectionTo(Main.MouseWorld).X);
			Projectile.rotation = Projectile.DirectionFrom(Owner.Center).ToRotation() + 0.78f;

			if (Projectile.timeLeft == 40) {
				alreadyHit.Clear();
				startPos = Projectile.Center;
			}

			if (Retracting) {
				Projectile.extraUpdates = 1;
				Projectile.Center = Vector2.Lerp(Owner.Center, startPos, EaseFunction.EaseCircularOut.Ease(Projectile.timeLeft / 40f));
			}
			else {
				if (Projectile.timeLeft > 50) {
					Vector2 vel = Vector2.Normalize(-Projectile.velocity).RotatedByRandom(0.4f) * Main.rand.NextFloat(2, 5);
					Dust.NewDustPerfect(Projectile.Center + (vel * 4), ModContent.DustType<Content.Dusts.GlowLine>(), vel, 0, Color.White, 0.5f);
				}
				Owner.velocity = Vector2.Zero;
				Projectile.velocity *= 0.935f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			if (Projectile.timeLeft > 40)
				Projectile.timeLeft = 41;
			return false;
		}

		public override bool PreDraw(ref Color lightColor) {
			Texture2D texture = TextureAssets.Item[Type].Value;
			Texture2D chainTexture = ModContent.Request<Texture2D>(Texture + "_Chain").Value;

			Vector2 pointToDrawFrom = Projectile.Center + new Vector2(-texture.Width, texture.Height).RotatedBy(Projectile.rotation);

			Vector2 pointToDrawTo = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, Owner.DirectionTo(Projectile.Center).ToRotation() - 1.57f);
			float length = (pointToDrawFrom - pointToDrawTo).Length();
			if (length > chainTexture.Height * 3) {
				for (float i = 0; i < length; i += chainTexture.Height + 4) {
					Vector2 pointToDraw = Vector2.Lerp(pointToDrawFrom, pointToDrawTo, i / length);
					Color chainColor = Lighting.GetColor((int)(pointToDraw.X / 16), (int)(pointToDraw.Y / 16));
					Main.spriteBatch.Draw(chainTexture, pointToDraw - Main.screenPosition, null, chainColor, pointToDrawFrom.DirectionFrom(Owner.Center).ToRotation() + 1.57f, chainTexture.Size() / 2, Projectile.scale, SpriteEffects.None, 0f);
				}
			}

			Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, new Vector2(texture.Width, 0), Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override bool? CanHitNPC(NPC target) {
			if (alreadyHit.Contains(target))
				return false;
			return null;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			alreadyHit.Add(target);
		}
	}
}
