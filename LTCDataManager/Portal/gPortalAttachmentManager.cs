using LTCDataManager.DataAccess;
using LTCDataModel.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTCDataManager.Portal
{
    public class gPortalAttachmentManager
    {
        public static gAttachment GetAttachment(string SyncIdentificator)
        {
            gAttachment model;
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental))
            {
                string qry = $"select * from _portal_attachment where SyncIdentificator = '" + SyncIdentificator + "' ;";
                model = db.Fetch<gAttachment>(qry).FirstOrDefault();
            }


            return model;
        }
        public static void UpdateAttachment(gAttachment attachment)
        {
            using (var db = new LTCDataModel.PetaPoco.Database(DbConfiguration.LtcDental))
            {
                string qry = $" Update _portal_attachment Set DateRead = '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm")}' , IsClientRead = 1 where SyncIdentificator = '" + attachment.SyncIdentificator + "' ;";
                db.Execute(qry);
            }
        }
    }
}
