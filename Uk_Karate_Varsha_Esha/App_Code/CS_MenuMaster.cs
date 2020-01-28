using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CS_MenuMaster
/// </summary>
public class CS_MenuMaster
{
    public int Menu_Id { get; set; }
    public string Menu_Name { get; set; }
    public int Parent_Id { get; set; }
    public float ForOrder { get; set; }
}