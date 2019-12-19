using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCDataManager.DataAccess;
using LTCDataModel.Configurations;
using LTCDataModel.Dashboard;
using Microsoft.Extensions.Options;

namespace LTCDataManager.Dashboard
{
    public class gDashboardChartManager
    {
        private readonly ConfigSettings _configuration;

        public gDashboardChartManager(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
            Utility.Config = configuration.Value; ;
        }

        public static List<gCharts> GetAllChartsByPageName(int officeId, int userId, string pageName)
        {
            var allowedCharts = new List<gCharts>();

            var db = PocoDatabase.DbConnection(DbConfiguration.LtcDashboard);
            var model = db.Fetch<gCharts>($"SELECT * FROM charts where page_name='{pageName}';").ToList();
            var permissions = GetChartPermissions(officeId, userId);

            var permissionLevel = 1;
            if (permissions != null)
            {
                if (permissions.Permission_Level.HasValue)
                {
                    permissionLevel = permissions.Permission_Level.Value;
                }

                if (permissions.Group_Permission_Level.HasValue)
                {
                    permissionLevel = permissions.Group_Permission_Level.Value;
                }
            }

            foreach (var item in model)
            {
                if (item.Required_Permission_Level >= permissionLevel)
                {
                    allowedCharts.Add(item);
                }
            }

            return allowedCharts;
 }

        public static gChartPermission GetChartPermissions(int officeId, int userId)
        {
            var db = PocoDatabase.DbConnection(DbConfiguration.LtcDashboard);
            return db.Fetch<gChartPermission>($"SELECT CP.*, AG.Name 'Permission_Group_Name', AG.Permission_Level as 'Group_Permission_Level' FROM chart_permissions CP LEFT JOIN authentication_groups AG on CP.Permission_Group = AG.ID WHERE CP.Id = {userId} AND CP.Office_Sequence = {officeId}").FirstOrDefault();

            ////get permission based on user id/groupid/officeid
            //var model = new List<gDashboardChartPermission>();
            //model.Add(new gDashboardChartPermission { ID = 1, ChartName = "NewPatient", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 2, ChartName = "TotalNetProduction", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 3, ChartName = "TotalNetRecareProduction", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 4, ChartName = "TotalNetTreatmentProduction", ChartLevel = 1 });

            //model.Add(new gDashboardChartPermission { ID = 5, ChartName = "RecareReappointmentPassive", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 6, ChartName = "RecareReappointmentActive", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 7, ChartName = "TreatmentAcceptance", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 8, ChartName = "TreatmentScheduled", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 9, ChartName = "CancellationAndNoShows", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 10, ChartName = "UnscheduledTreatment", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 11, ChartName = "UnscheduledRecareActivePatient", ChartLevel = 1 });
            //model.Add(new gDashboardChartPermission { ID = 12, ChartName = "NetCollection", ChartLevel = 1 });
            //return model;
        }

        public static List<gUserCharts> GetUserCharts(int officeId, int userId)
        {
            var db = PocoDatabase.DbConnection(DbConfiguration.LtcDashboard);
            return db.Fetch<gUserCharts>($"SELECT * FROM user_charts WHERE User_Id = {userId}").ToList();
        }

        public static void AddUserChart(int chartId, int userId, int officeId)
        {
            using (var db = PocoDatabase.DbConnection(DbConfiguration.LtcDashboard))
            {
                var model = db.Fetch<gUserCharts>($"SELECT * FROM user_charts WHERE User_Id = {userId} AND Chart_Id={chartId}").FirstOrDefault();
                if (model == null)
                {
                    gUserCharts userChart = new gUserCharts();
                    userChart.User_Id = userId;
                    userChart.Chart_Id = chartId;
                    db.Save(userChart);
                }
            }
        }

        public static void DeleteChart(int chartId, int userId, int officeId)
        {
            using (var db = PocoDatabase.DbConnection(DbConfiguration.LtcDashboard))
            {
                //db.Delete("user_charts", "ID", new gUserCharts { User_Id = userId, Chart_Id = chartId }, null);
                db.Execute($"SET SQL_SAFE_UPDATES = 0; DELETE FROM user_charts WHERE User_Id = {userId} AND Chart_Id={chartId}; SET SQL_SAFE_UPDATES = 1;");
            }
        }

    }
}
