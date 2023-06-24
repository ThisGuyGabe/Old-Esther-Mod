using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items;

public class ArcaneLens : ModItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults() {
		Item.width = 24;
		Item.height = 30;
		Item.rare = ItemRarityID.Quest;
		Item.maxStack = 999;
	}
}
