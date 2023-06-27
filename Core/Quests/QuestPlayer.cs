using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Core;
using MonoMod.Utils;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace EstherMod.Core.Quests;

public sealed class QuestPlayer : ModPlayer {
	public const int ActiveQuestsCap = 3;

	public string[] ActiveQuests { get; private set; }
	public HashSet<string> CompletedQuests { get; private set; }
	public HashSet<string> CompletedQuests2 { get; private set; }

	public Dictionary<string, bool[]> GoalsCompletedByQuest { get; private set; }

	public override void PostUpdate() {
		for (int questIndex = 0; questIndex < ActiveQuestsCap; questIndex++) {
			var quest = ActiveQuests[questIndex];
			if (quest == null) continue;

			bool allComplete = true;
			foreach (ref var goalCompletion in GoalsCompletedByQuest[quest].AsSpan()) {
				allComplete &= goalCompletion;
			}
			QuestSystem.questsByName[quest].Complete(Player);
		}
	}

	public override void Initialize() {
		ActiveQuests = new string[ActiveQuestsCap];
		CompletedQuests = new();
		CompletedQuests2 = new();
		GoalsCompletedByQuest = new();
	}

	public sealed unsafe override void SaveData(TagCompound tag) {
		tag[nameof(ActiveQuests)] = ActiveQuests;
		tag[nameof(CompletedQuests)] = CompletedQuests.ToList();
		tag[nameof(CompletedQuests2)] = CompletedQuests2.ToList();
		tag[nameof(GoalsCompletedByQuest) + "Keys"] = GoalsCompletedByQuest.Keys.ToList();
		tag[nameof(GoalsCompletedByQuest) + "Values"] = GoalsCompletedByQuest.Values.ToList();
	}

	public sealed unsafe override void LoadData(TagCompound tag) {
		if (tag.TryGet(nameof(ActiveQuests), out string[] activeQuests))
			activeQuests.CopyTo(ActiveQuests, 0);
		if (tag.TryGet(nameof(CompletedQuests), out List<string> completedQuests))
			CompletedQuests = completedQuests.ToHashSet();
		if (tag.TryGet(nameof(CompletedQuests2), out List<string> completedQuests2))
			CompletedQuests2 = completedQuests2.ToHashSet();

		if (tag.TryGet(nameof(GoalsCompletedByQuest) + "Keys", out List<string> goalsCompletedByQuestKeys)
			&& tag.TryGet(nameof(GoalsCompletedByQuest) + "Values", out List<string> goalsCompletedByQuestValues)) {
			GoalsCompletedByQuest.AddRange(goalsCompletedByQuestKeys.Zip(goalsCompletedByQuestValues, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value));
		}
	}

	public sealed override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => OnHitNPCWithAnything(null, target, hit, damageDone);
	public sealed override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone) => OnHitNPCWithAnything(item, target, hit, damageDone);
	public sealed override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) => OnHitNPCWithAnything(proj, target, hit, damageDone);
	private void OnHitNPCWithAnything(Entity withEntity, NPC target, NPC.HitInfo hit, int damageDone) {
		for (int questIndex = 0; questIndex < ActiveQuestsCap; questIndex++) {
			var quest = ActiveQuests[questIndex];
			if (quest == null) continue;

			int goalIndex = 0;
			foreach (ref var goalCompletion in GoalsCompletedByQuest[quest].AsSpan()) {
				QuestSystem.questsByName[quest].Goals[goalIndex].OnHitNPCWithAnything(Player, withEntity, target, hit, damageDone);
				goalIndex++;
			}
		}
	}
}
