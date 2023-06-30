using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Magic;

public sealed class EmpyrealStaff : BaseItem {
	public override void SetStaticDefaults() {
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults() {
		Item.width = 40;
		Item.height = 40;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 60, copper: 20);
		Item.UseSound = SoundID.Item72;

		Item.damage = 23;
		Item.crit = 7;
		Item.mana = 10;
		Item.knockBack = 7;
		Item.DamageType = DamageClass.Magic;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.autoReuse = true;
		Item.useTime = 23;
		Item.useAnimation = 23;
		Item.shoot = ProjectileID.HallowStar;
		Item.shootSpeed = 12;
	}

	public override float UseAnimationMultiplier(Player player) {
		if (Main.dayTime) {
			return 0.5f;
		}
		return 1f;
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
		if (Main.dayTime) {
			mult -= 0.5f;
		}
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		if (Main.dayTime) {
			damage = (int)(damage / 1.5);
		}
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.Cloud, 25)
			.AddIngredient(ItemID.DemoniteBar, 5)
			.AddIngredient(ItemID.FallenStar, 5)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.Cloud, 25)
			.AddIngredient(ItemID.CrimtaneBar, 5)
			.AddIngredient(ItemID.FallenStar, 5)
			.AddTile(TileID.Anvils)
			.Register();
	}
}