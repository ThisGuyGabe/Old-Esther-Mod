using System.Collections.Immutable;
using EstherMod.Common.Conditions;
using EstherMod.Content.Items.Weapons.Melee;
using EstherMod.Content.Quests.Goals;
using EstherMod.Content.Quests.Rewards;
using EstherMod.Core.Quests;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Quests;

public sealed class QueenBeeQuest : ModQuest {
	public override string Texture => "EstherMod/Assets/Quests/QueenBeeQuest";
	public override QuestFrame QuestFrame => QuestFrames.Bounty;
	public override float Order => 3f;

	public override void AddConditions(ImmutableList<Condition>.Builder conditions) {
		conditions.Add(EstherConditions.QuestCompleted<EaterOfWorldsQuest>(() => Main.LocalPlayer));
		conditions.Add(EstherConditions.QuestCompleted<PestControlQuest>(() => Main.LocalPlayer));
	}

	public override void AddGoals(ImmutableList<QuestGoal>.Builder goals) {
		goals.Add(new NPCKillQuestGoal(npc => npc.type == NPCID.QueenBee));
	}

	public override void AddRewards(ImmutableList<QuestReward>.Builder rewards) {
		rewards.Add(new ItemQuestReward(ItemID.HiveFive, 1));
		rewards.Add(new ItemQuestReward(ItemID.AnkletoftheWind, 1));
		rewards.Add(new ItemQuestReward(ItemID.GoldCoin, 15));
	}
}
