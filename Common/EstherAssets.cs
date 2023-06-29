using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using static Terraria.ModLoader.ModContent;

namespace EstherMod.Common;

public static class EstherAssets {
	public static readonly Asset<Texture2D> BonestromsStaff_Glowmask = Request<Texture2D>("EstherMod/Content/GlowMasks/BonestromStaff_Glow");
	public static readonly Asset<Texture2D> GoldenReckage_Glowmask = Request<Texture2D>("EstherMod/Content/GlowMasks/GoldenReckage_Glow");
}
