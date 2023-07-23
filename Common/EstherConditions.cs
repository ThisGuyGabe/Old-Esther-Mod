using System;
using CascadeMod.Core.Quests;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CascadeMod.Common.Conditions;

public static class EstherConditions {
	public static Condition AtLeastMaxHealth(Func<Player> playerGetter, int value) {
		return new Condition(Language.GetText("Mods.CascadeMod.Conditions.AtLeastMaxHealth").Format(value), () => playerGetter().statLifeMax >= value);
	}

	public static Condition QuestCompleted<T>(Func<Player> playerGetter) where T : ModQuest {
		return new Condition(Language.GetText("Mods.CascadeMod.Conditions.QuestCompletion").Format(ModContent.GetInstance<T>().DisplayName), () => ModContent.GetInstance<T>().IsCompleted(playerGetter()));
	}
}
