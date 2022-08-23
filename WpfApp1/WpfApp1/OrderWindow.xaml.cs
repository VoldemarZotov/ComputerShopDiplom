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
using WpfApp1.Models.Order;
using WpfApp1.Order;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        ShopEntities db = new ShopEntities();

        public OrderWindow()
        {
            InitializeComponent();
            update();

            switch (Enum.Parse(typeof(Role), db.Employee.Find(User.idUser).role.ToString()))
            {
                case Role.MANAGER:

                    container.IsEnabled = false;

                    break;

            }
        }

        private void update(string searchText = "")
        {
            using (ShopEntities db = new ShopEntities())
            {
                listView.ItemsSource = db.order
                    .ToList()
                    .Select(x => new OrderViewModel
                    {
                        Id = x.id,
                        Date = x.date.ToLocalTime().ToString("dd.MM.yyyy в HH:mm"),
                        Cost = $"{x.cost} руб.",
                        ClientData = x.client == null ? "Нет (через кассу)" : $"{x.client.name} {x.client.phone}",
                        Employee = x.Employee.name,
                        Status = getStatusString((OrderStatus)Enum.Parse(typeof(OrderStatus), x.status.ToString()))
                    })
                    .Where(x => x.ClientData.Contains(searchText)
                                || x.Employee.Contains(searchText)
                                || x.Status.Contains(searchText)
                                || x.Date.Contains(searchText)
                                || x.Cost.Contains(searchText)
                    );
            }
        }

        private string getStatusString(OrderStatus status)
        {
            var statusString = "";

            switch (status)
            {
                case OrderStatus.ACCEPTED:
                    statusString = "Принят";
                    break;
                case OrderStatus.ASSEMBLY:
                    statusString = "Сборка";
                    break;
                case OrderStatus.WAITING:
                    statusString = "Ожидает выдачи";
                    break;
                case OrderStatus.ISSUED:
                    statusString = "Завершен";
                    break;
            }

            return statusString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var orderId = (String)(sender as Button).Tag;
            OrderCompositionWindow window = new OrderCompositionWindow(orderId);
            window.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateOrderWindow window = new CreateOrderWindow();
            window.ShowDialog();
            update();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {

                if (listView.SelectedItem != null)
                {
                    var itemId = (listView.SelectedItem as OrderViewModel).Id;
                    var item = db.order.Find(itemId);

                    switch (item.status)
                    {
                        case (int)OrderStatus.ACCEPTED:
                            item.status = (int)OrderStatus.ASSEMBLY;
                            break;
                        case (int)OrderStatus.ASSEMBLY:
                            item.status = (int)OrderStatus.WAITING;
                            break;
                        case (int)OrderStatus.WAITING:
                            item.status = (int)OrderStatus.ISSUED;
                            break;
                        case (int)OrderStatus.ISSUED: break;
                    }

                    db.SaveChanges();
                    update();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                var itemId = (listView.SelectedItem as OrderViewModel).Id;

                var item = db.order.Find(itemId);

                if (item.status != (int)OrderStatus.ISSUED)
                    changeStatusButton.IsEnabled = true;
                else
                    changeStatusButton.IsEnabled = false;
            }
            else
            {
                changeStatusButton.IsEnabled = false;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            update(searchTextBox.Text);
        }
    }
}
