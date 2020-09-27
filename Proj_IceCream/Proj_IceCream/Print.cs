using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Proj_IceCream
{
    class Print
    {
        SelectedData SelectedData;
        _Application excel;
        Workbook wbTemplate;
        Worksheet wsTemplate;
        string pathTemplate;
        bool singlePage;
        int counter;

        public Print(string _pathTemplate)
        {
            counter = 0;
            pathTemplate = _pathTemplate;

            excel = new _Excel.Application();

            wbTemplate = excel.Workbooks.Open(pathTemplate);
            wsTemplate = wbTemplate.Worksheets[1];

            //excel.Visible = true;
            //wsTemplate.PrintPreview();
            //wsPersonalData.Select.
            //excel.Visible = false;

            
        }

        public void setSelectedData(SelectedData value)
        {
            SelectedData = value;
            printInitialise();
            counter++;
        }

        public void printInitialise()
        {
            writeIceCreamList();
            writeClient();
            writeDateAndCar();
            writePersonalData();
        }

        private string calculateVolume(string type)
        {
            int amount = 0;
            if (type == "F")
            {
                foreach (var i in SelectedData.getIceCreamList())
                {
                    if (i.gettype() == "F")
                        amount += i.getamount();
                }
                return (amount * 4).ToString();
            }
            else if (type == "M")
            {
                foreach (var i in SelectedData.getIceCreamList())
                {
                    if (i.gettype() == "M" || i.gettype() == "I")
                        amount += i.getamount();
                }
                return (amount * 4).ToString();
            }
            else if (type == "S")
            {
                foreach (var i in SelectedData.getIceCreamList())
                {
                    if (i.gettype() == "S")
                        amount += i.getamount();
                }
                return (amount * 4).ToString();
            }
            else
            {
                Console.WriteLine("Unknown IceCream type. Print.cs, calculateVolume method.");
                return "-1";
            }


        }

        public void printPreview(Form1 f)
        {
            excel.Visible = true;
            f.Visible = false;
            wsTemplate.PrintPreview();
            excel.Visible = false;
            f.Visible = true;
            f.TopMost = true;
            f.TopMost = false;
            //excel.DisplayAlerts = false;
            //wbTemplate.Close(0);
            //excel.Quit();
        }

        public void print()
        {
            if(singlePage)
                wsTemplate.PrintOutEx(1, 1, 2);  //print page from 1 to 1 and create 2 copys
            else
                wsTemplate.PrintOutEx(1, 2, 2);  //print page from 1 to 1 and create 2 copys
            //excel.DisplayAlerts = false;
            //wbTemplate.Close(0);
            //excel.Quit();
        }

        private void writeDateAndCar()
        {

            wsTemplate.Cells[12, 3].Value2 = SelectedData.getLicensePlate();
            wsTemplate.Cells[14, 3].Value2 = System.DateTime.Today;
            wsTemplate.Cells[15, 3].Value2 = System.DateTime.Today;

        }

        private void writeClient()
        {
            if (SelectedData.getClient().getShopname() == "-1")
                wsTemplate.Cells[10, 5].Value2 = "";
            else
                wsTemplate.Cells[10, 5].Value2 = SelectedData.getClient().getShopname();

            if (SelectedData.getClient().getName() != "-1" && SelectedData.getClient().getLastName() != "-1")
                wsTemplate.Cells[12, 5].Value2 = SelectedData.getClient().getName() + " " + SelectedData.getClient().getLastName();
            else if (SelectedData.getClient().getName() == "-1" && SelectedData.getClient().getLastName() != "-1")
                wsTemplate.Cells[12, 5].Value2 = SelectedData.getClient().getLastName();
            else if (SelectedData.getClient().getName() == "-1" && SelectedData.getClient().getLastName() == "-1")
                wsTemplate.Cells[12, 5].Value2 = "";

            if (SelectedData.getClient().getCity() != "-1" && SelectedData.getClient().getStreet() != "-1" && SelectedData.getClient().getPsc() != "-1")
                wsTemplate.Cells[13, 5].Value2 = SelectedData.getClient().getCity() + ", " + SelectedData.getClient().getStreet() + ", " + SelectedData.getClient().getPsc();
            else if (SelectedData.getClient().getCity() != "-1" && SelectedData.getClient().getStreet() == "-1" && SelectedData.getClient().getPsc() != "-1")
                wsTemplate.Cells[13, 5].Value2 = SelectedData.getClient().getCity() + ", " + SelectedData.getClient().getPsc();
            else if (SelectedData.getClient().getCity() != "-1" && SelectedData.getClient().getStreet() == "-1" && SelectedData.getClient().getPsc() == "-1")
                wsTemplate.Cells[13, 5].Value2 = SelectedData.getClient().getCity();
            else if (SelectedData.getClient().getCity() == "-1" && SelectedData.getClient().getStreet() == "-1" && SelectedData.getClient().getPsc() == "-1")
                wsTemplate.Cells[13, 5].Value2 = "";

            if (SelectedData.getClient().getState() == "-1")
                wsTemplate.Cells[14, 5].Value2 = "";
            else
                wsTemplate.Cells[14, 5].Value2 = SelectedData.getClient().getState();
           
            if (SelectedData.getClient().getPhoneNumber() == "-1")
                wsTemplate.Cells[15, 5].Value2 = "";
            else
                wsTemplate.Cells[15, 5].Value2 = "Mobil: " + SelectedData.getClient().getPhoneNumber();
            
            
            if (SelectedData.getClient().getIco() == "-1")
                wsTemplate.Cells[14, 8].Value2 = "";
            else
                wsTemplate.Cells[14, 8].Value2 =SelectedData.getClient().getIco();
            if (SelectedData.getClient().getDic() == "-1")
                wsTemplate.Cells[15, 8].Value2 = "";
            else
                wsTemplate.Cells[15, 8].Value2 = SelectedData.getClient().getDic();

        }

        private void writePersonalData()
        {
            PersonalData s = SelectedData.getPersonalData();

            wsTemplate.Cells[3, 3].Value2 = "   " + s.getCompanyName();                                     //company name
            wsTemplate.Cells[4, 3].Value2 = "   " + s.getCity() + " " + s.getStreet();                      //city
            wsTemplate.Cells[5, 3].Value2 = "   " + s.getPsc();                                             //psc
            wsTemplate.Cells[6, 3].Value2 = "   " + s.getState();                                           //state
            wsTemplate.Cells[3, 5].Value2 = "  Meno: " + s.getName() + " " + s.getLastName();               //seler name
            wsTemplate.Cells[4, 5].Value2 = "      IČO: " + s.getIco();                                     //ico
            wsTemplate.Cells[5, 5].Value2 = "      DIČ: " + s.getDic();                                     //dic
            wsTemplate.Cells[6, 5].Value2 = "IČ DPH: " + s.getIcDPH();                                      //ic dph
            wsTemplate.Cells[4, 8].Value2 = s.getPhoneNumber();                                             //phone number
            wsTemplate.Cells[5, 8].Value2 = s.getEmail();                                                   //email adress
            wsTemplate.Cells[6, 8].Value2 = s.getEmail2(); ;                                                //second email adress
            wsTemplate.Cells[8, 2].Value2 = "Dodací list č: " + s.getLastID() + "/" + s.getYearOFLastID();  //Dodaci list č:

            wsTemplate.Cells[54, 3].Value2 = "   " + s.getCompanyName();                                     //company name
            wsTemplate.Cells[55, 3].Value2 = "   " + s.getCity() + " " + s.getStreet();                      //city
            wsTemplate.Cells[56, 3].Value2 = "   " + s.getPsc();                                             //psc
            wsTemplate.Cells[57, 3].Value2 = "   " + s.getState();                                           //state
            wsTemplate.Cells[54, 5].Value2 = "  Meno: " + s.getName() + " " + s.getLastName();               //seler name
            wsTemplate.Cells[55, 5].Value2 = "      IČO: " + s.getIco();                                     //ico
            wsTemplate.Cells[56, 5].Value2 = "      DIČ: " + s.getDic();                                     //dic
            wsTemplate.Cells[57, 5].Value2 = "IČ DPH: " + s.getIcDPH();                                      //ic dph
            wsTemplate.Cells[55, 8].Value2 = s.getPhoneNumber();                                             //phone number
            wsTemplate.Cells[56, 8].Value2 = s.getEmail();                                                   //email adress
            wsTemplate.Cells[57, 8].Value2 = s.getEmail2(); ;                                                //second email adress
        }

        private void writeIceCreamList()
        {
            float sum1 = 0;
            float sum2 = 0;
            float sum3 = 0;
            float sum4 = 0;
            int i = 19;
            string name;
            float amount;
            float priceForUnitWithoutDPH;
            float priceForUnitWithDPH;
            float DPH = 20;
            float DPHCoefficient = (DPH + 100) / 100;
            float totalPriceWithoutDPH;
            float DPHFromTotalPrice;
            float totalPrice;
            string iceCreamType;
            
            if (SelectedData.getPersonalData().getName() == "Zuzana")
            {
                wsTemplate.Cells[17, 2].Value2 = "Názov položky";
                wsTemplate.Cells[17, 4].Value2 = "Počet";
                wsTemplate.Cells[18, 4].Value2 = "ks";
                wsTemplate.Cells[17, 5].Value2 = "Cena/ks ";
                wsTemplate.Cells[18, 5].Value2 = "bez DPH";
                wsTemplate.Cells[17, 6].Value2 = "Cena/ks";
                wsTemplate.Cells[18, 6].Value2 = "s DPH";
                wsTemplate.Cells[17, 7].Value2 = "DPH %";
                wsTemplate.Cells[17, 8].Value2 = "Cena";
                wsTemplate.Cells[18, 8].Value2 = "bez DPH";
                wsTemplate.Cells[17, 9].Value2 = "DPH z";
                wsTemplate.Cells[18, 9].Value2 = "ceny";
                wsTemplate.Cells[17, 10].Value2 = "Cena";
                wsTemplate.Cells[18, 10].Value2 = "s DPH";
            }
            if (SelectedData.getPersonalData().getName() == "Peter")
            {
                wsTemplate.Cells[17, 2].Value2 = "Názov položky";
                wsTemplate.Cells[17, 4].Value2 = "";
                wsTemplate.Cells[18, 4].Value2 = "";
                wsTemplate.Cells[17, 5].Value2 = "";
                wsTemplate.Cells[18, 5].Value2 = "";
                wsTemplate.Cells[17, 6].Value2 = "";
                wsTemplate.Cells[18, 6].Value2 = "";
                wsTemplate.Cells[17, 7].Value2 = "";
                wsTemplate.Cells[17, 8].Value2 = "Počet";
                wsTemplate.Cells[18, 8].Value2 = "ks";
                wsTemplate.Cells[17, 9].Value2 = "Cena/ks";
                wsTemplate.Cells[18, 9].Value2 = "s DPH";
                wsTemplate.Cells[17, 10].Value2 = "Cena";
                wsTemplate.Cells[18, 10].Value2 = "s DPH";
            }

            if (counter != 0)
            {
                wsTemplate.Range["B19", "B48"].Value2 = "";
                wsTemplate.Range["B19", "B48"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                wsTemplate.Range["C19", "J48"].Value2 = "";
                wsTemplate.Range["C19", "J48"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                wsTemplate.Range["B61", "B101"].Value2 = "";
                wsTemplate.Range["B61", "B101"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                wsTemplate.Range["C61", "J101"].Value2 = "";
                wsTemplate.Range["C61", "J101"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            }
            
            foreach (var iceCream in SelectedData.getIceCreamList())
            {
                iceCreamType = iceCream.gettype();
                name = iceCream.getname();
                amount = (float)(iceCream.getamount());
                priceForUnitWithDPH = (iceCreamType == "S") ? (float)(SelectedData.getPersonalData().getPricesS()) : (float)(SelectedData.getPersonalData().getPricesN());
                priceForUnitWithoutDPH = priceForUnitWithDPH / DPHCoefficient;
                totalPrice = amount * priceForUnitWithDPH;
                totalPriceWithoutDPH = totalPrice / DPHCoefficient;
                DPHFromTotalPrice = totalPrice - totalPriceWithoutDPH;
                sum1 += amount;
                sum2 += totalPriceWithoutDPH;
                sum3 += DPHFromTotalPrice;
                sum4 += totalPrice;

                if (i == 45)
                {
                    i = 61;                    
                }
                if (SelectedData.getPersonalData().getName() == "Zuzana")
                {
                    wsTemplate.Cells[i, 2].Value2 = name;
                    wsTemplate.Cells[i, 4].Value2 = Math.Round(amount, 2).ToString("F") + " ks";
                    wsTemplate.Cells[i, 5].Value2 = Math.Round(priceForUnitWithoutDPH, 2).ToString("F") + " €";
                    wsTemplate.Cells[i, 6].Value2 = Math.Round(priceForUnitWithDPH, 2).ToString("F") + " €";
                    wsTemplate.Cells[i, 7].Value2 = Math.Round(DPH, 2).ToString() + "%";
                    wsTemplate.Cells[i, 8].Value2 = Math.Round(totalPriceWithoutDPH, 2).ToString("F") + " €";
                    wsTemplate.Cells[i, 9].Value2 = Math.Round(DPHFromTotalPrice, 2).ToString("F") + " €";
                    wsTemplate.Cells[i, 10].Value2 = Math.Round(totalPrice, 2).ToString("F") + " €";
                }
                if (SelectedData.getPersonalData().getName() == "Peter")
                {
                    wsTemplate.Cells[i, 2].Value2 = name;
                    wsTemplate.Cells[i, 8].Value2 = Math.Round(amount, 2).ToString("F") + " ks";
                    wsTemplate.Cells[i, 9].Value2 = Math.Round(priceForUnitWithDPH, 2).ToString("F") + " €";
                    wsTemplate.Cells[i, 10].Value2 = Math.Round(totalPrice, 2).ToString("F") + " €";
                }


                i++;
            }
            if (SelectedData.getPersonalData().getName() == "Zuzana")
            {
                wsTemplate.Cells[i, 2].Value2 = "'---------------------------------------------------------------------------------------------------------------------------------------------------------";
                i++;
                wsTemplate.Cells[i, 2].Value2 = "Spolu:";
                wsTemplate.Cells[i, 3].Value2 = "";
                wsTemplate.Cells[i, 4].Value2 = sum1.ToString("F") + " ks";
                wsTemplate.Cells[i, 5].Value2 = "";
                wsTemplate.Cells[i, 6].Value2 = "";
                wsTemplate.Cells[i, 7].Value2 = "";
                wsTemplate.Cells[i, 8].Value2 = Math.Round(sum2, 2).ToString("F") + " €";
                wsTemplate.Cells[i, 9].Value2 = Math.Round(sum3, 2).ToString("F") + " €";
                wsTemplate.Cells[i, 10].Value2 = sum4.ToString("F") + " €";
                i++;
                wsTemplate.Cells[i, 2].Value2 = "Spolu v litroch:";
                wsTemplate.Cells[i, 5].Value2 = "Mliečne:";
                wsTemplate.Cells[i, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                wsTemplate.Cells[i, 6].Value2 = calculateVolume("M") + "l";
                wsTemplate.Cells[i, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                wsTemplate.Cells[i, 7].Value2 = "Ovocné:";
                wsTemplate.Cells[i, 7].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                wsTemplate.Cells[i, 8].Value2 = calculateVolume("F") + "l";
                wsTemplate.Cells[i, 8].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                wsTemplate.Cells[i, 9].Value2 = "Sorbety:";
                wsTemplate.Cells[i, 9].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                wsTemplate.Cells[i, 10].Value2 = calculateVolume("S") + "l";
                wsTemplate.Cells[i, 10].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                i++;
                wsTemplate.Cells[i, 2].Value2 = "'---------------------------------------------------------------------------------------------------------------------------------------------------------";
            }
            if (SelectedData.getPersonalData().getName() == "Peter")
            {
                wsTemplate.Cells[i, 2].Value2 = "'---------------------------------------------------------------------------------------------------------------------------------------------------------";
                i++;
                wsTemplate.Cells[i, 2].Value2 = "Spolu:";
                wsTemplate.Cells[i, 8].Value2 = sum1.ToString("F") + " ks";
                wsTemplate.Cells[i, 10].Value2 = sum4.ToString("F") + " €";
                i++;
                
                wsTemplate.Cells[i, 2].Value2 = "Spolu v litroch:";
                wsTemplate.Cells[i, 5].Value2 = "Mliečne:";
                wsTemplate.Cells[i, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                wsTemplate.Cells[i, 6].Value2 = calculateVolume("M") + "l";
                wsTemplate.Cells[i, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                wsTemplate.Cells[i, 7].Value2 = "Ovocné:";
                wsTemplate.Cells[i, 7].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                wsTemplate.Cells[i, 8].Value2 = calculateVolume("F") + "l";
                wsTemplate.Cells[i, 8].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                wsTemplate.Cells[i, 9].Value2 = "Sorbety:";
                wsTemplate.Cells[i, 9].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                wsTemplate.Cells[i, 10].Value2 = calculateVolume("S") + "l";
                wsTemplate.Cells[i, 10].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                i++;
                wsTemplate.Cells[i, 2].Value2 = "'---------------------------------------------------------------------------------------------------------------------------------------------------------";
            }

            if (i > 48)
                singlePage = false;
            else
                singlePage = true;
        }


        public void closeExcel()
        {
            excel.DisplayAlerts = false;
            wbTemplate.Close(0);
            excel.Quit();
        }

    }

}





