using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EstherMod.Content.Items;

public class EthericScrap : ModItem {
	public override void SetDefaults() {
		Item.width = 22;
		Item.height = 32;
		Item.value = Item.sellPrice(silver: 8);
		Item.rare = ItemRarityID.Blue;
		Item.maxStack = 9999;
	}
}
