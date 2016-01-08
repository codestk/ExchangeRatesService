using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace CoreDb.Collection
{
    public class CommandCollection : IEnumerable
    {
        string _sqlCommand;
        public string SqlCommand
        {
            get
            {
                return _sqlCommand.Trim();
            }
            set
            {
                _sqlCommand = value;
            }
        }

        public List<IDataParameter>Parameter{ get; set; }

        [DefaultValue(CommandType.Text)]
        public CommandType ExecuteType{ get; set; }



        public ArrayList Arr = new ArrayList();
        public CommandCollection Get(int i)
        {
            return (CommandCollection)Arr[i]; //แสดงข้อมูลในตำแหน่ง ที่ i
        }
        public void Add(CommandCollection p)
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

/////         CommandCollection tranSet = new CommandCollection();
//            tranSet.SqlCommand = sql;

//            var parameter = new List<IDataParameter> { Db.CreateParameterDb(":BRAND_TYPE_ID", obj.BRAND_TYPE_ID), Db.CreateParameterDb(":BRAND_TYPE_NAME", obj.BRAND_TYPE_NAME), Db.CreateParameterDb(":BRAND_TYPE_ACTIVE", "A") };

//            tranSet.Parameter = parameter;

//            tranSet.Add(tranSet);