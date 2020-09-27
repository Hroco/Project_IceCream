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
    public partial class Popup : Form
    {
        Form1 f;
        ManageClients clientsForm;

        public Popup()
        {
            InitializeComponent();
        }

        private void Popup_Load(object sender, EventArgs e)
        {

        }

        public void InitializeData(Form1 form, string question)
        {
            questionLabel.Text = question;
            f = form;
        }

        public void InitializeData(ManageClients form, string question)
        {
            clientsForm = form;
            questionLabel.Text = question;
        }

        private void Yesbutton_Click(object sender, EventArgs e)
        {
            if (f != null)
                f.setAnswer(true);
            if (clientsForm != null)
                clientsForm.setAnswer(true);

            this.Close();
        }

        private void Nobutton_Click(object sender, EventArgs e)
        {
            if (f != null)
                f.setAnswer(false);
            if (clientsForm != null)
                clientsForm.setAnswer(false);

            this.Close();
        }

        private void questionLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
