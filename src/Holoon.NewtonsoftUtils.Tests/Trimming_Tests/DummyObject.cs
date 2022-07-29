using Holoon.NewtonsoftUtils.Trimming;
using System;
using System.Collections.Generic;

namespace Holoon.NewtonsoftUtils.Tests.Trimming_Tests;

public class SpacedStringObject
{
    public SpacedString Property1 { get; set; }
}
public class SpacedStringAttributeObject
{
    [SpacedString]
    public string Property1 { get; set; }
}
public class NormalStringObject
{
    public string Property1 { get; set; }
}
public class NormalStringCtorObject
{
    public NormalStringCtorObject(string property1)
    {
        Property1 = property1;
    }
    public string Property1 { get; set; }
}
public class NormalSubStringObject
{
    public string Property1 { get; set; }
    public NormalStringsObject Sub { get; set; }
}
public class NormalStringsObject
{
    public string Property1 { get; set; }
    public string Property2 { get; set; }
}
public class WrongTypeAttributeObject
{
    [SpacedString]
    public int Property1 { get; set; }
}
public class StringNoSettableObject
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "It is a test class.")]
    public string Property1 => "  You can not Trim this even if you want to !  ";
}
public class TabObject 
{
    public NormalSubStringObject[] Property1 { get; set; }
}
public class CollectionContainer
{
    public CollectionOfCollection Container { get; set; }  
}
public class CollectionOfCollection 
{
    public ICollection<CollectionOfObjectOfString> Collection1 { get; private set; } = new List<CollectionOfObjectOfString>(); 
}
public class CollectionOfObjectOfString 
{
    public string Property2 { get; set; } 
    public ICollection<NormalStringObject> Collection2 { get; private set; } = new List<NormalStringObject>(); 
}