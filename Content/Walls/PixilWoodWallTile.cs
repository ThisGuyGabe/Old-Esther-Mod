using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EstherMod.Content.Walls
{
	public class PixilWoodWallTile : ModWall
	{
		public override void SetStaticDefaults() {
			Main.wallHouse[Type] = true;

			ItemDrop = ModContent.ItemType<Items.Placeable.PixilWoodWall>();

			AddMapEntry(new Color(61, 44, 31));
		}
	}
}