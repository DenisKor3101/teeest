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
    public partial class AddEditMerop : Form
    {
        Event ev;
        Bitmap bmp;
        string path = Application.StartupPath + @"\photo\";
        
        public AddEditMerop(Event ev)
        {
            InitializeComponent();
            this.ev = ev;
            buttonAddEditTov.Tag = "add";
            buttonDeleteTovar.Visible = false;
        }

        int arrticul;
        public AddEditMerop(Event param, int articul)
        {
            InitializeComponent();
            this.ev = param;
            arrticul = articul;
            buttonAddEditTov.Tag = "edit";
            buttonDeleteTovar.Visible = false;
            load();
            buttonDeleteTovar.Visible = true;
        }

        private void AddEditMerop_Load(object sender, EventArgs e)
        {
            comboBoxDirection.DataSource = Helper.DBContext.Direction.ToList();
            comboBoxDirection.ValueMember = "DirectionId";
            comboBoxDirection.DisplayMember = "DirectionName";
        }

        private void buttonAddEditTov_Click(object sender, EventArgs e)
        {
            switch (buttonAddEditTov.Tag)
            {
                case "add": addTovar();break;
                case "edit": editTovar();break;
            }
        }

        private void load()
        {
            var load = Helper.DBContext.Event.Where(x => x.EventId == arrticul).ToList().FirstOrDefault();

            textBoxId.Text = load.EventId.ToString();
            textBoxName.Text = load.EventName;
            comboBoxDirection.SelectedValue = load.DirectionId;
            dateTimePickerPa.Text = load.EventDate.ToString();
            textBoxTime.Text = load.EventTime.ToString();
            numericUpDownDuraction.Value = load.EventDuration;
            if (String.IsNullOrEmpty(load.EventPhoto))
            {
                bmp = TEts.Properties.Resources.picture;
                pictureBoxMain.Image = bmp;
            }
            else
            {
                bmp = new Bitmap(path + load.EventPhoto);
                pictureBoxMain.Image = bmp;
            }
        }

        private void addTovar()
        {
            if(String.IsNullOrEmpty(textBoxId.Text) || String.IsNullOrEmpty(textBoxName.Text) || String.IsNullOrEmpty(textBoxTime.Text))
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }



            var eventt = new Entity.Event()
            {
                EventId = Convert.ToInt32(textBoxId.Text),
                EventName = textBoxName.Text,
                DirectionId = (int)comboBoxDirection.SelectedValue,
                EventDate = Convert.ToDateTime(dateTimePickerPa.Text),
                //EventTime = (textBoxTime.Text),
                EventDuration = (int)numericUpDownDuraction.Value,
             

            };

            try
            {
                Helper.DBContext.Event.Add(eventt);
                Helper.DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Мероприятие созданно");
            this.Hide();
            ev.Show();
        } 
        private void editTovar()
        {
            var find = Helper.DBContext.Event.Find(arrticul);
            find.EventId = Convert.ToInt32(textBoxId.Text);
            find.EventName = textBoxName.Text;
            find.DirectionId = (int)comboBoxDirection.SelectedValue;
            find.EventDate = Convert.ToDateTime(dateTimePickerPa.Text);
            find.EventDuration = (int)numericUpDownDuraction.Value;

            try
            {
                Helper.DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Мероприятие изменено");
        }

        private void buttonDeleteTovar_Click(object sender, EventArgs e)
        {
            var delete = Helper.DBContext.Event.Where(x => x.EventId == arrticul).ToList().FirstOrDefault();

            if(delete != null)
            {
                Helper.DBContext.Event.Remove(delete);
                try
                {
                    Helper.DBContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                
            }
            else{
                MessageBox.Show("Ошибка");

            }

            this.Hide();
            ev.Show();
            MessageBox.Show("Меропирятие удалено!");
          

        }

        private void buttonDeletePhoto_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void buttonAddPhoto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данный функционал не реалезован!");
        }
    }
}
