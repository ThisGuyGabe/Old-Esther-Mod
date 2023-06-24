using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EstherMod.Content.Items;

public class EthericScrap : ModItem {
	public override void SetDefaults() {
		Item.width = 16;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 8);
		Item.rare = ItemRarityID.Blue;
		Item.maxStack = 9999;
	}
}
