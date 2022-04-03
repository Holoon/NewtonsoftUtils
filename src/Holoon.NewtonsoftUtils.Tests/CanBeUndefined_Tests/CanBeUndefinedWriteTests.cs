using System;
using Holoon.NewtonsoftUtils.CanBeUndefined;
using NUnit.Framework;

namespace Holoon.NewtonsoftUtils.Tests.CanBeUndefined_Tests
{
    public class CanBeUndefinedWriteTests
    {
        [Test]
        public void Write_Non_Regression_For_Basic_JSon()
        {
            var testObject = new BasicObject
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
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":42,\"Property2\":\"Arthur Dent\",\"Property3\":\"1952-03-11T00:00:00\",\"Property4\":{\"Property1\":99,\"Property2\":\"Ford Perfect\",\"Property3\":\"2001-05-11T00:00:00\"}}",
                json);
        }

        [Test]
        public void Write_Non_Regression_For_Default_JSon()
        {
            var testObject = new BasicObject
            {
                Property1 = default,
                Property2 = null,
                Property3 = default,
                Property4 = null
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":0,\"Property2\":null,\"Property3\":\"0001-01-01T00:00:00\",\"Property4\":null}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Scalar_Is_Defined_To_Default()
        {
            var testObject = new ScalarObject
            {
                Property1 = default(int)
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":0}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Reference_Is_Defined_To_Null()
        {
            var testObject = new ReferenceObject
            {
                Property1 = null
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":null}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_JSonObject_Is_Defined_To_Null()
        {
            var testObject = new ClassObject
            {
                Property1 = null
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":null}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Collection_Is_Defined_To_Null()
        {
            var testObject = new CollectionObject
            {
                Property1 = null
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":null}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Array_Is_Defined_To_Null()
        {
            var testObject = new ArrayScalarObject
            {
                Property1 = null
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":null}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Nullable_Is_Defined_To_Null()
        {
            var testObject = new NullableObject
            {
                Property1 = null
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":null}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Scalar_Is_Defined()
        {
            var testObject = new ScalarObject
            {
                Property1 = 42
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":42}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Reference_Is_Defined()
        {
            var testObject = new ReferenceObject
            {
                Property1 = "Arthur Dent"
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":\"Arthur Dent\"}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_JSonObject_Is_Defined()
        {
            var testObject = new ClassObject
            {
                Property1 = new OtherBasicObject
                {
                    Property1 = 99,
                    Property2 = "Ford Perfect",
                    Property3 = new DateTime(2001, 5, 11),
                }
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":{\"Property1\":99,\"Property2\":\"Ford Perfect\",\"Property3\":\"2001-05-11T00:00:00\"}}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Collection_Is_Defined()
        {
            var testObject = new CollectionObject
            {
                Property1 = new System.Collections.Generic.List<int>() { 42, 99 }
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":[42,99]}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Array_Is_Defined()
        {
            var testObject = new ArrayScalarObject
            {
                Property1 = new int[] { 42, 99 }
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":[42,99]}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Nullable_Is_Defined()
        {
            var testObject = new NullableObject
            {
                Property1 = 42
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":42}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Scalar_Is_Undefined()
        {
            var testObject = new ScalarObject
            {
                Property1 = Undefined.Value
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Reference_Is_Undefined()
        {
            var testObject = new ReferenceObject
            {
                Property1 = Undefined.Value
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_JSonObject_Is_Undefined()
        {
            var testObject = new ClassObject
            {
                Property1 = Undefined.Value
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Collection_Is_Undefined()
        {
            var testObject = new CollectionObject
            {
                Property1 = Undefined.Value
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Array_Is_Undefined()
        {
            var testObject = new ArrayScalarObject
            {
                Property1 = Undefined.Value
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Nullable_Is_Undefined()
        {
            var testObject = new NullableObject
            {
                Property1 = Undefined.Value
            };
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{}",
                json);
        }


        [Test]
        public void Write_MultiAssign()
        {
            var testObject = new ScalarObject
            {
                Property1 = 42
            };


            testObject.Property1 = 43;
            testObject.Property1 = 44;
            testObject.Property1 = 45;

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":45}",
                json);
        }

        [Test]
        public void Write_CanBeUndefined_Null_Properties()
        {
            string nullString = null;
            CanBeUndefined.CanBeUndefined<string> cbuNullString = nullString;
            
            Assert.AreEqual(cbuNullString.IsUndefined, false);
            Assert.AreEqual(cbuNullString.Value, null);
            
            Assert.DoesNotThrow(()=>cbuNullString.GetHashCode());

        }

        [Test]
        public void Write_CanBeUndefined_Array_With_Undefined_Values()
        {
            var testObject = new CollectionUndefinedObject
            {
                Property1 = new System.Collections.Generic.List<NewtonsoftUtils.CanBeUndefined.CanBeUndefined<int>>()
            };

            testObject.Property1.Value.Add(1);
            testObject.Property1.Value.Add(Undefined.Value);
            testObject.Property1.Value.Add(3);

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CanBeUndefined.CanBeUndefinedResolver()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

            Assert.AreEqual("{\"Property1\":[1,3]}",
                json);

        }
    }
}
