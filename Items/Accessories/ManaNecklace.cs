using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Accessories
{
	public class ManaNecklace : ModItem
	{

		public override void SetDefaults()
		{

			Item.width = 34;
			Item.height = 38;
			Item.rare = 2;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 1, silver: 30);
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mana Necklace");
			// Tooltip.SetDefault("Effects of the Mana Regeneration Band and Mana Flower");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Naszyjnik Many");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ManaFlower, 1);
			recipe.AddIngredient(ItemID.ManaRegenerationBand, 1);
			recipe.AddTile(114);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.manaCost -= 0.08f;
			player.statManaMax2 += 20;
			player.manaFlower = true;
			player.manaRegen += 5;

		}
	}
}
