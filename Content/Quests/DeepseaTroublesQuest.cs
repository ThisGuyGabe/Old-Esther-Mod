using System.Collections.Immutable;
using CascadeMod.Content.Items;
using CascadeMod.Content.Quests.Goals;
using CascadeMod.Content.Quests.Rewards;
using CascadeMod.Core.Quests;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Quests;

public sealed class DeepseaTroublesQuest : ModQuest {
	public override string Texture => "CascadeMod/Assets/Quests/DeepseaTroublesQuest";
	public override QuestFrame QuestFrame => QuestFrames.Hunter;
	public override float Order => 0.1f;

	public override void AddGoals(ImmutableList<QuestGoal>.Builder goals) {
		goals.Add(new MultipleNPCsKillQuestGoal(npc => NPCID.Sets.ZappingJellyfish[npc.type], 6));
	}

	public override void AddRewards(ImmutableList<QuestReward>.Builder rewards) {
		rewards.Add(new ItemQuestReward(ModContent.ItemType<SoulPearl>()));
	}
}
