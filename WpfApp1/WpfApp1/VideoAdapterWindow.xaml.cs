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
    /// Логика взаимодействия для VideoAdapterWindow.xaml
    /// </summary>
    public partial class VideoAdapterWindow : Window
    {
        ShopEntities db = new ShopEntities();

        List<VideoAdapterViewModel> videos;

        string mode = "add";

        public VideoAdapterWindow()
        {
            InitializeComponent();

            add.IsChecked = true;

            updateItems();
            add.IsChecked = true;
            var videomemoris = new List<VideoMemory>
            {
                VideoMemory.MEMORY2GB,
                VideoMemory.MEMORY4GB,
                VideoMemory.MEMORY6GB,
                VideoMemory.MEMORY8GB,
                VideoMemory.MEMORY12GB,
                VideoMemory.MEMORY24GB
            };

            storageComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(videomemoris);

            var videomemoristypes = new List<VideoMemoryType>
            {
                VideoMemoryType.TYPEDDR4,
                VideoMemoryType.TYPEDDR5
            };
            typeComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(videomemoristypes);

            switch (Enum.Parse(typeof(Role), db.Employee.Find(User.idUser).role.ToString()))
            {
                case Role.SELLER:

                    container.IsEnabled = false;

                    break;

            }
        }

        private void updateItems()
        {

            videos = db.videoadapter.Select(x =>
            new VideoAdapterViewModel
            {
                id = x.id,
                name = x.name,
                price = x.price,
                type = x.type,
                typeName = EnumViewModel.EnumDisplayName((VideoMemoryType)x.type),
                videoMemory = x.video_memory,
                videoMemoryName = EnumViewModel.EnumDisplayName((VideoMemory)x.video_memory),
                count = x.count
            }).ToList();

            listView.ItemsSource = videos;
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (sender as RadioButton);

            if (radio.Name == "add" && radio.IsChecked == true)
            {
                mode = radio.Name;

                nameTextBox.Text = "";
                priceTextBox.Text = "";

                saveButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
                countTextBox.Text = "";
            }

            if (radio.Name == "edit" && radio.IsChecked == true)
            {
                mode = radio.Name;

                var selectedItem = (listView.SelectedItem as VideoAdapterViewModel);


                if (selectedItem == null)
                {

                    var videoadapter = this.videos.First();

                    listView.SelectedIndex = 0;

                    nameTextBox.Text = videoadapter.name;
                    priceTextBox.Text = videoadapter.price.ToString();
                    storageComboBox.SelectedValue = videoadapter.videoMemory;
                    typeComboBox.SelectedValue = videoadapter.type;
                    countTextBox.Text = videoadapter.count.ToString();
                }
                else
                {
                    nameTextBox.Text = selectedItem.name;
                    priceTextBox.Text = selectedItem.price.ToString();
                    storageComboBox.SelectedValue = selectedItem.videoMemory;
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
                videoadapter item = new videoadapter
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    price = Convert.ToDecimal(priceTextBox.Text),
                    video_memory = Convert.ToInt32(storageComboBox.SelectedValue),
                    type = Convert.ToInt32(typeComboBox.SelectedValue),
                    count=Convert.ToInt32(countTextBox.Text)
                };

                db.videoadapter.Add(item);

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
                var id = (listView.SelectedItem as VideoAdapterViewModel).id;

                videoadapter item = db.videoadapter.Find(id);

                item.name = nameTextBox.Text;
                item.price = Convert.ToDecimal(priceTextBox.Text);
                item.video_memory = Convert.ToInt32(storageComboBox.SelectedValue);
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

                nameTextBox.Text = (listView.SelectedItem as VideoAdapterViewModel).name;
                priceTextBox.Text = (listView.SelectedItem as VideoAdapterViewModel).price.ToString();
                storageComboBox.SelectedValue = (listView.SelectedItem as VideoAdapterViewModel).videoMemory; 
                typeComboBox.SelectedValue = (listView.SelectedItem as VideoAdapterViewModel).type;
                countTextBox.Text = (listView.SelectedItem as VideoAdapterViewModel).count.ToString();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as VideoAdapterViewModel).id;

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

                var item = db.videoadapter.Find(id);

                db.videoadapter.Remove(item);

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
