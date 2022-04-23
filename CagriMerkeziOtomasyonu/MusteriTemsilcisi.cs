using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CagriMerkeziOtomasyonu
{
    public abstract class MusteriTemsilcisi
    {
        public int TemsilciId { get; set; }
        public string Isim { get; set; }
        public int CevaplananAramaSayisi { get; set; }

    }
}
