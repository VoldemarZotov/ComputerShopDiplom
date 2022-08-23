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
using WpfApp1.Models.Const;
using WpfApp1.Order;

namespace WpfApp1.EmployeeActions
{
    /// <summary>
    /// Логика взаимодействия для SellerActions.xaml
    /// </summary>
    public partial class SellerActions : Window
    {
        public SellerActions()
        {
            InitializeComponent();
            empTextBlock.Text = "Сотрудник: " + User.nameUser;
        }

        private void ordersButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow window = new OrderWindow();
            window.ShowDialog();
        }

        private void compucterButton_Click(object sender, RoutedEventArgs e)
        {
            ComputerWindow window = new ComputerWindow();
            window.ShowDialog();
        }

        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            PowerWindow window = new PowerWindow();
            window.ShowDialog();

        }

        private void corpusButton_Click(object sender, RoutedEventArgs e)
        {
            CorpusWindow window = new CorpusWindow();
            window.ShowDialog();
        }

        private void ramButton_Click(object sender, RoutedEventArgs e)
        {
            RamWindow window = new RamWindow();
            window.ShowDialog();
        }

        private void hardDriveButton_Click(object sender, RoutedEventArgs e)
        {
            HardDriveWindow window = new HardDriveWindow();
            window.ShowDialog();
        }

        private void vidioadapterButton_Click(object sender, RoutedEventArgs e)
        {
            VideoAdapterWindow window = new VideoAdapterWindow();
            window.ShowDialog();
        }

        private void processorButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessorWindow window = new ProcessorWindow();
            window.ShowDialog();
        }

        private void motherButton_Click(object sender, RoutedEventArgs e)
        {
            MotherWindow window = new MotherWindow();
            window.ShowDialog();
        }
    }
}
