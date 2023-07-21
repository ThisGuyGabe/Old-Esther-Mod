using EstherMod.Content.Projectiles;
using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged;

public sealed class FoulLongbow : BaseItem {
	public override void SetDefaults() {
		Item.damage = 15;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 22;
		Item.height = 56;
		Item.useTime = 22;
		Item.useAnimation = 22;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 5;
		Item.value = Item.sellPrice(gold: 1);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item5;

		Item.shoot = ProjectileID.WoodenArrowFriendly;
		Item.useAmmo = AmmoID.Arrow;
		Item.shootSpeed = 7f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		for (int i = 0; i < 3; i++) {
			// Rotate the velocity randomly by 30 degrees at max.
			Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));

			// Create a projectile.
			Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
		}
		if (source.AmmoItemIdUsed != ItemID.EndlessQuiver) {
			player.ConsumeItem(source.AmmoItemIdUsed);
			player.ConsumeItem(source.AmmoItemIdUsed);
		}
		return false;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		if (type == ProjectileID.WoodenArrowFriendly)
			type = ModContent.ProjectileType<FoulArrow>();
	}
}
