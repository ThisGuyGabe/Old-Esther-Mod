﻿namespace CascadeMod.Core.Quests;

public interface IQuestProperty {
	ModQuest Quest { get; set; }
	int Ordinal { get; set; }
}
