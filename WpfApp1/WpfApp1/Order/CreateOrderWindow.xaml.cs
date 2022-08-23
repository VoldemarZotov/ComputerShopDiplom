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
using WpfApp1.Models;
using WpfApp1.Models.Const;
using WpfApp1.Models.Order;

namespace WpfApp1.Order
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        ShoppingCart cart = new ShoppingCart();

        ShopEntities db = new ShopEntities();
        List<ItemsViewModel> items = new List<ItemsViewModel>();

        public CreateOrderWindow()
        {
            InitializeComponent();
            CollectList();

            listView.ItemsSource = items;
        }

        private void CollectList()
        {
            items.Clear();
            items.AddRange(db.compukter.Where(x=>x.count > 0).Select(x => new ItemsViewModel()
            {
                Id = x.id,
                Name = x.name,
                Cost = x.price,
                Type = ItemType.COMPUTER,
                MaxCount = x.count
            }));

            items.AddRange(db.processor.Where(x => x.count > 0).Select(x => new ItemsViewModel()
            {
                Id = x.id,
                Name = x.name,
                Cost = x.price,
                Type = ItemType.PROCESSOR,
                MaxCount = x.count
            }));

            items.AddRange(db.mother.Where(x => x.count > 0).Select(x => new ItemsViewModel()
            {
                Id = x.id,
                Name = x.name,
                Cost = x.price,
                Type = ItemType.MOTHER,
                MaxCount = x.count
            }));

            items.AddRange(db.hard_drive.Where(x => x.count > 0).Select(x => new ItemsViewModel()
            {
                Id = x.id,
                Name = x.name,
                Cost = x.price,
                Type = ItemType.HARD,
                MaxCount = x.count
            }));

            items.AddRange(db.ram.Where(x => x.count > 0).Select(x => new ItemsViewModel()
            {
                Id = x.id,
                Name = x.name,
                Cost = x.price,
                Type = ItemType.RAM,
                MaxCount = x.count
            }));

            items.AddRange(db.corpus.Where(x => x.count > 0).Select(x => new ItemsViewModel()
            {
                Id = x.id,
                Name = x.name,
                Cost = x.price,
                Type = ItemType.CORPUS,
                MaxCount = x.count
            }));

            items.AddRange(db.power.Where(x => x.count > 0).Select(x => new ItemsViewModel()
            {
                Id = x.id,
                Name = x.name,
                Cost = x.price,
                Type = ItemType.POWER,
                MaxCount = x.count
            }));

            items.AddRange(db.videoadapter.Where(x => x.count > 0).Select(x => new ItemsViewModel()
            {
                Id = x.id,
                Name = x.name,
                Cost = x.price,
                Type = ItemType.VIDEO,
                MaxCount = x.count
            }));

            items = items.OrderBy(x => x.Name).ToList();
        }

        //удаление из корзины
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            cart.RemoveItem(listView1.SelectedItem as ShoppingCartItem);
            UpdateCart();
        }

        //добавить в корзину
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string idSelectedItem = (sender as Button).Tag.ToString();

            cart.AddItem(items.Find(x => x.Id == idSelectedItem).MapToCartItem());
            UpdateCart();
        }

        //создать заказ
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var idOrder = Guid.NewGuid().ToString();

            try
            {
                order order = new order
                {
                    id = idOrder,
                    status = (int)OrderStatus.ISSUED,
                    cost = cart.FinalCost,
                    id_client = null,
                    date = DateTime.Now.ToUniversalTime(),
                    id_employee = User.idUser
                };

                db.order.Add(order);

                foreach (var item in cart.OrderItems)
                {
                    var orderItems = new order_items
                    {
                        id_order = idOrder,
                        id_item = item.Id,
                        count = item.Count,
                        type = (int)item.Type
                    };

                    db.order_items.Add(orderItems);
                }

                updateItemsCounts();

                db.SaveChanges();

                MessageBox.Show("Заказ успешно создан");

                Close();
            }
            catch 
            {
                try
                {
                    db.order_items.RemoveRange(db.order_items.Where(x => x.id_order == idOrder));
                    db.SaveChanges();
                }
                catch { }

                try
                {
                    db.order.Remove(db.order.Find(idOrder));
                    db.SaveChanges();
                }
                catch { }

                MessageBox.Show("Произошла ошибка при создании заказа, попробуйте еще раз");
            }
        }

        private void updateItemsCounts()
        {
            foreach (var item in cart.OrderItems)
            {

                switch (item.Type)
                {
                    case ItemType.COMPUTER:
                        db.compukter.Find(item.Id).count -= item.Count;
                        break;
                    case ItemType.MOTHER:
                        db.mother.Find(item.Id).count -= item.Count;
                        break;
                    case ItemType.PROCESSOR:
                        db.processor.Find(item.Id).count -= item.Count;
                        break;
                    case ItemType.HARD:
                        db.hard_drive.Find(item.Id).count -= item.Count;
                        break;
                    case ItemType.CORPUS:
                        db.corpus.Find(item.Id).count -= item.Count;
                        break;
                    case ItemType.VIDEO:
                        db.videoadapter.Find(item.Id).count -= item.Count;
                        break;
                    case ItemType.RAM:
                        db.ram.Find(item.Id).count -= item.Count;
                        break;
                    case ItemType.POWER:
                        db.power.Find(item.Id).count -= item.Count;
                        break;
                }
            }
            db.SaveChanges();
        }

        private void UpdateCart()
        {
            listView1.ItemsSource = null;
            listView1.ItemsSource = cart.OrderItems;
            priceTextBlock.Text = $"Сумма: {cart.FinalCost} руб.";
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            listView.ItemsSource = items.Where(x=>x.Name.ToUpper().Contains(searchTextBox.Text.ToUpper()));
        }
    }
}
