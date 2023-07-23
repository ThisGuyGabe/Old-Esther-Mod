using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CascadeMod.Content.Tiles {
	public class PixilWoodTile : ModTile {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileSpelunker[Type] = false;
			AddMapEntry(new Color(101, 84, 71));
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
			r = 0.5f;
			g = 0.5f;
			b = 0.5f;
		}
	}
}