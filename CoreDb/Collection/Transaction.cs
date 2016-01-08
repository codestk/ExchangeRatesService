using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace CoreDb.Collection
{
  public   class Transaction
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

        public List<IDataParameter> Parameter { get; set; }

        [DefaultValue(CommandType.Text)]
        public CommandType ExecuteType { get; set; }
    }
}
