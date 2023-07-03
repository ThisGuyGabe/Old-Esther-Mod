using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using EstherMod.Content.Projectiles;
using EstherMod.Core;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class SoulSlaughter : BaseItem {
	public override void SetDefaults() {
		Item.width = 32;
		Item.height = 22;
		Item.useStyle = ItemUseStyleID.Rapier;
		Item.useTime = 3;
		Item.useAnimation = 3;
		Item.damage = 17;
		Item.autoReuse = true;
		Item.shoot = ModContent.ProjectileType<SoulSlaughter_Projectile>();
		Item.shootSpeed = 0f;
		Item.DamageType = DamageClass.Melee;
		Item.knockBack = 5;
		Item.noUseGraphic = true;
		Item.value = Item.sellPrice(gold: 2, silver: 86);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item1;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<SoulPebble>(), 14)
			.AddIngredient(ModContent.ItemType<SoulPearl>(), 1)
			.AddTile(TileID.Anvils)
			.Register();
	}
}