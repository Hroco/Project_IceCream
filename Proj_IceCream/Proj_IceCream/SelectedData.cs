using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Proj_IceCream
{
    [DataContract]
    class SelectedData
    {
        [DataMember]
        private List<IceCream> iceCreamList = new List<IceCream>();
        [DataMember]
        private Clients client;
        [DataMember]
        private PersonalData personalData;
        [DataMember]
        private string licensePlate;

        //add to list
        public void addToList(IceCream iceCream)
        {
            bool iceCreaFound = false;
            if (iceCreamList.Count > 0)
            {

                for (int i = 0; i < iceCreamList.Count; i++)
                {
                   if (iceCreamList[i].getname() == iceCream.getname())
                    {
                        int newAmmount = iceCreamList[i].getamount() + iceCream.getamount();
                        iceCreamList[i].setAmount(newAmmount);
                        iceCreaFound = true;
                    }
                }
            }

            if (!iceCreaFound)
                iceCreamList.Add(iceCream);
        }

        public void resetIceCream()
        {
            iceCreamList.Clear();
        }

        //remove from list
        public void removeFromList(IceCream iceCream)
        {
            for (int i = 0; i < iceCreamList.Count; i++)
            {
                if (iceCreamList[i].getname() == iceCream.getname())
                {
                    if (iceCreamList[i].getamount() - iceCream.getamount() <= 0)
                    {
                        iceCreamList.RemoveAt(i);
                        return;
                    }
                    else
                    {
                        int oldAmmount = iceCreamList[i].getamount();
                        iceCreamList[i].setAmount(oldAmmount - iceCream.getamount());
                        return;
                    }
                }
            }
        }

        public List<IceCream> getIceCreamList()
        {
            return iceCreamList;
        }

        public Clients getClient()
        {
            return client;
        }

        public PersonalData getPersonalData()
        {
            return personalData;
        }

        public string getLicensePlate()
        {
            return licensePlate;
        }

        public void setPersonalData(PersonalData value)
        {
            personalData = value;
        }

        public void setClient(Clients c)
        {
            client = c;
        }

        public void setLicensePlate(string plate)
        {
            licensePlate = plate;
        }
    }
}
