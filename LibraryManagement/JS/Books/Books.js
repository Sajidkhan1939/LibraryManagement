function getallBooks() {
    const url = "/Dashboard/GetallBooks";
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
            $("#booksdata").empty();
            if (data && data.length > 0) {
                const bookshtml = data.map(books => {
                    return `<tr><td hidden>${books.Book_ID}</td>
                <td class="name">${books.title}</td>
                <td class="name">${books.author}</td>
                <td class="email">${books.genre}</td>
                <td class="phone">${books.publisher}</td>
                <td class="phone">${books.publication_year}</td>
                <td class="phone">${books.ISBN}</td>
                <td class="phone">${books.total_copies}</td>
                <td>
                    <a  class="edit" data-toggle="modal"><i class="bi bi-pencil-fill" data-toggle="tooltip" onclick="
                        updateBook(${books.Book_ID},'${books.title}','${books.author}','${books.genre}','${books.publisher}',${books.publication_year},'${books.ISBN}',${books.total_copies})" title="Edit"></i>
                        </a>
					<a href="#deleteEmployeeModal" class="delete" data-toggle="modal"><i class="bi bi-trash-fill" data-toggle="tooltip" onclick="deleteBook(${books.Book_ID})" title="Delete"></i></a>
                </td></tr>`
                }).join('');
                $("#booksdata").html(bookshtml);
                $("#bookstable").dataTable({
                    "bFilter": false,
                    "bLengthChange": false,
                    paging: true,
                    searching: true

                });
            }
        })
        .catch(err => console.log(err));
}
function updateBook(Book_ID,title,author,genre,publisher,publication_year,ISBN,total_copies)
{
    console.log('edit button clicked');
    $("#addEmployeeModal").modal('show');
    $("#bookid").val(Book_ID);
    $("#booktitle").val(title);
    $("#author").val(author);
    $("#genre").val(genre);
    $("#publisher").val(publisher);
    $("#pyear").val(publication_year);
    $("#isbn").val(ISBN);
    $("#tcopies").val(total_copies)
}
function deleteBook(id) {
    const url = "/Dashboard/DeleteBook";
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Book_ID:id})
    };
    fetch(url, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            getallBooks();
        });
}
function addbook() {
    let bookid = $("#bookid").val();
    let title = $("#booktitle").val();
    let author = $("#author").val();
    let genre = $("#genre").val();
    let publisher = $("#publisher").val();
    let publication_year = $("#pyear").val();
    let ISBN = $("#isbn").val();
    let total_copies = $("#tcopies").val();
    let imageFiles = $("#imageFile")[0].files;
    let imageFile = null;
    if (imageFiles && imageFiles.length > 0) {
        imageFile = imageFiles[0];
    }
    console.log(imageFile);

    const formData = new FormData();
    formData.append('Book_ID', bookid);
    formData.append('title', title);
    formData.append('author', author);
    formData.append('genre', genre);
    formData.append('publisher', publisher);
    formData.append('publication_year', publication_year);
    formData.append('ISBN', ISBN);
    formData.append('total_copies', total_copies);
    formData.append('imagefile', imageFile);
    console.log(formData);
    //return false;
    const url = "/Dashboard/AddBooks";
    const requestOptions = {
        method: 'POST',
        body: formData
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
            $("#booktitle").val('');
            $("#author").val('');
            $("#genre").val('');
            $("#pyear").val('');
            $("#isbn").val('');
            $("#tcopies").val('');
            $("#publisher").val('');
            getallBooks();
        })
        .catch(error => {
            console.error('Error:', error);
        });
}


$(document).ready(function () {
    
    getallBooks();
});