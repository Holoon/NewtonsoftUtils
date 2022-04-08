using Holoon.NewtonsoftUtils.CanBeUndefined;
using NUnit.Framework;

namespace Holoon.NewtonsoftUtils.Tests.CanBeUndefined_Tests
{
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
    }
}