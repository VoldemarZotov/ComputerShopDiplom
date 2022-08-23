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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.EmployeeActions;
using WpfApp1.Models.Const;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ShopEntities db = new ShopEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(loginTextBox.Text) ||
                 String.IsNullOrEmpty(passwordTextBox.Password))
            {
                MessageBox.Show("Ошибка, одно или оба поле пустые");
                return;
            }

            Employee emp = db.Employee.Where(x => x.login == loginTextBox.Text && x.password == passwordTextBox.Password).SingleOrDefault();

            if (emp == null)
            {
                MessageBox.Show("Логин или пароль неверны");
                return;
            }

            User.idUser = emp.id;
            User.nameUser = emp.name;

            switch (Enum.Parse(typeof(Role), emp.role.ToString())) 
            {
                case Role.ADMIN:

                    User.Role = Role.ADMIN; 
                    AdminActions window = new AdminActions();
                    window.ShowDialog();
                    break;

                case Role.MANAGER:

                    User.Role = Role.MANAGER;
                    ManagerActions window1 = new ManagerActions();
                    window1.ShowDialog();
                    break;

                case Role.SELLER:

                    User.Role = Role.SELLER;
                    SellerActions window2 = new SellerActions();
                    window2.ShowDialog();
                    break;

            }
        }
    }
}