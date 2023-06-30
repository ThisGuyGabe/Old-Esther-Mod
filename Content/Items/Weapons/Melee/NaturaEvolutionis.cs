using EstherMod.Content.Projectiles;
using EstherMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class NaturaEvolutionis : BaseItem {
	public override void SetStaticDefaults() {
		ItemID.Sets.Yoyo[Item.type] = true;
	}

	public override void SetDefaults() {
		Item.damage = 9;
		Item.crit = 5;
		Item.rare = ItemRarityID.Green;
		Item.width = 32;
		Item.height = 46;
		Item.useAnimation = 35;
		Item.useTime = 35;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 2.5f;
		Item.shootSpeed = 6;
		Item.DamageType = DamageClass.Melee;
		Item.channel = true;
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.value = Item.sellPrice(silver: 73, copper: 22);
		Item.UseSound = SoundID.Item1;
		Item.shoot = ModContent.ProjectileType<NaturaProjectile>();
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.JungleYoyo)
			.AddIngredient(ItemID.Stinger, 5)
			.AddIngredient(ItemID.RichMahogany, 25)
			.AddIngredient(ItemID.Vine, 1)
			.AddTile(TileID.Anvils)
			.Register();
	}
}