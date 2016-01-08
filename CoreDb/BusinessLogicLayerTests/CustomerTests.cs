using System;
using System.Diagnostics;
using BusinessLogicLayer;
using DbUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
 
namespace BusinessLogicLayerTests
{
    [TestClass()]
    public class CustomerTests
    {
        [TestMethod()]
        public void TestSelectWitTransaction2Instance()
        {
            Console.WriteLine("Start");
            Debug.WriteLine(DateTime.Now.ToString( ));
            try
            {
              //var db = new SetDataAccesLayer();//fails
                for (int i = 1; i < 2; i++)
                {


                    var dbconfig = Config.GetDbConfig();
                    var cs = new Customer(DbFactory.GetDataAccessLayer(dbconfig));
                    var cs2 = new Customer(DbFactory.GetDataAccessLayer(dbconfig));
                    //cs2.GetCustomerProfile();
                    //cs.SelectTransaction();



                    Console.WriteLine("Begin Round : " + i.ToString() + " " + DateTime.Now.ToString());
                    cs2.GetCustomerProfile();
                    cs.SelectTransaction();
                    cs.UpdateTransaction();
                    cs.GetCustomerProfile();
                    cs2.SelectDataApadter();

                    //cs.SelectDataApadter();
                    //cs2.SelectDataApadter();


                    //Thread thread1 = new Thread(new ThreadStart(cs.SelectDataApadter));
                    //Thread thread2 = new Thread(new ThreadStart(cs2.SelectDataApadter));
                    //thread1.Start();
                    //thread2.Start();
                    //thread1.Join();
                    //thread2.Join();
                    Console.WriteLine("End Round : " + i.ToString() + " " + DateTime.Now.ToString());
                }
            }
             catch (Exception ex)
             {

                 Debug.WriteLine(ex.ToString());
                 //Console.WriteLine(ex.ToString());

             }


           // Assert.Fail();
        }


        [TestMethod()]
        public void InsertAutoIdTransaction()
        {
            Console.WriteLine("Start");
            Debug.WriteLine(DateTime.Now.ToString());
            try
            {
                //var db = new SetDataAccesLayer();//fails
                for (int i = 1; i < 2; i++)
                {


                    var dbconfig = Config.GetDbConfig();
                    var cs = new Customer(DbFactory.GetDataAccessLayer(dbconfig));
                    var cs2 = new Customer(DbFactory.GetDataAccessLayer(dbconfig));
                    //cs2.GetCustomerProfile();
                    //cs.SelectTransaction();



                    Console.WriteLine("Begin Round : " + i.ToString() + " " + DateTime.Now.ToString());
                    cs2.GetCustomerProfile();
                    cs.SelectTransaction();
                    cs.UpdateTransaction();
                    cs.GetCustomerProfile();
                    cs2.SelectDataApadter();

                    //cs.SelectDataApadter();
                    //cs2.SelectDataApadter();


                 
                    Console.WriteLine("End Round : " + i.ToString() + " " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());
                 

            }


            // Assert.Fail();
        }

    }
}
