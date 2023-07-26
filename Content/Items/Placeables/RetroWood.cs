using CascadeMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Placeables;

public sealed class RetroWood : ModItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults() {
		Item.DefaultToPlaceableTile(ModContent.TileType<RetroWoodTile>());
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient<PixilWood>()
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}