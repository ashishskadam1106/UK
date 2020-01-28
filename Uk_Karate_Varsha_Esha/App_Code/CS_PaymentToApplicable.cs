using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_PaymentToApplicable
/// </summary>
public class CS_PaymentToApplicable
{
    public int PaymentTemplateApplicableToId { get; set; }
    public int PaymentTemplateId { get; set; }
    public int PaymentToId { get; set; }
    public int ReferenceId { get; set; }
    public string Reference { get; set; }
    public string AdditionalInformation { get; set; }
    public int InitialFeeGenerationTypeReferenceId { get; set; }
    public string InitialFeeGenerationTypeReference { get; set; }
    public bool IsModificationToConsiderForInitialPayment { get; set; }
    public bool IsActive { get; set; }
    public string IsActiveDescription { get; set; }
    public double Amount { get; set; }
}