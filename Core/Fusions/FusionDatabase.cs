using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Core.Fusions;

public sealed class FusionDatabase : ModSystem {
	internal static bool hasSetup = false;
	private static List<Fusion> fusions = new();
	private static ImmutableDictionary<int, List<Fusion>> itemUsages;
	private static Fusion[,] fusionByTypes;

	public static IReadOnlyCollection<Fusion> Fusions => fusions;
	public static IReadOnlyDictionary<int, List<Fusion>> ItemUsages => itemUsages;
	public static Fusion[,] FusionsByMaterials => fusionByTypes;

	public static void Add(Fusion fusion) {
		fusions.Add(fusion);
	}

	public override void Load() {
		Fusion.Create(ItemID.EnchantedSword, ItemID.FallenStar, ItemID.Starfury).Register();
	}

	public override void PostSetupContent() {
		var itemUsagesBuilder = ImmutableDictionary.CreateBuilder<int, List<Fusion>>();
		fusionByTypes = new Fusion[ItemLoader.ItemCount, ItemLoader.ItemCount];

		hasSetup = true;
		foreach (var fusion in fusions) {
			fusionByTypes[fusion.Main, fusion.Secondary]
				= fusionByTypes[fusion.Secondary, fusion.Main]
				= fusion;
			
			if (itemUsagesBuilder.ContainsKey(fusion.Main)) {
				itemUsagesBuilder[fusion.Main].Add(fusion);
			}
			else {
				itemUsagesBuilder[fusion.Main] = new();
			}

			if (itemUsagesBuilder.ContainsKey(fusion.Secondary)) {
				itemUsagesBuilder[fusion.Secondary].Add(fusion);
			}
			else {
				itemUsagesBuilder[fusion.Secondary] = new();
			}
		}

		itemUsages = itemUsagesBuilder.ToImmutable();
	}

	public override void Unload() {
		Array.Clear(fusionByTypes);
		fusions.Clear();
		fusionByTypes = null;
		itemUsages = null;
		fusions = null;
	}
}
