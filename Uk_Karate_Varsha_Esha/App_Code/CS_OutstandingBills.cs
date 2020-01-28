using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_OutstandingBills
/// </summary>
public class CS_OutstandingBills
{
    public int Index { get; set; }
    public int SrNo { get; set; }
    public int PurchaseBillHeaderId { get; set; }
    public string BillNumber { get; set; }
    public DateTime BillDate { get; set; }
    public string GRNNumber { get; set; }
    public DateTime GRNDate { get; set; }

    
    public int SupplierId { get; set; }
   
    public double TotalBillAmount { get; set; }
    public double PaidAmount { get; set; }
    public double BalanceAmount { get; set; }
    public double AllocatedAmount { get; set; }
    public double CurrentBalance { get; set; }
    public int CustomerRefId { get; set; }
    public double ReceivedAmount { get; set; }

    public int RABillHeaderId { get; set; }
    public double AdvancePaid { get; set; }
    public double OverReceived { get; set; }
}