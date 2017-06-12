using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using EmployeeListWin.Window;

namespace EmployeeListWin
{
    
    public partial class MainWindow
    {

        EmployeeContext db;

        public MainWindow()
        {
            InitializeComponent();
            
           
            db = new EmployeeContext();
            db.Employees.Load();
            db.Positions.Load();
            employeeGrid.ItemsSource = db.Employees.Local.ToBindingList();
            positionColumn.ItemsSource = db.Positions.Local.ToBindingList();
            searchPosition.ItemsSource = db.Positions.Local.ToBindingList();
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeeGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < employeeGrid.SelectedItems.Count; i++)
                {
                    Employee employee = employeeGrid.SelectedItems[i] as Employee;
                    if (employee != null)
                    {
                        db.Employees.Remove(employee);
                    }
                }
            }
            db.SaveChanges();
        }

        private void createListPosition_Click(object sender, RoutedEventArgs e)
        {
            
            ListPosition listPosition = new ListPosition();
            listPosition.Show();
            this.Close();
        }
        private void createStatisticsWindow_Click(object sender, RoutedEventArgs e)
        {

            StatisticsWindow statisticWindow = new StatisticsWindow();
            statisticWindow.Show();
            this.Close();
        }
        private void searchEmployeeClick_Click(object sender, RoutedEventArgs e)
        {
            if (searchFIO.Text != "" & searchPosition.Text != "")
                employeeGrid.ItemsSource = db.Employees.Local.Where(c => c.FIO == searchFIO.Text).ToList().Where(c => c.Position.NamePost == searchPosition.Text);
            else if (searchFIO.Text != "" & searchPosition.Text == "")
                employeeGrid.ItemsSource = db.Employees.Local.Where(c => c.FIO == searchFIO.Text).ToList();
            else if (searchFIO.Text == "" & searchPosition.Text != "")
                employeeGrid.ItemsSource = db.Employees.Local.Where(c => c.Position.NamePost == searchPosition.Text).ToList();
            positionColumn.ItemsSource = db.Positions.Local.ToBindingList();
        }
        private void refreshEmployee_Click(object sender, RoutedEventArgs e)
        {
            employeeGrid.ItemsSource = db.Employees.Local.ToBindingList();
            positionColumn.ItemsSource = db.Positions.Local.ToBindingList();
        }
        
    }
}
