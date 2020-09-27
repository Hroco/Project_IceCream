using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

namespace Proj_IceCream
{
    [DataContract]
    public class Car
    {
        [DataMember]
        private uint id;
        [DataMember]
        private string licensePlate;
        [DataMember]
        private string carName;

        public Car() { }
        public Car(uint _id, string license, string name)
        {
            id = _id;
            licensePlate = license;
            carName = name;
        }

        public string getName()
        {
            return carName;
        }

        public string getLicensePlate()
        {
            return licensePlate;
        }
        public uint getID()
        {
            return id;
        }
        public void setID(uint value)
        {
            id = value;
        }
    }
}
