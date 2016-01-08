using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CoreDb
{
    public class DataBaseSql : DataAccessLayer
    {
        //public DataBaseSql( ){ }

        public DataBaseSql(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbCommand GetCommandDb(string sql, CommandType commandType = CommandType.Text)
        {
            {
                if (Com == null)
                {
                    Com = new SqlCommand();
                }

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
                Adapter = new SqlDataAdapter();
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
                Con = new SqlConnection(Constr);
                ConTransaction = Con; // Save Connect for Transaction
            }

            return Con;
        }

        protected override IDataParameter GetParameterDb(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }
    }
}