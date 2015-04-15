$(document).ready(function () {
    $('#searchText').keyup(function (e) {
        //alert("Ảd");
        delay(function () {
            var key = null;
            if (e.which != null) {
                key = e.which;
            } else {
                key = e.keyCode;
            }
            if ($('#searchText').val().trim() != '' && key != 40 && key != 39 && key != 38 && key != 37) {
                $.ajax({
                    type: "POST",
                    url: "/Search/GetSearchResult",
                    data: "{'search' : '" + $('#searchText').val() + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    beforeSend: function () {
                        $('#results').html('');
                        $('#loadingDiv').show();
                    },
                    complete: function () {
                        $('#loadingDiv').hide();
                    },
                    success: function (data) {
                        //alert(data);
                        $('#results').append(data);

                    },
                    error: function (data) {
                        alert("Error!");
                    }
                });

                $('#contactBox').removeClass('clsHide');
            } else {
                $('#contactBox').removeClass('clsHide');
            }
        }, 700);

    });

    $('#searchText').blur(function () {
        if ($('#searchText').val().trim() == '') {
            $('#contactBox').addClass('clsHide');
        }
    });

    $("#results").on("mouseover", "li", function () {
        $(this).addClass('hover');
        liSelected.removeClass('selected');
    });
    $("#results").on("mouseout", "li", function () {
        $(this).removeClass('hover');
    });

    var li = $('#results li');
    var liSelected;
    $(window).keydown(function (e) {
        if (e.which === 40) {
            $('#results li').removeClass('hover');
            if (liSelected) {
                liSelected.removeClass('selected');
                next = liSelected.next();
                if (next.length > 0) {
                    liSelected = next.addClass('selected');
                } else {
                    liSelected = $('#results li').eq(0).addClass('selected');
                }
            } else {
                liSelected = $('#results li').eq(0).addClass('selected');
            }

        } else if (e.which === 38) {
            $('#results li').removeClass('hover');
            if (liSelected) {
                liSelected.removeClass('selected');
                next = liSelected.prev();
                if (next.length > 0) {
                    liSelected = next.addClass('selected');
                } else {
                    liSelected = $('#results li').last().addClass('selected');
                }
            } else {
                liSelected = $('#results li').last().addClass('selected');
            }
        }
    });

});

var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

//$(document).click(function () {
//    if ($('#searchText').val() != null) {
//        $('#searchText').val('');
       
//    }
//});