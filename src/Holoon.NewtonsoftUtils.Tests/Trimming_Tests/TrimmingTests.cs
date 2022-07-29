using Holoon.NewtonsoftUtils.Trimming;
using NUnit.Framework;
using System;
using System.Linq;

namespace Holoon.NewtonsoftUtils.Tests.Trimming_Tests;

public class TrimmingTests
{
    private const string K_TEST_TEXT = "    => A Long Text With Spaces At The End And The Start...     ";
    private const string K_TEST_TEXT_TRIMMED_BOTH = "=> A Long Text With Spaces At The End And The Start...";
    private const string K_TEST_TEXT_TRIMMED_END = "    => A Long Text With Spaces At The End And The Start...";
    private const string K_TEST_TEXT_TRIMMED_START = "=> A Long Text With Spaces At The End And The Start...     ";

    [Test]
    public void Read_SpacedString([Values] TrimmingOption trimOption, [Values(K_TEST_TEXT)] string expectedResult)
    {
        var json = $"{{\"Property1\":\"{K_TEST_TEXT}\"}}";

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SpacedStringObject>(json, settings);

        Assert.AreEqual(expectedResult, (string)testObject.Property1);
    }

    [Test]
    public void Read_SpacedString_Attribute([Values] TrimmingOption trimOption, [Values(K_TEST_TEXT)] string expectedResult)
    {
        var json = $"{{\"Property1\":\"{K_TEST_TEXT}\"}}";

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SpacedStringAttributeObject>(json, settings);

        Assert.AreEqual(expectedResult, testObject.Property1);
    }

    [TestCase(TrimmingOption.NoTrim, K_TEST_TEXT, K_TEST_TEXT)]
    [TestCase(TrimmingOption.TrimBoth, K_TEST_TEXT, K_TEST_TEXT_TRIMMED_BOTH)]
    [TestCase(TrimmingOption.TrimEnd, K_TEST_TEXT, K_TEST_TEXT_TRIMMED_END)]
    [TestCase(TrimmingOption.TrimStart, K_TEST_TEXT, K_TEST_TEXT_TRIMMED_START)]
    public void Read_SpacedString_FluentConfig([Values] TrimmingOption trimOption, [Values] string expectedResult1, [Values] string expectedResult2)
    {
        var json = $"{{\"Property1\":\"{K_TEST_TEXT}\",\"Property2\":\"{K_TEST_TEXT}\"}}";

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        var converter = new TrimmingConverter(trimOption, trimOption);
        converter.StringPropertiesToNotTrim.Add<NormalStringsObject>(o => o.Property1);
        settings.Converters.Add(converter);

        var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<NormalStringsObject>(json, settings);

        Assert.AreEqual(expectedResult1, testObject.Property1);
        Assert.AreEqual(expectedResult2, testObject.Property2);
    }

    [TestCase(TrimmingOption.NoTrim, K_TEST_TEXT)]
    [TestCase(TrimmingOption.TrimBoth, K_TEST_TEXT_TRIMMED_BOTH)]
    [TestCase(TrimmingOption.TrimEnd, K_TEST_TEXT_TRIMMED_END)]
    [TestCase(TrimmingOption.TrimStart, K_TEST_TEXT_TRIMMED_START)]
    public void Read_NormalString(TrimmingOption trimOption, string expectedResult)
    {
        var json = $"{{\"Property1\":\"{K_TEST_TEXT}\"}}";

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<NormalStringObject>(json, settings);

        Assert.AreEqual(expectedResult, testObject.Property1);
    }

