using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_Fees
/// </summary>
public class CS_Fees
{
	
    public int Index { get; set; }
    public int SrNo { get; set; }
    public int FeeId { get; set; }
    public string FeeName { get; set; }
    public int   FeeTemplateId	{ get; set; }
    public string  FeeTemplate	{ get; set; }
    public int FeeGenerationTypeId { get; set; }
    public string FeeGenerationType { get; set; }
    public int FeeCategoryId { get; set; }
    public string FeeCategory { get; set; }
    public int FeeCollectionStageId { get; set; }
    public string FeeCollectionStage { get; set; }
    public double Amount { get; set; }
    public double DiscountAmount { get; set; }	
    public double FinalAmount	 { get; set; }
    public double AmountPaid { get; set; }
    public double Balance { get; set; }
    public int FeeGenerationTypeReferenceId { get; set; }
    public string FeeGenerationTypeReference { get; set; }
    public Boolean IsOneOfTheGroup { get; set; }
    public Boolean IsEnabled { get; set; }
    public string RadioGroup { get; set; }
    public string Remark { get; set; } 
    public Boolean IsInitial { get; set; }
    public Boolean IsGroupRBChecked { get; set; }


    //Used for pay fees
    public int StudentFeesDetailId { get; set; }
    public double AmountReceivedTillNow { get; set; }
    public double BalanceTillNow { get; set; }
    public double AllocatedAmount { get; set; }
    public int StudentId { get; set; }
}