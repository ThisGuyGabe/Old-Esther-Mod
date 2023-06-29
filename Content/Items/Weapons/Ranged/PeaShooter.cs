using EstherMod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged;

public sealed class PeaShooter : ModItem {
	public override void SetDefaults() {
		Item.damage = 15;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 90;
		Item.height = 36;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 3.5f;
		Item.value = Item.sellPrice(silver: 80, copper: 32);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item61;
		Item.autoReuse = true;
		Item.shoot = ModContent.ProjectileType<PeaShooterProjectile>();
		Item.shootSpeed = 10;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.Acorn, 10)
			.AddIngredient(ItemID.JungleSpores, 10)
			.AddIngredient(ItemID.DirtBlock, 35)
			.AddIngredient(ItemID.Emerald, 6)
			.AddTile(TileID.Anvils)
			.Register();
	}
}