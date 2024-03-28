using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using LibraryManagement.Models;
using Microsoft.AspNet.Identity;
using Recipe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class BookRepositories
    {
        private readonly DBHelper db = new DBHelper();
        public Results Results { get; set; }
        public BookRepositories()
        {
            Results = new Results();
        }
        public int AdBook(BooksViewModel dto, string imageUrl)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@userid", dto.UserId),
                    new SqlParameter("@title", dto.title),
                    new SqlParameter("@author", dto.author),
                    new SqlParameter("@genre", dto.genre),
                    new SqlParameter("@publisher", dto.publisher),
                    new SqlParameter("@publication_year",dto.publication_year),
                    new SqlParameter("@ISBN", dto.ISBN),
                    new SqlParameter("@total_copies", dto.total_copies),
                    new SqlParameter("@bookid", dto.Book_ID)
                };
                var result = db.DtabaseCrud("addbooks", parameters);
                var bookid = result.DataResult.Tables[0].Rows[0][0];

                // Check if the result of adding the book is successful and imageUrl is provided
                if (result.Result == true && !string.IsNullOrEmpty(imageUrl))
                {
                    try
                    {
                        // Execute the AddImage stored procedure
                        List<SqlParameter> imageParams = new List<SqlParameter>
                        {
                            new SqlParameter("@imageUrl", imageUrl),
                            new SqlParameter("@BookId", bookid)
                        };
                        db.DtabaseCrud("AddImage", imageParams);
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }
                    
                }

                return result.Result ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteBook(int id)
        {
            string response = "";
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@id",id)
                };
                var result = db.DtabaseCrud("DeleteBook", parameters);
                if (result.Result)
                {
                     response= "Book deleted successfully";
                }
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        //public Results DeleteProdPicture(int id)
        //{
        //    DBHelper DB = new DBHelper();
        //    DBResponse response = new DBResponse();
        //    try
        //    {
        //        response = DB.DtabaseCrud("sp_DeleteProductPicture", new List<SqlParameter>()
        //        {
        //            new SqlParameter("@id",id)                    
        //        }
        //        );
        //        if (response.Result)
        //        {
        //            var ImageURL = response.DataResult.Tables[0].Rows[0][0].ToString();
        //            string physicalPath = Path.Combine(HttpContext.Current.Server.MapPath("~" + ImageURL));
        //            if (File.Exists(physicalPath))
        //            {
        //                File.Delete(physicalPath);
        //            }
        //            Results.Message = Convert.ToString("Picture Deleted");
        //            Results.Result = Convert.ToString(true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Results();
        //    }
        //    return Results;
        //}
        public List<BooksDetails> GetBooksDetails()
        {
            try
            {
                List<BooksDetails> books = new List<BooksDetails>();
                var result = db.DtabaseCrud("BooksDetails");
                if(result.Result && result.DataResult.Tables.Count > 0)
                {
                    var dataTable = result.DataResult.Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            BooksDetails book = new BooksDetails
                            {
                                totalbooks = Convert.ToInt32(row["totalbooks"]),
                                issuedbooks = Convert.ToInt32(row["issuedbooks"]),
                                AvailableBooks = Convert.ToInt32(row["AvailableBooks"])
                            };

                            books.Add(book);
                        }

                        return books;
                    }
                    else
                    {
                        return books;
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
        public List<BooksViewModel> GetDetails()
        {
            try
            {
                List<BooksViewModel> booksdetail = new List<BooksViewModel>();
                var result = db.DtabaseCrud("issuedbookdetails");
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
                                UserName = Convert.ToString(row["UserName"]),
                                Email = Convert.ToString(row["Email"]),
                                PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                                issue_date = Convert.ToString(row["issue_date"]),
                                return_date = Convert.ToString(row["return_date"]),
                                Status= Convert.ToString(row["Status"]),
                                Fine = Convert.ToInt32(row["Fine"])
                            };

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
       
        public List<BooksViewModel> AllBooks()
        {
            try
            {
                List<BooksViewModel> booksdetail = new List<BooksViewModel>();               
                var result = db.DtabaseCrud("AllBooks");
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
                                publisher = Convert.ToString(row["publisher"]),
                                publication_year= Convert.ToInt32(row["publication_year"]),
                                genre = Convert.ToString(row["genre"]),
                                ISBN = Convert.ToString(row["ISBN"]),
                                total_copies= Convert.ToInt32(row["total_copies"])
                            };

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
        public int IssueBook(BooksViewModel dto)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {

                    new SqlParameter("@userid", dto.UserId),
                    new SqlParameter("@bookid", dto.Book_ID)                    
                };
                var result = db.DtabaseCrud("issuebook", parameters);
                if (result.Result)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<BooksViewModel> GetAdminUsers()
        {
            try
            {
                List<BooksViewModel> admins = new List<BooksViewModel>();
                var result = db.DtabaseCrud("GetAdminUsers");
                if(result.Result && result.DataResult.Tables.Count > 0)
                {
                    var dataTable = result.DataResult.Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach(DataRow row in dataTable.Rows)
                        {
                            BooksViewModel adminusers = new BooksViewModel
                            {
                                UserName = Convert.ToString(row["UserName"]),
                                Email = Convert.ToString(row["Email"]),
                                PhoneNumber = Convert.ToString(row["PhoneNumber"])
                            };
                            admins.Add(adminusers);
                        }
                        return admins;
                    }
                    return admins;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }            
        }
    }
    public class BooksDetails
    {
        public int totalbooks { get; set; }
        public int issuedbooks { get; set; }
        public int AvailableBooks { get; set; }
    }
    
}       