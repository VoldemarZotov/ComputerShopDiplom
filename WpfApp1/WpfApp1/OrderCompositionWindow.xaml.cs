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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для OrderCompositionWindow.xaml
    /// </summary>
    public partial class OrderCompositionWindow : Window
    {
        ShopEntities db = new ShopEntities();

        public OrderCompositionWindow(string orderId)
        {
            InitializeComponent();
            using (ShopEntities db = new ShopEntities())
            {
                var order = db.order.Find(orderId);


                listView.ItemsSource = order.order_items
                    .ToList()
                    .Select(x => new OrderItemViewModel
                    {
                        Name = getItemName(x.id_item, x.type),
                        Count = x.count,
                        CostOne = $"{getItemCost(x.id_item, x.type)} руб.",
                        CostFull = $"{getItemCost(x.id_item, x.type) * x.count} руб.",
                        Type = getItemType(x.id_item, x.type)
                    });

                if (order.id_client != null)
                {
                    clientTextBox.Text = $"Клиент: {order.client.name}";
                } else
                {
                    clientTextBox.Text = $"Клиент: Нет (через кассу)";
                }

                var fullPrice = order.cost + " руб. ";
                var date = order.date.ToLocalTime().ToString("dd.MM.yyyy в HH:mm");

                descTextBox.Text = $"Стоимость: {fullPrice}. Дата заказа: {date}";
            }
        }

        private string getItemName(string id, int type)
        {
            var name = "";

            switch ((ItemType)type)
            {
                case ItemType.COMPUTER:
                    name = db.compukter.Find(id).name;
                    break;
                case ItemType.MOTHER:
                    name = db.mother.Find(id).name;
                    break;
                case ItemType.PROCESSOR:
                    name = db.processor.Find(id).name;
                    break;
                case ItemType.HARD:
                    name = db.hard_drive.Find(id).name;
                    break;
                case ItemType.CORPUS:
                    name = db.corpus.Find(id).name;
                    break;
                case ItemType.VIDEO:
                    name = db.videoadapter.Find(id).name;
                    break;
                case ItemType.RAM:
                    name = db.ram.Find(id).name;
                    break;
                case ItemType.POWER:
                    name = db.power.Find(id).name;
                    break;
            }

            return name;
        }

        private decimal getItemCost(string id, int type)
        {
            decimal cost = new decimal();

            switch ((ItemType)type)
            {
                case ItemType.COMPUTER:
                    cost = db.compukter.Find(id).price;
                    break;
                case ItemType.MOTHER:
                    cost = db.mother.Find(id).price;
                    break;
                case ItemType.PROCESSOR:
                    cost = db.processor.Find(id).price;
                    break;
                case ItemType.HARD:
                    cost = db.hard_drive.Find(id).price;
                    break;
                case ItemType.CORPUS:
                    cost = db.corpus.Find(id).price;
                    break;
                case ItemType.VIDEO:
                    cost = db.videoadapter.Find(id).price;
                    break;
                case ItemType.RAM:
                    cost = db.ram.Find(id).price;
                    break;
                case ItemType.POWER:
                    cost = db.power.Find(id).price;
                    break;
            }

            return cost;
        }

        private string getItemType(string id, int type)
        {
            var name = "";

            switch ((ItemType)type)
            {
                case ItemType.COMPUTER:
                    name = "Компьютер";
                    break;
                case ItemType.MOTHER:
                    name = "Мат. плата";
                    break;
                case ItemType.PROCESSOR:
                    name = "Процессор";
                    break;
                case ItemType.HARD:
                    name = "Диск";
                    break;
                case ItemType.CORPUS:
                    name = "Корпус";
                    break;
                case ItemType.VIDEO:
                    name = "Видеокарта";
                    break;
                case ItemType.RAM:
                    name = "Оперативная память";
                    break;
                case ItemType.POWER:
                    name = "Блок питания";
                    break;
            }

            return name;
        }
    }
}
