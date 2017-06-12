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

namespace EmployeeListWin
{
    /// <summary>
    /// Логика взаимодействия для ListPosition.xaml
    /// </summary>
    public partial class ListPosition
    {
        EmployeeContext db;
        public ListPosition()
        {
            InitializeComponent();
            db = new EmployeeContext();
            db.Positions.Load();
            positionGrid.ItemsSource = db.Positions.Local.ToBindingList();
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
            if (positionGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < positionGrid.SelectedItems.Count; i++)
                {
                    Position position = positionGrid.SelectedItems[i] as Position;
                    if (position != null)
                    {
                        db.Positions.Remove(position);
                    }
                }
            }
            db.SaveChanges();
        }
        private void createMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
