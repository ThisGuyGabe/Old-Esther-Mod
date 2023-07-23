using CascadeMod.Common;
using CascadeMod.Content.Projectiles;
using CascadeMod.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Weapons.Magic;

public sealed class BonestromStaff : BaseItem {
	public override void SetStaticDefaults() {
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults() {
		Item.width = 48;
		Item.height = 50;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(gold: 2, silver: 40, copper: 20);

		Item.damage = 65;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 4;
		Item.knockBack = 6;
		Item.UseSound = SoundID.Item20;
		Item.noMelee = true;
		Item.channel = true;
		Item.shoot = ModContent.ProjectileType<BonestromSigil>();
		Item.shootSpeed = 9f;
	}

	public override bool CanUseItem(Player player) {
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		position = Main.MouseWorld;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.Bone, 15)
			.AddIngredient(ItemID.HellstoneBar, 8)
			.AddIngredient(ItemID.Ruby)
			.AddTile(TileID.Anvils)
			.Register();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) {
		spriteBatch.Draw(
			EstherAssets.BonestromsStaff_Glowmask.Value,
			new Vector2(
				Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
				Item.position.Y - Main.screenPosition.Y + Item.height - EstherAssets.BonestromsStaff_Glowmask.Height() * 0.5f + 2f
			),
			null,
			Color.White,
			rotation,
			EstherAssets.BonestromsStaff_Glowmask.Size() * 0.5f,
			scale,
			SpriteEffects.None,
			0f
		);
	}
}