using System.IO;
using CascadeMod.Common;
using CascadeMod.Content;
using CascadeMod.Core.Quests;
using Terraria;
using Terraria.ModLoader;

namespace CascadeMod;

public sealed class Esther : Mod {
	public static Esther Instance { get; private set; }

	public Esther() {
		Instance = this;
	}

	public sealed override void Load() {
		EstherAssets.Load();
		EstherEffects.Load();
	}

	public override void HandlePacket(BinaryReader reader, int whoAmI) {
		byte messageId = reader.ReadByte();
		switch ((EstherPackets.MessageID)messageId) {
			case EstherPackets.MessageID.GoalCompletion: {
				byte playerIndex = reader.ReadByte();
				byte ordinal = reader.ReadByte();
				byte quoteIndex = reader.ReadByte();

				if (Main.player[playerIndex].TryGetModPlayer(out QuestPlayer questPlayer)) {
					questPlayer.GoalsCompletedByQuest[QuestSystem.quests[quoteIndex].FullName][ordinal] = true;
				}
			}
			break;
			case EstherPackets.MessageID.ClaimQuestRewards: {
				byte playerIndex = reader.ReadByte();
				byte quoteIndex = reader.ReadByte();

				if (Main.player[playerIndex].TryGetModPlayer(out QuestPlayer questPlayer)) {
					questPlayer.CompletedQuests2.Add(QuestSystem.quests[quoteIndex].FullName);
				}
			}
			break;
			case EstherPackets.MessageID.AssignQuest: {
				byte playerIndex = reader.ReadByte();
				byte quoteIndex = reader.ReadByte();
				byte count = reader.ReadByte();

				if (Main.player[playerIndex].TryGetModPlayer(out QuestPlayer questPlayer)) {
					questPlayer.GoalsCompletedByQuest[QuestSystem.quests[quoteIndex].FullName] = new bool[count];
				}
			}
			break;
			case EstherPackets.MessageID.CompleteQuest: {
				byte playerIndex = reader.ReadByte();
				byte quoteIndex = reader.ReadByte();

				if (Main.player[playerIndex].TryGetModPlayer(out QuestPlayer questPlayer)) {
					string quote = QuestSystem.quests[quoteIndex].FullName;
					questPlayer.CompletedQuests.Add(quote);
					questPlayer.GoalsCompletedByQuest.Remove(quote);
				}
			}
			break;
		}
	}

	public sealed override void Unload() {
		Instance = null;
	}
}