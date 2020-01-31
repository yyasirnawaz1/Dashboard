using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCDashboard.Models;
using LTCDataManager.Review;
using LTCDataModel.Configurations;
using Microsoft.AspNetCore.Mvc;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.Extensions.Options;
using LTCDataModel.NewsLetter;
using LTCDataManager.NewsLetter;

namespace LTCDashboard.Controllers
{
    public class ReportController : BaseController
    {
        private ConfigSettings _configuration;
        public ReportController(IOptions<ConfigSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }


        public JsonResult LoadReviews(int office_sequence, string startDate, string endDate)
        {
            ReviewViewModel viewModel = new ReviewViewModel();
            var res = gReviewManager.GetAllOfficeReviewLinks(office_sequence);
            viewModel.source = new List<SocialMediaSource>();
            var reviews = gReviewManager.LoadReviews(office_sequence, DateTime.Parse(startDate), DateTime.Parse(endDate));
            viewModel.AverageRatingBySource = new List<AverageRatingBySource>();

            var dates = GetDatesBetween(DateTime.Parse(startDate), DateTime.Parse(endDate));
            foreach (var item in res)
            {
                var rate = new AverageRatingBySource()
                {
                    Review_Link_Name = item.Review_Link_Name,
                    Review_Link_Type = item.Review_Link_Type,
                    currentRating = new List<CurrentRating>(),
                    NewRatingCount = reviews.Count(c => c.ReviewType == item.Review_Link_Type && !string.IsNullOrEmpty(c.ReviewBody))
                };
                foreach (var date in dates)
                {
                    var rateItem = reviews.Where(c => c.ReviewType == item.Review_Link_Type && c.ReviewDate.Date == date.Date && c.Rating != 0);
                    double dailyAverage = 0;
                    if (rateItem.Count() > 0)
                    {
                        dailyAverage = rateItem.Average(a => a.Rating);
                    }
                    var reviewItem = reviews.Where(c => c.ReviewType == item.Review_Link_Type && c.ReviewDate.Date == date.Date && !string.IsNullOrEmpty(c.ReviewBody));
                    var todayRating = 0;
                    if (reviewItem != null)
                        todayRating = reviewItem.Count();

                    rate.currentRating.Add(new CurrentRating()
                    {
                        Date = date.ToString("yyyy-MM-dd"),
                        Rating = dailyAverage,
                        Reviews = todayRating
                    });
                }
                viewModel.AverageRatingBySource.Add(rate);
                var countBeforeDate = gReviewManager.CountBeforeDate(office_sequence, item.Review_Link_Type, DateTime.Parse(startDate));
                if (countBeforeDate < 1)
                    countBeforeDate = 1;

                var currentMonth = gReviewManager.Count(office_sequence, item.Review_Link_Type, DateTime.Parse(startDate), DateTime.Parse(endDate));
                var ratingPercent = (currentMonth / countBeforeDate) * 100;
                viewModel.source.Add(new SocialMediaSource()
                {
                    Review_Link_Name = item.Review_Link_Name,
                    CurrentMonthCount = currentMonth,
                    RatingCount = reviews.Count(r => r.ReviewType == item.Review_Link_Type),
                    RatingPecentage = ratingPercent
                });
            }
            viewModel.reviewResults = new List<ReviewResults>();
            if (reviews != null)
            {
                viewModel.totalReviewRatings = new TotalReviewRatings();
                viewModel.totalReviewRatings.OneStar = reviews.Count(r => r.Rating == 1);
                viewModel.totalReviewRatings.TwoStar = reviews.Count(r => r.Rating == 2);
                viewModel.totalReviewRatings.ThreeStar = reviews.Count(r => r.Rating == 3);
                viewModel.totalReviewRatings.FourStar = reviews.Count(r => r.Rating == 4);
                viewModel.totalReviewRatings.FiveStar = reviews.Count(r => r.Rating == 5);
                viewModel.totalReviewRatings.SumOfRatings = viewModel.totalReviewRatings.OneStar + viewModel.totalReviewRatings.TwoStar
                    + viewModel.totalReviewRatings.ThreeStar + viewModel.totalReviewRatings.FourStar + viewModel.totalReviewRatings.FiveStar;
                var count = reviews.Count(p => p.Rating != 0);
                if (count >= 1)
                {
                    viewModel.AverageRating = reviews.Sum(r => r.Rating) / count;
                    var countBefore = gReviewManager.AverageCountBeforeDate(office_sequence, DateTime.Parse(startDate));
                    if (countBefore < 1)
                        countBefore = 1;
                    viewModel.AverageRatingPercentage = (count / countBefore) * 100;
                }
            }
            foreach (var review in reviews)
            {
                var reviewObj = res.FirstOrDefault(p => p.Review_Link_Type == review.ReviewType);
                if (reviewObj != null)
                    review.ReviewType = reviewObj.Review_Link_Name;

                if (review.Patient_number > -1)
                    review.ReviewerName = review.lastname + " " + review.firstname;

                var result = new ReviewResults() { Date = review.ReviewDate.ToString("yyyy-MM-dd H:mm:ss"), Name = review.ReviewerName, Publisher = review.ReviewType, Rating = review.Rating, Review = review.ReviewBody };
                viewModel.reviewResults.Add(result);

            }

            return Json(new { obj = viewModel });

        }
        public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates;
        }


        #region Newsletter report
         // GET: Reports
        public ActionResult Home()
        {
            @ViewBag.OfficeName = OfficeName;

            return View();
        }

        // GET: Reports
        public ActionResult ScheduledNewsLetters()
        {
              @ViewBag.OfficeName = OfficeName;
            return View();
        }
       
        [HttpGet()]
        public IActionResult Get([DataTablesRequest] DataTablesRequest dataRequest)
        {
            IEnumerable<gPatientCallListView> products = gNewsLetterManager.GetPatientCallList(OfficeSequence).Where(p=>p.Status == 1);
            int recordsTotal = products.Count();
            int recordsFilterd = recordsTotal;

            if (!string.IsNullOrEmpty(dataRequest.Search?.Value))
            {
                products = products.Where(e => e.TemplateTitle.Contains(dataRequest.Search.Value));
                recordsFilterd = products.Count();
            }
            products = products.Skip(dataRequest.Start).Take(dataRequest.Length);


             

            products = products.Skip(dataRequest.Start).Take(dataRequest.Length).ToList();


            return Json(products
                .Select(e => new
                {
                    NewsletterId = e.NewsletterId,
                    Account = e.Account,
                    AppointDate = e.AppointDate,
                    TemplateBodymarkup = e.TemplateBodymarkup,
                    TemplateSourceMarkup = e.TemplateSourceMarkup,
                    TemplateTitle = e.TemplateTitle,
                    Status = e.Status

                })
                .ToDataTablesResponse(dataRequest, recordsTotal, recordsFilterd));
        }

        #endregion
    }
}