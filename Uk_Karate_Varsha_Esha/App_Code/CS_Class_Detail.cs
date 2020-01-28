using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_PurchaseBill_Detail
/// used in purchase bill add.aspx
/// </summary>
public class CS_Class_Detail 
{
    public int Index { get; set; }
    public int SrNo { get; set; }
    public int DojoClassesScheduleId { get; set; }
    public int DojoId { get; set; }
    public int ClassId { get; set; }
    public string Class { get; set; }
    public int DayId { get; set; }
    public string Day { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; } 
    public int IsDeleted { get; set; }
    public Boolean IsChecked { get; set; }
}