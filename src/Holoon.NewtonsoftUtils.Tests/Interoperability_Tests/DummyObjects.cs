using Holoon.NewtonsoftUtils.CanBeUndefined;

namespace Holoon.NewtonsoftUtils.Tests.Interoperability_Tests;

public class Container
{
    public CanBeUndefined<Contained> Property1 { get; set; }
}
public class Contained
{
    public string Property1 { get; set; }
}
