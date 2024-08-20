using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace GeneratorExample.Generator;

[Generator]
public class MyGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(c =>
        {
            c.AddSource(
                "MyMarkerAttribute.g.cs",
                SourceText.From("""
                                namespace GeneratorExample.Generator;

                                [System.AttributeUsage(System.AttributeTargets.Class)]
                                public class MyMarkerAttribute : System.Attribute
                                {
                                    public MyMarkerAttribute(string foo) {
                                        Foo = foo;
                                    }
                                    
                                    public string Foo { get; }
                                }
                                """, Encoding.UTF8));
        });

        IncrementalValuesProvider<string> pipeline = context.SyntaxProvider.ForAttributeWithMetadataName<string>(
            fullyQualifiedMetadataName: "GeneratorExample.Generator.MyMarkerAttribute",
            predicate: static (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax,
            transform: static (c, _) =>
            {
                // TODO - null checks etc.
                AttributeData attribute = c.Attributes.First();

                return "breakpoint here";
            });

        context.RegisterSourceOutput(pipeline, static (c, model) =>
        {
            // TODO - generate the things
        });
    }
}