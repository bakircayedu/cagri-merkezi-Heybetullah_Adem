using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CagriMerkeziOtomasyonu
{
    public abstract class LinkedListADT
    {
        public Node Head;
        public int Size = 0;
        public abstract void InsertFirst(Cagri value);
        public abstract void InsertLast(Cagri value);
        public abstract Cagri DeleteFirst();
        public abstract void DeleteLast();
        public abstract void DeletePos(Cagri silinecek);
        public abstract Cagri GetByCagriId(int id);
        public abstract int GetPosition(int aranan);
        public abstract Cagri GetByMusteriId(int id);
    }
}
