using EstherMod.Content;
using Terraria;
using Terraria.ModLoader;

namespace EstherMod;

public sealed class EstherPlayer : ModPlayer {
	public bool partyBombActive;
	public float bombStart;
	private float bombProgress;

	public override void PostUpdate() {
		if (partyBombActive) {
			if (bombProgress < 260) {
				bombProgress = Main.GlobalTimeWrappedHourly - bombStart;
				EstherEffects.Confetti.GetShader().UseProgress(bombProgress * 0.725f);
			}
		}
	}
}