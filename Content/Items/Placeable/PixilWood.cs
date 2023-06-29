using Terraria.ModLoader;
using EstherMod.Content.Tiles;
using Terraria.ID;
using Terraria;

namespace EstherMod.Content.Items.Placeable;

public sealed class PixilWood : ModItem {
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