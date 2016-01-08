using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for P_fxrates_update
/// </summary>
///
namespace ExchangeService
{
    public class P_fxrates_update
    {
        public P_fxrates_update()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string _Day = string.Empty;

        public string Day
        {
            get
            {
                return _Day;
            }
            set
            {
                _Day = value;
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

        private string _Time = string.Empty;

        public string Time
        {
            get
            {
                return _Time;
            }
            set
            {
                _Time = value;
            }
        }
    }
}