using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons
{
	public class SurgeCannon : ModItem
    {
        public float bulletSpread = 12.5f; // The spread the bullets has when fired(adds the number affects both degrees up and down).

    	public override void SetStaticDefaults() {
        
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override Vector2? HoldoutOffset() { 
            return new Vector2(5f, 5.5f);
        }

        public override void SetDefaults() {
            Item.width = 72;
            Item.height = 28;

            Item.useTime = 52;
            Item.useAnimation = 52;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.UseSound = SoundID.Item36;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 3, silver: 42);

            Item.damage = 62;
            Item.crit = 3;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7.5f;

            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = Mod.Find<ModProjectile>("SurgeCannonProjectile").Type;
            Item.shootSpeed = 12.5f;

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            const int NumProjectiles = 6; // Amount of projectiles fired

            // Creates a cone angle that comes from the float variable where the player is looking.
            for (int i = 0; i < NumProjectiles; i++) {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(bulletSpread));

                // Slows some bullets down for better visuals
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI); // Spawns the projectiles.
            }

            return false;  // Prevents the game from firing the bullet from the SetDefaults hook.
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
                type = ModContent.ProjectileType<Content.Projectiles.SurgeCannonProjectile>();
        }
    }

}