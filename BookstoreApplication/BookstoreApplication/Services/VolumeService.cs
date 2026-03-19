using System.Text.Json;
using BookstoreApplication.DTOs;
using BookstoreApplication.Connections;
using AutoMapper;
using BookstoreApplication.Repositories;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly IComicVineConnection _comicVineConnection;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IIssueRepository _issueRepository;


        public VolumeService(IComicVineConnection comicVineConnection,
            IConfiguration configuration, IMapper mapper, IIssueRepository issueRepository)
        {
            _comicVineConnection = comicVineConnection;
            _config = configuration;
            _mapper = mapper;
            _issueRepository = issueRepository;
        }

        public async Task<List<VolumeDto>> SearchVolumesByName(string filter)
        {
            var url = $"{_config["ComicVineBaseUrl"]}/volumes" +
              $"?api_key={_config["ComicVineAPIKey"]}" +
              $"&format=json" +
              $"&filter=name:{Uri.EscapeDataString(filter)}";

            var json = await _comicVineConnection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<VolumeDto>>(json, options);
        }

        public async Task<List<IssueDto>>GetIssuesByVolume(int volumeId)
        {
            var url = $"{_config["ComicVineBaseUrl"]}/issues" +
              $"?api_key={_config["ComicVineAPIKey"]}" +
              $"&format=json" +
              $"&filter=volume:{volumeId}";

            var json = await _comicVineConnection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<IssueDto>>(json, options);
        }

        public async Task<Issue> SaveIssue(SaveIssueDto dto)
        {
            var url = $"{_config["ComicVineBaseUrl"]}/issue/4000-{dto.ExternalApiId}" +
                      $"?api_key={_config["ComicVineAPIKey"]}" +
                      $"&format=json";

            var json = await _comicVineConnection.Get(url);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var apiIssue = JsonSerializer.Deserialize<IssueDto>(json, options);

            if (apiIssue == null) throw new Exception("Issue not found");

            DateTime? coverDate = null;
            if (!string.IsNullOrEmpty(apiIssue.Cover_Date))
                coverDate = DateTime.SpecifyKind(DateTime.Parse(apiIssue.Cover_Date), DateTimeKind.Utc);

            var issue = new Issue
            {
                Name = apiIssue.Name,
                CoverDate = coverDate ?? DateTime.UtcNow,
                IssueNumber = apiIssue.Issue_Number,
                Description = apiIssue.Description,
                ImagePath = null,
                ExternalApiId = dto.ExternalApiId,
                Price = dto.Price,
                AvailableCopies = dto.AvailableCopies,
                CreatedAt = DateTime.UtcNow
            };

            return await _issueRepository.AddAsync(issue);
        }
    }
}
