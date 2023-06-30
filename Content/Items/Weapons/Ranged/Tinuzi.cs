using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged;

public sealed class Tinuzi : BaseItem {
	public override void SetDefaults() {
		Item.width = 48;
		Item.height = 36;
		Item.scale = 0.70f;

		Item.useTime = 10;
		Item.useAnimation = 10;
		Item.useStyle = ItemUseStyleID.Shoot;

		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(gold: 1, silver: 40);

		Item.damage = 16;
		Item.DamageType = DamageClass.Ranged;
		Item.knockBack = 0.5f;

		Item.autoReuse = true;
		Item.noMelee = true;
		Item.UseSound = SoundID.Item11;

		Item.shoot = ProjectileID.WoodenArrowFriendly;
		Item.shootSpeed = 13.5f;
		Item.useAmmo = AmmoID.Bullet;
	}


	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		velocity = velocity.RotatedByRandom(MathHelper.ToRadians(7.5f));
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.TinBar, 40)
			.AddIngredient(ItemID.ShadowScale, 15)
			.AddIngredient(ItemID.IllegalGunParts, 1)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.TinBar, 40)
			.AddIngredient(ItemID.TissueSample, 15)
			.AddIngredient(ItemID.IllegalGunParts, 1)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
