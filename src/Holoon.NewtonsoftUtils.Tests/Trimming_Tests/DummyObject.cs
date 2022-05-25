using Holoon.NewtonsoftUtils.Trimming;
using System;

namespace Holoon.NewtonsoftUtils.Tests.Trimming_Tests
{
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
        public string Property1 => "  You can not Trim this even if you want to !  ";
    }
}
