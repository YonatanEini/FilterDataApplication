using FilterDataApplication.TableViewElementModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FilterDataApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(List<FilterElement> elements)
        {
            InitializeComponent();
            EmployeesList.ItemsSource = new List<FilterElement>(elements);
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
