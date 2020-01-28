<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DefaultLinks.ascx.cs" Inherits="User_Control_DefaultLinks" %>

<meta charset="utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<%--basic required links start--%>
<!-- Tell the browser to be responsive to screen width -->
<meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
<!-- Bootstrap 3.3.6 -->
<link rel="stylesheet" href="bootstrap/css/bootstrap.min.css"/>
<!-- Font Awesome -->
<%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css"/>--%>
<!--For test Font 1-->
<%--<link rel="stylesheet" href="Font1/font-awesome.min.css"/>--%>
 <!-- Font Awesome -->
<%--<link rel="stylesheet" href="Font/font-awesome.min.css"/>--%>
<link rel="stylesheet" href="fontawesome/css/font-awesome.css"/>
<link rel="stylesheet" href="fontawesome/css/font-awesome.min.css"/>
<!-- Ionicons -->
<link rel="stylesheet" href="Icon/ionicons.min.css"/>
<!-- Theme style -->
<link rel="stylesheet" href="dist/css/AdminLTE.min.css"/>
<!-- AdminLTE Skins. Choose a skin from the css/skins
    folder instead of downloading all of them to reduce the load. -->
<link rel="stylesheet" href="dist/css/skins/_all-skins.min.css"/>
<%--<link rel="stylesheet" href="dist/css/skins/skin-blue.min.css"/>--%>
<%--basic required links end [Other basic scripts are added at page end--%>

    <link rel="stylesheet" href="plugins/iCheck/flat/blue.css"/>
<!-- Morris chart -->
<link rel="stylesheet" href="plugins/morris/morris.css"/>
<!-- jvectormap -->
<link rel="stylesheet" href="plugins/jvectormap/jquery-jvectormap-1.2.2.css"/>
<!-- Date Picker -->
<link rel="stylesheet" href="plugins/datepicker/datepicker3.css"/>
<!-- Daterange picker -->
<link rel="stylesheet" href="plugins/daterangepicker/daterangepicker-bs3.css"/>
<!-- bootstrap wysihtml5 - text editor -->
<link rel="stylesheet" href="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>
    
      
<%--Required start footer links--%>
<!-- jQuery 2.2.0 -->
<script src="plugins/jQuery/jQuery-2.2.0.min.js"></script>
<!-- jQuery UI 1.11.4 -->
<script src="JS/jquery-ui.min.js"></script>
<!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
<script>
    $.widget.bridge('uibutton', $.ui.button);
</script>
<!-- Bootstrap 3.3.6 -->
<script src="bootstrap/js/bootstrap.min.js"></script>
<%--Required end--%>

<%--Following links are as per requirement only i.e. if the functionality is used--%>
<!-- jQuery 2.2.0 -->
<!-- Morris.js charts -->
<%--<script src="JS/MorrisExtra/raphael-min.js"></script>
<script src="plugins/morris/morris.min.js"></script>--%>

<!-- Sparkline -->
<%--<script src="plugins/sparkline/jquery.sparkline.min.js"></script>--%>

<!-- jvectormap -->
<%--<script src="plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
<script src="plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>--%>

<!-- jQuery Knob Chart -->
<%--<script src="plugins/knob/jquery.knob.js"></script>--%>

<!-- daterangepicker -->
<%--<script src="JS/moment.min.js"></script>
<script src="plugins/daterangepicker/daterangepicker.js"></script>--%>

<!-- datepicker -->
<script src="plugins/datepicker/bootstrap-datepicker.js"></script>

<!-- Bootstrap WYSIHTML5 Another required start-->
<script src="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
<!-- Slimscroll -->
<script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
<!-- FastClick -->
<script src="plugins/fastclick/fastclick.js"></script>
<!-- AdminLTE App -->
<script src="dist/js/app.min.js"></script>
<%--Another required end--%>

<%--<!-- AdminLTE dashboard demo (This is only for demo purposes) -->
<script src="dist/js/pages/dashboard.js"></script>
<!-- AdminLTE for demo purposes -->
<script src="dist/js/demo.js"></script>--%>

<%--Custom Links--%>
<link rel="stylesheet" href="CSS/CustomFA.css" />
<link rel="stylesheet" href="CSS/CustomStyles.css" />

<%--select 2 start--%>
<!-- Select2 -->
<script src="plugins/select2/select2.full.min.js"></script>
    <!-- Select2 -->
<link rel="stylesheet" href="plugins/select2/select2.min.css"/>
<script>
    $(function () {
        //Initialize Select2 Elements
        $(".select2").select2();
    });
</script>

<style>
    .select2-container--default .select2-selection--single {
        border: 1px solid #d2d6de; 
        border-radius: 0px; 
        height: 34px;
    }
   
</style>

