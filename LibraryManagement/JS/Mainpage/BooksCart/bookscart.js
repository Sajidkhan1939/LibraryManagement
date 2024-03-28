function getbook(bookID) {
    const url = "/User/GetBook";
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Book_ID:bookID})
    };
    fetch(url, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Iterate over each book in the data and render it
            data.map(book => {
                // Create HTML elements for each book
                const item = `
                    <div class="wrapper row">
                <div class="preview col-md-6">
                    <div class="preview-pic tab-content">
                        <div class="tab-pane active" id="pic-1"><img src="${book.bookimages[0].imageUrl}" /></div>
                    </div>
                </div>
                <div class="details col-md-6">
                    <h3 class="product-title">${book.title}</h3>
                    <p class="product-description">${book.author}</p>
                    <p class="product-description">${book.genre}</p>
                    <p class="product-description">${book.publisher}</p>
                    <p class="product-description"><b>Publication Year:  </b>${book.publication_year}</p>
                    <b>Warning:</b>
                    <p class="product-description" style="color:yellowgreen">If book is not been returned with in the week the fine should be charged.It will be the 100 Rupees per day</p>
                    <div class="action">
                        <button class="add-to-cart btn btn-default" type="button"
                        onclick="requestBook(${book.Book_ID})">Request book</button>
                    </div>
                </div>
            </div>
                `;
                $(".singlebook").append(item);
            });
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}
function requestBook(bookID) {
    const url = "/User/IssuenewBook";
    var params = { Book_ID: bookID };
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ dto: params})
    };
    fetch(url, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
        })
        .then(data => {
            Swal.fire({
                title: "Congratulations",
                text: "Your book is Issued Successfully",
                icon: "success"
            });
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}
// Function to retrieve query string parameter value by name
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$(document).ready(function () {
    var bookID = getParameterByName('bookid');
    getbook(bookID);
    $(".message").hide();
    $(".add-to-cart").click(function () {
        $(".save")
            .transition({
                animation: "fade down",
                duration: 400
            })
            .transition({
                animation: "fly left",
                interval: 4000,
                duration: 2000
            });
    });
});