using EstherMod.Content.Projectiles;
using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Magic;

public sealed class EthericalChant : BaseItem {
	public override void SetDefaults() {
		Item.width = 30;
		Item.height = 36;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(gold: 1, silver: 30, copper: 10);

		Item.damage = 14;
		Item.mana = 3;
		Item.DamageType = DamageClass.Magic;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.UseSound = SoundID.Item8;
		Item.useAnimation = 26;
		Item.useTime = 26;
		Item.noMelee = true;
		Item.shoot = ModContent.ProjectileType<ChantSpark>();
		Item.shootSpeed = 7f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		for (int i = 0; i < Main.rand.Next(3, 7); i++) {
			Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
			newVelocity *= 1f - Main.rand.NextFloat(0.3f);

			Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
		}
		return false;
	}
}
