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
    public partial class NewScheduleForm : Form
    {
        public string GroupName { get; private set; }
        public int CourseNumber { get; private set; }
        public FormLearn FormLearn { get; private set; }

        public NewScheduleForm()
        {
            
            InitializeComponent();
            button1.Click += (sender, e) =>
            {
                GroupName = textBox1.Text;
                CourseNumber = Int32.Parse(textBox2.Text);
                if (radioButton1.Checked)
                {
                    FormLearn = FormLearn.Ochn;
                }
                else
                {
                    FormLearn = FormLearn.Zaochn;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            button2.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
