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
    /// Логика взаимодействия для MotherWindow.xaml
    /// </summary>
    public partial class MotherWindow : Window
    {

        ShopEntities db = new ShopEntities();

        List<mother> mother;

        string mode = "add";

        public MotherWindow()
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

            mother = db.mother.ToList();

            listView.ItemsSource = mother;
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (sender as RadioButton);

            if (radio.Name == "add" && radio.IsChecked == true)
            {
                mode = radio.Name;

                nameTextBox.Text = "";
                priceTextBox.Text = "";
                freezeTextBox.Text = "";
                countTextBox.Text = "";

                saveButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
            }

            if (radio.Name == "edit" && radio.IsChecked == true)
            {
                mode = radio.Name;

                var selectedItem = (listView.SelectedItem as mother);


                if (selectedItem == null)
                {

                    var item = this.mother.First();

                    listView.SelectedIndex = 0;

                    nameTextBox.Text = item.name;
                    priceTextBox.Text = item.price.ToString();
                    socketComboBox.SelectedValue = item.id_socket;
                    freezeTextBox.Text = item.freeze.ToString();
                    countTextBox.Text = item.count.ToString();
                }
                else
                {
                    nameTextBox.Text = selectedItem.name;
                    priceTextBox.Text = selectedItem.price.ToString();
                    socketComboBox.SelectedValue = selectedItem.id_socket;
                    freezeTextBox.Text = selectedItem.freeze.ToString();
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
                mother item = new mother
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    price = Convert.ToDecimal(priceTextBox.Text),
                    id_socket = socketComboBox.SelectedValue.ToString(),
                    freeze = (freezeTextBox.Text),
                    count = Convert.ToInt32(countTextBox.Text)
                };

                db.mother.Add(item);

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
                var id = (listView.SelectedItem as mother).id;

                mother item = db.mother.Find(id);

                item.name = nameTextBox.Text;
                item.price = Convert.ToDecimal(priceTextBox.Text);
                item.id_socket = socketComboBox.SelectedValue.ToString();
                item.freeze = freezeTextBox.Text;
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

                var selectedItem = (listView.SelectedItem as mother);

                nameTextBox.Text = selectedItem.name;
                priceTextBox.Text = selectedItem.price.ToString();
                socketComboBox.SelectedValue = selectedItem.id_socket;
                freezeTextBox.Text = selectedItem.freeze.ToString();
                countTextBox.Text = selectedItem.count.ToString();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as mother).id;

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

                var item = db.mother.Find(id);

                db.mother.Remove(item);

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

        private void countTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
