using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

namespace medexweb
{
    public partial class Dashboard : System.Web.UI.Page
    {
        patient currentPatient;

        private dataConnection patientConnect = new dataConnection();

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

            int accountNumber = currentPatient.ID;      
            DataTable dt = patientConnect.getPrescriptionDatatable(accountNumber);

            //prescriptionData.DataSource = dt;
            //prescriptionData.DataBind();

        }      
    }
}