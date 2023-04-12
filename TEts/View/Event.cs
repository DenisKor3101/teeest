using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEts.View
{
    public partial class Event : Form
    {
        public Event()
        {
            InitializeComponent();
        }
        Bitmap bmp;
        string path = Application.StartupPath + @"\photo\";

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {

            }
        }

        private void Event_Load(object sender, EventArgs e)
        {

            tableLayoutPanel1.BackColor = Color.FromArgb(231, 250, 191);
            tableLayoutPanel2.BackColor = Color.FromArgb(231, 250, 191);
            info();
            comboBoxOrder.SelectedIndex = 0;

            comboBoxDeriction.DataSource = Helper.DBContext.Direction.ToList();
            comboBoxDeriction.ValueMember = "DirectionId";
            comboBoxDeriction.DisplayMember = "DirectionName";
        }

        private void info()
        {
            dataGridView1.Rows.Clear();

            int i = 0;

            var info = Helper.DBContext.Event.ToList();
            
            int countFirst = info.Count();

            switch (comboBoxOrder.SelectedIndex)
            {
                case 0: info = info.OrderByDescending(x => x.EventDate).ToList();
                    break;
                case 1: info = info.OrderBy(x => x.EventDate).ToList(); 
                    break;
            }

            if(!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                info = info.Where(x => x.EventName.Contains(textBoxSearch.Text)).ToList();
            }

            int countEnd = info.Count();

            labelAll.Text = countFirst + " из " + countEnd;

            foreach (var item in info)
            {

                dataGridView1.Rows.Add();

                

                dataGridView1.Rows[i].Cells[0].Value = item.EventId;
                if (String.IsNullOrEmpty(item.EventPhoto))
                {
                    bmp = TEts.Properties.Resources.picture;
                    dataGridView1.Rows[i].Cells[1].Value = bmp;
                }
                else
                {
                  bmp = new Bitmap(path + item.EventPhoto);
                  dataGridView1.Rows[i].Cells[1].Value = bmp;
                }

                dataGridView1.Rows[i].Cells[2].Value = "Название: " + item.EventName + '\n' 
                    + "Направление: " + item.Direction.DirectionName + '\n' 
                    + "Дата: " + item.EventDate + '\n' 
                    + "Время: " + item.EventTime + '\n' 
                    + "Продолжительность: " + item.EventDuration + " минут";

                if(item.EventDate == DateTime.Today)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                
                i++;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new Autorisation(this).ShowDialog(); 
        }

        private void comboBoxOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            info();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            info();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddEditMerop(this).ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string art = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            new AddEditMerop(this, Convert.ToInt32(art)).ShowDialog();
        }
    }
}
