﻿using Holoon.NewtonsoftUtils.CanBeUndefined;
using NUnit.Framework;
using System;
using System.Linq;

namespace Holoon.NewtonsoftUtils.Tests.CanBeUndefined_Tests;

public class CanBeUndefinedCommonTests
{
    [Test]
    public void CanBeUndefined_Null_ImplicitCast_Properties()
    {
        string nullString = null;
        CanBeUndefined<string> cbuNullString = nullString;

        Assert.AreEqual(false, cbuNullString.IsUndefined);
        Assert.AreEqual(null, cbuNullString.Value);
    }

    [Test]
    public void CanBeUndefined_Null_ImplicitCast_BaseOverride()
    {
        string nullString = null;
        CanBeUndefined<string> cbuNullString = nullString;

        Assert.DoesNotThrow(() => cbuNullString.GetHashCode());
        Assert.DoesNotThrow(() => cbuNullString.ToString());
    }

    [Test]
    public void CanBeUndefined_Null_ImplicitCast_Equal()
    {
        string nullString = null;
        CanBeUndefined<string> cbuNullString = nullString;
        CanBeUndefined<string> cbuNullString2 = null;
        CanBeUndefined<string> cbuNullString3 = Undefined.Value;
        int? nullInt = null;

        Assert.AreEqual(true, cbuNullString.Equals(nullString));
        Assert.AreEqual(true, cbuNullString.Equals(null));
        Assert.AreEqual(true, cbuNullString.Equals(nullInt)); // NOTE: Is True to match the framework. (nullInt.Equals(nullString) is true)
        Assert.AreEqual(true, cbuNullString.Equals(cbuNullString));
        Assert.AreEqual(true, cbuNullString.Equals(cbuNullString2));
        Assert.AreEqual(false, cbuNullString.Equals(cbuNullString3));
    }

    [Test]
    public void CanBeUndefined_NotNull_Equal()
    {
        CanBeUndefined<string> cbu = "42";
        CanBeUndefined<string> cbu2 = null;
        CanBeUndefined<string> cbu3 = Undefined.Value;

        Assert.AreEqual(true, cbu.Equals("42"));
        Assert.AreEqual(false, cbu.Equals("43"));
        Assert.AreEqual(false, cbu.Equals(42));
        Assert.AreEqual(false, cbu.Equals(cbu2));
        Assert.AreEqual(false, cbu.Equals(cbu3));
    }

    [Test]
    public void CanBeUndefined_Interface_Cast()
    {
        var interfaceObject = (ICloneable)"Arthur Dent";
        var testObject = new InterfaceObject
        {
            // NOTE: Implicit user defined conversions don't work when one of the types is an interface, so, we are force to use an explicit conversion. (From the C# specs: 6.4.1 Permitted user-defined conversions)
            Property1 = (string)interfaceObject
        };
        var settings = new Newtonsoft.Json.JsonSerializerSettings
        {
            ContractResolver = new CanBeUndefinedResolver()
        };
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(testObject, settings);

        Assert.AreEqual("{\"Property1\":\"Arthur Dent\"}",
            json);
    }

    [Test]
    public void CanBeUndefined_Implicit_Cast()
    {
        var testObject = new ScalarObject { Property1 = 42 };
        int casted;
        casted = testObject.Property1;

        Assert.AreEqual(42, casted);
    }

    [Test]
    public void CanBeUndefined_ImplicitCast_BetweenCanBeUndefinedObjects()
    {
        var testObject1 = new ScalarObject { Property1 = 42 };
        var testObject2 = new ScalarObject { Property1 = 42 };
        var testObject3 = new ScalarObject { Property1 = Undefined.Value };

        Assert.AreNotEqual(testObject3.Property1, testObject1.Property1);
        Assert.AreEqual(testObject2.Property1, testObject1.Property1);
    }

    [Test]
    public void CanBeUndefined_ImplicitCast_Dictionary()
    {
        var testObject1 = new ScalarObject { Property1 = 42 };
        var testObject2 = new ScalarObject { Property1 = 43 };
        var dictionary = new ScalarObject[] { testObject1, testObject2 }.ToDictionary(x => x.Property1);

        Assert.AreEqual(testObject1, dictionary[42]);
    }

    [Test]
    public void CanBeUndefined_ImplicitCast_Lookup()
    {
        var testObject1 = new ScalarObject { Property1 = 42 };
        var testObject2 = new ScalarObject { Property1 = 43 };
        var testObject3 = new ScalarObject { Property1 = 43 };
        var lookup = new ScalarObject[] { testObject1, testObject2, testObject3 }.ToLookup(x => x.Property1);

        Assert.AreEqual(testObject2, lookup[43]?.FirstOrDefault());
        Assert.AreEqual(testObject3, lookup[43]?.LastOrDefault());
    }
}