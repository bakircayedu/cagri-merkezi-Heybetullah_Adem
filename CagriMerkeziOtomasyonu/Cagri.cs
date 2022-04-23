using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CagriMerkeziOtomasyonu
{
    public class Cagri
    {
        public int CagriId { get; set; }
        public DateTime BaslamaZamani { get; set; }
        public DateTime BitisZamani { get; set; }
        public MusteriTemsilcisi CevaplayanTemsilci { get; set; }
        public Musteri ArayanMusteri { get; set; }
        public string CagriNotu { get; set; }
        public Cagri(DateTime dateTime,Musteri musteri)
        {
            BaslamaZamani = dateTime;
            ArayanMusteri = musteri;
        }
        //Çağrı bitirme fonksiyonu 
        public void CagriBitir(CagriLinkedList cagriLinkedList, Cagri cagri,TextBox textBox, CagriMerkezi cagriMerkezi)
        {
            cagri.BitisZamani = DateTime.Now;
            cagri.CagriNotu = textBox.Text;
            cagriLinkedList.InsertLast(cagri);
            Not not = new Not
            {
                CagriNotu = cagri.CagriNotu,
                AramaZamani = cagri.BaslamaZamani,
                MusteriId = cagri.ArayanMusteri.MusteriId,
                TemsilciIsmi = cagri.CevaplayanTemsilci.Isim
            };
            cagriMerkezi.NotEkle(not);
        }
        //Arama yapıldığında aramayı bekleme listesine eklemek için kullanılıyor
        public void CagriEkle(CagriLinkedList cagriLinkedList, Cagri cagri)
        {
                cagriLinkedList.InsertLast(cagri);
        }

        //Uygun müşteri temsilcisine çağırı atamak için ve form üzerinde gerekli değişiklikleri yapmak için kullanılıyor
        public void CagriAtama(GroupBox groupBox, MusteriTemsilcisi musteriTemsilcisi, TextBox[] textBoxes,Cagri cagri)
        {
            groupBox.Visible = true;
            cagri.CevaplayanTemsilci = musteriTemsilcisi;
            textBoxes[0].Text = cagri.CevaplayanTemsilci.TemsilciId.ToString();
            textBoxes[1].Text = cagri.ArayanMusteri.MusteriId.ToString();
            textBoxes[2].Text = cagri.BaslamaZamani.ToString("h:mm:ss");
            textBoxes[3].Text = cagri.CagriId.ToString();
        }

    }
}
