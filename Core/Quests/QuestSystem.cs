using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace EstherMod.Core.Quests;

public sealed class QuestSystem : ModSystem {
	public static Asset<Texture2D>[] IconById { get; private set; }
	public static Ref<Func<string>>[][] RewardsTextById { get; private set; }

	internal static List<ModQuest> quests = new();
	internal static Dictionary<string, ModQuest> questsByName = new();

	public override void Load() {
	}

	public override void OnModLoad() {
		if (Main.dedServ) return;

		IconById = new Asset<Texture2D>[quests.Count];
		RewardsTextById = new Ref<Func<string>>[quests.Count][];
		for (int i = 0; i < quests.Count; i++) {
			IconById[i] = ModContent.Request<Texture2D>(quests[i].Texture, AssetRequestMode.ImmediateLoad);
			questsByName[quests[i].FullName] = quests[i];
		}
	}

	public override void PostSetupContent() {
		if (Main.dedServ) return;

		for (int i = 0; i < quests.Count; i++) {
			RewardsTextById[i] = new Ref<Func<string>>[quests[i].Rewards.Count];
			for (int j = 0; j < RewardsTextById[i].Length; j++) {
				var reward = quests[i].Rewards[j];
				RewardsTextById[i][j] = new(() => reward.Text);
			}
		}
	}

	public override void OnModUnload() {
		Array.Fill(IconById, null);
		Array.Fill(RewardsTextById, null);
		quests.Clear();
		questsByName.Clear();

		IconById = null;
		RewardsTextById = null;
		quests = null;
		questsByName = null;
	}
}
