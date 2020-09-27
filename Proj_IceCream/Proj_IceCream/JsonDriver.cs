using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Proj_IceCream
{
    public class JsonDriver
    {
        private List<IceCream> IceCreamList = new List<IceCream>();
        private List<Clients> ClientsList = new List<Clients>();
        private List<Car> CarList = new List<Car>();
        private List<PersonalData> PersonalDataList = new List<PersonalData>();

        public JsonDriver(string _projectIceCreamPath)
        {
            ExecuteTasks();
            PersonalDataList = updateID();
        }
        
        public void AddOrEditData(Clients c)
        {
            if (c.getID() == 0)
            {
                Clients last = ClientsList[ClientsList.Count - 1];
                c.setID(ClientsList[ClientsList.Count - 1].getID());
                ClientsList[ClientsList.Count - 1] = c;
                last.setID(ClientsList[ClientsList.Count - 1].getID() + 1);
                ClientsList.Add(last);
                return;
            }
            else
            {
                for (int i = 0; i < ClientsList.Count; i++)
                    if (ClientsList[i].getID() == c.getID())
                    {
                        ClientsList[i] = c;
                        return;
                    }
            }  
        }

        public void AddOrEditData(IceCream c)
        {
            if (c.getID() == 0)
            {
                IceCream last = IceCreamList[IceCreamList.Count - 1];
                c.setID(IceCreamList[IceCreamList.Count - 1].getID());
                IceCreamList[IceCreamList.Count - 1] = c;
                last.setID(IceCreamList[IceCreamList.Count - 1].getID() + 1);
                IceCreamList.Add(last);
                return;
            }
            else
            {
                for (int i = 0; i < IceCreamList.Count; i++)
                    if (IceCreamList[i].getID() == c.getID())
                    {
                        IceCreamList[i] = c;
                        return;
                    }
            }
        }

        public void AddOrEditData(PersonalData c)
        {
            if (c.getID() == 0)
            {
                PersonalData last = PersonalDataList[PersonalDataList.Count - 1];
                c.setID(PersonalDataList[PersonalDataList.Count - 1].getID());
                PersonalDataList[PersonalDataList.Count - 1] = c;
                last.setID(PersonalDataList[PersonalDataList.Count - 1].getID() + 1);
                PersonalDataList.Add(last);
                return;
            }
            else
            {
                for (int i = 0; i < PersonalDataList.Count; i++)
                    if (PersonalDataList[i].getID() == c.getID())
                    {
                        PersonalDataList[i] = c;
                        return;
                    }
            }
        }

        public void AddOrEditData(Car c)
        {
            if (c.getID() == 0)
            {
                Car last = CarList[CarList.Count - 1];
                c.setID(CarList[CarList.Count - 1].getID());
                CarList[CarList.Count - 1] = c;
                last.setID(CarList[CarList.Count - 1].getID() + 1);
                CarList.Add(last);
                return;
            }
            else
            {
                for (int i = 0; i < CarList.Count; i++)
                    if (CarList[i].getID() == c.getID())
                    {
                        CarList[i] = c;
                        return;
                    }
            }
        }

        public void deleteClient(uint id)
        {
            for (int i = 0; i < ClientsList.Count; i++)
                if (ClientsList[i].getID() == id)
                {
                    ClientsList.RemoveAt(i);
                    break;
                }
            for (int i = 0; i < ClientsList.Count; i++)
                ClientsList[i].setID(Convert.ToUInt16(i + 1));
        }

        public List<PersonalData> updateID()
        {
            List<PersonalData> newPersonalDataList = new List<PersonalData>();
            string actualYear = System.DateTime.Today.Year.ToString();

            foreach (var i in PersonalDataList)
            {
                if (actualYear != i.getYearOFLastID())
                {
                    i.setYearOFLastID(actualYear);
                    i.setLastID("0");
                }
                newPersonalDataList.Add(i);
            }
            return newPersonalDataList;
        }

        public List<IceCream> getIceCreamList()
        {
            return IceCreamList;
        }

        public List<Clients> getClientsList()
        {
            return ClientsList;
        }

        public List<PersonalData> getPersonalDataList()
        {
            return PersonalDataList;
        }

        public List<Car> getCarList()
        {
            return CarList;
        }

        private void ExecuteTasks()
        {
            ReadIceCream();
            ReadClients();
            ReadPersonalData();
            ReadCars();
        }
        
        private void ReadIceCream()
        {
            IceCreamList = JsonConvert.DeserializeObject<List<IceCream>>(File.ReadAllText(Environment.CurrentDirectory + "\\data\\IceCream.json"));
        }
        private void ReadClients()
        {
            ClientsList = JsonConvert.DeserializeObject<List<Clients>>(File.ReadAllText(Environment.CurrentDirectory + "\\data\\Clients.json"));
        }
        private void ReadPersonalData()
        {
            PersonalDataList = JsonConvert.DeserializeObject<List<PersonalData>>(File.ReadAllText(Environment.CurrentDirectory + "\\data\\PersonalData.json"));
        }
        private void ReadCars()
        {
            CarList = JsonConvert.DeserializeObject<List<Car>>(File.ReadAllText(Environment.CurrentDirectory + "\\data\\Cars.json"));
        } 

        public void saveJsons()
        {
            File.WriteAllText(Environment.CurrentDirectory + "\\data\\IceCream.json", JsonConvert.SerializeObject(IceCreamList, Formatting.Indented));
            File.WriteAllText(Environment.CurrentDirectory + "\\data\\Clients.json", JsonConvert.SerializeObject(ClientsList, Formatting.Indented));
            File.WriteAllText(Environment.CurrentDirectory + "\\data\\PersonalData.json", JsonConvert.SerializeObject(PersonalDataList, Formatting.Indented));
            File.WriteAllText(Environment.CurrentDirectory + "\\data\\Cars.json", JsonConvert.SerializeObject(CarList, Formatting.Indented));
        }

        public void closeExcel()
        {
            saveJsons();
        }
    }
}