using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace ExchangeService.Code
{
    public class ExchnageLinq
    {
        public P_fxrates[] GetLatestfxrates()
        {
            var db = new DataClasses1DataContext();
            CultureInfo culture;
            culture = new CultureInfo("en-Us", false);
            var fxrates_update = GetfxratesLastUpdate();
            string[] dt;
            int? upd;
            dt = fxrates_update[0].Day.Split('/');
            upd = Convert.ToInt32(fxrates_update[0].Update);
            int yyyy, mm, dd;
            yyyy = Convert.ToInt32(dt[2]);
            mm = Convert.ToInt32(dt[1]);
            dd = Convert.ToInt32(dt[0]);
            var datetime = new DateTime(yyyy, mm, dd);
            var query = from temp in db.sp_Getfxrates(datetime, upd)
                        select new P_fxrates
                        {
                            ID = temp.ID.Value.ToString(),
                            Description = temp.Description,
                            BuyingRates = temp.BuyingRates,
                            SellingRates = temp.SellingRates,
                            SightBill = temp.SightBill,
                            Family = temp.Family.Trim(),
                            FamilyLong = temp.FamilyLong,
                            Bill_DD_TT = temp.Bill_DD_TT,
                            TT = temp.TT,
                            Update = temp.Update.Value.ToString(),
                            Ddate = temp.Date.Value.ToString("d/MM/yyyy", culture),
                            DTime = temp.Time
                        };

            return query.ToArray();
        }

        public P_fxrates_update[] GetfxratesLastUpdate()
        {
            var db = new DataClasses1DataContext();
            CultureInfo culture;
            culture = new CultureInfo("en-Us", false);
            // var query = (from temp in db.sp_GetFxRateLastUpdate()
            var query = from temp in db.sp_GetFxRateLastUpdate()
                        select new P_fxrates_update
                        {
                            Day = temp.Date.Value.ToString("dd/MM/yyyy", culture),
                            Update = Convert.ToString(temp.Update),
                            Time = temp.Time
                        };

            // Context.Cache.Insert("GetfxratesLastUpdate", query.ToArray<P_fxrates_update>(), null, DateTime.Now.AddSeconds(CacheTime), TimeSpan.Zero);

            return query.ToArray();
            //return query.ToArray<P_fxrates_update>();
        }
    }
}