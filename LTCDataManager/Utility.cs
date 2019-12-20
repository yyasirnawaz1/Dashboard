using LTCDataModel.Office;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel;
using LTCDataModel.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SelectPdf;


namespace LTCDataManager
{
    public class Utility
    {
        public static ConfigSettings Config;
        public static List<gDentalProvider> ProviderToList(string[] model)
        {
            List<gDentalProvider> result = new List<gDentalProvider>();
            foreach (var item in model)
            {
                var data = item.Split('_');
                if (data.Length == 2)
                {
                    result.Add(new gDentalProvider
                    {
                        Office_Sequence = Convert.ToInt32(data[0]),
                        Provider = data[1]
                    });
                }
            }
            return result;
        }

        public static string FilterProviderToString(int officeSequence, List<gDentalProvider> model)
        {
            var result = "";

            var officeRecords = model.Where(x => x.Office_Sequence == officeSequence);
            foreach (var item in officeRecords)
            {
                result += "\'" + item.Provider + "\'" + ",";
            }

            result = result.Trim(',');
            return result;
        }

        public static string StringToCharacterString(string originalType, string backupType)
        {
            string result = "";
            if (string.IsNullOrEmpty(originalType))
            {
                result = backupType;
            }
            else
            {
                string[] originalTypeArray = originalType.Split(',');

                foreach (var item in originalTypeArray)
                {
                    if (!string.IsNullOrEmpty(item))
                        result += "\'" + item + "\'";
                }
            }
            return result;
        }

