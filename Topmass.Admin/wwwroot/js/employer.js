
function openPageMember(id = -1) {

    var dataString = "id=" + id;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "GET",

        url: '/GroupPage?handler=FormMember&id=' + id,

        success: function (data) {
            $("#contentModal").empty();
            $("#contentModal").append(data);
            $('#formModal').modal('show');
            loadData(id);

        },
        error: function (jqXHR, exception) {
           
        },
        complete: function () {

        },
    });
}

function loadData(groupId) {
   
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "GET",
        url: '/GroupPage?handler=ListDataMember&groupId=' + groupId,
        success: function (data) {
            $("#DataListView").empty();
            $("#DataListView").append(data);
        },
        error: function (jqXHR, exception) {
           
        },
        complete: function () {

        },
    });
}

function OpenChangPassword(id = -1, router = "employee") {

    var dataString = "id=" + id;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "GET",

        url: '/' + router + '?handler=FormChangePassword&id=' + id,

        success: function (data) {
            $("#contentModal").empty();
            $("#contentModal").append(data);
            $('#formModal').modal('show');

        },
        error: function (jqXHR, exception) {
       
        },
        complete: function () {

        },
    });
}
function openFormEdit(id = -1, router = "employee") {
    var dataString = "id=" + id;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "GET",
  
        url: '/' + router + '?handler=FormEdit&id='+id,
        success: function (data) {
            $("#contentModal").empty();
            $("#contentModal").append(data);
            $('#formModal').modal('show'); 
        },
        error: function (jqXHR, exception) {
           
        },
        complete: function () {
            getAllGroup();
            getAllPartner();
            getAllJob();
            loadDataPartner();

            $('.my-select').selectpicker();
            loadCombobx();
        },
    });
}



function openFormAssignee(id = -1, router = "OrderAssignee") {
  
    var dataString = "id=" + id;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "GET",
  
        url: '/' + router + '?handler=openForm&id='+id,
        success: function (data) {
            $("#contentModal").empty();
            $("#contentModal").append(data);

            const sonucModal= document.getElementById('formModal');
            const modalEl = new bootstrap.Modal(sonucModal);
            modalEl.show();
            // setTimeout(() => {
            //     $('#formModal').modal('show'); 
            // }, 2000);
           
         },
        error: function (jqXHR, exception) {

        },
        complete: function () {
            
         },
    });
}
function login() {
    var userName = $("#txtUserName").val();
    var password = $("#txtPassword").val();
    if (userName == "") {
        addError("txtUserName", "yêu cầu nhập tên đăng nhập");
        return;
    }
    else {
        removeError("txtUserName");
    }
    if (password == "") {
        addError("txtPassword", "Yêu cầu nhập mật khẩu");
        return;
    }
    else {
        removeError("txtUserName");
    }
   
    var dataRequest = {
        UserName: userName,
        Password: password
    };
    submitForm("formLogin");
    postAjax('/login?handler=Login', dataRequest)
    then((value) => {
        debugger;
        console.log(value);
        // Expected output: "Success!"
    });

    return;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/login?handler=Login',
        data: dataRequest,
        success: function (data) {
            if (data.success == true) {
                window.location.href = "/";
            }
        },
        error: function (jqXHR, exception) {
          
        },
        complete: function () {
           
        },
    });


}


function postAjax(url, body, callSuccess =nulll, error = null, complete = null) {
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: url,
        data: body,
        success: function (data) {
            resolve('I am doing something');
        },
        error: function (jqXHR, exception) {

        },
        complete: function () {

        },
    });
}
function updateEmployInfo(idEmp) {

    var updateName = getValueControl("txtUpdateName");
    var updateNote = getValueControl("txtUpdateNote");
  
    removeAllEror("formUpdateEmployee");

    if (updateName == "") {
        addError("txtUpdateName", "yêu cầu nhập họ và tên");
        return;
    }
    else {
        removeError("txtUpdateName");
    }
    
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/employee?handler=UpdateInfo',
        data: {
          
            FullName: updateName,
            Id: idEmp,
           
            Noted: updateNote
           
      
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}


function changePassword(reloadPage =true) {
    

    var currentPassword = getValueControl("txtcurrentPassword");
    var newPassword = getValueControl("txtnewPassword");
    var renewPassword = getValueControl("txtrenewPassword");
    removeAllEror("updateChangePassword");

  

    if (newPassword == "") {
        addError("txtnewPassword", "yêu cầu nhập mật khẩu mới");
        return;
    }
    else {
        removeError("txtnewPassword");
    }

    if (renewPassword == "") {
        addError("txtrenewPassword", "yêu cầu nhập lại mật khẩu mới");
        return;
    }
    else {
        removeError("txtrenewPassword");
    }

    if (newPassword != renewPassword) {
        addError("txtrenewPassword", "Hai mật khẩu không trùng khớp");
    }

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/employee?handler=ChangePassword',
        data: {

            newPassword: newPassword
           


        },
        success: function (data) {

            if (reloadPage == true) {
                successAdd(1);
            }
            else {
                openAlertAndClosePopup(1);
            }
           
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}
function changePassword2(reloadPage = false, idEmp, reset = false) {


    var currentPassword = getValueControl("txtcurrentPassword");
    var newPassword = getValueControl("txtnewPassword");
    var renewPassword = getValueControl("txtrenewPassword");
    removeAllEror("updateChangePassword");
    if (reset == false) {

        if (newPassword == "") {
            addError("txtnewPassword", "yêu cầu nhập mật khẩu mới");
            return;
        }
        else {
            removeError("txtnewPassword");
        }

        if (renewPassword == "") {
            addError("txtrenewPassword", "yêu cầu nhập lại mật khẩu mới");
            return;
        }
        else {
            removeError("txtrenewPassword");
        }

        if (newPassword != renewPassword) {
            addError("txtrenewPassword", "Hai mật khẩu không trùng khớp");
        }
    }
    else {
        newPassword = "Vietstar@2021";
    }


    

    var isReset = reset;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/employee?handler=ChangePassword',
        data: {

            newPassword: newPassword,
            id: idEmp,
            resetPass: isReset



        },
        success: function (data) {

            if (reloadPage == true) {
                successAdd(1);
            }
            else {
                openAlertAndClosePopup(1);
            }

        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}

function SaveEmployee(idEmp) {

    var code = getValueControl("txtUserName");
    var fullNametext = getValueControl("txtFullName");
    var dobcb = getValueControl("dob");
    var dateOnboardTemp = getValueControl("dateOnboard");
    var txtLineCodeTemp = getValueControl("txtLineCode");
    var phoneText = getValueControl("txtPhone");
    var passText = getValueControl("txtPass");
    var roleCodeText = getValueControl("txtRoleCode");
    var txtColorCode = getValueControl("txtColor");
   
    var isActiveCb = 1;
    var txtNotedText = getValueControl("txtNoted");
    removeAllEror("mainForm");

    if (fullNametext == "") {
        addError("txtFullName", "yêu cầu nhập họ và tên");
        return;
    }
    else {
        removeError("txtFullName");
    }
    if (phoneText == "") {
        addError("txtPhone", "Yêu cầu nhập số điện thoại");
        return;
    }
    else {
        removeError("txtPhone");
    }

    if (idEmp < 0) {
        if (passText == "") {
            addError("txtPass", "Yêu cầu nhập mật khẩu");
            return;
        }
        else {
            removeError("txtPass");
        }
    }
   

    if (roleCodeText == "") {
        addError("txtRoleCode", "Yêu cầu chọn thông tin vai trò");
        return;
    }
    else {
        removeError("txtRoleCode");
    }

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/employee?handler=AddEmployeee',
        data: {
            UserName: code,
            FullName: fullNametext,
            phone: phoneText,
            Id: idEmp,
            Onboard: dateOnboardTemp,
            LineCode: txtLineCodeTemp,
            RoleCode: roleCodeText,
            Noted: txtNotedText,
            ColorCode :txtColorCode,
            Pass: passText,
            Dob: dobcb, 
            IsActive: isActiveCb
        },
        success: function (data) {
        
            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {
           
        }
    });
}
function SaveGroup(idEmp) {
    var fullNametext = getValueControl("txtName");
    var managerId = getValueControl("cbManagerid");
    var isActiveCb = getValueControl("isActive");
    var txtNotedText = getValueControl("txtNoted");
    removeAllEror("mainForm");
    if (fullNametext == "") {
        addError("txtName", "yêu cầu nhập tên nhóm");
        return;
    }
    else {
        removeError("txtName");
    }
    if (isActiveCb == "") {
        addError("isActive", "Yêu cầu chọn trạng thái");
        return;
    }
    else {
        removeError("isActive");
    }

    if (managerId == "") {
        addError("cbManagerid", "Yêu cầu chọn trưởng nhóm");
        return;
    }
    else {
        removeError("cbManagerid");
    }

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/GroupPage?handler=Add',
        data:
        {
            Name: fullNametext,
            ManagerId: managerId,
            Id: idEmp,
            IsActive: isActiveCb
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}
function openFormGroupEdit(id = -1) {

    var dataString = "id=" + id;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "GET", 

        url: '/GroupPage?handler=FormEdit&id=' + id,

        success: function (data) {
            $("#contentModal").empty();
            $("#contentModal").append(data);
            $('#formModal').modal('show');
        },
        error: function (jqXHR, exception) {
            
        },
        complete: function () {

        },
    });
}
function changePage(pageNumber) {

    var urlcurent = new URL(window.location.href);
    urlcurent.searchParams.set('page', pageNumber);
    window.location.href = urlcurent.toString();

}
var isHasInteract = true;
function deleteRecord(idEmp, routerInput) { 
   
    isHasInteract = false;
   
   
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: routerInput  +'?handler=delete',
        data: {

            Id: idEmp,

        },
        success: function (data) {

            successAdd(idEmp, true);
            isHasInteract = true;
        },
        error: function (jqXHR, exception) {
            showErrorDelete(jqXHR);
            isHasInteract = true;
        },
        complete: function () {
            isHasInteract = true;

        }
    });
}


