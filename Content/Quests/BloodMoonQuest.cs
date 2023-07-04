using System.Collections.Immutable;
using EstherMod.Common.Conditions;
using EstherMod.Content.Quests.Goals;
using EstherMod.Content.Quests.Rewards;
using EstherMod.Core.Quests;
using Terraria;
using Terraria.ID;

namespace EstherMod.Content.Quests;

public sealed class BloodMoonQuest : ModQuest {
	public override string Texture => "EstherMod/Assets/Quests/BloodMoonQuest";
	public override QuestFrame QuestFrame => QuestFrames.Hunter;
	public override float Order => 2.1f;

	public override void AddConditions(ImmutableList<Condition>.Builder conditions) {
		conditions.Add(EstherConditions.QuestCompleted<EyeSpyQuest>(() => Main.LocalPlayer));
	}

	public override void AddGoals(ImmutableList<QuestGoal>.Builder goals) {
		goals.Add(new MultipleNPCsKillQuestGoal(npc => npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler, 20));
	}

	public override void AddRewards(ImmutableList<QuestReward>.Builder rewards) {
		rewards.Add(new ItemQuestReward(ItemID.BloodMoonStarter));
		rewards.Add(new ItemQuestReward(ItemID.VampireFrogStaff));
		rewards.Add(new ItemQuestReward(ItemID.GoldCoin, 10));
	}
}
