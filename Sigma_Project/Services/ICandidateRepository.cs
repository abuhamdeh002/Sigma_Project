using Sigma_Project.Models;
using System.Collections.Generic;

namespace Sigma_Project.Services
{
    public interface ICandidateRepository
    {
        IEnumerable<Candidate> GetAllCandidates();
        Candidate GetCandidateByEmail(string email);
        void AddOrUpdateCandidate(Candidate candidate);
    }
}
