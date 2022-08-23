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
using WpfApp1.Models.Const;
using WpfApp1.Models.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для hard_drive.xaml
    /// </summary>
    public partial class HardDriveWindow : Window
    {
        ShopEntities db = new ShopEntities();

        List<MemoryViewModel> drives;

        string mode = "add";

        public HardDriveWindow()
        {
            InitializeComponent();

            add.IsChecked = true;

            var types = new List<Storage>
            {
                Storage.SSD64,
                Storage.SSD128,    
                Storage.SSD256,        
                Storage.SSD512,    
                Storage.SSD1TB,    
                Storage.SSD2TB,    
                Storage.HDD512GB,    
                Storage.HDD1TB,    
                Storage.HDD2TB    
            };

            storageComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(types);

            var typehards = new List<Typehard>
            {
                Typehard.HDD,
                Typehard.SSD
            };

            typeComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(typehards);

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

            drives = db.hard_drive.Select(x =>
            new MemoryViewModel
            {
                id = x.id,
                name = x.name,
                price = x.price,
                type = x.type,
                typeName = EnumViewModel.EnumDisplayName((Typehard)x.type),
                storage = x.storage,
                storageName = EnumViewModel.EnumDisplayName((Storage)x.storage),
                count = x.count
            }).ToList();

            listView.ItemsSource = drives;
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

                    var harddrive = drives.First();

                    listView.SelectedIndex = 0;

                    nameTextBox.Text = harddrive.name;
                    priceTextBox.Text = harddrive.price.ToString();
                    storageComboBox.SelectedValue = harddrive.storage;
                    typeComboBox.SelectedValue = harddrive.type;
                    countTextBox.Text = harddrive.count.ToString();
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
                hard_drive item = new hard_drive
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    price = Convert.ToDecimal(priceTextBox.Text),
                    storage = Convert.ToInt32(storageComboBox.SelectedValue),
                    type = Convert.ToInt32(typeComboBox.SelectedValue),
                    count=Convert.ToInt32(countTextBox.Text)
                };

                db.hard_drive.Add(item);

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

                hard_drive item = db.hard_drive.Find(id);

                item.name = nameTextBox.Text;
                item.price = Convert.ToDecimal(priceTextBox.Text);
                item.storage = Convert.ToInt32(storageComboBox.SelectedValue);
                item.type = Convert.ToInt32(typeComboBox.SelectedValue);
                item.count = Convert.ToInt32(countTextBox.Text);
                //комбобокс

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
                typeComboBox.SelectedValue = (listView.SelectedItem as MemoryViewModel).type;
                storageComboBox.SelectedValue = (listView.SelectedItem as MemoryViewModel).storage;
                countTextBox.Text = (listView.SelectedItem as MemoryViewModel).count.ToString();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as MemoryViewModel).id;

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

                var item = db.hard_drive.Find(id);

                db.hard_drive.Remove(item);

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
