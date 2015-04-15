
var textContainer, textareaSize, input;
var autoSize = function () {
    // also can use textContent or innerText
    textareaSize.innerHTML = input.value + '\n';
};

document.addEventListener('DOMContentLoaded', function () {
    textContainer = document.querySelector('.textarea-container');
    textareaSize = textContainer.querySelector('.textarea-size');
    input = textContainer.querySelector('textarea');

    autoSize();
    input.addEventListener('input', autoSize);
});


//create post: upload file and text
function createPost() {
    var data = new FormData();
    //alert("ds");
    var files = $("#uploadFile").get(0).files;
    var textPost = $("#text").val();
    var userId = $("#userId").val();
    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        data.append("UploadedImage", files[0]);
        if (textPost != null)
            data.append("Text", textPost);


        data.append("UserId", userId);
        //alert("SD");
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "/Posts/CreatePost",
            contentType: false,
            processData: false,
            data: data,
            success: function (chuoiJson) {
                //alert(chuoiJson);
                addRowTop(chuoiJson);
            }
        });
    }
    else {
        var dataToBeSend = {
            Text: textPost,
            UserId: userId
        };
        $.ajax({
            type: "POST",
            url: '/Posts/CreatePost',
            data: JSON.stringify(dataToBeSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (chuoiJson) {
                //alert(chuoiJson);
                addRowTop(chuoiJson);
            }
        })
    }

    // Make Ajax request with the contentType = false, and procesDate = false



};

function addRowTop(chuoiJson) {
    var div = document.createElement('div');
    div.innerHTML = chuoiJson;
    $('#postList').prepend(div);
    destroy();
    document.getElementById("text").value = '';
}

$(function () {
    $("#uploadFile").on("change", function () {

        var div = document.createElement('img');
        div.id = "img";
        //div.className = "img-responsive";
        document.getElementById('post').appendChild(div);



        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function () { // set image data as background of div

                $("#img").attr('src', this.result);
                $("#x").show().css("margin-right", "10px");
            }
        }
    });
});
function destroy() {
    $("#img").remove();
    $("#x").hide();
    var control = $('#uploadFile');
    control.replaceWith(control = control.clone(true));
};


