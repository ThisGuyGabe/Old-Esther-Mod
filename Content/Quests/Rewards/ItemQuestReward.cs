using CascadeMod.Common.EntitySources;
using CascadeMod.Core.Quests;
using Terraria;
using Terraria.GameContent.UI.Chat;
using Terraria.Localization;

namespace CascadeMod.Content.Quests.Rewards;

public sealed class ItemQuestReward : QuestReward {
	public Item Item { get; init; }
	public int Stack { get; init; }

	public override string Text => Language.GetText("Mods.CascadeMod.CascadeMod.Quests.Rewards.ItemQuestReward").FormatWith(new {
		ItemTag = ItemTagHandler.GenerateTag(Item),
		ItemName = Item.Name,
		Stack
	});

	public ItemQuestReward(Item item, int stack = 1) {
		Item = item;
		Stack = stack;
	}

	public ItemQuestReward(int item, int stack = 1) : this(new Item(item), stack) {
	}

	public override void Grant(Player player) {
		player.QuickSpawnItem(new EntitySource_QuestReward(Quest, player), Item, Stack);
	}
}