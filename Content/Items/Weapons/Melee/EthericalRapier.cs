using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EstherMod.Content.Projectiles;
using EstherMod.Core;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class EthericalRapier : BaseItem {
	public override void SetDefaults() {
		Item.width = 52;
		Item.height = 52;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 30);

		Item.damage = 16;
		Item.DamageType = DamageClass.MeleeNoSpeed;
		Item.useTime = 6;
		Item.useAnimation = 6;
		Item.useStyle = ItemUseStyleID.Rapier;
		Item.knockBack = 5;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;
		Item.noUseGraphic = true;
		Item.noMelee = true;
		Item.shootSpeed = 2.1f;
		Item.shoot = ModContent.ProjectileType<EthericalRapierProjectile>();
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<EthericScrap>(), 4)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
