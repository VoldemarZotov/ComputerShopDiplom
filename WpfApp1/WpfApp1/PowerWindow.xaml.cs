using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Models.Const;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Блоки_питания.xaml
    /// </summary>
    public partial class PowerWindow : Window
    {
        ShopEntities db = new ShopEntities();

        List<power> powers;

        string mode = "add";

        public PowerWindow()
        {
            InitializeComponent();

            add.IsChecked = true;

            updateItems();

            switch (Enum.Parse(typeof(Role), db.Employee.Find(User.idUser).role.ToString()))
            {
                case Role.SELLER:

                    container.IsEnabled = false;

                    break;

            }
        }

        private void updateItems()
        {

            powers = db.power.ToList();

            listView.ItemsSource = powers;
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (sender as RadioButton);

            if (radio.Name == "add" && radio.IsChecked == true)
            {
                mode = radio.Name;

                nameTextBox.Text = "";
                priceTextBox.Text = "";
                powerTextBox.Text = "";
                countTextBox.Text = "";

                saveButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
            }

            if (radio.Name == "edit" && radio.IsChecked == true)
            {
                mode = radio.Name;

                var selectedItem = (listView.SelectedItem as power);


                if (selectedItem == null)
                {

                    var power = powers.First();

                    listView.SelectedIndex = 0;

                    nameTextBox.Text = power.name;
                    priceTextBox.Text = power.price.ToString();
                    powerTextBox.Text = power.power1;
                    countTextBox.Text = power.count.ToString();
                }
                else
                {
                    nameTextBox.Text = selectedItem.name;
                    priceTextBox.Text = selectedItem.price.ToString();
                    powerTextBox.Text = selectedItem.power1;
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
                power item = new power
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    power1 = powerTextBox.Text,
                    price = Convert.ToDecimal(priceTextBox.Text),
                    count=Convert.ToInt32(countTextBox.Text)
                };

                db.power.Add(item);

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
                var id = (listView.SelectedItem as power).id;

                power item = db.power.Find(id);

                item.name = nameTextBox.Text;
                item.price = Convert.ToDecimal(priceTextBox.Text);
                item.power1 = powerTextBox.Text;
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

                nameTextBox.Text = (listView.SelectedItem as power).name;
                powerTextBox.Text = (listView.SelectedItem as power).power1;
                priceTextBox.Text = (listView.SelectedItem as power).price.ToString();
                countTextBox.Text = (listView.SelectedItem as power).count.ToString();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as power).id;

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

                var item = db.power.Find(id);

                db.power.Remove(item);

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