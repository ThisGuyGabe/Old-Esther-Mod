using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.ItemDropRules;

namespace EstherMod
{
    public class WorldLoot : ModSystem
    {
        int itemstoPlaceInChestChoice = 0;
        public override void PostWorldGen()
        {
            for (int chestIndex = 0; chestIndex < 800; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                int[] ItemChestLoot = { Mod.Find<ModItem>("Needle").Type };
                int NeedleNum = Main.rand.Next(1, 1);

                // Spider chest btw
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 15 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == 0)
                        {
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
