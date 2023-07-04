using EstherMod.Content.Tiles;
using EstherMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Placeables;

public sealed class RetroWood : BaseItem {
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