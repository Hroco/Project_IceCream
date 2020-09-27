using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Proj_IceCream
{
    [DataContract]
    public class IceCream
    {
        [DataMember]
        private uint id;
        [DataMember]
        private string name;
        [DataMember]
        private string type;
        [DataMember]
        private int amount;

        public IceCream() { }
        public IceCream(uint _id, string _name, string _type)
        {
            id = _id;
            name = _name;
            type = _type;
        }

        public void setAmount(int value)
        {
            amount = value;
        }

        public uint getID()
        {
            return id;
        }

        public string getname()
        {
            return name;
        }

        public string gettype()
        {
            return type;
        }

        public int getamount()
        {
            return amount;
        }
        public void setID(uint value)
        {
            id = value;
        }

    }

}
