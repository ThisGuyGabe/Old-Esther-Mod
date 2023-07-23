using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CascadeMod.Content.Walls {
	public class PixilWoodWallTile : ModWall {
		public override void SetStaticDefaults() {
			Main.wallHouse[Type] = true;
			AddMapEntry(new Color(61, 44, 31));
		}
	}
}