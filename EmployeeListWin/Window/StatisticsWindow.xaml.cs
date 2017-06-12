using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeListWin.Window
{
    /// <summary>
    /// Логика взаимодействия для StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow
    {
        EmployeeContext db;
        public StatisticsWindow()
        {
            InitializeComponent();
            db = new EmployeeContext();
            db.Employees.Load();
            db.Positions.Load();
            //получим количество сотрудников и запишем
            int count = db.Employees.Local.Count;
            countEmployee.Text = count.ToString();
            //получим вакансии и запишем
            var queryPos = db.Database.SqlQuery<Position>("SELECT * FROM Positions p WHERE p.id not in (select positionId from Employees)").ToList();
            //строка, которую будем выводить
            String vacancy = "";
            foreach (var position in queryPos)
                vacancy += position.NamePost + ", ";
            //удаляем лишнюю запятую
            if ( vacancy != "" )
                vacancy = vacancy.Substring(0, vacancy.Length - 2);
            currentPosition.Text = vacancy;
            //получим среднюю зп 
            var querySalary = db.Database.SqlQuery<Position>("SELECT * from Employees e join Positions p on p.Id = e.PositionId");
            decimal AVGsalary = 0;
            foreach (var position in querySalary)
                AVGsalary += position.Salary;
            if (AVGsalary != 0)
            {
                AVGsalary = AVGsalary / count;
                averageSalary.Text = Math.Round(AVGsalary, 2).ToString();
            }
        }
        private void createListPosition_Click(object sender, RoutedEventArgs e)
        {

            ListPosition listPosition = new ListPosition();
            listPosition.Show();
            this.Close();
        }
        private void createMainWindow_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
