using System;
using System.Configuration;
using DbUtility;

namespace BusinessLogicLayerTests
{
    public class Config
    {
 
            public static DbConfig GetDbConfig()
            {

                return new DbConfig("ConnectionString");
            }
        
    }
}
