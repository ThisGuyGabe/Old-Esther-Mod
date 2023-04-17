using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ChargedCap : ModItem
	{
		public override void SetStaticDefaults() {
			// Tooltip.SetDefault("");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.width = 26;
			Item.height = 18;
			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.Blue;
			Item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ModContent.ItemType<ChargedPlating>() && legs.type == ModContent.ItemType<ChargedTracers>();
		}

		public override void UpdateArmorSet(Player player) {
			player.setBonus = "Movement speed increased by 15%. Attack speed increased by 15% but decreased 10% damage.";
			player.moveSpeed += 0.15f;
			player.GetAttackSpeed(DamageClass.Generic) += 0.15f;
			player.GetDamage(DamageClass.Generic) += 0.10f;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips[0].OverrideColor = new Color(205, 127, 50);
		}
	}
}