    [TestCase(TrimmingOption.NoTrim, K_TEST_TEXT)]
    [TestCase(TrimmingOption.TrimBoth, K_TEST_TEXT_TRIMMED_BOTH)]
    [TestCase(TrimmingOption.TrimEnd, K_TEST_TEXT_TRIMMED_END)]
    [TestCase(TrimmingOption.TrimStart, K_TEST_TEXT_TRIMMED_START)]
    public void Read_NormalString_WithConstructor(TrimmingOption trimOption, string expectedResult)
    {
        var json = $"{{\"Property1\":\"{K_TEST_TEXT}\"}}";

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<NormalStringCtorObject>(json, settings);

        Assert.AreEqual(expectedResult, testObject.Property1);
    }

    [Test]
    public void Write_SpacedString([Values] TrimmingOption trimOption, [Values(K_TEST_TEXT)] string expectedResult)
    {
        var testObject = new SpacedStringObject
        {
            Property1 = K_TEST_TEXT
        };

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

        var expected = $"{{\"Property1\":\"{expectedResult}\"}}";
        Assert.AreEqual(expected, json);
    }

    [TestCase(TrimmingOption.NoTrim, K_TEST_TEXT, K_TEST_TEXT)]
    [TestCase(TrimmingOption.TrimBoth, K_TEST_TEXT, K_TEST_TEXT_TRIMMED_BOTH)]
    [TestCase(TrimmingOption.TrimEnd, K_TEST_TEXT, K_TEST_TEXT_TRIMMED_END)]
    [TestCase(TrimmingOption.TrimStart, K_TEST_TEXT, K_TEST_TEXT_TRIMMED_START)]
    public void Write_SpacedString_FluentConfig([Values] TrimmingOption trimOption, [Values] string expectedResult1, [Values] string expectedResult2)
    {
        var testObject = new NormalStringsObject
        {
            Property1 = K_TEST_TEXT,
            Property2 = K_TEST_TEXT
        };

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        var converter = new TrimmingConverter(trimOption, trimOption);
        converter.StringPropertiesToNotTrim.Add<NormalStringsObject>(o => o.Property1);
        settings.Converters.Add(converter);

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

        var expected = $"{{\"Property1\":\"{expectedResult1}\",\"Property2\":\"{expectedResult2}\"}}";
        Assert.AreEqual(expected, json);
    }

    [Test]
    public void Write_SpacedString_Attribute([Values] TrimmingOption trimOption, [Values(K_TEST_TEXT)] string expectedResult)
    {
        var testObject = new SpacedStringAttributeObject
        {
            Property1 = K_TEST_TEXT
        };

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

        var expected = $"{{\"Property1\":\"{expectedResult}\"}}";
        Assert.AreEqual(expected, json);
    }

    [TestCase(TrimmingOption.NoTrim, K_TEST_TEXT)]
    [TestCase(TrimmingOption.TrimBoth, K_TEST_TEXT_TRIMMED_BOTH)]
    [TestCase(TrimmingOption.TrimEnd, K_TEST_TEXT_TRIMMED_END)]
    [TestCase(TrimmingOption.TrimStart, K_TEST_TEXT_TRIMMED_START)]
    public void Write_NormalString(TrimmingOption trimOption, string expectedResult)
    {
        var testObject = new NormalStringObject
        {
            Property1 = K_TEST_TEXT
        };

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

        var expected = $"{{\"Property1\":\"{expectedResult}\"}}";
        Assert.AreEqual(expected, json);
    }

    [Test]
    public void Write_WrongTypeAttribute([Values] TrimmingOption trimOption)
    {
        var testObject = new WrongTypeAttributeObject
        {
            Property1 = 42
        };

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

        var expected = $"{{\"Property1\":42}}";
        Assert.AreEqual(expected, json);
    }

    [Test]
    public void Read_WrongTypeAttribute([Values] TrimmingOption trimOption)
    {
        var json = $"{{\"Property1\":42}}";

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<WrongTypeAttributeObject>(json, settings);

        Assert.AreEqual(42, testObject.Property1);
    }

    [Test]
    public void Write_StringNoSettable([Values] TrimmingOption trimOption)
    {
        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(new StringNoSettableObject(), settings);

        var expected = $"{{\"Property1\":\"  You can not Trim this even if you want to !  \"}}";
        Assert.AreEqual(expected, json);
    }

