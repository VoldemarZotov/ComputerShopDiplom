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
using WpfApp1.Models.Const;
using WpfApp1.Models.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CorpusWindow.xaml
    /// </summary>
    public partial class CorpusWindow : Window
    {

        ShopEntities db = new ShopEntities();

        List<CorpusViewModel> corpus;

        string mode = "add";

        public CorpusWindow()
        {
            InitializeComponent();

            add.IsChecked = true;

            var types = new List<ColorType>
            {
                ColorType.BLACK,
                ColorType.GRAY,
                ColorType.WHITE
            };

            colorComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(types);
            colorComboBox.SelectedIndex = 0;

            var formfactor = new List<FormFactor>
            {
                FormFactor.ATX,
                FormFactor.MINI_ATX,
                FormFactor.MICRO_ATX,
            };

            formfactorComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(formfactor);
            formfactorComboBox.SelectedIndex = 0;

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

            corpus = db.corpus.Select(x =>
            new CorpusViewModel
            {
                id = x.id,
                name = x.name,
                price = x.price,
                color = x.color,
                colorName = EnumViewModel.EnumDisplayName((ColorType)x.color),
                formfactor = x.formfactor,
                formfactorName = EnumViewModel.EnumDisplayName((FormFactor)x.formfactor),
                count = x.count
            }).ToList();

            listView.ItemsSource = corpus;
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (sender as RadioButton);

            if (radio.Name == "add" && radio.IsChecked == true)
            {
                mode = radio.Name;

                nameTextBox.Text = "";
                priceTextBox.Text = "";
                CountTextBox.Text = "";
                //комбобоксы

                saveButton.Visibility = Visibility.Visible;
                saveEditButton.Visibility = Visibility.Collapsed;
            }

            if (radio.Name == "edit" && radio.IsChecked == true)
            {
                mode = radio.Name;

                var selectedItem = (listView.SelectedItem as CorpusViewModel);

                if (selectedItem == null)
                {

                    var corpus = this.corpus.First();

                    listView.SelectedIndex = 0;

                    nameTextBox.Text = corpus.name;
                    priceTextBox.Text = corpus.price.ToString();
                    colorComboBox.SelectedValue = corpus.color;
                    formfactorComboBox.SelectedValue = corpus.formfactor;
                    CountTextBox.Text = corpus.count.ToString();
                }
                else
                {
                    nameTextBox.Text = selectedItem.name;
                    priceTextBox.Text = selectedItem.price.ToString();
                    colorComboBox.SelectedValue = selectedItem.color;
                    formfactorComboBox.SelectedValue = selectedItem.formfactor;
                    CountTextBox.Text = selectedItem.count.ToString();
                }

                saveButton.Visibility = Visibility.Collapsed;
                saveEditButton.Visibility = Visibility.Visible;

            }
        }

        private void saveButton_Click_add(object sender, RoutedEventArgs e)
        {

            try
            {
                corpus item = new corpus
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    price = Convert.ToDecimal(priceTextBox.Text),
                    color = Convert.ToInt32(colorComboBox.SelectedValue),
                    formfactor = Convert.ToInt32(formfactorComboBox.SelectedValue),
                    count=Convert.ToInt32(CountTextBox.Text)
                };

                db.corpus.Add(item);

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
                var id = (listView.SelectedItem as CorpusViewModel).id;

                corpus item = db.corpus.Find(id);

                item.name = nameTextBox.Text;
                item.price = Convert.ToDecimal(priceTextBox.Text);
                item.color = Convert.ToInt32(colorComboBox.SelectedValue);
                item.formfactor = Convert.ToInt32(formfactorComboBox.SelectedValue);
                item.count = Convert.ToInt32(CountTextBox.Text);
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

                nameTextBox.Text = (listView.SelectedItem as CorpusViewModel).name;
                priceTextBox.Text = (listView.SelectedItem as CorpusViewModel).price.ToString();
                CountTextBox.Text = (listView.SelectedItem as CorpusViewModel).count.ToString();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as CorpusViewModel).id;

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

                var item = db.corpus.Find(id);

                db.corpus.Remove(item);

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
