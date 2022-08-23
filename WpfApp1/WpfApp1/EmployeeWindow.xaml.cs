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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {

        ShopEntities db = new ShopEntities();

        List<EmployeeViewModel> emps;

        string mode = "add";

        public EmployeeWindow()
        {
            InitializeComponent();

            add.IsChecked = true;

            var types = new List<Role>
            {
                Role.ADMIN,
                Role.SELLER,
                Role.MANAGER
            };

            roleComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(types);

            updateItems();
        }

        private void updateItems()
        {

            emps = db.Employee.Select(x =>
            new EmployeeViewModel
            {
                id = x.id,
                name = x.name,
                login = x.login,
                password = x.password,
                roleName = EnumViewModel.EnumDisplayName((Role)x.role),
                role = x.role
            }).ToList();

            listView.ItemsSource = emps;
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (sender as RadioButton);

            if (radio.Name == "add" && radio.IsChecked == true)
            {
                mode = radio.Name;

                nameTextBox.Text = "";
                loginTextBox.Text = "";
                passwordTextBox.Text = "";

                saveButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
            }

            if (radio.Name == "edit" && radio.IsChecked == true)
            {
                mode = radio.Name;

                var selectedemp = (listView.SelectedItem as EmployeeViewModel);


                if (selectedemp == null)
                {

                    var employee = emps.First();
  
                    listView.SelectedIndex = 0;

                    nameTextBox.Text = employee.name;
                    loginTextBox.Text = employee.login;
                    passwordTextBox.Text = employee.password;
                    roleComboBox.SelectedValue = employee.role;
                }
                else
                {
                    nameTextBox.Text = selectedemp.name;
                    loginTextBox.Text = selectedemp.login;
                    passwordTextBox.Text = selectedemp.password;
                    roleComboBox.SelectedValue = selectedemp.role;
                }

                saveButton.Visibility = Visibility.Collapsed;
                saveEditButton.Visibility = Visibility.Visible;

            }
        }

        private void saveButton_Click_add(object sender, RoutedEventArgs e)
        {

            try
            {
                Employee item = new Employee
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    login = loginTextBox.Text,
                    password = passwordTextBox.Text,
                    role = Convert.ToInt32(roleComboBox.SelectedValue)
                };

                db.Employee.Add(item);

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
                var id = (listView.SelectedItem as EmployeeViewModel).id;

                Employee item = db.Employee.Find(id);

                item.name =nameTextBox.Text;
                item.login =loginTextBox.Text;
                item.password =passwordTextBox.Text;
                item.role = Convert.ToInt32(roleComboBox.SelectedValue);

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

                nameTextBox.Text = (listView.SelectedItem as EmployeeViewModel).name;
                loginTextBox.Text = (listView.SelectedItem as EmployeeViewModel).login;
                passwordTextBox.Text = (listView.SelectedItem as EmployeeViewModel).password;
                roleComboBox.SelectedValue = (listView.SelectedItem as EmployeeViewModel).role;
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as EmployeeViewModel).id;

            if (String.IsNullOrEmpty(id))
            {
                MessageBox.Show("net");

                return;
            }

            try
            {
                var item = db.Employee.Find(id);

                db.Employee.Remove(item);

                db.SaveChanges();

                updateItems();
            }
            catch (Exception)
            {

            }

        }
    }
}
