$(document).ready(function () {

    var table = $("#articles").DataTable({
        responsive: true,
        ajax: {
            url: "/api/articles",
            dataSrc: ""
        },
        columns: [
            {
                data: "articlesGroup.name"
            },
            {
                data: "name",
                render: function (data, type, article) {
                    return "<a href='articles/Details/" + article.id + "'>" + article.name + "</a>";
                }
            },
            {
                data: "description"
            },
            {
                data: "stockQty"
            },
            {
                data: "id",
                render: function (data, type, article) {
                    return "<a  class='btn btn-link' href='Articles/Edit/" + data + "'>Update</a>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn btn-link js-delete' data-article-id=" + data + ">Remove</button>";
                }
            },
            {
                data: "images.0.fileName",
                render: getImg
            }
        ]        
    });

    //new $.fn.dataTable.FixedHeader(table);

    $("#articles").on("click", ".js-delete", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to remove this Product", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/articles/" + button.attr("data-article-id"),
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                    }
                });
            }
        });
    });
        
});

$(document).ready(function () {

    $("#ArticleGroups").DataTable({
        responsive: true,
        ajax: {
            url: "/api/ArticlesGroups",
            dataSrc: ""
        },
        columns: [
            {
                data: "parentId"
            },
            {
                data: "name",
                render: function (data, type, ArticlesGroup) {

                    return "<a href='ArticlesGroups/Details/" + ArticlesGroup.id + "'>" + ArticlesGroup.name + "</a>";
                }
            },
            {
                data: "fileName",
                render: getImg
            },
            {
                data: "description"
            },
            {
                data: "id",
                render: function (data, type, ArticlesGroup) {
                    return "<a  class='btn btn-link' href='ArticlesGroups/Create/" + ArticlesGroup.id + "'>Add</a>";
                }
            },
            {
                data: "id",
                render: function (data, type, ArticlesGroup) {
                    return "<a  class='btn btn-link' href='ArticlesGroups/Edit/" + ArticlesGroup.id + "'>Update</a>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn btn-link js-delete' data-article-id=" + data + ">Remove</button>";
                }
            },
        ]
    });


    $("#ArticleGroups").on("click", ".js-delete", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to remove this Category", function (result) {
            if (result) {
                $.ajax({
                    url: "api/ArticlesGroups/" + button.attr("data-article-id"),
                    method: "DELETE",
                    success: function () {
                        alert("Success")
                        button.parents("tr").remove();
                    }
                });
            }
        });
    });

});

function getImg(data) {
    
    if (data == "" || data == null) {
        return '<img src="http://localhost:1324/Photos/NotAvailable.png" height="80" width="80">';
    }
    return '<img src="' + '../Photos/' + data + '" height="80" width="80">';

}

// Load and Preview Multiple Images
var inputLocalFont = document.getElementById("image-input");
inputLocalFont.addEventListener("change", previewImages, false);

function previewImages() {
    var fileList = this.files;

    var anyWindow = window.URL || window.webkitURL;

    for (var i = 0; i < fileList.length; i++) {
        var objectUrl = anyWindow.createObjectURL(fileList[i]);
        $('.preview-area').append('<img src="' + objectUrl + '" />');
        window.URL.revokeObjectURL(fileList[i]);
        
    }
    $('#CurrentImg').hide();
    
    $(inputLocalFont).hide();
    inputLocalFont = $('<input type="file" class="dimmy" multiple />').appendTo("#filecontainer").get(0);
    inputLocalFont.addEventListener("change", previewImages, false);
}

// Upload Multiple Images, used later in Controller
$(function () {
    $('#image-input').fileupload({
        dataType: 'json',
        done: function (e, data) {
            $.each(data.result.files, function (index, file) {
                $('<p/>').text(file.name).appendTo(document.body);
                
            });
        }
    });
});

$(document).ready(function () {

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

});

function FilterProducts() {
    $('#SearchProducts').DataTable().ajax.reload();
}

function ResetFilters() {
    $('#Colour').val("");
    $('#Price').val("");
    FilterProducts();
}