function insertComment(postId, userId) {
    //debugger
    //alert("sd");
    var id = "#cmttext_" + postId;
    var text = $(id).val();
    //alert(text + userId);
    document.getElementById("cmttext_" + postId).value = '';

    var dataToBeSend = {
        text: text,
        postId: postId,
        userId: userId
    };
    $.ajax({
        type: "POST",
        url: '/Comment/CreateComment',
        data: JSON.stringify(dataToBeSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (comment) {
            //alert(comment);
            addRowComment(comment, postId);
        },

    })

}

function addRowComment(comment, postId) {
    var table = document.createElement('table');
    table.innerHTML = comment;
    var id = "comment_" + postId;
    document.getElementById(id).appendChild(table);
}

function focusComment(postId) {
    var id = "#cmttext_" + postId;
    $(id).focus();
}


function like(like, postId, userId) {
    //alert(like + "" + postId + "" + userId);
    var dataToBeSend = {
        like: like,
        postId: postId,
        userId: userId
    };
    $.ajax({
        type: "POST",
        url: '/Posts/CreateLike',
        data: JSON.stringify(dataToBeSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (ok) {
            //alert(ok);
            if (ok) {
                var likeId = "#ilike_" + postId;
                var spanLikeId = "#spanlike_" + postId;

                var dislikeId = "#idislike_" + postId;
                var spanDislikeId = "#spandislike_" + postId;
                if (like) {
                    //alert("huhu");
                    $(likeId).attr('class', 'iliked');
                    $(spanLikeId).attr('class', 'spanLiked');
                    $(dislikeId).attr('class', 'idislike');
                    $(spanDislikeId).removeClass();
                }
                else {
                    $(likeId).attr('class', 'ilike');
                    $(spanLikeId).removeClass();
                    $(dislikeId).attr('class', 'idisliked');
                    $(spanDislikeId).attr('class', 'spanDisLiked');
                }
                loadLike(postId, userId);
            }
        },

    })
}

function loadComment() {
    //alert("ada");
    var dataToBeSend = {
        postId: postId,
        commentId: 0
    };
    $.ajax({
        type: "POST",
        url: '/Comment/LoadComment',
        data: JSON.stringify(dataToBeSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chuoiJson) {
            var hienThi = chuoiJson.split("|")[0];
            var cmtId = chuoiJson.split("|")[1];
            //alert(cmtId);
            var id = "comment_" + postId;
            var table = document.createElement("table");
            table.innerHTML = hienThi;
            document.getElementById(id).appendChild(table);

            var cmtHidden = "#comment_hidden_" + postId;
            //alert(cmtHidden);
            $(cmtHidden).val(cmtId);
            //var cmtId = $(cmtHidden).val();
            // var postID = @item.postId;
            //var auto_refresh = setInterval(function() { autoReLoadComment(postID,cmtId) }, 5000);
        },

    })
}

function loadLike(postId, userId) {
    var dataToBeSend = {
        postId: postId,
        userId: userId,
    };
    $.ajax({
        type: "POST",
        url: '/Posts/LoadLike',
        data: JSON.stringify(dataToBeSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chuoiJson) {

            if (chuoiJson.split("|")[0] == "true") {
                var likeId = "#ilike_" + postId;
                var spanLikeId = "#spanlike_" + postId;

                var dislikeId = "#idislike_" + postId;
                var spanDislikeId = "#spandislike_" + postId;

                if (chuoiJson.split("|")[1] == "true") {
                    $(likeId).attr('class', 'iliked');
                    $(spanLikeId).attr('class', 'spanLiked');

                    $(dislikeId).attr('class', 'idislike');
                    $(spanDislikeId).removeClass();
                }
                else {
                    $(likeId).attr('class', 'ilike');
                    $(spanLikeId).removeClass();

                    $(dislikeId).attr('class', 'idisliked');
                    $(spanDislikeId).attr('class', 'spanDisLiked');
                }
            }
            else { }

            var idLike = "sumLike_" + postId;
            document.getElementById(idLike).innerHTML = chuoiJson.split("|")[2] + "Người thích";


            var idDislike = "sumDislike_" + postId;
            document.getElementById(idDislike).innerHTML = chuoiJson.split("|")[3] + "Người không thích";

        },

    })
}


function hoverAcc(e, userId, postId, cmtId) {

    var hide = "#divHidden_" + userId;
    $(hide).remove();
    var dataToBeSend = {
        userId: userId,
    };
    //alert(userId + "_" + postId + "_" + cmtId);
    $.ajax({
        type: "POST",
        url: '/Account/LoadAccountByUserId',
        data: JSON.stringify(dataToBeSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chuoiJson) {
            //alert(chuoiJson);
            var div = document.createElement('div');
            div.className = 'show';
            div.id = "divHidden_" + userId;

            div.innerHTML = chuoiJson;
            if (postId != 0) {
                document.getElementById('post_nickName_' + userId + "_" + postId).appendChild(div);
            }
            else {
                document.getElementById('cmt_nickName_' + userId + "_" + cmtId).appendChild(div);
            }

            div.addEventListener("mouseover", function () {
                mouseover(userId);
            }, false);
            div.addEventListener("mouseout", function () {
                mouseout(userId);
            }, false);
        }
    })
}

function mouseover(userId) {
    //alert(userId);
    var hide = "#divHidden_" + userId;
    $(hide).attr('class', 'show');
}

function mouseout(userId) {
    //alert(userId);
    var hide = "#divHidden_" + userId;
    $(hide).attr('class', 'hidden');
}


function follow(myAccId, userId) {
    //alert(myAccId + "_" + userId);

    var dataToBeSend = {
        myAccId: myAccId,
        userId: userId,
    };
    //alert("Ad");
    $.ajax({
        type: "POST",
        url: '/Relationship/AddFollow',
        data: JSON.stringify(dataToBeSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (ok) {
            //alert(ok);
            var btnFollowId = "#btn_fllow_" + myAccId + "_" + userId;
            $(btnFollowId).attr('value', ok);
        }
    })
}

//function OutHoverAcc(e, userId) {
//    setTimeout(function () { alert("Hello"); }, 3000);
//    var hide = "#divHidden_" + userId;
//    $(hide).remove();
//}


$(function () {
    $("#change_avatar").on("change", function () {

        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file
            reader.onloadend = function () { // set image data as background of div
                $("#image_change_avatar").attr('src', this.result);
                $("#idUndoAvatar").show();
                $("#idUpdateAvatar").show();
            }
        }
    });
});

function undoAvatar() {
    var image = $("#link_avatar_old").val();
    $("#image_change_avatar").attr('src', image);
    var control = $('#change_avatar');
    control.replaceWith(control = control.clone(true));
    $("#idUpdateAvatar").hide();
    $("#idUndoAvatar").hide();
}

function updateAvatar(userId) {
    //alert(userId +1);
    var file = $('#change_avatar')[0].files[0]

    var data = new FormData();
    var files = $("#change_avatar").get(0).files;
    data.append("UploadedImage", files[0]);
    data.append("userId", userId);

    var ajaxRequest = $.ajax({
        type: "POST",
        url: "/AccountDetail/ChangeAvatar",
        contentType: false,
        processData: false,
        data: data,
        success: function (ok) {
            $("#link_avatar_old").val('/Content/images/Avatar/' + file.name);
            $("#idUpdateAvatar").hide();
            $("#idUndoAvatar").hide();
        }
    });

}
