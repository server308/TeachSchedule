using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOTPISP_2
{   
   public enum Day_of_week
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }

    public enum LessonTime
    {
        FirstPair = 1,
        SecondPair = 2,
        ThirdPair = 3,
        FourthPair = 4
    }

    public enum FormLearn
    {
        Ochn,
        Zaochn
    }

    public class TimeSlot
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public TimeSlot(string startTime, string endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public override string ToString()
        {
            return $"{StartTime} - {EndTime}";
        }
    }
    public class Lesson
    {
        private string nazvanie;
        private string description;
        private string FIO_Prepod;
        private string Number_of_cabinet;
        private Day_of_week Day_of;
        private LessonTime lessonTime;
        
        // Конструктор
        public Lesson(string nazvanie, string description, string fioPrepod, string numberOfCabinet, Day_of_week dayOf, LessonTime lessonTime)
        {
            this.nazvanie = nazvanie;
            this.description = description;
            this.FIO_Prepod = fioPrepod;
            this.Number_of_cabinet = numberOfCabinet;
            this.Day_of = dayOf;
            this.lessonTime = lessonTime;
        }

        // Геттеры и сеттеры
        public string Nazvanie
        {
            get { return nazvanie; }
            set { nazvanie = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string fIO_Prepod
        {
            get { return FIO_Prepod; }
            set { FIO_Prepod = value; }
        }

        public string number_of_cabinet
        {
            get { return Number_of_cabinet; }
            set { Number_of_cabinet = value; }
        }

        public Day_of_week day_of
        {
            get { return Day_of; }
            set { Day_of = value; }
        }

        public LessonTime LessonTime
        {
            get { return lessonTime; }
            set { lessonTime = value; }
        }
    }

    public class Raspisanie
    {
        private string GroupName;
        private int Number_of_course;
        private FormLearn form_learn;
        private List<Lesson> lessons;

        // Конструктор
        public Raspisanie(string groupname, int numberOfCourse, FormLearn formLearn)
        {
            GroupName = groupname;
            Number_of_course = numberOfCourse;
            form_learn = formLearn;
            lessons = new List<Lesson>();
        }

        // Геттеры и сеттеры
        public int number_of_course
        {
            get { return Number_of_course; }
            set { Number_of_course = value; }
        }

        public string Group
        {
            get { return GroupName; }
            set { GroupName = value; }
        }

        public FormLearn FormLearn
        {
            get { return form_learn; }
            set { form_learn = value; }
        }

        public List<Lesson> Lessons
        {
            get { return new List<Lesson>(lessons); } // Возвращаем копию списка
                                                      // Установка списка невозможно (комментарий ниже к методам добавления/изменения уроков)
        }

        public void AddSchedule(Lesson lesson)
        {
            lessons.Add(lesson);
        }

        public bool EditSchedule(Lesson existingLesson, Lesson updatedLesson)
        {
            int index = lessons.IndexOf(existingLesson);
            if (index != -1)
            {
                lessons[index] = updatedLesson;
                return true;
            }
            return false;
        }

        public bool DeleteSchedule(Lesson lesson)
        {
            return lessons.Remove(lesson);
        }

        public List<Lesson> GetAllLessons()
        {
            return new List<Lesson>(lessons); // Возвращаем копию списка уроков
        }
    }
  
    public partial class Form1 : Form
    {

        private const string TimePattern = @"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$";
        public List<KeyValuePair<LessonTime, string>> pairs = new List<KeyValuePair<LessonTime, string>>();
        public List<Raspisanie> raspisanieList = new List<Raspisanie>();
        private bool canEdit = false;
        public LinkLabel linkLabel1 = new LinkLabel();
       
        public Form1()
        {



            InitializeComponent();
            listBox1.DoubleClick += listBox1_DoubleClick;
            linkLabel1.Visible = false;
            pairs.Add(new KeyValuePair<LessonTime, string>(LessonTime.FirstPair, "8:30 - 9:55"));
            pairs.Add(new KeyValuePair<LessonTime, string>(LessonTime.SecondPair, "10:05 - 11:30"));
            pairs.Add(new KeyValuePair<LessonTime, string>(LessonTime.ThirdPair, "11:45 - 13:10"));
            pairs.Add(new KeyValuePair<LessonTime, string>(LessonTime.FourthPair, "13:40 - 15:05"));

          //  raspisanie = new Raspisanie("22-ИТ-1",1, FormLearn.Ochn);
            InitializeLessons(); // Инициализация уроков
            UpdateListBox(); // Обновление ListBox

           
            this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(DataGridView1_CellDoubleClick);
           
        }

        private void InitializeLessons()
        {
           


            // Добавим некоторые уроки в расписание
           /* raspisanie.AddSchedule(new Lesson("Математика", "Лекции и практика", "Иванов И.И.", "101", Day_of_week.Monday, LessonTime.FirstPair));            raspisanie.AddSchedule(new Lesson("Физика", "Лабораторные работы", "Петров П.П.", "102", Day_of_week.Wednesday, LessonTime.SecondPair));
            raspisanie.AddSchedule(new Lesson("Программирование", "Практические занятия", "Сидоров С.С.", "104", Day_of_week.Friday, LessonTime.ThirdPair));
            raspisanie.AddSchedule(new Lesson("История", "Обсуждение тем", "Кузнецова А.А.", "103", Day_of_week.Tuesday, LessonTime.FourthPair));*/
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            foreach (var raspisanie in raspisanieList)
            {
                listBox1.Items.Add(raspisanie.Group);
            }

        }

        private void CreateNewSchedule()
        {
            using (var newScheduleForm = new NewScheduleForm())
            {
                if (newScheduleForm.ShowDialog() == DialogResult.OK)
                {
                    var newRaspisanie = new Raspisanie(newScheduleForm.GroupName, newScheduleForm.CourseNumber, newScheduleForm.FormLearn);
                    raspisanieList.Add(newRaspisanie);
                    UpdateListBox();
                }
            }
        }

       


        private void OpenLoginForm()
        {   
            using (var loginForm = new LoginForm())
            {
                loginForm.ShowDialog();
                canEdit = loginForm.IsAuthenticated; // Получаем статус аутентификации
            }
        }

        
         private void UpdateDataGridView(Raspisanie selectedSchedule)
         {
                DataTable table = GetData(selectedSchedule);
                dataGridView1.DataSource = table;
                dataGridView1.ReadOnly = !canEdit;
                dataGridView1.Columns[0].ReadOnly = true;
         }



        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenLoginForm();
           if (canEdit == true)
            {
                button1.Visible = false;
                linkLabel1.Visible = true;
                linkLabel1.Text = "Выйти"; // текст ссылки
                linkLabel1.AutoSize = true; // автоматически подгоняем размер
                linkLabel1.Location = new Point(button1.Left, button1.Bottom); // располагаем под button1 с отступом в 5 пикселей
                linkLabel1.LinkClicked += linkLabel1_LinkClicked; // Подписка на событие клика
                Controls.Add(linkLabel1);
            }

        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            canEdit = false;
            button1.Visible = true;
            linkLabel1.Visible = false; 
            MessageBox.Show("Вы вышли из системы");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var selectedSchedule = raspisanieList[listBox1.SelectedIndex];
                UpdateDataGridView(selectedSchedule);
            }
        }

       

        private DataTable GetData(Raspisanie selectedSchedule)
        {
            DataTable table = new DataTable();

            // Создаем столбцы таблицы
            table.Columns.Add("Время");
            table.Columns.Add("Понедельник");
            table.Columns.Add("Вторник");
            table.Columns.Add("Среда");
            table.Columns.Add("Четверг");
            table.Columns.Add("Пятница");
            table.Columns.Add("Суббота");

            foreach (var pair in pairs)
            {
                DataRow newRow = table.NewRow();
                newRow["Время"] = pair.Value;

                // Подстановка уроков в соответствующие ячейки
                foreach (var lesson in selectedSchedule.GetAllLessons())
                {
                    string lessonInfo = " ";
                    if (lesson.Nazvanie != null)
                    {
                        lessonInfo = $"{lesson.Nazvanie}\n{lesson.fIO_Prepod}\n{lesson.number_of_cabinet}\n";
                    }
                    if (lesson.LessonTime == pair.Key)
                    {
                        switch (lesson.day_of)
                        {
                            case Day_of_week.Monday:
                                newRow["Понедельник"] = lessonInfo;
                                break;
                            case Day_of_week.Tuesday:
                                newRow["Вторник"] = lessonInfo;
                                break;
                            case Day_of_week.Wednesday:
                                newRow["Среда"] = lessonInfo;
                                break;
                            case Day_of_week.Thursday:
                                newRow["Четверг"] = lessonInfo;
                                break;
                            case Day_of_week.Friday:
                                newRow["Пятница"] = lessonInfo;
                                break;
                            case Day_of_week.Saturday:
                                newRow["Суббота"] = lessonInfo;
                                break;
                        }
                    }
                }

                table.Rows.Add(newRow);
            }

            return table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (canEdit && e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Редактируем время занятия
                string newTime = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (Regex.IsMatch(newTime, TimePattern))
                {
                    using (EditTimeForm editTimeForm = new EditTimeForm(newTime))
                    {
                        if (editTimeForm.ShowDialog() == DialogResult.OK)
                        {
                            string updatedTime = editTimeForm.UpdatedTime;
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = updatedTime;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Некорректный формат времени. Используйте формат 'HH:MM - HH:MM'.");
                }
            }

            if (canEdit && e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string lessonInfo = selectedRow.Cells[e.ColumnIndex].Value?.ToString() ?? string.Empty;

                // Определяем текущее выбранное расписание
                Raspisanie selectedSchedule = raspisanieList[listBox1.SelectedIndex];

                Day_of_week dayOfWeek = GetDayOfWeekFromColumnIndex(e.ColumnIndex);
                LessonTime lessonTime = GetLessonTimeFromRowIndex(e.RowIndex);

                Lesson existingLesson = FindLessonByInfo(lessonInfo, selectedSchedule);

                // Если урок найден, редактируем
                if (existingLesson != null)
                {
                    using (EditForm editForm = new EditForm(existingLesson, selectedSchedule))
                    {
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            selectedSchedule.EditSchedule(existingLesson, editForm.EditedLesson);
                            UpdateDataGridView(selectedSchedule); // Обновляем DataGridView после редактирования
                        }
                    }
                }
                else
                {
                    // Создаем новый урок, если урок отсутствует
                    using (EditForm editForm = new EditForm(new Lesson(string.Empty, string.Empty, string.Empty, string.Empty, dayOfWeek, lessonTime), selectedSchedule))
                    {
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            selectedSchedule.AddSchedule(editForm.EditedLesson);
                            UpdateDataGridView(selectedSchedule); // Обновляем DataGridView после добавления
                        }
                    }
                }
            }
        }




        // Метод для получения дня недели на основе индекса столбца
        private Day_of_week GetDayOfWeekFromColumnIndex(int columnIndex)
        {
            switch (columnIndex)
            {
                case 1: return Day_of_week.Monday;
                case 2: return Day_of_week.Tuesday;
                case 3: return Day_of_week.Wednesday;
                case 4: return Day_of_week.Thursday;
                case 5: return Day_of_week.Friday;
                case 6: return Day_of_week.Saturday;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        // Метод для получения времени урока на основе индекса строки
        private LessonTime GetLessonTimeFromRowIndex(int rowIndex)
        {
            return pairs[rowIndex].Key; // предполагается, что список пар соответствует порядку строк в DataGridView
        }


        private Lesson FindLessonByInfo(string info, Raspisanie select)
        {
            var lessonDetails = info.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(line => line.Trim())
                                     .ToArray();

            // Параметры для сравнения
            string nazvanie = lessonDetails.ElementAtOrDefault(0) ?? string.Empty;
            string fioPrepod = lessonDetails.ElementAtOrDefault(1) ?? string.Empty;
            string numberOfCabinet = lessonDetails.ElementAtOrDefault(2) ?? string.Empty;

            return select.GetAllLessons()
                             .FirstOrDefault(lesson => lesson.Nazvanie.Equals(nazvanie, StringComparison.InvariantCulture) &&
                                                        lesson.fIO_Prepod.Equals(fioPrepod, StringComparison.InvariantCulture) &&
                                                        lesson.number_of_cabinet.Equals(numberOfCabinet, StringComparison.InvariantCulture));
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (canEdit == true)
            CreateNewSchedule();
            else
            {
                MessageBox.Show("Вы не имеете прав для добавления расписания.", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (canEdit == true)
            {
                if (listBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Пожалуйста, выберите расписание для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Запрашиваем подтверждение на удаление
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить расписание?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Получаем индекс выбранного расписания и удаляем его
                    int selectedIndex = listBox1.SelectedIndex;
                    raspisanieList.RemoveAt(selectedIndex);
                    UpdateListBox(); // Обновляем ListBox
                }
            }
            else
            {
                MessageBox.Show("Вы не имеете прав для удаления расписания.", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (canEdit)
            {
                if (listBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Пожалуйста, выберите расписание для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedSchedule = raspisanieList[listBox1.SelectedIndex];

                // Создание и показ формы для редактирования расписания
                using (var editScheduleForm = new ScheduleDetailsForm(selectedSchedule))
                {
                    if (editScheduleForm.ShowDialog() == DialogResult.OK)
                    {
                        selectedSchedule.Group = editScheduleForm.raspisanie.Group;
                        selectedSchedule.number_of_course = editScheduleForm.raspisanie.number_of_course;
                        selectedSchedule.FormLearn = editScheduleForm.raspisanie.FormLearn;
                        UpdateListBox();
                    }
                }
                UpdateListBox();
            }
            else
            {
                MessageBox.Show("Вы не имеете прав для редактирования расписания.", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
    public class EditTimeForm : Form
    {
        private TextBox textBoxNewTime;
        private Button buttonOK, buttonCancel;

        public string UpdatedTime => textBoxNewTime.Text;

        public EditTimeForm(string currentTime)
        {
            textBoxNewTime = new TextBox { Text = currentTime, Dock = DockStyle.Fill };
            buttonOK = new Button { Text = "OK", Dock = DockStyle.Bottom };
            buttonCancel = new Button { Text = "Cancel", Dock = DockStyle.Bottom };

            buttonOK.Click += buttonOK_Click;
            buttonCancel.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            Controls.Add(textBoxNewTime);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Проверяем корректность формата времени
            if (Regex.IsMatch(textBoxNewTime.Text, @"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректный формат времени. Используйте формат 'HH:MM - HH:MM'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
