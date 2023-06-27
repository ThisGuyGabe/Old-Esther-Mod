using Terraria;
using Terraria.Localization;

namespace EstherMod.Core.Quests;

public abstract class QuestReward : IQuestProperty {
	public ModQuest Quest { get; set; }
	public int Ordinal { get; set; }
	public abstract string Text { get; }

	public abstract void Grant(Player player);
}
