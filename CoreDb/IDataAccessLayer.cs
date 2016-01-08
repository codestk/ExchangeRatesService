using CoreDb.Collection;
using System.Collections.Generic;
using System.Data;

namespace CoreDb
{
    public interface IDataAccessLayer
    {
        DataSet GetDataSet(string sql);

        DataSet GetDataSet(string sql, List<IDataParameter> parms,
            CommandType commandType = CommandType.Text);

        string FbExecuteScalar(string sql);

        string FbExecuteScalar(string sql, List<IDataParameter> parms);

        int FbExecuteNonQuery(TransactionCollection command);

        int FbExecuteNonQuery(string sql);

        int FbExecuteNonQuery(string sql, List<IDataParameter> parms, CommandType commandType = CommandType.Text);

        void SetCommandTimeOut(int commandTimeOut);

        /// <summary>
        ///     Retruen  FbDataReader  You must Custom Close Data By Db.CloseFbData()
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IDataReader FbExecuteReader(string sql, List<IDataParameter> parms,
            CommandType commandType = CommandType.Text);

        void OpenFbData();

        void CloseFbData();

        void Dispose();

        void BeginTransaction();

        void BeginTransaction(IsolationLevel iso);

        void CommitTransaction();

        void RollBackTransaction();

        /// <summary>
        ///     var para = new List
        ///     <IDataParameter />
        ///     ();
        ///     para.Add(Db.CreateParameterDb("@keyword", "%8%"));
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IDataParameter CreateParameterDb(string parameterName, object value);
    }
}