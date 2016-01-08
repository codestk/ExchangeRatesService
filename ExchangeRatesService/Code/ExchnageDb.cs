using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace ExchangeService.Code
{
    public class ExchangeDb : DataAccess
    {
        public ExchangeDb()
        {
        }

        public P_fxrates_update[] GetfxratesLastUpdateOLE()
        {
            CultureInfo culture;
            culture = new CultureInfo("en-Us", false);
            // var query = (from temp in db.sp_GetFxRateLastUpdate()
            var ds = Db.GetDataSet("sp_GetFxRateLastUpdate");
            var query = from temp in ds.Tables[0].AsEnumerable()
                        select new P_fxrates_update
                        {
                            //Day = temp.Date.Value.ToString("dd/MM/yyyy", culture),
                            //Update = Convert.ToString(temp.Update),
                            //Time = temp.Time

                            Day = temp.Field<DateTime>("Date").ToString("dd/MM/yyyy", culture),
                            Update = Convert.ToString(temp.Field<decimal>("Update")),
                            Time = temp.Field<string>("Time")
                        };

            // Context.Cache.Insert("GetfxratesLastUpdate", query.ToArray<P_fxrates_update>(), null, DateTime.Now.AddSeconds(CacheTime), TimeSpan.Zero);

            return query.ToArray();
            //return query.ToArray<P_fxrates_update>();
        }

        public P_fxrates[] GetLatestfxratesOLE()
        {
            CultureInfo culture;
            culture = new CultureInfo("en-Us", false);
            var fxrates_update = GetfxratesLastUpdateOLE();
            string[] dt;
            int? upd;
            dt = fxrates_update[0].Day.Split('/');
            upd = Convert.ToInt32(fxrates_update[0].Update);
            int yyyy, mm, dd;
            yyyy = Convert.ToInt32(dt[2]);
            mm = Convert.ToInt32(dt[1]);
            dd = Convert.ToInt32(dt[0]);
            var datetime = new DateTime(yyyy, mm, dd);

            var prset = new List<IDataParameter>();
            prset.Add(Db.CreateParameterDb("@DDateTime", datetime));
            prset.Add(Db.CreateParameterDb("@Udp", upd));
            var ds = Db.GetDataSet("sp_Getfxrates", prset, CommandType.StoredProcedure);

            var query = from temp in ds.Tables[0].AsEnumerable()
                        select new P_fxrates
                        {
                            //ID = temp.ID.Value.ToString(),
                            //Description = temp.Description,
                            //BuyingRates = temp.BuyingRates,
                            //SellingRates = temp.SellingRates,
                            //SightBill = temp.SightBill,
                            //Family = temp.Family.Trim(),
                            //FamilyLong = temp.FamilyLong,
                            //Bill_DD_TT = temp.Bill_DD_TT,
                            //TT = temp.TT,
                            //Update = temp.Update.Value.ToString(),
                            //Ddate = temp.Date.Value.ToString("d/MM/yyyy", culture),
                            //DTime = temp.Time

                            ID = temp.Field<int>("ID").ToString(),
                            Description = temp.Field<string>("Description"),
                            BuyingRates = temp.Field<string>("BuyingRates"),
                            SellingRates = temp.Field<string>("SellingRates"),
                            SightBill = temp.Field<string>("SightBill"),
                            Family = temp.Field<string>("Family"),
                            FamilyLong = temp.Field<string>("FamilyLong"),
                            Bill_DD_TT = temp.Field<string>("Bill-DD-TT"),
                            TT = temp.Field<string>("TT"),
                            Update = temp.Field<object>("Update").ToString(),
                            Ddate = temp.Field<DateTime>("Date").ToString("d/MM/yyyy", culture),
                            DTime = temp.Field<object>("Time").ToString()
                        };

            return query.ToArray();
        }
    }
}