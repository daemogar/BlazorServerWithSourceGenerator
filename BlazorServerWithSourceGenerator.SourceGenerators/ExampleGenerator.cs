using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace BlazorServerWithSourceGenerator.SourceGenerators;

[Generator]
public class ExampleGenerator : ISourceGenerator
{
	public void Execute(GeneratorExecutionContext context)
	{
		if(!Debugger.IsAttached)
			Debugger.Launch();

		var json = JsonSerializer.Serialize(new { Hello = "World" });
		var text = $@"
namespace Hello;

public class World
{{
	public static string Message() => ""Hello World!"";
	public static string JsonString() => @""{json.Replace('"', '`')}"";
}}
";

		context.AddSource("HelloWorld.cs", SourceText.From(text, Encoding.UTF8));
	}

	public void Initialize(GeneratorInitializationContext context)
	{
	}
}
