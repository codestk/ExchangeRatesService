using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for P_fxrates
/// </summary>

namespace ExchangeService
{
    public class P_fxrates
    {
        public P_fxrates()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string _ID = string.Empty;

        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        private string _Description = string.Empty;

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        private string _Family = string.Empty;

        public string Family
        {
            get
            {
                return _Family;
            }
            set
            {
                _Family = value;
            }
        }

        private string _FamilyLong = string.Empty;

        public string FamilyLong
        {
            get
            {
                return _FamilyLong;
            }
            set
            {
                _FamilyLong = value;
            }
        }

        private string _BuyingRates = string.Empty;

        public string BuyingRates
        {
            get
            {
                return _BuyingRates;
            }
            set
            {
                _BuyingRates = value;
            }
        }

        private string _SellingRates = string.Empty;

        public string SellingRates
        {
            get
            {
                return _SellingRates;
            }
            set
            {
                _SellingRates = value;
            }
        }

        private string _SightBill = string.Empty;

        public string SightBill
        {
            get
            {
                if ((Family.Trim() == "USD1") || ((Family.Trim() == "USD5")))
                {
                    return "";
                }
                return _SightBill;
            }
            set
            {
                _SightBill = value;
            }
        }

        private string _Bill_DD_TT = string.Empty;

        public string Bill_DD_TT
        {
            get
            {
                if ((Family.Trim() == "USD1") || ((Family.Trim() == "USD5")))
                {
                    return "";
                }
                return _Bill_DD_TT;
            }
            set
            {
                _Bill_DD_TT = value;
            }
        }

        private string _TT = string.Empty;

        public string TT
        {
            get
            {
                if ((Family.Trim() == "USD1") || ((Family.Trim() == "USD5")))
                {
                    return "";
                }
                return _TT;
            }
            set
            {
                _TT = value;
            }
        }

        private string _Ddate = string.Empty;

        public string Ddate
        {
            get
            {
                return _Ddate;
            }
            set
            {
                _Ddate = value;
            }
        }

        private string _Update = string.Empty;

        public string Update
        {
            get
            {
                return _Update;
            }
            set
            {
                _Update = value;
            }
        }

        private string _DTime = string.Empty;

        public string DTime
        {
            get
            {
                return _DTime;
            }
            set
            {
                _DTime = value;
            }
        }
    }
}