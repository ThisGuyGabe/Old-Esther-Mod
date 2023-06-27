using System.Collections.Immutable;
using EstherMod.Content.Items;
using EstherMod.Content.Quests.Goals;
using EstherMod.Content.Quests.Rewards;
using EstherMod.Core.Quests;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Quests;

public sealed class DeepseaTroublesQuest : ModQuest {
	public override string Texture => "EstherMod/Assets/Quests/DeepseaTroublesQuest";
	public override QuestFrame QuestFrame => QuestFrames.Hunter;
	public override float Order => 0.1f;

	public override void AddGoals(ImmutableList<QuestGoal>.Builder goals) {
		goals.Add(new MultipleNPCsKillQuestGoal(npc => NPCID.Sets.ZappingJellyfish[npc.type], 6));
	}

	public override void AddRewards(ImmutableList<QuestReward>.Builder rewards) {
		rewards.Add(new ItemQuestReward(ModContent.ItemType<SoulPearl>()));
	}
}
