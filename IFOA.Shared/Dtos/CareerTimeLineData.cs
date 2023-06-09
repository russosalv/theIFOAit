﻿using IFOA.Shared.Enums;

namespace IFOA.Shared.Dtos;

public class CareerTimeLineData
{
    public DateTimeOffset FromDate { get; set; }
    public DateTimeOffset ToDate { get; set; }
    
    public TimeLineTypeElementEnum Type { get; set; }
    
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
}
