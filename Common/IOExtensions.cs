using System;
using Terraria.ModLoader.IO;

namespace EstherMod.Common;

public static class IOExtensions {
	public static T Get<T>(this TagCompound tagCompound, string key, Func<T> defaultValue) {
		if (tagCompound.TryGet(key, out T value)) {
			return value;
		}
		return defaultValue();
	}
}
