using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CascadeMod.Content.Walls {
	public class RetroWoodWallTile : ModWall {
		public override void SetStaticDefaults() {
			Main.wallHouse[Type] = true;

			AddMapEntry(new Color(142, 165, 137));
		}
	}
}