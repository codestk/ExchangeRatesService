using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;

namespace CoreDb
{
    public class DataBaseFireBird : DataAccessLayer
    {
        public DataBaseFireBird(string connectionString)
            : base(connectionString)
        {
        }


        protected override DbCommand GetCommandDb(string sql, CommandType commandType = CommandType.Text)
        {
            {
                if (Com == null)
                {
                    //_com = new DbCommand (sql, _con);
                    Com = new FbCommand();
                }

                //_com.Connection = _con;
                if (Tran != null)
                {
                    Com.Transaction = Tran;
                }

                Com.Connection = Con;
                Com.CommandText = sql;
                Com.CommandType = commandType;
                Com.CommandTimeout = CommandTimeout;
                return Com;
            }
        }

        protected override DbDataAdapter GetAdapterDb(DbCommand com)
        {
            if (Adapter == null)
            {
                Adapter = new FbDataAdapter();
            }
            com.CommandTimeout = CommandTimeout;
            Adapter.SelectCommand = com;
            return Adapter;
        }

        protected override DbConnection GetConnectionDb()
        {
            if (Tran != null)
            {
                Con = ConTransaction; //Set OldTra Saction
            }

            //Check Null
            if (Con == null)
            {
                Con = new FbConnection(Constr);
                ConTransaction = Con; // Save Connect for Transaction
            }
            return Con;
        }

        protected override IDataParameter GetParameterDb(string parameterName, object value)
        {
            return new FbParameter(parameterName, value);
        }
    }
}