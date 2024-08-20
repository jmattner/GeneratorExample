namespace GeneratorExample.Generator;

[AttributeUsage(AttributeTargets.Class)]
public class MyMarkerAttribute : Attribute
{
    public MyMarkerAttribute(string foo)
    {
        Foo = foo;
    }

    public string Foo { get; }
}