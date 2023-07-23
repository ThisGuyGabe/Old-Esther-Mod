using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace CascadeMod.Content.Projectiles
{
    public class Onomatopoeia : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 96;
            Projectile.height = 42;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.alpha = 255;
            Projectile.light = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1f;
        }
        public override void AI()
        {
            if (Projectile.timeLeft >= 25)
            {
                Projectile.alpha -= 51;
            }
            Projectile.light -= 0.03333333333f;
            Projectile.velocity.Y *= 0.90f;
            if (Projectile.timeLeft <= 5)
            {
                Projectile.alpha += 51;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return true;
        }
    }
}
