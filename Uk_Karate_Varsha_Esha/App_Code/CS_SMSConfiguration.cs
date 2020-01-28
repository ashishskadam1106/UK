using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_SMSConfiguration
/// </summary>
public class CS_SMSConfiguration
{
    public int SMSConfigurationId { get; set; }
    public int ServiceCentre_Id { get; set; }
    public string APIUrl { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string SenderUserName { get; set; }
    public int RouteType { get; set; }
}