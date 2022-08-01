using Holoon.NewtonsoftUtils.Trimming;
using NUnit.Framework;
using System;
using System.Linq;

namespace Holoon.NewtonsoftUtils.Tests.Trimming_Tests;


public class CreateInstanceTests
{
    [Test]
    public void Create_Object_Public_ParameterlessCtor()
    {
        var instance = InstanceCreator.Create(typeof(NormalStringObject)) as NormalStringObject;

        Assert.IsNotNull(instance);
    }


    [Test]
    public void Create_Object_Private_ParameterlessCtor()
    {
        var instance = InstanceCreator.Create(typeof(PrivateCtorStringObject)) as PrivateCtorStringObject;

        Assert.IsNotNull(instance);
    }

    [Test]
    public void Create_Object_No_ParameterlessCtor()
    {
        var instance = InstanceCreator.Create(typeof(NormalStringCtorObject)) as NormalStringCtorObject;

        Assert.IsNotNull(instance);
    }

    [Test]
    public void Create_Object_With_PrivateCollectionInitializer()
    {
        var instance = InstanceCreator.Create(typeof(CollectionOfObjectOfString)) as CollectionOfObjectOfString;

        Assert.IsNotNull(instance);
        Assert.IsNotNull(instance.Collection2);
    }
}
