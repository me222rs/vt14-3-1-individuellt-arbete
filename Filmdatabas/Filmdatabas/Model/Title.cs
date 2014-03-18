using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Filmdatabas.Model
{
    public class Title
    {
        //private int _formatID;


        public int TitelID { get; set; }

        [Required(ErrorMessage = "En titel måste anges.")]
        [StringLength(40, ErrorMessage = "Titeln måste ha ett namn på under 40 tecken.")]
        public string Titel { get; set; }

        public int Betyg { get; set; }

        [Required(ErrorMessage = "Ett land måste anges.")]
        [StringLength(30, ErrorMessage = "Landet kan bestå av som mest 30 tecken.")]
        public string Land { get; set; }

        [Required(ErrorMessage = "Ett Produktionsår måste anges.")]
        [Range(1897, 2100, ErrorMessage = "Året du angett kanske var innan filmens tid eller alltför långt fram i framtiden")]
        //[RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Året du angett kanske var innan filmens tid eller alltför långt fram i framtiden")]
        public int Produktionsar { get; set; }

        [Required(ErrorMessage = "Ett filmbolag måste anges.")]
        [StringLength(40, ErrorMessage = "Filmbolaget kan bestå av som mest 40 tecken.")]
        public string Filmbolag { get; set; }

        [StringLength(500, ErrorMessage = "Beskrivningen kan bestå av som mest 500 tecken.")]
        public string Beskrivning { get; set; }

        [Required(ErrorMessage = "En hyllplats måste anges.")]
        [StringLength(10, ErrorMessage = "Hyllplatsen kan bestå av som mest 10 tecken.")]
        public string Hyllplats { get; set; }

        [Required(ErrorMessage = "Filmens längd måste anges.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Speltiden måste anges i minuter.")]
        public int Speltid { get; set; }


        public int FilmformatID { get; set; }
        public int FormatID { get; set; }
    }
}