using Terraria;
using Terraria.Localization;

namespace EstherMod.Core.Quests;

public abstract class QuestGoal : IQuestProperty {
	public ModQuest Quest { get; set; }
	public int Ordinal { get; set; }

	public void SetComplete(Player player) {
		if (!player.TryGetModPlayer(out QuestPlayer questPlayer))
			return;

		questPlayer.GoalsCompletedByQuest[Quest.FullName][Ordinal] = true;
	}

	/// <summary>
	/// Allows you to create special effects when player or projectile created by player hits an NPC by swinging a melee weapon.
	/// </summary>
	/// <param name="player">The player.</param>
	/// <param name="withEntity"><seealso cref="Item"/> that is used by or <seealso cref="Projectile"/> created by <paramref name="player"/>.</param>
	/// <param name="target">The target <seealso cref="NPC"/>.</param>
	/// <param name="hit"><inheritdoc cref="NPC.HitInfo"/></param>
	/// <param name="damageDone"></param>
	public virtual void OnHitNPCWithAnything(Player player, Entity withEntity, NPC target, NPC.HitInfo hit, int damageDone) {
	}
}
