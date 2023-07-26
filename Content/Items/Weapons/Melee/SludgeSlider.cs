using System;
using CascadeMod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Weapons.Melee;

public sealed class SludgeSlider : ModItem {
	public override void SetStaticDefaults() {
		ItemID.Sets.Yoyo[Item.type] = true;
		ItemID.Sets.GamepadExtraRange[Item.type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
	}

	public override void SetDefaults() {
		Item.width = 24;
		Item.height = 36;

		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.UseSound = SoundID.Item1;

		Item.damage = 26;
		Item.DamageType = DamageClass.MeleeNoSpeed;
		Item.knockBack = 3.5f;
		Item.channel = true;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.buyPrice(silver: 20, copper: 34);

		Item.shoot = ModContent.ProjectileType<SludgeSliderProjectile>();
		Item.shootSpeed = 16f;
	}

	private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

	public override bool AllowPrefix(int pre) {
		if (Array.IndexOf(unwantedPrefixes, pre) > -1) {
			return false;
		}

		return true;
	}
}
