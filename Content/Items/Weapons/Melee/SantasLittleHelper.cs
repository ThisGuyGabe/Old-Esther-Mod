using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using EstherMod.Content.Projectiles;
using EstherMod.Core;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class SantasLittleHelper : BaseItem {
	public override void SetDefaults() {
		Item.width = 48;
		Item.height = 48;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.damage = 14;
		Item.autoReuse = true;
		Item.shootSpeed = 3;
		Item.DamageType = DamageClass.Melee;
		Item.knockBack = 2;
		Item.value = Item.sellPrice(silver: 30);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item1;
		Item.shoot = ModContent.ProjectileType<YellowPresent>();
		Item.shootSpeed = 12f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		for (int i = 0; i < 1; i++) {
			Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));
			newVelocity *= 1f - Main.rand.NextFloat(0.3f);
			newVelocity *= -1;

			Projectile.NewProjectile(source, new Vector2(Main.MouseWorld.X + Main.rand.Next(-1, 1), player.Center.Y - 600 + Main.rand.Next(-1, 1)), newVelocity, type, damage, knockback, player.whoAmI);

		}
		return false;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		velocity = new(0f, -30f);

		type = Main.rand.Next(3) switch {
			1 => ModContent.ProjectileType<GreenPresent>(),
			2 => ModContent.ProjectileType<BluePresent>(),
			_ => ModContent.ProjectileType<YellowPresent>(),
		};
	}
}