using EstherMod.Content.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Placeable;

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
			.AddTile(TileID.DemonAltar)
			.Register();
	}
}
