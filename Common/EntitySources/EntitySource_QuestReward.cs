using CascadeMod.Core.Quests;
using Terraria;
using Terraria.DataStructures;

namespace CascadeMod.Common.EntitySources;

public sealed record class EntitySource_QuestReward(in ModQuest Quest, in Player Player) : IEntitySource {
	public string Context => nameof(EntitySource_QuestReward);
}
