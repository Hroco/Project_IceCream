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
    public partial class ManageClients : Form
    {
        private JsonDriver ex;
        private bool answer = false;

        public ManageClients()
        {
            InitializeComponent();
        }

        private void ManageClients_Load(object sender, EventArgs e)
        {

        }

        public void InitializeData(JsonDriver e)
        {
            ex = e;
            LoadClients();
        }

        private void LoadClients()
        {
            ClientComboBox.Items.Clear();
            List<Clients> clients = ex.getClientsList();

            for (int i = 0; i < clients.Count; i++)
            {
                ClientComboBox.Items.Add(clients[i].getNick());
            }
        }

        private void ClientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClientComboBox.Text == "Prazdne")
            {
                ResetInfoTexts();
                addClientButton.Enabled = true;
                saveClientButton.Enabled = false;
                deleteClientButton.Enabled = false;

                return;
            }

            for (int i = 0; i < ex.getClientsList().Count; i++)
            {
                if (ex.getClientsList()[i].getNick() == ClientComboBox.Text)
                {

                    Clients c = ex.getClientsList()[i];

                    nickTextBox.Text = (c.getNick() == "-1") ? " " : c.getNick();
                    BusinesstextBox.Text = (c.getShopname() == "-1") ? " " : c.getShopname();
                    string s = string.Empty;
                    if (c.getName() != "-1")
                        s = c.getName();
                    if (c.getLastName() != "-1")
                        s = s + " " + c.getLastName();
                    NametextBox.Text = s;
                    CitytextBox.Text = (c.getCity() == "-1") ? " " : c.getCity();
                    StreettextBox.Text = (c.getStreet() == "-1") ? " " : c.getStreet();
                    PostCodetextBox.Text = (c.getPsc() == "-1") ? " " : c.getPsc();
                    StatetextBox.Text = (c.getState() == "-1") ? " " : c.getState();
                    PhonetextBox.Text = (c.getPhoneNumber() == "-1") ? " " : c.getPhoneNumber();
                    ICOtextBox.Text = (c.getIco() == "-1") ? " " : c.getIco();
                    DICtextBox.Text = (c.getDic() == "-1") ? " " : c.getDic();

                    addClientButton.Enabled = false;
                    saveClientButton.Enabled = true;
                    deleteClientButton.Enabled = true;

                    break;
                }
            }
        }

        private void ResetInfoTexts()
        {
            nickTextBox.Text = string.Empty;
            BusinesstextBox.Text = string.Empty;
            NametextBox.Text = string.Empty;
            CitytextBox.Text = string.Empty;
            StreettextBox.Text = string.Empty;
            PostCodetextBox.Text = string.Empty;
            StatetextBox.Text = string.Empty;
            PhonetextBox.Text = string.Empty;
            ICOtextBox.Text = string.Empty;
            DICtextBox.Text = string.Empty;
        }

        private void InitializePopup(string message)
        {
            Popup p = new Popup();
            p.InitializeData(this, message);
            p.ShowDialog();
        }

        public void setAnswer(bool status)
        {
            answer = status;
        }

        private void addClientButton_Click(object sender, EventArgs e)
        {
            string s = "Naozaj chcete pridať odberatela " + nickTextBox.Text + "?";
            InitializePopup(s);

            if (answer)
            {
                string[] name = NametextBox.Text.Split(' ');
                Clients c = new Clients(0, nickTextBox.Text, BusinesstextBox.Text, name[0], name[1], CitytextBox.Text, StreettextBox.Text, PostCodetextBox.Text, StatetextBox.Text, PhonetextBox.Text, ICOtextBox.Text, DICtextBox.Text);
                ex.AddOrEditData(c);
                ResetInfoTexts();
                LoadClients();
            }

            answer = false;
        }

        private void saveClientButton_Click(object sender, EventArgs e)
        {
            string s = "Naozaj chcete upraviť odberatela " + ClientComboBox.Text + "?";
            InitializePopup(s);

            if (answer)
            {
                uint id = 0;
                string[] name = NametextBox.Text.Split(' ');

                for (int i = 0; i < ex.getClientsList().Count; i++)
                {
                    if (ex.getClientsList()[i].getNick() == ClientComboBox.Text)
                    {
                        id = ex.getClientsList()[i].getID();
                        break;
                    }
                }

                Clients c = new Clients(id, nickTextBox.Text, BusinesstextBox.Text, name[0], name[1], CitytextBox.Text, StreettextBox.Text, PostCodetextBox.Text, StatetextBox.Text, PhonetextBox.Text, ICOtextBox.Text, DICtextBox.Text);
                ex.AddOrEditData(c);
                LoadClients();
            }
            answer = false;
        }

        private void deleteClientButton_Click(object sender, EventArgs e)
        {
            string s = "Naozaj chcete zmazať odberatela " + ClientComboBox.Text + "?";
            InitializePopup(s);

            if (answer)
            {
                uint id = 0;

                for (int i = 0; i < ex.getClientsList().Count; i++)
                {
                    if (ex.getClientsList()[i].getNick() == ClientComboBox.Text)
                    {
                        id = ex.getClientsList()[i].getID();
                        break;
                    }
                }
                ex.deleteClient(id);
                ResetInfoTexts();
                LoadClients();
                ClientComboBox.Text = string.Empty;
            }

            answer = false;
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }   
}
