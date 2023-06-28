using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EstherMod.Content.Projectiles;

namespace EstherMod.Content.Items.Weapons.Ranged
{
	public class EthericalBow : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 50;
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(silver: 30, copper: 10);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Sinewave").Type;
			Item.useAmmo = AmmoID.Arrow;
			Item.shootSpeed = 6f;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {

			for (int i = 0; i < 2; i++) 
			{
				float waveOffset = i / (float)(2 - 1);

				Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);

				Sinewave modProjectile = projectile.ModProjectile as Sinewave;
				modProjectile.waveOffset = waveOffset * (1f - 1f / 2);

			}
			return false;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) 
		{
			type = Mod.Find<ModProjectile>("Sinewave").Type;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<EthericScrap>(), 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}