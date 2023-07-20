using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using CascadeMod.Content.Items.Weapons.Magic;
using System;
using Terraria.Graphics.Renderers;

namespace CascadeMod.Content.Projectiles
{
    public class Starlight : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
            Projectile.alpha = 0;
            Projectile.light = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1f;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.channel && owner.HeldItem.type == ModContent.ItemType<StarStaff>())
            {

                Projectile.ai[0]++;
                Projectile.timeLeft = 2;
                Lighting.AddLight(Projectile.Center, 1, 1, 0.25f + ((float)Math.Cos(Projectile.ai[0] * 3) * 0.25f));
                Projectile.scale = 0.5f + ((float)Math.Cos(Projectile.ai[0] * 3) * 0.5f);
                Projectile.Center = Main.MouseWorld;
                Projectile.velocity = Vector2.Zero;

                if (Main.rand.Next(1, 20) == 2)
                {
                    Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, 17, Main.rand.NextFloat(0.25f, 1));
                }
                if (Main.rand.Next(1, 20) == 2)
                {
                    Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, 16, Main.rand.NextFloat(0.25f, 1));
                }

                if (Main.netMode != NetmodeID.MultiplayerClient && Projectile.ai[0] % 50 == 25)
                {
                    if (owner.statMana < owner.HeldItem.mana)
                    {
                        Projectile.Kill();
                        SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, owner.Center.Y - (Main.screenHeight / 2)), new Vector2(0, 22.05f), ProjectileID.StarCannonStar, Projectile.damage, 1);
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, owner.Center.Y - (Main.screenHeight / 2)), new Vector2(0, 22.05f), ProjectileID.StarCannonStar, Projectile.damage, 1);

                    }
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] > 15)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY) + Projectile.Center;
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
                return true;
            }
            return false;
        }
    }
}