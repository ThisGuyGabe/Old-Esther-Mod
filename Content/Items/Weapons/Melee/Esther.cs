using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class Esther : ModItem {
	public override void SetDefaults() {
		Item.width = 50;
		Item.height = 50;
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(gold: 4);

		Item.damage = 35;
		Item.crit = 9;
		Item.DamageType = DamageClass.Melee;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 24;
		Item.useTime = 24;
		Item.knockBack = 5f;
		Item.shootSpeed = 25f;
		Item.autoReuse = true;
		Item.useTurn = true;
		Item.UseSound = SoundID.Item1;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.Muramasa)
			.AddIngredient(ItemID.EbonwoodSword)
			.AddIngredient(ItemID.LightsBane)
			.AddTile(TileID.DemonAltar)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.Muramasa)
			.AddIngredient(ItemID.ShadewoodSword)
			.AddIngredient(ItemID.BloodButcherer)
			.AddTile(TileID.DemonAltar)
			.Register();
	}
}
