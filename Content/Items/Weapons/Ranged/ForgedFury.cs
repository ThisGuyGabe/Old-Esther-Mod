using CascadeMod.Content.Projectiles;
using CascadeMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Weapons.Ranged;

public sealed class ForgedFury : BaseItem {
	public override void SetDefaults() {
		Item.damage = 36;
		Item.rare = ItemRarityID.Orange;

		Item.width = 20;
		Item.height = 52;
		Item.useAnimation = 24;
		Item.useTime = 24;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 2.5f;
		Item.shootSpeed = 12f;
		Item.DamageType = DamageClass.Ranged;
		Item.noMelee = true;
		Item.value = Item.sellPrice(gold: 1, silver: 95);
		Item.UseSound = SoundID.Item5;
		Item.shoot = ProjectileID.VilePowder;
		Item.useAmmo = AmmoID.Arrow;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		if (type == ProjectileID.WoodenArrowFriendly) {
			type = ProjectileID.FireArrow; // Turns wooden Arrows into fire arrows
		}

		if (type == ProjectileID.FireArrow) {
			type = ModContent.ProjectileType<ForgedFuryProjectile>(); // Turns flaming arrows into fury arrows.
		}
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.ShadewoodBow)
			.AddIngredient(ItemID.Obsidian, 15)
			.AddIngredient(ItemID.HellstoneBar, 15)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.EbonwoodBow)
			.AddIngredient(ItemID.Obsidian, 15)
			.AddIngredient(ItemID.HellstoneBar, 15)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
