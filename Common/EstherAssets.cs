using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace EstherMod.Common;

public static class EstherAssets {
	public static Asset<Texture2D> BonestromsStaff_Glowmask { get; private set; }
	public static Asset<Texture2D> GoldenReckage_Glowmask { get; private set; }

	public static void Load() {
		// Dedicated servers can't load assets
		if (Main.dedServ)
			return;

		BonestromsStaff_Glowmask = Request<Texture2D>("EstherMod/Content/GlowMasks/BonestromStaff_Glow");
		GoldenReckage_Glowmask = Request<Texture2D>("EstherMod/Content/GlowMasks/GoldenReckage_Glow");
	}
}
