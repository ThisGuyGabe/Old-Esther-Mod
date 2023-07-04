using EstherMod.Content.Items.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod {
	public class WorldLoot : ModSystem {
		int itemstoPlaceInChestChoice = 0;
		public override void PostWorldGen() {
			for (int chestIndex = 0; chestIndex < Main.chest.Length; chestIndex++) {
				Chest chest = Main.chest[chestIndex];
				int[] ItemChestLoot = { ModContent.ItemType<Needle>() };
				int NeedleNum = 1;

				// Spider chest btw
				if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 15 * 36) {
					if (Main.rand.NextBool(2)) // the chance!!!
					{
						for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++) {
							if (chest.item[inventoryIndex].type == ItemID.None) {
								chest.item[inventoryIndex].SetDefaults(ItemChestLoot[itemstoPlaceInChestChoice]); // fills slot with item
								chest.item[inventoryIndex].stack = NeedleNum;
								itemstoPlaceInChestChoice = (itemstoPlaceInChestChoice + 1) % ItemChestLoot.Length;
								break;
							}
						}
					}
				}
			}
		}
	}
}
