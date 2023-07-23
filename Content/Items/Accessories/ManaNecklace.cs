using CascadeMod.Core;
using Terraria;
using Terraria.ID;

namespace CascadeMod.Content.Items.Accessories;

public sealed class ManaNecklace : BaseItem {
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
		player.manaFlower = true;
		player.manaCost -= 0.08f;

		player.statManaMax2 += 20;
		player.manaRegenDelayBonus += 1f;
		player.manaRegenBonus += 25;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.ManaFlower)
			.AddIngredient(ItemID.ManaRegenerationBand)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
