using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Core.Fusions;

public sealed class FusionDatabase : ModSystem {
	private static List<Fusion> fusions = new();
	private static Dictionary<int, List<Fusion>> itemUsages = new();
	private static Fusion[,] fusionByTypes;
	private static HashSet<int> fusionResultTypes = new();

	public static IReadOnlyCollection<Fusion> Fusions => fusions;
	public static IReadOnlyDictionary<int, List<Fusion>> ItemUsages => itemUsages;
	public static Fusion[,] FusionsByMaterials => fusionByTypes;
	public static IReadOnlySet<int> ResultTypes => fusionResultTypes;

	public static void Add(Fusion fusion) {
		fusionByTypes ??= new Fusion[ItemLoader.ItemCount, ItemLoader.ItemCount];

		fusions.Add(fusion);

		fusionResultTypes.Add(fusion.Result);

		fusionByTypes[fusion.Main, fusion.Secondary]
			= fusionByTypes[fusion.Secondary, fusion.Main]
			= fusion;

		if (itemUsages.ContainsKey(fusion.Main)) {
			itemUsages[fusion.Main].Add(fusion);
		}
		else {
			itemUsages[fusion.Main] = new() { fusion };
		}

		if (itemUsages.ContainsKey(fusion.Secondary)) {
			itemUsages[fusion.Secondary].Add(fusion);
		}
		else {
			itemUsages[fusion.Secondary] = new() { fusion };
		}
	}

	public override void OnModLoad() {
		Fusion.Create(ItemID.EnchantedSword, ItemID.FallenStar, ItemID.Starfury).Register();
	}

	public override void Unload() {
		Array.Clear(fusionByTypes);
		itemUsages.Clear();
		fusions.Clear();
		fusionResultTypes.Clear();
		fusionByTypes = null;
		itemUsages = null;
		fusions = null;
		fusionResultTypes = null;
	}
}
