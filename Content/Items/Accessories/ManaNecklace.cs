using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Accessories;

public sealed class ManaNecklace : ModItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 1;
	}

	public override void SetDefaults() {
		Item.width = 34;
		Item.height = 38;
		Item.rare = ItemRarityID.Green;

		Item.accessory = true;
		Item.value = Item.sellPrice(gold: 1, silver: 30);
	}

	public override void UpdateAccessory(Player player, bool hideVisual) {
		player.manaCost -= 0.08f;
		player.statManaMax2 += 20;
		player.manaFlower = true;
		player.manaRegen += 5;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.ManaFlower, 1)
			.AddIngredient(ItemID.ManaRegenerationBand, 1)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
