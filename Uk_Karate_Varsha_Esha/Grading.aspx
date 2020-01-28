<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grading.aspx.cs" Inherits="Grading" %>

<!DOCTYPE html>
<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>
    <%--<link rel="stylesheet" href="//jqueryui.com/jquery-wp-content/themes/jquery/css/base.css?v=1">
    <link rel="stylesheet" href="//jqueryui.com/jquery-wp-content/themes/jqueryui.com/style.css">--%>
    <link rel="stylesheet" type="text/css" href="http://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="http://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">

    <%-- Default css--%>

    <DL:DefaultLinks runat="server" ID="DL_1" />
    <style>
        .c-grid-profile-pic {
            width: 120px;
            text-align: center;
            padding-top: 1% !important;
            padding-bottom: 1% !important;
            vertical-align: top;
        }

        .c-btn-Renewal {
            width: 17.7%;
            float: right;
            padding-right: 16px;
        }

        .c-grid-col-size-300 {
            width: 350px !important;
            text-align: left;
            padding: 1% !important;
            vertical-align: middle;
        }

        .c-grid-col-size-100 {
            width: 100px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-150 {
            width: 150px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-50 {
            width: 50px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-200 {
            width: 200px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-250 {
            width: 250px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-275 {
            width: 275px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }


        .c-grid-label-3 {
            font-size: 18px;
            color: tomato;
        }

        .c-grid-label-center {
            text-align: center !important;
            vertical-align: middle !important;
        }

        .c-grid-label-left {
            text-align: left !important;
            vertical-align: middle !important;
        }

        .c-grid-label-green {
            color: green;
        }

        .c-grid-label-red {
            color: red;
        }

        .c-grid-label-1 {
            font-size: 14px;
            margin-bottom: 2px;
        }

        .c-btn-search {
            width: 15%;
            float: right;
        }

        .c-btn-widht-15 {
            width: 15%;
            float: right;
        }

        .c-btn-widht-20 {
            width: 20%;
            float: right;
        }

        .c-grid-row {
            background-color: #B2BABB !important;
            color: white;
        }

        .c-col-size-43 {
            width: 43%;
        }

        @media screen and (max-width:900px) {
            .c-btn-widht-15 {
                width: 100%;
                float: right;
            }

            .c-btn-widht-20 {
                width: 50%;
                float: right;
            }

            .c-col-size-40 {
                width: 100%;
            }

            .c-col-size-43 {
                width: 100%;
            }
        }
    </style>
    <%--end default css ---%>

    <style>
        #gallery {
            float: left;
        }

        .gallery.custom-state-active {
            background: #eee;
        }

        .gallery li {
            float: left;
            height: 160px;
            padding: 0.4em;
            margin: 0 0.4em 0.4em 0;
            text-align: center;
        }

            .gallery li h5 {
                margin: 0 0 0.4em;
                cursor: move;
            }

            .gallery li a {
                float: right;
            }

                .gallery li a.ui-icon-zoomin {
                    float: left;
                }

            .gallery li img {
                width: 100%;
                cursor: move;
            }

        .container {
             
            width: 32%;
            min-height: 16em;
            padding: 1%;
        }

            .container h4 {
                line-height: 16px;
                margin: 0 0 0.4em;
            }

                .container h4 .ui-icon {
                    float: left;
                }

            .container .gallery h5 {
                display: none;
            }

        /* ask */
        .main-container .row {
            width: 100%;
            min-height: 25px;
            margin: 0px 5px 0px 5px;
            background-color: #ddd;
            border-color: bisque;
            border: 1px;
            border-style: solid;
            height: auto;
        }

        .w3-code {
            width: auto;
            background-color: #fff;
            padding: 8px 12px;
            border-left: 4px solid #4CAF50;
            word-wrap: break-word;
        }

        /*Pop-up*/

        li {
            list-style-type: none;
        }

            li.ui-state-disabled {
            }

            li.ui-state-disabled {
                margin: 3px 3px 3px 0;
                padding: 1px;
                float: left;
                width: 20px !important;
                height: 150px;
                font-size: 13px;
                text-align: center;
                border: 1px solid black;
                clear: left;
                opacity: 100 !important;
                background-color: darkseagreen;
                font-weight: bolder;
            }

            li.ui-state-defaultinclude {
                border: 1px solid #c5c5c5;
                background: #f6f6f6;
                font-weight: normal;
                color: #454545;
                margin: 3px 3px 3px 0;
                padding: 1px;
                float: left;
                width: 250px;
                height: 150px;
                font-size: 13px;
                text-align: center;
            }

        #note {
            width: 100%;
        }

        .close {
            font-size: 32px;
            float: right;
        }
        /*End pop-up css*/ 
       .row ul {
            padding: 0px;
        }
        .content-wrapper {
            overflow: hidden;
        }
    </style>

</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">

                    <table id="example" class="display" style="width: 100%">
                        <thead>
                            <tr>
                                <th>level</th>
                                <th>Dojo Code</th>
                                <th>Student ID</th>
                                <th>Full Name</th>
                                <th>Grade</th>
                                <th>Belt ID</th>
                                <th>Fees</th>
                                <th>Fees Paid</th>
                                <th>Grading Paid</th>
                                <th>Membership  Paid</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody id="tlist"></tbody>
                    </table>

                    <button id="btnAddRow" type="button">Add Row</button>
                    <button id="btnDeleteRow" type="button">Delete Row</button>
                    <button id="btnSaveGrade" type="button">Save Grade</button>
                    <button id="btnFinishGrade" type="button">Finish Grade</button>
                    <div class="main-container">
                        <div id="trash1" class="container row ui-widget-content ui-state-default">

                            <ul>
                                <li class="ui-state-default ui-state-disabled">1 </li>
                            </ul>

                        </div>
                    </div>

                    <input type="hidden" id="Perm" value="1">

                    <!-- Modal -->
                    <div class="modal fade" id="myModal" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Note</h4>
                                </div>
                                <div class="modal-body">
                                    <textarea rows="4" cols="2" id="note"></textarea>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default btnSaveNote">Save</button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>

    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {

            var table;
            function content(data) {
                return '<li class="ui-state-default ui-state-defaultinclude grade" data-id="' + data["2"] + '"   >[' + data["3"] + '] <br><div class="initialBelt_' + data["2"] + '"> (' + data["4"] + ')</div> <br> <div class="selectbox "> <select id="beltid_' + data[2] + '" class="form-control beltTypes"></select> </div> <div class="checkbox"> <label> <input style="width:20px; height:20px;" type="checkbox" id="provisional_' + data[2] + '" class="provisional"><strong>&nbsp;Provisional</strong> </label>  <i style="font-size:24px;" data-id="' + data[2] + '" class="fa fa-pencil-square-o note_' + data[2] + '" aria-hidden="true"></i></div><div class="close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></div> </li>'
            }

            function getUrlParameter(name) {
                name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
                var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
                var results = regex.exec(location.search);
                return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
            };

            function Permission(expression) {
                switch (expression) {
                    case 1:
                        $('#btnAddRow').hide();
                        $('#btnDeleteRow').hide();
                        $('#btnSaveGrade').hide();
                        $('#example_wrapper').hide()
                        $(".grade").draggable({ cancel: ".row" });
                        break;
                    case 2:
                        $('#btnFinishGrade').hide();
                        $("select").prop("disabled", true);
                        $('input[id^=provisional_]').prop("disabled", true);
                        break;

                        //  default:
                }
            }


            var belTypes = ["White - 0 Kyu", "White 1 Tag - 1st Level 9th Kyu", "Half Yellow - 2nd Level 9th Kyu", "Half Yellow + Tag - 3rd Level 9th Kyu", "Yellow - 9 Kyu", "Yellow +1 Tag - 1st Level 8th Kyu", "Half Orange - 2nd Level 8th Kyu", "Half Orange +Tag - 3rd Level 8th Kyu", "Orange - 8 Kyu", "Orange +1 Tag - 1st Level 7th Kyu", "Half Red - 2nd Level 7th Kyu", "Half Red +Tag - 3rd Level 7th Kyu", "Red - 7 Kyu", "Red +1 Tag - 1st Level 6th Kyu", "Half Green - 2nd Level 6th Kyu", "Half Green +Tag - 3rd Level 6th Kyu", "Green - 6 Kyu", "Green +1 Tag - 1st Level 5th Kyu", "Half Blue - 2nd Level 5th Kyu", "Half Blue +Tag - 3rd Level 5th Kyu", "Blue - 5 Kyu", "Blue +1 Tag - 1st Level 4th Kyu", "Half Purple - 2nd Level 4th Kyu", "Half Purple +Tag - 3rd Level 4th Kyu", "Purple - 4 Kyu", "Purple +1 Tag - 1st Level 3rd Kyu", "Half Brown - 2nd Level 3rd Kyu", "Half Brown +Tag - 3rd Level 3rd Kyu", "Brown 1 - 3 Kyu", "3rd Kyu Brown +1 Tag - 1st Level 2nd Kyu", "3rd Kyu Brown +2 Tag - 2nd Level 2nd Kyu", "3rd Kyu Brown +3 Tag - 3rd Level 2nd Kyu", "Brown 2 - 2 Kyu", "2 Kyu Brown +1 Tag - 1st Level 1st Kyu", "2 Kyu Brown +2 Tag - 2nd Level 1st Kyu", "2 Kyu Brown +3 Tag - 3rd Level 1st Kyu", "Brown 3 - 1 Kyu", "1 Kyu Brown +1 Tag - 1st Level Shodan-Ho", "1 Kyu Brown +2 Tag - 2nd Level Shodan-Ho", "1 Kyu Brown +3 Tag - 3rd Level Shodan-Ho", "Black - Shodan-Ho", "Black - 1st Dan", "Black - 2nd Dan", "Black - 3rd Dan", "Black - 4th Dan", "Black - 5th Dan", "Black - 6th Dan", "Black - 7th Dan", "Black - 8th Dan", "Black - 9th Dan"]

            init();
            function init() {
                
                $.ajax({
                    type: "POST",
                    url: "Grading.aspx/GetData",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ eventID: getUrlParameter('ID') }),
                    dataType: "json",
                    success: function (response) {
                        if (response.d == "") {
                            alert('No data found !')
                            return 0;
                        }

                        var parsed = JSON.parse(response.d);

                        $.map(parsed.data, function (val, i) {
                            $("#tlist").append("<tr> <td>" + val["Index"] + "</td> <td>" + val["DojoName"] + "</td>  <td>" + val["StudentId"] + "</td> <td>" + val["FullName"] + "</td> <td>" + val["Grade"] + "</td> <td>" + val["BeltId"] + "</td>  <td>" + val["Fees"] + "</td> <td>" + val["FeesPaid"] + "</td> <td><b style='color: green;'>" + val["GradingFeeStatus"] + "</b></td> <td><b style='color: green;'>" + val["MembershipFee"] + "</b></td> <td></td></tr>")
                        });

                        table = $('#example').DataTable({
                            createdRow: function (row, data, dataIndex) {
                                $(row).attr('data-id', data[2]);
                                $(row).attr('data-beltid', data[5]);
                            },
                            "columnDefs": [{
                                "targets": -1,
                                "data": null,
                                "defaultContent": "<button id='btnMove' type='button'>Move</button>"
                            }]

                        });
                        bindGradeData(parsed.grade);

                        Permission(parsed.Permission);
                        $('#Perm').val(parsed.Permission);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }


            $('#example tbody').on('click', 'button', function () {
                var tr = $(this).closest('tr');
                var studentID = tr.attr('data-id');
                var beltId = tr.attr('data-beltid');

                var row = table.row($(this).parents('tr'))

                data = row.data();
                row.remove().draw();
                var rowCount = $('.main-container').children().length;
                $('#trash' + rowCount + ' ul').append(content(data));
                $("li").draggable({
                    //cancel: "a.ui-icon", // clicking an icon won't initiate dragging
                    revert: "invalid", // when not dropped, the item will revert back to its initial position
                    containment: "document",
                    helper: "clone",
                    cursor: "move"
                });

                $('.initialBelt_' + studentID).attr('data-id', beltId)

                beltFun(data, beltId);

                //remove student
                $('.close').click(function () {
                    $(this).closest('.grade').remove();
                    //  location.reload()
                });
                $('.fa-pencil-square-o').prop("disabled", true);
                //addNotes();
                Permission(parseInt($('#Perm').val()));
            });

            function beltFun(data, belt) {
                $('#beltid_' + data["2"]).empty();
                $.each(belTypes, function (key, value) {
                    key = key + 1;
                    $('#beltid_' + data["2"]).append($("<option></option>").attr("value", key).text(value));
                    if (belt == key) {
                        $('#beltid_' + data["2"] + ' option').attr("selected", "selected");
                    }
                });
            }

            $('#btnFinishGrade').click(function () {
                SaveGradeData();
                $.ajax({
                    type: "POST",
                    url: "Grading.aspx/FinishGrading",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        alert(response.d);
                        location.reload();
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            })

            $('#btnAddRow').click(function () {
                var divCount = $('.main-container > div').length + 1;
                var content = '<div id="trash' + divCount + '" class="container row ui-widget-content ui-state-default"> <ul> <li class="ui-state-default ui-state-disabled">' + divCount + ' </li> </ul>    </div> </div>'

                $('.main-container').append(content)
                $('.container').droppable({
                    classes: {
                        "ui-droppable-active": "ui-state-highlight"
                    },
                    drop: function (event, ui) {
                        $trash = $(this).closest('.container');
                        deleteImage(ui.draggable);
                    }
                });

                function deleteImage($item) {
                    $item.fadeOut(function () {
                        var $list = $("ul", $trash).length ?
                            $("ul", $trash) :
                            $("<ul class='gallery ui-helper-reset'/>").appendTo($trash);

                        $item.appendTo($list).fadeIn(function () {
                            $item
                                //.animate({ width: "48px" })
                                .find("img")
                                .animate({ height: "36px" });
                        });
                    });
                }

                addNotes();

            });

            $('#btnDeleteRow').click(function () {
                var r = confirm("Are yousure to delete last row ?");
                if (r == true) {

                    var lastRowId = $(".container").last().attr('id')
                    var divCount = ($('#' + lastRowId + ' li').length) - 1;
                    if (divCount > 0) {
                        alert('This row has some data, so can\'t delete this row.')
                    } else {
                        $('#' + lastRowId + '').remove()
                    }
                }
            });


            $('#btnSaveGrade').click(function () {
                SaveGradeData();
                //reload page
                location.reload();
            });

            /*Retrieve data*/
            function bindGradeData(data) {
                var i;
                for (i = 0; i < data.length; i++) {
                    loadData(data[i]);
                }
            }


            function SaveGradeData() {
                jsonObj = [];
                $('.main-container').children().each(function (i, val) {
                    var row = '#trash' + (i + 1);
                    $(row).children().find('.grade').each(function (j, val) {
                        item = {}
                        var propvisionalFlag = $(this).find('.provisional').prop('checked');
                        var belt = $(this).find('.beltTypes').val();
                        var id = $(this).attr('data-id');
                        item = {}
                        item["row"] = i;
                        item["id"] = id;
                        item["belt"] = belt;
                        item["propvisionalFlag"] = propvisionalFlag;
                        item["initialBelt"] = $('.initialBelt_' + id).attr('data-id');

                        jsonObj.push(item);
                    })
                })
                $.ajax({
                    type: "POST",
                    url: "Grading.aspx/SaveGradeData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ data: jsonObj }),
                    success: function (response) {
                        alert('successfully added.')
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
                addNotes();
            }

            function loadData(data) {
                var rowCount = $('.main-container').children().length;

                var divCount = parseInt(data["row"]) + 1;
                if (divCount > rowCount) {
                    var content = '<div id="trash' + divCount + '" class="container row ui-widget-content ui-state-default"> <ul> <li class="ui-state-default ui-state-disabled">' + divCount + ' </li> </ul>    </div> </div>'
                    $('.main-container').append(content); // 

                }
                addData(data);
                $('.close').addClass('hidden');
                $('.fa-pencil-square-o').prop("disabled", false);
                addNotes();
            }



            function addData(data) {
                var divCount = parseInt(data["row"]) + 1;
                var initialBeltID = parseInt(data["initialBelt"]);
                var id = parseInt(data["id"]);
                var Obj = { "2": id, "3": data["FullName"], "4": belTypes[initialBeltID - 1] };
                debugger
                $('#trash' + divCount + ' ul').append(content(Obj));
                var dt = { "2": parseInt(data["id"]) };
                beltFun(dt, data["belt"]);
                $('.initialBelt_' + id).attr('data-id', initialBeltID);

                $('#provisional_' + id).attr('checked', data["propvisionalFlag"] == "True");

                $('.container').droppable({
                    classes: {
                        "ui-droppable-active": "ui-state-highlight"
                    },
                    drop: function (event, ui) {
                        $trash = $(this).closest('.container');
                        deleteImage(ui.draggable);
                    }
                });



                function deleteImage($item) {
                    $item.fadeOut(function () {
                        var $list = $("ul", $trash).length ?
                            $("ul", $trash) :
                            $("<ul class='gallery ui-helper-reset'/>").appendTo($trash);

                        $item.appendTo($list).fadeIn(function () {
                            $item
                                //.animate({ width: "48px" })
                                .find("img")
                                .animate({ height: "36px" });
                        });
                    });
                }
                $("li").draggable({
                    //cancel: "a.ui-icon", // clicking an icon won't initiate dragging
                    revert: "invalid", // when not dropped, the item will revert back to its initial position
                    containment: "document",
                    helper: "clone",
                    cursor: "move"
                });

                addNotes();
            }


            function addNotes() {
                $('.fa-pencil-square-o').unbind().click(function () {
                    //alert($(this).attr('data-id'));
                    var studentId = $(this).attr('data-id');
                    $.ajax({
                        type: "post",
                        url: "Grading.aspx/GetGrading",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ data: studentId }),
                        success: function (response) {
                            $("#myModal").modal()
                            $('#note').val('').val(response.d);
                            $('.btnSaveNote').unbind().click(function () {
                                $("#myModal").modal('toggle')
                                var note = $('#note').val();
                                var d = { studentId: studentId, note: note,eventID: getUrlParameter('ID') };
                                $.ajax({
                                    type: "post",
                                    url: "Grading.aspx/SaveGrading",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ data: d }),
                                    success: function (response) {
                                        alert(response.d);
                                    },
                                    failure: function (response) {
                                        alert(response.d);
                                    }
                                });

                            });

                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                });
            }

        });

    </script>


</body>
</html>
