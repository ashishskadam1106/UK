using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_QuotationDetails
/// </summary>
public class CS_QuotationDetails
{
    public int QuotationDetailId { get; set; }
    public int QuotationId { get; set; }
    public int ServiceId { get; set; }
    public string ServiceName { get; set; }
    public int SubServiceId { get; set; }
    public string SubServiceName { get; set; }
    public int UnitId { get; set; }
    public string Unit { get; set; }
    public int DiscountTypeId { get; set; }
    public string DiscountType { get; set; }
    public float FinalRate { get; set; }
    public float Rate { get; set; }
    public float DiscountPercent { get; set; }
    public float DiscountAmount { get; set; }
    public double Amount { get; set; }
    public int Index { get; set; }
    public int SrNo { get; set; }
    public double Quantity { get; set; }
    public int IsDeleted { get; set; }
    public string ServiceDescription { get; set; }
}