using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items;

public sealed class SoulPebble : ModItem {
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
