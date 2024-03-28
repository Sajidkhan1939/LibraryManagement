function userbooks() {
    const url = "/User/UserissuedBooks";
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
            $("#userallbooks").empty();
            if (data && data.length > 0) {
                const bookshtml = data.map(userdata => {
                    return `<tr><td><img class="userbooksimages" src="${userdata.bookimages[0].imageUrl}"/></td>
                <td class="text-right align-middle" title="Amount">${userdata.title}</td>
                <td class="text-right align-middle" title="Amount">${userdata.issue_date}</td>
                <td class="text-right align-middle" title="Price">${userdata.return_date}</td>
                <td class="text-right align-middle" title="Total">${userdata.Status}</td>
                <td class="text-right align-middle" title="Total">${userdata.Fine}</td>
                <td class="text-right align-middle"><button onclick="returnbook(${userdata.issue_id})">Return</button>
                </td></tr>`
                }).join('');
                $("#userallbooks").html(bookshtml);
            }
            else {
                $("#userallbooks").html(`<tr><td class="text-center align-middle" colspan="7">no book issued yet</td></tr>`);
            }
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        })
}
function returnbook(issue_id) {
    const url = "/User/ReturnBook";
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id:issue_id})    
    };
    fetch(url, requestOptions)
        .then(response => {
            if (response.result == false) {
                alert("Please contact Librarian");
            }
            else {
                alert("SUCCESSFULLY RETURNED");
                userbooks();
            }
        })       
}

$(document).ready(function () {
    userbooks();
});