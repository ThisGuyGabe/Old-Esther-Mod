using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EstherMod.Core;

public static class BaseTypeCommons {
	public static string GetKeyPath(Type type) {
		// Ignore EstherMod and Content folders
		return string.Join('.', type.Namespace.Split('.')[2..]);
	}
}

public abstract class BaseItem : ModItem {
	private string KeyPath => BaseTypeCommons.GetKeyPath(GetType());
	public override LocalizedText DisplayName => Language.GetOrRegister($"Mods.EstherMod.{KeyPath}.DisplayName", PrettyPrintName);
	public override LocalizedText Tooltip => Language.GetOrRegister($"Mods.EstherMod.{KeyPath}.Tooltip", () => string.Empty);
}
public abstract class BaseNPC : ModNPC {
	private string KeyPath => BaseTypeCommons.GetKeyPath(GetType());
	public override LocalizedText DisplayName => Language.GetOrRegister($"Mods.EstherMod.{KeyPath}.DisplayName", PrettyPrintName);
}