function deleteRecord2(idEmp, routerInput) { 
    isHasInteract = false;
   
   
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: routerInput  +'?handler=reactive',
        data: {

            Id: idEmp,

        },
        success: function (data) {

            khoiphucXoa(idEmp, true);
            isHasInteract = true;
        },
        error: function (jqXHR, exception) {
            showErrorDelete(jqXHR);
            isHasInteract = true;
        },
        complete: function () {
            isHasInteract = true;

        }
    });
}
function deleteGroupMember(idEmp, groupId = -1, routerInput = "/GroupPage") {
    isHasInteract = false;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: routerInput + '?handler=deleteMember',
        data: {

            Id: idEmp,

        },
        success: function (data) {

            successDeleteMember(idEmp, true, groupId);
            isHasInteract = true;
        },
        error: function (jqXHR, exception) {
            showErrorDelete(jqXHR);
            isHasInteract = true;
        },
        complete: function () {
            isHasInteract = true;

        }
    });
}


function changeResult(idEmp, routerInput = "order") {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
          confirmButton: "btn btn-success",
          cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
      });
      swalWithBootstrapButtons.fire({
        title: "Kết quả ứng tuyển",
        text: "Bạn nhấn nút để kết thúc ứng tuển",
        icon: "warning",
        showCancelButton: true,
        showConfirmButton: false,
        confirmButtonText: "Onboard",
        cancelButtonText: "Kết thúc ứng tuyển",
        reverseButtons: true,
         denyButtonText: `Đóng`
      }).then((result) => {
        if (result.isConfirmed) {
            changeResultTD(idEmp,1,swalWithBootstrapButtons );
         
        } else if (
          /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel
        ) {
             changeResultTD(idEmp,2,swalWithBootstrapButtons );
         
        }
      });

  

}


function changeApply(idEmp) {
    
    var  cvlink1 = getValueControl("inputCvlink");

    var  cbJobId1 = getValueControl("cbJobId1");

    if( cbJobId1  < 1  )
    {
        addError("cbJobId1", "Yêu cầu Vị trí Việc làm");
        return;
    }
        

    if( cvlink1 ==""  ||  cvlink1 =="" )
    {
         addError("inputCvlink", "Yêu cầu có file CV");
         return;
    }


    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
          confirmButton: "btn btn-success",
          cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
      });
      swalWithBootstrapButtons.fire({
        title: "Đẩy qua trang danh sách ứng tuyển",
        text: "Bạn nhấn nút  để xác nhận",
        icon: "warning",
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: "Đẩy qua",
        cancelButtonText: "Bỏ qua",
        reverseButtons: true,
         denyButtonText: `Đóng`
      }).then((result) => {
        if (result.isConfirmed) {
                pushCase(idEmp,1,swalWithBootstrapButtons );
         
        } else if (
          /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel
        ) {
            //  changeResultTD(idEmp,2,swalWithBootstrapButtons );
         
        }
      });

  

}
function changeReturnOrderAlert(idEmp) {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
          confirmButton: "btn btn-success",
          cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
      });
      swalWithBootstrapButtons.fire({
        title: "Đẩy qua trang danh sách khai thác lại",
        text: "Bạn nhấn nút  để xác nhận",
        icon: "warning",
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: "Đẩy qua",
        cancelButtonText: "Bỏ qua",
        reverseButtons: true,
         denyButtonText: `Đóng`
      }).then((result) => {
        if (result.isConfirmed) {
            changeReturnOrder(idEmp,1,swalWithBootstrapButtons );
         
        } else if (
          /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel
        ) {
            //  changeResultTD(idEmp,2,swalWithBootstrapButtons );
         
        }
      });

  

}


