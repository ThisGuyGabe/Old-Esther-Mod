using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged
{

	public class CopperPopper : ModItem
	{
		public float bulletSpread = 25f; // The spread the bullets has when fired(adds the number affects both degrees up and down).
		public override void SetDefaults()
		{
			Item.width = 52;
			Item.height = 14;

			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 36;
			Item.useAnimation = 36;

			Item.autoReuse = true;
			Item.noMelee = true;

			Item.UseSound = SoundID.Item36;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(gold: 1, silver: 40);

			Item.DamageType = DamageClass.Ranged;
			Item.damage = 16;
			Item.knockBack = 8;

			Item.shoot = 1;
			Item.shootSpeed = 7.5f;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const int NumProjectiles = 5; // Amount of projectiles fired

			// Creates a cone angle that comes from the float variable where the player is looking.
			for (int i = 0; i < NumProjectiles; i++)
			{
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(bulletSpread));

				// Slows some bullets down for better visuals
				newVelocity *= 1f - Main.rand.NextFloat(0.3f);

				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI); // Spawns the projectiles.
			}

			return false;  // Prevents the game from firing the bullet from the SetDefaults hook.
		}

		public override Vector2? HoldoutOffset()
		{ // So you are not holding on the buttstock
			return new Vector2(2f, -2f);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{ // Makes it appear out the barrel.
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

			if (Collision.CanHit(position, 5, 0, position + muzzleOffset, 5, 0))
			{
				position += muzzleOffset;
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Boomstick, 1);
			recipe.AddIngredient(ItemID.CopperBar, 30);
			recipe.AddIngredient(ItemID.ShadowScale, 15);
			recipe.AddIngredient(ItemID.IllegalGunParts, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Boomstick, 1);
			recipe.AddIngredient(ItemID.CopperBar, 30);
			recipe.AddIngredient(ItemID.TissueSample, 15); // Tissue Sample
			recipe.AddIngredient(ItemID.IllegalGunParts, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}