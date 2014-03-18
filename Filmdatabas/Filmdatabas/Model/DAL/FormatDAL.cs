using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Filmdatabas.Model.DAL
{
    public class FormatDAL
    {
        #region Connection
        private static string _connectionString;
        static FormatDAL()
        {
            //Denna kod hämtar ut anslutningssträngen från web.config
            _connectionString = WebConfigurationManager.ConnectionStrings["MovieConnectionString"].ConnectionString;
        }
        


        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        #endregion


        #region Get formattypes
        public static IEnumerable<Format> GetFormatTypes()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("Movie.usp_GetFormatTypes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Skapar det List-objekt som initialt har plats för 10 referenser till ContactType-objekt.
                    List<Format> formatTypes = new List<Format>(10);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                    // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Tar reda på vilket index de olika kolumnerna har. Det är mycket effektivare att göra detta
                        // en gång för alla innan while-loopen. Genom att använda GetOrdinal behöver du inte känna till
                        // i vilken ordning de olika kolumnerna kommer, bara vad de heter.
                        var formattypindex = reader.GetOrdinal("Formattyp");
                        

                        // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
                        // poster returnerar Read false.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
                            // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
                            formatTypes.Add(new Format
                            {

                                //Formattyp = reader.GetInt32(formattypindex),
                                
                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minne
                    // som inte används.
                    formatTypes.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med ContactType-objekt.
                    return formatTypes;
                }

                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException();
                }
            }
        }
        #endregion

        public Format GetFormatTypesByFormatID(int filmFormatId)
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Movie.usp_GetFormatTypes", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("FormatID", filmFormatId);
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            var formatTypIndex = reader.GetOrdinal("Formattyp");
                            

                            return new Format
                            {
                                Formattyp = reader.GetString(formatTypIndex),

                            };
                        }

                    }
                    return null;
                }


                catch
                {
                    //throw new ApplicationException("Fel inträffade när kontakterna skulle hämtas från databasen.");
                    return null;
                }
            }
        }

        public IEnumerable<Format> GetFormats()
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    var formats = new List<Format>(10);

                    var cmd = new SqlCommand("Movie.GetAllFormats", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var formatIndex = reader.GetOrdinal("Formattyp");
                        var formatIDIndex = reader.GetOrdinal("FormatID");



                        while (reader.Read())
                        {
                            formats.Add(new Format
                            {
                                Formattyp = reader.GetString(formatIndex),
                                FormatID = reader.GetInt32(formatIDIndex),
                            });

                        }
                    }
                    formats.TrimExcess();
                    return formats;
                }
                catch
                {
                    //throw new ApplicationException("Fel inträffade när kontakterna skulle hämtas från databasen.");
                    return null;
                }
            }
        }

    }
}