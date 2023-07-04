using EstherMod.Content.Projectiles;
using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged;

public sealed class EthericalBow : BaseItem {
	public override void SetDefaults() {
		Item.width = 24;
		Item.height = 50;
		Item.damage = 12;
		Item.DamageType = DamageClass.Ranged;
		Item.useTime = 40;
		Item.useAnimation = 40;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 5;
		Item.value = Item.sellPrice(silver: 30, copper: 10);
		Item.rare = ItemRarityID.Blue;
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.shoot = ModContent.ProjectileType<Sinewave>();
		Item.useAmmo = AmmoID.Arrow;
		Item.shootSpeed = 6f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		for (int i = 0; i < 2; i++) {
			float waveOffset = i / (float)(2 - 1);

			Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);

			var sinewaveProj = projectile.ModProjectile as Sinewave;
			if (sinewaveProj != null)
				sinewaveProj.waveOffset = waveOffset * (1f - 1f / 2);
		}
		return false;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		type = ModContent.ProjectileType<Sinewave>();
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<EthericScrap>(), 5)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}