using CascadeMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Items.Weapons.Ranged
{
	public class CactusBow : BaseItem
	{
		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 38;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = 1800;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item5;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 5f;
			Item.useAmmo = AmmoID.Arrow;
		}
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cactus, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
	}
}
