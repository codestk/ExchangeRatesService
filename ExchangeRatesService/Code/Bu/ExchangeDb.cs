using ExchangeService;
using ExchangeService.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace ExchangeRatesService.Code.Bu
{
    public class ExchangeDb : DataAccess
    {
        public P_fxrates_update[] GetDayInMonth(int Year, int Month)
        {
            CultureInfo culture;
            culture = new CultureInfo("en-Us", false);

            var prset = new List<IDataParameter>();
            prset.Add(Db.CreateParameterDb("@Month", Month));
            prset.Add(Db.CreateParameterDb("@Year", Year));
            var ds = Db.GetDataSet("sp_Getfxrates_GetDayInMonth", prset, CommandType.StoredProcedure);

            var query = from temp in ds.Tables[0].AsEnumerable()
                        select new P_fxrates_update
                        {
                            Day = temp.Field<Int32>("DDAYS").ToString(),
                        };

            return query.ToArray();
        }

        public P_fxrates_update[] GetUpdateInMonth(int dd, int mm, int yyyy)
        {
            CultureInfo culture;
            culture = new CultureInfo("en-Us", false);

            var prset = new List<IDataParameter>();
            DateTime FxDatetime = new DateTime(yyyy, mm, dd);
            prset.Add(Db.CreateParameterDb("@FxDatetime", FxDatetime));

            var ds = Db.GetDataSet("sp_GetUpdateInMonth", prset, CommandType.StoredProcedure);

            var query = from temp in ds.Tables[0].AsEnumerable()
                        select new P_fxrates_update
                        {
                            Update = Convert.ToString(temp.Field<decimal>("Update")),
                            Time = temp.Field<string>("Time")
                        };

            return query.ToArray();
        }

        public P_fxrates[] Getfxrates(int dd, int mm, int yyyy, int upd, string Lang)
        {
            CultureInfo culture;
            if (Lang == "Th")
            {
                culture = new System.Globalization.CultureInfo("th-TH", false);
            }
            else
            {
                culture = new System.Globalization.CultureInfo("en-Us", false);
            }

            var prset = new List<IDataParameter>();
            DateTime FxDatetime = new DateTime(yyyy, mm, dd);
            prset.Add(Db.CreateParameterDb("@DDateTime", FxDatetime));
            prset.Add(Db.CreateParameterDb("@Udp", upd));

            var ds = Db.GetDataSet("sp_Getfxrates", prset, CommandType.StoredProcedure);

            var query = from temp in ds.Tables[0].AsEnumerable()
                        select new P_fxrates
                        {
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

        public P_fxrates_update[] GetfxratesLastUpdate()
        {
            CultureInfo culture;
            culture = new CultureInfo("en-Us", false);
            // var query = (from temp in db.sp_GetFxRateLastUpdate()
            var ds = Db.GetDataSet("sp_GetFxRateLastUpdate");
            var query = from temp in ds.Tables[0].AsEnumerable()
                        select new P_fxrates_update
                        {
                            Day = temp.Field<DateTime>("Date").ToString("dd/MM/yyyy", culture),
                            Update = Convert.ToString(temp.Field<decimal>("Update")),
                            Time = temp.Field<string>("Time")
                        };

            return query.ToArray();
        }

        public P_fxrates[] GetChartfxrates(int Fdd, int Fmm, int Fyyyy, int Tdd, int Tmm, int Tyyyy, string Family, string Lang)
        {
            P_fxrates[] output;

            DateTime Fdatetime = new DateTime(Fyyyy, Fmm, Fdd);
            DateTime Tdatetime = new DateTime(Tyyyy, Tmm, Tdd);

            ////fix hack========================================================
            int yearDiff = Tdatetime.Year - Fdatetime.Year;
            yearDiff = Math.Abs(yearDiff);
            if (yearDiff > 4)
            {
                return null;
            }
            ////================================================================

            System.Globalization.CultureInfo culture;
            if (Lang == "Th")
            {
                culture = new System.Globalization.CultureInfo("th-TH", false);
            }
            else
            {
                culture = new System.Globalization.CultureInfo("en-Us", false);
            }

            var prset = new List<IDataParameter>();

            prset.Add(Db.CreateParameterDb("@Fdatetime", Fdatetime));
            prset.Add(Db.CreateParameterDb("@Tdatetime", Tdatetime));
            prset.Add(Db.CreateParameterDb("@Family", Family));

            var ds = Db.GetDataSet("sp_GetChartfxrates", prset, CommandType.StoredProcedure);

            if (Fdatetime == Tdatetime)
            {
                var query = from temp in ds.Tables[0].AsEnumerable()
                            select new P_fxrates
                            {
                                BuyingRates = temp.Field<string>("BuyingRates"),
                                SellingRates = temp.Field<string>("SellingRates"),

                                DTime = temp.Field<string>("Time"),

                                SightBill = temp.Field<string>("SightBill"),

                                Bill_DD_TT = temp.Field<string>("Bill-DD-TT"),
                                TT = temp.Field<string>("TT"),

                                Ddate = temp.Field<DateTime>("Date").ToString("MM/dd/yyyy", culture),
                            };
                output = query.ToArray();
            }
            else
            {
                var query = from temp in ds.Tables[0].AsEnumerable()
                            select new P_fxrates
                            {
                                BuyingRates = temp.Field<string>("BuyingRates"),
                                SellingRates = temp.Field<string>("SellingRates"),

                                // DTime = temp.Field<string>("Time"),

                                SightBill = temp.Field<string>("SightBill"),

                                Bill_DD_TT = temp.Field<string>("Bill-DD-TT"),
                                TT = temp.Field<string>("TT"),

                                Ddate = temp.Field<DateTime>("Date").ToString("MM/dd/yyyy", culture),
                            };
                output = query.ToArray();
            }

            return output;
        }

        public P_fxrates[] GetDownloadfxrates(int Fdd, int Fmm, int Fyyyy, int Tdd, int Tmm, int Tyyyy, string Family, string Lang)
        {
            DateTime Fdatetime = new DateTime(Fyyyy, Fmm, Fdd);
            DateTime Tdatetime = new DateTime(Tyyyy, Tmm, Tdd);

            //fix hack========================================================
            int yearDiff = Tdatetime.Year - Fdatetime.Year;
            yearDiff = Math.Abs(yearDiff);
            if (yearDiff > 4)
            {
                return null;
            }
            //================================================================

            System.Globalization.CultureInfo culture;
            if (Lang == "Th")
            {
                culture = new System.Globalization.CultureInfo("th-TH", false);
            }
            else
            {
                culture = new System.Globalization.CultureInfo("en-Us", false);
            }

            var prset = new List<IDataParameter>();

            prset.Add(Db.CreateParameterDb("@Fdatetime", Fdatetime));
            prset.Add(Db.CreateParameterDb("@Tdatetime", Tdatetime));
            prset.Add(Db.CreateParameterDb("@Family", Family));

            var ds = Db.GetDataSet("sp_Getfxrates_download", prset, CommandType.StoredProcedure);

            var query = from temp in ds.Tables[0].AsEnumerable()
                        select new P_fxrates
                        {
                            BuyingRates = temp.Field<string>("BuyingRates"),
                            SellingRates = temp.Field<string>("SellingRates"),

                            // DTime = temp.Field<string>("Time"),

                            SightBill = temp.Field<string>("SightBill"),

                            Bill_DD_TT = temp.Field<string>("Bill-DD-TT"),
                            TT = temp.Field<string>("TT"),

                            Ddate = temp.Field<DateTime>("Date").ToString("MM/dd/yyyy", culture),
                        };

            //var query = (from temp in db.sp_Getfxrates_download(Fdatetime, Tdatetime, Family)

            //             select new P_fxrates
            //             {
            //                 BuyingRates = temp.BuyingRates,
            //                 SellingRates = temp.SellingRates,
            //                 DTime = temp.Time,

            //                 SightBill = temp.SightBill,
            //                 Family = temp.Family,
            //                 Bill_DD_TT = temp.Bill_DD_TT,
            //                 TT = temp.TT,

            //                 Ddate = temp.Date.Value.ToString("MM/dd/yyyy", culture)
            //             });

            return query.ToArray<P_fxrates>();

            //SetToAppPool();
            //DataClassesDataContext db = new DataClassesDataContext();
            //DateTime Fdatetime = new DateTime(Fyyyy, Fmm, Fdd);
            //DateTime Tdatetime = new DateTime(Tyyyy, Tmm, Tdd);

            ////fix hack========================================================
            //int yearDiff = Tdatetime.Year - Fdatetime.Year;
            //yearDiff = Math.Abs(yearDiff);
            //if (yearDiff > 4)
            //{
            //    return null;
            //}
            ////================================================================

            //System.Globalization.CultureInfo culture;
            //if (Lang == "Th")
            //{
            //    culture = new System.Globalization.CultureInfo("th-TH", false);
            //}
            //else
            //{
            //    culture = new System.Globalization.CultureInfo("en-Us", false);
            //}

            //var query = (from temp in db.sp_Getfxrates_download(Fdatetime, Tdatetime, Family)

            //             select new P_fxrates
            //             {
            //                 BuyingRates = temp.BuyingRates,
            //                 SellingRates = temp.SellingRates,
            //                 DTime = temp.Time,

            //                 SightBill = temp.SightBill,
            //                 Family = temp.Family,
            //                 Bill_DD_TT = temp.Bill_DD_TT,
            //                 TT = temp.TT,

            //                 Ddate = temp.Date.Value.ToString("MM/dd/yyyy", culture)
            //             });
            //RevertUSer();
            //return query.ToArray<P_fxrates>();
        }

        #region Cache

        public P_fxrates[] GetLatestfxrates()
        {
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

            //if (Context.Cache["GetLatestfxrates"] == null)
            //{
            //    SetToAppPool();
            //    DataClassesDataContext db = new DataClassesDataContext();
            //    System.Globalization.CultureInfo culture;
            //    culture = new System.Globalization.CultureInfo("en-Us", false);
            //    P_fxrates_update[] fxrates_update = GetfxratesLastUpdate();
            //    string[] dt;
            //    int? upd;
            //    dt = fxrates_update[0].Day.Split('/');
            //    upd = Convert.ToInt32(fxrates_update[0].Update.ToString());
            //    int yyyy, mm, dd;
            //    yyyy = Convert.ToInt32(dt[2]);
            //    mm = Convert.ToInt32(dt[1]);
            //    dd = Convert.ToInt32(dt[0]);
            //    DateTime datetime = new DateTime(yyyy, mm, dd);
            //    var query = (from temp in db.sp_Getfxrates(datetime, upd)
            //                 select new P_fxrates
            //                 {
            //                     ID = temp.ID.Value.ToString(),
            //                     Description = temp.Description,
            //                     BuyingRates = temp.BuyingRates,
            //                     SellingRates = temp.SellingRates,
            //                     SightBill = temp.SightBill,
            //                     Family = temp.Family.Trim(),
            //                     FamilyLong = temp.FamilyLong,
            //                     Bill_DD_TT = temp.Bill_DD_TT,
            //                     TT = temp.TT,
            //                     Update = temp.Update.Value.ToString(),
            //                     Ddate = temp.Date.Value.ToString("d/MM/yyyy", culture),
            //                     DTime = temp.Time
            //                 });
            //    RevertUSer();
            //    Context.Cache.Insert("GetLatestfxrates", query.ToArray<P_fxrates>(), null, DateTime.Now.AddSeconds(CacheTime), TimeSpan.Zero);
            //}
            //return (P_fxrates[])Context.Cache["GetLatestfxrates"];
        }

        public P_fxfamily[] Getfxfamily()
        {
            CultureInfo culture;
            culture = new CultureInfo("en-Us", false);
            // var query = (from temp in db.sp_GetFxRateLastUpdate()
            //var ds = Db.GetDataSet("sp_Getfxrates_GetDayInMonth");

            var ds = Db.GetDataSet("sp_Getfamily");

            var query = from temp in ds.Tables[0].AsEnumerable()
                        select new P_fxfamily
                        {
                            Family = temp.Field<string>("Family"),
                            Description = temp.Field<string>("Description"),
                            DescriptionCn = temp.Field<string>("DescriptionCn"),
                        };

            // Context.Cache.Insert("GetfxratesLastUpdate", query.ToArray<P_fxrates_update>(), null, DateTime.Now.AddSeconds(CacheTime), TimeSpan.Zero);

            return query.ToArray();

            //if (Context.Cache["Getfxfamily"] == null)
            //{
            //    SetToAppPool();
            //    DataClassesDataContext db = new DataClassesDataContext();
            //    var query = (from temp in db.sp_Getfamily()
            //                 select new P_fxfamily
            //                 {
            //                     Family = temp.Family,
            //                     Description = temp.Description,
            //                     DescriptionCn = temp.DescriptionCn
            //                 });
            //    RevertUSer();
            //    Context.Cache.Insert("Getfxfamily", query.ToArray<P_fxfamily>(), null, DateTime.Now.AddSeconds(CacheTime), TimeSpan.Zero);
            //}
            //return (P_fxfamily[])Context.Cache["Getfxfamily"];
        }

        #endregion Cache
    }
}