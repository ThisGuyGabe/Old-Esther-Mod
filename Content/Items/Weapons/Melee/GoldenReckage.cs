using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EstherMod.Content.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using EstherMod.Common;
using EstherMod.Core;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class GoldenReckage : BaseItem {
	public override void SetDefaults() {
		Item.damage = 29;
		Item.DamageType = DamageClass.Melee;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 2;
		Item.value = Item.sellPrice(gold: 1, silver: 30);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.noUseGraphic = true;
		Item.shoot = ModContent.ProjectileType<GoldenReckageProjectile>();
		Item.scale = 1.25f;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.GoldBroadsword)
			.AddIngredient(ItemID.Bone, 25)
			.AddIngredient(ItemID.GoldBar, 15)
			.AddIngredient(ItemID.Feather, 5)
			.AddIngredient(ItemID.FallenStar, 3)
			.Register();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) {
		spriteBatch.Draw(
			EstherAssets.GoldenReckage_Glowmask.Value,
			new Vector2
			(
				Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
				Item.position.Y - Main.screenPosition.Y + Item.height - EstherAssets.GoldenReckage_Glowmask.Height() * 0.5f + 2f
			),
			null,
			Color.White,
			rotation,
			EstherAssets.GoldenReckage_Glowmask.Size() * 0.5f,
			scale,
			SpriteEffects.None,
			0f
		);
	}
}
