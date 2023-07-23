using System.IO;
using CascadeMod.Common;
using CascadeMod.Content;
using CascadeMod.Core.Quests;
using Terraria;
using Terraria.ModLoader;

namespace CascadeMod;

public sealed class Cascade : Mod {
	public static Cascade Instance { get; private set; }

	public Cascade() {
		Instance = this;
	}

	public sealed override void Load() {
		EstherAssets.Load();
		EstherEffects.Load();
	}

	public override void HandlePacket(BinaryReader reader, int whoAmI) {
		byte messageId = reader.ReadByte();
		switch ((CascadePackets.MessageID)messageId) {
			case CascadePackets.MessageID.GoalCompletion: {
				byte playerIndex = reader.ReadByte();
				byte ordinal = reader.ReadByte();
				byte quoteIndex = reader.ReadByte();

				if (Main.player[playerIndex].TryGetModPlayer(out QuestPlayer questPlayer)) {
					questPlayer.GoalsCompletedByQuest[QuestSystem.quests[quoteIndex].FullName][ordinal] = true;
				}
			}
			break;
			case CascadePackets.MessageID.ClaimQuestRewards: {
				byte playerIndex = reader.ReadByte();
				byte quoteIndex = reader.ReadByte();

				if (Main.player[playerIndex].TryGetModPlayer(out QuestPlayer questPlayer)) {
					questPlayer.CompletedQuests2.Add(QuestSystem.quests[quoteIndex].FullName);
				}
			}
			break;
			case CascadePackets.MessageID.AssignQuest: {
				byte playerIndex = reader.ReadByte();
				byte quoteIndex = reader.ReadByte();
				byte count = reader.ReadByte();

				if (Main.player[playerIndex].TryGetModPlayer(out QuestPlayer questPlayer)) {
					questPlayer.GoalsCompletedByQuest[QuestSystem.quests[quoteIndex].FullName] = new bool[count];
				}
			}
			break;
			case CascadePackets.MessageID.CompleteQuest: {
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