using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EstherMod.Common.Extensions;

public static class DictionaryExtensions {
	/// <inheritdoc cref="GetValueOrDefault{TKey, TValue}(Dictionary{TKey, TValue}, TKey, Func{TValue})"/>
	public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) {
		return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
	}

	/// <inheritdoc cref="GetValueOrDefault{TKey, TValue}(Dictionary{TKey, TValue}, TKey, Func{TValue})"/>
	public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValue) {
		return dictionary.TryGetValue(key, out var value) ? value : defaultValue();
	}

	/// <inheritdoc cref="GetValueOrDefault{TKey, TValue}(Dictionary{TKey, TValue}, TKey, Func{TValue})"/>
	public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) {
		ref TValue value = ref CollectionsMarshal.GetValueRefOrNullRef(dictionary, key);
		return Unsafe.IsNullRef(ref value) ? defaultValue : value;
	}

	/// <summary>
	/// Gets the value associated with key. If specified key does not exists in the <paramref name="dictionary"/>.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <param name="dictionary">A collection of keys and values.</param>
	/// <param name="key"></param>
	/// <param name="defaultValue">Default value to return if key does not exists in the <paramref name="dictionary"/>.</param>
	/// <returns>If specified key exists in the <paramref name="dictionary"/>, returns value associated with it; otherwise returns <paramref name="defaultValue"/>.</returns>
	public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValue) {
		ref TValue value = ref CollectionsMarshal.GetValueRefOrNullRef(dictionary, key);
		return Unsafe.IsNullRef(ref value) ? defaultValue() : value;
	}

	/// <inheritdoc cref="GetOrAddValue{TKey, TValue}(Dictionary{TKey, TValue}, TKey, Func{TValue})"/>
	public static TValue GetOrAddValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) {
		return dictionary.TryAdd(key, defaultValue) ? defaultValue : dictionary[key];
	}

	/// <inheritdoc cref="GetOrAddValue{TKey, TValue}(Dictionary{TKey, TValue}, TKey, Func{TValue})"/>
	public static TValue GetOrAddValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValue) {
		dictionary.TryAdd(key, defaultValue());
		return dictionary[key];
	}

	/// <inheritdoc cref="GetOrAddValue{TKey, TValue}(Dictionary{TKey, TValue}, TKey, Func{TValue})"/>
	public static TValue GetOrAddValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) {
		ref TValue value = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, key, out bool exists);
		if (!exists) {
			value = defaultValue;
		}
		return value;
	}

	/// <summary>
	/// Gets the value associated with <paramref name="key"/>. If specified key does not exists in <paramref name="dictionary"/>, then adds a new entry and sets its value to <paramref name="defaultValue"/>.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the <paramref name="dictionary"/>.</typeparam>
	/// <typeparam name="TValue">The type of the values in the <paramref name="dictionary"/>.</typeparam>
	/// <param name="dictionary">A collection of keys and values.</param>
	/// <param name="key"></param>
	/// <param name="defaultValue">Default value to add if key does not exists in the <paramref name="dictionary"/>.</param>
	/// <returns>If specified key exists in the <paramref name="dictionary"/>, returns a value associated with it; otherwise returns <paramref name="defaultValue"/>.</returns>
	public static TValue GetOrAddValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValue) {
		ref TValue value = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, key, out bool exists);
		if (!exists) {
			value = defaultValue();
		}
		return value;
	}
}
