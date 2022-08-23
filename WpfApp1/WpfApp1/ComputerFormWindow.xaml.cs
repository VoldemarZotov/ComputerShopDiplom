using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ComputerFormWindow.xaml
    /// </summary>
    public partial class ComputerFormWindow : Window
    {
        ShopEntities db = new ShopEntities();

        byte[] file = null;

        string id = "";

        compukter item = null;

        OpenFileDialog openFileDialog = new OpenFileDialog();

        public ComputerFormWindow()
        {
            InitializeComponent();

            saveButton.Click += saveButton_Click;
        }

        public ComputerFormWindow(string id)
        {
            InitializeComponent();

            this.id = id;

            item = db.compukter.Find(id);

            saveButton.Click += saveButton_Click_edit;
        }

        void fillComboBoxes()
        {

            processorComboBox.ItemsSource = db.processor.Where(x => x.count >= 0).ToList();
            ramComboBox.ItemsSource = db.ram.Where(x => x.count >= 0).ToList();
            powerComboBox.ItemsSource = db.power.Where(x => x.count >= 0).ToList();
            employeeComboBox.ItemsSource = db.Employee.ToList();
            corpusComboBox.ItemsSource = db.corpus.Where(x => x.count >= 0).ToList();
            HarddriveComboBox.ItemsSource = db.hard_drive.Where(x => x.count >= 0).ToList();
            videoadapterComboBox.ItemsSource = db.videoadapter.Where(x => x.count >= 0).ToList();
            //motherComboBox.ItemsSource = db.mother.ToList();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                compukter computer = new compukter
                {
                    id = Guid.NewGuid().ToString(),
                    name = nameTextBox.Text,
                    price = Convert.ToDecimal(priceTextBox.Text),
                    desc = descTextBox.Text,
                    instock = instockCheckBox.IsChecked,
                    id_processor = processorComboBox.SelectedValue.ToString(),
                    id_ram = ramComboBox.SelectedValue.ToString(),
                    id_power = powerComboBox.SelectedValue.ToString(),
                    id_employee = employeeComboBox.SelectedValue.ToString(),
                    id_corpus = corpusComboBox.SelectedValue.ToString(),
                    id_hard_drive = HarddriveComboBox.SelectedValue.ToString(),
                    id_mother = motherComboBox.SelectedValue.ToString(),
                    id_videoadapter = videoadapterComboBox.SelectedValue.ToString(),
                    count = Convert.ToInt32(CountTextBox.Text)
                };

                if (file != null) {
                    computer.photo = file;
                } else {
                    System.Windows.MessageBox.Show("Добавьте фотографию");
                    return;
                }
                 

                db.compukter.Add(computer);

                db.SaveChanges();

                Close();

            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void saveButton_Click_edit(object sender, RoutedEventArgs e)
        {
            try
            {

                var computer = db.compukter.Find(id);


                computer.name = nameTextBox.Text;
                computer.price = Convert.ToDecimal(priceTextBox.Text);
                computer.desc = descTextBox.Text;
                computer.instock = instockCheckBox.IsChecked;
                computer.id_processor = processorComboBox.SelectedValue.ToString();
                computer.id_ram = ramComboBox.SelectedValue.ToString();
                computer.id_power = powerComboBox.SelectedValue.ToString();
                computer.id_employee = employeeComboBox.SelectedValue.ToString();
                computer.id_corpus = corpusComboBox.SelectedValue.ToString();
                computer.id_hard_drive = HarddriveComboBox.SelectedValue.ToString();
                computer.id_mother = motherComboBox.SelectedValue.ToString();
                computer.id_videoadapter = videoadapterComboBox.SelectedValue.ToString();
                computer.count = Convert.ToInt32(CountTextBox.Text);

                if (file != null)
                    computer.photo = file;

                db.mother.Find(computer.id_mother).count -= 1;
                db.processor.Find(computer.id_processor).count -= 1;
                db.hard_drive.Find(computer.id_hard_drive).count -= 1;
                db.corpus.Find(computer.id_corpus).count -= 1;
                db.videoadapter.Find(computer.id_videoadapter).count -= 1;
                db.ram.Find(computer.id_ram).count -= 1;
                db.power.Find(computer.id_power).count -= 1;


                db.SaveChanges();

                Close();

            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void processorComboBox_Selected(object sender, RoutedEventArgs e)
        {
            var idProcessor = "";

            if (id == "")
            {

                idProcessor = processorComboBox.SelectedValue.ToString();

            }
            else
            {

                var comp = db.compukter.Find(id);

                idProcessor = comp.id_processor;

            }

            var selectedProcessor = db.processor.Find(idProcessor);

            motherComboBox.IsEnabled = true;

            motherComboBox.ItemsSource = db.mother.Where(x => x.id_socket == selectedProcessor.id_socket).Where(x => x.count >= 0).ToList();
        }

        private void addPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                addPhotoButton.Content = filename;

                file = File.ReadAllBytes(filename);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            fillComboBoxes();


            if (id != "")
            {
                nameTextBox.Text = item.name;
                priceTextBox.Text = item.price.ToString();
                descTextBox.Text = item.desc;
                instockCheckBox.IsChecked = item.instock;
                processorComboBox.SelectedValue = item.id_processor;
                ramComboBox.SelectedValue = item.id_ram;
                powerComboBox.SelectedValue = item.id_power;
                employeeComboBox.SelectedValue = item.id_employee;
                corpusComboBox.SelectedValue = item.id_corpus;
                HarddriveComboBox.SelectedValue = item.id_hard_drive;
                videoadapterComboBox.SelectedValue = item.id_videoadapter;
                motherComboBox.SelectedValue = item.id_mother;
                CountTextBox.Text = item.count.ToString();
            }
        }

    }
}