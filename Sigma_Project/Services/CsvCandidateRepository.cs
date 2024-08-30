using Sigma_Project.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Sigma_Project.Services
{
    public class CsvCandidateRepository : ICandidateRepository
    {
        private readonly string _filePath = "Data/candidates.csv";

        public IEnumerable<Candidate> GetAllCandidates()
        {
            if (!File.Exists(_filePath)) return new List<Candidate>();

            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            return csv.GetRecords<Candidate>().ToList();
        }

        public Candidate GetCandidateByEmail(string email)
        {
            return GetAllCandidates().FirstOrDefault(c => c.Email == email);
        }

        public void AddOrUpdateCandidate(Candidate candidate)
        {
            var candidates = GetAllCandidates().ToList();
            var existingCandidate = candidates.FirstOrDefault(c => c.Email == candidate.Email);

            if (existingCandidate != null)
            {
                // Update existing candidate
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.PreferredContactTime = candidate.PreferredContactTime;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comments = candidate.Comments;
            }
            else
            {
                // Add new candidate
                candidates.Add(candidate);
            }

            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(candidates);
        }
    }
}
