using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

namespace EstherMod.Content.Items
{
	public class GalvanicBar : ModItem
	{
		public override void SetDefaults()
		{

			Item.width = 32;
			Item.height = 24;
			Item.rare = 0;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(silver: 30);
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Galvanic Bar");
			// Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Galwaniczna sztabka");

		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips[0].OverrideColor = new Color(205, 127, 50);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("GalvanicOre").Type, 4);
			recipe.AddTile(TileID.Furnaces);
			recipe.Register();
		}
	}
}
