using Terraria.ID;
using Terraria;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Shaders;

namespace EstherMod.Content;

public static class EstherEffects {
	public const string HydraShockwaveID = "EstherMod/HydraShockwave";
	public const string ConfettiID = "EstherMod/Confetti";
	public const string UnderworldFilterID = "EstherMod/UnderworldFilter";
	public const string ShockwaveID = "EstherMod/Shockwave";

	public static Filter HydraShockwave => Filters.Scene[HydraShockwaveID];
	public static Filter Confetti => Filters.Scene[ConfettiID];
	public static Filter UnderworldFilter => Filters.Scene[UnderworldFilterID];
	public static Filter Shockwave => Filters.Scene[ShockwaveID];

	public static void Load() {
		if (Main.netMode == NetmodeID.Server) return;

		var screenRef = new Ref<Effect>(Esther.Instance.Assets.Request<Effect>("Effects/CustomScreenShaders", AssetRequestMode.ImmediateLoad).Value);

		Filters.Scene[HydraShockwaveID] = CreateAndGetFilter(new(screenRef, "HydraShockwave"), EffectPriority.VeryHigh);
		Filters.Scene[ConfettiID] = CreateAndGetFilter(new(screenRef, "Confetti"), EffectPriority.VeryHigh);
		Filters.Scene[UnderworldFilterID] = CreateAndGetFilter(new(screenRef, "UnderworldFilter"), EffectPriority.VeryHigh);
		Filters.Scene[ShockwaveID] = CreateAndGetFilter(new(screenRef, "Shockwave"), EffectPriority.VeryHigh);

		static Filter CreateAndGetFilter(ScreenShaderData shaderData, EffectPriority effectPriority) {
			var filter = new Filter(shaderData, effectPriority);
			filter.Load();
			return filter;
		}
	}
}
