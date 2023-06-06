using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ParticleLibrary;
using EstherMod.Particles;
using Terraria.DataStructures;
using System;
using Terraria.GameContent;

namespace EstherMod.Content.Projectiles
{
	public class EmberFlamethrowerProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{

        }

        public override void SetDefaults()
        {
            Projectile.width = 256;
            Projectile.height = 256;
            Projectile.scale = 0.5f;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 60;
            Projectile.extraUpdates = 2;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            Projectile.localAI[0] = Main.rand.NextFloat(-0.1f, 0.1f);
            Projectile.spriteDirection = Main.rand.NextBool() ? 1 : -1;

            Projectile.timeLeft = (int)Projectile.ai[1];

            Projectile.localAI[1] = 1f;

            if (Projectile.ai[1] > 60f)
            {
                Projectile.usesIDStaticNPCImmunity = false;
                Projectile.usesLocalNPCImmunity = true;
                Projectile.localNPCHitCooldown = 10;

                Projectile.tileCollide = false;
            }
        }
        public override void AI()
        {
            // ParticleManager.NewParticle(Projectile.position, Vector2.Zero, new EmberParticle(), default, 0f);
            if (Projectile.ai[1] > 60f)
            {
                //decelerate if long-lasting
                Projectile.velocity *= 0.98f;
                Projectile.localAI[0] *= 0.98f;
            }


            Vector2 oldCenter = Projectile.Center;

            if (Projectile.ai[1] <= 60f)
                Projectile.scale += 0.45f / Projectile.ai[1];
            else
                Projectile.scale = 0.5f;

            Projectile.width = (int)(256 * Projectile.scale);
            Projectile.height = (int)(256 * Projectile.scale);
            Projectile.Center = oldCenter;

            Projectile.rotation += Projectile.localAI[0];

            if (Projectile.timeLeft < Projectile.ai[1] / 2f)
            {
                Projectile.alpha = (int)(255 * (1 - (Projectile.timeLeft / (Projectile.ai[1] / 2f))));
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 2;
            height = 2;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = Color.Lerp(Color.Red, Color.Purple, (float)(Math.Sin(Projectile.ai[0]) + 1) * 0.5f).MultiplyRGB(Lighting.GetColor(Projectile.Center.ToTileCoordinates())) * (1 - Projectile.alpha / 255f);
            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, TextureAssets.Projectile[Type].Frame(), color, Projectile.rotation, TextureAssets.Projectile[Type].Size() / 2, Projectile.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

            return false;
        }
    }
}
