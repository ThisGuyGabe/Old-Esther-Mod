using System;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EstherMod.Core;

public struct SpriteBatchSnapshot {
	private sealed record class ReflectionValue<TType, TValue>(in Func<TType, TValue> Getter, in Action<TType, TValue> Setter) {
		public TValue Get(TType type) => Getter(type);

		public void Set(TType type, TValue value) => Setter(type, value);
	}

	private static readonly ReflectionValue<SpriteBatch, SpriteSortMode> spriteSortModeValue;
	private static readonly ReflectionValue<SpriteBatch, BlendState> blendStateValue;
	private static readonly ReflectionValue<SpriteBatch, SamplerState> samplerStateValue;
	private static readonly ReflectionValue<SpriteBatch, DepthStencilState> depthStencilStateValue;
	private static readonly ReflectionValue<SpriteBatch, RasterizerState> rasterizerStateValue;
	private static readonly ReflectionValue<SpriteBatch, Effect> customEffectValue;
	private static readonly ReflectionValue<SpriteBatch, Matrix> transformMatrixValue;

	static SpriteBatchSnapshot() {
		static ReflectionValue<SpriteBatch, T> createValue<T>(string name) {
			static Func<SpriteBatch, T> createGetter(string name) {
				var spriteBatchParameter = Expression.Parameter(typeof(SpriteBatch));

				var getter = Expression.Field(spriteBatchParameter, typeof(SpriteBatch), name);

				return Expression.Lambda<Func<SpriteBatch, T>>(getter, spriteBatchParameter).Compile();
			}
			static Action<SpriteBatch, T> createSetter(string name) {
				var spriteBatchParameter = Expression.Parameter(typeof(SpriteBatch));
				var typeParameter = Expression.Parameter(typeof(T));

				var getter = Expression.Field(spriteBatchParameter, typeof(SpriteBatch), name);
				var setter = Expression.Assign(getter, typeParameter);

				return Expression.Lambda<Action<SpriteBatch, T>>(setter, spriteBatchParameter, typeParameter).Compile();
			}

			return new ReflectionValue<SpriteBatch, T>(createGetter(name), createSetter(name));
		}

		spriteSortModeValue = createValue<SpriteSortMode>("sortMode");
		blendStateValue = createValue<BlendState>("blendState");
		samplerStateValue = createValue<SamplerState>("samplerState");
		depthStencilStateValue = createValue<DepthStencilState>("depthStencilState");
		rasterizerStateValue = createValue<RasterizerState>("rasterizerState");
		customEffectValue = createValue<Effect>("customEffect");
		transformMatrixValue = createValue<Matrix>("transformMatrix");
	}

	public SpriteSortMode SortMode { get; set; }
	public BlendState BlendState { get; set; }
	public SamplerState SamplerState { get; set; }
	public DepthStencilState DepthStencilState { get; set; }
	public RasterizerState RasterizerState { get; set; }
	public Effect Effect { get; set; }
	public Matrix TransformationMatrix { get; set; }

	public SpriteBatchSnapshot(SpriteBatch spriteBatch) {
		SortMode = spriteSortModeValue.Get(spriteBatch);
		BlendState = blendStateValue.Get(spriteBatch);
		SamplerState = samplerStateValue.Get(spriteBatch);
		DepthStencilState = depthStencilStateValue.Get(spriteBatch);
		RasterizerState = rasterizerStateValue.Get(spriteBatch);
		Effect = customEffectValue.Get(spriteBatch);
		TransformationMatrix = transformMatrixValue.Get(spriteBatch);
	}
}
