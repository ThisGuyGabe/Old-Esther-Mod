using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class AquaticBoots : ModItem
	{
		public override void SetStaticDefaults() 
		{
			/* Tooltip.SetDefault("Charged to perfection."
				+ "\n10% increased movement speed and double jump"); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = Item.sellPrice(silver: 45);
			Item.rare = ItemRarityID.LightRed; 
			Item.defense = 0;
		}
	}
}
