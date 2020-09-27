using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proj_IceCream
{
    public partial class Form1 : Form
    {
        private LoadingScreen loadingScreen;
        private JsonDriver ex;
        private SelectedData selectedData;
        private Print print;
        private bool iceCreamNameFound = false, answer = false;

        List<Rectangle> OriginalRectList = new List<Rectangle>();
        List<Button> ButtonList = new List<Button>();
        List<Label> LabelList = new List<Label>();
        List<ComboBox> ComboBoxList = new List<ComboBox>();
        List<TextBox> TextBoxList = new List<TextBox>();
        List<Control> ControlList = new List<Control>();

        private Size formOriginalSize;

        string ProjectIceCreamPath;
        string TemplatePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {  
            fillResizeLists();
            formOriginalSize = this.Size;
            loadingScreen = new LoadingScreen();
            loadingScreen.Show();

            loadingScreen.setLoadingLabel("Getting file paths");
            string executingFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            ProjectIceCreamPath = System.IO.Path.Combine(executingFolder, "ProjectIceCream.xlsx");
            TemplatePath = System.IO.Path.Combine(executingFolder, "data\\Template.xlsx");
            loadingScreen.setLoadingLabel("Loading Template.xlsx");
            print = new Print(TemplatePath);

            Console.WriteLine(print.GetType().ToString());

            Icon icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(executingFolder, "Proj_IceCream.ico"));
            this.Icon = icon;

            printButton.Enabled = false;
            previewButton.Enabled = false;

            loadingScreen.setLoadingLabel("Loading JsonFiles");
            ex = new JsonDriver(ProjectIceCreamPath);
            selectedData = new SelectedData();
            loadingScreen.setLoadingLabel("Loading ice cream data");
            LoadIceCreamData();
            loadingScreen.setLoadingLabel("Getting clients");
            LoadClients();
            loadingScreen.setLoadingLabel("Starting cars");
            LoadCars();
            loadingScreen.setLoadingLabel("Notifiyng sellers");
            LoadSelers();

            this.Visible = true;
            loadingScreen.Close();
        }

        private void fillResizeLists()
        {
            foreach (Control c in this.Controls)
            {
                ControlList.Add(c);
                OriginalRectList.Add(new Rectangle(c.Location.X, c.Location.Y, c.Width, c.Height));
            }
        }

        private void manageResize()
        {
            for(int i = 0;i<ControlList.Count;i++)
            {
                resizeControl(OriginalRectList[i], ControlList[i]);
            }
        }

        private void resizeControl(Rectangle originalControlRect, Control control)
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);

            int newX = (int)(originalControlRect.X * xRatio);
            int newY = (int)(originalControlRect.Y * yRatio);

            int newWidth = (int)(originalControlRect.Width * xRatio);
            int newHeight = (int)(originalControlRect.Height * yRatio);

            control.Location = new Point(newX, newY);
            control.Size = new Size(newWidth, newHeight);
        }

        //parse data to coresponding textbox
        private void LoadIceCreamData()
        {
            List<IceCream> iceCreamList = ex.getIceCreamList();
            string icecream;

            for (int i = 0; i < iceCreamList.Count; i++)
            {
                switch (iceCreamList[i].gettype())
                {
                    case "M":                       
                        icecream = iceCreamList[i].getID() + " " + iceCreamList[i].getname() + "\n";
                        MilkBaseTextBox.Text = MilkBaseTextBox.Text + icecream;
                        break;
                    case "F":
                        icecream = iceCreamList[i].getID() + " " + iceCreamList[i].getname() + "\n";
                        FruitBaseTextBox.Text = FruitBaseTextBox.Text + icecream;
                        break;
                    case "I":
                        icecream = iceCreamList[i].getID() + " " + iceCreamList[i].getname() + "\n";
                        InterlacedTextBox.Text =  InterlacedTextBox.Text + icecream;
                        break;
                    case "S":
                        icecream = iceCreamList[i].getID() + " " + iceCreamList[i].getname() + "\n";
                        SorbetBaseTextBox.Text = SorbetBaseTextBox.Text + icecream;
                        break;
                }
            }
        }

        //fill Clients combobox
        private void LoadClients()
        {
            clientsComboBox.Items.Clear();

            List<Clients> clients = ex.getClientsList();

            for (int i = 0; i < clients.Count; i++)
            {
                clientsComboBox.Items.Add(clients[i].getNick());
            }
        }

        //fill Cars combobox
        private void LoadCars()
        {
            List<Car> c = ex.getCarList();

            for (int i = 0; i < c.Count; i++)
            {
                carsComboBox.Items.Add(c[i].getName());
            }
        }

        //fill Selers combobox
        private void LoadSelers()
        {
            List<PersonalData> s = ex.getPersonalDataList();

            for (int i = 0; i < s.Count; i++)
            {
                selerComboBox.Items.Add(s[i].getName());
            }
        }

        //text validation for man textbox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string allowedLetters = "0123456789*";



            if (ValidateText(textBox1.Text, allowedLetters))
            {
                OutputLabel.Text = FormatOutputText(textBox1.Text, "*");

            }
            else
            {
                string temp = textBox1.Text.Remove(textBox1.Text.Length - 1);
                textBox1.Text = temp;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
            }           
        }

        //error check
        private bool ValidateText(string texToValidate, string allowedLetters)
        {
            int multiplyCount = 0;

             foreach (char c in texToValidate)
             {
                 if (!allowedLetters.Contains(c.ToString()))
                     return false;

                if (c.ToString().Contains("*"))
                    multiplyCount++;

                if (multiplyCount > 1)
                    return false;
             }
             return true;         
        }

        //format output text for outputLabel marked Vypis at startup
        private string FormatOutputText(string textToFormat, string allowedLetters)
        {
            string output;
            bool containsMultiply = false;
            int charLocation = 0;
            iceCreamNameFound = false;

            foreach(char c in textToFormat)
            {
                if (allowedLetters.Contains(c.ToString()))
                {
                    containsMultiply = true;
                    charLocation = textToFormat.IndexOf("*", StringComparison.Ordinal);
                    break;
                }
            }

            if (containsMultiply)
            {
                output = textToFormat.Substring(0, charLocation) + "*";
                OutputLabel.Text = textToFormat.Substring(0, charLocation);

                if (charLocation + 1 == textToFormat.Length)
                {
                    return output;
                }

                int iceCreamId = int.Parse(textToFormat.Substring(charLocation + 1));

                for (int i = 0; i < ex.getIceCreamList().Count; i++)
                {
                    if (ex.getIceCreamList()[i].getID() == iceCreamId)
                    {
                        output = output + ex.getIceCreamList()[i].getname();
                        iceCreamNameFound = true;
                    }
                }

                if (!iceCreamNameFound)
                    output = output + "Zmrzlina neexistuje";
            }
            else
            {
                output = textToFormat;
            }

            return output;
        }

        //method detect id enter button is pressed and then add value to output
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddToOutput();
            }
        }

        //add sellected iceCream to output (same as pressing Enter button)
        private void addButton_Click(object sender, EventArgs e)
        {
            AddToOutput();
        }

        //adds iceCream to ouput text
        private void AddToOutput()
        {
            if (iceCreamNameFound)
            {
                int charLocation = OutputLabel.Text.IndexOf("*", StringComparison.Ordinal);

                if (charLocation == 0 || charLocation == OutputLabel.Text.Length - 1)
                {
                    textBox1.Text = String.Empty;
                    return;
                }

                int ammount = int.Parse(OutputLabel.Text.Substring(0, charLocation));
                string name = OutputLabel.Text.Substring(charLocation + 1);

                for (int i = 0; i < ex.getIceCreamList().Count; i++)
                {
                    if (ex.getIceCreamList()[i].getname() == name)
                    {
                        IceCream select = new IceCream(ex.getIceCreamList()[i].getID(), ex.getIceCreamList()[i].getname(), ex.getIceCreamList()[i].gettype());
                        select.setAmount(ammount);
                        selectedData.addToList(select);
                        break;
                    }
                }

                reDrawOutput();
            }
            else
            {
                textBox1.Text = String.Empty;
            }

            canPrint();
        }

        //print template
        private void printButton_Click(object sender, EventArgs e)
        {
            string s = "Tlačiť dokument?";
            InitializePopup(s);

            if (answer)
            {
                printInitialisation();
                print.print();
            }
            answer = false;
        }

        //resize form
        private void Form1_Resize(object sender, EventArgs e)
        {
            manageResize();
        }

        //save and close excel when program stops
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ex.closeExcel();
            if (print != null)
                print.closeExcel();
        }

        //remove from list button
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                string s = "Naozaj chcete zmazať všetko?";
                InitializePopup(s);

                if (answer)
                {
                    outputTextBox.Text = string.Empty;
                    selectedData.resetIceCream();
                    answer = false;
                    return;
                }
                else
                {
                    answer = false;
                    return;
                }
            }
            

            int charLocation = OutputLabel.Text.IndexOf("*", StringComparison.Ordinal);

            int ammount = int.Parse(OutputLabel.Text.Substring(0, charLocation));
            string name = OutputLabel.Text.Substring(charLocation + 1);

            for (int i = 0; i < ex.getIceCreamList().Count; i++)
            {
                if (ex.getIceCreamList()[i].getname() == name)
                {
                    IceCream select = new IceCream(ex.getIceCreamList()[i].getID(), ex.getIceCreamList()[i].getname(), ex.getIceCreamList()[i].gettype());
                    select.setAmount(ammount);
                    selectedData.removeFromList(select);
                    break;
                }
            }

            reDrawOutput();
        }

        //print with preview
        private void previewButton_Click(object sender, EventArgs e)
        {
            string s = "Vytvoriť náhľad?";
            InitializePopup(s);

            if (answer)
            {              
                printInitialisation();
                print.printPreview(this);
            }
            answer = false;
        }

        //redraw output textbox with updated list
        private void reDrawOutput()
        {
            List<IceCream> selectedList = selectedData.getIceCreamList();
            outputTextBox.Text = string.Empty;

            for (int i = 0; i < selectedList.Count; i++)
            {
                string s = selectedList[i].getamount() + " * " + selectedList[i].getname() + "\n";

                outputTextBox.Text = outputTextBox.Text + s;
            }


            textBox1.Text = String.Empty;
        }

        //fill information boxes based on selected client
        private void clientsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientsComboBox.Text == "Prazdne")
            {
                ResetInfoTexts();
                return;
            }

            for (int i = 0; i < ex.getClientsList().Count; i++)
            {
                if (ex.getClientsList()[i].getNick() == clientsComboBox.Text)
                {
                    Clients c = ex.getClientsList()[i];

                    nickTextBox.Text = (c.getNick() == "-1") ? " " : c.getNick();
                    BusinesstextBox.Text = (c.getShopname() == "-1") ? " " : c.getShopname();
                    string s = string.Empty;
                    if (c.getName() != "-1")
                        s = c.getName();
                    if (c.getLastName() != "-1")
                        s = s + " " + c.getLastName();
                    ownerNametextBox.Text = s;
                    citytextBox.Text = (c.getCity() == "-1") ? " " : c.getCity();
                    streettextBox.Text = (c.getStreet() == "-1") ? " " : c.getStreet();
                    postCodetextBox.Text = (c.getPsc() == "-1") ? " " : c.getPsc();
                    statetextBox.Text = (c.getState() == "-1") ? " " : c.getState();
                    phonetextBox.Text = (c.getPhoneNumber() == "-1") ? " " : c.getPhoneNumber();
                    icotextBox.Text = (c.getIco() == "-1") ? " " : c.getIco();
                    dictextBox.Text = (c.getDic() == "-1") ? " " : c.getDic();
                    break;
                }
            }
            canPrint();
        }       

        //actions necessary before printing fill selectedData Class and creatw print object
        private void printInitialisation()
        {
            Clients c = null;
            string l = null;
            PersonalData s = null;

            //find client
            for (int i = 0; i < ex.getClientsList().Count; i++)
            {
                if (ex.getClientsList()[i].getNick() == clientsComboBox.Text)
                {
                    c = ex.getClientsList()[i];
                    break;
                }
            }
            //find license plate
            for (int i = 0; i < ex.getCarList().Count; i++)
            {
                if (ex.getCarList()[i].getName() == carsComboBox.Text)
                {
                    l = ex.getCarList()[i].getLicensePlate();
                    break;
                }
            }
            //find seler
            for (int i = 0; i < ex.getPersonalDataList().Count; i++)
            {
                if (ex.getPersonalDataList()[i].getName() == selerComboBox.Text)
                {
                    s = ex.getPersonalDataList()[i];
                    break;
                }
            }

            s.setLastID(SelerIdtextBox.Text);
            SelerIdtextBox.Text = (Convert.ToInt32(SelerIdtextBox.Text) + 1).ToString(); 
            selectedData.setPersonalData(s);
            selectedData.setClient(c);
            selectedData.setLicensePlate(l);
            print.setSelectedData(selectedData);
        }             

        private void carsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            canPrint();
        }

        private void selerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PersonalData pd = ex.getPersonalDataList()[selerComboBox.SelectedIndex];
            SelerIdtextBox.Text = (Convert.ToInt32(pd.getLastID()) + 1).ToString();
            yeartextBox.Text = pd.getYearOFLastID().Substring(pd.getYearOFLastID().Length - 2);
            canPrint();
        }

        public void setAnswer(bool status)
        {
            answer = status;
        }

        //if all conditions are met we can print or preview
        private void canPrint()
        {
            if (clientsComboBox.SelectedIndex == -1 || selerComboBox.SelectedIndex == -1 || carsComboBox.SelectedIndex == -1)
            {
                printButton.Enabled = false;
                previewButton.Enabled = false;
            }
            else
            {
                printButton.Enabled = true;
                previewButton.Enabled = true;
            }
        }

        private void SelerIdtextBox_TextChanged(object sender, EventArgs e)
        {
            string allowedLetters = "0123456789";

            if (!ValidateText(SelerIdtextBox.Text, allowedLetters))
            {
                string temp = SelerIdtextBox.Text.Remove(SelerIdtextBox.Text.Length - 1);
                SelerIdtextBox.Text = temp;
                SelerIdtextBox.SelectionStart = SelerIdtextBox.Text.Length;
                SelerIdtextBox.SelectionLength = 0;
            }
        }

        private void InitializePopup(string message)
        {
            Popup p = new Popup();
            p.InitializeData(this, message);
            p.ShowDialog();
        }

        private void ManageClientsbutton_Click(object sender, EventArgs e)
        {
            ManageClients c = new ManageClients();
            c.InitializeData(ex);
            c.ShowDialog();
            LoadClients();
        }

        private void ResetInfoTexts()
        {
            nickTextBox.Text = string.Empty;
            BusinesstextBox.Text = string.Empty;
            ownerNametextBox.Text = string.Empty;
            citytextBox.Text = string.Empty;
            streettextBox.Text = string.Empty;
            postCodetextBox.Text = string.Empty;
            statetextBox.Text = string.Empty;
            phonetextBox.Text = string.Empty;
            icotextBox.Text = string.Empty;
            dictextBox.Text = string.Empty;
        }

        public void setLoadingScreen(LoadingScreen s)
        {
            loadingScreen = s;
        }
    }
}
