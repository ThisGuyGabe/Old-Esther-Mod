using CascadeMod.Content.Tiles;
using CascadeMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Placeables;

public sealed class PixilWood : BaseItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults() {
		Item.DefaultToPlaceableTile(ModContent.TileType<PixilWoodTile>());
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.RichMahogany)
			.AddIngredient(ItemID.Wood)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}