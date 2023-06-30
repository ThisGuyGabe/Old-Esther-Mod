using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EstherMod.Core;

public static class BaseTypeCommons {
	public static string GetKeyPath(Type type) {
		// Ignore EstherMod and Content folders
		var split = type.Namespace.Split('.');
		var skip = split[2..];
		return string.Join('.', skip);
	}
}

public abstract class BaseItem : ModItem {
	private string KeyPath => BaseTypeCommons.GetKeyPath(GetType());
	public override LocalizedText DisplayName => Language.GetOrRegister($"Mods.EstherMod.{KeyPath}.{Name}.DisplayName", PrettyPrintName);
	public override LocalizedText Tooltip => Language.GetOrRegister($"Mods.EstherMod.{KeyPath}.{Name}.Tooltip", () => string.Empty);
}
public abstract class BaseNPC : ModNPC {
	private string KeyPath => BaseTypeCommons.GetKeyPath(GetType());
	public override LocalizedText DisplayName => Language.GetOrRegister($"Mods.EstherMod.{KeyPath}.{Name}.DisplayName", PrettyPrintName);
}