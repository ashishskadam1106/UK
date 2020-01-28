using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_FeeCategoryView
/// </summary>
public class CS_FeeCategoryView
{
    public int Index { get; set; }
    public int SrNo { get; set; }
    public int FeeId { get; set; }
    public int FeeCategoryId { get; set; }
    public string FeeCategory { get; set; }
    public Boolean IsOneOfTheGroup { get; set; }
    public String Remark { get; set; }
    public string FeeCollectionStage { get; set; }
    public string IsOneOfTheGroupText { get; set; }

}