using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class ChargedPlating : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Charged Plating");
			// Tooltip.SetDefault("Increases crit by 5%");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 34;
			Item.height = 18;
			Item.value = Item.sellPrice(silver: 60);
			Item.rare = ItemRarityID.Blue;
			Item.defense = 6;
		}

		public override void UpdateEquip(Player player) {
			player.GetCritChance(DamageClass.Generic) += 0.05f;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips[0].OverrideColor = new Color(205, 127, 50);
		}
	}
}
