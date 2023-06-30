using EstherMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Accessories;

public sealed class MagneticDynamo : BaseItem {
	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(12, 6);

	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 1;
	}

	public override void SetDefaults() {
		Item.width = 34;
		Item.height = 30;
		Item.rare = ItemRarityID.Green;

		Item.accessory = true;
		Item.value = Item.sellPrice(gold: 2, silver: 32);
	}

	public override void UpdateAccessory(Player player, bool hideVisual) {
		player.GetDamage(DamageClass.Generic) -= 0.06f;
		player.GetAttackSpeed(DamageClass.Generic) += 0.12f;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.Bone, 20)
			.AddIngredient(ItemID.Diamond, 3)
			.AddIngredient(ItemID.StoneBlock, 50)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
