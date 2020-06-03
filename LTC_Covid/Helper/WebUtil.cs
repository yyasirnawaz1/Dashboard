
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTCDataModel.Helper
{
    public class WebUtil
    {
        
        public static string GetFormattedDate(object date)
        {
            string result = string.Empty;
            try
            {
                result = Convert.ToDateTime(date).ToString("dd/MM/yyyy");

                if (result == DateTime.MinValue.ToString("dd/MM/yyyy"))
                {
                    return string.Empty;
                }
                return Convert.ToDateTime(date).ToString("dd/MM/yyyy");

            }
            catch
            {


            }
            return result;
        }

        public static string GetFormattedTime(object date)
        {
            string result = string.Empty;
            try
            {

                return Convert.ToDateTime(date).ToString("hh:mm tt");
            }
            catch
            {


            }
            return result;
        }

        public static string GetFormattedDateWithTime(object date)
        {
            string result = string.Empty;
            try
            {

                return Convert.ToDateTime(date).ToString("dd/MM/yyyy hh:mm:ss tt");
            }
            catch
            {


            }
            return result;
        }

        public static string GetDateFormatWithDayMonthTime(object date)
        {
            string result = string.Empty;
            try
            {
                if (date != null)
                    return Convert.ToDateTime(date).ToString("dddd, mmmm dd, yyyy hh:mm:ss tt");
            }
            catch
            {


            }
            return result;
        }



    }
}