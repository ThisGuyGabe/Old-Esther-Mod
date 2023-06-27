using System;
using System.Collections.Immutable;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EstherMod.Core.Quests;

public abstract class ModQuest : ModTexturedType, ILocalizedModType {
	// The reason that its not 'Quests' is cause many mods tend to add their own quests
	// and we don't want to confuse our quest localization with theirs.
	public string LocalizationCategory => "EstherMod.Quests";
	public virtual QuestFrame QuestFrame => QuestFrames.Main;
	public virtual LocalizedText DisplayName => this.GetLocalization("DisplayName", PrettyPrintName);
	public virtual LocalizedText Description => this.GetLocalization("Description", () => string.Empty);

	public int Type { get; private set; }
	public int Ordinal { get; private set; } = -1;

	public abstract float Order { get; }
	public ImmutableList<Condition> Conditions { get; private set; }
	public ImmutableList<QuestGoal> Goals { get; private set; }
	public ImmutableList<QuestReward> Rewards { get; private set; }

	public sealed override void SetupContent() {
		ImmutableList<T> Create<T>(Action<ImmutableList<T>.Builder> populator) where T : class, IQuestProperty {
			var builder = ImmutableList.CreateBuilder<T>();
			populator(builder);
			for (int i = 0; i < builder.Count; i++) {
				builder[i].Quest = this;
				builder[i].Ordinal = i;
			}
			return builder.ToImmutable();
		}

		var conditionsBuilder = ImmutableList.CreateBuilder<Condition>();
		AddConditions(conditionsBuilder);
		Conditions = conditionsBuilder.ToImmutable();

		Goals = Create<QuestGoal>(AddGoals);
		Rewards = Create<QuestReward>(AddRewards);

		SetStaticDefaults();
	}

	protected sealed override void Register() {
		ModTypeLookup<ModQuest>.Register(this);

		Type = QuestSystem.quests.Count;
		QuestSystem.quests.Add(this);
		QuestSystem.questsByName[FullName] = this;

		ModContent.Request<Texture2D>(Texture);
	}

	public abstract void AddGoals(ImmutableList<QuestGoal>.Builder goals);

	public abstract void AddRewards(ImmutableList<QuestReward>.Builder rewards);

	public virtual void AddConditions(ImmutableList<Condition>.Builder conditions) {
	}

	public bool TryAssign(Player player) {
		if (IsUnlocked() && player.TryGetModPlayer(out QuestPlayer questPlayer) && !IsAssignedTo(player)) {
			int i = 0;
			foreach (ref var quest in questPlayer.ActiveQuests.AsSpan()) {
				if (!string.IsNullOrEmpty(quest)) {
					i++;
					continue;
				}

				quest = FullName;
				Ordinal = i;

				questPlayer.GoalsCompletedByQuest[quest] = new bool[Goals.Count];

				if (Main.netMode == NetmodeID.MultiplayerClient) {
					Esther.Instance.Packet_AssignQuest(Main.myPlayer, quest, Goals.Count);
				}
				return true;
			}
		}
		return false;
	}

	public bool IsAssignedTo(Player player) {
		if (player.TryGetModPlayer(out QuestPlayer questPlayer)) {
			return questPlayer.ActiveQuests.AsSpan().Contains(FullName);
		}
		return false;
	}

	public bool IsUnlocked() {
		foreach (var condition in Conditions) {
			if (!condition.IsMet())
				return false;
		}
		return true;
	}

	public bool IsCompleted(Player player) {
		if (!player.TryGetModPlayer(out QuestPlayer questPlayer))
			return false;
		return questPlayer.CompletedQuests.Contains(FullName);
	}

	public void Complete(Player player) {
		if (!player.TryGetModPlayer(out QuestPlayer questPlayer))
			return;

		Main.NewText($"Quest '{DisplayName.Value}' has been completed!", 255, 255, 200);

		questPlayer.ActiveQuests[Ordinal] = string.Empty;
		questPlayer.CompletedQuests.Add(FullName);
		questPlayer.GoalsCompletedByQuest.Remove(FullName);

		if (Main.netMode == NetmodeID.MultiplayerClient) {
			Esther.Instance.Packet_CompleteQuest(player.whoAmI, FullName);
		}

		Ordinal = -1;
	}
}
