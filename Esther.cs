using Terraria.ModLoader;
using EstherMod.Content;

namespace EstherMod;

public sealed class Esther : Mod {
	public static Esther Instance { get; private set; }

	public Esther() {
		Instance = this;
	}

    public sealed override void Load() {
		EstherEffects.Load();
    }

	public sealed override void Unload() {
		Instance = null;
	}
}