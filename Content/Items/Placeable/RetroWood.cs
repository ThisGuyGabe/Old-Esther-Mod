using Terraria.ModLoader;
using EstherMod.Content.Tiles;
using Terraria.ID;
using Terraria;

namespace EstherMod.Content.Items.Placeable;

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
			.AddTile(TileID.DemonAltar)
			.Register();
	}
}