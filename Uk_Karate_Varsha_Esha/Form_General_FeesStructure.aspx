<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_General_FeesStructure.aspx.cs" Inherits="Form_General_FeesStructure" %>

<!DOCTYPE html>
<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>

    <DL:DefaultLinks runat="server" ID="DL_1" />
    <style>
        .c-label-right {
            float: right;
            margin-right: 5px;
        }

        .c-col-size-15 {
            width: 15%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-16 {
            width: 16%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-23 {
            width: 23%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-20 {
            width: 20%;
            padding-left: 0px;
            padding-right: 0px;
            top: 11px;
            left: 0px;
            height: 37px;
        }



        .c-btn-addnew {
            width: 120%;
            float: left;
            padding-top: 20px;
            padding-left: 120px;
        }



        .c-col-size-40 {
            width: 40%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-btn-widht-37 {
            width: 37%;
        }

        .c-col-size-8 {
            width: 8%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-height {
            margin: 1%;
            height: 28px;
        }

        .c-btn-height {
            margin: 1%;
            height: 28px;
            padding: 0px;
        }

        .c-width-66p {
            width: 65px;
        }

        .c-width-200p {
            width: 200px;
        }

        @media screen and (max-width:992px) {
            .c-col-size-8 {
                width: 8%;
                padding-left: 0px;
                padding-right: 0px;
            }

            .c-col-size-20 {
                width: 100%;
            }

            .c-col-size-40 {
                width: 100%;
            }

            .c-btn-widht-37 {
                width: 70%;
                margin-left: 15%;
                margin-bottom: 2%;
            }

            .c-width-66p {
                width: 100%;
                margin-top: 1px;
            }




            .c-btn-addnew {
                padding-top: 0px;
            }
        }
    </style>




     <script type="text/javascript">

         $(function () {
             $('#Tb_start_date').datepicker({
                 dateFormate: 'dd/mm/yyyy'
             });
         })



    </script>


     <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css" />
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>




</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

             <div class="content-wrapper">
                <div class="box  box-solid box-info c-border-blueish c-container-box"  style="margin-bottom:0">
                    <div class="box-header c-box-blueish  with-border" >
                        <h3 class="box-title">Fee Management</h3>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 no-padding">
                                        
                            <div class="col-md-12">
            
                           
			   
			              <div class="panel panel-info">
					           <%--<div class="panel-heading c-panel-blueish-new with-border">--%>
                                   <div class="box-header c-box-blueish  with-border ">
                       <%--  <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                              <div class="box-header c-box-blueish-new with-border">--%>
					              <h4>Basic</h4>
					           </div>
			
			<div class="panel-body">
				<div class="row">
					  
						<div class="tab-pane active" id="tab_EventLabel" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label2" runat="server"  Text="Annual Memebrship Fees *"></asp:Label>
                            </div>
                            <div class="col-md-12 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_AnnualFee" type="text" id="Tb_PostCode" class="form-control">--%>
                                              <asp:TextBox ID="Tb_AnnualFee" runat="server" class="form-control" Text="115"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Btn_GetAddress" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" ">--%>
                                            <asp:Button ID="Btn_AnnualFee" runat="server" Text="Update"   class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_AnnualFee_Click"/>
                                        </div>                        
                                                        
                             <div class="col-md-1 c-col-size-24 c-label-1">
                                <asp:Label ID="Label1" runat="server"  Text="Membership Fee For First Year *"></asp:Label>
                            </div>
                               
                                        <div class="col-md-2 c-col-size-10">
                                           
                                             <asp:TextBox ID="Tb_TornamentFee" runat="server" class="form-control" Text="18"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit1" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_TornamentFee" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_TornamentFee_Click" />
                                        </div>                        
                                </div>
                              
                           </div>
                            

    <div class="tab-pane active" id="Div4" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label3" runat="server"  Text="Enrollment Fee *"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <asp:TextBox ID="Tb_EnrolmentFee" runat="server" class="form-control" Text="20"></asp:TextBox>
                                        
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit2" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_EnrollmentFee" runat="server" Text="Update"  class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_EnrollmentFee_Click" />
                                        </div>                        
                                                        
                             <div class="col-md-1 c-col-size-24 c-label-1">
                                <asp:Label ID="Label4" runat="server"  Text="Term Fees *"></asp:Label>
                                           <%-- <asp:TextBox ID="TextBox1" runat="server" value="12"></asp:TextBox>--%>
                            </div>
                                        <div class="col-md-2 c-col-size-10">
                                           <%-- <input name="Tb_TermFees" type="text" id="Tb_TermFee" class="form-control" value="12">--%>
                                            <asp:TextBox ID="Tb_TermFee" runat="server"   class="form-control" value="12"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit3" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_TermFee" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_TermFee_Click" />
                                        </div>                        
                                </div>
                                 

                        </div>

						
                           

							
				</div>
			</div>
		</div>
			
    </div>
</div>
   </div>


























                            <%-- Grading --%>
                            <div class="col-md-12">
            
                           
			   
			              <div class="panel panel-info">
					           <%--<div class="panel-heading">--%>
                                   <div class="box-header c-box-blueish  with-border ">
					              <h4>Grading</h4>
					              </div>
			
			<div class="panel-body">
				<div class="row">
					  
						<div class="tab-pane active" id="Div1" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label5" runat="server"  Text="Grading Fee white *"></asp:Label>
                            </div>
                            <div class="col-md-12 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text4" class="form-control">--%>
                                            <asp:TextBox ID="Tb_GradeFee" runat="server" class="form-control" Text="10"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit4" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_GradeFee" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_GradeFee_Click" />
                                        </div>                        
                                                        
                             <div class="col-md-1 c-col-size-24 c-label-1">
                                <asp:Label ID="Label6" runat="server"  Text="Grading Fee Orange *"></asp:Label>
                            </div>
                                        <div class="col-md-2 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text5" class="form-control">--%>
                                            <asp:TextBox ID="Tb_GradingFeeOrng" runat="server" class="form-control" Text="15"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit5" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_GradingFeeOrng" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_GradingFeeOrng_Click"  />
                                        </div>                        
                                </div>
                           </div>
                            

    <div class="tab-pane active" id="Div2" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label7" runat="server"  Text="Brown Belt Grading Fee *"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Tb_GradingFeeBrn1" class="form-control">--%>
                                            <asp:TextBox ID="Tb_GradingFeeBrn" runat="server" class="form-control" Text="110"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Btn_GradingFeeBrn" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_GradingFeeBrn" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_GradingFeeBrn_Click" />
                                        </div>                        
                                                        
                             <div class="col-md-1 c-col-size-24 c-label-1">
                                <asp:Label ID="Label8" runat="server"  Text="Black Belt Grading Fee *"></asp:Label>
                            </div>
                                        <div class="col-md-2 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text7" class="form-control">--%>
                                            <asp:TextBox ID="Tb_GradingFeeBlk" runat="server" class="form-control" OnTextChanged="Tb_GradingFeeBlk_TextChanged" Text="110"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit7" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_Tb_GradingFeeBlk" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" />
                                        </div>                        
                                </div>
                                 

                        </div>
	
				</div>
			</div>
		</div>
			
    </div>
</div>
   </div>






                            <%-- Events --%>







                            <div class="col-md-12">
            
                           
			   
			              <div class="panel panel-info">
					         <%--  <div class="panel-heading">--%>
                                   <%--<div class="box-header c-box-blueish  with-border ">--%>
                                       <div class="box-header c-box-blueish  with-border ">
					              <h4>Events</h4>
					              </div>
			
			<div class="panel-body">
				<div class="row">
					  
						<div class="tab-pane active" id="Div3" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label9" runat="server"  Text="Japanese Seminar *"></asp:Label>
                            </div>
                            <div class="col-md-12 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text8" class="form-control">--%>
                                            <asp:TextBox ID="Tb_JpSeminar" runat="server" class="form-control" Text="20"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit8" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_JpSeminar" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_JpSeminar_Click" />
                                        </div>                        
                                                        
                             <div class="col-md-1 c-col-size-24 c-label-1">
                                <asp:Label ID="Label10" runat="server"  Text="Tournament Fee *"></asp:Label>
                            </div>
                                        <div class="col-md-2 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text9" class="form-control">--%>
                                            <asp:TextBox ID="Tb_TournamentFee" runat="server" class="form-control" Text="7"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit9" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_TournamentFee" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_TournamentFee_Click" />
                                        </div>                        
                                </div>
                           </div>
                            

    <div class="tab-pane active" id="Div5" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label11" runat="server"  Text="Tournament Fee Spirring *"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Tb_TournamentFee" class="form-control">--%>
                                            <asp:TextBox ID="Tb_TournamentEFee" runat="server" class="form-control" Text="7"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit10" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_TournamentEFee" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_TournamentEFee_Click" />
                                        </div>                        
                                  
	
				</div>
			</div>
		</div>
			
    </div>
</div>
   </div>
                            
                    </div>

                            </div>


                    <div class="col-md-12">
            
                           
			   
			              <div class="panel panel-info">
					           <%--<div class="panel-heading">--%>
                                <div class="box-header c-box-blueish  with-border ">
					              <h4>Kits</h4>
					              </div>
			
			<div class="panel-body">
				<div class="row">
					  
						<div class="tab-pane active" id="Div6" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label12" runat="server"  Text="Bronze Karate Kit *"></asp:Label>
                            </div>
                            <div class="col-md-12 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text11" class="form-control">--%>
                                            <asp:TextBox ID="Tb_BronzeKit" runat="server" class="form-control" Text="40"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit11" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_BronzeKit" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_BronzeKit_Click" />
                                        </div>                        
                                                        
                             <div class="col-md-1 c-col-size-24 c-label-1">
                                <asp:Label ID="Label13" runat="server"  Text="Silver Package *"></asp:Label>
                            </div>
                                        <div class="col-md-2 c-col-size-10">
                                           <%-- <input name="Tb_PostCode" type="text" id="Text12" class="form-control">--%>
                                            <asp:TextBox ID="Tb_SilverPkg" runat="server" class="form-control" Text="50"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit12" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_SilverPkg" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_SilverPkg_Click"/>
                                        </div>                        
                                </div>
                           </div>
                            
                            <%-- Kits --%>
    <div class="tab-pane active" id="Div7" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label14" runat="server"  Text="Gold Package *"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text13" class="form-control">--%>
                                            <asp:TextBox ID="Tb_GoldPkg" runat="server" class="form-control" Text="80"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit13" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_GoldPkg" runat="server" Text="Update"   class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_GoldPkg_Click"/>
                                        </div>                        
                                				</div>
			</div>
		</div>
			
    </div>
</div>
   </div>             
                    </div>

                            </div>


                            <div class="col-md-12">
            
                           <%-- Instruction Fee --%>
			   
			              <div class="panel panel-info">
					          <%-- <div class="panel-heading">--%>
                                   <div class="box-header c-box-blueish  with-border ">
					              <h4>Instructor's Fee</h4>
					              </div>
			
			<div class="panel-body">
				<div class="row">
					  
						<div class="tab-pane active" id="Div8" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label15" runat="server"  Text="Yearly Instructor Fee *"></asp:Label>
                            </div>
                            <div class="col-md-12 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text14" class="form-control">--%>
                                            <asp:TextBox ID="Tb_YearlyInstrFee" runat="server" class="form-control" Text="200"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit14" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_YearlyInstrFee" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_YearlyInstrFee_Click" />
                                        </div>                        
                                                        
                             <div class="col-md-1 c-col-size-24 c-label-1">
                                <asp:Label ID="Label16" runat="server"  Text="Seiwakai Fee *"></asp:Label>
                            </div>
                                        <div class="col-md-2 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text15" class="form-control">--%>
                                            <asp:TextBox ID="Tb_SeiwakaiFee" runat="server" class="form-control" Text="5"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit15" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_SeiwakaiFee" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_SeiwakaiFee_Click" />
                                        </div>                        
                                </div>
                           </div>
                            
                            <%-- Kits --%>
    <div class="tab-pane active" id="Div9" runat="server">
                             
		</div>
			
    </div>
</div>
   </div>
                            
                    </div>

                            </div>





                            <div class="col-md-12">
            
                           <%-- Instruction Fee --%>
			   
			              <div class="panel panel-info">
					          <%-- <div class="panel-heading">--%>
                                   <div class="box-header c-box-blueish  with-border ">
					              <h4>Grading Time Frame</h4>
					              </div>
			
			<div class="panel-body">
				<div class="row">
					  
						<div class="tab-pane active" id="Div10" runat="server">
                             
                            <div class="col-md-12">
                            <div class="col-md-1 c-col-size-5 c-label-1">
                                <asp:Label ID="Label17" runat="server"  Text="Time Frame *"></asp:Label>
                            </div>
                            <div class="col-md-12 no-padding c-col-size-86">
                                        <div class="col-md-3 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text16" class="form-control">--%>
                                            <asp:TextBox ID="Tb_TimeFrame" runat="server" class="form-control" Text="2"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit16" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" >--%>
                                            <asp:Button ID="Btn_TimeFrame" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_TimeFrame_Click" />
                                        </div>                        
                                                        
                             <div class="col-md-1 c-col-size-24 c-label-1">
                                <asp:Label ID="Label18" runat="server"  Text="Late Fee *"></asp:Label>
                            </div>
                                        <div class="col-md-2 c-col-size-10">
                                            <%--<input name="Tb_PostCode" type="text" id="Text17" class="form-control">--%>
                                            <asp:TextBox ID="Tb_LateFee" runat="server" class="form-control" Text="10.30"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit17" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_LateFee" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_LateFee_Click" />
                                        </div>                        
                                </div>
                           </div>
                            
                           
</div>



    
			
    </div>
</div>
   </div>
                            
                    </div>





                                <%-- My Dojo --%>







                            <div class="col-md-12">
            
                           <%-- Instruction Fee --%>
			   
			              <div class="panel panel-info">
					           <%--<div class="panel-heading">--%>
                                   <div class="box-header c-box-blueish  with-border ">
					              <h4>My Dojos</h4>
					              </div>
			
			<div class="panel-body">
				<div class="row">
					  

                     <div class="col-md-2 c-col-size-10">
                         <%--<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>--%>
                            <asp:DropDownList ID="Dd_Title" runat="server" DataValueField="" DataTextField="Level" CssClass="form-control select2" OnSelectedIndexChanged="Dd_Title_SelectedIndexChanged" OnTextChanged="Dd_Title_TextChanged"></asp:DropDownList>
                                 </div>
 
                                        <div class="col-md-2 c-col-size-10 c-padding-left-1">
                                            <%--<input type="submit" name="Btn_GetAddress" value="Update" id="Submit19" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">--%>
                                            <asp:Button ID="Btn_Dojo" runat="server" Text="Update" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" />

                                        </div>                        
                                </div>
                           </div>
                            
                           
</div>



    
			
    </div>
</div>
   </div>
                            
                    </div>

                            </div>

                        </div>                        
                    </div>
                </div>
               
            </div>
        <%--content end--%>

   <FT:Footer runat="server" ID="FT_1" />
        </div>
    
    </form>
</body>
</html>
