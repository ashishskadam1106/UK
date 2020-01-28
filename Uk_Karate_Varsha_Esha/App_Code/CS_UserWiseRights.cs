using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_UserWiseRights
/// </summary>
public class CS_UserWiseRights
{
    public int Employee_Id { get; set; }
    public int User_Id { get; set; } //AuthenticationId
    public int ProcessMaster_Id { get; set; }
    public string Process_Name { get; set; }
    public int Right_Type_Id { get; set; }
    public string Remark { get; set; }
    public int Userwise_Right_Id { get; set; }
    public int Is_Applicable { get; set; }
}