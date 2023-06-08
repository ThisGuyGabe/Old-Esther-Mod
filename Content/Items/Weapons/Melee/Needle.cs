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

namespace EstherMod.Content.Items.Weapons.Melee
{
	public class Needle : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Throws out a needle that goes in a line and retreats. \nIf it's used in the air it lets the player hover.");
		}

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

    internal class NeedleProj : ModProjectile
    {
        Player owner => Main.player[Projectile.owner];

        private bool retracting => Projectile.timeLeft < 40;

        Vector2 startPos;

        private List<NPC> alreadyHit = new List<NPC>();

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.timeLeft = 80;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            owner.heldProj = Projectile.whoAmI;
            owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, owner.DirectionTo(Projectile.Center).ToRotation() - 1.57f);
            owner.itemAnimation = owner.itemTime = 2;
            owner.direction = Math.Sign(owner.DirectionTo(Main.MouseWorld).X);
            Projectile.rotation = Projectile.DirectionFrom(owner.Center).ToRotation() + 0.78f;
            if (Projectile.timeLeft == 40)
            {
                alreadyHit = new List<NPC>();
                startPos = Projectile.Center;
            }
            if (retracting)
            {
                Projectile.extraUpdates = 1;
                Projectile.Center = Vector2.Lerp(owner.Center, startPos, EaseFunction.EaseCircularOut.Ease(Projectile.timeLeft / 40f));
            }
            else
            {
                if (Projectile.timeLeft > 50)
                {
                    Vector2 vel = Vector2.Normalize(-Projectile.velocity).RotatedByRandom(0.4f) * Main.rand.NextFloat(2, 5);
                    Dust.NewDustPerfect(Projectile.Center + (vel * 4), ModContent.DustType<Content.Dusts.GlowLine>(), vel, 0, Color.White, 0.5f);
                }
                owner.velocity = Vector2.Zero;
                Projectile.velocity *= 0.935f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 40)
                Projectile.timeLeft = 41;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Texture2D chaintex = ModContent.Request<Texture2D>(Texture + "_Chain").Value;
            Vector2 origin = new Vector2(0, tex.Height);
            origin = new Vector2(tex.Width, 0);

            Vector2 pointToDrawFrom = Projectile.Center + new Vector2(-tex.Width, tex.Height).RotatedBy(Projectile.rotation);

            Vector2 pointToDrawTo = owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, owner.DirectionTo(Projectile.Center).ToRotation() - 1.57f);
            float length = (pointToDrawFrom - pointToDrawTo).Length();
            if (length > chaintex.Height * 3)
            {
                for (float i = 0; i < length; i += chaintex.Height + 4)
                {
                    Vector2 pointToDraw = Vector2.Lerp(pointToDrawFrom, pointToDrawTo, i / length);
                    Color chainColor = Lighting.GetColor((int)(pointToDraw.X / 16), (int)(pointToDraw.Y / 16));
                    Main.spriteBatch.Draw(chaintex, pointToDraw - Main.screenPosition, null, chainColor, pointToDrawFrom.DirectionFrom(owner.Center).ToRotation() + 1.57f, chaintex.Size() / 2, Projectile.scale, SpriteEffects.None, 0f);
                }
            }

            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (alreadyHit.Contains(target))
                return false;
            return base.CanHitNPC(target);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            alreadyHit.Add(target);
        }
    }
}
