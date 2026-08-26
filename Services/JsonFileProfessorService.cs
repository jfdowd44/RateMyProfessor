using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using RateMyProfessor.Models;

namespace RateMyProfessor.Services
{
    // Below I have the JsonFileProfessorService class that handles reading from
    // and writing to the JSON file containing professor's data.
    public class JsonFileProfessorService
    {
        public IWebHostEnvironment Env { get; }

        private string JsonFileName =>
            Path.Combine(Env.WebRootPath, "data", "professors.json");

        public JsonFileProfessorService(IWebHostEnvironment env)
        {
            Env = env;
        }

        public List<Professor> GetProfessors()
        {
            if (!File.Exists(JsonFileName))
            {
                return new List<Professor>();
            }

            using var reader = File.OpenText(JsonFileName);

            var content = reader.ReadToEnd();

            return JsonSerializer.Deserialize<List<Professor>>(
                       content,
                       new JsonSerializerOptions
                       {
                           PropertyNameCaseInsensitive = true
                       }
                   ) ?? new List<Professor>();
        }

        //Below is the AddReview method which puts new reviews into the correct professor's data.
        public void AddReview(string professorId, int stars, string text)
        {
            var professors = GetProfessors();

            var professor = professors.FirstOrDefault(p => p.Id == professorId);
            if (professor == null)
                return;

            if (professor.Ratings == null)
                professor.Ratings = new List<int>();
            if (professor.Reviews == null)
                professor.Reviews = new List<Review>();

            professor.Ratings.Add(stars);

            professor.Reviews.Add(new Review
            {
                Stars = stars,
                Text = text,
                CreatedAt = DateTime.Now
            });

            var json = JsonSerializer.Serialize(
                professors,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(JsonFileName, json);
        }
    }
}
