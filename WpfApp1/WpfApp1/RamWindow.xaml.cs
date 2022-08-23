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
using WpfApp1.Models.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RamWindow.xaml
    /// </summary>
    public partial class RamWindow : Window
    {
        ShopEntities db = new ShopEntities();

        List<MemoryViewModel> ram;

        string mode = "add";

        public RamWindow()
        {
            InitializeComponent();

            add.IsChecked = true;

            updateItems();
            add.IsChecked = true;
            var ramstorage = new List<RamStorage>
            {
                RamStorage.RAM_4GB,
                RamStorage.RAM_8GB,
                RamStorage.RAM_16GB,
                RamStorage.RAM_32GB,
                RamStorage.RAM_64GB
            };

            storageComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(ramstorage);

            var ramtype = new List<RamType>
            {
                RamType.DDR4,
                RamType.DDR5
            };
            typeComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(ramtype);

            switch (Enum.Parse(typeof(Role), db.Employee.Find(User.idUser).role.ToString()))
            {
                case Role.SELLER:

                    container.IsEnabled = false;

                    break;

            }
        }

        private void updateItems()
        {

            ram = db.ram.Select(x =>
            new MemoryViewModel
            {
                id = x.id,
                name = x.name,
                price = x.price,
                type = x.type,
                typeName = EnumViewModel.EnumDisplayName((RamType)x.type),
                storage = x.storage,
                storageName = EnumViewModel.EnumDisplayName((RamStorage)x.storage),
                count = x.count
            }).ToList();

            listView.ItemsSource = ram;
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (sender as RadioButton);

            if (radio.Name == "add" && radio.IsChecked == true)
            {
                mode = radio.Name;

                nameTextBox.Text = "";
                priceTextBox.Text = "";
                countTextBox.Text = "";
                //комбобоксы

                saveButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
            }

            if (radio.Name == "edit" && radio.IsChecked == true)
            {
                mode = radio.Name;

                var selectedItem = (listView.SelectedItem as MemoryViewModel);


                if (selectedItem == null)
                {

                    var ram = this.ram.First();

                    listView.SelectedIndex = 0;

                    nameTextBox.Text = ram.name;
                    priceTextBox.Text = ram.price.ToString();
                    storageComboBox.SelectedValue = ram.storage;
                    typeComboBox.SelectedValue = ram.type;
                    countTextBox.Text = ram.count.ToString();
                }
                else
                {
                    nameTextBox.Text = selectedItem.name;
                    priceTextBox.Text = selectedItem.price.ToString();
                    storageComboBox.SelectedValue = selectedItem.storage;
                   typeComboBox.SelectedValue = selectedItem.type;
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
                ram item = new ram
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    price = Convert.ToDecimal(priceTextBox.Text),
                    storage=Convert.ToInt32(storageComboBox.SelectedValue),
                    type=Convert.ToInt32(typeComboBox.SelectedValue),
                    count=Convert.ToInt32(countTextBox.Text)
                };

                db.ram.Add(item);

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
                var id = (listView.SelectedItem as MemoryViewModel).id;

                ram item = db.ram.Find(id);

                item.name = nameTextBox.Text;
                item.price = Convert.ToDecimal(priceTextBox.Text);
                item.storage = Convert.ToInt32(storageComboBox.SelectedValue);
                item.type = Convert.ToInt32(typeComboBox.SelectedValue);
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

                nameTextBox.Text = (listView.SelectedItem as MemoryViewModel).name;
                priceTextBox.Text = (listView.SelectedItem as MemoryViewModel).price.ToString();
                storageComboBox.SelectedValue = (listView.SelectedItem as MemoryViewModel).storage;
                typeComboBox.SelectedValue = (listView.SelectedItem as MemoryViewModel).type;
                countTextBox.Text = (listView.SelectedItem as MemoryViewModel).count.ToString();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as ram).id;

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

                var item = db.ram.Find(id);

                db.ram.Remove(item);

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
