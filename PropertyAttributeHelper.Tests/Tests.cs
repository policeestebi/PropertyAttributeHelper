using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PropertyAttributeUtils;
using PropertyAttributeUtils.Tests;
using System.Runtime.Serialization;

namespace PropertyAttributeHelper.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ClassWithAttributeShouldReturnAnAttr()
        {
            var classAttribute = AttributeUtil.GetClassAttributeValue<MyTestClass, DataContractAttribute>();

            Assert.IsNotNull(classAttribute);
        }

        [TestMethod]
        public void PropertyWithAttributesShouldReturnAnAttr()
        {
            var attribute = AttributeUtil.GetPropertyAttributeValue<MyTestClass, int, JsonPropertyAttribute>(MyTestClass => MyTestClass.MyProp1);

            Assert.IsNotNull(attribute);
        }

        [TestMethod]
        public void UsingPropInfoShouldReturnAnAttr()
        {
            var propInfo = PropertyUtil.GetProperty<MyTestClass, int>(MyTestClass => MyTestClass.MyProp1);

            var attribute = AttributeUtil.GetAttributeFromPropInfo<JsonPropertyAttribute>(propInfo);

            Assert.IsNotNull(attribute);
        }

        [TestMethod]
        public void GetAllAttributesShouldReturnADictionary()
        {
            var attributes = AttributeUtil.GetPropetiesAttributes<MyTestClass, JsonPropertyAttribute>();

            Assert.IsNotNull(attributes);
        }

        [TestMethod]
        public void GetAllAttributesFromAllPropertiesShouldReturnADictionary()
        {
            var attributes = AttributeUtil.GetPropetiesAttributes<MyTestClass>();

            Assert.IsNotNull(attributes);
        }

    }
}
