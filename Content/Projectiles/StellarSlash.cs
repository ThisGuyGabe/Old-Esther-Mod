using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace CascadeMod.Content.Projectiles
{
    public class StellarSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 154;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.alpha = 200;
            Projectile.light = 0.7f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            
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

        public override void AI()
        {
            if (Projectile.ai[0] % 5 == 2 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 6)
            {
                Dust.NewDust(Projectile.position, 80, 154, DustID.MagicMirror, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f, 125, Color.DarkSlateGray);
            }
            float lightLevel = -Projectile.timeLeft / 100 + 1;
            Lighting.AddLight(Projectile.Center, 0.34f * lightLevel, 0.78f * lightLevel, 0.99f * lightLevel);
            if (Projectile.ai[0] == 0)
            {
                Projectile.Center = Main.player[Projectile.owner].Center;
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 45) { Projectile.alpha += 3; }
            
            Projectile.velocity /= 1.05f;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}
