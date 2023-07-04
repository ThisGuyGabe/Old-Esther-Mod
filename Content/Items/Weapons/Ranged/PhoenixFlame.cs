using EstherMod.Content.Projectiles;
using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged;

public sealed class PhoenixFlame : BaseItem {
	public override void SetDefaults() {
		Item.damage = 70;
		Item.rare = ItemRarityID.Red;
		Item.width = 28;
		Item.height = 80;
		Item.useAnimation = 20;
		Item.useTime = 6;
		Item.reuseDelay = 15;

		Item.consumeAmmoOnLastShotOnly = true;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 5f;

		Item.shootSpeed = 10f;
		Item.DamageType = DamageClass.Ranged;
		Item.autoReuse = true;
		Item.noMelee = true;
		Item.value = Item.sellPrice(gold: 18, silver: 42);
		Item.UseSound = SoundID.Item5;
		Item.shoot = ProjectileID.WoodenArrowFriendly;
		Item.useAmmo = AmmoID.Arrow;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.HellwingBow)
			.AddIngredient(ItemID.FragmentSolar, 25)
			.AddIngredient(ItemID.SoulofFright, 15)
			.AddIngredient(ItemID.HellstoneBar, 30)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		if (type == ProjectileID.WoodenArrowFriendly) {
			type = ModContent.ProjectileType<PhoenixFlameProjectile>(); // Turns Wooden Arrows into Phoenix arrows
		}

		if (type == ProjectileID.FireArrow) {
			type = ProjectileID.HellfireArrow; // Turns Flaming Arrows into Hellfire Arrows
		}
	}
}
