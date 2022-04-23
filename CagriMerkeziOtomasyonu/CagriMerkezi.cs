using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CagriMerkeziOtomasyonu
{
    public class CagriMerkezi
    {
        public List<MusteriTemsilcisi> TumTemsilciler = new List<MusteriTemsilcisi>(); //çağrı merkezinde çalışan müşteri temsilcilerin listesi
        public List<Not> Nots = new List<Not>(); //aramalarda alınan notların listesi

        //Not eklemek için kullanılıyor
        public void NotEkle(Not not)
        {
            Nots.Add(not);
        }

        //Temsilci eklemek için kullanılıyor.
        public void TemsilciEkle(MusteriTemsilcisi musteriTemsilcisi)
        {
            TumTemsilciler.Add(musteriTemsilcisi);
        }

        //Notlar arasında arama yapmak için kullanılıyor
        public List<Not> ArananNotlar(List<Not> nots,string arananKelime)
        {
            List<Not> UygunNotlar = new List<Not>();
            
            for (int i = 0; i < nots.Count; i++)
            {
                string[] kelimeler = nots[i].CagriNotu.Split();
                for (int j = 0; j < kelimeler.Length; j++)
                {
                    if (kelimeler[j].ToLower() == arananKelime)
                    {
                        UygunNotlar.Add(nots[i]);
                        break;
                    }
                }
            }
            return UygunNotlar;
        }

        //Cevaplanmayı bekleyen çağrıları listelemek için kullanılıyor
        public void BekleyenCagrilariListele(ListBox listBox1, ListBox listBox2, CagriLinkedList cagriLinkedList1, CagriLinkedList cagriLinkedList2)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var item in cagriLinkedList1.DisplayElements())
            {
                if (item != null)
                {
                    listBox1.Items.Add("Müşteri Id: " + item.Data.ArayanMusteri.MusteriId);
                }
            }
            foreach (var item in cagriLinkedList2.DisplayElements())
            {
                if (item != null)
                {
                    listBox2.Items.Add("Müşteri Id: " + item.Data.ArayanMusteri.MusteriId);
                }
            }
        }

        //Çağrı cevaplamak üzere uygun olan müşteri temsilcileri listelemek için kullanılıyor.
        public void BostakiTemsilcileriListele(ListBox listBox1, ListBox listBox2,MusteriTemsilcisiKuyrukYapısı musteriTemsilcileri1, MusteriTemsilcisiKuyrukYapısı musteriTemsilcileri2)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var item in musteriTemsilcileri1.Queue)
            {
                if (item != null)
                {
                    listBox1.Items.Add("Temsilci Id: " + item.TemsilciId);
                }
            }
            foreach (var item in musteriTemsilcileri2.Queue)
            {
                if (item != null)
                {
                    listBox2.Items.Add("Temsilci Id: " + item.TemsilciId);
                }
            }
        }

        //Müşteri temsilcisine çağrı atamak için kullanılıyor.
        public MusteriTemsilcisi CagiriAtama(MusteriTemsilcisiKuyrukYapısı musteriTemsilcileri,MusteriTemsilcisi musteriTemsilcisi, CagriLinkedList devamEdenCagrilar, Cagri cagri, List<MusteriTemsilcisi> GorusmedeOlanTemsilciler)
        {
            if (cagri != null)
            {
                musteriTemsilcisi = musteriTemsilcileri.Remove();
                if (musteriTemsilcisi != null)
                {
                    devamEdenCagrilar.InsertLast(cagri);
                    musteriTemsilcisi.CevaplananAramaSayisi++;
                    GorusmedeOlanTemsilciler.Add(musteriTemsilcisi);
                }
                else
                {
                    MessageBox.Show("Uygun müşteri temsilcisi bulunmamaktadır. Lütfen Bekleyiniz.");
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Lütfen listede bulunan bir çağrıyı seçiniz.");
                return null;
            }
            return musteriTemsilcisi;
        }

        //Cevaplanan çağrı sayısına göre müşteri temsilcilerini sıralamak için kullanılıyor.
        public List<MusteriTemsilcisi> TemsilcileriSirala(List<MusteriTemsilcisi> tumTemsilciler)
        {
            int n = 4;//Toplam 4 tane müşteri temsilcimiz var
            int minIndis = 0;

            for (int i = 0; i < n; i++)
            {
                minIndis = i;
                for (int j = i+1; j < n; j++)
                {
                    if (tumTemsilciler[j].CevaplananAramaSayisi > tumTemsilciler[minIndis].CevaplananAramaSayisi)
                    {
                        minIndis = j;
                    }
                }
                if (minIndis != i)
                {
                    MusteriTemsilcisi temp = tumTemsilciler[i];
                    tumTemsilciler[i] = tumTemsilciler[minIndis];
                    tumTemsilciler[minIndis] = temp;
                }
            }
            return tumTemsilciler;
        }
    }
}
