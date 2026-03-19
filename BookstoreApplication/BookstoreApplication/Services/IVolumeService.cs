using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IVolumeService
    {
        Task<List<VolumeDto>> SearchVolumesByName(string filter);
        Task<List<IssueDto>> GetIssuesByVolume(int volumeId);
        Task<Issue> SaveIssue(SaveIssueDto dto);
    }
}
