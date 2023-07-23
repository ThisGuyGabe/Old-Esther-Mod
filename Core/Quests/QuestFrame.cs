using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CascadeMod.Core.Quests;

[Autoload(false)]
public sealed record class QuestFrame(in Asset<Texture2D> Texture) : ILoadable {
	public const string IconFrameHunter = "CascadeMod/Assets/Quests/IconFrameHunter";
	public const string IconFrameBounty = "CascadeMod/Assets/Quests/IconFrameBounty";
	public const string IconFrameMain = "CascadeMod/Assets/Quests/IconFrameMain";

	public Mod Mod { get; private set; }
	public int Type { get; private set; }

	public void Load(Mod mod) {
		Mod = mod;

		Type = QuestFrames.questFrames.Count;
		QuestFrames.questFrames.Add(this);
	}

	public void Unload() {
		Mod = null;
	}
}
