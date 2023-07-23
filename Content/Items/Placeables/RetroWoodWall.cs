using CascadeMod.Content.Walls;
using CascadeMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Placeables;

public sealed class RetroWoodWall : BaseItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults() {
		Item.DefaultToPlaceableWall(ModContent.WallType<RetroWoodWallTile>());
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient<PixilWoodWall>()
			.AddTile(TileID.DemonAltar)
			.Register();
	}
}
