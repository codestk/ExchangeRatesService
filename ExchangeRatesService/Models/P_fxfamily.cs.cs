using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for P_fxfamily
/// </summary>
namespace ExchangeService
{
    public class P_fxfamily
    {
        public P_fxfamily()
        {
            //
            // TODO: Add constructor logic here
            //
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

        private string _Description = string.Empty;

        public string Description
        {
            get
            {
                // return NewConstraint(_Description);
                return (_Description);
            }
            set
            {
                _Description = value;
            }
        }

        private string _DescriptionCn = string.Empty;

        public string DescriptionCn
        {
            get
            {
                // return NewConstraint(_Description);
                return (_DescriptionCn);
            }
            set
            {
                _DescriptionCn = value;
            }
        }

        private string NewConstraint(string fDescription)
        {
            string Out_Put = "";
            switch (Family.Trim())
            {
                case "USD1":
                    Out_Put = "US Dollar 1-2";
                    break;

                case "USD5":
                    Out_Put = "US Dollar 5-20";
                    break;

                case "USD50":
                    Out_Put = "US Dollar 50-100";
                    break;

                default:
                    Out_Put = fDescription;
                    // Description = strValue;
                    break;
            }

            return Out_Put;
        }
    }
}