using EstherMod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Melee
{
	public class EthericalDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("EthericalDagger"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Rapier;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
            Item.noUseGraphic = true;
			Item.noMelee = true;
            Item.shootSpeed = 2.1f;
			Item.shoot = ModContent.ProjectileType<EthericalDaggerProjectile>();
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<EthericScrap>(), 4);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}