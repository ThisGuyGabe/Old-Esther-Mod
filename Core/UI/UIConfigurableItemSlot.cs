using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace EstherMod.Core.UI;

public sealed class UIConfigurableItemSlot : UIElement {
	public sealed class Options {
		public bool IsLeftClickable { get; set; }
		public bool IsRightClickable { get; set; }
		public bool OverrideHover { get; set; }

		public Options(
			bool isLeftClickable = true,
			bool isRightClickable = true,
			bool overrideHover = true) {
			IsLeftClickable = isLeftClickable;
			IsRightClickable = isRightClickable;
			OverrideHover = overrideHover;
		}
	}

	private readonly int context;
	private readonly Options options;

	public Item Item { get; set; }
	public float Scale { get; set; } = 1f;
	public Options Option => options;
	public Color Color { get; set; } = Color.White;

	public UIConfigurableItemSlot(Item item, int context, in Options options) {
		Item = item;
		this.context = context;
		this.options = options;

		Width = StyleDimension.FromPixels(48f);
		Height = StyleDimension.FromPixels(48f);
	}

	private void HandleItemSlotLogic() {
		if (IsMouseHovering) {
			Main.LocalPlayer.mouseInterface = true;

			var item = Item;

			if (options.OverrideHover)
				ItemSlot.OverrideHover(ref item, context);
			if (options.IsLeftClickable)
				ItemSlot.LeftClick(ref item, context);
			if (options.IsRightClickable)
				ItemSlot.RightClick(ref item, context);
			ItemSlot.MouseHover(ref item, context);

			Main.HoverItem = item;
			Main.instance.MouseText(Main.HoverItem.HoverName);

			Item = item;
		}
	}

	public override void DrawSelf(SpriteBatch spriteBatch) {
		var scale = Main.inventoryScale;
		Main.inventoryScale = Scale;

		HandleItemSlotLogic();

		var position = GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;

		var itemArray = new Item[1] { Item };
		ItemSlot.Draw(spriteBatch, itemArray, context, 0, position);
		Item = itemArray[0];

		Main.inventoryScale = scale;
	}
}
