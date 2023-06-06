using EstherMod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EstherMod.Content.Items;

namespace EstherMod.Content.Items.Weapons.Ranged
{
	public class EthericalBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("EthericalBow"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 16;
			Item.height = 38;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(silver: 30, copper: 10);
			Item.rare = 1;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;

			Item.shoot = 1;
			Item.useAmmo = AmmoID.Arrow;
			Item.shootSpeed = 6f;
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