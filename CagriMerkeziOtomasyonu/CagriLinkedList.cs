using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CagriMerkeziOtomasyonu
{
    public class CagriLinkedList : LinkedListADT
    {
        public override Cagri DeleteFirst()
        {
            Cagri temp = Head.Data;
            Node YeniBaslangic = Head.Next;
            Head = YeniBaslangic;
            return temp;
        }

        public override void DeleteLast()
        {
            Node eskiSon = Head;

            while (eskiSon != null)
            {
                if (eskiSon.Next.Next != null)
                {
                    eskiSon = eskiSon.Next;
                }
                else
                {
                    eskiSon.Next = null;
                    break;
                }
            }
        }

        //Çağrı linkedList'inin içinde aradan  çağrı atanınca o çağrıyı listeden silmek için kullanılıyor.
        public override void DeletePos(Cagri silinecek)
        {
            Node temp = Head;
            if (temp.Data == silinecek)
            {
                Head = Head.Next;
            }
            else
            {
                while (temp.Next != null)
                {
                    if (temp.Next.Data == silinecek)
                    {
                        temp.Next = temp.Next.Next;
                        break;
                    }
                    else if (temp.Next != null)
                    {
                        temp = temp.Next;
                    }
                    else
                        break;
                }
            }
            
        }


        public override int GetPosition(int aranan)
        {
            int sira = 1;
            Node temp = Head;
            while (temp != null)
            {
                if (temp.Data.ArayanMusteri.MusteriId == aranan)
                {
                    break;
                }
                else if (temp.Next != null)
                {
                    sira++;
                    temp = temp.Next;
                }
                else
                {
                    sira = 0;
                    break;
                }
                    
            }
            return sira;
        }

        public override void InsertFirst(Cagri value)
        {
            Node gecici = new Node()
            {
                Data = value,
            };
            if (Head == null)
            {
                Head = gecici;
            }
            else
            {
                gecici.Next = Head;
                Head = gecici;
            }
        }

        public override void InsertLast(Cagri value)
        {
            Node eskiSon = Head;
            Node yeniSon = new Node()
            {
                Data = value,
            };
            if (Head == null)
            {
                Head = yeniSon;
            }
            else
            {
                while (eskiSon != null)
                {
                    if (eskiSon.Next != null)
                    {
                        eskiSon = eskiSon.Next;
                    }
                    else
                    {
                        eskiSon.Next = yeniSon;
                        yeniSon.Next = null;
                        break;
                    }
                }
            }
        }
        public List<Node> DisplayElements()
        {
            List<Node> temp = new List<Node>();
            Node item = Head;
            while (item != null)
            {
                temp.Add(item);
                item = item.Next;
            }



            return temp;
        }

        public override Cagri GetByCagriId(int id)
        {
            Node temp = Head;
            Cagri arananCagri = temp.Data;
            while (temp != null)
            {
                if (temp.Data.CagriId == id)
                {
                    break;
                }
                else if (temp.Next != null)
                {
                    temp = temp.Next;
                    arananCagri = temp.Data;
                }
                else
                {
                    break;
                }

            }
            return arananCagri;
        }
        public override Cagri GetByMusteriId(int id)
        {
            Node temp = Head;
            Cagri arananCagri = temp.Data;
            while (temp != null)
            {
                if (temp.Data.ArayanMusteri.MusteriId == id)
                {
                    break;
                }
                else if (temp.Next != null)
                {
                    temp = temp.Next;
                    arananCagri = temp.Data;
                }
                else
                {
                    return null;
                }

            }
            return arananCagri;
        }
    }
}
