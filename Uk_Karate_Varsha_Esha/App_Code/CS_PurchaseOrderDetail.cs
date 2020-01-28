﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_PurchaseOrderDetail
/// </summary>
public class CS_PurchaseOrderDetail
{
	public int Index { get; set; }
    public int SrNo { get; set; }
    public int MaterialMasterId { get; set; }
    public string MaterialName { get; set; }
    public double Rate { get; set; }
    public double Quantity { get; set; }
    public string DiscountType { get; set; }
    public int DiscountTypeId { get; set; }
    public double DiscountPer { get; set; }
    public double DiscountAmount { get; set; }
    public double TotalAmount { get; set; }
    public int PurchaseOrderDetailId { get; set; }
    public int PurchaseOrderHeaderId { get; set; }
}