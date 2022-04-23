using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CagriMerkeziOtomasyonu
{
    interface IKuyrukTemsilci
    {
        void Insert(MusteriTemsilcisi o);
        MusteriTemsilcisi Remove();
        MusteriTemsilcisi Peek();
        Boolean IsEmpty();
    }
}
