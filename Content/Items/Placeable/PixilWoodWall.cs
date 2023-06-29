using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using EstherMod.Content.Walls;

namespace EstherMod.Content.Items.Placeable;

public sealed class PixilWoodWall : ModItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults() {
		Item.DefaultToPlaceableWall(ModContent.WallType<PixilWoodWallTile>());
	}

	public override void AddRecipes() {
		CreateRecipe(4)
			.AddIngredient<PixilWood>()
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
