using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using EstherMod.Content.Items.Weapons.Magic;
using System;

namespace EstherMod.Content.Projectiles
{
    public class Starlight : ModProjectile {
		public override string Texture => "EstherMod/Assets/Textures/EmptyPixel";
		public override void SetStaticDefaults()
        {
			
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 72;
            Projectile.height = 72;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
            Projectile.alpha = 255;
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
				Lighting.AddLight(owner.Center, 1, 0.95f, 0.25f + ((float)Math.Cos(Projectile.ai[0] * 3) * 0.25f));
				Lighting.AddLight(Projectile.Center, 1, 0.95f, 0.25f + ((float)Math.Cos(Projectile.ai[0] * 3) * 0.25f));
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
				if  (Projectile.ai[0] % 50 == 25)
                {
                    if (owner.statMana < owner.HeldItem.mana)
                    {
						Projectile.Kill();
                        SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, Projectile.Center);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, owner.Center.Y - (Main.screenHeight / 2)), new Vector2(0, 22.05f), ProjectileID.StarCannonStar, Projectile.damage, 1);
					}
                    else
                    {
                        SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, Projectile.Center);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, owner.Center.Y - (Main.screenHeight / 2)), new Vector2(0, 22.05f), ProjectileID.StarCannonStar, Projectile.damage, 1);
					}
                }
            }
        }
		public override bool PreDraw(ref Color lightColor) {

			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition; 
				Color color = new Color(1, 0.95f, 0.36f + ((float)Math.Cos((Projectile.ai[0] - k) * 3) * 0.35f)) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(TextureAssets.Projectile[540].Value, drawPos - new Vector2((Projectile.scale - 1) * 36, (Projectile.scale - 1) * 36), null, color, 0, Vector2.Zero, Projectile.scale, SpriteEffects.None, 0);

			}
			return false;
		}
		public override void PostDraw(Color lightColor) {
			Main.EntitySpriteDraw(TextureAssets.Projectile[540].Value, Projectile.Center + new Vector2(-Projectile.scale, -Projectile.scale), null, new Color(0.1f, 0.25f + ((float)Math.Cos((Projectile.ai[0]) * 3) * 0.25f), 0.95f), 0, Vector2.Zero, Projectile.scale, SpriteEffects.None, 0);
		}
	}
}

