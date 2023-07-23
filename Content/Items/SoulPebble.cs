using CascadeMod.Core;
using Terraria;
using Terraria.ID;

namespace CascadeMod.Content.Items;

public sealed class SoulPebble : BaseItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults() {
		Item.width = 22;
		Item.height = 22;
		Item.rare = ItemRarityID.Green;
		Item.maxStack = Item.CommonMaxStack;
		Item.value = Item.sellPrice(silver: 6);
	}
}
