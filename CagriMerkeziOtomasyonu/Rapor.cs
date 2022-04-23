using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CagriMerkeziOtomasyonu
{
    public class Rapor
    {
        public int CagriId { get; set; }
        public int MusteriId { get; set; }
        public int CalisanId { get; set; }
        public string MusteriIsim { get; set; }
        public string CalisanIsim { get; set; }
        public string CagriNotu { get; set; }
        public DateTime AramaZamani { get; set; }
    }
}
