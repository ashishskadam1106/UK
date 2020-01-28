<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_StudentLogin.aspx.cs" Inherits="Form_StudentLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>UK Karate</title>
    <%--basic required links start--%>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="Font/font-awesome.min.css" />
    <link rel="stylesheet" href="fontawesome/css/font-awesome.css" />
    <link rel="stylesheet" href="fontawesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="Icon/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
        folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css" />
    <%--<link rel="stylesheet" href="dist/css/skins/skin-blue.min.css"/>--%>
    <%--basic required links end--%>

    <%--    <link rel="stylesheet" href="plugins/iCheck/flat/blue.css"/>
    <!-- Morris chart -->
    <link rel="stylesheet" href="plugins/morris/morris.css"/>
    <!-- jvectormap -->
    <link rel="stylesheet" href="plugins/jvectormap/jquery-jvectormap-1.2.2.css"/>
    <!-- Date Picker -->
    <link rel="stylesheet" href="plugins/datepicker/datepicker3.css"/>
    <!-- Daterange picker -->
    <link rel="stylesheet" href="plugins/daterangepicker/daterangepicker-bs3.css"/>
    <!-- bootstrap wysihtml5 - text editor -->
    <link rel="stylesheet" href="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>--%>
    <style>
        .main-header {
            /*background: linear-gradient(45deg, #7873CA, #605ca8) !important;*/
            background: linear-gradient(45deg, #01B9D3, #f2f2f2) !important;
            box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 1px 5px 0 rgba(0, 0, 0, 0.12);
        }

        .logo {
            background-color: transparent !important;
        }

        .navbar {
            background-color: transparent !important;
        }
    </style>

    <style>
        .C-LoginText {
            padding-top: 5%;
            padding-bottom: 5%;
            margin-bottom: 0px;
            color: white;
        }

        .C-MinCol {
            width: 50%;
        }

        @media only screen and (max-width:230px) {
            .C-LoginText {
                font-size: 24px;
            }

            .c-sign-in {
                color: white !important;
                font: 14px !important;
            }
        }

        .main-header {
            box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 1px 5px 0 rgba(0, 0, 0, 0.12);
        }

        .login-box {
            width: 400px;
            margin-left: 850px;
            margin-top: 200px;
            box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 1px 5px 0 rgba(0, 0, 0, 0.12);
            border-top: 0.5px solid #595959;
            background-color: #595959;
            opacity: 0.60;
            border-radius: 10px;
            border: 2px solid #595959 !important;
        }

        .login-logo {
            /*box-shadow: 0 16px 38px -12px rgba(0, 0, 0, 0.56), 0 4px 25px 0px rgba(0, 0, 0, 0.12), 0 8px 10px -5px rgba(0, 0, 0, 0.2);*/
            margin-left: 20px;
            margin-right: 20px;
            margin-top: -20px;
            padding: 10px 0;
            background-image: url('dist/img/mainlogo1.jpg');
            background: linear-gradient(25deg, #000000, #f2f2f2) !important;
            border-radius: 15px;
            border: 2px solid #f2f2f2 !important;
        }

        .c-sign-in {
            color: white !important;
            font: 18px !important;
        }

        .c-with-bottom-border {
            border: none;
            /*background-image: linear-gradient(#9c27b0, #9c27b0), linear-gradient(#D2D2D2, #D2D2D2);*/
            /*background-image: linear-gradient(55deg, #8B87D3, #48448C), linear-gradient(#D2D2D2, #D2D2D2);*/
            background-image: linear-gradient(45deg, #01B9D3, #f2f2f2), linear-gradient(#D2D2D2, #D2D2D2);
            background-size: 0 2px, 100% 1px;
            background-repeat: no-repeat;
            background-position: center bottom, center calc(100% - 1px);
            background-color: transparent;
            transition: background 0s ease-out;
            color: white !important;
        }


        .c-grid-label-center {
            text-align: center !important;
            vertical-align: middle !important;
        }

        .c-with-bottom-border:focus {
            outline: none;
            /*background-image: linear-gradient(#9c27b0, #9c27b0), linear-gradient(#D2D2D2, #D2D2D2);*/
            /*background-image: linear-gradient(55deg, #8B87D3, #48448C), linear-gradient(#D2D2D2, #D2D2D2);*/
            background-image: linear-gradient(45deg, #01B9D3, #f2f2f2), linear-gradient(#D2D2D2, #D2D2D2);
            background-size: 100% 2px, 100% 1px;
            box-shadow: none;
            transition-duration: 0.3s;
        }

        .c-with-shadow:hover {
            box-shadow: 0 14px 20px -12px #ccc, 0 4px 14px 0px #ccc, 0 6px 8px -5px #ccc !important;
        }

        .login-box-body {
            background-color: #595959;
            padding: 20px;
            border-top: 0;
            color: white;
        }

        .login-box-body {
            color: white;
            font-size: 15px;
            text-transform: uppercase;
        }

        .login-box-msg {
            margin: 0;
            text-align: center;
            padding: 0 20px 20px 20px;
            color: white;
        }

        .wrapper {
            background-image: url('dist/img/karatenew3.jpg');
            background-size: cover;
            background-position: top center;
            min-height: 800px;
        }

        .c-product-title {
            font-size: 28px;
        }

        .c-bg-blueish {
            /*Sign In box*/
            background: linear-gradient(25deg, #000000, #f2f2f2) !important;
            margin-left: 90px;
            border: 2px solid #f2f2f2 !important;
            border-radius: 15px !important;
        }

        @media (max-width: 768px) {
            .login-box {
                width: 90%;
                /*margin-top: 20px;*/
                margin: 20% auto;
            }

            .wrapper {
                min-height: 600px;
            }
        }
    </style>
</head>
<body class="hold-transition skin-purple-light sidebar-mini">
    <form id="form2" runat="server">
        <%--wrapper class is a container for all--%>
        <div class="wrapper" style="background-color: white;">
            <div>
            </div>
              <div>
            </div>
              <div>
            </div>
              <div>
            </div>
               <div>
            </div>

           <div style="text-align: center">
                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="White" Text="Registered Successfully! Please Login to access your account"></asp:Label>

                </div>

            <div class="login-box">
             

                <div class="login-logo C-LoginText">

                    <a href="#" style="color: white" class="c-product-title">Please Sign In </a>
                    <%-- <a href="#" style="font-size:15px; color:white" class="c-product-title">(Goju-Kai)</a><br />--%>
                </div>

                <!-- /.login-logo -->
                <div class="login-box-body" style="padding-top: 0">
                    <br />
                    <p class="login-box-msg"></p>
                    <div class="form-group has-feedback">
                        <asp:TextBox runat="server" ID="Tb_UserName" CssClass="form-control c-with-bottom-border" required="required"
                            oninvalid="this.setCustomValidity('Please enter Email Id')" oninput="setCustomValidity('')" placeholder="Email Id/Membership No" />
                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                    </div>
                    <div class="form-group has-feedback">
                        <asp:TextBox runat="server" ID="Tb_Password" CssClass="form-control c-with-bottom-border" required="required" oninvalid="this.setCustomValidity('Please enter Password')" oninput="setCustomValidity('')" placeholder="Password" />
                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                    </div>
                    <div class="row">
                        <div class="col-md-4 C-MinCol">
                            <asp:Button ID="Btn_Submit" runat="server" class="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" Text="Sign In" OnClick="Btn_Submit_Click"></asp:Button>
                        </div>
                    </div>
                    <p></p>
                    <%--<a style="text-transform:capitalize" href="#">Forgot Password</a>--%>
                </div>
                <!-- /.login-box-body -->

            </div>

        </div>
        <footer class="main-footer" style="border-bottom: 1px solid #f1eded; background: #fff9e6; margin: 0">
            <div class="pull-right hidden-xs">
                <b>Powered by Softpage IT Solutions   </b>
            </div>
            <strong>Copyright &copy; 2018-2019 <a>Softpage IT Solutions</a>.</strong> All rights reserved.
        </footer>
    </form>
</body>
</html>
