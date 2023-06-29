using EstherMod.Core;
using Microsoft.Xna.Framework.Graphics;

namespace EstherMod.Common.Extensions;

public static class SpriteBatchExtensions {
	public static SpriteBatchSnapshot TakeSnapshot(this SpriteBatch spriteBatch) => new(spriteBatch);

	public static void Begin(this SpriteBatch spriteBatch, SpriteBatchSnapshot snapshot) {
		spriteBatch.Begin(snapshot.SortMode, snapshot.BlendState, snapshot.SamplerState, snapshot.DepthStencilState, snapshot.RasterizerState, snapshot.Effect, snapshot.TransformationMatrix);
	}
}
