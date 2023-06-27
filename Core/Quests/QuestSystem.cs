using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace EstherMod.Core.Quests;

public sealed class QuestSystem : ModSystem {
	public static Asset<Texture2D>[] IconById { get; private set; }

	internal static List<ModQuest> quests = new();
	internal static Dictionary<string, ModQuest> questsByName = new();

	public override void Load() {
	}

	public override void OnModLoad() {
		IconById = new Asset<Texture2D>[quests.Count];
		for (int i = 0; i < quests.Count; i++) {
			IconById[i] = ModContent.Request<Texture2D>(quests[i].Texture, AssetRequestMode.ImmediateLoad);
		}
	}

	public override void OnModUnload() {
		Array.Fill(IconById, null);
		quests.Clear();
		questsByName.Clear();

		IconById = null;
		quests = null;
		questsByName = null;
	}
}
