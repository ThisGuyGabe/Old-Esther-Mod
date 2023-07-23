namespace CascadeMod.Core.Fusions;

public sealed record class Fusion(in int Main, in int Secondary, in int Result) {
	public static Fusion Create(int first, int second, int result) => new(first, second, result);

	public void Register() {
		FusionDatabase.Add(this);
	}
}
