using System;
using System.Runtime.CompilerServices;

namespace EstherMod.Core.Fusions;

public sealed record class Fusion(in int Main, in int Secondary, in int Result) {
	public static Fusion Create(int first, int second, int result) => new(first, second, result);

	public void Register() {
		if (FusionDatabase.hasSetup)
			Throw();

		FusionDatabase.Add(this);

		[MethodImpl(MethodImplOptions.NoInlining)]
		static void Throw() {
			throw new InvalidOperationException("Unable to register fusion because it's already too late to register new ones!");
		}
	}
}
