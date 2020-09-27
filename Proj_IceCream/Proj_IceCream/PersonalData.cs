using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Proj_IceCream
{
    [DataContract]
    public class PersonalData
    {
        [DataMember]
        private uint id;
        [DataMember]
        private string companyName;
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
        private string ico;
        [DataMember]
        private string dic;
        [DataMember]
        private string icdph;
        [DataMember]
        private string www;
        [DataMember]
        private string phonenumber;
        [DataMember]
        private string email;
        [DataMember]
        private string email2;
        [DataMember]
        private uint priceN;
        [DataMember]
        private uint priceS;
        [DataMember]
        private string lastID;
        [DataMember]
        private string yearOFLastID;


        public PersonalData(uint _id, string _companyName, string _name, string _lastname, string _city, string _street, string _psc, string _state, string _ico, string _dic, string _icdph, string _www, string _phonenumber, string _email, string _email2, uint _priceN, uint _priceS, string _lastID, string _yearOFLastID)
        {
            id = _id;
            companyName = _companyName;
            name = _name;
            lastname = _lastname;
            city = _city;
            street = _street;
            psc = _psc;
            state = _state;
            ico = _ico;
            dic = _dic;
            icdph = _icdph;
            www = _www;
            phonenumber = _phonenumber;
            email = _email;
            email2 = _email2;
            priceN = _priceN;
            priceS = _priceS;
            lastID = _lastID;
            yearOFLastID = _yearOFLastID;
        }

        public string getCompanyName()
        {
            return companyName;
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

        public string getIco()
        {
            return ico;
        }

        public string getDic()
        {
            return dic;
        }

        public string getIcDPH()
        {
            return icdph;
        }

        public string getWww()
        {
            return www;
        }
    
        public string getPhoneNumber()
        {
            return phonenumber;
        }

        public string getEmail()
        {
            return email;
        }
    
        public string getEmail2()
        {
            return email2;
        }

        public uint getPricesN()
        {
            return priceN;
        }
    
        public uint getPricesS()
        {
            return priceS;
        }

        public string getLastID()
        {
            return lastID;
        }

        public void setLastID(string value)
        {
            lastID = value;
        }

        public void setYear(string value)
        {
            yearOFLastID = value;
        }

        public string getYearOFLastID()
        {
            return yearOFLastID;
        }
        public void setYearOFLastID(string value)
        {
            yearOFLastID = value;
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