function pushCVCTV(idEmp) {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
          confirmButton: "btn btn-success",
          cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
      });
      swalWithBootstrapButtons.fire({
        title: "Đẩy qua đối tác",
        text: "Bạn nhấn nút  để xác nhận",
        icon: "warning",
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: "Đẩy qua",
        cancelButtonText: "Bỏ qua",
        reverseButtons: true,
         denyButtonText: `Đóng`
      }).then((result) => {
        if (result.isConfirmed) {
            pushCaseCTV(idEmp,1,swalWithBootstrapButtons );
         
        } else if (
          /* Read more about handling dismissals below */
          result.dismiss === Swal.DismissReason.cancel
        ) {
            //  changeResultTD(idEmp,2,swalWithBootstrapButtons );
         
        }
      });

  

}
function pushCaseCTV(idEmp,result,swalWithBootstrapButtons) {

    

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/order?handler=pushCaseCTV',
        data:
        {
            Id: idEmp
        },
        success: function (data) {

            if(data.success == false)
                {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Có lỗi sảy ra!",
                        footer: ''
                      });
                 
                }
        
                swalWithBootstrapButtons.fire({
                    title: "Đã chuyển qua thành công!",
                    text: ".",
                    icon: "success"
                  });
          

        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });

   
}

function changeReturnOrder(idEmp,result,swalWithBootstrapButtons) {

    

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/order?handler=changeReturnOrder',
        data:
        {
            Id: idEmp
        },
        success: function (data) {

            if(data.success == false)
                {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Có lỗi sảy ra!",
                        footer: ''
                      });
                 
                }
        
            if(result ==1)
                {
                    swalWithBootstrapButtons.fire({
                        title: "Đã chuyển qua thành công!",
                        text: "Danh sách ứng tuyển.",
                        icon: "success"
                      });
                }
             else  {
               
             }
          

        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });

   
}
function pushCase(idEmp,result,swalWithBootstrapButtons) {

    

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/order?handler=ChangeStatusApply',
        data:
        {
            OrderId: idEmp
        },
        success: function (data) {

            if(data.success == false)
                {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Có lỗi sảy ra!",
                        footer: ''
                      });
                 
                }
        
            if(result ==1)
                {
                    swalWithBootstrapButtons.fire({
                        title: "Đã chuyển qua thành công!",
                        text: "Danh sách ứng tuyển.",
                        icon: "success"
                      });
                }
             else  {
                // swalWithBootstrapButtons.fire({
                //     title: "Ghi nhận kết quả tuyển dụng",
                //     text: "Done",
                //     icon: "error"
                //   });
             }
          

        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });

   
}
function changeResultTD(idEmp,result,swalWithBootstrapButtons) {

    

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/order?handler=ChangeResult',
        data:
        {
            Result: result,
            Id: idEmp
        },
        success: function (data) {

            if(data.success == false)
                {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Có lỗi sảy ra!",
                        footer: ''
                      });
                 
                }
        
            if(result ==1)
                {
                    swalWithBootstrapButtons.fire({
                        title: "Ghi nhận kết quả tuyển dụng!",
                        text: "Onboard.",
                        icon: "success"
                      });
                }
             else  {
                swalWithBootstrapButtons.fire({
                    title: "Ghi nhận kết quả tuyển dụng",
                    text: "Done",
                    icon: "error"
                  });
             }
          

        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });

   
}

function confirmDelete(idEmp, routerInput = "employee") {
    var urlrouter = "/" + routerInput;
    if (isHasInteract == false) {
        return;
    }
    isHasInteract = false;
    if (idEmp < 1) {
        isHasInteract = true;
        return;    
    }
    

    Swal.fire({
        title: "Bạn chắc chắn xoá?",
        text: "Cân nhắc trước khi nhấn nút Xoá!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xoá!"
    }).then((result) => {
        if (result.isConfirmed) {
            deleteRecord(idEmp, routerInput);
        }
        else {
            isHasInteract = true;
        }
    }); 
}

function reActive(idEmp, routerInput = "employee") {
    var urlrouter = "/" + routerInput;
    if (isHasInteract == false) {
        return;
    }
    isHasInteract = false;
    if (idEmp < 1) {
        isHasInteract = true;
        return;    
    }
    

    Swal.fire({
        title: "Bạn chắc chắn thao tác?",
        text: "Active lại nhân viên!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "khôi phục!"
    }).then((result) => {
        if (result.isConfirmed) {
            deleteRecord2(idEmp, routerInput);
        }
        else {
            isHasInteract = true;
        }
    }); 
}



function SaveMember(groupId) {
 
    var isActiveCb = getValueControl("cbmemberId");

    if (isActiveCb == "-1")
        isActiveCb = "";
   
    removeAllEror("mainFormMember");
    if (isActiveCb == "") {
        addError("cbmemberId", "yêu cầu chọn thành viên để thêm vào nhóm");
        return;
    }
    else {
        removeError("cbmemberId");
    }
  
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/GroupPage?handler=AddMember',
        data:
        {
            GroupId: groupId,
            MemberId: isActiveCb
        },
        success: function (data) {

            successAddMember(groupId, false, groupId);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}




function openFormCommon(id = -1, controller = "partner") {

    var dataString = "id=" + id;
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "GET",

        url: '/' + controller + '?handler=FormEdit&id=' + id,

        success: function (data) {
            $("#contentModal").empty();
            $("#contentModal").append(data);
            $('#formModal').modal('show');



        },
        error: function (jqXHR, exception) {
            debugger;
        },
        complete: function () {

        },
    });
}



function SavePartner(idEmp) {

    var code = getValueControl("txtCode");
    var fullNametext = getValueControl("txtFullName");
    var isActiveCb = getValueControl("isActive");
    var txtNotedText = getValueControl("txtNoted");
    var txtShortNameValue = getValueControl("txtShortName");
    var txtTaxCodeValue  = getValueControl("txtTaxCode");

    var parrentChild = document.getElementById("lisItemAdd")
    var allInputList =  parrentChild.querySelectorAll("input");

    var arrayList = [];

   
    for (let index = 0; index < allInputList.length; index++) {
        const intpuText = allInputList[index];
        var textValueTemp = intpuText.value;
        var idElement = intpuText.getAttribute("customvalue");
        var itemAdd = {
            Text : textValueTemp ,
            Id:  idElement,
            RelId: idEmp,
            Type : "1"
        };
        arrayList.push(itemAdd);
    }



    var parrentChild2 = document.getElementById("listProject")
    var allInputList2 =  parrentChild2.querySelectorAll("input");

    var arrrayProject = [];

   
    for (let index = 0; index < allInputList2.length; index++) {
        const intpuText = allInputList2[index];
        var textValueTemp = intpuText.value;
        var idElement = intpuText.getAttribute("customvalue");
        var itemAdd = {
            Text : textValueTemp ,
            Id:  idElement,
            RelId: idEmp,
            Type : "2"
        };
        arrrayProject.push(itemAdd);
    }



    removeAllEror("mainForm");
 

    
    if (fullNametext == "") {
        addError("txtFullName", "yêu cầu nhập tên đối tác");
        return;
    }
    else {
        removeError("txtFullName");
    }
      if (isActiveCb == "") {
        addError("isActiveCb", "Yêu cầu chọn trạng thái");
        return;
    }
    else {
        removeError("isActiveCb");
    }

    var bodyResquest = { 
            Name: fullNametext,
            addressList: arrayList,
            projectList: arrrayProject,
            Id: idEmp,
            ShortName: txtShortNameValue,
            TaxCode: txtTaxCodeValue,
            Noted: txtNotedText,
            IsActive: isActiveCb
     };

   
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/partner?handler=Add',
        data:bodyResquest,
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}


