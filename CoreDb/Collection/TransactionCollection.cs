using System.Collections;
using System.ComponentModel;
using System.Data;

namespace CoreDb.Collection
{
   public  class TransactionCollection: IEnumerable
    {
           [DefaultValue(CommandType.Text)]
        public CommandType ExecuteType{ get; set; }



        public ArrayList Arr = new ArrayList();
        public Transaction Get(int i)
        {
            return (Transaction)Arr[i]; //แสดงข้อมูลในตำแหน่ง ที่ i
        }
        public void Add(Transaction p)
        {
            Arr.Add(p); // เพื่ม ข้อมูลชนิด object ของคลาส Person เข้าไปใน ArrayList
        }
        public int Count
        {
            get
            {
                return Arr.Count; // นับจำนวนสมาชิกใน ArrayList
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Arr.GetEnumerator(); //สำหรับการวนลูปโดยใช้ foreach
        }
    }
    }

