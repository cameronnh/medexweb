using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace medexweb
{
    public partial class login : System.Web.UI.Page
    {
        patient currentPatient;

        private dataConnection patientConnect = new dataConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {            
            string tryUsername = txtUserName.Text;
            string tryPassword = txtPassword.Text;

            currentPatient = patientConnect.ValidatePatient(tryUsername, tryPassword);//sends login request to patient in dataconnection

            if(currentPatient.userName == txtUserName.Text)
            {
                //patientConnect.ValidatePerscription(currentPatient);//gets the patients prescriptions IM NOT SURE IF THIS COULD GET DELETED

                Session["object"] = currentPatient;                
                Response.Redirect("Schedule.aspx");                   
            }
            else
            {
                lblErrorMessage.Visible = true;
            }
       
        }
    }
}