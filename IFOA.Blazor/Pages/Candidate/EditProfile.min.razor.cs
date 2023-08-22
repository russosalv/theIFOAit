using System.Text.RegularExpressions;
using FluentValidation;
using IFOA.Shared;
using IFOA.Shared.Dtos;
using ISOLib.Model;
using MudBlazor;

namespace IFOA.Blazor.Pages.Candidate;

public partial class EditProfile
{
    private IEnumerable<string> MobileValidation(string pn)
    {
        if (string.IsNullOrWhiteSpace(pn))
        {
            yield return "Mobile number is required!";
            yield break;
        }

        if (pn.StartsWith("+") == false && pn.StartsWith("00") == false)
        {
            yield return "Mobile number must start with + or 00";
        }

        if (pn.Length < 5)
        {
            yield return "Mobile number is too short";
        }

        if (Regex.IsMatch(pn, @"[A-Za-z]"))
            yield return "Mobile number must contain only numbers";
    }

    private void AddPreferredLocation()
    {
        CandidateDto.CandidatePreferredLocations?.Add(new CandidatePreferredLocationDto()
        {
            FeId = Guid.NewGuid().ToString(),
        });
    }

    private async Task AddSpokenLanguage()
    {
        CandidateDto.CandidateSpokenLanguages?.Add(new CandidateSpokenLanguageDto()
        {
            FeId = Guid.NewGuid().ToString(),
        });
    }

    private void CancelPreferredLocation(CandidatePreferredLocationDto item)
    {
        if (CandidateDto.CandidatePreferredLocations != null)
            CandidateDto.CandidatePreferredLocations = CandidateDto.CandidatePreferredLocations
                .Where(x => x.FeId != item.FeId).ToList();
    }

    private void CancelSpokenLanguage(CandidateSpokenLanguageDto item)
    {
        if (CandidateDto.CandidateSpokenLanguages != null)
            CandidateDto.CandidateSpokenLanguages = CandidateDto.CandidateSpokenLanguages
                .Where(x => x.FeId != item.FeId).ToList();
    }

    private async Task<IEnumerable<string>> LanguagesSearchFunc(string arg)
    {
        if (string.IsNullOrEmpty(arg))
            return _languages.Select(x => x.Name);

        return _languages
            .Where(x => x.Name.ToString().StartsWith(arg, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => x.Name)
            .ToList();
    }

    private IEnumerable<string> ValidateCountry(Netizine.Enums.Country? value)
    {
        if (value is null)
        {
            yield return "The Country field is required";
            yield break;
        }

        if (!_countriesNullable.Any(x =>
                x.ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase)))
        {
            yield return "Incorrect Country";
        }
    }

    private Task ToggleJobFunctionClip(JobFunctionChip item)
    {
        item.IsSelected = !item.IsSelected;
        return Task.CompletedTask;
    }

    public class CandidateDtoFluentValidator : AbstractValidator<CandidateDto>
    {
        public CandidateDtoFluentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 100);


            When(x => x.ImageFile != null, () =>
            {
                RuleFor(x => x.ImageFile!.Size).LessThanOrEqualTo(IfoaConfigurationKeys.MaxImageSize)
                    .WithMessage("The maximum file size is 10 MB");
                RuleFor(x => x.ImageFile!.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/png"))
                    .WithMessage("The file must be a jpeg or png image");
            });

            RuleForEach(x => x.CandidatePreferredLocations).ChildRules(y =>
            {
                y.RuleFor(z => z.Country).NotEmpty().WithMessage("The Country field is required");
            });
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(ValidationContext<CandidateDto>.CreateWithOptions((CandidateDto)model,
                    x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

    public class CandidatePreferredLocationDtoValidator : AbstractValidator<CandidatePreferredLocationDto>
    {
        public CandidatePreferredLocationDtoValidator()
        {
            RuleFor(z => z.Country).NotEmpty().WithMessage("The Country field is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var cellContext = model as CellContext<CandidatePreferredLocationDto>;

            if (cellContext?.Item is null)
            {
                return Array.Empty<string>();
            }

            if (propertyName.Contains("."))
            {
                propertyName = propertyName.Split(".").Last();
            }
            
            var result =
                await ValidateAsync(ValidationContext<CandidatePreferredLocationDto>.CreateWithOptions(cellContext.Item, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}