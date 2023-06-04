using IFOA.Blazor.Definition.Enums;

namespace IFOA.Blazor.Definition.Dtos;

public class CareerTimeLineData
{
    public DateTimeOffset FromDate { get; set; }
    public DateTimeOffset ToDate { get; set; }
    
    public TimeLineTypeElementEnum Type { get; set; }
}