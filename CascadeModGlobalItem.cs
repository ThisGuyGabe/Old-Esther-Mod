using CascadeMod.Content.Items.Weapons.Melee;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod {
	public class CascadeModGlobalItem : GlobalItem {
		public override void SetDefaults(Item item) {
			item.autoReuse = true;
		}
		public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
			if (item.type == ItemID.Present) {
				itemLoot.Add(ItemDropRule.OneFromOptions(3, new int[] {
					ModContent.ItemType<SantasLittleHelper>()
				}));
			}
		}
	}
}
