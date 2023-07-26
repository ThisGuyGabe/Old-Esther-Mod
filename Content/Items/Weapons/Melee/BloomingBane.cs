using CascadeMod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Weapons.Melee;

public sealed class BloomingBane : ModItem {
	public override void SetDefaults() {
		Item.width = 40;
		Item.height = 40;
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item1;
		Item.value = Item.sellPrice(gold: 1, silver: 40, copper: 60);

		Item.damage = 26;
		Item.knockBack = 5f;
		Item.DamageType = DamageClass.Melee;
		Item.useAnimation = 25;
		Item.useTime = 25;
		Item.noUseGraphic = true;
		Item.channel = true;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTurn = true;
		Item.shootSpeed = 15f;
		Item.shoot = ModContent.ProjectileType<BloomingBaneProjectile>();
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.JungleSpores, 5)
			.AddIngredient(ItemID.Stinger, 3)
			.AddIngredient(ItemID.Vine)
			.AddIngredient(ItemID.Terragrim)
			.AddTile(TileID.Anvils)
			.Register();
	}
}


