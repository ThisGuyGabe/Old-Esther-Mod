using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

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
				if (Main.rand.NextBool(3)) // the chance!!!
				{
					int[] items = {
                        Mod.Find<ModItem>("SantasLittleHelper").Type
                    };
					itemLoot.Add(ItemDropRule.OneFromOptions(3, items));
				}
			}
		}
	}
}
