using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Filmdatabas.Model;
namespace Filmdatabas.Pages
{
    public partial class Home2 : System.Web.UI.Page
    {
        #region Fields
        private Service _service;
        #endregion

        #region Properties
        private Service Service
        {
            get
            {
                return _service ?? (_service = new Service());
            }
        }

        public CheckBoxList CheckBoxes { get; set; }
        #endregion

        #region Session
        private bool HasMessage
        {
            get
            {
                return Session["message"] != null;
            }
        }

        private string Message
        {
            get
            {
                var message = Session["message"] as string;
                Session.Remove("message");
                return message;
            }

            set
            {
                Session["message"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HasMessage)
            {
                ShowMessage.Text = Message;
                ShowMessage.Visible = true;
            }
        }


        public IEnumerable<Title> ContactListView_GetData()
        {

            return Service.GetMovies();
        }


        #region Delete
        public void ContactListView_DeleteItem(int titelID) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {

            try
            {
                Service.DeleteMovie(titelID);
                Message = "Filmen togs bort";
                Response.Redirect("~/Pages/Home2.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                Message = "Filmen togs inte bort";
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle tas bort.");
            }


        }
        #endregion

        #region Navigation buttons
        protected void AddRedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add2.aspx");
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void AddRedirectButton2_Click(object sender, EventArgs e)
        {
            Title title = new Title();

            Response.Redirect("Update.aspx?id=" + title.TitelID);
            Context.ApplicationInstance.CompleteRequest();
            //Context.ApplicationInstance.CompleteRequest();
        }
        #endregion


        protected void ContactListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Title title = e.Item.DataItem as Title;

            if (title != null)
            {

                Filmformat filmformat = new Filmformat();
                List<string> formatArray = new List<string>(6);

                var format = Service.GetFilmformatByMovieID(title.TitelID);
                //var formatTyp = Service.GetFormatByFilmformatID(format[0].FormatID);
                var literal = e.Item.FindControl("FormatLiteral") as Literal;

                for (int i = 0; i < format.Count; i++)
                {
                    var formatTyp = Service.GetFormatByFilmformatID(format[i].FormatID);
                    //Service.GetFormatByFilmformatID(format[0].FormatID);
                    formatArray.Add(formatTyp.Formattyp);
                    literal.Text = string.Join(", ", formatArray);
                }


            }
        }




        #region Update


        public void ContactListView_UpdateItem(int movieID) // Parameterns namn är samma som värdet DataKeyNames har.
        {
            try
            {
                var movie = Service.GetMovie(movieID);
                if (movie == null)
                {
                    ModelState.AddModelError(String.Empty,
                        String.Format("Filmen med ID {0} hittades inte.", movieID));
                    return;
                }


                if (TryUpdateModel(movie))
                {
                    int[] format_ids = CheckBoxes.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => int.Parse(li.Value)).ToArray();
                    Service.SaveMovie(movie, format_ids);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle uppdateras.");
            }

        }

        public IEnumerable<Format> FormatCheckboxList_GetData()
        {
            return Service.GetFormats();
        }

        protected void FormatCheckBoxList_DataBinding(object sender, EventArgs e)
        {
            var checkBoxes = sender as CheckBoxList;
            if (checkBoxes != null)
            {
                CheckBoxes = checkBoxes;
            }
        }

        #endregion
    }
}