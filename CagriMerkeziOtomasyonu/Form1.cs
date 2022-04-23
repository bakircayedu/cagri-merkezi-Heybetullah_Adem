using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CagriMerkeziOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //***************** Örnek Veriler ***********************
        Musteri musteri = new Musteri();
        Musteri musteri1 = new Musteri
        {
            MusteriTipi = "Bireysel Müşteri"
        };
        Musteri musteri2 = new Musteri
        {
            MusteriTipi = "Bireysel Müşteri"
        };
        Musteri musteri3 = new Musteri
        {
            MusteriTipi = "Ticari Müşteri"
        };
        Musteri musteri4 = new Musteri
        {
            MusteriTipi = "Ticari Müşteri"
        };
        Musteri musteri5 = new Musteri
        {
            MusteriTipi = "Ticari Müşteri"
        };
        BireyselTemsilci temsilci1 = new BireyselTemsilci
        {
            Isim = "Ali",
            TemsilciId = 1,
            CevaplananAramaSayisi=0
        };
        BireyselTemsilci temsilci2 = new BireyselTemsilci
        {
            Isim = "Beyza",
            TemsilciId = 2,
            CevaplananAramaSayisi = 0
        };
        TicariTemsilci temsilci3 = new TicariTemsilci
        {
            Isim = "Cemil",
            TemsilciId = 3,
            CevaplananAramaSayisi = 0
        };
        TicariTemsilci temsilci4 = new TicariTemsilci
        {
            Isim = "Deniz",
            TemsilciId = 4,
            CevaplananAramaSayisi = 0
        };
        Cagri cagri1, cagri2, cagri3, cagri4, cagri5;
        //  ************************************
        Cagri DevamedenCagri;
        CagriMerkezi cagriMerkezi = new CagriMerkezi();
        MusteriTemsilcisi GorusmedekiTemsilci;
        CagriLinkedList BireyselCagrilar_ = new CagriLinkedList();
        CagriLinkedList TicariCagrilar = new CagriLinkedList();
        CagriLinkedList DevamedenCagrilar = new CagriLinkedList();
        CagriLinkedList BitenCagrilar = new CagriLinkedList();
        MusteriTemsilcisiKuyrukYapısı BostaBireyselTemsilciler = new MusteriTemsilcisiKuyrukYapısı(2);
        MusteriTemsilcisiKuyrukYapısı BostaTicariTemsilciler = new MusteriTemsilcisiKuyrukYapısı(2);
        List<MusteriTemsilcisi> GorusmedeOlanTemsilciler = new List<MusteriTemsilcisi>();
        int gorusmeSirasi = 0;
        private void btnAramaYap_Click(object sender, EventArgs e)
        {
            CagriEkle(musteri);
            gBoxMusteri.Visible = true;
            gBoxArama.Visible = false;
            AramaSirasi(musteri);
        }

        //Çağrı eklenince sistemde kayıtlı olan müşterinin sırasını güncellemek için kullanılıyor.
        public void AramaSirasi(Musteri musteri)
        {
            gorusmeSirasi = BireyselCagrilar_.GetPosition(musteri.MusteriId);
            if (gorusmeSirasi !=0) 
            {
                lblMusteriSirasi.Text = "Lütfen Bekleyiniz Sıranız: "+gorusmeSirasi.ToString();
            }
            else
            {
                gorusmeSirasi = TicariCagrilar.GetPosition(musteri.MusteriId);
                if (gorusmeSirasi != 0)
                {
                    lblMusteriSirasi.Text = "Lütfen Bekleyiniz Sıranız: " + gorusmeSirasi.ToString();
                }
                else
                {
                    gBoxMusteri.Visible = false;
                    gBoxArama.Visible = true;
                }
            }
            
        }
        // sisteme kayıtlı olan müşteri üzerinden çağrı eklemek için kullanılıyor.
        public void CagriEkle(Musteri musteri)
        {
            if (checkListMusteriTipi.CheckedItems.Count == 1)
            {
                musteri.MusteriTipi = checkListMusteriTipi.CheckedItems[0].ToString();
                Cagri cagri = new Cagri(DateTime.Now, musteri);
                if (musteri.MusteriTipi == "Bireysel Müşteri")
                {
                    cagri.CagriEkle(BireyselCagrilar_, cagri);
                }
                else
                {
                    cagri.CagriEkle(TicariCagrilar, cagri);
                }
                BeklenenCagrilariListele();
            }
            else
            {
                MessageBox.Show("Müşteri Türnüzü Seçiniz");
            };
            
        }


        //Cevaplanmayı bekleyen çağrıları listelemek için kullanılıyor
        public void BeklenenCagrilariListele()
        {
            cagriMerkezi.BekleyenCagrilariListele(TicariCagrilarListBox, BireyselCagrilarListBox, TicariCagrilar, BireyselCagrilar_);            
        }

        //Çağrı cevaplamak üzere uygun olan müşteri temsilcileri listelemek için kullanılıyor.
        public void BostakiTemsilcileriListele()
        {
            cagriMerkezi.BostakiTemsilcileriListele(lBoxBostakiBirTemsilciler, lBoxBostakiTicTemsilciler, BostaBireyselTemsilciler, BostaTicariTemsilciler);
        }

        private void btnBireyselCagriBitir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtBireyselCagriId.Text);
            Cagri cagri = DevamedenCagrilar.GetByCagriId(id);
            cagri.CagriBitir(BitenCagrilar, cagri, txtBireyselAramaNotlari,cagriMerkezi);
            AramaSirasi(musteri);
            TemsilciListesiniGuncelle(txtBirTemsilciId, BostaBireyselTemsilciler);
            BostakiTemsilcileriListele();
            gBoxBireysel.Visible = false;
            txtBireyselMusteriId.Text = "";
        }

        private void btnBireyselAramaAta_Click(object sender, EventArgs e)
        {
            DevamedenCagri = BireyselCagrilar_.DeleteFirst();
            GorusmedekiTemsilci=cagriMerkezi.CagiriAtama(BostaBireyselTemsilciler, GorusmedekiTemsilci, DevamedenCagrilar, DevamedenCagri, GorusmedeOlanTemsilciler);
            if (txtBireyselMusteriId.Text != "")
            {
                DevamedenCagri.CagriAtama(gBoxBireysel1, GorusmedekiTemsilci, new TextBox[] { txtBirTemsilciId1, txtBireyselMusteriId1, txtBireyselAramaZamani1, txtBireyselCagriId1 }, DevamedenCagri);
            }
            else
            {
                DevamedenCagri.CagriAtama(gBoxBireysel, GorusmedekiTemsilci, new TextBox[] { txtBirTemsilciId, txtBireyselMusteriId, txtBireyselAramaZamani, txtBireyselCagriId }, DevamedenCagri);
            }
            BeklenenCagrilariListele();
            BostakiTemsilcileriListele();
            if (DevamedenCagri.ArayanMusteri.MusteriId == musteri.MusteriId)
            {
                lblMusteriSirasi.Text = "Görüşmeye Alındınız";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtBireyselCagriId1.Text);
            Cagri cagri = DevamedenCagrilar.GetByCagriId(id);
            cagri.CagriBitir(BitenCagrilar, cagri, txtBireyselAramaNotlari1,cagriMerkezi);
            AramaSirasi(musteri);
            TemsilciListesiniGuncelle(txtBirTemsilciId1, BostaBireyselTemsilciler);
            BostakiTemsilcileriListele();
            gBoxBireysel1.Visible = false;
            txtBireyselMusteriId1.Text = "";
        }

        private void btnTicAramaBitir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTicCagriId.Text);
            Cagri cagri = DevamedenCagrilar.GetByCagriId(id);
            cagri.CagriBitir(BitenCagrilar, cagri, txtTicCagriNotu,cagriMerkezi);
            AramaSirasi(musteri);
            TemsilciListesiniGuncelle(txtTicTemsilciId, BostaTicariTemsilciler);
            BostakiTemsilcileriListele();
            gBoxTicari.Visible = false;
            txtTicMusteriId.Text = "";
        }

        private void btnOzelTicariAramaAta_Click(object sender, EventArgs e)
        {
            
            DevamedenCagri = TicariCagrilar.GetByMusteriId(Convert.ToInt32(txtTicariCagriId.Text));
            GorusmedekiTemsilci = cagriMerkezi.CagiriAtama(BostaTicariTemsilciler, GorusmedekiTemsilci, DevamedenCagrilar, DevamedenCagri, GorusmedeOlanTemsilciler);
            if (txtTicMusteriId.Text != "")
            {
                DevamedenCagri.CagriAtama(gBoxTicari1, GorusmedekiTemsilci, new TextBox[] { txtTicTemsilciId1, txtTicMusteriId1, txtTicAramaZamani1, txtTicCagriId1 }, DevamedenCagri);
            }
            else
            {
                DevamedenCagri.CagriAtama(gBoxTicari, GorusmedekiTemsilci, new TextBox[] { txtTicTemsilciId, txtTicMusteriId, txtTicAramaZamani, txtTicCagriId }, DevamedenCagri);
            }
            BostakiTemsilcileriListele();
            TicariCagrilar.DeletePos(DevamedenCagri);
            BeklenenCagrilariListele();
            if (DevamedenCagri.ArayanMusteri.MusteriId == musteri.MusteriId)
            {
                lblMusteriSirasi.Text = "Görüşmeye Alındınız";
            }
        }

        private void btnOzelBireyselAramaAta_Click(object sender, EventArgs e)
        {
            DevamedenCagri = BireyselCagrilar_.GetByMusteriId(Convert.ToInt32(txtBirMusteriId.Text));
            GorusmedekiTemsilci = cagriMerkezi.CagiriAtama(BostaBireyselTemsilciler, GorusmedekiTemsilci, DevamedenCagrilar, DevamedenCagri, GorusmedeOlanTemsilciler);
            if (txtTicMusteriId.Text != "")
            {
                DevamedenCagri.CagriAtama(gBoxBireysel1, GorusmedekiTemsilci, new TextBox[] { txtBirTemsilciId1, txtBireyselMusteriId1, txtBireyselAramaZamani1, txtBireyselCagriId1 }, DevamedenCagri);
            }
            else
            {
                DevamedenCagri.CagriAtama(gBoxBireysel, GorusmedekiTemsilci, new TextBox[] { txtBirTemsilciId, txtBireyselMusteriId, txtBireyselAramaZamani, txtBireyselCagriId }, DevamedenCagri);
            }
            BostakiTemsilcileriListele();
            BireyselCagrilar_.DeletePos(DevamedenCagri);
            BeklenenCagrilariListele();
            if (DevamedenCagri.ArayanMusteri.MusteriId == musteri.MusteriId)
            {
                lblMusteriSirasi.Text = "Görüşmeye Alındınız";
            }
        }

        private void btnTicAramaBitir1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTicCagriId1.Text);
            Cagri cagri = DevamedenCagrilar.GetByCagriId(id);
            cagri.CagriBitir(BitenCagrilar, cagri, txtTicCagriNotu1,cagriMerkezi);
            AramaSirasi(musteri);
            TemsilciListesiniGuncelle(txtTicTemsilciId1, BostaTicariTemsilciler);
            BostakiTemsilcileriListele();
            gBoxTicari1.Visible = false;
            txtTicMusteriId1.Text = "";
        }

        private void btnRaporListele_Click(object sender, EventArgs e)
        {
            dgViewTemsilciSirasi.DataSource = null;
            List<MusteriTemsilcisi> siralanmisTemsilciler = cagriMerkezi.TemsilcileriSirala(cagriMerkezi.TumTemsilciler);
            dgViewTemsilciSirasi.DataSource = siralanmisTemsilciler;
        }

        private void btnTicariAramaAta_Click(object sender, EventArgs e)
        {
            DevamedenCagri = TicariCagrilar.DeleteFirst();
            GorusmedekiTemsilci = cagriMerkezi.CagiriAtama(BostaTicariTemsilciler, GorusmedekiTemsilci, DevamedenCagrilar, DevamedenCagri, GorusmedeOlanTemsilciler);
            if (txtTicMusteriId.Text != "")
            {
                DevamedenCagri.CagriAtama(gBoxTicari1, GorusmedekiTemsilci, new TextBox[] { txtTicTemsilciId1, txtTicMusteriId1, txtTicAramaZamani1, txtTicCagriId1 }, DevamedenCagri);
            }
            else
            {
                DevamedenCagri.CagriAtama(gBoxTicari, GorusmedekiTemsilci, new TextBox[] { txtTicTemsilciId, txtTicMusteriId, txtTicAramaZamani, txtTicCagriId }, DevamedenCagri);
            }
            BostakiTemsilcileriListele();
            BeklenenCagrilariListele();
            if (DevamedenCagri.ArayanMusteri.MusteriId == musteri.MusteriId)
            {
                lblMusteriSirasi.Text = "Görüşmeye Alındınız";
            }
        }

        private void btnNotAra_Click(object sender, EventArgs e)
        {
            dgvNotlar.DataSource = null;
            string aranaKelime = txtArananKelime.Text.ToLower();
            if (aranaKelime != "")
            {
                List<Not> UygunNotlar= cagriMerkezi.ArananNotlar(cagriMerkezi.Nots, aranaKelime);
                dgvNotlar.DataSource = UygunNotlar;
            }
        }


        //Görüşmesi biten müşteri temsilcisini tekrar çağırı almak üzere kuyruk yapısına eklemek için kullanılıyor.
        public void TemsilciListesiniGuncelle(TextBox textBox,MusteriTemsilcisiKuyrukYapısı musteriTemsilcileri)
        {
            MusteriTemsilcisi birTemsilci = GorusmedeOlanTemsilciler.Find(x => x.TemsilciId.ToString() == textBox.Text);
            musteriTemsilcileri.Insert(birTemsilci);
        }
        private void btnOrnekVeriYukle_Click(object sender, EventArgs e)
        {
            OrnekVeriler();
            TemsilcileriEkle();
        }
        
        public void OrnekVeriler()
        {
            musteri.MusteriId = 0;
            musteri1.MusteriId = 1;
            musteri2.MusteriId = 2;
            musteri3.MusteriId = 3;
            musteri4.MusteriId = 4;
            musteri5.MusteriId = 5;
            cagri1 = new Cagri(DateTime.Now, musteri1);
            cagri1.CagriId = 1;
            BireyselCagrilar_.InsertLast(cagri1);
            cagri2 = new Cagri(DateTime.Now, musteri2);
            cagri2.CagriId = 2;
            BireyselCagrilar_.InsertLast(cagri2);
            cagri3 = new Cagri(DateTime.Now, musteri3);
            cagri3.CagriId = 3;
            TicariCagrilar.InsertLast(cagri3);
            cagri4 = new Cagri(DateTime.Now, musteri4);
            cagri4.CagriId = 4;
            TicariCagrilar.InsertLast(cagri4);
            cagri5 = new Cagri(DateTime.Now, musteri5);
            cagri5.CagriId = 5;
            TicariCagrilar.InsertLast(cagri5);
            BeklenenCagrilariListele();
            BostaBireyselTemsilciler.Insert(temsilci1);
            BostaBireyselTemsilciler.Insert(temsilci2);
            BostaTicariTemsilciler.Insert(temsilci3);
            BostaTicariTemsilciler.Insert(temsilci4);
            BostakiTemsilcileriListele();
        }
        public void TemsilcileriEkle()
        {
            cagriMerkezi.TemsilciEkle(temsilci1);
            cagriMerkezi.TemsilciEkle(temsilci2);
            cagriMerkezi.TemsilciEkle(temsilci3);
            cagriMerkezi.TemsilciEkle(temsilci4);
        }
    }
}
