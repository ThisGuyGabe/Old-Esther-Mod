using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using CascadeMod.Content.Dusts;

namespace CascadeMod.Content.esther
{
    public class GoldenReckageProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 45;
            Projectile.alpha = 0;
            Projectile.light = 0.7f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin2 = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin2 + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin2, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
        private float mouseDirection;
        private Vector2 oldLocation;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {
                mouseDirection = player.Center.DirectionTo(Main.MouseWorld).ToRotation();
                oldLocation = player.Center;
            }
            Projectile.knockBack = 2;
            Projectile.velocity = new Vector2(0, 0);

            Projectile.ai[0]++;
            Projectile.rotation = mouseDirection + 0.785398f;
            Projectile.Center = player.Center - (player.Center - oldLocation) + new Vector2(Projectile.ai[0] * 10 + Projectile.ai[0] * (Projectile.ai[0] * 0.025f), 0).RotatedBy(Projectile.rotation - 0.785398f);
            if (Projectile.ai[0] % 15 == 0)
            {
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, (Projectile.rotation + 0.785398f).ToRotationVector2() * Projectile.ai[0] / 3, ProjectileID.HarpyFeather, Projectile.damage, Main.myPlayer);
                Main.projectile[p].hostile = false;
                Main.projectile[p].friendly = true;
                Main.projectile[p].damage = Projectile.damage / 3;
                int p2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, (Projectile.rotation - 0.785398f - 0.785398f - 0.785398f).ToRotationVector2() * Projectile.ai[0] / 3, ProjectileID.HarpyFeather, Projectile.damage, Main.myPlayer);
                Main.projectile[p].hostile = false;
                Main.projectile[p].friendly = true;
                Main.projectile[p].damage = Projectile.damage / 3;
            }
            if (Projectile.ai[0] % 20 == 0)
            {
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, (Projectile.rotation - 0.785398f).ToRotationVector2() * 10, ProjectileID.StarCannonStar, Projectile.damage, Main.myPlayer);
                Main.projectile[p].hostile = false;
                Main.projectile[p].friendly = true;
                Main.projectile[p].damage = Projectile.damage / 2;
            }
        }
    }
}