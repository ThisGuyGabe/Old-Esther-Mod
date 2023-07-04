using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace EstherMod.Core.Fusions;

public interface IItemOnFuseHook {
	public static readonly GlobalHookList<GlobalItem> Hook = ItemLoader.AddModHook(new GlobalHookList<GlobalItem>(typeof(IItemOnFuseHook).GetMethod(nameof(OnFuse))));

	void OnFuse(Item item);

	public static void Invoke(Item item) {
		foreach (IItemOnFuseHook g in Hook.Enumerate(item)) {
			g.OnFuse(item);
		}
	}
}
