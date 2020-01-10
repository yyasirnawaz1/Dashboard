using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTCDashboard.Models
{
    public class ReviewViewModel
    {
        public List<SocialMediaSource> source { get; set; }
        public List<ReviewResults> reviewResults { get; set; }
        public TotalReviewRatings totalReviewRatings { get; set; }
        public decimal AverageRating { get; set; }
        public int AverageRatingPercentage { get; set; }
        public List<AverageRatingBySource> AverageRatingBySource { get; set; }

    }
    public class SocialMediaSource
    {
        public string Review_Link_Type { get; set; }
        public int Office_Sequence { get; set; }
        public string Review_Link_Name { get; set; }
        public int CurrentMonthCount { get; set; }
        public int RatingCount { get; set; }

        public int RatingPecentage { get; set; }

    }
    public class TotalReviewRatings
    {
        public int OneStar { get; set; }
        public int TwoStar { get; set; }
        public int ThreeStar { get; set; }
        public int FourStar { get; set; }
        public int FiveStar { get; set; }
        public int SumOfRatings { get; set; }
    }
    public class ReviewResults
    {
        public string Date { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public string Publisher { get; set; }
        public string PublisherClass { get; set; }

    }
    public class CurrentRating
    {
        public string Date { get; set; }
        public double Rating { get; set; }
        public int Reviews { get; set; }
    }
    public class AverageRatingBySource
    {
        public string Review_Link_Type { get; set; }
        public string Review_Link_Name { get; set; }
        public List<CurrentRating> currentRating { get; set; }
        public int NewRatingCount { get; set; }
    }
}
