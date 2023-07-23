using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace CascadeMod.Core.Quests;

public sealed class QuestFrames : ILoadable {
	internal static List<QuestFrame> questFrames = new();
	public static QuestFrame Hunter { get; private set; }
	public static QuestFrame Bounty { get; private set; }
	public static QuestFrame Main { get; private set; }

	public void Load(Mod mod) {
		mod.AddContent(Hunter = new QuestFrame(ModContent.Request<Texture2D>(QuestFrame.IconFrameHunter)));
		mod.AddContent(Bounty = new QuestFrame(ModContent.Request<Texture2D>(QuestFrame.IconFrameBounty)));
		mod.AddContent(Main = new QuestFrame(ModContent.Request<Texture2D>(QuestFrame.IconFrameMain)));
	}

	public void Unload() {
		questFrames.Clear();
		questFrames = null;
	}
}
