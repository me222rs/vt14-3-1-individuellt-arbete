using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Filmdatabas.Model;
using System.Web.ModelBinding;

namespace Filmdatabas.Pages
{
    public partial class Update2 : System.Web.UI.Page
    {
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

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowMessage.Text = Message;
            ShowMessage.Visible = true;
        }
        #endregion

        #region Navigation
        protected void AddRedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home2.aspx");
            Context.ApplicationInstance.CompleteRequest();
        }
        #endregion

        #region Update

        public Title MovieListView_GetData([QueryString]int titleID)
        {
            //iD = titleID;
            //titleID = Convert.ToInt32(Request.QueryString["id"]);
            return Service.GetMovie(titleID);
        }

        public void MovieListView_UpdateItem([QueryString]int titleID) // Parameterns namn är samma som värdet DataKeyNames har.
        {
            //titelID = 66;
            if (ModelState.IsValid)
            {
                try
                {
                    var movie = Service.GetMovie(titleID);

                    if (movie == null)
                    {
                        ModelState.AddModelError(String.Empty,
                            String.Format("Filmen med ID {0} hittades inte.", titleID));
                        return;
                    }

                    var checkBoxList = (CheckBoxList)MovieListView.FindControl("FormatCheckBoxList");
                    int[] format_ids = checkBoxList.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => int.Parse(li.Value)).ToArray();
                    if (TryUpdateModel(movie))
                    {

                        movie.TitelID = titleID;
                        Service.SaveMovie(movie, format_ids);

                    }
                    Message = "Uppdateringen lyckades";
                    Response.Redirect("~/Pages/Home2.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    Message = "Uppdateringen misslyckades";
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle uppdateras.");
                }
            }
        }

        #region Checkbox
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

        #endregion

        //Metod som kryssar i de format filmen har när man ska redigera
        protected void FormatCheckBoxList_DataBound(object sender, EventArgs e)
        {
            int titleID = Convert.ToInt32(Request.QueryString["titleID"]);
            var checkBoxes = sender as CheckBoxList;
            var format = Service.GetFilmformatByMovieID(titleID).ToList();

            foreach (var checkBox in checkBoxes.Items.Cast<ListItem>())
            {
                for (int i = 0; i < format.Count; i++)
                {
                    if (format[i].FormatID.ToString() == checkBox.Value)
                    {
                        checkBox.Selected = true;
                    }
                }
            }
        }

        protected void CheckCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

            var checkBoxList = (CheckBoxList)MovieListView.FindControl("FormatCheckBoxList");
            var checkBoxChecked = checkBoxList.SelectedItem;

            if (checkBoxChecked != null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}