    [Test]
    public void Write_CamelCaseCompatibility()
    {
        var testObject = new NormalSubStringObject
        {
            Property1 = K_TEST_TEXT,
            Sub = new NormalStringsObject
            {
                Property1 = K_TEST_TEXT,
                Property2 = K_TEST_TEXT
            }
        };
            
        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        var resolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() { NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy() };
        settings.ContractResolver = resolver;

        settings.Converters.Add(new TrimmingConverter(TrimmingOption.TrimBoth, TrimmingOption.TrimBoth));

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

        var expected = $"{{\"property1\":\"{K_TEST_TEXT_TRIMMED_BOTH}\",\"sub\":{{\"property1\":\"{K_TEST_TEXT_TRIMMED_BOTH}\",\"property2\":\"{K_TEST_TEXT_TRIMMED_BOTH}\"}}}}";
        Assert.AreEqual(expected, json);
    }

    [Test]
    public void Write_CamelCase_And_OtherConverters_Compatibility()
    {
        var testObject = new NormalSubStringObject
        {
            Property1 = K_TEST_TEXT,
            Sub = new NormalStringsObject
            {
                Property1 = K_TEST_TEXT,
                Property2 = K_TEST_TEXT
            }
        };

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        var resolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() { NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy() };
        settings.ContractResolver = resolver;

        settings.Converters.Add(new TrimmingConverter(TrimmingOption.TrimBoth, TrimmingOption.TrimBoth));
        settings.Converters.Add(new SimpleTestConverter());
            
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

        var expected = $"{{\"property1\":\"{K_TEST_TEXT_TRIMMED_BOTH + SimpleTestConverter.ADDING}\",\"sub\":{{\"property1\":\"{K_TEST_TEXT_TRIMMED_BOTH + SimpleTestConverter.ADDING}\",\"property2\":\"{K_TEST_TEXT_TRIMMED_BOTH + SimpleTestConverter.ADDING}\"}}}}";
        Assert.AreEqual(expected, json);
    }

    [TestCase(TrimmingOption.NoTrim, K_TEST_TEXT)]
    [TestCase(TrimmingOption.TrimBoth, K_TEST_TEXT_TRIMMED_BOTH)]
    [TestCase(TrimmingOption.TrimEnd, K_TEST_TEXT_TRIMMED_END)]
    [TestCase(TrimmingOption.TrimStart, K_TEST_TEXT_TRIMMED_START)]
    public void Read_NormalString_TabWithSubObjects(TrimmingOption trimOption, string expectedResult)
    {
        var json = "{" +
                "\"Property1\":[" +
                    "{" +
                        $"\"Property1\":\"{K_TEST_TEXT}\"," +
                        "\"Sub\":null," +
                    "},{" +
                        $"\"Property1\":\"{K_TEST_TEXT}\"," +
                        "\"Sub\":{" +
                            $"\"Property1\":\"{K_TEST_TEXT}\"," +
                            "\"Property2\":null" +
                        "}," +
                    "}]" +
            "}";

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TabObject>(json, settings);

        Assert.IsNotNull(testObject.Property1);
        Assert.AreEqual(expected: 2, testObject.Property1.Length);
        Assert.AreEqual(expected: expectedResult, testObject.Property1[0].Property1);
        Assert.IsNull(testObject.Property1[0].Sub);
        Assert.AreEqual(expected: expectedResult, testObject.Property1[1].Property1);
        Assert.IsNotNull(testObject.Property1[1].Sub);
        Assert.AreEqual(expected: expectedResult, testObject.Property1[1].Sub.Property1);
        Assert.IsNull(testObject.Property1[1].Sub.Property2);
    }

