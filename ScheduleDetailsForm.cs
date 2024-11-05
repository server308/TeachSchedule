using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOTPISP_2
{
    public partial class ScheduleDetailsForm : Form
    {
        public Raspisanie raspisanie;

        public ScheduleDetailsForm(Raspisanie raspisanie)
        {
            InitializeComponent();
            this.raspisanie = raspisanie;
            LoadData();
        }

        private void LoadData()
        {
            textBox1.Text = raspisanie.Group;
            textBox2.Text = raspisanie.number_of_course.ToString();

            // Установка выбранного RadioButton в зависимости от формы обучения
            radioButton1.Checked = (raspisanie.FormLearn == FormLearn.Ochn);
            radioButton2.Checked = (raspisanie.FormLearn == FormLearn.Zaochn);

            this.Text = "Детали расписания";
            this.Size = new Size(300, 200);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Изменение формы обучения в зависимости от выбранного RadioButton
            if (radioButton1.Checked)
            {
                raspisanie.FormLearn = FormLearn.Ochn;
            }
            else if (radioButton2.Checked)
            {
                raspisanie.FormLearn = FormLearn.Zaochn;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            raspisanie.Group = textBox1.Text;
            if (int.TryParse(textBox2.Text, out int course))
            {
                raspisanie.number_of_course = course;
            }

            // Закрытие формы после сохранения
            MessageBox.Show("Данные успешно сохранены.");
            this.Close();
        }

        private void ScheduleDetailsForm_Load(object sender, EventArgs e)
        {
            // Возможно, инициализация данных при загрузке формы
        }

        // Вы можете удалить следующие методы, если они не используются
        private void label1_Click(object sender, EventArgs e) { }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_CheckedChanged(sender, e);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_CheckedChanged(sender, e);
        }

        

       
    }
}
