using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1: Form
    {
        public Form1()
        {
            //InitializeComponent();
            InitializeComponent();
            dataManager.LoadData();
            UpdateGroupList();
            UpdateGroupComboBoxForRecords(); // Добавленная строка: обновляем ComboBox на вкладке "Оценки"
        }
    }
}
