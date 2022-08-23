using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для SocketWindow.xaml
    /// </summary>
    public partial class SocketWindow : Window
    {

        ShopEntities db = new ShopEntities();

        List<socket> sockets;

        string mode = "add";

        public SocketWindow()
        {
            InitializeComponent();

            add.IsChecked = true;

            updateItems();
        }

        private void updateItems() {

            sockets = db.socket.ToList();

            listView.ItemsSource = sockets;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (sender as RadioButton);

            if (radio.Name == "add" && radio.IsChecked == true)
            {
                mode = radio.Name;

                socketNameTextBox.Text = "";

                saveButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
            }

            if (radio.Name == "edit" && radio.IsChecked == true)
            {
                mode = radio.Name;

                var socket = (listView.SelectedItem as socket);


                if (socket == null)
                {

                    var firstItem = sockets.First();

                    listView.SelectedIndex = 0;

                    socketNameTextBox.Text = firstItem.name;
                }
                else
                {
                    socketNameTextBox.Text = socket.name;
                }

                saveButton.Visibility = Visibility.Collapsed;
                saveEditButton.Visibility = Visibility.Visible;

            }
        }

        private void saveButton_Click_add(object sender, RoutedEventArgs e)
        {

            try
            {
                socket socket = new socket
                {
                    id = Guid.NewGuid().ToString(),
                    name = socketNameTextBox.Text
                };

                db.socket.Add(socket);

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
                var id = (listView.SelectedItem as socket).id;

                socket socket = db.socket.Find(id);

                socket.name = socketNameTextBox.Text;

                db.SaveChanges();

                updateItems();
            }
            catch (Exception)
            {

            }

        }

        private void listView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (mode == "edit") {

                socketNameTextBox.Text = (listView.SelectedItem as socket).name;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as socket).id;

            if (String.IsNullOrEmpty(id)) {
                MessageBox.Show("net");

                return;
            }

            try
            {
                var item = db.socket.Find(id);

                db.socket.Remove(item);

                db.SaveChanges();

                updateItems();
            }
            catch (Exception)
            {

            }

        }


    }
}
