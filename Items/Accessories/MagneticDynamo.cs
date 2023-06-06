using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Accessories
{
	public class MagneticDynamo : ModItem
	{

		public override void SetDefaults()
		{

			Item.width = 34;
			Item.height = 30;
			Item.rare = 2;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 2, silver: 32);
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magnetic Dynamo");
			/* Tooltip.SetDefault("Makes u attack faster but at the cost of some damage"
				+ "\nIncreases attack speed by 12% and decreases damage by 6%"); */
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Magnetyczne Dynamo");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Bone, 20);
			recipe.AddIngredient(ItemID.Diamond, 3);
			recipe.AddIngredient(ItemID.StoneBlock, 50);
			recipe.AddTile(114);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += -0.06f;
			player.GetAttackSpeed(DamageClass.Generic) += 0.12f;

		}
	}
}
