using Holoon.NewtonsoftUtils.CanBeUndefined;

namespace Holoon.NewtonsoftUtils.Tests.CanBeUndefined_Tests
{
    public class BasicObject
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }
        public System.DateTime Property3 { get; set; }
        public OtherBasicObject Property4 { get; set; }
    }
    public class OtherBasicObject
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }
        public System.DateTime Property3 { get; set; }
    }
    public class ScalarObject
    {
        public CanBeUndefined<int> Property1 { get; set; }
    }
    public class ReferenceObject
    {
        public CanBeUndefined<string> Property1 { get; set; }
    }
    public class ClassObject
    {
        public CanBeUndefined<OtherBasicObject> Property1 { get; set; }
    }
    public class CollectionObject
    {
        public CanBeUndefined<System.Collections.Generic.List<int>> Property1 { get; set; }
    }
    public class ArrayScalarObject
    {
        public CanBeUndefined<int[]> Property1 { get; set; }
    }
    public class ArrayReferenceObject
    {
        public CanBeUndefined<string[]> Property1 { get; set; }
    }
    public class NullableObject
    {
        public CanBeUndefined<int?> Property1 { get; set; }
    }
}
