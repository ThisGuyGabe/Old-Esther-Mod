using System.Collections.Immutable;
using CascadeMod.Content.Quests.Goals;
using CascadeMod.Content.Quests.Rewards;
using CascadeMod.Core.Quests;
using Terraria.ID;

namespace CascadeMod.Content.Quests;

public sealed class SlimyPurgeQuest : ModQuest {
	public override string Texture => "CascadeMod/Assets/Quests/SlimyPurgeQuest";
	public override QuestFrame QuestFrame => QuestFrames.Hunter;
	public override float Order => 0f;

	public override void AddGoals(ImmutableList<QuestGoal>.Builder goals) {
		goals.Add(new MultipleNPCsKillQuestGoal(npc => npc.type == NPCID.BlueSlime, 10));
	}

	public override void AddRewards(ImmutableList<QuestReward>.Builder rewards) {
		rewards.Add(new ItemQuestReward(ItemID.SlimeStaff));
		rewards.Add(new ItemQuestReward(ItemID.GoldCoin, 5));
	}
}
