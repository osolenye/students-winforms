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
            this.Text = "Учёт оценок и посещаемости";
            this.Font = new System.Drawing.Font("Segoe UI", 10); // Современный шрифт по умолчанию
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240); // Светлый фон формы
            this.Width = 850; // Немного увеличим ширину
            this.Height = 650; // Немного увеличим высоту

            // TabControl
            tabControlMain = new TabControl { Dock = DockStyle.Fill };
            tabControlMain.Appearance = TabAppearance.FlatButtons; // Более современный вид вкладок
            tabControlMain.BackColor = System.Drawing.Color.White;
            tabControlMain.SelectedIndexChanged += tabControlMain_SelectedIndexChanged; // Для возможной стилизации активной вкладки

            tabGroups = new TabPage("Группы") { BackColor = System.Drawing.Color.White };
            tabStudents = new TabPage("Учащиеся") { BackColor = System.Drawing.Color.White };
            tabRecords = new TabPage("Оценки и посещаемость") { BackColor = System.Drawing.Color.White };

            tabControlMain.TabPages.AddRange(new[] { tabGroups, tabStudents, tabRecords });
            this.Controls.Add(tabControlMain);

            InitializeGroupsTab();
            InitializeStudentsTab();
            InitializeRecordsTab();
        }
        private void InitializeGroupsTab()
        {
            // ListBox для групп
            listBoxGroups = new ListBox
            {
                Width = 300,
                Left = 20,
                Top = 20,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                BorderStyle = BorderStyle.FixedSingle, // Улучшенная граница
                Font = new System.Drawing.Font("Segoe UI", 10)
            };

            // TextBox для ввода названия группы
            textBoxGroupName = new TextBox
            {
                Left = listBoxGroups.Right + 30,
                Top = 20,
                Width = 400,
                Font = new System.Drawing.Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
                //PlaceholderText = "Название новой группы"
            };

            // Кнопка "Добавить"
            buttonAddGroup = new Button
            {
                Left = textBoxGroupName.Left,
                Top = textBoxGroupName.Bottom + 15,
                Width = 150,
                Height = 35,
                Text = "Добавить группу",
                FlatStyle = FlatStyle.Flat, // Более плоский стиль
                BackColor = System.Drawing.Color.FromArgb(70, 130, 180), // Приятный синий цвет
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10),
                Cursor = Cursors.Hand
            };
            buttonAddGroup.FlatAppearance.BorderSize = 0;
            buttonAddGroup.Click += buttonAddGroup_Click;
            buttonAddGroup.MouseEnter += (s, e) => buttonAddGroup.BackColor = System.Drawing.Color.FromArgb(50, 100, 150);
            buttonAddGroup.MouseLeave += (s, e) => buttonAddGroup.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);

            // Кнопка "Удалить"
            buttonDeleteGroup = new Button
            {
                Left = textBoxGroupName.Left,
                Top = buttonAddGroup.Bottom + 10,
                Width = 150,
                Height = 35,
                Text = "Удалить",
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(220, 53, 69), // Красный цвет для удаления
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10),
                Cursor = Cursors.Hand
            };
            buttonDeleteGroup.FlatAppearance.BorderSize = 0;
            buttonDeleteGroup.Click += buttonDeleteGroup_Click;
            buttonDeleteGroup.MouseEnter += (s, e) => buttonDeleteGroup.BackColor = System.Drawing.Color.FromArgb(180, 40, 55);
            buttonDeleteGroup.MouseLeave += (s, e) => buttonDeleteGroup.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);

            // Добавляем элементы на вкладку
            tabGroups.Controls.AddRange(new Control[]
            {
        listBoxGroups, textBoxGroupName, buttonAddGroup, buttonDeleteGroup
            });
        }

        private void InitializeRecordsTab()
        {
            // Группа
            comboBoxGroupForRecords = new ComboBox
            {
                Left = 20,
                Top = 20,
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.White,
                //BorderStyle = BorderStyle.FixedSingle
            };
            comboBoxGroupForRecords.SelectedIndexChanged += comboBoxGroupForRecords_SelectedIndexChanged;

            // Учащийся
            comboBoxStudentForRecords = new ComboBox
            {
                Left = comboBoxGroupForRecords.Right + 30,
                Top = 20,
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.White,
                //BorderStyle = BorderStyle.FixedSingle
            };
            comboBoxStudentForRecords.SelectedIndexChanged += comboBoxStudentForRecords_SelectedIndexChanged;

            // Оценка
            numericGrade = new NumericUpDown
            {
                Left = 20,
                Top = comboBoxGroupForRecords.Bottom + 15,
                Width = 120,
                Minimum = 1,
                Maximum = 5,
                Font = new System.Drawing.Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Присутствие
            checkBoxPresent = new CheckBox
            {
                Left = numericGrade.Right + 30,
                Top = numericGrade.Top + 5,
                Text = "Присутствовал",
                Font = new System.Drawing.Font("Segoe UI", 10)
            };

            // Сохранить
            buttonSaveRecord = new Button
            {
                Left = checkBoxPresent.Right + 30,
                Top = numericGrade.Top - 3,
                Width = 150,
                Height = 35,
                Text = "Сохранить",
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(46, 204, 113), // Зеленый цвет для сохранения
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10),
                Cursor = Cursors.Hand
            };
            buttonSaveRecord.FlatAppearance.BorderSize = 0;
            buttonSaveRecord.Click += buttonSaveRecord_Click;
            buttonSaveRecord.MouseEnter += (s, e) => buttonSaveRecord.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            buttonSaveRecord.MouseLeave += (s, e) => buttonSaveRecord.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);

            // Таблица с записями
            dataGridRecords = new DataGridView
            {
                Left = 20,
                Top = numericGrade.Bottom + 25,
                Width = 800,
                Height = 400,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            dataGridRecords.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dataGridRecords.EnableHeadersVisualStyles = false;

            tabRecords.Controls.AddRange(new Control[]
            {
        comboBoxGroupForRecords, comboBoxStudentForRecords, numericGrade, checkBoxPresent, buttonSaveRecord, dataGridRecords
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

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Дополнительная стилизация активной вкладки при необходимости
            // Например, изменение цвета фона активной вкладки
            foreach (TabPage tabPage in tabControlMain.TabPages)
            {
                tabPage.BackColor = System.Drawing.Color.White;
            }
            tabControlMain.SelectedTab.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
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
                Left = 20,
                Top = 20,
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.White,
                //BorderStyle = BorderStyle.FixedSingle
            };
            comboBoxGroupSelect.SelectedIndexChanged += comboBoxGroupSelect_SelectedIndexChanged;

            // ListBox студентов
            listBoxStudents = new ListBox
            {
                Left = 20,
                Top = comboBoxGroupSelect.Bottom + 15,
                Width = 300,
                Height = 400,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };

            // TextBox для имени студента
            textBoxStudentName = new TextBox
            {
                Left = listBoxStudents.Right + 30,
                Top = comboBoxGroupSelect.Bottom + 15,
                Width = 400,
                Font = new System.Drawing.Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
                //PlaceholderText = "ФИО ученика"
            };

            // Кнопка добавить
            buttonAddStudent = new Button
            {
                Left = textBoxStudentName.Left,
                Top = textBoxStudentName.Bottom + 15,
                Width = 150,
                Height = 35,
                Text = "Добавить ученика",
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(70, 130, 180),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10),
                Cursor = Cursors.Hand
            };
            buttonAddStudent.FlatAppearance.BorderSize = 0;
            buttonAddStudent.Click += buttonAddStudent_Click;
            buttonAddStudent.MouseEnter += (s, e) => buttonAddStudent.BackColor = System.Drawing.Color.FromArgb(50, 100, 150);
            buttonAddStudent.MouseLeave += (s, e) => buttonAddStudent.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);

            // Кнопка удалить
            buttonDeleteStudent = new Button
            {
                Left = textBoxStudentName.Left,
                Top = buttonAddStudent.Bottom + 10,
                Width = 150,
                Height = 35,
                Text = "Удалить",
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(220, 53, 69),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10),
                Cursor = Cursors.Hand
            };
            buttonDeleteStudent.FlatAppearance.BorderSize = 0;
            buttonDeleteStudent.Click += buttonDeleteStudent_Click;
            buttonDeleteStudent.MouseEnter += (s, e) => buttonDeleteStudent.BackColor = System.Drawing.Color.FromArgb(180, 40, 55);
            buttonDeleteStudent.MouseLeave += (s, e) => buttonDeleteStudent.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);

            tabStudents.Controls.AddRange(new Control[]
            {
        comboBoxGroupSelect, listBoxStudents, textBoxStudentName, buttonAddStudent, buttonDeleteStudent
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

