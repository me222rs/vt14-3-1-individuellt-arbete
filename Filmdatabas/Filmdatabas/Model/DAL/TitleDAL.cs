using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Filmdatabas.Model.DAL
{
    public class TitleDAL
    {
        #region Connection
        private static string _connectionString;

        static TitleDAL()
        {
            //Denna kod hämtar ut anslutningssträngen från web.config
            _connectionString = WebConfigurationManager.ConnectionStrings["MovieConnectionString"].ConnectionString;
        }

        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        #endregion


        #region Get all movies
        public IEnumerable<Title> GetMovies()
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    var movies = new List<Title>(100);

                    var cmd = new SqlCommand("Movie.usp_GetAllMovies", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var TitelIDIndex = reader.GetOrdinal("TitelID");
                        var titelIndex = reader.GetOrdinal("Titel");
                        var landIndex = reader.GetOrdinal("Land");
                        var produktionsarIndex = reader.GetOrdinal("Produktionsar");
                        var filmbolagIndex = reader.GetOrdinal("Filmbolag");
                        var beskrivningIndex = reader.GetOrdinal("Beskrivning");
                        var hyllplatsIndex = reader.GetOrdinal("Hyllplats");
                        var speltidIndex = reader.GetOrdinal("Speltid");

                        


                        while (reader.Read())
                        {
                            movies.Add(new Title
                            {
                                TitelID = reader.GetInt32(TitelIDIndex),
                                Titel = reader.GetString(titelIndex),
                                Land = reader.GetString(landIndex),
                                Produktionsar = reader.GetInt32(produktionsarIndex),
                                Filmbolag = reader.GetString(filmbolagIndex),
                                Beskrivning = reader.GetString(beskrivningIndex),
                                Hyllplats = reader.GetString(hyllplatsIndex),
                                Speltid = reader.GetInt32(speltidIndex),
                            });

                        }
                    }
                    movies.TrimExcess();
                    return movies;
                }
                catch
                {
                    //throw new ApplicationException("Fel inträffade när kontakterna skulle hämtas från databasen.");
                    return null;
                }
            }
        }
        #endregion


        #region Get a movie by id
        public Title GetMovieById(int titleID)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("Movie.usp_GetAMovie", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TitleID", titleID);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var titelIndex = reader.GetOrdinal("Titel");
                            var landIndex = reader.GetOrdinal("Land");
                            var produktionsarIndex = reader.GetOrdinal("Produktionsar");
                            var filmbolagIndex = reader.GetOrdinal("Filmbolag");
                            var beskrivningIndex = reader.GetOrdinal("Beskrivning");
                            var hyllplatsIndex = reader.GetOrdinal("Hyllplats");
                            var speltidIndex = reader.GetOrdinal("Speltid");

                            return new Title
                            {
                                Titel = reader.GetString(titelIndex),
                                Land = reader.GetString(landIndex),
                                Produktionsar = reader.GetInt32(produktionsarIndex),
                                Filmbolag = reader.GetString(filmbolagIndex),
                                Beskrivning = reader.GetString(beskrivningIndex),
                                Hyllplats = reader.GetString(hyllplatsIndex),
                                Speltid = reader.GetInt32(speltidIndex),

                            };
                        }
                    }
                    return null;
                }
                catch
                {
                    throw new ApplicationException("Ett fel inträffade i data access layer.");
                }
            }

        }
        #endregion

        #region Insert a movie
        public void InsertMovie(Title movie)
        {
            
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("Movie.usp_AddANewMovie", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    //cmd.Parameters.Add("@TitelID", SqlDbType.Int).Value = movie.TitelID;
                    cmd.Parameters.Add("@Titel", SqlDbType.VarChar, 40).Value = movie.Titel;
                    cmd.Parameters.Add("@Land", SqlDbType.VarChar, 30).Value = movie.Land;
                    cmd.Parameters.Add("@Produktionsar", SqlDbType.Int).Value = movie.Produktionsar;
                    cmd.Parameters.Add("@Filmbolag", SqlDbType.VarChar, 40).Value = movie.Filmbolag;
                    cmd.Parameters.Add("@Beskrivning", SqlDbType.VarChar, 500).Value = movie.Beskrivning;
                    cmd.Parameters.Add("@Hyllplats", SqlDbType.VarChar, 10).Value = movie.Hyllplats;
                    cmd.Parameters.Add("@Speltid", SqlDbType.Int).Value = movie.Speltid;


                    // Den här parametern är lite speciell. Den skickar inte något data till den lagrade proceduren,
                    // utan hämtar data från den. (Fungerar ungerfär som ref- och out-prameterar i C#.) Värdet 
                    // parametern kommer att ha EFTER att den lagrade proceduren exekverats är primärnycklens värde
                    // den nya posten blivit tilldelad av databasen.
                    cmd.Parameters.Add("@TitelID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar Customer-objektet värdet.
                    movie.TitelID = (int)cmd.Parameters["@TitelID"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        #endregion

        #region Update a movie
        public void UpdateMovie(Title movie)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Movie.usp_UpdateMovie", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Titel", SqlDbType.VarChar, 40).Value = movie.Titel;
                    cmd.Parameters.Add("@Land", SqlDbType.VarChar, 30).Value = movie.Land;
                    cmd.Parameters.Add("@Produktionsar", SqlDbType.Int).Value = movie.Produktionsar;
                    cmd.Parameters.Add("@Filmbolag", SqlDbType.VarChar, 40).Value = movie.Filmbolag;
                    cmd.Parameters.Add("@Beskrivning", SqlDbType.VarChar, 500).Value = movie.Beskrivning;
                    cmd.Parameters.Add("@Hyllplats", SqlDbType.VarChar, 10).Value = movie.Hyllplats;
                    cmd.Parameters.Add("@Speltid", SqlDbType.Int).Value = movie.Speltid;

                    cmd.Parameters.Add("@TitelID", SqlDbType.Int, 4).Value = movie.TitelID;

                    conn.Open();

                    cmd.ExecuteNonQuery();


                    // TODO: Implementera UpdateCustomer.

                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        #endregion

        #region Delete a movie
        public void DeleteMovie(int movieID)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Movie.usp_DeleteMovie", conn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@TitelID", SqlDbType.Int, 4).Value = movieID;

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