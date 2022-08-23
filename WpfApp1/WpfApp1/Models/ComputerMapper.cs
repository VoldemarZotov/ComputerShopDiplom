using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp1.Models
{
    static class ComputerMapper
    {

        public static compukter MapToDb(this ComputerViewModel item) {

            return new compukter
            {
                id = item.id,
                name = item.name,
                desc = item.desc,
                price = item.price,
                instock = item.instock,
                photo = item.photoByte,
                id_processor = item.id_processor,
                id_employee = item.id_employee,
                id_corpus = item.id_corpus,
                id_hard_drive = item.id_hard_drive,
                id_mother = item.id_mother,
                id_power = item.id_power,
                id_ram = item.id_ram,
                id_videoadapter = item.id_videoadapter,
                corpus = item.corpus,
                Employee = item.Employee,
                hard_drive = item.hard_drive,
                mother = item.mother,
                power = item.power,
                processor = item.processor,
                 ram = item.ram,
                 videoadapter = item.videoadapter
            };
            
        }

        public static ComputerViewModel MapToViewModel(this compukter item)
        {

            return new ComputerViewModel
            {
                id = item.id,
                name = item.name,
                desc = item.desc,
                price = item.price,
                instock = item.instock,
                photoByte = item.photo,
                photo = toBitmap(item.photo),
                id_processor = item.id_processor,
                id_employee = item.id_employee,
                id_corpus = item.id_corpus,
                id_hard_drive = item.id_hard_drive,
                id_mother = item.id_mother,
                id_power = item.id_power,
                id_ram = item.id_ram,
                id_videoadapter = item.id_videoadapter,
                corpus = item.corpus,
                Employee = item.Employee,
                hard_drive = item.hard_drive,
                mother = item.mother,
                power = item.power,
                processor = item.processor,
                ram = item.ram,
                videoadapter = item.videoadapter
            };

        }


        public static BitmapImage toBitmap(Byte[] value)
        {
            if (value != null && value is byte[])
            {
                byte[] ByteArray = value as byte[];
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(ByteArray);
                bmp.EndInit();
                return bmp;
            }
            return null;
        }
    }
}
