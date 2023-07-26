﻿using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Accessories;

public sealed class HauntedCandle : ModItem {
	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(5, 6, 20);

	public override void SetStaticDefaults() {
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
		ItemID.Sets.AnimatesAsSoul[Item.type] = true;

		ItemID.Sets.ItemIconPulse[Item.type] = false;
		ItemID.Sets.ItemNoGravity[Item.type] = false;

		Item.ResearchUnlockCount = 1;

	}

	public override void SetDefaults() {
		Item.width = 40;
		Item.height = 42;
		Item.rare = ItemRarityID.Orange;

		Item.accessory = true;
		Item.value = Item.sellPrice(gold: 2, silver: 64);
	}

	public override void UpdateAccessory(Player player, bool hideVisual) {
		player.statManaMax2 += 20;
		player.GetDamage(DamageClass.Magic) += 0.05f;
		player.GetCritChance(DamageClass.Magic) += 6;
		player.ZoneWaterCandle = true;
		player.AddBuff(BuffID.WaterCandle, -1);

		if (player.statLife < 250) {
			player.GetDamage(DamageClass.Generic) += 0.06f;
		}
		if (player.statLife < 150) {
			player.GetDamage(DamageClass.Generic) += 0.12f;
		}
		if (player.statLife < 50) {
			player.GetDamage(DamageClass.Generic) += 0.18f;
		}
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient<SoulPebble>(10)
			.AddIngredient<SoulPearl>()
			.AddIngredient(ItemID.TinkerersWorkshop)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
