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
    }
}