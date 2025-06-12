﻿using System;

public class Report
{
    public int Id { get; set; }
    public int ReporterId { get; set; }
    public int TargetId { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;

}
