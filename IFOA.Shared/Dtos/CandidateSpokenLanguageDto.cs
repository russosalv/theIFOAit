using IFOA.DB.Entities;
using ISOLib.ISO;
using ISOLib.Model;

namespace IFOA.Shared.Dtos;

public class CandidateSpokenLanguageDto
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string LanguageName { get; set; } = string.Empty;
    
    public CEFRLevelEnum ReadingCefrLevel { get; set; } = CEFRLevelEnum.A1;
    public CEFRLevelEnum WritingCefrLevel { get; set; } = CEFRLevelEnum.A1;
    public CEFRLevelEnum SpeakingCefrLevel { get; set; } = CEFRLevelEnum.A1;
    public CEFRLevelEnum ListeningCefrLevel { get; set; } = CEFRLevelEnum.A1;
    
    public static explicit operator CandidateSpokenLanguageDto(CandidateSpokenLanguage x)
    {
        ISO<Language> iso639 = new ISO639();
        var _languages = iso639.GetArray().ToList();

        return new CandidateSpokenLanguageDto()
        {
            Id = x.Id,
            CandidateId = x.CandidateId,
            LanguageCode = x.LanguageCode,
            LanguageName = _languages.FirstOrDefault(y => y.Alpha2 == x.LanguageCode)?.Name ?? string.Empty,
            ListeningCefrLevel = x.ListeningCefrLevel,
            ReadingCefrLevel = x.ReadingCefrLevel,
            SpeakingCefrLevel = x.SpeakingCefrLevel,
            WritingCefrLevel = x.WritingCefrLevel
        };
    }


    public static explicit operator CandidateSpokenLanguage(CandidateSpokenLanguageDto x)
    {
        return new CandidateSpokenLanguage()
        {
            Id = x.Id,
            CandidateId = x.CandidateId,
            LanguageCode = x.LanguageCode,
            ListeningCefrLevel = x.ListeningCefrLevel,
            ReadingCefrLevel = x.ReadingCefrLevel,
            SpeakingCefrLevel = x.SpeakingCefrLevel,
            WritingCefrLevel = x.WritingCefrLevel
        };
    }
}