        /// <summary>
        /// This method should cache the authentication_office_list table detail rather than hitting the database on every call, 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<gOfficelist> GetAllowedOffices(int userId)
        {
            try
            {
                var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
                var allowedOffices = db.Fetch<gOfficelist>($"SELECT AOL.Office_Sequence, AOL.Providerrange,AOI.IP_Address, AOI.DB_Name,AOI.DB_Port FROM authentication_office_list AOL LEFT JOIN authentication_office_ip AOI ON AOL.office_sequence = AOI.office_sequence WHERE AOL.UserID ={userId}").ToList();
                return allowedOffices;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string GetConnectionStringByOfficeId(int officeSequence)
        {
            try
            {
                ////TODO: Start Replace this code with [GetAllowedOffices] method as that will get the records from cache in future. 
                //Start 
                var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcGateway);
                var connectionString = db.Fetch<gOfficelist>($"SELECT * FROM authentication_office_ip WHERE Office_sequence = {officeSequence}").FirstOrDefault();
                //END
                if (connectionString != null)
                {
                    return CreateConnectionString(connectionString);
                }
                else
                {
                    return Config.FallbackConnection;//  ConfigurationManager.AppSettings["FallbackConnection"];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string CreateConnectionString(gOfficelist model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.IP_Address))
                {
                    //return $"Server={model.IP_Address};userid=ltcuser;password={System.Configuration.ConfigurationManager.AppSettings["databasePassword"]};database={model.DB_Name};Port={model.DB_Port};Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";
                    return $"Server={model.IP_Address};userid=ltcuser;password={Config.DatabasePassword};database={model.DB_Name};Port={model.DB_Port};Convert Zero Datetime=True;SslMode=none;Connection Timeout=190;";

                }
                else
                {
                    return Config.FallbackConnection;// ConfigurationManager.AppSettings["FallbackConnection"];
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void RemoveDuplicates(ref List<gDentalProvider> listWithoutDuplicate, List<gDentalProvider> listWithDuplicate)
        {
            try
            {
                foreach (var singleProvider in listWithDuplicate)
                {
                    if (listWithoutDuplicate.Count(x => x.Office_Sequence == singleProvider.Office_Sequence && x.Provider == singleProvider.Provider) == 0)
                    {
                        listWithoutDuplicate.Add(singleProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string HygenistType(string chartName)
        {
            var setting = (string)Config.GetType().GetProperty(chartName).GetValue(Config, null);  //ConfigurationManager.AppSettings[chartName];
            if (setting == null)
            {
                return "0,1";
            }
            else if (setting == "*")
            {
                return "0,1";
            }
            else if (setting.ToLower() == "d")
            {
                return "0";
            }
            else if (setting.ToLower() == "h")
            {
                return "1";
            }
            else
            {
                return "0,1";
            }
        }

        public static string GeneratePdf(string content, string filePath)
        {
            #region Create PDF file

            var htmlPage = System.IO.File.ReadAllText(filePath);

            var result = JsonConvert.DeserializeObject<List<gSelectedData>>(content);
            var html = " <form id='rendered-form'><div class='rendered-form'>";
            foreach (var element in result)
            {
                if (element.type == "header")
                    html += "<div class='form-group'><h1>" + element.label + "</h1></div>";
                else if (element.type == "paragraph")
                    html += "<div class='form-group'><p>" + element.label + "</p></div>";

                if (element.userData != null && element.userData[0] != string.Empty)
                {
                    //if (element.className == string.Empty)
                    //{
                    element.className = "form-control";
                    //}

                    if (element.type == "textarea")
                        html += "<div class='form-group'><label>" + element.label + "</label><p>";
                    else
                        html += "<div class='form-group'><label>" + element.label +
                                "</label><input class='form-control' id=" + element.name + " value = '";

                    foreach (var userData in element.userData)
                    {
                        html += userData + " ,";
                    }

                    html = html.Remove(html.Length - 1, 1);
                    if (element.type == "textarea")
                        html += "</p></div>";
                    else
                        html += "' /></div>";
                }
            }

            html += "</div></form>";
            htmlPage = htmlPage.Replace("[Body]", html);

            HtmlToPdf converter = new HtmlToPdf();

            PdfDocument doc = converter.ConvertHtmlString(htmlPage);
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            doc.Close();
            byte[] docBytes = stream.ToArray();
            return Encoding.ASCII.GetString(docBytes);

            #endregion
        }

        public static byte[] GeneratePdfArray(string content, string filePath)
        {
            #region Create PDF file

            var htmlPage = System.IO.File.ReadAllText(filePath);

            var result = JsonConvert.DeserializeObject<List<gSelectedData>>(content);
            var html = " <form id='rendered-form'><div class='rendered-form'>";
            foreach (var element in result)
            {
                if (element.type == "header")
                    html += "<div class='form-group'><h1>" + element.label + "</h1></div>";
                else if (element.type == "paragraph")
                    html += "<div class='form-group'><p>" + element.label + "</p></div>";
                else if (element.type == "button")
                    html += "<div class='form-group'><button type='button' class='btn btn-primary'>" + element.label + "</button><p></div>";
                else if (element.type == "NewLine")
                    html += "<div class='form-group'><br/></div>";
                else if (element.type == "starRating")
                    html += "<div class='form-group'><label>" + element.label + "<label></div>";
                else if (element.type == "LineSeprator")
                    html += "<div class='form-group'><hr/></div>";
                else if (element.type == "file")
                    html += "<div class='form-group'><label>" + element.label + "<label></div>";
                //else if (element.type == "number" || element.type == "text"|| element.type == "textarea"|| element.type == "date")
                //    html += "<div class='form-group'><label>" + element.label + "<label></div>";
                if (element.userData != null && element.userData[0] != string.Empty)
                {
                    //if (element.className == string.Empty)
                    //{
                    element.className = "form-control";
                    //}

                    if (element.type == "textarea")
                        html += "<div class='form-group'><label>" + element.label + "</label><p>";
                    else
                        html += "<div class='form-group'><label>" + element.label +
                                "</label><input class='form-control' id=" + element.name + " value = '";

                    foreach (var userData in element.userData)
                    {
                        html += userData + " ,";
                    }

                    html = html.Remove(html.Length - 1, 1);
                    if (element.type == "textarea")
                        html += "</p></div>";
                    else
                        html += "' /></div>";
                }
                //else if (element.values != null)
                //{
                //    element.className = "form-control";

                //    if (element.type == "textarea")
                //        html += "<div class='form-group'><label>" + element.label + "</label><p>";
                //    else
                //        html += "<div class='form-group'><label>" + element.label +
                //                "</label><input class='form-control' id=" + element.name + " value = ' ";

                //    foreach (var value in element.values)
                //    {
                //        if (value.selected)
                //        {
                //            html += value.value + " ,";
                //        }
                //    }

                //    html = html.Remove(html.Length - 1, 1);
                //    if (element.type == "textarea")
                //        html += "</p></div>";
                //    else
                //        html += "' /></div>";
                //}
            }

            html += "</div></form>";
            htmlPage = htmlPage.Replace("[Body]", html);

            HtmlToPdf converter = new HtmlToPdf();

            PdfDocument doc = converter.ConvertHtmlString(htmlPage);

            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            doc.Close();
            byte[] docBytes = stream.ToArray();


            return docBytes;



            #endregion
        }


    }
}
