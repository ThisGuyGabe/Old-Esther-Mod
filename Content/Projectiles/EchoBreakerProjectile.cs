using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles
{
    public class EchoBreakerProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.light = 0.1f;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.scale = 1.5f;
        }

        public override bool? CanHitNPC(NPC target)
        {
            Player player = Main.player[Projectile.owner];
            return !target.friendly && Projectile.localNPCImmunity[target.whoAmI] == 0 && player.CanHit(target);
        }

        private static float ItemRptToProjRot(float itemrotation, float playerdirection)
        {
            if (playerdirection > 0)
            {
                return itemrotation;
            }
            itemrotation = ((!(itemrotation >= 0f)) ? (-(float)Math.PI - itemrotation) : ((float)Math.PI - itemrotation));
            return 0f - itemrotation;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 unit = Utils.ToRotationVector2(ItemRptToProjRot(player.itemRotation, player.direction*1.55f));
            Projectile.Center = player.Center + unit * 85f * Projectile.scale;
            if (player.itemAnimation == 1)
                Projectile.Kill();
            if (player.itemAnimation > Projectile.ai[0] / 2f)
                Projectile.ai[1] += 1f;
            else
                Projectile.ai[1] -= 1f;

        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            int dir = player.direction > 0 ? 1 : -1;
            SpriteBatch spriteBatch = Main.spriteBatch;
            SpriteEffects effects = player.gravDir == -1f ? (player.direction > 0 ? SpriteEffects.FlipVertically : SpriteEffects.None) : (player.direction <= 0 ? SpriteEffects.FlipVertically : SpriteEffects.None);
            
            Texture2D slash = ModContent.Request<Texture2D>("EstherMod/Content/Projectiles/EchoBreakerProjectile", AssetRequestMode.AsyncLoad).Value;
            
            Rectangle rect = new Rectangle(0, 0, slash.Width, slash.Height);
            Vector2 vector = new Vector2(slash.Width / 2f, slash.Height / 2f);

            float top4RotationFloats = ItemRptToProjRot(player.itemRotation, player.direction);
            float numeroCuatro = top4RotationFloats - dir * 1.1f;
            float numeroTres = top4RotationFloats - dir * MathHelper.PiOver2;

            

            spriteBatch.Draw(slash, player.Center - Main.screenPosition, rect, lightColor, numeroCuatro, vector, Projectile.scale * 1.2f, effects, 0f);
            spriteBatch.Draw(slash, player.Center - Main.screenPosition, rect, lightColor, numeroTres, vector, Projectile.scale * 1.2f, effects, 0f);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            float rotation = ItemRptToProjRot(player.itemRotation, player.direction);

            int dir = player.direction > 0 ? 1 : -1;
            int gravDir = player.gravDir > 0 ? 1 : -1;

            Vector2 unit = Utils.ToRotationVector2(rotation - (dir * gravDir) * 1.6f);
            Vector2 pos = player.Center + unit * 85f * Projectile.scale;

            float collisionPoint = 0f; // Don't need that variable, but required as parameter

            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, Projectile.Center, 30f, ref collisionPoint) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, pos, 30f, ref collisionPoint);
        }
    }
}
