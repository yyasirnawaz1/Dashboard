using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LTC_Covid.Helper
{
    public static class Common 
    {
        public static string GenerateAPIKey()
        {
            int Length = 20;
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ~!@#$%^&*()_+";
            Random randNum = new Random();
            char[] chars = new char[Length];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < Length; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return new string(chars);


        }
        public static string GenerateCustomID()
        {
            int Length = 10;
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ~!@#$%^&*()_+";
            Random randNum = new Random();
            char[] chars = new char[Length];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < Length; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return DateTime.Now.ToString("yyMMddHHmmss") + new string(chars);


        }
        //public string GetUserConnectionString()
        //{
        //    try
        //    {

        //        HttpCookie faCookie = Request.Cookies["ltcDentalCookie"];

        //        var NewsletterConnectionString = faCookie.Value;
        //        var formAuth = FormsAuthentication.Decrypt(NewsletterConnectionString);

        //        var data = JsonConvert.DeserializeObject<CustomSerializeModel>(formAuth.UserData);
        //        return data.connectionString;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}
    }
}