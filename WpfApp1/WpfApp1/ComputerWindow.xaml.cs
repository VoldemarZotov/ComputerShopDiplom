using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using Paragraph = iTextSharp.text.Paragraph;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using WpfApp1.Models.Const;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ComputerWindow.xaml
    /// </summary>
    public partial class ComputerWindow : Window
    {

        ShopEntities db = new ShopEntities();

        string name = "";
        decimal price = 0;
        string processor = "";
        string videoadapter = "";
        string mother = "";
        string ram = "";
        string hard = "";
        string corpus = "";
        string power = "";

        public ComputerWindow()
        {
            InitializeComponent();

            updateItems();
            fillComboBoxes();

            switch (Enum.Parse(typeof(Role), db.Employee.Find(User.idUser).role.ToString()))
            {
                case Role.SELLER:
                    
                    addButton.IsEnabled = false;
                    editButton.IsEnabled = false;
                    exportPdfButton.IsEnabled = false;
                    exportButton.IsEnabled = false;
                   
                    break;

            }
        }
        
        void fillComboBoxes()
        {
            var pr = db.processor.ToList();
            pr.Add(new processor { id = "", name = "Все" });
            processorFilterTextBox.ItemsSource = pr;

            var rams = db.ram.ToList();
            rams.Add(new ram { id = "", name = "Все" });
            memoryFilterTextBox.ItemsSource = rams;

            var powers = db.power.ToList();
            powers.Add(new power { id = "", name = "Все" });
            powerFilterTextBox.ItemsSource = powers;

            var corpuses = db.corpus.ToList();
            corpuses.Add(new corpus { id = "", name = "Все" });
            corpusFilterTextBox.ItemsSource = corpuses;

            var hards= db.hard_drive.ToList();
            hards.Add(new hard_drive { id = "", name = "Все" });
            hardFilterTextBox.ItemsSource =hards;

            var videoadapters = db.videoadapter.ToList();
            videoadapters.Add(new videoadapter { id = "", name = "Все" });
            vidioadapterFilterTextBox.ItemsSource = videoadapters;

            var mothers = db.mother.ToList();
            mothers.Add(new mother { id = "", name = "Все" });
            matherFilterTextBox.ItemsSource = mothers;
        }

        private void updateItems()
        {
            var items =  db.compukter.ToList();

            if (name != "")
            {
                items = items.Where(x => x.name.Contains(name)).ToList();
            }

            if (price != 0)
            {
                items = items.Where(x => x.price <= price).ToList();
            }

            if (processor != "")
            {
                items = items.Where(x => x.id_processor == processor).ToList();
            }

            if (videoadapter != "")
            {
                items = items.Where(x => x.id_videoadapter == videoadapter).ToList();
            }
            if (mother != "")
            {
                items = items.Where(x => x.id_mother == mother).ToList();

            }
            if (ram != "")
            {
                items = items.Where(x => x.id_ram == ram).ToList();

            }
            if (hard != "")
            {
                items = items.Where(x => x.id_hard_drive == hard).ToList();
            }
            if (corpus != "")
            {
                items = items.Where(x => x.id_corpus == corpus).ToList();
            }
            if (power != "")
            {
                items = items.Where(x => x.id_power == power).ToList();
            }

            List<ComputerViewModel> list = new List<ComputerViewModel>();

            foreach (var item in items)
            {
                list.Add(item.MapToViewModel());
            }

            listView.ItemsSource = list;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            ComputerFormWindow window = new ComputerFormWindow();
            window.ShowDialog();

            updateItems();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedItem = (listView.SelectedItem as ComputerViewModel);

            if (selectedItem == null) {

                MessageBox.Show("Выберите элемент в списке");
                return;
            }

            ComputerFormWindow window = new ComputerFormWindow(selectedItem.id);
            window.ShowDialog();

            updateItems();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = (listView.SelectedItem as ComputerViewModel).id;

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

                var item = db.compukter.Find(id);

                db.compukter.Remove(item);

                db.SaveChanges();

                updateItems();
            }
            catch { }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            var items = db.compukter.ToList();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);


            int columnCounter = 1;

            ExcelApp.Cells[1, columnCounter] = "Наименование";
            ExcelApp.Cells[1, ++columnCounter] = "Описание";
            ExcelApp.Cells[1, ++columnCounter] = "Цена";
            ExcelApp.Cells[1, ++columnCounter] = "Наличие";
            ExcelApp.Cells[1, ++columnCounter] = "Процессор";
            ExcelApp.Cells[1, ++columnCounter] = "Оперативная память";
            ExcelApp.Cells[1, ++columnCounter] = "Материнская плата";
            ExcelApp.Cells[1, ++columnCounter] = "Видеокарта";
            ExcelApp.Cells[1, ++columnCounter] = "Жёсткий диск";
            ExcelApp.Cells[1, ++columnCounter] = "Блок питания";
            ExcelApp.Cells[1, ++columnCounter] = "корпус";
            ExcelApp.Cells[1, ++columnCounter] = "сотрудники";

            for (int i = 0; i < items.Count; i++)
            {
                int counter = 1;
                ExcelApp.Cells[i + 2, counter] = items[i].name;
                ExcelApp.Cells[i + 2, ++counter] = items[i].desc;
                ExcelApp.Cells[i + 2, ++counter] = items[i].price;
                ExcelApp.Cells[i + 2, ++counter] = items[i].instock;
                ExcelApp.Cells[i + 2, ++counter] = items[i].processor.name;
                ExcelApp.Cells[i + 2, ++counter] = items[i].ram.name;
                ExcelApp.Cells[i + 2, ++counter] = items[i].mother.name;
                ExcelApp.Cells[i + 2, ++counter] = items[i].videoadapter.name;
                ExcelApp.Cells[i + 2, ++counter] = items[i].hard_drive.name;
                ExcelApp.Cells[i + 2, ++counter] = items[i].power.name;
                ExcelApp.Cells[i + 2, ++counter] = items[i].corpus.name;
                ExcelApp.Cells[i + 2, ++counter] = items[i].Employee.name;
            }

            ExcelWorkSheet.Columns.AutoFit();
            ExcelWorkSheet.Rows.AutoFit();
            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            name = "";
            price = 0;
            processor = "";
            videoadapter = "";
            mother = "";
            ram = "";
            hard = "";
            corpus = "";
            power = "";

            nameFilterTextBox.Text = "";
            priceFilterTextBox.Text = "";
            processorFilterTextBox.SelectedValue = "";
            vidioadapterFilterTextBox.SelectedValue = "";
            matherFilterTextBox.SelectedValue = "";
            memoryFilterTextBox.SelectedValue = "";
            hardFilterTextBox.SelectedValue = "";
            corpusFilterTextBox.SelectedValue = "";
            powerFilterTextBox.SelectedValue = "";

            updateItems();
        }

        private void processorFilterTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            processor = processorFilterTextBox.SelectedValue.ToString();
            updateItems();
        }

        private void matherFilterTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mother = matherFilterTextBox.SelectedValue.ToString();
            updateItems();
        }

        private void vidioadapterFilterTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            videoadapter = vidioadapterFilterTextBox.SelectedValue.ToString();
            updateItems();
        }

        private void priceFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal result = 0;

            if (decimal.TryParse(priceFilterTextBox.Text, out result))
            {
                price = result;
            }
            else
            {
                price = 0;
            }

            updateItems();
        }

        private void nameFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = nameFilterTextBox.Text;
            updateItems();
        }

        private void memoryFilterTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ram = memoryFilterTextBox.SelectedValue.ToString();
            updateItems();
        }

        private void powerFilterTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            power = powerFilterTextBox.SelectedValue.ToString();
            updateItems();
        }

        private void corpusFilterTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            corpus = corpusFilterTextBox.SelectedValue.ToString();
            updateItems();
        }

        private void hardFilterTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hard = hardFilterTextBox.SelectedValue.ToString();
            updateItems();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            processorFilterTextBox.SelectedValue = "";
            hardFilterTextBox.SelectedValue = "";
            memoryFilterTextBox.SelectedValue = "";
            matherFilterTextBox.SelectedValue = "";
            corpusFilterTextBox.SelectedValue = "";
            powerFilterTextBox.SelectedValue = "";
            vidioadapterFilterTextBox.SelectedValue = "";
        }

        private void exportPdfButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.ShowDialog();

            var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3.Rotate(), 30f, 30f, 30f, 30f);
            using (System.IO.FileStream ms = new System.IO.FileStream(sfd.FileName + ".pdf", System.IO.FileMode.Create))
            {
                using (var writer = PdfWriter.GetInstance(document, ms))
                {
                    BaseFont bf = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    Font f = new Font(bf, 10, Font.NORMAL);


                    //шапка - просто надписи сверху таблицы
                    document.Open();
                    var p = new Paragraph(new iTextSharp.text.Chunk("Компьютеры", f));
                    p.Alignment = Element.ALIGN_CENTER;
                    document.Add(p);
                    document.Add(new Paragraph(" "));

                    //сама таблица

                    var items = db.compukter.ToList();

                    PdfPTable table = new PdfPTable(12);

                    PdfPCell cell = new PdfPCell();
                    cell.Padding = 5;
                    cell.Border = 1;

                    cell.Phrase = new Phrase("Наименование", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Описание", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Наличие", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Цена", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Процессор", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Оперативная память", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Материнская плата", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Видеокарта", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Жёсткий диск", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Блок питания", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Корпус", f);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase("Сотрудник", f);
                    table.AddCell(cell);

                    foreach (var item in items)
                    {

                            cell.Phrase = new Phrase($"{item.name}", f);
                            table.AddCell(cell);

                            cell.Phrase = new Phrase($"{item.desc}", f);
                            table.AddCell(cell);

                            cell.Phrase = new Phrase(item.instock == true ? "В наличии" : "Нет в наличии", f);
                            table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.price} руб.", f);
                        table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.processor.name}", f);
                        table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.ram.name}", f);
                        table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.mother.name}", f);
                        table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.videoadapter.name}", f);
                        table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.hard_drive.name}", f);
                        table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.power.name}", f);
                        table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.corpus.name}", f);
                        table.AddCell(cell);

                        cell.Phrase = new Phrase($"{item.Employee.name}", f);
                        table.AddCell(cell);


                    }

                    document.Add(table);
                    document.Close();
                    writer.Close();
                    ms.Close();
                }
            }
        }
    }
}
