﻿using System.Threading;
using CascadeMod.Common;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content;

public static class EstherEffects {
	public const string HydraShockwaveID = "CascadeMod/HydraShockwave";
	public const string ConfettiID = "CascadeMod/Confetti";
	public const string UnderworldFilterID = "CascadeMod/UnderworldFilter";
	public const string ShockwaveID = "CascadeMod/Shockwave";

	public static Filter HydraShockwave => Filters.Scene[HydraShockwaveID];
	public static Filter Confetti => Filters.Scene[ConfettiID];
	public static Filter UnderworldFilter => Filters.Scene[UnderworldFilterID];
	public static Filter Shockwave => Filters.Scene[ShockwaveID];

	public static ArmorShaderData GlowingDustArmor { get; private set; }
	public static DynamicShaderData GrayOutItem { get; private set; }

	public static void Load() {
		if (Main.netMode == NetmodeID.Server)
			return;

		var screenRef = new Ref<Effect>(Cascade.Instance.Assets.Request<Effect>("Effects/CustomScreenShaders", AssetRequestMode.ImmediateLoad).Value);

		Filters.Scene[HydraShockwaveID] = CreateAndGetFilter(new(screenRef, "HydraShockwave"), EffectPriority.VeryHigh);
		Filters.Scene[ConfettiID] = CreateAndGetFilter(new(screenRef, "Confetti"), EffectPriority.VeryHigh);
		Filters.Scene[UnderworldFilterID] = CreateAndGetFilter(new(screenRef, "UnderworldFilter"), EffectPriority.VeryHigh);
		Filters.Scene[ShockwaveID] = CreateAndGetFilter(new(screenRef, "Shockwave"), EffectPriority.VeryHigh);

		GlowingDustArmor = new ArmorShaderData(new Ref<Effect>(ModContent.Request<Effect>("CascadeMod/Effects/GlowingDust", AssetRequestMode.ImmediateLoad).Value), "GlowingDustPass");

		GrayOutItem = new DynamicShaderData((shaderData, dataDraw) => { }, CreateSensitiveEffect("Assets/Shaders/GrayOutItem"), "FilterMyShader");

		static Effect CreateSensitiveEffect(string path) {
			Effect effect = null;

			using var slimEvent = new ManualResetEventSlim();
			Main.QueueMainThreadAction(() => {
				effect = new(Main.graphics.GraphicsDevice, Cascade.Instance.GetFileBytes(path + ".fxb"));
				slimEvent.Set();
			});
			slimEvent.Wait();

			return effect;
		}
		static Filter CreateAndGetFilter(ScreenShaderData shaderData, EffectPriority effectPriority) {
			var filter = new Filter(shaderData, effectPriority);
			filter.Load();
			return filter;
		}
	}
}
