using System.Collections.Immutable;
using CascadeMod.Common.Conditions;
using CascadeMod.Content.Quests.Goals;
using CascadeMod.Content.Quests.Rewards;
using CascadeMod.Core.Quests;
using Terraria;
using Terraria.ID;

namespace CascadeMod.Content.Quests;

public sealed class PestControlQuest : ModQuest {
	public override string Texture => "CascadeMod/Assets/Quests/PestControlQuest";
	public override QuestFrame QuestFrame => QuestFrames.Hunter;
	public override float Order => 2.2f;

	public override void AddConditions(ImmutableList<Condition>.Builder conditions) {
		conditions.Add(EstherConditions.QuestCompleted<EyeSpyQuest>(() => Main.LocalPlayer));
	}

	public override void AddGoals(ImmutableList<QuestGoal>.Builder goals) {
		goals.Add(new MultipleNPCsKillQuestGoal(npc => NPCID.FromNetId(npc.netID) is NPCID.Hornet or NPCID.HornetFatty or NPCID.HornetHoney or NPCID.HornetLeafy or NPCID.HornetSpikey or NPCID.HornetStingy, 15));
	}

	public override void AddRewards(ImmutableList<QuestReward>.Builder rewards) {
		rewards.Add(new ItemQuestReward(ItemID.JungleYoyo));
		rewards.Add(new ItemQuestReward(ItemID.ThornWhip));
		rewards.Add(new ItemQuestReward(ItemID.GoldCoin, 5));
	}
}
