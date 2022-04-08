using System;
using Holoon.NewtonsoftUtils.CanBeUndefined;
using NUnit.Framework;

namespace Holoon.NewtonsoftUtils.Tests.CanBeUndefined_Tests
{
    public class CanBeUndefinedReadTests
    {
        [Test]
        public void Read_Non_Regression_For_Basic_JSon()
        {
            var json = "{\"Property1\":42,\"Property2\":\"Arthur Dent\",\"Property3\":\"1952-03-11T00:00:00\",\"Property4\":{\"Property1\":99,\"Property2\":\"Ford Perfect\",\"Property3\":\"2001-05-11T00:00:00\"}}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<BasicObject>(json, settings);

            var expected = new BasicObject
            {
                Property1 = 42,
                Property2 = "Arthur Dent",
                Property3 = new DateTime(1952, 3, 11),
                Property4 = new OtherBasicObject
                {
                    Property1 = 99,
                    Property2 = "Ford Perfect",
                    Property3 = new DateTime(2001, 5, 11),
                }
            };
            Assert.AreEqual(expected.Property1, testObject?.Property1);
            Assert.AreEqual(expected.Property2, testObject?.Property2);
            Assert.AreEqual(expected.Property3, testObject?.Property3);
            Assert.AreEqual(expected.Property4.Property1, testObject?.Property4?.Property1);
            Assert.AreEqual(expected.Property4.Property2, testObject?.Property4?.Property2);
            Assert.AreEqual(expected.Property4.Property3, testObject?.Property4?.Property3);
        }

        [Test]
        public void Read_Non_Regression_For_Default_JSon()
        {
            var json = "{\"Property1\":0,\"Property2\":null,\"Property3\":\"0001-01-01T00:00:00\",\"Property4\":null}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<BasicObject>(json, settings);

            var expected = new BasicObject
            {
                Property1 = default,
                Property2 = null,
                Property3 = default,
                Property4 = null
            };
            Assert.AreEqual(expected.Property1, testObject?.Property1);
            Assert.AreEqual(expected.Property2, testObject?.Property2);
            Assert.AreEqual(expected.Property3, testObject?.Property3);
            Assert.AreEqual(expected.Property4, testObject?.Property4);
        }

        [Test]
        public void Read_CanBeUndefined_Scalar_Is_Defined_To_Default()
        {
            var json = "{\"Property1\":0}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ScalarObject>(json, settings);

            var expected = new ScalarObject
            {
                Property1 = default(int)
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Reference_Is_Defined_To_Null()
        {
            var json = "{\"Property1\":null}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ReferenceObject>(json, settings);

            var expected = new ReferenceObject
            {
                Property1 = null
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_JSonObject_Is_Defined_To_Null()
        {
            var json = "{\"Property1\":null}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ClassObject>(json, settings);

            var expected = new ClassObject
            {
                Property1 = null
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Collection_Is_Defined_To_Null()
        {
            var json = "{\"Property1\":null}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CollectionObject>(json, settings);

            var expected = new CollectionObject
            {
                Property1 = null
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Array_Is_Defined_To_Null()
        {
            var json = "{\"Property1\":null}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayScalarObject>(json, settings);

            var expected = new ArrayScalarObject
            {
                Property1 = null
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Nullable_Is_Defined_To_Null()
        {
            var json = "{\"Property1\":null}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<NullableObject>(json, settings);

            var expected = new NullableObject
            {
                Property1 = null
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Scalar_Is_Defined()
        {
            var json = "{\"Property1\":42}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ScalarObject>(json, settings);

            var expected = new ScalarObject
            {
                Property1 = 42
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Reference_Is_Defined()
        {
            var json = "{\"Property1\":\"Arthur Dent\"}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ReferenceObject>(json, settings);

            var expected = new ReferenceObject
            {
                Property1 = "Arthur Dent"
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_JSonObject_Is_Defined()
        {
            var json = "{\"Property1\":{\"Property1\":99,\"Property2\":\"Ford Perfect\",\"Property3\":\"2001-05-11T00:00:00\"}}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ClassObject>(json, settings);

            var expected = new ClassObject
            {
                Property1 = new OtherBasicObject
                {
                    Property1 = 99,
                    Property2 = "Ford Perfect",
                    Property3 = new DateTime(2001, 5, 11),
                }
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1.Value.Property1, testObject?.Property1?.Value?.Property1);
            Assert.AreEqual(expected.Property1.Value.Property2, testObject?.Property1?.Value?.Property2);
            Assert.AreEqual(expected.Property1.Value.Property3, testObject?.Property1?.Value?.Property3);
        }

        [Test]
        public void Read_CanBeUndefined_Collection_Is_Defined()
        {
            var json = "{\"Property1\":[42,99]}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CollectionObject>(json, settings);

            var expected = new CollectionObject
            {
                Property1 = new System.Collections.Generic.List<int>() { 42, 99 }
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Array_Is_Defined()
        {
            var json = "{\"Property1\":[42,99]}";
            var json2 = "{\"Property1\":[\"42\",\"99\"]}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayScalarObject>(json, settings);
            var testObject2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayReferenceObject>(json2, settings);

            var expected = new ArrayScalarObject
            {
                Property1 = new int[] { 42, 99 }
            };
            var expected2 = new ArrayReferenceObject
            {
                Property1 = new string[] { "42", "99" }
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
            Assert.AreEqual(false, testObject2?.Property1?.IsUndefined);
            Assert.AreEqual(expected2.Property1?.Value, testObject2?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Nullable_Is_Defined()
        {
            var json = "{\"Property1\":42}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<NullableObject>(json, settings);

            var expected = new NullableObject
            {
                Property1 = 42
            };
            Assert.AreEqual(false, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Scalar_Is_Undefined()
        {
            var json = "{}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ScalarObject>(json, settings);

            var expected = new ScalarObject
            {
                Property1 = Undefined.Value
            };
            Assert.AreEqual(true, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Reference_Is_Undefined()
        {
            var json = "{}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ReferenceObject>(json, settings);

            var expected = new ReferenceObject
            {
                Property1 = Undefined.Value
            };
            Assert.AreEqual(true, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_JSonObject_Is_Undefined()
        {
            var json = "{}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ClassObject>(json, settings);

            var expected = new ClassObject
            {
                Property1 = Undefined.Value
            };
            Assert.AreEqual(true, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Collection_Is_Undefined()
        {
            var json = "{}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CollectionObject>(json, settings);


            var expected = new CollectionObject
            {
                Property1 = CanBeUndefined.Undefined.Value
            };
            Assert.AreEqual(true, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Array_Is_Undefined()
        {
            var json = "{}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayScalarObject>(json, settings);

            var expected = new ArrayScalarObject
            {
                Property1 = Undefined.Value
            };
            Assert.AreEqual(true, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }

        [Test]
        public void Read_CanBeUndefined_Nullable_Is_Undefined()
        {
            var json = "{}";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var testObject = Newtonsoft.Json.JsonConvert.DeserializeObject<NullableObject>(json, settings);

            var expected = new NullableObject
            {
                Property1 = Undefined.Value
            };
            Assert.AreEqual(true, testObject?.Property1?.IsUndefined);
            Assert.AreEqual(expected.Property1?.Value, testObject?.Property1?.Value);
        }
    }
}
