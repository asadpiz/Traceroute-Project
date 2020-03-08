
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;

    public class DBConnection
    {
        public string connectionString;

        public DBConnection()
        {
            // Get connection string from web.config.
            connectionString = WebConfigurationManager.ConnectionStrings["TraceRoute"].ConnectionString;
        }
        public DBConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertBook()
        {
            /*string[] Author=bk.BookAuthors;
            string Category = bk.BookCategory;

            int AuthorID = InsertAuthor(Author);
            int CategoryID = InsertCategory(Category);

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("InsertBooks", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Title"].Value = bk.BookTitle;
            cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmd.Parameters["@Price"].Value = bk.BookPrice;
            cmd.Parameters.Add(new SqlParameter("@Detail", SqlDbType.NVarChar, 100));
            cmd.Parameters["@Detail"].Value = bk.BookDetail;
            cmd.Parameters.Add(new SqlParameter("@AuthorID", SqlDbType.Int));
            cmd.Parameters["@AuthorID"].Value = AuthorID;
            cmd.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int));
            cmd.Parameters["@CategoryID"].Value =CategoryID;
            cmd.Parameters.Add(new SqlParameter("@BookID", SqlDbType.Int));
            cmd.Parameters["@BookID"].Direction = ParameterDirection.Output;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@BookID"].Value;
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }*/
        }

        
        /*public List<BooksDetail> GetBooks()
        {http://localhost:13396/Proutewebsite/Global.asax
            //string[] authorName;
            /*SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetBooks", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Create a collection for all the employee records.
            List<BooksDetail> books = new List<BooksDetail>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] authorName = {(string)reader["FName"],(string)reader["LName"]};
                    System.Console.WriteLine("Fname {0} LName{1}", authorName[0], authorName[1]);
                    System.Console.WriteLine("BookID  ", (int)reader["BookID"]);
                    System.Console.WriteLine("Price  ", (double)reader["Price"]);
                    System.Console.WriteLine("Title  ", (string)reader["Title"]);
                    System.Console.WriteLine("Detail  ", (string)reader["Detail"]);
                    System.Console.WriteLine("CatName  ", (string)reader["CatName"]);

                    //authorName[1]=(string)reader["LName"];

                    BooksDetail bk = new BooksDetail(
                    (int)reader["BookID"],(double)reader["Price"],(string)reader["Title"], (string)reader["Detail"],
                    authorName, (string)reader["CatName"]);
                    books.Add(bk);
                }
                reader.Close();

                return books;
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }*/
}
