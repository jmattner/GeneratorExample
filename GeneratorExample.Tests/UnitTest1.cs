using GeneratorExample.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace GeneratorExample.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        const string decoratedSource = """
                                       using GeneratorExample.Generator;
                                       
                                       namespace GeneratorExample.Tests

                                       [MyMarker("success")]
                                       public partial class DecoratedClass
                                       {
                                       }
                                       """;

        IEnumerable<string> sources = [decoratedSource];

        IEnumerable<SyntaxTree> syntaxTrees = sources.Select(source => CSharpSyntaxTree.ParseText(source));
        IEnumerable<PortableExecutableReference> references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        CSharpCompilation compilation = CSharpCompilation.Create(
            "Tests",
            syntaxTrees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        var generator = new MyGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGenerators(compilation);

        // TODO - assert things
    }
}