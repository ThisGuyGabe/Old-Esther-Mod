using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace EstherMod.Core.UI;

public sealed class UITextWScrollbar : UIElement {
	private sealed class UIInnerText : UIText {
		public UIInnerText(string text, float textScale = 1, bool large = false) : base(text, textScale, large) {
		}

		public UIInnerText(LocalizedText text, float textScale = 1, bool large = false) : base(text, textScale, large) {
		}

		public override bool ContainsPoint(Vector2 point) {
			return true;
		}

		public override void Draw(SpriteBatch spriteBatch) {
			var parentPos = Parent.GetDimensions().Position();
			var parentDim = new Vector2(Parent.GetDimensions().Width, Parent.GetDimensions().Height);

			var thisPos = GetDimensions().Position();
			var thisDim = new Vector2(GetDimensions().Width, GetDimensions().Height);

			if (Collision.CheckAABBvAABBCollision(parentPos, parentDim, thisPos, thisDim)) {
				base.Draw(spriteBatch);
			}
		}

		public override Rectangle GetViewCullingArea() {
			return Parent.GetDimensions().ToRectangle();
		}
	}

	private readonly UIInnerText _innerText;
	private float _textHeight;
	private UIScrollbar _scrollbar;

	public float WrappedTextBottomPadding {
		get => _innerText.WrappedTextBottomPadding;
		set => _innerText.WrappedTextBottomPadding = value;
	}
	public bool IsWrapped {
		get => _innerText.IsWrapped;
		set => _innerText.IsWrapped = value;
	}
	public float ViewPosition {
		get => _scrollbar.ViewPosition;
		set => _scrollbar.ViewPosition = value;
	}

	public UITextWScrollbar(string text, float textScale = 1, bool large = false, Vector2? origin = null) {
		_innerText = new(text, textScale, large);
		InnerSet(origin);
	}

	public UITextWScrollbar(LocalizedText text, float textScale = 1, bool large = false, Vector2? origin = null) {
		_innerText = new(text, textScale, large);
		InnerSet(origin);
	}

	private void InnerSet(Vector2? origin) {
		_innerText.Width = StyleDimension.Fill;
		_innerText.Height = StyleDimension.Fill;
		_innerText.OverflowHidden = false;
		_innerText.IsWrapped = true;

		if (origin.HasValue) {
			_innerText.TextOriginX = origin.Value.X;
			_innerText.TextOriginY = origin.Value.Y;
		}

		OverflowHidden = true;
		Append(_innerText);
	}

	public void SetText(string text) => _innerText.SetText(text);
	public void SetText(LocalizedText text) => _innerText.SetText(text);
	public void SetText(string text, float textScale, bool large) => _innerText.SetText(text, textScale, large);
	public void SetText(LocalizedText text, float textScale, bool large) => _innerText.SetText(text, textScale, large);

	public override void Recalculate() {
		base.Recalculate();
		UpdateScrollbar();
	}

	public override void ScrollWheel(UIScrollWheelEvent evt) {
		base.ScrollWheel(evt);
		if (_scrollbar != null) {
			_scrollbar.ViewPosition -= evt.ScrollWheelValue;
		}
	}

	public override void RecalculateChildren() {
		base.RecalculateChildren();
		_textHeight = _innerText.GetOuterDimensions().Height;
	}

	private void UpdateScrollbar() {
		_scrollbar?.SetView(GetInnerDimensions().Height, _textHeight);
	}

	public void SetScrollbar(UIScrollbar scrollbar) {
		_scrollbar = scrollbar;
		UpdateScrollbar();
	}

	public override void DrawSelf(SpriteBatch spriteBatch) {
		if (_scrollbar != null) {
			_innerText.Top.Set(0f - _scrollbar.GetValue(), 0f);
		}
		Recalculate();

		base.DrawSelf(spriteBatch);
	}
}