function SaveMasterData2(idEmp, namecontroller = "nganhnghe") {

    var code = getValueControl("txtCode");
    var fullNametext = getValueControl("txtFullName");
    var isActiveCb = getValueControl("isActive");
    var txtNotedText = getValueControl("txtNoted");

    
    var txtExtraText = getValueControl("txtExtra");
    var applyFor = getValueControl("applyFor");
    removeAllEror("mainForm");

    if (fullNametext == "") {
        addError("txtFullName", "yêu cầu nhập tên đối tác");
        return;
    }
    else {
        removeError("txtFullName");
    }



    if (isActiveCb == "") {
        addError("isActiveCb", "Yêu cầu chọn trạng thái");
        return;
    }
    else {
        removeError("isActiveCb");
    }

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/' + namecontroller +'?handler=Add',
        data: {

            Name: fullNametext,

            Id: idEmp,

            Noted: txtNotedText,
            Extra: txtExtraText,
            applyFor: applyFor,

            IsActive: isActiveCb
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}

function SaveMasterData(idEmp, namecontroller = "nganhnghe") {

    var code = getValueControl("txtCode");
    var fullNametext = getValueControl("txtFullName");
    var isActiveCb = getValueControl("isActive");
    var txtNotedText = getValueControl("txtNoted");


    removeAllEror("mainForm");

    if (fullNametext == "") {
        addError("txtFullName", "yêu cầu nhập tên đối tác");
        return;
    }
    else {
        removeError("txtFullName");
    }



    if (isActiveCb == "") {
        addError("isActiveCb", "Yêu cầu chọn trạng thái");
        return;
    }
    else {
        removeError("isActiveCb");
    }

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/' + namecontroller +'?handler=Add',
        data: {

            Name: fullNametext,

            Id: idEmp,

            Noted: txtNotedText,

            IsActive: isActiveCb
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}


function saveJob(idEmp) {


    var fullNametext = getValueControl("txtName");
    var txtWarrantyValue = getValueControl("txtWarrantydate");

    var cbField = getValueControl("cbField");
    var cbCareerId = getValueControl("cbCareerId");
    var txtNotedText = getValueControl("txtNoted");

    var contentText = getValueControl("txtContent");
    var shortDesText = getValueControl("txtShortDes");
    var cbisActive = getValueControl("cbisActive");
    var inputfileValue = getValueControl("txtinputfile");
    var partnerIdValue = getValueControl("cbpartnerId4");
    var projectIdValue = getValueControl("cbProject");

    removeAllEror("mainForm");

    if (fullNametext == "") {
        addError("txtName", "yêu cầu nhập tiêu đề việc làm");
        return;
    }
    else {
        removeError("txtName");
    }

   

    if (cbField == "") {
        addError("cbField", "Yêu cầu nhập lĩnh vực");
        return;
    }
    else {
        removeError("cbField");
    }

    if (txtWarrantyValue == "" || txtWarrantyValue < 1) {
        addError("txtWarrantydate", "Yêu cầu nhập số ngày bảo hành cho job");
        return;
    }
    else {
        removeError("txtWarrantydate");
    }



    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/job?handler=Add',
        data: {
             Name: fullNametext,
            Field: cbField,
            Id: idEmp,
            CareerId: "1",
            Noted: txtNotedText,
            Content: contentText,
            ShortDes: shortDesText,
            IsActive: cbisActive,
            WarrantyDate : txtWarrantyValue,
            Inputfile : inputfileValue,
            partnerId:  partnerIdValue,
            projectId: projectIdValue
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}

function SaveCandidatenew(idEmp) {

    var txtFullName = getValueControl("txtFullName");
    var txtPhone = getValueControl("txtPhone");
    var dobcb = getValueControl("dob");
    var txtNoted = getValueControl("txtNotedCand");

    var txtEmail = getValueControl("txtEmail");
    var txtSourceCode = 0;
    if(txtSourceCode =="")
        {
            txtSourceCode = 0;
        }


    var txtShortDes = getValueControl("txtNotedCand");

    var cbisActive = 1;

    var fileCvLinkInput = getValueControl("inputCvlink1");

    removeAllEror("mainForm");

    if (txtFullName == "") {
        addError("txtFullName", "yêu cầu nhập họ và tên");
        return;
    }
    else {
        removeError("txtFullName");
    }
    if (txtPhone == "") {
        addError("txtPhone", "Yêu cầu nhập số điện thoại");
        return;
    }
    else {
        removeError("txtPhone");
    }
    


    if (cbisActive == "") {
        addError("cbisActive", "yêu cầu nhập trạng thái");
        return;
    }
    else {
        removeError("cbisActive");
    }


    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/candidate?handler=Add',
        data: {
           
            Phone: txtPhone,
            Name: txtFullName,
            Email: txtEmail,
            Id: idEmp,
            Dob: dobcb,
            AvatarLink: "",
            CVLink: fileCvLinkInput,
            ShortDes:"",
            Noted: txtNoted ,
            Source : 0,
            IsActive: 1
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}


function SaveCandidate(idEmp) {

    var txtFullName = getValueControl("txtFullName");
    var txtPhone = getValueControl("txtPhone");
    var dobcb = getValueControl("dob");
    var txtNoted = getValueControl("txtNoted");
    var txtEmail = getValueControl("txtEmail");
    var txtSourceCode = getValueControl("cbSource");

    if(txtSourceCode =="")
        {
            txtSourceCode = 0;
        }


    var txtShortDes = getValueControl("txtShortDes");

    var cbisActive = getValueControl("cbisActive");

    var fileCvLinkInput = getValueControl("inputCvlink1");
    var avatarFileInput = getValueControl("avatarFile");
    removeAllEror("mainForm");

    if (txtFullName == "") {
        addError("txtFullName", "yêu cầu nhập họ và tên");
        return;
    }
    else {
        removeError("txtFullName");
    }
    if (txtPhone == "") {
        addError("txtPhone", "Yêu cầu nhập số điện thoại");
        return;
    }
    else {
        removeError("txtPhone");
    }
    


    if (cbisActive == "") {
        addError("cbisActive", "yêu cầu nhập trạng thái");
        return;
    }
    else {
        removeError("cbisActive");
    }


    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/candidate?handler=Add',
        data: {
           
            Phone: txtPhone,
            Name: txtFullName,
            Email: txtEmail,
            Id: idEmp,
            Dob: dobcb,
            AvatarLink: avatarFileInput,
            CVLink: fileCvLinkInput,
            ShortDes: txtShortDes,
            Noted: txtNoted ,
            Source : txtSourceCode,
            IsActive: cbisActive
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}

function saveOrderMarketting(idEmp) {
     //var ProjectIdValue = getValueControl("cbProject");
    var cbcandidateId = getValueControl("cbcandidateId");
    var cbJobId = getValueControl("cbJobId");
    // var cbPartnerId = getValueControl("cbpartnerId2");
    var txtShortDes = getValueControl("txtShortDes");
    var txtNoted = getValueControl("txtNoted");
    var txtSourceCode = getValueControl("txtNoted");
    var cvLink = getValueControl("inputCvlink");
    removeAllEror("mainForm2");
    if (cbcandidateId == "") {
        addError("cbcandidateId", "Chọn thông tin ứng cử viên");
        return;
    }
    else {
        removeError("cbcandidateId");
    }
    if (cbJobId == "") {
        addError("cbJobId", "Yêu cầu chọn thông tin vị trí");
        return;
    }
    else {
        removeError("cbJobId");
    }
   
      $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/order?handler=AddMarketting',
        data: {
            CandidateId: cbcandidateId,
            JobId: cbJobId,
            CVLink: cvLink,
            ShortDes: txtShortDes,
            Noted: txtNoted, 
            id: idEmp
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}
function assingeeOrder(idEmp) {
   
   var asssigneeId = getValueControl("cbAssingeeId");
   removeAllEror("mainForm2");
   if (asssigneeId == "") {
       addError("cbAssingeeId", "Chọn thông tin TC");
       return;
   }
   else {
       removeError("cbAssingeeId");
   }
     $.ajax({
       headers: {
           "RequestVerificationToken":
               $('input[name="__RequestVerificationToken"]').val()
       },
       type: "POST",
       datatype: "JSON",
       url: '/OrderAssignee?handler=Assingee',
       data: {
            Assignee: asssigneeId,
           id: idEmp
       },
       success: function (data) {

           successAdd(idEmp);
       },
       error: function (jqXHR, exception) {
           showError(jqXHR);
       },
       complete: function () {

       }
   });
}


function saveOrder(idEmp) {
    var ProjectIdValue = getValueControl("cbProject");
    var cbcandidateId = getValueControl("cbcandidateId");
    var cbJobId = getValueControl("cbJobId1");
    var cbPartnerId = getValueControl("cbpartnerId2");
    var txtShortDes = getValueControl("txtShortDes");
    var txtNoted = getValueControl("txtNoted");
    var cvLink = getValueControl("inputCvlink");
    removeAllEror("mainForm2");
    if (cbcandidateId == "") {
        addError("cbcandidateId", "Chọn thông tin ứng cử viên");
        return;
    }
    else {
        removeError("cbcandidateId");
    }
    if (cbJobId == "") {
        addError("cbJobId", "Yêu cầu chọn thông tin vị trí");
        return;
    }
    else {
        removeError("cbJobId");
    }
 
      $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/order?handler=Add',
        data: {
         
            CandidateId: cbcandidateId,
            JobId: cbJobId, 
            ProjectId:  ProjectIdValue,
            PartnerId : cbPartnerId,
            CVLink: cvLink,
            ShortDes: txtShortDes,
            Noted: txtNoted, 
            id: idEmp

        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}
function saveOrder2(idEmp) {

    var cbcandidateId = getValueControl("cbcandidateId");
    var cbJobId = getValueControl("cbJobId1");
    var phoneNumber = getValueControl("txtPhoneCall");
    var txtShortDes = getValueControl("txtShortDes");
    var txtNoted = getValueControl("txtNoted");
    var cvLink = getValueControl("inputCvlink");
    removeAllEror("mainForm2");
    if (cbcandidateId == "") {
        addError("cbcandidateId", "Chọn thông tin ứng cử viên");
        return;
    }
    else {
        removeError("cbcandidateId");
    }
    if (cbJobId == "") {
        addError("cbJobId", "Yêu cầu chọn thông tin vị trí");
        return;
    }
    else {
        removeError("cbJobId");
    }
   
      $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/order?handler=Add',
        data: {
         
            CandidateId: cbcandidateId,
            JobId: cbJobId, 
            CVLink: cvLink,
            ShortDes: txtShortDes,
            PhoneNumber: phoneNumber,
            Noted: txtNoted, 
            id: idEmp

        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}


function saveInfoCV(idEmp) {
    var cbcandidateId = getValueControl("txtcanddiateInfo")
    var fullName = getValueControl("txtFullName");
    var dobTextBox = getValueControl("dob");
    var phoneInputText = getValueControl("txtPhoneCall");
    var txtEmail= getValueControl("txtEmail");
    var txtShortDes = getValueControl("txtShortDes");
    var txtNoted = getValueControl("txtNoted");
    var cvLink = getValueControl("inputCvlink");
    var notedCan = getValueControl("txtNoted");
    var cbJobId = getValueControl("cbJobId1");

    removeAllEror("mainForm2");
    if (fullName == "") {
        addError("txtFullName", "Điền thông tin họ và tên");
        return;
    }
    else {
        removeError("txtFullName");
    }

    if (phoneInputText == "") {
        addError("phoneInputText", "Điền thông tin số điện thoại");
        return;
    }
    else {
        removeError("phoneInputText");
    }
    var bodyData = {
        PhoneNumber: phoneInputText,
        FulName: fullName,
        Email: txtEmail,
        Dob: dobTextBox,
        RequestId: cbcandidateId,
        JobId: cbJobId ,
        ShortDes : txtShortDes ,
        CVLink: cvLink,
        Noted: txtNoted ,


    };
    
      $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/order?handler=AddInfo',
        data: bodyData ,
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}

function saveCanddiateOrder(idEmp) {

    var cbcandidateId = getValueControl("cbcandidateId");
    var cbJobId = getValueControl("cbJobId1");
    var txtFullName = getValueControl("txtFullName");
    var dobCan = getValueControl("dob");
    var phoneNumber = getValueControl("txtPhone");

    var EmailText = getValueControl("txtEmail");
    var txtShortDes = "";
    var txtNotedCand = getValueControl("txtNotedCand");
    var cvLink = getValueControl("inputCvlink1");
    var statusAplly = getValueControl("statusAplly");
    var txtShortDesOrder = getValueControl("txtShortDesOrder");
    removeAllEror("mainForm");
    if (txtFullName == "") {
        addError("txtFullName", "yêu cầu nhập họ và tên");
        return;
    }
    else {
        removeError("txtFullName");
    }
    if (phoneNumber == "") {
        addError("txtPhone", "Yêu cầu nhập số điện thoại");
        return;
    }
    else {
        removeError("txtPhone");
    }
   
    if (statusAplly < 1) {
        addError("statusAplly", "Yêu cầu chọn trạng thái");
        return;
    }
    else {
        removeError("statusAplly");
    }

    var valueDateFrom = getValueControl("dateFrom");
    if( statusAplly ==12 || statusAplly ==9)
    {
         if(isBlank(valueDateFrom))
         {
            addError("dateFrom", "Yêu cầu chọn ngày");
         }
         else 
         {
             removeError("dateFrom");
         }
       
    }
  


    

    // if (cvLink == "" &&  txtShortDes =="") {
    //     addError("txtShortDes", "Yêu cầu chọn file CV hoặc link tài liệU ");
    //     return;
    // }
    // else {
    //     removeError("txtShortDes");
    // }

 
   
      $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/Candidate?handler=AddWidthOrder',
        data: {
            
            CandidateId: cbcandidateId,
            ShortDes: txtShortDes,
            JobId: cbJobId, 
            CVLink: cvLink,
            statusAplly: statusAplly,
            Phone: phoneNumber,
            Email: EmailText,
            Name: txtFullName,
            Dob: dobCan,
            NotedCan: txtNotedCand, 
            ShortDesOrder :txtNotedCand
         

        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}


function saveImpact(orderCode) {

 
    
    var cbPartnerId2Value = getValueControl("cbpartnerId3");
    var cbSelectStatus1 = getValueControl("cbSelectStatus");
  
    var dateFromVal1 = getValueControl("dateFrom");

    var txtTimer1 = getValueControl("txtTimer");

    var txtPlace1 = getValueControl("txtPlace");
    var txtNotedExtra1 = getValueControl("txtNotedExtra");
 
    var radioOther =   document.getElementById('checkDefaultAddress').checked;

    if(radioOther == true)
    {

        txtPlace1 = $('#cbAddress :selected').text(); 
    } 



    removeAllEror("formSecond");

    

    if (cbSelectStatus1 == "") {
        addError("cbSelectStatus", "Chưa chọn tình trạng trạng thái hồ sơ");
        return;
    }
    else {
        removeError("cbSelectStatus");

    }

  

    if(cbSelectStatus1 == 1 )
    {

        if (dateFromVal1 == "") {
            addError("dateFrom", "Chưa chọn thông tin ngày");
            return;
        }
        else {
            removeError("dateFrom");
    
        }


        if (txtTimer1 == "") {
            addError("txtTimer", "Chưa chọn thông tin giờ ");
            return;
        }
        else {
            removeError("txtTimer");
    
        }

        
    }
    if (isBlank(txtNotedExtra1)) {
        addError("txtNotedExtra", "Chưa có thông tin ghi chú ");
        return;
    }
    else {
        removeError("txtNotedExtra");

    }
    

if( cbSelectStatus1 == 47)
    {
        if (isBlank(dateFromVal1)) {
            addError("dateFrom", "Cung cấp ngày phỏng vấn");
            return;
        }
        else {
            removeError("dateFrom");
    
        }
    }


    if( cbSelectStatus1 == 12)
    {
        if (isBlank(dateFromVal1)) {
            addError("dateFrom", "Cung cấp ngày Onboard");
            return;
        }
        else {
            removeError("dateFrom");
    
        }
    }

    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/OrderDetail?handler=Add',
        data: {
            OrderCode: orderCode,
            Noted: txtNotedExtra1,
            dateFrom:  dateFromVal1,
            txtTimer: txtTimer1,
            NewStatus: cbSelectStatus1, 
            txtPlace : txtPlace1,
            radioOtherAdress:  radioOther,
            PartnerId: cbPartnerId2Value
        },
        success: function (data) {
          
            successAddImpact(orderCode);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}

function openFormModal()
{
    $('#exampleModalCenter').modal('show'); 
}

function closeForm()
{
    $('#exampleModalCenter').modal('hide'); 
} 

function closeFormUser()
{
    $('#dataUser').modal('hide'); 
} 


function UploadImage1() {
    alert ("3");
    return;
    var fileInput = document.getElementById("fileImport");
    if (fileInput.files.length < 1)
        return;
    var fileAccess = fileInput.files[0];
    var formData = new FormData();
    formData.append('FileRequest', fileAccess);
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        processData: false,  // tell jQuery not to process the data
        contentType: false,
        url: '/Candidate?handler=mportSource',
        data: formData ,
        success: function (data, reponse) {
               
               Swal.fire({
           
                position: "center",
                    icon: "success",
                    title: "import thành công",
                    showConfirmButton: false,
                    timer: 2000
                }).then((result) => {
                    // loadData(groupId);
                });
            
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });

}

function  exportFileDashboard () {
    
    Swal.fire({
        title: 'Đang chuẩn bị thông tin file '
        });
        Swal.showLoading();

        var fromDate = getValueControl("fromDate");
        var endDate = getValueControl("toDate");
        var jobId = getValueControl("cbjob");
        var statusId = getValueControl("cbstatus2");
        $.ajax({
            headers: {
                "RequestVerificationToken":
                    $('input[name="__RequestVerificationToken"]').val()
            },
            type: "POST",
            url: '/FileReport?handler=FileDashboard',

            data: {
                From: fromDate,
                To:  endDate,
                job: jobId, 
                status: statusId
            },
            success: function (data) {

                    Swal.fire({
                        title: "File báo cáo đã sẵn sàng",
                        text: "nhấn nút tải xuống để tải file về!",
                        icon: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#3085d6",
                        cancelButtonColor: "#d33",
                        confirmButtonText: "Tải file xuống"
                    }).then((result) => {
                        if (result.isConfirmed && data.success==true) {

                            var link = document.createElement("a");
                            var fileName= "reportDashboard_"+ new Date().getTime() + ".xlsx";
                            link.setAttribute('download', fileName);
                            link.href = data.linkResult;
                            document.body.appendChild(link);
                            link.click();
                            link.remove(); 
                        }
                    });

             },
            error: function (jqXHR, exception) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                    footer: '<a href="#">Why do I have this issue?</a>'
                  });
            },
            complete: function () {
    
            },
        });

       
}


function isVietnamesePhoneNumber(number) {
    
    return /([\+84|84|0]+(3|5|7|8|9|1[2|6|8|9]))+([0-9]{8})\b/.test(number);
  }

  function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
  }
function saveOrderCandidateMarketting(idEmp,waysave= false) {
    //var ProjectIdValue = getValueControl("cbProject");
   var cbcandidateId = getValueControl("cbcandidateId");
    var fullName = getValueControl("txtFullName");

    var birthDay = getValueControl("dob");

    var emailCand = getValueControl("txtEmail");

    var phoneCand = getValueControl("txtPhone");

    var cbSource  = getValueControl("cbSource");

   var txtNotedCand = getValueControl("txtNoted");

   var cvLink = getValueControl("inputCvlink");

   var cbJobId = getValueControl("cbJobId");

   // var cbPartnerId = getValueControl("cbpartnerId2");
   var txtShortDes = getValueControl("txtShortDes");
   var txtNotedOrder = getValueControl("txtNotedOrder");
   var txtSourceCode = getValueControl("txtNoted");
  
   removeAllEror("mainForm");
   if (cbcandidateId == "") {
       addError("cbcandidateId", "Chọn thông tin ứng cử viên");
       return;
   }
   else {
       removeError("cbcandidateId");
   }

    if (fullName == "") {
            addError("txtFullName", "Chưa chọn tên");
            return;
    }
    else {
            removeError("txtFullName");
    }


    if( phoneCand == "" && emailCand == "")
    {
        addError("txtPhone", "Chọn điền email hoặc số điện thoại");

    
        return;
    }
   
    else
    {
        if( phoneCand.length > 0)
        {
            if(!isVietnamesePhoneNumber(phoneCand))
            {
                addError("txtPhone", "Số điện thoại không đúng format, vui lòng kiểm tra lại");
                return;
             
            }
            else 
            {
                removeError("txtPhone");
            }
        }

        if( emailCand.length > 0)
        {
            if(!validateEmail(emailCand))
            {
                addError("txtEmail", "Email không đúng format, vui lòng kiểm tra lại");
                return;
            }
            else 
            {
                removeError("txtEmail");
            }
        }
     }


    if( waysave ==true)
    {
        if( cbJobId == ""  || cbJobId == "-1")
            {
                addError("cbJobId", "Chưa chọn thông tin vị trí việc làm");
                return;
            }
            else 
            { 
                // removeError("cbJobId");
                // if( cvLink =="" &&  txtShortDes =="" )
                // {
                //     addError("inputCvlink", "Chưa đính kèm CV hoặc link tài liệu");
                //     return;
                // }
                // else {
                //     removeError("inputCvlink");
                //     removeError("txtShortDes");
                // }
        
            }

    }
    else {
        cbJobId  = -1;
    }
   
     $.ajax({
       headers: {
           "RequestVerificationToken":
               $('input[name="__RequestVerificationToken"]').val()
       },
       type: "POST",
       datatype: "JSON",
       url: '/candidate?handler=AddCandidateOrderMarketting',
       data: {
            Id: cbcandidateId,
           Name: fullName,
           Dob: birthDay,
           Email: emailCand,
           PhoneNumber: phoneCand,
           Source: cbSource,
           SaveOrder: waysave,
           CVLink: cvLink,
           JobId: cbJobId,
           Document: txtShortDes,
           NotedOrder: txtNotedOrder,
           NotedCan: txtNotedCand, 
           id: idEmp
       },
       success: function (data) {

           successAdd(idEmp);
       },
       error: function (jqXHR, exception) {
        debugger;
           showError(jqXHR);
       },
       complete: function () {

       }
   });
}


document.addEventListener("DOMContentLoaded", () => {

    // const config = {
    //     type: 'pie',
    //     data: data,
    //   };
    //   const data = {
    //     labels: [
    //       'Red',
    //       'Blue',
    //       'Yellow'
    //     ],
    //     datasets: [{
    //       label: 'My First Dataset',
    //       data: [300, 50, 100],
    //       backgroundColor: [
    //         'rgb(255, 99, 132)',
    //         'rgb(54, 162, 235)',
    //         'rgb(255, 205, 86)'
    //       ],
    //       hoverOffset: 4
    //     }]
    //   };

    //     const ctx = document.getElementById('myChart');

    //     new Chart(ctx,config);
});

function OpenPageOrder()
{
    window.location.href ="/OrderDetailNew?OrderId=-1";
}

function editOnboard(){
   

}



function SaveOnboard(idEmp) {

    var onboarDay = getValueControl("dateOnboard");
    var cbOnboarded = getValueControl("cbOnboarded");

    if( cbOnboarded ==1)
    {
        if(onboarDay =='')
        {
            return;
        }
    }
     $.ajax({
        headers: {
                "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/OrderOnboard?handler=Update',
        data: {
           
            OrderId: idEmp,
            Result:  cbOnboarded,
            DateOnboard: onboarDay
        },
        success: function (data) {

            successAdd(idEmp);
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });
}


function openFormModalChart(itemDAta)
{
   renderChartCV(itemDAta);
    $('#exampleModalCenter').modal('show'); 
}

function opneFormActiveUser()
{ 
     $('#dataUser').modal('show'); 

}

function updateArticle() {

    removeAllEror("mainForm");
     var titleArticle = getValueControl("txtTitle");
     var shortDesArticle = getValueControl("ShortDes");
     var keywordArticle = getValueControl("keyword");
     var linkImageArticle = getValueControl("CoverImage");
     var categryIdLinkArticle = getValueMutitpleSelect();
    var contentArticle = getContentEditor("contentEditor");
    var slugArticle = getValueControl("txtSlug");
     if (titleArticle == "") {
        addError("txtTitle", "yêu cầu nhập ");
        return;
    }
    else {
        removeError("txtTitle");
    }

   

    if (shortDesArticle == "") {
        addError("ShortDes", "yêu cầu nhập");
        return;
    }
    else {
        removeError("ShortDes");
    }

    if (linkImageArticle == "") {
        addError("CoverImage", "yêu cầu nhập");
        return;
    }
    else {
        removeError("CoverImage");
    }


    if (categryIdLinkArticle == "") {
        addError("contentEditor", "yêu cầu nhập");
        return;
    }
    else {
        removeError("contentEditor");
    }
     var bodyRequest =  {
        id : getValueControl("idinput"), 
        titleArticle: titleArticle,
        slugArticle: slugArticle,
        shortDesArticle: shortDesArticle, 
        keywordArticle : keywordArticle, 
        linkImageArticle : linkImageArticle.split('/').pop(),
        categryIdLinkArticle : categryIdLinkArticle , 
        contentArticle : contentArticle

     };
   
     $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/ArticleDetail?handler=Update',
        data: bodyRequest,
        success: function (data) {
           
            successAdd(getValueControl("idinput"));
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {
           
        }
    });
}


// ntd
function UpdatedocumnetStatus() {
    var cbStatusDucomentText = getValueControl("cbStatusDucoment");
    var txtNotedDocument = getValueControl("txtNoteDocumnet");
    debugger;
    var linkFile = getValueControl("inputFileDocument");
    var cbReasonreject = getValueControl("cbStatusReasonStatus");
    if (cbStatusDucomentText  < 0 ) {
        Swal.fire({
            icon: "error",
            title: "Có lỗi xảy ra.",
            text: "Chưa chọn trạng thái!",
            footer: ''
          });
          return;
    }


    if (cbStatusDucomentText == 2 ) {

        if (cbReasonreject == "" || cbReasonreject < 0) {
            Swal.fire({
                icon: "error",
                title: "yêu cầu nghiệp vụ",
                text: "Chưa có thông tin  lý do để từ chối",
                footer: ''
            });
            return;

        }
    }
    else {
        cbReasonreject = -1;
    }
 

    var bodyRequest =  {
        id : getValueControl("idinput"), 
        statusChange :  cbStatusDucomentText,
        notedChange: txtNotedDocument,
        linkFile: linkFile,
        reasonReject: cbReasonreject
    };

   
     $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/NTDDetail?handler=UpdateBusinessDocument',
        data: bodyRequest,
        success: function (data) {
            debugger;
            successAdd(getValueControl("idinput"));
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {
           
        }
    });


    
}

function updateDocumentStatus2() {

    
}

function updateInformationAccout() {

   
    var cbStatusDucomentText1 = getValueControl("cbStatusConfirmEmail");
    var cbLockAccout = getValueControl("cbLockAccout");
    var txtNoteAccountInfo1Value = getValueControl("txtNoteAccountInfo1");
    var cbReasonLockAccount = getValueControl("cbReasonLockAccount");
    if (cbStatusDucomentText1  < 0 ) {
        Swal.fire({
            icon: "error",
            title: "Có lỗi xảy ra.",
            text: "Chưa chọn trạng thái!",
            footer: ''
          });
          return;
    }
 
    if (cbLockAccout.length < 1 ) {
        Swal.fire({
            icon: "error",
            title: "yêu cầu nghiệp vụ",
            text: "Chưa có thông tin  lý do để thay đổi trạng thái",
            footer: ''
          });
          return;

    }
  
    if (cbLockAccout == 0 || cbLockAccout == 2) {

        if (cbReasonLockAccount == "" || cbReasonLockAccount < 0) {
            Swal.fire({
                icon: "error",
                title: "yêu cầu nghiệp vụ",
                text: "Chưa có thông tin  lý do để khóa tài khoản",
                footer: ''
            });
            return;

        }
    }
    else {
        cbReasonLockAccount = -1; 
    }

    
    var bodyRequest =  {
        id : getValueControl("idinput"), 
        StatusAccount :  cbLockAccout,
        ConfirmAccout: cbStatusDucomentText1,
        reasonLock: cbReasonLockAccount,
        Noted:  txtNoteAccountInfo1Value
       
     };
     $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/NTDDetail?handler=UpdateInfoHuman',
        data: bodyRequest,
        success: function (data) {
            debugger;
            successAdd(getValueControl("idinput"));
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {
           
        }
    });
}


function ResetpasswordAcount() {

   
    var bodyRequest =  {
        id : getValueControl("idinput"), 
     
       
     };
     $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/NTDDetail?handler=ResetPasswordAccount',
        data: bodyRequest,
        success: function (data) {
            
            Swal.fire({
                title: " Yêu cầu reset mật khẩu ?",
                text: "Đã gửi nội dung đến email của tài khoản ?",
            
              });
            
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {
           
        }
    });
}


function SendMailAtiveAccount() {

   
    var bodyRequest =  {
        id : getValueControl("idinput"), 
     
       
     };
     $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/NTDDetail?handler=SendMailActiveAccount',
        data: bodyRequest,
        success: function (data) {
            
            Swal.fire({
                title: " Yêu cầu reset mật khẩu ?",
                text: "Đã gửi nội dung đến email của tài khoản ?",
            
              });
            
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {
           
        }
    });
}

//endntd
function openFormCommon() {
    
    window.open("/ArticleDetail?Id=-1");

}


//job

function updateConfrimStatusJob() {

    var cbStatusDucomentText = getValueControl("cbStatusjob");
    var txtNotedDocument = getValueControl("txtStatusDocumnet");

    if (cbStatusDucomentText.length   < 1 ) {
        Swal.fire({
            icon: "error",
            title: "Có lỗi xảy ra.",
            text: "Chưa chọn trạng thái!",
            footer: ''
          });
          return;
    }
 

    var bodyRequest =  {
        id : getValueControl("idinput"), 
        statusChange :  cbStatusDucomentText,
        notedChange: txtNotedDocument
     };
     $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/JobDetailModel?handler=ConfirmStatusJob',
        data: bodyRequest,
        success: function (data) {
            Swal.fire({
                title: " Thao tác duyệt",
                text: "Cập nhật thành công",
            
              });
        
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {
           
        }
    });


    
}



function updateNTDInfo() {

    var fullNameupdate = getValueControl("txtName");
    var cbGenderSelect = getValueControl("cbGender");
    var txtphoneValue = getValueControl("txtphone");
    if (fullNameupdate.length < 1) {
        Swal.fire({
            icon: "error",
            title: "Có lỗi xảy ra.",
            text: "Chưa chọn họ tên!",
            footer: ''
        });
        return;
    }
    if (cbGenderSelect < 0) {
        Swal.fire({
            icon: "error",
            title: "Có lỗi xảy ra.",
            text: "Chưa chọn giới tính!",
            footer: ''
        });
        return;
    }
    if (txtphoneValue.length < 1) {
        Swal.fire({
            icon: "error",
            title: "Có lỗi xảy ra.",
            text: "chưa điện thôn tin số điện thoại của người đại diện!",
            footer: ''
        });
        return;
    }

    var bodyRequest = {
        id: getValueControl("idinput"),
        phone: txtphoneValue,
        Gender: cbGenderSelect,
        fullName  : fullNameupdate
    };
    $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/NTDDetail?handler=UpdatePersonalPerson',
        data: bodyRequest,
        success: function (data) {
            Swal.fire({
                title: " Thao tác",
                text: "Cập nhật thành công",

            });

        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {

        }
    });



}

function updateStatusDisplay() {

    var cbStatusDucomentText = getValueControl("cbStatusDisplay");
    var txtNotedDocument = getValueControl("txtNoteDocumnetDisplay");

    if (cbStatusDucomentText.length   < 1 ) {
        Swal.fire({
            icon: "error",
            title: "Có lỗi xảy ra.",
            text: "Chưa chọn trạng thái!",
            footer: ''
          });
          return;
    }
 

    var bodyRequest =  {
        id : getValueControl("idinput"), 
        statusChange :  cbStatusDucomentText,
        notedChange: txtNotedDocument
     };
     $.ajax({
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        type: "POST",
        datatype: "JSON",
        url: '/JobDetailModel?handler=UpdateDisplayJob',
        data: bodyRequest,
        success: function (data) {
            Swal.fire({
                title: " Thao tác duyệt",
                text: "Cập nhật thành công",
            
              });
        
        },
        error: function (jqXHR, exception) {
            showError(jqXHR);
        },
        complete: function () {
           
        }
    });


    
}
//endjob