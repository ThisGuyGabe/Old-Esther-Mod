using EstherMod.Core.Quests;
using Terraria;
using Terraria.DataStructures;

namespace EstherMod.Common.EntitySources;

public sealed record class QuestReward_EntitySource(in ModQuest Quest, in Player Player) : IEntitySource {
	public string Context => nameof(QuestReward_EntitySource);
}
