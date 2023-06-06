using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Placeable
{
	public class RetroWoodWall : ModItem
	{
		public override void SetStaticDefaults() {
			// Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
		}

		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 7;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createWall = ModContent.WallType<Walls.RetroWoodWallTile>();

		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("PixilWoodWall").Type, 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}
