using CascadeMod.Content.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Placeables;

public sealed class RetroWoodWall : ModItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults() {
		Item.DefaultToPlaceableWall(ModContent.WallType<RetroWoodWallTile>());
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient<PixilWoodWall>()
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
