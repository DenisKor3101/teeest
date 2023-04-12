using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEts.Entity;

namespace TEts.View
{

    public partial class ModeratorReg : Form
    {
        Autorisation au;
        public ModeratorReg(Autorisation au)
        {
            InitializeComponent();
            this.au = au;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            au.Show();
        }

        private void ModeratorReg_Load(object sender, EventArgs e)
        {
            Combo();

            int chislo = 0;
            Random rnd = new Random();
            chislo = rnd.Next();
            textBoxCode.Text = chislo.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {


            var add = new User()
            {
                NumberId = Convert.ToInt32(textBoxCode.Text),
                LastName = textBoxLastName.Text,
                FirstName = textBoxFirstName.Text,
                Patronymic = textBoxPatronimic.Text,
                GenderId = (int)comboBoxPol.SelectedValue,
                Phone = maskedTextBoxphone.Text,
                Birthday = Convert.ToDateTime(textBoxDateRoj.Text),
                Email = textBoxEmail.Text,
                Login = textBoxLogin.Text,
                Password = textBoxPassword.Text,
                RoleId = (int)comboBoxRole.SelectedValue
            };

            try
            {
                Helper.DBContext.User.Add(add);
                Helper.DBContext.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Пользователь добавлен!");

        }

        private void Combo()
        {
            comboBoxPol.DataSource = Helper.DBContext.Gender.ToList();
            comboBoxPol.ValueMember = "GenderId";
            comboBoxPol.DisplayMember = "GenderName";
            
            comboBoxRole.DataSource = Helper.DBContext.Role.ToList();
            comboBoxRole.ValueMember = "RoleId";
            comboBoxRole.DisplayMember = "RoleName";
            
        
        }
    }
}
