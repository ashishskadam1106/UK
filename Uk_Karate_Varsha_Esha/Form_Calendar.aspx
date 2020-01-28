<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_Calendar.aspx.cs" Inherits="Form_Calendar" %>

<!DOCTYPE html>
<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />
    <script>
        function Confirm() {
            if (confirm('Sure to delete ?') == false) {
                return true;
            }
            else {
                return false;
            }
        }


        $(function () {
            $('#Dp_FromDate').datepicker({
                dateFormat: 'dd-mm-yy'
            });
        });
        $(function () {
            $('#Dp_ToDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });

    </script>
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
        }

        .c-col-size-40 {
            width: 40%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-btn-width-47 {
            width: 47%;
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

        .c-grid-col-size-300 {
            width: 65%;
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

            .c-btn-width-47 {
                width: 100%;
            }
        }
    </style>

    <link rel="stylesheet" href="../bower_components/bootstrap/dist/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="../bower_components/font-awesome/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="../bower_components/Ionicons/css/ionicons.min.css">
  <!-- fullCalendar -->
  <link rel="stylesheet" href="../bower_components/fullcalendar/dist/fullcalendar.min.css">
  <link rel="stylesheet" href="../bower_components/fullcalendar/dist/fullcalendar.print.min.css" media="print">
  <!-- Theme style -->
  <link rel="stylesheet" href="../dist/css/AdminLTE.min.css">
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href="../dist/css/skins/_all-skins.min.css">

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

  <!-- Google Font -->
  <link rel="stylesheet"
        href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">

</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Calendar"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">

                        <section class="content-header">
                            <h1>Calendar
      
                            </h1>
                           
                        </section>


                        <section class="content">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="box box-solid">
                                        <div class="box-header with-border">
                                            <h4 class="box-title">Draggable Events</h4>
                                        </div>
                                        <div class="box-body">
                                            <!-- the events -->
                                            <div id="external-events">
                                                <div class="external-event bg-green">Lunch</div>
                                                <div class="external-event bg-yellow">Go home</div>
                                                <div class="external-event bg-aqua">Holiday</div>
                                                <div class="external-event bg-light-blue">Event</div>
                                                <div class="external-event bg-red">Exam</div>
                                                <div class="checkbox">
                                                    <label for="drop-remove">
                                                        <input type="checkbox" id="drop-remove">
                                                        remove after drop
                 
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.box-body -->
                                    </div>
                                    <!-- /. box -->
                                    <div class="box box-solid">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Create Event</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="btn-group" style="width: 100%; margin-bottom: 10px;">
                                                <!--<button type="button" id="color-chooser-btn" class="btn btn-info btn-block dropdown-toggle" data-toggle="dropdown">Color <span class="caret"></span></button>-->
                                                <ul class="fc-color-picker" id="color-chooser">
                                                    <li><a class="text-aqua" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-blue" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-light-blue" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-teal" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-yellow" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-orange" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-green" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-lime" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-red" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-purple" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-fuchsia" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-muted" href="#"><i class="fa fa-square"></i></a></li>
                                                    <li><a class="text-navy" href="#"><i class="fa fa-square"></i></a></li>
                                                </ul>
                                            </div>
                                            <!-- /btn-group -->
                                            <div class="input-group">
                                                <input id="new-event" type="text" class="form-control" placeholder="Event Title">

                                                <div class="input-group-btn">
                                                    <button id="add-new-event" type="button" class="btn btn-primary btn-flat">Add</button>
                                                </div>
                                                <!-- /btn-group -->
                                            </div>
                                            <!-- /input-group -->
                                        </div>
                                    </div>
                                </div>
                                <!-- /.col -->
                                <div class="col-md-9">
                                    <div class="box box-primary">
                                        <div class="box-body no-padding">
                                            <!-- THE CALENDAR -->
                                            <div id="calendar"></div>
                                        </div>
                                        <!-- /.box-body -->
                                    </div>
                                    <!-- /. box -->
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->
                        </section>


                    </div>
                </div>

            </div>
            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
    
<!-- jQuery 3 -->
<script src="../bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- jQuery UI 1.11.4 -->
<script src="../bower_components/jquery-ui/jquery-ui.min.js"></script>
<!-- Slimscroll -->
<script src="../bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
<!-- FastClick -->
<script src="../bower_components/fastclick/lib/fastclick.js"></script>
<!-- AdminLTE App -->
<script src="../dist/js/adminlte.min.js"></script>
<!-- AdminLTE for demo purposes -->
<script src="../dist/js/demo.js"></script>
<!-- fullCalendar -->
<script src="../bower_components/moment/moment.js"></script>
<script src="../bower_components/fullcalendar/dist/fullcalendar.min.js"></script>
<!-- Page specific script -->
<script>
    $(function () {

        /* initialize the external events
         -----------------------------------------------------------------*/
        function init_events(ele) {
            ele.each(function () {

                // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
                // it doesn't need to have a start or end
                var eventObject = {
                    title: $.trim($(this).text()) // use the element's text as the event title
                }

                // store the Event Object in the DOM element so we can get to it later
                $(this).data('eventObject', eventObject)

                // make the event draggable using jQuery UI
                $(this).draggable({
                    zIndex: 1070,
                    revert: true, // will cause the event to go back to its
                    revertDuration: 0  //  original position after the drag
                })

            })
        }

        init_events($('#external-events div.external-event'))

        /* initialize the calendar
         -----------------------------------------------------------------*/
        //Date for the calendar events (dummy data)
        var date = new Date()
        var d = date.getDate(),
            m = date.getMonth(),
            y = date.getFullYear()
        $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            buttonText: {
                today: 'today',
                month: 'month',
                week: 'week',
                day: 'day'
            },
            //Random default events
            events: [
              {
                  title: 'All Day Event',
                  start: new Date(y, m, 1),
                  backgroundColor: '#f56954', //red
                  borderColor: '#f56954' //red
              },
              {
                  title: 'Long Event',
                  start: new Date(y, m, d - 5),
                  end: new Date(y, m, d - 2),
                  backgroundColor: '#f39c12', //yellow
                  borderColor: '#f39c12' //yellow
              },
              {
                  title: 'Meeting',
                  start: new Date(y, m, d, 10, 30),
                  allDay: false,
                  backgroundColor: '#0073b7', //Blue
                  borderColor: '#0073b7' //Blue
              },
              {
                  title: 'Lunch',
                  start: new Date(y, m, d, 12, 0),
                  end: new Date(y, m, d, 14, 0),
                  allDay: false,
                  backgroundColor: '#00c0ef', //Info (aqua)
                  borderColor: '#00c0ef' //Info (aqua)
              },
              {
                  title: 'Birthday Party',
                  start: new Date(y, m, d + 1, 19, 0),
                  end: new Date(y, m, d + 1, 22, 30),
                  allDay: false,
                  backgroundColor: '#00a65a', //Success (green)
                  borderColor: '#00a65a' //Success (green)
              },
              {
                  title: 'Click for Google',
                  start: new Date(y, m, 28),
                  end: new Date(y, m, 29),
                  url: 'http://google.com/',
                  backgroundColor: '#3c8dbc', //Primary (light-blue)
                  borderColor: '#3c8dbc' //Primary (light-blue)
              }
            ],
            editable: true,
            droppable: true, // this allows things to be dropped onto the calendar !!!
            drop: function (date, allDay) { // this function is called when something is dropped

                // retrieve the dropped element's stored Event Object
                var originalEventObject = $(this).data('eventObject')

                // we need to copy it, so that multiple events don't have a reference to the same object
                var copiedEventObject = $.extend({}, originalEventObject)

                // assign it the date that was reported
                copiedEventObject.start = date
                copiedEventObject.allDay = allDay
                copiedEventObject.backgroundColor = $(this).css('background-color')
                copiedEventObject.borderColor = $(this).css('border-color')

                // render the event on the calendar
                // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                $('#calendar').fullCalendar('renderEvent', copiedEventObject, true)

                // is the "remove after drop" checkbox checked?
                if ($('#drop-remove').is(':checked')) {
                    // if so, remove the element from the "Draggable Events" list
                    $(this).remove()
                }

            }
        })

        /* ADDING EVENTS */
        var currColor = '#3c8dbc' //Red by default
        //Color chooser button
        var colorChooser = $('#color-chooser-btn')
        $('#color-chooser > li > a').click(function (e) {
            e.preventDefault()
            //Save color
            currColor = $(this).css('color')
            //Add color effect to button
            $('#add-new-event').css({ 'background-color': currColor, 'border-color': currColor })
        })
        $('#add-new-event').click(function (e) {
            e.preventDefault()
            //Get value and make sure it is not null
            var val = $('#new-event').val()
            if (val.length == 0) {
                return
            }

            //Create events
            var event = $('<div />')
            event.css({
                'background-color': currColor,
                'border-color': currColor,
                'color': '#fff'
            }).addClass('external-event')
            event.html(val)
            $('#external-events').prepend(event)

            //Add draggable funtionality
            init_events(event)

            //Remove event from text input
            $('#new-event').val('')
        })
    })
</script>
</body>
</html>
