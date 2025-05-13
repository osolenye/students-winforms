using System.Linq;
using System.Windows.Forms;
using System;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        private TabControl tabControlMain;
        private TabPage tabGroups;
        private TabPage tabStudents;
        private TabPage tabRecords;

        // Группы
        private ListBox listBoxGroups;
        private TextBox textBoxGroupName;
        private Button buttonAddGroup;
        private Button buttonDeleteGroup;

        // Учащиеся
        private ComboBox comboBoxGroupSelect;
        private ListBox listBoxStudents;
        private TextBox textBoxStudentName;
        private Button buttonAddStudent;
        private Button buttonDeleteStudent;

        // Оценки и посещаемость
        private ComboBox comboBoxGroupForRecords;
        private ComboBox comboBoxStudentForRecords;
        private NumericUpDown numericGrade;
        private CheckBox checkBoxPresent;
        private Button buttonSaveRecord;
        private DataGridView dataGridRecords;

        private readonly DataManager dataManager = new DataManager();


        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";


            // Основные настройки формы
            this.Text = "Учёт оценок и посещаемости";
            this.Font = new System.Drawing.Font("Segoe UI", 10);
            this.Width = 800;
            this.Height = 600;

            // TabControl
            tabControlMain = new TabControl { Dock = DockStyle.Fill };
            tabGroups = new TabPage("Группы");
            tabStudents = new TabPage("Учащиеся");
            tabRecords = new TabPage("Оценки и посещаемость");

            tabControlMain.TabPages.AddRange(new[] { tabGroups, tabStudents, tabRecords });
            this.Controls.Add(tabControlMain);

            InitializeGroupsTab();
            InitializeStudentsTab();
            InitializeRecordsTab();
        }

        private void InitializeRecordsTab()
        {
            // Группа
            comboBoxGroupForRecords = new ComboBox
            {
                Left = 10,
                Top = 10,
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBoxGroupForRecords.SelectedIndexChanged += comboBoxGroupForRecords_SelectedIndexChanged;

            // Учащийся
            comboBoxStudentForRecords = new ComboBox
            {
                Left = comboBoxGroupForRecords.Right + 20,
                Top = 10,
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBoxStudentForRecords.SelectedIndexChanged += comboBoxStudentForRecords_SelectedIndexChanged;

            // Оценка
            numericGrade = new NumericUpDown
            {
                Left = 10,
                Top = comboBoxGroupForRecords.Bottom + 10,
                Width = 100,
                Minimum = 1,
                Maximum = 5
            };

            // Присутствие
            checkBoxPresent = new CheckBox
            {
                Left = numericGrade.Right + 20,
                Top = numericGrade.Top + 3,
                Text = "Присутствовал"
            };

            // Сохранить
            buttonSaveRecord = new Button
            {
                Left = checkBoxPresent.Right + 20,
                Top = numericGrade.Top - 2,
                Width = 150,
                Text = "Сохранить запись"
            };
            buttonSaveRecord.Click += buttonSaveRecord_Click;

            // Таблица с записями
            dataGridRecords = new DataGridView
            {
                Left = 10,
                Top = numericGrade.Bottom + 20,
                Width = 750,
                Height = 350,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            tabRecords.Controls.AddRange(new Control[]
            {
        comboBoxGroupForRecords,
        comboBoxStudentForRecords,
        numericGrade,
        checkBoxPresent,
        buttonSaveRecord,
        dataGridRecords
            });

            UpdateGroupComboBoxForRecords();
        }

        private void UpdateGroupComboBoxForRecords()
        {
            comboBoxGroupForRecords.Items.Clear();
            foreach (var group in dataManager.Groups)
                comboBoxGroupForRecords.Items.Add(group);

            if (comboBoxGroupForRecords.Items.Count > 0)
                comboBoxGroupForRecords.SelectedIndex = 0;
        }

        private void comboBoxGroupForRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStudentForRecords.Items.Clear();
            if (comboBoxGroupForRecords.SelectedItem is Group group)
            {
                foreach (var student in group.Students)
                    comboBoxStudentForRecords.Items.Add(student);
            }

            if (comboBoxStudentForRecords.Items.Count > 0)
                comboBoxStudentForRecords.SelectedIndex = 0;
        }

        private void comboBoxStudentForRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRecordsTable();
        }

        private void UpdateRecordsTable()
        {
            dataGridRecords.Rows.Clear();
            dataGridRecords.Columns.Clear();

            dataGridRecords.Columns.Add("date", "Дата");
            dataGridRecords.Columns.Add("grade", "Оценка");
            dataGridRecords.Columns.Add("present", "Присутствие");

            if (comboBoxStudentForRecords.SelectedItem is Student student)
            {
                foreach (var record in student.Records.OrderByDescending(r => r.Date))
                {
                    dataGridRecords.Rows.Add(record.Date.ToShortDateString(), record.Grade?.ToString() ?? "-", record.Present ? "Да" : "Нет");
                }
            }
        }

        private void buttonSaveRecord_Click(object sender, EventArgs e)
        {
            if (comboBoxStudentForRecords.SelectedItem is Student student)
            {
                var today = DateTime.Today;
                var existing = student.Records.FirstOrDefault(r => r.Date == today);
                if (existing != null)
                {
                    existing.Grade = (int)numericGrade.Value;
                    existing.Present = checkBoxPresent.Checked;
                }
                else
                {
                    student.Records.Add(new Record
                    {
                        Date = today,
                        Grade = (int)numericGrade.Value,
                        Present = checkBoxPresent.Checked
                    });
                }

                dataManager.SaveData();
                UpdateRecordsTable();
                MessageBox.Show("Запись сохранена!");
            }
        }

        private void InitializeStudentsTab()
        {
            // ComboBox для выбора группы
            comboBoxGroupSelect = new ComboBox
            {
                Left = 10,
                Top = 10,
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBoxGroupSelect.SelectedIndexChanged += comboBoxGroupSelect_SelectedIndexChanged;

            // ListBox студентов
            listBoxStudents = new ListBox
            {
                Left = 10,
                Top = comboBoxGroupSelect.Bottom + 10,
                Width = 300,
                Height = 400
            };

            // TextBox для имени студента
            textBoxStudentName = new TextBox
            {
                Left = listBoxStudents.Right + 20,
                Top = comboBoxGroupSelect.Bottom + 10,
                Width = 300,
                //PlaceholderText = "ФИО ученика"
            };

            // Кнопка добавить
            buttonAddStudent = new Button
            {
                Left = textBoxStudentName.Left,
                Top = textBoxStudentName.Bottom + 10,
                Width = 200,
                Text = "Добавить ученика"
            };
            buttonAddStudent.Click += buttonAddStudent_Click;

            // Кнопка удалить
            buttonDeleteStudent = new Button
            {
                Left = textBoxStudentName.Left,
                Top = buttonAddStudent.Bottom + 10,
                Width = 200,
                Text = "Удалить выбранного"
            };
            buttonDeleteStudent.Click += buttonDeleteStudent_Click;

            tabStudents.Controls.AddRange(new Control[]
            {
        comboBoxGroupSelect,
        listBoxStudents,
        textBoxStudentName,
        buttonAddStudent,
        buttonDeleteStudent
            });

            UpdateGroupDropdown();
        }
        private void UpdateGroupDropdown()
        {
            comboBoxGroupSelect.Items.Clear();
            foreach (var group in dataManager.Groups)
                comboBoxGroupSelect.Items.Add(group);

            if (comboBoxGroupSelect.Items.Count > 0)
                comboBoxGroupSelect.SelectedIndex = 0;
        }

        private void comboBoxGroupSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStudentList();
        }

        private void UpdateStudentList()
        {
            listBoxStudents.Items.Clear();
            if (comboBoxGroupSelect.SelectedItem is Group selectedGroup)
            {
                foreach (var student in selectedGroup.Students)
                    listBoxStudents.Items.Add(student);
            }
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            if (comboBoxGroupSelect.SelectedItem is Group selectedGroup)
            {
                string name = textBoxStudentName.Text.Trim();
                if (!string.IsNullOrEmpty(name) && !selectedGroup.Students.Any(s => s.FullName == name))
                {
                    selectedGroup.Students.Add(new Student { FullName = name });
                    dataManager.SaveData();
                    UpdateStudentList();
                    textBoxStudentName.Clear();
                }
                else
                {
                    MessageBox.Show("Введите уникальное имя ученика.");
                }
            }
        }

        private void buttonDeleteStudent_Click(object sender, EventArgs e)
        {
            if (comboBoxGroupSelect.SelectedItem is Group selectedGroup &&
                listBoxStudents.SelectedItem is Student selectedStudent)
            {
                var result = MessageBox.Show($"Удалить '{selectedStudent.FullName}'?", "Подтверждение", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    selectedGroup.Students.Remove(selectedStudent);
                    dataManager.SaveData();
                    UpdateStudentList();
                }
            }
        }

        private void UpdateGroupList()
        {
            listBoxGroups.Items.Clear();
            foreach (var group in dataManager.Groups)
                listBoxGroups.Items.Add(group);

            UpdateGroupDropdown();
        }


        private void InitializeGroupsTab()
        {
            // ListBox для групп
            listBoxGroups = new ListBox
            {
                Width = 300,
                Left = 10,
                Top = 10,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left
            };

            // TextBox для ввода названия группы
            textBoxGroupName = new TextBox
            {
                Left = listBoxGroups.Right + 20,
                Top = 10,
                Width = 400,
                //PlaceholderText = "Название новой группы"
            };

            // Кнопка "Добавить"
            buttonAddGroup = new Button
            {
                Left = textBoxGroupName.Left,
                Top = textBoxGroupName.Bottom + 10,
                Width = 200,
                Text = "Добавить группу"
            };
            buttonAddGroup.Click += buttonAddGroup_Click;

            // Кнопка "Удалить"
            buttonDeleteGroup = new Button
            {
                Left = textBoxGroupName.Left,
                Top = buttonAddGroup.Bottom + 10,
                Width = 200,
                Text = "Удалить выбранную"
            };
            buttonDeleteGroup.Click += buttonDeleteGroup_Click;

            // Добавляем элементы на вкладку
            tabGroups.Controls.AddRange(new Control[]
            {
                listBoxGroups, textBoxGroupName, buttonAddGroup, buttonDeleteGroup
            });
        }

        //private void UpdateGroupList()
        //{
        //    listBoxGroups.Items.Clear();
        //    foreach (var group in dataManager.Groups)
        //        listBoxGroups.Items.Add(group);
        //}

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            var groupName = textBoxGroupName.Text.Trim();
            if (!string.IsNullOrEmpty(groupName) && !dataManager.Groups.Any(g => g.Name == groupName))
            {
                dataManager.Groups.Add(new Group { Name = groupName });
                dataManager.SaveData();
                UpdateGroupList();
                textBoxGroupName.Clear();
            }
            else
            {
                MessageBox.Show("Введите уникальное название группы.");
            }
        }

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            if (listBoxGroups.SelectedItem is Group selectedGroup)
            {
                var result = MessageBox.Show($"Удалить группу '{selectedGroup.Name}'?", "Подтверждение", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    dataManager.Groups.Remove(selectedGroup);
                    dataManager.SaveData();
                    UpdateGroupList();
                }
            }
        }
        #endregion
    }
}

