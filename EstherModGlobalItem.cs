using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using EstherMod.Content.Items.Weapons.Melee;

namespace EstherMod
{
    public class EstherModGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            item.autoReuse = true;
        }
		public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
		{
			if (item.type == ItemID.Present)
			{
				itemLoot.Add(ItemDropRule.OneFromOptions(3, new int[] {
					ModContent.ItemType<SantasLittleHelper>()
				}));
			}
		}
	}
}
