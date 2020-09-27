using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Proj_IceCream
{
    [DataContract]
    public class Clients
    {
        [DataMember]
        private uint id;
        [DataMember]
        private string nick;
        [DataMember]
        private string shopName;
        [DataMember]
        private string name;
        [DataMember]
        private string lastname;
        [DataMember]
        private string city;
        [DataMember]
        private string street;
        [DataMember]
        private string psc;
        [DataMember]
        private string state;
        [DataMember]
        private string phonenumber;
        [DataMember]
        private string ico;
        [DataMember]
        private string dic;


        public Clients(uint _id, string _nick, string _shopName, string _name, string _lastname, string _city, string _street, string _psc, string _state, string _phonenumber, string _ico, string _dic)
        {
            id = _id;
            nick = _nick;
            shopName = _shopName;
            name = _name;
            lastname = _lastname;
            city = _city;
            street = _street;
            psc = _psc;
            state = _state;
            phonenumber = _phonenumber;
            ico = _ico;
            dic = _dic;
        }

        public uint getID()
        {
            return id;
        }

        public string getNick()
        {
            return nick;
        }

        public string getShopname()
        {
            return shopName;
        }

        public string getName()
        {
            return name;
        }

        public string getLastName()
        {
            return lastname;
        }
    
        public string getCity()
        {
            return city;
        }

        public string getStreet()
        {
            return street;
        }

        public string getPsc()
        {
            return psc;
        }

        public string getState()
        {
            return state;
        }

        public string getPhoneNumber()
        {
            return phonenumber;
        }

        public string getIco()
        {
            return ico;
        }

        public string getDic()
        {
            return dic;
        }

        public void setID(uint value)
        {
            id = value;
        }

    }

}
