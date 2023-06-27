using EstherMod.Core.Quests;
using Terraria.ModLoader;

namespace EstherMod;

public static class EstherPackets {
	public enum MessageID {
		/// <summary>
		/// byte playerIndex<br/>
		/// byte ordinal<br/>
		/// byte quoteIndex<br/>
		/// </summary>
		GoalCompletion,
		/// <summary>
		/// byte playerIndex<br/>
		/// byte quoteIndex<br/>
		/// </summary>
		ClaimQuestRewards,
		/// <summary>
		/// byte playerIndex<br/>
		/// byte quoteIndex<br/>
		/// byte count<br/>
		/// </summary>
		AssignQuest,
		/// <summary>
		/// byte playerIndex<br/>
		/// byte quoteIndex<br/>
		/// </summary>
		CompleteQuest,
	}

	public static void Packet_GoalCompletion(
		this Esther esther,
		int playerIndex,
		int ordinal,
		string quote
		) {
		var packet = esther.GetPacket();
		packet.Write((byte)MessageID.GoalCompletion);
		packet.Write((byte)playerIndex);
		packet.Write((byte)ordinal);
		packet.Write((byte)QuestSystem.questsByName[quote].Type);
		packet.Send(-1, playerIndex);
	}

	public static void Packet_ClaimQuestRewards(
		this Esther esther,
		int playerIndex,
		string quote
		) {
		var packet = esther.GetPacket();
		packet.Write((byte)MessageID.ClaimQuestRewards);
		packet.Write((byte)playerIndex);
		packet.Write((byte)QuestSystem.questsByName[quote].Type);
		packet.Send(-1, playerIndex);
	}

	public static void Packet_AssignQuest(
		this Esther esther,
		int playerIndex,
		string quote,
		int count
		) {
		var packet = esther.GetPacket();
		packet.Write((byte)MessageID.AssignQuest);
		packet.Write((byte)playerIndex);
		packet.Write((byte)QuestSystem.questsByName[quote].Type);
		packet.Write((byte)count);
		packet.Send(-1, playerIndex);
	}

	public static void Packet_CompleteQuest(
		this Esther esther,
		int playerIndex,
		string quote
		) {
		var packet = esther.GetPacket();
		packet.Write((byte)MessageID.CompleteQuest);
		packet.Write((byte)playerIndex);
		packet.Write((byte)QuestSystem.questsByName[quote].Type);
		packet.Send(-1, playerIndex);
	}
}
