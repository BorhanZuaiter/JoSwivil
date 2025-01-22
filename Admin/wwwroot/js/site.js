$(document).ready(function () {
    $("#ser").keyup(function (event) {
        if (event.keyCode === 13) {
            app.Searchfun();
        }
    });
});
function DeleteConfirm(id) {
    document.getElementById('deleteId').value = id;
    $('#deleteConfirm').modal("toggle");
}
function OpenMessageModal(icon, message) {
    Swal.fire({
        title: icon,
        text: message,
        icon: icon,
        didOpen: () => {
            $('.select2-container').css('z-index', '1050');
        },
        didClose: () => {
            $('.select2-container').css('z-index', '9999');
        }
    });
}
function Search(controller) {
    var ser = $("#ser").val();
    window.location.href = `/${controller}/Index?search=${ser}`;
}
function Delete(controller) {
    var Id = $("#deleteId").val();
    ShowLoadingButton("deleteButton", "deleteLoadingButton");
    $.ajax({
        url: `/${controller}/Delete`,
        type: 'Delete',
        data: { id: Id },
        success: function (response) {
            if (response.status) {
                window.location.reload();
            }
            else {
                ShowLoadingButton("deleteButton", "deleteLoadingButton");
                OpenMessageModal("error", response.message);
            }
        }
    });
}
function Edit(controller, data) {
    $.ajax({
        url: `/${controller}/Edit`,
        type: 'Put',
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.status) {
                window.location.href = `/${controller}/Index`;
            }
            else {
                ShowLoadingButton("EditButton", "LoadingButton");
                OpenMessageModal("error", response.message);
            }
        }
    });
}
function Create(controller, data) {
    $.ajax({
        url: `/${controller}/Create`,
        type: 'Post',
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.status) {
                window.location.href = `/${controller}/Index`;
            }
            else {
                ShowLoadingButton("CreateButton", "LoadingButton");
                OpenMessageModal("error", response.message);
            }
        }
    });
}
function ShowLoadingButton(buttonId, loadingButtonId) {
    var button = document.getElementById(buttonId);
    var loadingButton = document.getElementById(loadingButtonId);

    if (button) {
        button.classList.remove('d-inline');
        button.classList.add('d-none');
    }

    if (loadingButton) {
        loadingButton.classList.remove('d-none');
        loadingButton.classList.add('d-inline');
    }
}
function HideLoadingButton(buttonId, loadingButtonId) {
    var button = document.getElementById(buttonId);
    var loadingButton = document.getElementById(loadingButtonId);

    if (button) {
        button.classList.remove('d-none');
        button.classList.add('d-inline');
    }

    if (loadingButton) {
        loadingButton.classList.remove('d-inline');
        loadingButton.classList.add('d-none');
    }
}
function GetCategories(index) {
    $.ajax({
        url: '/Category/GetDDL',
        type: 'Get',
        success: function (datareturn) {
            $("#CategoryId option").remove();
            $("#CategoryId").append($("<option/>").val(0).text("اختر الفئة"));
            $.each(datareturn, function () {
                if (this.id == index) {
                    $("#CategoryId").append($("<option/>").val(this.id).text(this.name).attr('selected', 'selected'));
                }
                else {
                    $("#CategoryId").append($("<option/>").val(this.id).text(this.name));
                }
            });
        }
    });
}
function GetSellers(index) {
    $.ajax({
        url: '/Seller/GetDDL',
        type: 'Get',
        success: function (datareturn) {
            $("#SellerId option").remove();
            $("#SellerId").append($("<option/>").val(0).text("اختر التاجر"));
            $.each(datareturn, function () {
                if (this.id == index) {
                    $("#SellerId").append($("<option/>").val(this.id).text(this.name).attr('selected', 'selected'));
                }
                else {
                    $("#SellerId").append($("<option/>").val(this.id).text(this.name));
                }
            });
        }
    });
}

