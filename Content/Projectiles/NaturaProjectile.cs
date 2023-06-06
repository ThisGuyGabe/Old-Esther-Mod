using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace EstherMod.Content.Projectiles
{
    public class NaturaProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Natura Projectile");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6; // how long trail is
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 150;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 20f;
        }

        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 99;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 6; ++i)
            {
                Vector2 vec = new Vector2(-5, 0f).RotatedByRandom(MathHelper.ToRadians(Main.rand.Next(520)));
                int gabe = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 6f, Projectile.position.Y + 4f, vec.X, vec.Y, ProjectileID.HornetStinger, Projectile.damage / 2, 6f, Projectile.owner);
                Main.projectile[gabe].DamageType = DamageClass.Melee;
            }
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
            return true;
        }
    }
}