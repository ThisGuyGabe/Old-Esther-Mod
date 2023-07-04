using System;
using EstherMod.Core.Quests;
using Terraria;

namespace EstherMod.Content.Quests.Goals;

public sealed class NPCKillQuestGoal : QuestGoal {
	public Predicate<NPC> NPCPredicate { get; init; }

	public NPCKillQuestGoal(Predicate<NPC> predicate) {
		NPCPredicate = predicate;
	}

	public override void OnHitNPCWithAnything(Player player, Entity withEntity, NPC target, NPC.HitInfo hit, int damageDone) {
		if (NPCPredicate(target) && target.life <= 0) {
			SetComplete(player);
		}
	}
}