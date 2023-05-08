using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class AquaticDiverHelmet : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// Tooltip.SetDefault("");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() 
		{
			Item.width = 32;
			Item.height = 34;
			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 0;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) 
		{
			return body.type == ModContent.ItemType<AquaticPlating>() && legs.type == ModContent.ItemType<AquaticBoots>();
		}
	}
}
