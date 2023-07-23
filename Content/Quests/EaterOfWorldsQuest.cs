using System.Collections.Immutable;
using CascadeMod.Content.Quests.Goals;
using CascadeMod.Content.Quests.Rewards;
using CascadeMod.Core.Quests;
using Terraria.ID;

namespace CascadeMod.Content.Quests;

public sealed class EaterOfWorldsQuest : ModQuest {
	public override string Texture => "CascadeMod/Assets/Quests/EaterOfWorldsQuest";
	public override QuestFrame QuestFrame => QuestFrames.Main;
	public override float Order => 3f;

	public override void AddGoals(ImmutableList<QuestGoal>.Builder goals) {
		goals.Add(new NPCKillQuestGoal(npc => (uint)(npc.type - NPCID.EaterofWorldsHead) < 3 && npc.boss));
	}

	public override void AddRewards(ImmutableList<QuestReward>.Builder rewards) {
		rewards.Add(new ItemQuestReward(ItemID.AncientShadowHelmet));
		rewards.Add(new ItemQuestReward(ItemID.AncientShadowScalemail));
		rewards.Add(new ItemQuestReward(ItemID.AncientShadowGreaves));
		rewards.Add(new ItemQuestReward(ItemID.LifeforcePotion));
		rewards.Add(new ItemQuestReward(ItemID.GoldCoin, 10));
	}
}
