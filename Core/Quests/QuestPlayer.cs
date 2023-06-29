using System;
using System.Collections.Generic;
using System.Linq;
using EstherMod.Common.Extensions;
using EstherMod.Content.NPCs;
using MonoMod.Utils;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace EstherMod.Core.Quests;

public sealed class QuestPlayer : ModPlayer {
	public const int ActiveQuestsCap = 3;

	/// <summary>
	/// Quests that are active at the moment.
	/// </summary>
	public string[] ActiveQuests { get; private set; }
	/// <summary>
	/// Quests that were completed.
	/// </summary>
	public HashSet<string> CompletedQuests { get; private set; }
	/// <summary>
	/// Quests that were completed and reward is taken.
	/// </summary>
	public HashSet<string> CompletedQuests2 { get; private set; }

	public Dictionary<string, bool[]> GoalsCompletedByQuest { get; private set; }

	public override void PostUpdate() {
		for (int questIndex = 0; questIndex < ActiveQuestsCap; questIndex++) {
			var quest = ActiveQuests[questIndex];
			if (string.IsNullOrEmpty(quest)) continue;

			bool allComplete = true;
			foreach (ref var goalCompletion in GoalsCompletedByQuest[quest].AsSpan()) {
				allComplete &= goalCompletion;
				if (!allComplete) break;
			}
			if (allComplete)
				QuestSystem.questsByName[quest].Complete(Player);
		}

		if ((Main.LocalPlayer.TalkNPC == null || Main.LocalPlayer.TalkNPC.ModNPC is not BloodCultist) && BloodCultistUI.bloodCultistUi?.CurrentState != null) {
			BloodCultist.SelectedMenu = BloodCultist.Menu.None;
			BloodCultistUI.bloodCultistUi?.SetState(null);
		}
	}

	public override void Initialize() {
		ActiveQuests = new string[ActiveQuestsCap];
		Array.Fill(ActiveQuests, string.Empty);
		CompletedQuests = new();
		CompletedQuests2 = new();
		GoalsCompletedByQuest = new();
	}

	public sealed override void SaveData(TagCompound tag) {
		for (int i = 0; i < ActiveQuestsCap; i++) {
			if (string.IsNullOrEmpty(ActiveQuests[i])) continue;
			tag[nameof(ActiveQuests) + i] = ActiveQuests[i];
		}
		if (CompletedQuests.Count != 0)
			tag[nameof(CompletedQuests)] = CompletedQuests.ToList();
		if (CompletedQuests2.Count != 0)
			tag[nameof(CompletedQuests2)] = CompletedQuests2.ToList();
		if (GoalsCompletedByQuest.Count != 0)
			tag.AddDictionary(nameof(GoalsCompletedByQuest), GoalsCompletedByQuest);
	}

	public sealed override void LoadData(TagCompound tag) {
		for (int i = 0; i < ActiveQuestsCap; i++) {
			if (tag.TryGet(nameof(ActiveQuests) + i, out string activeQuest))
				ActiveQuests[i] = activeQuest;
		}
		if (tag.TryGet(nameof(CompletedQuests), out List<string> completedQuests))
			CompletedQuests = completedQuests.ToHashSet();
		if (tag.TryGet(nameof(CompletedQuests2), out List<string> completedQuests2))
			CompletedQuests2 = completedQuests2.ToHashSet();
		if (tag.TryGetDictionary(nameof(GoalsCompletedByQuest), out Dictionary<string, bool[]> goalsCompletedByQuest))
			GoalsCompletedByQuest.AddRange(goalsCompletedByQuest);
	}

	public sealed override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => OnHitNPCWithAnything(null, target, hit, damageDone);
	public sealed override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone) => OnHitNPCWithAnything(item, target, hit, damageDone);
	public sealed override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) => OnHitNPCWithAnything(proj, target, hit, damageDone);
	private void OnHitNPCWithAnything(Entity withEntity, NPC target, NPC.HitInfo hit, int damageDone) {
		for (int questIndex = 0; questIndex < ActiveQuestsCap; questIndex++) {
			var quest = ActiveQuests[questIndex];
			if (string.IsNullOrEmpty(quest)) continue;

			int goalIndex = 0;
			foreach (ref var goalCompletion in GoalsCompletedByQuest[quest].AsSpan()) {
				QuestSystem.questsByName[quest].Goals[goalIndex].OnHitNPCWithAnything(Player, withEntity, target, hit, damageDone);
				goalIndex++;
			}
		}
	}
}
