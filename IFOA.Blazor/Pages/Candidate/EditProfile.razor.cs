using System.Diagnostics;
using Azure.Storage.Sas;
using IFOA.Blazor.Common;
using IFOA.Blazor.Helpers;
using IFOA.DB.Entities;
using IFOA.Shared;
using IFOA.Shared.Dtos;
using IFOA.Shared.Services;
using ISOLib.ISO;
using ISOLib.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Country = Netizine.Enums.Country;

namespace IFOA.Blazor.Pages.Candidate;

public partial class EditProfile : DbPage
{
    [Parameter] public Guid? Id { get; set; }
    [Inject] NavigationManager Navigation { get; set; }
    [Inject] PictureBlobStorageService PictureBlobStorageService { get; set; } = null!;

    CandidateDto CandidateDto = new();

    List<Country?> _countriesNullable = new List<Country?>();
    List<Language> _languages = new List<Language>();
    List<JobFunctionChip> _jobFunctionChips = new List<JobFunctionChip>();


    protected override async Task OnInitializedAsync()
    {
        foreach (var country in EnumHelper.GetValues<Country>().ToList().Where(country => country != Country.NotSet))
        {
            _countriesNullable.Add(country);
        }

        ISO<Language> iso639 = new ISO639();
        _languages = iso639.GetArray().ToList();

        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates.AsNoTracking()
            .Include(x => x.CandidateSpokenLanguages)
            .Include(x => x.CandidatePreferredJobFunctions)
            .ThenInclude(x => x.JobFunction)
            .Include(x => x.CandidatePreferredLocations)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (dbData is not null)
        {
            CandidateDto = (CandidateDto)dbData;
            CandidateDto.CandidateSpokenLanguages =
                dbData.CandidateSpokenLanguages
                    .Select(x => (CandidateSpokenLanguageDto)x).ToList();

            CandidateDto.CandidatePreferredLocations = dbData.CandidatePreferredLocations
                .Select(x => (CandidatePreferredLocationDto)x).ToList();
        }

        var jobFunctions = await _context.JobFunctions.AsNoTracking().ToListAsync();

        foreach (var jobFunction in jobFunctions)
        {
            var isSelected = dbData?.CandidatePreferredJobFunctions.ToList()
                .FirstOrDefault(x => x.JobFunctionId == jobFunction.Id) is not null;

            _jobFunctionChips.Add(new JobFunctionChip()
            {
                JobFunction = jobFunction,
                IsSelected = isSelected
            });
        }
    }

    private async Task SaveData()
    {
        await form.Validate();

        if (form.IsValid is false)
            return;

        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await
            _context?.Candidates
                .Include(x => x.CandidatePreferredLocations)
                .Include(x => x.CandidateSpokenLanguages)
                .Include(x => x.CandidatePreferredJobFunctions)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == CandidateDto.Id)!;

        if (dbData is null)
        {
            var newCandidate = (DB.Entities.Candidate)CandidateDto;
            newCandidate.CandidatePreferredLocations = CandidateDto.CandidatePreferredLocations
                .Select(x => (DB.Entities.CandidatePreferredLocation)x).ToList();
            newCandidate.CandidateSpokenLanguages = CandidateDto.CandidateSpokenLanguages
                .Select(x => (DB.Entities.CandidateSpokenLanguage)x).ToList();
            newCandidate.CandidatePreferredJobFunctions = _jobFunctionChips.Where(x => x.IsSelected).Select(x =>
                new CandidatePreferredJobFunction()
                {
                    JobFunctionId = x.JobFunction.Id,
                    CandidateId = newCandidate.Id
                }).ToList();

            dbData = newCandidate;

            _context?.Candidates.Add(dbData);
        }
        else
        {
            dbData.Gender = CandidateDto.Gender;
            dbData.Name = CandidateDto.Name;
            dbData.Surname = CandidateDto.Surname;
            dbData.BirthDate = CandidateDto.BirthDate;
            dbData.Nationality = CandidateDto.Nationality ?? Country.NotSet;
            dbData.Email = CandidateDto.Email;
            dbData.PhoneNumber = CandidateDto.PhoneNumber;
            dbData.ResidenceCountry = CandidateDto.ResidenceCountry ?? Country.NotSet;
            dbData.Address = CandidateDto.Address;
            dbData.City = CandidateDto.City;
            dbData.ZipCode = CandidateDto.ZipCode;
            dbData.CoverLetter = CandidateDto.CoverLetter;
            dbData.Biography = CandidateDto.Biography;

            dbData.CandidatePreferredLocations.Clear();
            dbData.CandidatePreferredLocations =
                CandidateDto?.CandidatePreferredLocations?.Select(x => (DB.Entities.CandidatePreferredLocation)x)
                    ?.ToList() ?? new List<DB.Entities.CandidatePreferredLocation>();

            dbData.CandidateSpokenLanguages.Clear();
            dbData.CandidateSpokenLanguages = CandidateDto?.CandidateSpokenLanguages?
                .Select(x => (DB.Entities.CandidateSpokenLanguage)x)?.ToList() ?? new List<CandidateSpokenLanguage>();

            dbData.CandidatePreferredJobFunctions.Clear();
            dbData.CandidatePreferredJobFunctions = _jobFunctionChips.Where(x => x.IsSelected).Select(x =>
                new CandidatePreferredJobFunction()
                {
                    JobFunctionId = x.JobFunction.Id,
                    CandidateId = dbData.Id
                }).ToList();

            _context?.Candidates.Update(dbData);
        }

        //Save images
        if (CandidateDto?.IsImageFileChanged == true && dbData is not null && CandidateDto.ImageFile is not null)
        {
            var image = CandidateDto?.ImageFile?.OpenReadStream(IfoaConfigurationKeys.MaxImageSize);
            if (image is not null)
            {
                var blobClient =
                    await PictureBlobStorageService.UploadPictureAsync(image,
                        $"{CandidateDto!.Id}-{CandidateDto!.ImageFile!.Name}");
                dbData.ImageLink = blobClient.Uri.ToString();
            }
        }

        await _context?.SaveChangesAsync(CancellationToken.None)!;
        Navigation.NavigateTo($"{ViewProfile.PageUrl}/{CandidateDto.Id}");
    }

    public class JobFunctionChip
    {
        public JobFunction JobFunction { get; set; }
        public bool IsSelected { get; set; }

        public Color Color => IsSelected ? Color.Primary : Color.Dark;

        public string Icon =>
            IsSelected
                ? Icons.Material.Filled.Check
                : GraphicHelpers.GetMudIconByName(
                    JobFunction.Icon,
                    Icons.Material.Filled.NotInterested,
                    includeRounded: true);
    }
}