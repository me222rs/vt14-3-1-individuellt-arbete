using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Filmdatabas.Model.DAL
{
    public class FilmformatDAL
    {
        #region Connection
        private static string _connectionString;

        static FilmformatDAL()
        {
            //Denna kod hämtar ut anslutningssträngen från web.config
            _connectionString = WebConfigurationManager.ConnectionStrings["MovieConnectionString"].ConnectionString;
        }

        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        #endregion

        #region Get format by id
        public List<Filmformat> GetFormatByMovieId(int titelID)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("Movie.usp_GetFormatByMovieID", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TitelID", titelID);

                    List<Filmformat> formats = new List<Filmformat>(10);

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {

                            var formatIndex = reader.GetOrdinal("FormatID");
                            
                            while (reader.Read())
                            {
                                formats.Add(new Filmformat 
                                {
                                    FormatID = reader.GetInt32(formatIndex),
                                });

                            }
                        
                    }
                    formats.TrimExcess();
                    return formats;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade i data access layer.");
                }
            }

        }
        #endregion

        #region Insert
        public void InsertFilmformat(Title movie, int i)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("Movie.usp_AddFilmFormat", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.

                    cmd.Parameters.Add("@TitelID", SqlDbType.Int).Value = movie.TitelID;
                    cmd.Parameters.Add("@FormatID", SqlDbType.Int).Value = i;


                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        #endregion

        #region Update


        public void UpdateFilmformat(Title movie, int i)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("Movie.usp_UpdateFormat", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TitelID", movie.TitelID);
                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.

                    //cmd.Parameters.Add("@TitelID", SqlDbType.Int).Value = movie.TitelID;
                    cmd.Parameters.Add("@FormatID", SqlDbType.Int).Value = i;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public IEnumerable<Format> GetFormatIDByTitleID(int titleId)
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    var movieList = new List<Format>(100);
                    var cmd = new SqlCommand("Movie.usp_GetFormatIdByTitleID", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("TitleID", titleId);
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {


                        var formatIndex = reader.GetOrdinal("FormatID");
                        //var formatTypIndex = reader.GetOrdinal("Formattyp");

                        while (reader.Read())
                        {
                            movieList.Add(new Format
                            {
                                FormatID = reader.GetInt32(formatIndex),
                                //Formattyp = reader.GetString(formatTypIndex),
                            });


                        }
                    }
                    movieList.TrimExcess();

                    return movieList;
                }


                catch
                {
                    throw new ApplicationException();
                }

            }
        }

        public void DeleteFormat(int titleID)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Movie.usp_DeleteFormat", conn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@TitleID", SqlDbType.Int, 4).Value = titleID;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    // TODO: Implementera DeleteCustomer.

                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        #endregion

 
    }
}