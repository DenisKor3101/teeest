using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEts.View;

namespace TEts
{
    public partial class Autorisation : Form
    {
        View.Event evvent;
        public Autorisation(View.Event evvent)
        {
            InitializeComponent();
            this.evvent = evvent;   
        }
        string alf = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {

            }
        }

        private void Autorisation_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.BackColor = Color.FromArgb(231, 250, 191);
            tableLayoutPanel2.BackColor = Color.FromArgb(231, 250, 191);
            Capthca();
            labelCapthca.Visible = false;
            textBoxCapthca.Visible = false;
        }

        private void buttonAutorisation_Click(object sender, EventArgs e)
        {
            var name = Helper.DBContext.User.Where(x => x.Login == textBoxLogin.Text && x.Password == textBoxPassword.Text).FirstOrDefault();

            
            if (name != null)
            {
                MessageBox.Show($"Вы вошли как: {name.LastName} {name.FirstName} {name.Patronymic} \n \nВаша роль: {name.Role.RoleName}", "Авторизация");
                Helper.roleId = name.RoleId;
                if(Helper.roleId == 1)
                {
                    this.Hide();
                    new ModeratorReg(this).ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Данные введены не верно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Threading.Thread.Sleep(5000);
                labelCapthca.Visible = true;
                textBoxCapthca.Visible = true;
            }
        }
        private void Capthca()
        {
            Random rnd = new Random();
            Bitmap bmp = new Bitmap(labelCapthca.Width, labelCapthca.Height);
            for (int i = 0; i < 1000; i++)
            {
                bmp.SetPixel(rnd.Next(labelCapthca.Width), rnd.Next(labelCapthca.Height), Color.Black);
            }
            labelCapthca.Image = bmp;
            string cap = "";
            for (int i = 0; i < 4; i++)
            {
                cap += alf[rnd.Next(alf.Length)].ToString();
            }
            labelCapthca.Text = cap;
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonGuest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы вошли как гость!", "Гость", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
