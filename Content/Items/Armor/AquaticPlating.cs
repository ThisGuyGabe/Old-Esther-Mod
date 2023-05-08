using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class AquaticPlating : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Charged Plating");
			// Tooltip.SetDefault("Increases crit by 5%");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = Item.sellPrice(silver: 60);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 0;
		}
	}
}
