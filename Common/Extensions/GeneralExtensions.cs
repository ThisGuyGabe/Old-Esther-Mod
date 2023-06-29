namespace EstherMod.Common.Extensions;

public static class GeneralExtensions {
	public static T Assign<T>(this T self, out T value) {
		value = self;
		return value;
	}
}
