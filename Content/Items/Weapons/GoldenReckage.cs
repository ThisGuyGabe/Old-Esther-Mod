using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Localization;


namespace EstherMod.Content.Items.Weapons
{
	public class GoldenReckage : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 39;
			Item.DamageType = DamageClass.Melee;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = 1;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(gold: 1, silver: 15);
			Item.rare = 3;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.scale = 1.5f;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Golden Wreckage");
			// Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Złote Przeładowanie");
		}

		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Feather, 10);
			recipe.AddIngredient(ItemID.GoldBar, 12);
			recipe.AddIngredient(ItemID.TissueSample, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Feather, 10);
			recipe.AddIngredient(ItemID.GoldBar, 12);
			recipe.AddIngredient(ItemID.ShadowScale, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(Item.GetSource_None(), player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 8f, ProjectileID.FallingStar, damageDone, player.whoAmI);
			}
		}
	}
}
