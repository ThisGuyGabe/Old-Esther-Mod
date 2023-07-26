using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Weapons.Ranged;

public sealed class BeamBow : ModItem {
	public override void SetDefaults() {
		Item.width = 22;
		Item.height = 34;
		Item.rare = ItemRarityID.Green;
		Item.damage = 5;
		Item.knockBack = 2f;
		Item.noMelee = true;
		Item.useTime = 10;
		Item.useAnimation = 10;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.UseSound = SoundID.Item5;
		Item.DamageType = DamageClass.Ranged;
		Item.value = Item.sellPrice(silver: 80, copper: 40);
		Item.shoot = ProjectileID.GreenLaser;
		Item.shootSpeed = 46f;
		Item.useAmmo = AmmoID.Arrow;
		Item.UseSound = SoundID.Item12;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		type = ProjectileID.GreenLaser;
	}

	public override void UpdateInventory(Player player) {
		// Concerning.
		Item.useTime = Main.rand.Next(5, 15);
		Item.useAnimation = Item.useTime;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.MeteoriteBar, 10)
			.AddIngredient(ItemID.DemoniteBar, 5)
			.AddIngredient(ItemID.FallenStar)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.MeteoriteBar, 10)
			.AddIngredient(ItemID.CrimtaneBar, 5)
			.AddIngredient(ItemID.FallenStar)
			.AddTile(TileID.Anvils)
			.Register();
	}
}