using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace CascadeMod.Core;

public static class ILHelper {
	public static int AddVariable<T>(this ILCursor c) => c.Context.AddVariable<T>();
	public static int AddVariable(this ILCursor c, Type type) => c.Context.AddVariable(type);
	public static int AddVariable(this ILCursor c, TypeReference typeDefinition) => c.Context.AddVariable(typeDefinition);
	public static int AddVariable(this ILCursor c, VariableDefinition variableDefinition) => c.Context.AddVariable(variableDefinition);

	public static int AddVariable<T>(this ILContext context) => context.AddVariable(typeof(T));
	public static int AddVariable(this ILContext context, Type type) => context.AddVariable(new VariableDefinition(context.Import(type)));
	public static int AddVariable(this ILContext context, TypeReference typeDefinition) => context.AddVariable(new VariableDefinition(typeDefinition));
	public static int AddVariable(this ILContext context, VariableDefinition variableDefinition) {
		context.Body.Variables.Add(variableDefinition);
		return context.Body.Variables.Count - 1;
	}
}
