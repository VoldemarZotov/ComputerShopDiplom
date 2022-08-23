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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ProcessorWindow.xaml
    /// </summary>
    public partial class ProcessorWindow : Window
    {
        ShopEntities db = new ShopEntities();

        List<processor> processor;

        string mode = "add";

        public ProcessorWindow()
        {
            InitializeComponent();

            add.IsChecked = true;

            updateItems();
            add.IsChecked = true;

            socketComboBox.ItemsSource = db.socket.ToList();

            switch (Enum.Parse(typeof(Role), db.Employee.Find(User.idUser).role.ToString()))
            {
                case Role.SELLER:

                    container.IsEnabled = false;

                    break;

            }
        }

        private void updateItems()
        {

            processor = db.processor.ToList();

            listView.ItemsSource = processor;
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (sender as RadioButton);

            if (radio.Name == "add" && radio.IsChecked == true)
            {
                mode = radio.Name;

                nameTextBox.Text = "";
                priceTextBox.Text = "";
                frequencyTextBox.Text = "";
                cache1TextBox.Text = "";
                cache2TextBox.Text = "";
                cache3TextBox.Text = "";
                countTextBox.Text = "";

                saveButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
            }

            if (radio.Name == "edit" && radio.IsChecked == true)
            {
                mode = radio.Name;

                var selectedItem = (listView.SelectedItem as processor);


                if (selectedItem == null)
                {

                    var item = this.processor.First();

                    listView.SelectedIndex = 0;

                    nameTextBox.Text = item.name;
                    priceTextBox.Text = item.price.ToString();
                    socketComboBox.SelectedValue = item.id_socket;
                    frequencyTextBox.Text = item.frequency.ToString();
                    cache1TextBox.Text = item.cache_1;
                    cache2TextBox.Text = item.cache_2;
                    cache3TextBox.Text = item.cache_3;
                    countTextBox.Text = item.count.ToString();
                }
                else
                {
                    nameTextBox.Text = selectedItem.name;
                    priceTextBox.Text = selectedItem.price.ToString();
                    socketComboBox.SelectedValue = selectedItem.id_socket;
                    frequencyTextBox.Text = selectedItem.frequency.ToString();
                    cache1TextBox.Text = selectedItem.cache_1;
                    cache2TextBox.Text = selectedItem.cache_2;
                    cache3TextBox.Text = selectedItem.cache_3;
                    countTextBox.Text = selectedItem.count.ToString();
                }

                saveButton.Visibility = Visibility.Collapsed;
                saveEditButton.Visibility = Visibility.Visible;

            }
        }

        private void saveButton_Click_add(object sender, RoutedEventArgs e)
        {

            try
            {
                processor item = new processor
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    price = Convert.ToDecimal(priceTextBox.Text),
                    id_socket = socketComboBox.SelectedValue.ToString(),
                    frequency=Convert.ToDecimal(frequencyTextBox.Text),
                    cache_1=cache1TextBox.Text,
                    cache_2=cache2TextBox.Text,
                    cache_3=cache3TextBox.Text,
                    count=Convert.ToInt32(countTextBox.Text)
                };

                db.processor.Add(item);

                db.SaveChanges();

                updateItems();
            }
            catch (Exception)
            {

            }

        }


        private void saveButton_Click_edit(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = (listView.SelectedItem as processor).id;

                processor item = db.processor.Find(id);

                item.name = nameTextBox.Text;
                item.price = Convert.ToDecimal(priceTextBox.Text);
                item.id_socket = socketComboBox.SelectedValue.ToString();
                item.frequency = Convert.ToDecimal(frequencyTextBox.Text);
                item.cache_1 = cache1TextBox.Text;
                item.cache_2 = cache2TextBox.Text;
                item.cache_3 = cache3TextBox.Text;
                item.count = Convert.ToInt32(countTextBox.Text);
                db.SaveChanges();

                updateItems();
            }
            catch (Exception)
            {

            }

        }

        private void listView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (mode == "edit")
            {

                var selectedItem = (listView.SelectedItem as processor);

                nameTextBox.Text = selectedItem.name;
                priceTextBox.Text = selectedItem.price.ToString();
                socketComboBox.SelectedValue = selectedItem.id_socket;
                frequencyTextBox.Text = selectedItem.frequency.ToString();
                cache1TextBox.Text = selectedItem.cache_1;
                cache2TextBox.Text = selectedItem.cache_2;
                cache3TextBox.Text = selectedItem.cache_3;
                countTextBox.Text = selectedItem.count.ToString();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as processor).id;

            if (String.IsNullOrEmpty(id))
            {
                MessageBox.Show("net");

                return;
            }

            try
            {
                var list = db.order_items.Where(x => x.id_item == id).ToList();

                if (list.Count != 0)
                {
                    MessageBox.Show("Данный товар был заказан, удалить невозможно");
                }

                var item = db.processor.Find(id);

                db.processor.Remove(item);

                db.SaveChanges();

                updateItems();
            }
            catch { }
        }

        private void priceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fullText, out val);
        }
    }
}

