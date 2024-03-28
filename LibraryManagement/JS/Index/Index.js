function GetBooksDetails() {
    const url = "/Dashboard/GetBooksDetails";
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
            $("#total-books").text(data[0].totalbooks);
            $("#issued-books").text(data[0].issuedbooks);
            $("#available-books").text(data[0].AvailableBooks);
        })
        .catch(err => console.log(err));
}
function IssuedbooksDetails() {
    const url = "/Dashboard/GetDetails";
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
            $("#tbodycontent").empty();
            if (data && data.length > 0) {
                const htmldata = data.map(detail => {
                    return `<tr><td>${detail.title}</td>
                <td>${detail.author}</td>
                <td>${detail.genre}</td>
                <td>${detail.UserName}</td>
                <td>${detail.Email}</td>
                <td>${detail.PhoneNumber}</td>
                <td>${detail.issue_date}</td>
                <td>${detail.return_date}</td>
                <td>${detail.Status}</td>
                <td>${detail.Fine}</td>
                </tr>`
                }).join('');
                $("#tbodycontent").html(htmldata);
                //$("#booksissuedtable").dataTable({
                //    "bFilter": false,
                //    "bLengthChange": false,
                //});
            }
        })
        .catch(err => console.log(err));
}
$(document).ready(function () {
    IssuedbooksDetails();
    GetBooksDetails();
    $("#searchInput").on("keyup", function () {
        const value = $(this).val().toLowerCase();
        $("#tbodycontent tr").filter(function () {
            $(this).toggle(
                $(this).text().toLowerCase().indexOf(value) > -1 ||
                $(this).children("td:eq(6)").text().toLowerCase().indexOf(value) > -1 ||
                $(this).children("td:eq(7)").text().toLowerCase().indexOf(value) > -1
            );
        });
    });
});