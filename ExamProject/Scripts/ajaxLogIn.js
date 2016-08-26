$("#btnLogUserTeacher").click(function () {
    $.ajax({
        url: "/Home/Index",
        type: "POST",
        data: { "userType": "1" }, //teacher
        success: function (data) {
            console.log(data);
        },
        error: function (request, status, error) {
            alert('oh, errors here. The call to the server is not working.')
        }
    });
});

$("#btnLogUserStudent").click(function () {
    $.ajax({
        url: "/Home/Index",
        type: "POST",
        data: { "userType": "2" }, //student
        success: function (data) {
            console.log(data);
        },
        error: function (request, status, error) {
            alert('oh, errors here. The call to the server is not working.')
        }
    });
});