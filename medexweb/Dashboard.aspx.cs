using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace medexweb
{
    public partial class Dashboard : System.Web.UI.Page
    {
        patient currentPatient;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["object"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            currentPatient = (patient)Session["object"];
            //lblWelcome.Text = "Welcome "+currentPatient.myName+"!";


            //Session.Abandon();
            //Response.Redirect("Login.aspx"); //these is for logout button click
        }
    }
}