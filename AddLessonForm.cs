using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OOTPISP_2
{
    public partial class AddLessonForm : Form
    {
        public Lesson NewLesson { get; private set; }

        public AddLessonForm(Day_of_week dayOfWeek, LessonTime lessonTime)
        {
            InitializeComponent();
            textBox4.Text = dayOfWeek.ToString();
            textBox5.Text = lessonTime.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddLessonForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nazvanie = textBox1.Text;
            string description = textBox2.Text;
            string fioPrepod = textBox3.Text;
            int numberOfCabinet = int.Parse(textBox6.Text);
        }
    }
}
