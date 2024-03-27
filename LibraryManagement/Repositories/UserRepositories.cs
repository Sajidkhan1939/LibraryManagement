using LibraryManagement.Models;
using Recipe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace LibraryManagementSystem.Models
{

    public class UserRepositories
    {
        private readonly DBHelper db = new DBHelper();
        public List<BooksViewModel> AllBooks(BooksViewModel dto)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@id",dto.Book_ID),
                    new SqlParameter("@userid",dto.UserId)
                };
                List<BooksViewModel> booksdetail = new List<BooksViewModel>();
                var result = db.DtabaseCrud("AllBooks",parameters);
                if (result.Result && result.DataResult.Tables.Count > 0)
                {
                    var dataTable = result.DataResult.Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            BooksViewModel details = new BooksViewModel
                            {
                                Book_ID = Convert.ToInt32(row["Book_ID"]),
                                title = Convert.ToString(row["title"]),
                                author = Convert.ToString(row["author"]),
                                genre = Convert.ToString(row["genre"]),
                                publisher = Convert.ToString(row["publisher"]),
                                publication_year = Convert.ToInt32(row["publication_year"]),
                                ISBN = Convert.ToString(row["ISBN"]),
                                bookimages = new List<Images>()
                            };
                            Images imagedata = new Images()
                            {
                                imageUrl = Convert.ToString(row["imageUrl"])
                            };
                            details.bookimages.Add(imagedata);

                            booksdetail.Add(details);
                        }

                        return booksdetail;
                    }
                    else
                    {
                        return booksdetail;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<BooksViewModel> AllUserBooks(BooksViewModel dto)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@id",dto.Book_ID),
                    new SqlParameter("@userid",dto.UserId)
                };
                List<BooksViewModel> booksdetail = new List<BooksViewModel>();
                var result = db.DtabaseCrud("AllBooks", parameters);
                if (result.Result && result.DataResult.Tables.Count > 0)
                {
                    var dataTable = result.DataResult.Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            BooksViewModel details = new BooksViewModel
                            {
                                issue_id= Convert.ToInt32(row["issue_id"]),
                                Book_ID = Convert.ToInt32(row["Book_ID"]),
                                title = Convert.ToString(row["title"]),
                                author = Convert.ToString(row["author"]),
                                genre = Convert.ToString(row["genre"]),
                                publisher = Convert.ToString(row["publisher"]),
                                publication_year = Convert.ToInt32(row["publication_year"]),
                                ISBN = Convert.ToString(row["ISBN"]),
                                issue_date = Convert.ToString(row["issue_date"]),
                                return_date = Convert.ToString(row["return_date"]),
                                Status = Convert.ToString(row["Status"]),
                                Fine = Convert.ToInt32(row["Fine"]),
                                bookimages = new List<Images>()
                            };
                            Images imagedata = new Images()
                            {
                                imageUrl = Convert.ToString(row["imageUrl"])
                            };
                            details.bookimages.Add(imagedata);

                            booksdetail.Add(details);
                        }

                        return booksdetail;
                    }
                    else
                    {
                        return booksdetail;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public string ReturnBook(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@issueid",id)
                };
                var res = db.DtabaseCrud("returnBook", parameters);
                if (res.Result)
                {
                    var dataTable = res.DataResult.Tables[0];
                    
                    DataRow row = dataTable.Rows[0];

                    var contentres =row.ItemArray[0].ToString();
                    if (contentres == "0")
                    {
                        string message = row.ItemArray[1].ToString();
                        return message;
                    }
                    else {
                        string message = "success";
                        return message;
                    }
                }
                else
                {
                    return "Please contact librarian";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string IssueNewBook(BooksViewModel dto)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {

                    new SqlParameter("@userid", dto.UserId),
                    new SqlParameter("@bookid", dto.Book_ID)
                };
                var result = db.DtabaseCrud("IssueBooks", parameters);
                if (result.Result)
                {
                    return "book is issued successfully";
                }
                else
                {
                    return "something went wrong";
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}