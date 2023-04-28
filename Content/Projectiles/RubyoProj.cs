using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;

namespace EstherMod.Content.Projectiles
{
    public class RubyoProj : ModProjectile
    {
        const int ShootRate = 60;
        const float ShootDistance = 500f;
        const float ShootSpeed = 10f;
        const int ShootDamage = 38;
        const float ShootKnockback = 4f;
        int ShootType = -1;
        int TimeToShoot = ShootRate;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rubyo Proj");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4; // how long trail is
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 100;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 200;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15f;
        }

        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.width = 24;
            Projectile.height = 32;
            Projectile.aiStyle = 99;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
        }

        public static Vector2 VelocityToPoint(Vector2 A, Vector2 B, float Speed)
        {
            var Move = B - A;
            return Move * (Speed / (float)Math.Sqrt(Move.X * Move.X + Move.Y * Move.Y));
        }
        public override void AI()
        {
            if (--TimeToShoot <= 0)
            {
                TimeToShoot = ShootRate;
                if (ShootType == -1)
                    ShootType = Mod.Find<ModProjectile>("Ruby").Type;

                float NearestNPCDist = ShootDistance;
                int NearestNPC = -1;
                foreach (NPC npc in Main.npc)
                {
                    if (!npc.active)
                        continue;
                    if (npc.friendly || npc.lifeMax <= 5)
                        continue;
                    if (NearestNPCDist == -1 || npc.Distance(Projectile.Center) < NearestNPCDist && Collision.CanHitLine(Projectile.Center, 16, 16, npc.Center, 16, 16))
                    {
                        NearestNPCDist = npc.Distance(Projectile.Center);
                        NearestNPC = npc.whoAmI;
                    }
                }
                if (NearestNPC == -1)
                    return;
                Vector2 Velocity = VelocityToPoint(Projectile.Center, Main.npc[NearestNPC].Center, ShootSpeed);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, Velocity.X, Velocity.Y, ShootType, ShootDamage, ShootKnockback, Projectile.owner);
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
