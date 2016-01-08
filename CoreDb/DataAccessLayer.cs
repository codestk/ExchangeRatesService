using CoreDb.Collection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace CoreDb
{
    public abstract class DataAccessLayer : IDataAccessLayer
    {
        #region ExecuteReader

        /// <summary>
        ///     Retruen  FbDataReader  You must Custom Close Data By Db.CloseFbData()
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IDataReader FbExecuteReader(string sql, List<IDataParameter> parms,
            CommandType commandType = CommandType.Text)
        {
            OpenFbData();

            //Using not work with transaction
            Com = GetCommandDb(sql, commandType);

            SetParameter(parms, ref Com);

            IDataReader fbrd = Com.ExecuteReader();

            return fbrd;
        }

        #endregion ExecuteReader

        #region Valiable

        protected static DbConnection ConTransaction; //Share For Transaction
        protected static DbTransaction Tran; //Sharde For Transaction Temp
        protected DbDataAdapter Adapter;
        protected DbCommand Com;
        protected int CommandTimeout;
        protected DbConnection Con;
        protected string Constr;

        #endregion Valiable

        #region Constructors

        protected DataAccessLayer()
        {
            CommandTimeout = 120;
        }

        protected DataAccessLayer(string connectionString)
        {
            CommandTimeout = 120;
            Com = null;
            Adapter = null;
            Constr = connectionString;
        }

        #endregion Constructors

        #region DataSet

        public DataSet GetDataSet(string sql)
        {
            // Description: fill DataSet via OleDbDataAdapter, connect Firebird, Interbase
            using (var ds = new DataSet())
            {
                try
                {
                    OpenFbData();

                    //Using not work with transaction
                    Com = GetCommandDb(sql);

                    Adapter = GetAdapterDb(Com);

                    Adapter.Fill(ds);
                }
                finally
                {
                    ReleaseResource();
                }

                return ds;
            }
        }

        public virtual DataSet GetDataSet(string sql, List<IDataParameter> parms,
            CommandType commandType = CommandType.Text)
        {
            // Description: fill DataSet via OleDbDataAdapter, connect Firebird, Interbase
            using (var ds = new DataSet())
            {
                try
                {
                    OpenFbData();

                    Com = GetCommandDb(sql, commandType);

                    SetParameter(parms, ref Com);

                    Adapter = GetAdapterDb(Com);

                    Adapter.Fill(ds);
                }
                finally
                {
                    ReleaseResource();
                }

                return ds;
            }
        }

        #endregion DataSet

        #region ExecuteScalar

        public string FbExecuteScalar(string sql)
        {
            string output;

            try
            {
                OpenFbData();

                //Usirng detroy value of time out
                Com = GetCommandDb(sql);

                output = Convert.ToString(Com.ExecuteScalar());
            }
            finally
            {
                ReleaseResource();
            }

            return output;
        }

        public string FbExecuteScalar(string sql, List<IDataParameter> parms)
        {
            string output;

            try
            {
                OpenFbData();

                //Using not work wit transaction
                Com = GetCommandDb(sql);

                SetParameter(parms, ref Com);

                output = Com.ExecuteScalar().ToString();
            }
            finally
            {
                ReleaseResource();
            }

            return output;
        }

        #endregion ExecuteScalar

        #region ExecuteNonQuery

        public int FbExecuteNonQuery(string sql)
        {
            // Description: ExecuteScalar - gets a single value. Firebird, Interbase .Net provider (c#)

            int affectRow;

            try
            {
                OpenFbData();

                Com = GetCommandDb(sql);

                affectRow = Com.ExecuteNonQuery();
            }
            finally
            {
                ReleaseResource();
            }

            return affectRow;
        }

        public int FbExecuteNonQuery(string sql, List<IDataParameter> parms, CommandType commandType = CommandType.Text)
        {
            int affectRow;
            try
            {
                OpenFbData();

                Com = GetCommandDb(sql, commandType);

                SetParameter(parms, ref Com);

                affectRow = Com.ExecuteNonQuery();
            }
            finally
            {
                ReleaseResource();
            }

            //Command Can Effect one or more rows.
            return affectRow;
        }

        public int FbExecuteNonQuery(TransactionCollection command)
        {
            var rowAffect = 0;
            try
            {
                OpenFbData();

                foreach (Transaction t in command)
                {
                    Com = GetCommandDb(t.SqlCommand, t.ExecuteType);

                    if (t.Parameter != null)
                    {
                        SetParameter(t.Parameter, ref Com);
                    }

                    rowAffect += Com.ExecuteNonQuery();
                }
            }
            finally
            {
                ReleaseResource();
            }

            return rowAffect;
        }

        #endregion ExecuteNonQuery

        //~DataAccessLayer()

        #region Transactions

        public virtual void BeginTransaction()
        {
            Tran = Con.BeginTransaction();
        }

        //Must OpenFbData
        //    ReadCommitted	Shared locks are held while the data is being read to avoid dirty reads, but the data can be changed before the end of the transaction, resulting in non-repeatable reads or phantom data.
        //    ReadUncommitted	A dirty read is possible, meaning that no shared locks are issued and no exclusive locks are honored.
        //    RepeatableRead	Locks are placed on all data that is used in a query, preventing other users from updating the data. Prevents non-repeatable reads but phantom rows are still possible.
        //    Serializable	A range lock is placed on the DataSet, preventing other users from updating or inserting rows into the dataset until the transaction is complete.
        //    Snapshot	Reduces blocking by storing a version of data that one application can read while another is modifying the same data. Indicates that from one transaction you cannot see changes made in other transactions, even if you requery.
        //    Unspecified	A different isolation level than the one specified is being used, but the level cannot be determined.
        //When using OdbcTransaction, if you do not set IsolationLevel or you set IsolationLevel to Unspecified, the transaction executes according to the isolation level that is determined by the driver that is being used.
        //
        /// Chaos	The pending changes from more highly isolated transactions cannot be overwritten.
        /// <param name="isolevel"></param>
        public virtual void BeginTransaction(IsolationLevel isolevel)
        {
            //OpenFbData();

            Tran = Con.BeginTransaction(isolevel);
        }

        public virtual void CommitTransaction()
        {
            Tran.Commit();
            Tran = null;
        }

        public virtual void RollBackTransaction()
        {
            //if  (Tran != null)
            Tran.Rollback();
            Tran = null;
        }

        #endregion Transactions

        #region Common

        public int SetCommandTimeout
        {
            get { return CommandTimeout; }
            set { CommandTimeout = value; }
        }

        public virtual void OpenFbData()
        {
            Con = GetConnectionDb();
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
        }

        public virtual void CloseFbData()
        {
            if (Con == null) //Fix Close non open data
                return;

            //in case Open Tran Section
            if (Tran != null)
            {
                return;
            }

            if (Con.State == ConnectionState.Open)
            {
                Con.Close(); // Close  _conTransaction
            }
        }

        public void Dispose()
        {
            DisposeAll();
        }

        public IDataParameter CreateParameterDb(string parameterName, object value)
        {
            return GetParameterDb(parameterName, value);
        }

        public void SetCommandTimeOut(int commandTimeout)
        {
            CommandTimeout = commandTimeout;
        }

        private void DisposeAll()
        {
            //Prevent Dispos if have transaction
            if (Tran?.Connection != null)
            {
            }

            //if (Tran != null)
            //{
            //    Tran.Dispose();
            //    Tran = null;
            //}

            //if (Com != null)
            //{
            //    Com.Dispose();
            //    Com = null;
            //}

            //if (Con != null)
            //{
            //    Con.Dispose();
            //    Con = null;
            //}

            //if (Adapter != null)
            //{
            //    Adapter.Dispose();
            //    Adapter = null;
            //}
        }

        private void SetParameter(IEnumerable<IDataParameter> parms, ref DbCommand com)
        {
            com.Parameters.Clear();
            foreach (var p in parms)
            {
                com.Parameters.Add(p);
            }
        }

        private void ReleaseResource()
        {
            CloseFbData();
            DisposeAll();
        }

        #endregion Common

        #region For Implement

        protected abstract DbCommand GetCommandDb(string sql, CommandType commandType = CommandType.Text);

        protected abstract DbDataAdapter GetAdapterDb(DbCommand com);

        protected abstract DbConnection GetConnectionDb();

        protected abstract IDataParameter GetParameterDb(string parameterName, object value);

        #endregion For Implement
    }
}