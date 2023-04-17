using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;


namespace EstherMod.Content.Items.Weapons
{
	public class PeaShooter : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Peashooter");
			// Tooltip.SetDefault("Kill those zombies!");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Groszkostrzelec");
		}

		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 88;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3.5f;
			Item.value = Item.sellPrice(silver: 80, copper: 32);
			Item.rare = 2;
			Item.UseSound = SoundID.Item61;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("PeaShooterProjectile").Type;
			Item.shootSpeed = 10;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Acorn, 10);
			recipe.AddIngredient(ItemID.JungleSpores, 10);
			recipe.AddIngredient(ItemID.DirtBlock, 35);
			recipe.AddIngredient(ItemID.Emerald, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
} 