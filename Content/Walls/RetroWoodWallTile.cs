using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EstherMod.Content.Walls
{
	public class RetroWoodWallTile : ModWall
	{
		public override void SetStaticDefaults() {
			Main.wallHouse[Type] = true;

			AddMapEntry(new Color(142, 165, 137));
		}
	}
}