using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Filmdatabas.Model
{
    public class Filmformat
    {
        private int _formatID;

        public int FormatID { get { return _formatID; } set { _formatID = value; } }
        public int TitelID { get; set; }
        public int FilmformatID { get; set; }
    }
}