using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace medexweb
{
    public class dataConnection
    {
        protected MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public dataConnection()
        {
            conntectToMysql();
        }

        protected void conntectToMysql()
        {
            server = "localhost";
            database = "medex";//you need to source the text file from github in sql
            uid = "root";
            password = "Cam81182041";//unique sql password
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Integrated Security=True;";           

            connection = new MySqlConnection(connectionString);
        }

        protected bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {             
                switch (ex.Number)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        protected bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.Message); //messageboxes only work for code in windows application
                return false;
            }
        }

        //could seperate patients and doctors but its only patients for now

        public patient ValidatePatient(string user, string password)
        {
            string query = "SELECT accountNumber,name, userName, password,email,phoneNumber,birthDate FROM patientaccounts Where patientAccounts.userName = '" + user + "' and patientAccounts.password = '" + password + "'";

            patient currentPatient = new patient();
           
            if (this.OpenConnection() == true)
            {             
                MySqlCommand cmd = new MySqlCommand(query, connection);
                
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();                  
                    dataReader.Read();

                    currentPatient = new patient(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(),
                         dataReader.GetValue(3).ToString(), "Temporary Address", dataReader.GetValue(4).ToString(), dataReader.GetValue(5).ToString());//adds data from db to currentpatient               

                    currentPatient.setDob(dataReader.GetValue(6).ToString());
                    this.CloseConnection();
                }

                catch (Exception e)
                {
                    this.CloseConnection();
                    return currentPatient;
                }                

                return currentPatient;

            }
            else
            {
                patient temp = new patient();
                return temp;
            }
        }
        
        public DataTable getPrescriptionDatatable(int accountNumber)
        {
            string query = "SELECT rXNAME, rXDosage FROM patientPrescriptions WHERE patientFID = '" + accountNumber +"';";
            DataTable dt = new DataTable();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                try
                {                    
                    dt.Load(cmd.ExecuteReader());
                    this.CloseConnection();
                }
                catch(Exception e)
                {
                    this.CloseConnection();
                    return dt;
                }
                return dt;
            }
            else
            {
                return dt;
            }
            
        }
        public patient ValidatePerscription(patient tempPatient)
        {
            patient currentPatient = tempPatient;
            string query1 = "SELECT idNumber, rXName, rxDosage, rxPillcount," +
                " rxnumberofdays, prescriptionDesc, dateShipped  FROM prescriptions, patientPrescriptions" +
                " Where prescriptions.prescriptionID = patientPrescriptions.prescriptionFID and" +
                " patientPrescriptions.patientFID = '" + tempPatient.ID + "';";

            int count = 0;
            List<prescription> tempPrescriptions = new List<prescription>();
            
            if (this.OpenConnection() == true)
            {             
                MySqlCommand cmd = new MySqlCommand(query1, connection);
                
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                   
                    while (dataReader.Read())
                    {
                        tempPrescriptions.Add(new prescription(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(),
                            dataReader.GetValue(2).ToString(), Convert.ToInt32(dataReader.GetValue(3)), Convert.ToInt32(dataReader.GetValue(4)), dataReader.GetValue(5).ToString()));
                        tempPrescriptions[count].myDeliveryDate = Convert.ToDateTime(dataReader.GetValue(6));
                        count++;
                    }
                    tempPatient.myPrescriptions = tempPrescriptions;                    
                    this.CloseConnection();

                }
                catch (Exception e)
                {
                    this.CloseConnection();
                    return tempPatient;

                }               
                return tempPatient;

            }
            else
            {
                return tempPatient;
            }
        }
    }
}