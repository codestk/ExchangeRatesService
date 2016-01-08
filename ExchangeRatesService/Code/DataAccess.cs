using CoreDb;

namespace ExchangeService.Code
{
    public class DataAccess
    {
        public DataAccessLayer Db;

        public DataAccess()
        {
            //if (((ConfigurationManager.ConnectionStrings["CFireBird"] == null)))
            //{
            //    throw new System.Exception("Connection Error");
            //}

            if (Config.ConnectionString() == null)
            {
                throw new System.Exception("Connection Error");
            }

            //var connecStionstring = ConfigurationManager.ConnectionStrings["CFireBird"].ToString();
            ////DB_R2 = new DataBaseFireBird(connecStionstring);
            var connecStionstring = Config.ConnectionString();
            Db = new DataBaseSql(connecStionstring);
        }

        //=====================================================================================================

        /// <summary>
        ///     Utility Sort
        /// </summary>
        /// <param name="sortAscending"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public string SetSort(bool sortAscending, string sortExpression)
        {
            var sort = "";
            if (sortExpression == null)
            {
                sort += "";
            }
            else
            {
                sort += string.Format(" order by {0}", sortExpression);
                sort += (sortAscending ? "" : " desc");
            }

            return sort;
        }
    }
}