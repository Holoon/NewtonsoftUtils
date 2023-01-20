using Holoon.NewtonsoftUtils.CanBeUndefined;
using Holoon.NewtonsoftUtils.Trimming;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Holoon.NewtonsoftUtils.Tests.Interoperability_Tests;

public class InteroperabilityTests
{
    [Test]
    public void UsingTrimmingAndCanBeUndefinedTogether()
    {
        var testObject = new Container { Property1 = new Contained { Property1 = "42 " } };

        var settings = new Newtonsoft.Json.JsonSerializerSettings();

        // NOTE: Because the `Equals` of `CanBeUndefined` is override to return `CanBeUndefined<MyClass> == MyClass`, the `TrimmingConverter` think there is a "Self referencing loop detected" when trimming a `CanBeUndefined<MyClass>`.
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;

        var trimmingConverter = new TrimmingConverter(TrimmingOption.TrimEnd, TrimmingOption.TrimEnd);
        settings.Converters.Add(trimmingConverter);
        settings.ContractResolver = new CanBeUndefinedResolver();

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);
        Assert.AreEqual("{\"Property1\":{\"Property1\":\"42\"}}", json);
    }
}
