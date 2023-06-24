using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace EstherMod.Content.Items;

public class ImpHorn : ModItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults() {
		Item.width = 10;
		Item.height = 18;
		Item.rare = ItemRarityID.Quest;
		Item.maxStack = 999;
	}
}
