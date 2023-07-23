using System;
using CascadeMod.Core.Quests;
using Terraria;

namespace CascadeMod.Content.Quests.Goals;

public sealed class MultipleNPCsKillQuestGoal : QuestGoal {
	public Predicate<NPC> NPCPredicate { get; init; }
	public int Amount { get; init; }

	private int hitAmount;

	public MultipleNPCsKillQuestGoal(Predicate<NPC> predicate, int amount) {
		NPCPredicate = predicate;
		Amount = amount;
	}

	public override void OnHitNPCWithAnything(Player player, Entity withEntity, NPC target, NPC.HitInfo hit, int damageDone) {
		if (NPCPredicate(target) && target.life <= 0) {
			hitAmount++;
			if (hitAmount >= Amount) {
				SetComplete(player);
				hitAmount = 0;
			}
		}
	}
}