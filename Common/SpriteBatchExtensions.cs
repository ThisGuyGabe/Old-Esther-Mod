using EstherMod.Core;
using Microsoft.Xna.Framework.Graphics;

namespace EstherMod.Common;

public static class SpriteBatchExtensions {
	public static SpriteBatchSnapshot TakeSnapshot(this SpriteBatch spriteBatch) => new(spriteBatch);
}