    //[TestCase(TrimmingOption.NoTrim, K_TEST_TEXT)]
    //[TestCase(TrimmingOption.TrimBoth, K_TEST_TEXT_TRIMMED_BOTH)]
    [TestCase(TrimmingOption.TrimEnd, K_TEST_TEXT_TRIMMED_END)]
    //[TestCase(TrimmingOption.TrimStart, K_TEST_TEXT_TRIMMED_START)]
    public void Read_MultiplesCollection_PrivateSetter(TrimmingOption trimOption, string expectedResult)
    {
        var json = $@"
{{
  ""Container"": {{
    ""Collection1"": [
      {{
        ""Property2"": ""{K_TEST_TEXT}"",
        ""Collection2"": [
          {{
            ""Property1"": ""{K_TEST_TEXT}""
          }}
        ]
      }},
      {{
        ""Property2"": ""{K_TEST_TEXT}"",
        ""Collection2"": [
          {{
            ""Property1"": ""{K_TEST_TEXT}""
          }},
		  {{
            ""Property1"": ""{K_TEST_TEXT}""
          }}
        ]
      }}
    ]
  }}
}}";

        var settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.Converters.Add(new TrimmingConverter(trimOption, trimOption));

        var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CollectionContainer>(json, settings);

        Assert.IsNotNull(testObject.Container);
        var firstCollection = testObject.Container.Collection1;
        Assert.IsNotNull(firstCollection);
        Assert.AreEqual(expected: 2, firstCollection.Count);

        Assert.AreEqual(expected: expectedResult, firstCollection.First().Property2);
        var firstElementSecondCollection = firstCollection.First().Collection2;
        Assert.IsNotNull(firstElementSecondCollection);
        Assert.AreEqual(expected: 1, firstElementSecondCollection.Count);
        Assert.AreEqual(expected: expectedResult, firstElementSecondCollection.First().Property1);

        Assert.AreEqual(expected: expectedResult, firstCollection.Skip(1).First().Property2);
        var secondElementSecondCollection = firstCollection.Skip(1).First().Collection2;
        Assert.IsNotNull(secondElementSecondCollection);
        Assert.AreEqual(expected: 2, secondElementSecondCollection.Count);
        Assert.AreEqual(expected: expectedResult, secondElementSecondCollection.First().Property1);
        Assert.AreEqual(expected: expectedResult, secondElementSecondCollection.Skip(1).First().Property1);
    }


    [Test]
    public void GetUninitializedObject()
    {
        var json = $@"{{
            ""Property2"": ""{K_TEST_TEXT}"",
            ""Collection2"": [
              {{
                ""Property1"": ""{K_TEST_TEXT}""
              }},
		      {{
                ""Property1"": ""{K_TEST_TEXT}""
              }}
            ]
          }}";

        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
        var pop1 = new Class1();
        serializer.Populate(new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(json)), pop1);

        var pop2 = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(Class1)) as Class1;
        // TODO: 2022-08-29 - BUG TO FIX - By calling Init, the collection is correctly populate by Newtonsoft. 
        // Search to init object with GetUninitializedObject and call ctor and prop initializers.
        //pop2.Init();
        serializer.Populate(new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(json)), pop2);

        Assert.IsNotNull(pop2.Collection2);
        Assert.AreEqual(2, pop2.Collection2.Count);
        Assert.IsNotNull( pop2.Collection2.First());
        Assert.IsNotNull( pop2.Collection2.Skip(1).First());
        Assert.AreEqual(K_TEST_TEXT, pop2.Collection2.First().Property1);
        Assert.AreEqual(K_TEST_TEXT, pop2.Collection2.Skip(1).First().Property1);
    }
    public class Class1 
    {
        public System.Collections.Generic.ICollection<Class2> Collection2 { get; private set; } = new System.Collections.Generic.List<Class2>();
        public string Property2 { get; set; }
        public void Init()
        {
            Collection2 = new System.Collections.Generic.List<Class2>();
        }
    }
    public class Class2
    {
        public string Property1 { get; set; }
    }
}