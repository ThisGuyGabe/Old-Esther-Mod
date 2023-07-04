using EstherMod.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Accessories;

public sealed class TheSoul : BaseItem {
	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(4, 5, 50, 4, 5);

	public override void SetStaticDefaults() {
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 16)); // 16 frames
		ItemID.Sets.AnimatesAsSoul[Item.type] = true;

		ItemID.Sets.ItemIconPulse[Item.type] = false;
		ItemID.Sets.ItemNoGravity[Item.type] = false;

		Item.ResearchUnlockCount = 1;
	}

	public override void SetDefaults() {
		Item.width = 40;
		Item.height = 46;
		Item.rare = ItemRarityID.Pink;

		Item.accessory = true;
		Item.value = Item.sellPrice(gold: 9, silver: 48);
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.SoulofNight, 5)
			.AddIngredient(ItemID.SoulofLight, 5)
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddIngredient(ItemID.SoulofMight, 5)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddIngredient(ItemID.SoulofFlight, 5)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}

	public override void UpdateAccessory(Player player, bool hideVisual) {
		player.GetDamage(DamageClass.Generic) -= 0.04f; // this is the percentage
		player.GetAttackSpeed(DamageClass.Generic) += 0.05f;
		player.statLifeMax2 += 50;
		player.endurance -= 0.05f;
		player.jumpBoost = true;
		player.statDefense -= 6;
	}
}
