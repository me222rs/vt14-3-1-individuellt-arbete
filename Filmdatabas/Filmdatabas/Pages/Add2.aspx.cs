using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Filmdatabas.Model;

namespace Filmdatabas.Pages
{
    public partial class Add2 : System.Web.UI.Page
    {
        #region Properties
        private Service _service;

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


        #region Insert
        public void MovieListView_InsertItem(Title titelID)
        {
            if (ModelState.IsValid)
            {
                //Lägger till de formaten som användaren kryssat i checkboxen.
                int[] format_ids = CheckBoxes.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => int.Parse(li.Value)).ToArray();
                try
                {
                    //Arrayen skickas vidare till Service klassen tillsammans med filmen.
                    Service.SaveMovie(titelID, format_ids);
                    Message = "Filmen lades till";
                    Response.Redirect("~/Pages/Add2.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle läggas till.");
                    Message = "Filmen lades inte till";
                    Response.Redirect("~/Pages/Add2.aspx");
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }
        #endregion

        #region Navigation
        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Home2.aspx");
            Context.ApplicationInstance.CompleteRequest();
        }
        #endregion

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