using Holoon.NewtonsoftUtils.Trimming;
using NUnit.Framework;

namespace Holoon.NewtonsoftUtils.Tests.Trimming_Tests
{
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
    }
}