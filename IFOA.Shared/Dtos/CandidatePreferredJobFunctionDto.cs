using IFOA.DB.Entities;

namespace IFOA.Shared.Dtos;

public class CandidatePreferredJobFunctionDto : FeEntity
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public int JobFunctionId { get; set; }
    public virtual JobFunction JobFunction { get; set; }
    
    public static explicit operator CandidatePreferredJobFunctionDto(CandidatePreferredJobFunction x)
    {
        //var countries = EnumHelper.GetValues<Country>().ToList();

        return new CandidatePreferredJobFunctionDto()
        {
            FeId = x.Id.ToString(),
            Id = x.Id,
            CandidateId = x.CandidateId,
            JobFunction = x.JobFunction,
            JobFunctionId = x.JobFunctionId
        };
    }


    public static explicit operator CandidatePreferredJobFunction(CandidatePreferredJobFunctionDto x)
    {
        return new CandidatePreferredJobFunction()
        {
            Id = x.Id,
            CandidateId = x.CandidateId,
            JobFunctionId = x.JobFunctionId
        };
    }
}