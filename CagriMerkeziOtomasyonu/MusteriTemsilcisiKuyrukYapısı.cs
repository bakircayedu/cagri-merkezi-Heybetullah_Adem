using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CagriMerkeziOtomasyonu
{
    public class MusteriTemsilcisiKuyrukYapısı : IKuyrukTemsilci
    {
        public MusteriTemsilcisi[] Queue;
        private int front = -1;
        private int rear = -1;
        private int size = 0;
        private int count = 0;
        public MusteriTemsilcisiKuyrukYapısı(int size)
        {
            this.size = size;
            Queue = new MusteriTemsilcisi[size];
        }
        public void Insert(MusteriTemsilcisi o)
        {
            if ((count == size))
                throw new Exception("Queue dolu.");
            if (front == -1)
                front = 0;
            if (rear == size - 1)
            {
                rear = 0;
                Queue[rear] = o;
            }
            else
                Queue[++rear] = o;
            count++;
        }

        public bool IsEmpty()
        {
            return (count == 0);
        }

        public MusteriTemsilcisi Peek()
        {
            if (IsEmpty())
            {
                throw new Exception("Queue boş.");
            }
            else
            {
                MusteriTemsilcisi temp = Queue[front];
                return temp;
            }
        }

        public MusteriTemsilcisi Remove()
        {
            if (IsEmpty())
                return null;
            MusteriTemsilcisi temp = Queue[front];
            Queue[front] = null;
            if (front == size - 1)
                front = 0;
            else
                front++;
            count--;
            return temp;
        }

    }
}
