using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.IO;

namespace EstherMod.Common.Extensions;

public static class IOExtensions {
	public static void AddDictionary<TKey, TValue>(this TagCompound tagCompound, string key, IReadOnlyDictionary<TKey, TValue> value) {
		var dictTag = new TagCompound {
			["Keys"] = value.Keys.ToList(),
			["Values"] = value.Values.ToList()
		};
		tagCompound[key] = dictTag;
	}

	public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(this TagCompound tagCompound, string key) {
		var dictTag = tagCompound.Get<TagCompound>(key);
		return dictTag.Get<List<TKey>>("Keys").Zip(dictTag.Get<List<TValue>>("Values"), (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
	}

	public static bool TryGetDictionary<TKey, TValue>(this TagCompound tagCompound, string key, out Dictionary<TKey, TValue> value) {
		value = null;
		if (tagCompound.TryGet(key, out TagCompound dictTag)) {
			value = dictTag.Get<List<TKey>>("Keys").Zip(dictTag.Get<List<TValue>>("Values"), (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
			return true;
		}
		return false;
	}
}
