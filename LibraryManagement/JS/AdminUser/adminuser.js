function getadmins() {
    const url = "/Dashboard/GetAdminUsers";
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({})
    };
    fetch(url, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            $("#studentdata").empty();
            if (data && data.length > 0) {
                const htmldata = data.map(admins => {
                    return `<tr><td>${admins.UserName}</td>
                <td class="name">${admins.Email}</td>
                <td class="email">${admins.PhoneNumber}</td></tr>`
                }).join('');
                $("#studentdata").html(htmldata);
            }
        })
        .catch(err => console.log(err));
}
function editstudent(StudentId, StudentName, StudentEmail, StudentPhone) {
    $("#addEmployeeModal").modal('show');
    $("#sId").val(StudentId);
    $("#sname").val(StudentName);
    $("#sEmail").val(StudentEmail);
    $("#sphone").val(StudentPhone);
}
function addstudent() {
    let sid = $("#sId").val().trim();
    let sname = $("#sname").val().trim();
    let sEmail = $("#sEmail").val().trim();
    let sphone = $("#sphone").val().trim();

    const url = "/Home/AddStudent";
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ StudentId: sid, StudentName: sname, StudentEmail: sEmail, StudentPhone: sphone })
    };
    fetch(url, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            $("#addEmployeeModal").modal('hide');
            $("#sId").val("");
            $("#sname").val("");
            $("#sEmail").val("");
            $("#sphone").val("");
        });

}

$(document).ready(function () {
    getadmins();
});