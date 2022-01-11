using FilterDataApplication.DataBaseDataFilter;
using FilterDataApplication.HandleFilterProperties;
using FilterDataApplication.TableViewElementModel;
using FilterDataApplication.TcpDataListener;
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
using System.Windows.Shapes;

namespace FilterDataApplication
{
    /// <summary>
    /// Interaction logic for ChooseFilterScreen.xaml
    /// </summary>
    public partial class ChooseFilterScreen : Window
    {
        public ChooseFilterScreen()
        {
            InitializeComponent();
            TcpProtocolListener tcpDataListener = new TcpProtocolListener(10000);
            //starting tcp listener on port 10000
            Task.Factory.StartNew(() => tcpDataListener.ActivateServer());
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            string content = (sender as CheckBox).Content.ToString();
            FilterPropertiesValidator checkBoxData = FilterPropertiesValidator.GetInstance();
            _ = checkBoxData.UserIcdSelctedProperties.Remove(content);
            Console.WriteLine($"Check Box: {content} Is UnChecked");

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string content = (sender as CheckBox).Content.ToString();
            FilterPropertiesValidator checkBoxData = FilterPropertiesValidator.GetInstance();
            checkBoxData.UserIcdSelctedProperties.Add(content);
            Console.WriteLine($"Check Box: {content} Is Checked");
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        public void ActivateFilter()
        {
            FilterPropertiesValidator dataValidator = FilterPropertiesValidator.GetInstance();
            if (dataValidator.CheckFilterProprties(this.startingDate.SelectedDate, this.endingDate.SelectedDate,
                                            this.startingHour.SelectedTime, this.endigHour.SelectedTime))
            {
                MongoDbConnection dbConnection = MongoDbConnection.GetInstance("FillterAppDataBase", "Collection");
                DataBaseFilter dbFilter = new DataBaseFilter(dbConnection.ClientMongoCollection, dataValidator.StartingDateTime,
                                                                dataValidator.EndingDateTime, dataValidator.UserIcdSelctedProperties);
                List<FilterElement> filterElements = dbFilter.FilterData();
                if (filterElements.Count() > 0)
                {
                    // there are some filter results
                    MainWindow mw = new MainWindow(filterElements);
                    mw.Show();
                }
                if (filterElements.Count() == 0 && dbFilter.SelectedIcdProperties.Count() > 0)
                {
                    // no results, all filter properties were filled by the user
                    AlertWindow alertWindow = new AlertWindow("No Results Are Found!");
                    alertWindow.Show();
                }
            }
        }
        private void SubmitProperties_Click(object sender, RoutedEventArgs e)
        {
            ActivateFilter();
        }
    }
}
