﻿using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class PermaFrost : BaseItem {
	public override void SetDefaults() {
		Item.width = 60;
		Item.height = 60;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 22;
		Item.useAnimation = 22;
		Item.damage = 64;
		Item.shootSpeed = 3;
		Item.DamageType = DamageClass.Melee;
		Item.knockBack = 5;
		Item.value = Item.sellPrice(gold: 5, silver: 41);
		Item.rare = ItemRarityID.LightRed;
		Item.UseSound = SoundID.Item1;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
		target.AddBuff(BuffID.Frostburn, 240);

		Projectile.NewProjectile(Item.GetSource_None(), player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 8f, ModContent.ProjectileType<Projectiles.Frostpike>(), damageDone, player.whoAmI);
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.BorealWood, 7)
			.AddIngredient(ItemID.TitaniumBar, 9)
			.AddIngredient(ItemID.FrostCore, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.BorealWood, 7)
			.AddIngredient(ItemID.AdamantiteBar, 9)
			.AddIngredient(ItemID.FrostCore, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}