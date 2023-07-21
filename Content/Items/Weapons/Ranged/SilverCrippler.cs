using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged;

public sealed class SilverCrippler : BaseItem {
	public override void SetDefaults() {
		Item.damage = 10;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 54;
		Item.height = 28;
		Item.useTime = 4;
		Item.useAnimation = 12;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.reuseDelay = 16;
		Item.noMelee = true;
		Item.knockBack = 1;
		Item.value = Item.sellPrice(gold: 1, silver: 21, copper: 60);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item31;
		Item.shoot = ProjectileID.PurificationPowder;
		Item.shootSpeed = 8f;
		Item.useAmmo = AmmoID.Bullet;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player) {
		return Main.rand.NextBool(3);
	}

	public override Vector2? HoldoutOffset() {
		return new Vector2(-10, 0);
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.IllegalGunParts, 1)
			.AddIngredient(ItemID.SilverBar, 5)
			.AddIngredient(ItemID.Wood, 25)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.IllegalGunParts, 1)
			.AddIngredient(ItemID.TungstenBar, 5)
			.AddIngredient(ItemID.Wood, 25)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
