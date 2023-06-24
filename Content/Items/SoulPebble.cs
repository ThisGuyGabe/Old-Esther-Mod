using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items;

public class SoulPebble : ModItem {
	public override void SetDefaults() {
		Item.width = 22;
		Item.height = 22;
		Item.rare = ItemRarityID.Green;
		Item.maxStack = 999;
		Item.value = Item.sellPrice(silver: 6);
	}

	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 25;
	}
}
