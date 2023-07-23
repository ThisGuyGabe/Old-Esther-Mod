using CascadeMod.Core.Quests;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace CascadeMod.Core.UI;

public sealed class QuestElement : UIPanel {
	public ModQuest quest;
	public bool locked;

	public UIImageFramed image;
	public UIText text;

	public QuestElement(ModQuest quest) {
		this.quest = quest;
		locked = !quest.IsUnlocked();

		Width = StyleDimension.Fill;
		Height = StyleDimension.Fill;

		image = new UIImageFramed(QuestSystem.IconById[quest.Type], new Rectangle(0, 0, 80, 80)) {
			Width = StyleDimension.FromPixels(80f),
			Height = StyleDimension.FromPixels(80f),
			Left = StyleDimension.FromPixels(-5f)
		};
		Append(image);

		var frame = new UIImage(quest.QuestFrame.Texture);
		frame.CopyStyle(image);
		Append(frame);

		text = new UIText(quest.DisplayName, textScale: 1f) {
			Width = StyleDimension.FromPixels(100f),
			Height = StyleDimension.FromPixels(10f),
			Left = StyleDimension.FromPixels(80f),
			HAlign = 0f,
			VAlign = 0f,
			TextOriginX = 0f,
			WrappedTextBottomPadding = 10f,
			IsWrapped = true,
			TextColor = Color.White
		};
		Append(text);
	}

	public override void Update(GameTime gameTime) {
		locked = !quest.IsUnlocked();

		base.Update(gameTime);

		if (locked) {
			image.SetFrame(new Rectangle(80, 0, 80, 80));
			text.TextColor = Color.Gray;
		}
		else if (quest.IsCompleted(Main.LocalPlayer)) {
			text.TextColor = Color.Yellow;
		}
	}

	public override int CompareTo(object obj) {
		if (obj is not QuestElement other)
			return base.CompareTo(obj);
		return quest.Order.CompareTo(other.quest.Order);
	}
}
