using CoreDb;

namespace BusinessLogicLayerTests
{
    public class SetDataAccesLayer
    {
        private IDataAccessLayer _database;

        public SetDataAccesLayer()
        {
            string connectionstring = Config.GetConnectionString();

            _database = new DataBaseSql(connectionstring); //Set Sql Clien  Can Change if fire Bird or oracle

            _database.SetCommandTimeOut(Config.GetCommandTimeOut());//SetCommand TimeOut
        }

        public IDataAccessLayer GetDataAccessLayer()
        {
            return _database;
        }

        ~SetDataAccesLayer()
        {
            _database.Dispose();
            _database = null;
        }
    }
}
