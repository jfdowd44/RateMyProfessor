using System;
using System.Collections.Generic;
using System.Linq;

namespace RateMyProfessor.Models
{
    //Below is our Review and Professor classes
    public class Review
    {
        public int Stars { get; set; }
        public string Text { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class Professor
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string img { get; set; } = "";
        public string Position { get; set; } = "";
        public string Office { get; set; } = "";
        public string Phone { get; set; } = "";

        public List<int> Ratings { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();

        public double AverageRating =>
            Ratings == null || Ratings.Count == 0
                ? 0
                : Ratings.Average();
    }
}
