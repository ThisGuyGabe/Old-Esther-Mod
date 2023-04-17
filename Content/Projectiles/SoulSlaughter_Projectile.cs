using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Projectiles
{
    public class SoulSlaughter_Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Slaughter");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.alpha = 0;
            Projectile.light = 0.25f;
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
                Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale - k / 10, SpriteEffects.None, 0f);
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
        private Vector2 MousePos;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 240);
            target.AddBuff(BuffID.Frostburn2, 240);
            target.immune[Projectile.owner] = 3;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {
                MousePos = Main.MouseWorld;
            }
            Projectile.knockBack = 2;
            Projectile.ai[0]++;
            Projectile.ai[1] -= 0.025f;
            Projectile.Center = player.Center + new Vector2(55, 0).RotatedBy(player.Center.DirectionTo(MousePos).ToRotation() + (Projectile.ai[0] / (Projectile.ai[1] + 1)));
            Projectile.rotation = player.Center.DirectionTo(MousePos).ToRotation() + 0.785398f + (Projectile.ai[0] / 100);
            if (Main.rand.Next(1, 10) == 1)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, Projectile.velocity.X, Projectile.velocity.Y);
            }
            Lighting.AddLight(Projectile.position, 0.00f, 0.75f, 0.95f);
            if (Projectile.ai[0] % 5 == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, (Projectile.rotation - 0.785398f).ToRotationVector2() * 10, ModContent.ProjectileType<SoulSlaughterHighlight>(), Projectile.damage / 2, 2, player.whoAmI);

            }
        }
    }
}