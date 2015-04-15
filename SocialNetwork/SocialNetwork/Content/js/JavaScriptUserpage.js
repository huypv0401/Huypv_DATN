
$(document).ready(function () {
  
    var pageNumber = 1;
    var userId = $('#userId').val();
    function InfiniteScroll() {
        $('#divPostsLoader').html('<img src="/Content/images/Icon/loading.gif" />');
        var dataToBeSend = {
            pageNumber: pageNumber,
            userId: userId
        };
        $.ajax({
            type: "POST",
            url: '/Posts/InfiniteScrollPost',
            data: JSON.stringify(dataToBeSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (post) {
                //alert("ok");
                $('#divPostsLoader').empty();
                addRow(post);
            },
            error: function () {
                alert("Oh noes");
            }
        })
    };

    function addRow(post) {
        var div = document.createElement('div');
        div.innerHTML = post;
        document.getElementById('posts').appendChild(div);
    }

    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {

            //alert(userId);
            pageNumber++;
            InfiniteScroll();
        }
    });
});


