using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace CascadeMod.Common;

public class DynamicShaderData : ShaderData {
	public delegate void ApplyDelegate(ShaderData shaderData, DrawData? drawData = null);

	private readonly ApplyDelegate applyHook;

	public DynamicShaderData(ApplyDelegate apply, Effect effect, string passName) : this(apply, new Ref<Effect>(effect), passName) { }
	public DynamicShaderData(ApplyDelegate apply, Ref<Effect> shader, string passName) : base(shader, passName) {
		applyHook = apply;
	}

	public virtual void Apply(DrawData? drawData = null) {
		applyHook(this, drawData);
		base.Apply();
	}
}
