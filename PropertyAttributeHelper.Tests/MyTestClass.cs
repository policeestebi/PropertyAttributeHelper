using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PropertyAttributeUtils.Tests
{
    [DataContract]
    public class MyTestClass
    {
        [DataMember]
        [JsonProperty]
        public int MyProp1 { get; set; }

        [JsonProperty]
        public int MyProp2 { get; set; }

        [DataMember]
        public int MyPro3 { get; set; }
    }
}
