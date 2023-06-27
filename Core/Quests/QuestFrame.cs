using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace EstherMod.Core.Quests;

[Autoload(false)]
public sealed record class QuestFrame(in Asset<Texture2D> Texture) : ILoadable {
	public const string IconFrameHunter = "EstherMod/Assets/Quests/IconFrameHunter";
	public const string IconFrameBounty = "EstherMod/Assets/Quests/IconFrameBounty";
	public const string IconFrameMain = "EstherMod/Assets/Quests/IconFrameMain";

	public int Type { get; private set; }

	public void Load(Mod mod) {
		Type = QuestFrames.questFrames.Count;
		QuestFrames.questFrames.Add(this);
	}

	public void Unload() {
	}
}
