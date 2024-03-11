function getallbooks() {
    const url = "/User/GetallBooks";
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
            console.log(data);
            // Iterate over each book in the data and render it
            data.map(book => {
                // Create HTML elements for each book
                const item = `<div class="col-lg-3 col-md-4 col-sm-6  col-xs-12">
                   <div class="box">
                    <!-- slide-img -->
                    <div class="slide-img">
                        <img class="booksimages" alt="1" src="${book.bookimages[0].imageUrl}">
                        <!-- overlayer -->
                        <div class="overlay">
                            <!-- buy-btn -->
                            <a  href="/User/BooksCart?bookid=${book.Book_ID}" class="buy-btn">Rent</a>
                        </div>
                    </div>
                    <!-- detail-box -->
                    <div class="detail-box">
                        <!-- type -->
                        <div class="type">
                            <a href="#">${book.title}</a>
                            <span>${book.genre}</span>
                        </div>                       
                    </div>
                </div></div>
                `;
                $(".allbooks").append(item);
            });
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}

$(document).ready(function () {
    getallbooks();
});