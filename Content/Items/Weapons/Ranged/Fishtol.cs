erusing Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace EstherMod.Content.Items.Weapons.Ranged
{
    public class Fishtol : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 38;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 1800;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item61;
            Item.shoot = ModContent.ProjectileType<FishProjectile>();
            Item.shootSpeed = 9.5f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source,position,velocity.RotatedByRandom(MathHelper.ToRadians(7)),type,damage,knockback,player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Toilet, 1);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
            recipe.AddIngredient(ItemID.Goldfish, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

    public class FishProjectile : ModProjectile
    {
        public override string Texture => "EstherMod/Assets/Weapons/Ranged/FishProjectile";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 300;
            Projectile.alpha = 254;
            Projectile.penetrate = 2;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.CountsAsClass(DamageClass.Ranged);
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.frame = Main.rand.Next(0, 3);
            }
            if (Projectile.ai[0] > 8 && Projectile.alpha != 0)
            {
                Projectile.alpha -= 51;
            }
            Projectile.ai[0]++;
            if (Projectile.timeLeft <= 270)
            {
                Projectile.velocity.Y += 0.15f;
                Projectile.velocity.X *= 0.99f;
            }
            else
            {
                if (Main.rand.NextBool(1,5))
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Water, Projectile.velocity.X / 2, Projectile.velocity.Y - 2);
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position, 10, 16, DustID.Water, Projectile.velocity.X * -1 + Main.rand.Next(-3, 3), Projectile.velocity.Y * -1 + Main.rand.Next(-3, 3));
            }
            return true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D orangefish = (Texture2D)ModContent.Request<Texture2D>("EstherMod/Assets/Weapons/Ranged/OrangeFish");
            Texture2D greenfish = (Texture2D)ModContent.Request<Texture2D>("EstherMod/Assets/Weapons/Ranged/GreenFish");
            Texture2D purplefish = (Texture2D)ModContent.Request<Texture2D>("EstherMod/Assets/Weapons/Ranged/PurpleFish");


            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                if (Projectile.frame == 0)
                {
                    Main.spriteBatch.Draw(orangefish, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
                else
                {
                    if (Projectile.frame == 1)
                    {
                        Main.spriteBatch.Draw(purplefish, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        Main.spriteBatch.Draw(greenfish, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                    }
                }
            }
            return true;
        }
    }
}
