using System.Collections.Immutable;
using CascadeMod.Common.Conditions;
using CascadeMod.Content.Items.Weapons.Melee;
using CascadeMod.Content.Items.Weapons.Ranged;
using CascadeMod.Content.Quests.Goals;
using CascadeMod.Content.Quests.Rewards;
using CascadeMod.Core.Quests;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Quests;

public sealed class EyeSpyQuest : ModQuest {
	public override string Texture => "CascadeMod/Assets/Quests/EyeSpyQuest";
	public override QuestFrame QuestFrame => QuestFrames.Bounty;
	public override float Order => 2f;

	public override void AddConditions(ImmutableList<Condition>.Builder conditions) {
		conditions.Add(EstherConditions.AtLeastMaxHealth(() => Main.LocalPlayer, 200));
	}

	public override void AddGoals(ImmutableList<QuestGoal>.Builder goals) {
		goals.Add(new NPCKillQuestGoal(npc => npc.type == NPCID.EyeofCthulhu));
	}

	public override void AddRewards(ImmutableList<QuestReward>.Builder rewards) {
		rewards.Add(new ItemQuestReward(ItemID.Starfury, 1));
		rewards.Add(new ItemQuestReward(ItemID.Trimarang, 1));
		rewards.Add(new ItemQuestReward(ItemID.GoldCoin, 10));
	}
}
