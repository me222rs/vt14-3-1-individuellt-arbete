using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Filmdatabas.Model.DAL;
using System.ComponentModel.DataAnnotations;

namespace Filmdatabas.Model
{
    public class Service
    {
            #region Fält
        private static TitleDAL _titleDAL;
            private static FilmformatDAL _techInfoDAL;
            private static FormatDAL _filmFormatDAL;
        #endregion


            #region Egenskaper

            private TitleDAL TitleDAL
            {
                get
                {
                    return _titleDAL ?? (_titleDAL = new TitleDAL());
                }
            }

            private FilmformatDAL TechInfoDAL
            {
                get 
                {
                    return _techInfoDAL ?? (_techInfoDAL = new FilmformatDAL());
                }
            }

            private FormatDAL FilFormatDAL
            {
                get 
                {
                    return _filmFormatDAL ?? (_filmFormatDAL = new FormatDAL());
                }
            }
            #endregion


            #region Format


            public IEnumerable<Format> GetFormats()
            {
                return FilFormatDAL.GetFormats();
            }
            #endregion


            #region Title

            public Title GetMovie(int movieID)
            {
                return TitleDAL.GetMovieById(movieID);
            }

            public IEnumerable<Title> GetMovies()
            {
                return TitleDAL.GetMovies();
            }

            public void DeleteMovie(int movieID)
            {
                TitleDAL.DeleteMovie(movieID);
            }

            public IEnumerable<Format>GetFormatIDByTitleID(int titleID)
            {
                return TechInfoDAL.GetFormatIDByTitleID(titleID);
            }

            public void SaveMovie(Title movie, int[] format_ids)
            {

                ICollection<ValidationResult> validationResults;
                if (!movie.Validate(out validationResults)) // Använder "extension method" för valideringen!
                {                                              // Klassen finns under App_Infrastructure.
                    // ...kastas ett undantag med ett allmänt felmeddelande samt en referens 
                    // till samlingen med resultat av valideringen.
                    var ex = new ValidationException("Objektet klarade inte valideringen.");
                    ex.Data.Add("ValidationResults", validationResults);
                    throw ex;
                }


                if (movie.TitelID == 0) // Ny post om CustomerId är 0!
                {
                    //Skickar filmen till TitleDAL där infon läggs in i databasen.
                    TitleDAL.InsertMovie(movie);

                    //Loopar igenom alla ikryssade format och skickar dom till InserFilmFormat metoden som sedan lägger till varje format.
                    for (int i = 0; format_ids.Length - 1>= i; i++)
                    {
                        TechInfoDAL.InsertFilmformat(movie, format_ids[i]);
                    }
                        
                    
                }
                else
                {
                    TitleDAL.UpdateMovie(movie);
                    TechInfoDAL.DeleteFormat(movie.TitelID);
                    for (int i = 0; format_ids.Length - 1 >= i; i++)
                    {
                        TechInfoDAL.InsertFilmformat(movie, format_ids[i]);
                    }
                }
            }
            #endregion

            public Format GetFormatByFilmformatID(int filmformatID) 
            {
                return FilFormatDAL.GetFormatTypesByFormatID(filmformatID);
            }

            public void DeleteFormat(int titleID)
            {
                TechInfoDAL.DeleteFormat(titleID);
            }

            #region Filmformat
            public List<Filmformat> GetFilmformatByMovieID(int titleID)
            {
                return TechInfoDAL.GetFormatByMovieId(titleID);
            }

            #endregion
    }
}