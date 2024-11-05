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
    public partial class EditForm : Form
    {
        
        public Lesson EditedLesson { get; private set; }
        private Raspisanie raspisanie;
        public EditForm(Lesson lesson, Raspisanie raspisanie)
        {
            InitializeComponent();
            this.raspisanie = raspisanie;
            EditedLesson = lesson;
            textBox1.Text = lesson.Nazvanie;
            textBox2.Text = lesson.Description;
            textBox3.Text = lesson.fIO_Prepod;
            textBox4.Text = lesson.number_of_cabinet.ToString();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditedLesson.Nazvanie = textBox1.Text;
            EditedLesson.Description = textBox2.Text;
            EditedLesson.fIO_Prepod = textBox3.Text;
            EditedLesson.number_of_cabinet = textBox4.Text;
            this.DialogResult = DialogResult.OK; // Возвращаем результат "ОК"
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Возвращаем результат "Отмена"
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (raspisanie.DeleteSchedule(EditedLesson))
            {
                MessageBox.Show("Урок успешно удален.");
                this.DialogResult = DialogResult.OK; // Возвращаем результат "ОК"
            }
            else
            {
                MessageBox.Show("Не удалось удалить урок.");
            }

            this.Close();
        }
    }
}
