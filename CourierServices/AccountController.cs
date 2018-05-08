using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CourierServices.Models;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Web;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.IO;

namespace CourierServices.Controllers
{
    public class AccountController : ApiController
    {
        //private const string LocalLoginProvider = "Local";

        public AccountController()
        {
        }


        #region insertlogin
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name=""></param>
        [Route("api/Account/insertlogin")]
        [HttpPost]
        public void insertlogin(Login logindetails)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.login(UserName,Password,ConfirmPassword,Role,RoleId,SignatureImageName,SignatureImagePath) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", logindetails.UserName);
                    cmd.Parameters.AddWithValue("@val2", logindetails.Password);
                    cmd.Parameters.AddWithValue("@val3", logindetails.ConfirmPassword);
                    cmd.Parameters.AddWithValue("@val4", logindetails.Role);
                    cmd.Parameters.AddWithValue("@val5", logindetails.RoleId);
                    cmd.Parameters.AddWithValue("@val6", logindetails.SignatureImageName);
                    cmd.Parameters.AddWithValue("@val7", logindetails.SignatureImagePath);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getsignUpDetails

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// 
        /// <returns name="patientAllRegistrationInfo"></returns>
        /// 

        [Route("api/Account/getsignUpDetails")]
        [HttpPost]
        public Login getsignUpDetails(Login getdetails)
        {
            Login loginDetails = new Login();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * from pathoclinic.login where UserName='" + getdetails.UserName + "' AND  Password ='" + getdetails.Password + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            loginDetails.RoleId = (int)dr["RoleId"];
                            loginDetails.Role = dr["Role"].ToString();
                            //  loginDetails.patientRegistration.Guardian = dr["Guardian"].ToString();


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return loginDetails;
            }
        }

        #endregion


        #region getAllsignUpDetails
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllsignUpDetails")]
        [HttpPost]
        public List<Login> getAllsignUpDetails()
        {

            List<Login> lstlogin = new List<Login>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * from pathoclinic.login where Id !=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Login logindetails = new Login();

                            logindetails.Id = (int)dr["Id"];
                            logindetails.UserName = dr["UserName"].ToString();
                            logindetails.Password = dr["Password"].ToString();
                            logindetails.ConfirmPassword = dr["ConfirmPassword"].ToString();
                            logindetails.Role = dr["Role"].ToString();
                            lstlogin.Add(logindetails);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstlogin;

            }
        }
        #endregion

        #region getAllRegistrationDetails
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllRegistrationDetails")]
        [HttpGet]
        public List<PatientRegistration> getAllRegistrationDetails(string searchNameorRegId)
        {
            List<PatientRegistration> lstregistrationDetails = new List<PatientRegistration>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.patientregistration Where FirstName LIKE'%" + searchNameorRegId + "%' OR LastName LIKE '%" + searchNameorRegId + "%' OR RegID LIKE '%" + searchNameorRegId + "%'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            PatientRegistration patientRegistration = new PatientRegistration();

                            patientRegistration.RegID = Convert.ToInt32(dr["RegID"]);
                            patientRegistration.Title = dr["Title"].ToString();
                            patientRegistration.FirstName = dr["FirstName"].ToString();
                            patientRegistration.MiddleName = dr["MiddleName"].ToString();
                            patientRegistration.LastName = dr["LastName"].ToString();
                            patientRegistration.Guardian = dr["Guardian"].ToString();
                            patientRegistration.Relation = dr["Relation"].ToString();
                            patientRegistration.PatientType = dr["PatientType"].ToString();
                            patientRegistration.Sex = dr["Sex"].ToString();
                            patientRegistration.MaritalStatus = dr["MaritalStatus"].ToString();
                            patientRegistration.DOB = dr["DOB"].ToString();
                            patientRegistration.Age = Convert.ToInt16(dr["age"]);
                            patientRegistration.DateOfReg = dr["DateOfReg"].ToString();
                            patientRegistration.PhoneNumber = dr["PhoneNumber"].ToString();
                            lstregistrationDetails.Add(patientRegistration);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstregistrationDetails;
            }
        }
        #endregion

        #region getAllRegistrationDetailsEmailPhone
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllRegistrationDetailsEmailPhone")]
        [HttpGet]
        public List<PatientRegistration> getAllRegistrationDetailsEmailPhone(string searchNameorRegId)
        {
            List<PatientRegistration> lstregistrationDetails = new List<PatientRegistration>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.patientregistration Where PhoneNumber LIKE'%" + searchNameorRegId + "%' OR ContactEmail LIKE '%" + searchNameorRegId + "%' OR RegID LIKE '%" + searchNameorRegId + "%'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            PatientRegistration patientRegistration = new PatientRegistration();

                            patientRegistration.RegID = Convert.ToInt32(dr["RegID"]);
                            patientRegistration.Title = dr["Title"].ToString();
                            patientRegistration.FirstName = dr["FirstName"].ToString();
                            patientRegistration.MiddleName = dr["MiddleName"].ToString();
                            patientRegistration.LastName = dr["LastName"].ToString();
                            patientRegistration.Guardian = dr["Guardian"].ToString();
                            patientRegistration.Relation = dr["Relation"].ToString();
                            patientRegistration.PatientType = dr["PatientType"].ToString();
                            patientRegistration.Sex = dr["Sex"].ToString();
                            patientRegistration.MaritalStatus = dr["MaritalStatus"].ToString();
                            patientRegistration.DOB = dr["DOB"].ToString();
                            patientRegistration.Age = Convert.ToInt16(dr["age"]);
                            patientRegistration.DateOfReg = dr["DateOfReg"].ToString();
                            patientRegistration.PhoneNumber = dr["PhoneNumber"].ToString();
                            lstregistrationDetails.Add(patientRegistration);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstregistrationDetails;
            }
        }
        #endregion

        #region getAllRegistrationDetailsByToday
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllRegistrationDetailsByToday")]
        [HttpGet]
        public List<PatientRegistration> getAllRegistrationDetailsByToday()
        {
            List<PatientRegistration> registrationDetails = new List<PatientRegistration>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //DateTime today = new DateTime();
                    string today = DateTime.Now.ToString("MM/dd/yyyy");
                    string strSQL = "SELECT * FROM pathoclinic.patientregistration Where DateOfReg = '" + today + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            PatientRegistration registration = new PatientRegistration();


                            registration.FirstName = dr["FirstName"].ToString();
                            registration.LastName = dr["LastName"].ToString();
                            registration.DateOfReg = dr["DateOfReg"].ToString();
                            registration.Age = (int)dr["Age"];
                            if (registration.Age == 0)
                            {

                                registration.AgeCategory = dr["AgeCategory"].ToString();
                                registration.Age = (int)dr["UnknownAge"];
                            }

                            registration.PhoneNumber = dr["PhoneNumber"].ToString();
                            registration.Sex = dr["Sex"].ToString();
                            registration.RegID = Convert.ToInt16(dr["RegID"]);

                            registrationDetails.Add(registration);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return registrationDetails;
            }
        }
        #endregion


        #region getAllDetailsForApprover
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllDetailsForApprover")]
        [HttpGet]
        public List<Approver> getAllDetailsForApprover()
        {
            List<Approver> lstApproverDetails = new List<Approver>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.approver Where Status = 0";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Approver approver = new Approver();

                            approver.Id = (int)dr["Id"];
                            approver.PatientName = dr["PatientName"].ToString();
                            approver.CreateDate = dr["CreateDate"].ToString();
                            approver.InvoiceNumber = dr["InvoiceNumber"].ToString();
                            approver.MRD = dr["MRD"].ToString();
                            approver.LapTest = dr["LapTest"].ToString();
                            lstApproverDetails.Add(approver);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstApproverDetails;
            }
        }
        #endregion


        //New


        #region Patient Registration

        #region insertPatientRegistration
        /// <summary>
        /// Table - PatientRegistration
        /// </summary>
        /// This service helps to add new patient Visit.
        /// <param name="RegID"></param>
        [Route("api/Account/insertPatientRegistration")]
        [HttpPost]
        public void insertPatientRegistration(PatientRegistration registration)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.PatientRegistration(Title,FirstName,MiddleName,LastName,Guardian,Relation,PatientType,Sex,MaritalStatus,DOB,Age,DateOfReg,PhoneNumber,ProfilePicture,AgeDay,AgeMonth,AgeYear,UnknownAge,AgeCategory,ContactEmail) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16,@val17,@val18,@val19,@val20)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", registration.Title);
                    cmd.Parameters.AddWithValue("@val2", registration.FirstName);
                    cmd.Parameters.AddWithValue("@val3", registration.MiddleName);
                    cmd.Parameters.AddWithValue("@val4", registration.LastName);
                    cmd.Parameters.AddWithValue("@val5", registration.Guardian);
                    cmd.Parameters.AddWithValue("@val6", registration.Relation);
                    cmd.Parameters.AddWithValue("@val7", registration.PatientType);
                    cmd.Parameters.AddWithValue("@val8", registration.Sex);
                    cmd.Parameters.AddWithValue("@val9", registration.MaritalStatus);


                    if (registration.DOB == null)
                    {
                        DateTime dobDynamic = DateTime.Now.Date;
                        if (registration.AgeCategory == "Months")
                        {
                            int result = registration.UnknownAge;
                            dobDynamic = DateTime.Today.AddMonths(-result);
                            registration.DOB = Convert.ToString(dobDynamic.ToString("MM/dd/yyyy"));
                            cmd.Parameters.AddWithValue("@val10", registration.DOB);

                        }
                        else if (registration.AgeCategory == "Days")
                        {
                            int result = registration.UnknownAge;
                            dobDynamic = DateTime.Today.AddDays(-result);
                            registration.DOB = Convert.ToString(dobDynamic.ToString("MM/dd/yyyy"));
                            cmd.Parameters.AddWithValue("@val10", registration.DOB);
                        }
                        else
                        {
                            if (registration.Age != 0)
                            {

                                dobDynamic = DateTime.Today.AddYears(-registration.Age);
                                registration.DOB = Convert.ToString(dobDynamic.ToString("MM/dd/yyyy"));
                                cmd.Parameters.AddWithValue("@val10", registration.DOB);
                            }
                        }


                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@val10", registration.DOB);
                    }


                    cmd.Parameters.AddWithValue("@val11", registration.Age);
                    cmd.Parameters.AddWithValue("@val12", registration.DateOfReg);
                    cmd.Parameters.AddWithValue("@val13", registration.PhoneNumber);
                    cmd.Parameters.AddWithValue("@val14", registration.ProfilePicture);

                    cmd.Parameters.AddWithValue("@val15", registration.AgeDay);
                    cmd.Parameters.AddWithValue("@val16", registration.AgeMonth);
                    cmd.Parameters.AddWithValue("@val17", registration.AgeYear);

                    cmd.Parameters.AddWithValue("@val18", registration.UnknownAge);
                    cmd.Parameters.AddWithValue("@val19", registration.AgeCategory);
                    cmd.Parameters.AddWithValue("@val20", registration.ContactEmail);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region insertPatientContactInfo
        /// <summary>
        /// Table - PatientContactInfo
        /// </summary>
        /// This service helps to add patient contact information for already created patient in PatientRegistration.Here RegID referred as foriengn key of PatientRegistration table.
        /// <param name="RegID"></param>
        [Route("api/Account/insertPatientContactInfo")]
        [HttpPost]
        public void insertPatientContactInfo(PatientContactInfo contactInfo)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.PatientContactInfo(RegID,DoorNo,FlatName,StreetName1,StreetName2,City,State,MobileNO,NotifySMS,PhoneNo,Locality,Pincode,NotifyEmail,ContactEmail) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", contactInfo.RegID);
                    cmd.Parameters.AddWithValue("@val2", contactInfo.DoorNo);
                    cmd.Parameters.AddWithValue("@val3", contactInfo.FlatName);
                    cmd.Parameters.AddWithValue("@val4", contactInfo.StreetName1);
                    cmd.Parameters.AddWithValue("@val5", contactInfo.StreetName2);
                    cmd.Parameters.AddWithValue("@val6", contactInfo.City);
                    cmd.Parameters.AddWithValue("@val7", contactInfo.State);
                    cmd.Parameters.AddWithValue("@val8", contactInfo.MobileNO);
                    cmd.Parameters.AddWithValue("@val9", contactInfo.NotifySMS);
                    cmd.Parameters.AddWithValue("@val10", contactInfo.PhoneNo);
                    cmd.Parameters.AddWithValue("@val11", contactInfo.Locality);
                    cmd.Parameters.AddWithValue("@val12", contactInfo.Pincode);
                    cmd.Parameters.AddWithValue("@val13", contactInfo.NotifyEmail);
                    cmd.Parameters.AddWithValue("@val14", contactInfo.ContactEmail);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region insertPatientSocialInfo
        /// <summary>
        /// Table - PatientSocialInfo
        /// </summary>
        /// This service helps to add patient contact information for already created patient in PatientRegistration.Here RegID referred as foriengn key of PatientRegistration table.
        /// <param name="RegID"></param>
        [Route("api/Account/insertPatientSocialInfo")]
        [HttpPost]
        public void insertPatientSocialInfo(PatientSocialInfo socialInfo)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.patientsocialinfo(RegID,SocialRelationship,Name,EmploymentStatus,EmployerName,EmployerAddress,City,State,IncomeGroup) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9)";
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", socialInfo.RegID);
                    cmd.Parameters.AddWithValue("@val2", socialInfo.SocialRelationship);
                    cmd.Parameters.AddWithValue("@val3", socialInfo.Name);
                    cmd.Parameters.AddWithValue("@val4", socialInfo.EmploymentStatus);
                    cmd.Parameters.AddWithValue("@val5", socialInfo.EmployerName);
                    cmd.Parameters.AddWithValue("@val6", socialInfo.EmployerAddress);
                    cmd.Parameters.AddWithValue("@val7", socialInfo.City);
                    cmd.Parameters.AddWithValue("@val8", socialInfo.State);
                    cmd.Parameters.AddWithValue("@val9", socialInfo.IncomeGroup);


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region insertPatientEmergencyContact
        /// <summary>
        /// Table - PatientEmergencyContact
        /// </summary>
        /// This service helps to add patient emergency Contact for already created patient in PatientRegistration. Here RegID referred as foriengn key of PatientRegistration table.
        /// <param name="RegID"></param>
        [Route("api/Account/insertPatientEmergencyContact")]
        [HttpPost]
        public void insertPatientEmergencyContact(PatientEmergencyContact emergencyContact)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO pathoclinic.PatientEmergencyContact(RegID,ContactName,EmergencyRelationShip,ContactNo) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", emergencyContact.RegID);
                    cmd.Parameters.AddWithValue("@val2", emergencyContact.ContactName);
                    cmd.Parameters.AddWithValue("@val4", emergencyContact.EmergencyRelationShip);
                    cmd.Parameters.AddWithValue("@val3", emergencyContact.ContactNo);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getPatientAllRegistrationInfo

        /// <summary>
        /// getPatientAllRegistrationInfo
        /// </summary>
        /// Pass the reg ID as a parameter and get all patient from patientregistration, PatientContactInfo, PatientSocialInfo and PatientEmergencyContact tables and return it.
        ///  /// <param name="regID"></param>
        /// <returns name="patientAllRegistrationInfo"></returns>
        /// 

        [Route("api/Account/getPatientAllRegistrationInfo")]
        [HttpGet]
        public PatientAllRegistrationInfo getPatientAllRegistrationInfo(int regID)
        {
            PatientAllRegistrationInfo patientAllRegistrationInfo = new PatientAllRegistrationInfo();

            patientAllRegistrationInfo.patientContactInfo = new PatientContactInfo();
            patientAllRegistrationInfo.patientRegistration = new PatientRegistration();
            patientAllRegistrationInfo.patientSocialInfo = new PatientSocialInfo();
            patientAllRegistrationInfo.patientEmgContact = new PatientEmergencyContact();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT pReg.Regid,pReg.Title,pReg.FirstName,pReg.MiddleName,pReg.LastName,pReg.Guardian,pReg.Relation,pReg.PatientType,pReg.Sex,pReg.MaritalStatus,pReg.DOB,pReg.Age,pReg.DateOfReg,pReg.ProfilePicture,pContact.DoorNo,pContact.FlatName,pContact.StreetName1,pContact.StreetName2,pContact.City,pContact.State,pContact.MobileNO,pContact.NotifySMS,pContact.PhoneNo,pContact.Locality,pContact.Pincode,pContact.NotifyEmail,pContact.ContactEmail,pSocial.SocialRelationship,pSocial.Name,pSocial.EmploymentStatus,pSocial.EmployerName,pSocial.EmployerAddress,pSocial.City,pSocial.State,pSocial.IncomeGroup,pEmergency.ContactName,pEmergency.ContactNo,pEmergency.EmergencyRelationShip FROM pathoclinic.patientregistration pReg INNER JOIN pathoclinic.PatientContactInfo pContact ON pReg.RegID = pContact.RegID INNER JOIN pathoclinic.PatientSocialInfo pSocial ON pSocial.RegID = pContact.RegID INNER JOIN pathoclinic.PatientEmergencyContact pEmergency ON pEmergency.RegID = '" + regID + "'";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            //patientregistration table
                            patientAllRegistrationInfo.patientRegistration.RegID = Convert.ToInt32(dr["RegID"]);
                            patientAllRegistrationInfo.patientRegistration.Title = dr["Title"].ToString();
                            patientAllRegistrationInfo.patientRegistration.FirstName = dr["FirstName"].ToString();
                            patientAllRegistrationInfo.patientRegistration.MiddleName = dr["MiddleName"].ToString();
                            patientAllRegistrationInfo.patientRegistration.LastName = dr["LastName"].ToString();
                            patientAllRegistrationInfo.patientRegistration.Guardian = dr["Guardian"].ToString();
                            patientAllRegistrationInfo.patientRegistration.Relation = dr["Relation"].ToString();
                            patientAllRegistrationInfo.patientRegistration.PatientType = dr["PatientType"].ToString();
                            patientAllRegistrationInfo.patientRegistration.Sex = dr["Sex"].ToString();
                            patientAllRegistrationInfo.patientRegistration.MaritalStatus = dr["MaritalStatus"].ToString();
                            patientAllRegistrationInfo.patientRegistration.DOB = dr["DOB"].ToString();
                            patientAllRegistrationInfo.patientRegistration.Age = Convert.ToInt16(dr["age"]);
                            patientAllRegistrationInfo.patientRegistration.DateOfReg = dr["DateOfReg"].ToString();

                            //PatientEmergencyContact
                            patientAllRegistrationInfo.patientEmgContact.ContactName = dr["ContactName"].ToString();
                            patientAllRegistrationInfo.patientEmgContact.EmergencyRelationShip = dr["EmergencyRelationShip"].ToString();
                            patientAllRegistrationInfo.patientEmgContact.ContactNo = Convert.ToInt64(dr["ContactNo"]);

                            //PatientSocialInfo
                            patientAllRegistrationInfo.patientSocialInfo.SocialRelationship = dr["SocialRelationship"].ToString();
                            patientAllRegistrationInfo.patientSocialInfo.Name = dr["Name"].ToString();
                            patientAllRegistrationInfo.patientSocialInfo.EmploymentStatus = dr["EmploymentStatus"].ToString();
                            patientAllRegistrationInfo.patientSocialInfo.EmployerName = dr["EmployerName"].ToString();
                            patientAllRegistrationInfo.patientSocialInfo.EmployerAddress = dr["EmployerAddress"].ToString();
                            patientAllRegistrationInfo.patientSocialInfo.City = dr["City"].ToString();
                            patientAllRegistrationInfo.patientSocialInfo.State = dr["State"].ToString();
                            patientAllRegistrationInfo.patientSocialInfo.IncomeGroup = dr["IncomeGroup"].ToString();

                            //PatientContactInfo
                            patientAllRegistrationInfo.patientContactInfo.DoorNo = dr["DoorNo"].ToString();
                            patientAllRegistrationInfo.patientContactInfo.FlatName = dr["FlatName"].ToString();
                            patientAllRegistrationInfo.patientContactInfo.StreetName1 = dr["StreetName1"].ToString();
                            patientAllRegistrationInfo.patientContactInfo.StreetName2 = dr["StreetName2"].ToString();
                            patientAllRegistrationInfo.patientContactInfo.City = dr["City"].ToString();
                            patientAllRegistrationInfo.patientContactInfo.State = dr["State"].ToString();
                            patientAllRegistrationInfo.patientContactInfo.MobileNO = Convert.ToInt64(dr["ContactNo"]);
                            patientAllRegistrationInfo.patientContactInfo.NotifySMS = Convert.ToBoolean(dr["NotifySMS"]);
                            patientAllRegistrationInfo.patientContactInfo.PhoneNo = Convert.ToInt64(dr["PhoneNo"]); ;
                            patientAllRegistrationInfo.patientContactInfo.Locality = dr["Locality"].ToString();
                            patientAllRegistrationInfo.patientContactInfo.Pincode = Convert.ToInt64(dr["Pincode"]); ;
                            patientAllRegistrationInfo.patientContactInfo.NotifyEmail = Convert.ToBoolean(dr["NotifyEmail"]);
                            patientAllRegistrationInfo.patientContactInfo.ContactEmail = dr["ContactEmail"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return patientAllRegistrationInfo;
            }
        }

        #endregion


        #region getPatientRegistrationInfoByRegID

        /// <summary>
        /// getPatientAllRegistrationInfo
        /// </summary>
        /// Pass the reg ID as a parameter and get all patient from patientregistration, PatientContactInfo, PatientSocialInfo and PatientEmergencyContact tables and return it.
        ///  /// <param name="regID"></param>
        /// <returns name="patientAllRegistrationInfo"></returns>
        /// 

        [Route("api/Account/getPatientRegistrationInfoByRegID")]
        [HttpGet]
        public PatientAllRegistrationInfo getPatientRegistrationInfoByRegID(int regID)
        {
            PatientAllRegistrationInfo patientAllRegistrationInfo = new PatientAllRegistrationInfo();
            patientAllRegistrationInfo.patientRegistration = new PatientRegistration();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * from pathoclinic.patientregistration where RegID = '" + regID + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            //patientregistration table
                            patientAllRegistrationInfo.patientRegistration.RegID = Convert.ToInt32(dr["RegID"]);
                            patientAllRegistrationInfo.patientRegistration.Title = dr["Title"].ToString();
                            patientAllRegistrationInfo.patientRegistration.FirstName = dr["FirstName"].ToString();
                            patientAllRegistrationInfo.patientRegistration.MiddleName = dr["MiddleName"].ToString();
                            patientAllRegistrationInfo.patientRegistration.LastName = dr["LastName"].ToString();
                            patientAllRegistrationInfo.patientRegistration.Guardian = dr["Guardian"].ToString();
                            patientAllRegistrationInfo.patientRegistration.Relation = dr["Relation"].ToString();
                            patientAllRegistrationInfo.patientRegistration.PatientType = dr["PatientType"].ToString();
                            patientAllRegistrationInfo.patientRegistration.Sex = dr["Sex"].ToString();
                            patientAllRegistrationInfo.patientRegistration.MaritalStatus = dr["MaritalStatus"].ToString();
                            patientAllRegistrationInfo.patientRegistration.DOB = dr["DOB"].ToString();
                            patientAllRegistrationInfo.patientRegistration.Age = Convert.ToInt16(dr["age"]);
                            patientAllRegistrationInfo.patientRegistration.DateOfReg = dr["DateOfReg"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return patientAllRegistrationInfo;
            }
        }

        #endregion

        #endregion




        #region getParentRegistraionIdForGroup
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getParentRegistraionIdForGroup")]
        [HttpGet]
        public GetRegistration getParentRegistraionIdForGroup(string firstName, string lastName, string DateOfReg)
        {
            GetRegistration registrationId = new GetRegistration();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT RegID,Sex  from pathoclinic.grouppatientregistration Where FirstName = '" + firstName + "' && LastName = '" + lastName + "'&& DateOfReg='" + DateOfReg + "'";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            registrationId.RegId = (int)(dr["RegId"]);
                            registrationId.Sex = dr["Sex"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return registrationId;
            }
        }
        #endregion
        #region getParentRegistraionId
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getParentRegistraionId")]
        [HttpGet]
        public GetRegistration getParentRegistraionId(string phoneNumber)
        {
            GetRegistration registrationId = new GetRegistration();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT RegID,Sex  from pathoclinic.patientregistration Where PhoneNumber = '" + phoneNumber + "'";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            registrationId.RegId = (int)(dr["RegId"]);
                            registrationId.Sex = dr["Sex"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return registrationId;
            }
        }
        #endregion

        #region LabOrder
        #region getLabOrderMasterList
        /// <summary>
        /// getLabOrderMasterList 
        /// </summary>
        /// To get all records from LabOrderMasterList table.
        /// <returns></returns>

        [Route("api/Account/getLabOrderMasterList")]
        [HttpGet]
        public List<LabOrderMasterList> getLabOrderMasterList()
        {
            List<LabOrderMasterList> lstLabOrderMaster = new List<LabOrderMasterList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.labordermasterlist";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            LabOrderMasterList LabOrderMaster = new LabOrderMasterList();
                            LabOrderMaster.ProfileID = Convert.ToInt16(dr["ProfileID"]);
                            LabOrderMaster.ProfileName = dr["ProfileName"].ToString();
                            LabOrderMaster.Amount = Convert.ToDouble(dr["Amount"]);

                            lstLabOrderMaster.Add(LabOrderMaster);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabOrderMaster;
            }
        }
        #endregion



        #region getLabOrderTestList
        /// <summary>
        /// getLabOrderTestList
        /// </summary>
        /// Pass the profileID as a parameter and get all records from labordertestlist.
        /// <returns></returns>

        [Route("api/Account/getLabOrderTestList")]
        [HttpGet]
        public List<LabOrderTestList> getLabOrderTestList(int profileID)
        {
            List<LabOrderTestList> lstLabOrderTest = new List<LabOrderTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.labordertestlist where ProfileID ='" + profileID + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            LabOrderTestList labTestOrder = new LabOrderTestList();
                            labTestOrder.TestID = Convert.ToInt16(dr["TestID"]);
                            labTestOrder.ProfileID = Convert.ToInt16(dr["ProfileID"]);
                            labTestOrder.TestName = dr["TestName"].ToString();
                            labTestOrder.Amount = Convert.ToInt64(dr["Amount"]);

                            lstLabOrderTest.Add(labTestOrder);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabOrderTest;
            }
        }
        #endregion

        #endregion
        //

        //Lab Order Insertion
        #region AllInsertLabOrder

        #region insertDepartmentDetails
        /// <summary>
        /// Table - Department
        /// </summary>
        /// Inserted the department details
        /// <param name="department"></param>
        [Route("api/Account/insertDepartmentDetails")]
        [HttpPost]
        public void insertDepartmentDetails(Department department)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DateTime today = DateTime.Today;
                    department.CreateDate = today.ToString();

                    DateTime now = DateTime.Now;

                    Console.WriteLine(today);
                    Console.WriteLine(now);



                    string strSQL = "INSERT INTO pathoclinic.Department(DepartmentName,CreateDate) VALUES(@val1,@val2)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", department.DepartmentName);
                    cmd.Parameters.AddWithValue("@val2", department.CreateDate);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region insertMasterProfileList
        /// <summary>
        /// Table - MasterProfileList
        /// </summary>
        /// Inserted the MasterProfileList table values. The Department Id reffered from Department table.
        /// <param name="masterProfileList"></param>
        [Route("api/Account/insertMasterProfileList")]
        [HttpPost]
        public void insertMasterProfileList(MasterProfileList masterProfileList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DateTime today = DateTime.Today;
                    masterProfileList.CreateDate = today.ToShortDateString();
                    string strSQL = "INSERT INTO MasterProfileList(DepartmentID,ProfileName,Amount,CreateDate) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", masterProfileList.DepartmentID);
                    cmd.Parameters.AddWithValue("@val2", masterProfileList.ProfileName);
                    cmd.Parameters.AddWithValue("@val3", masterProfileList.Amount);
                    cmd.Parameters.AddWithValue("@val4", masterProfileList.CreateDate);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region insertChildTestList
        /// <summary>
        /// Table - ChildTestList
        /// Inserted the ChildTestList table values. The Profile ID reffered from MasterProfileList table.
        /// </summary>
        /// <param name="childTestList"></param>
        [Route("api/Account/insertChildTestList")]
        [HttpPost]
        public void insertChildTestList(ChildTestList ChildTestList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    //  ChildTestList.ProfileID = 1;
                    string testCode = "";
                    string fetch = "Select Max(TestCode) from ChildTestList";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(fetch, conn);
                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            testCode = dr[0].ToString();

                        }
                        if (testCode.Trim() != "")
                        {
                            var tempArray = testCode.Split('T');
                            string tempId = tempArray[2];
                            int id = Convert.ToInt32(tempId);
                            id = id + 1;
                            testCode = "TST" + String.Format("{0:000}", id);
                        }
                        else
                        {
                            testCode = "TST" + String.Format("{0:000}", 1);
                        }
                    }



                    DateTime today = DateTime.Today;
                    ChildTestList.CreateDate = today.ToShortDateString();
                    string strSQL = "INSERT INTO ChildTestList(ProfileID,TestName,UnitMeasurementNumeric,UnitMesurementFreeText,TableRequiredPrint,DefaultValues,GenderMale,GenderFemale,AdditionalFixedComments,LowerCriticalValue,UpperCriticalValue,OtherCriticalReport,AgewiseCriticalValue,AgewiseReferenceRange,units,TurnAroundTime,RequiredBiospyTestNumber,RequiredSamples,PatientPreparation,ExpectedResultDate,Amount,Finaloutput,TestbasedDiscount,Outsourced,CreateDate,SampleContainer,Methodology,NumericOrText,Pregnancyrefrange,TestCode,DepartmentName,Priority,multiplecomponents,AlternativeSampleContainer,DisplayTestName,AmountValidDate,TestSchedule,cutOffTime,TestInformation,CalculationPresent,ActiveStatus,InstrumentReagent,commonParagraph,UrineCulture,AgewiseSexReferenceValue,ProfilePriority,TestPriority) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val2,@val13,@val14,@val15,@val16,@val17,@val18,@val19,@val20,@val21,@val22,@val23,@val24,@val25,@val26,@val27,@val28,@val29,@val30,@val31,@val32,@val33,@val34,@val35,@val36,@val37,@val38,@val39,@val40,@val41,@val42,@val43,@val44,@val45,@val46,@val47)";
                    //      conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", ChildTestList.ProfileID);
                    cmd.Parameters.AddWithValue("@val2", ChildTestList.TestName);
                    cmd.Parameters.AddWithValue("@val3", ChildTestList.UnitMeasurementNumeric);
                    cmd.Parameters.AddWithValue("@val4", ChildTestList.UnitMesurementFreeText);
                    cmd.Parameters.AddWithValue("@val5", ChildTestList.TableRequiredPrint);
                    cmd.Parameters.AddWithValue("@val6", ChildTestList.DefaultValues);
                    cmd.Parameters.AddWithValue("@val7", ChildTestList.GenderMale);
                    cmd.Parameters.AddWithValue("@val8", ChildTestList.GenderFemale);
                    cmd.Parameters.AddWithValue("@val9", ChildTestList.AdditionalFixedComments);
                    cmd.Parameters.AddWithValue("@val10", ChildTestList.LowerCriticalValue);
                    cmd.Parameters.AddWithValue("@val11", ChildTestList.UpperCriticalValue);
                    cmd.Parameters.AddWithValue("@val12", ChildTestList.OtherCriticalReport);
                    cmd.Parameters.AddWithValue("@val13", ChildTestList.AgewiseCriticalValue);
                    cmd.Parameters.AddWithValue("@val14", ChildTestList.AgewiseReferenceRange);
                    cmd.Parameters.AddWithValue("@val15", ChildTestList.units);
                    cmd.Parameters.AddWithValue("@val16", ChildTestList.TurnAroundTime);
                    cmd.Parameters.AddWithValue("@val17", ChildTestList.RequiredBiospyTestNumber);
                    cmd.Parameters.AddWithValue("@val18", ChildTestList.RequiredSamples);
                    cmd.Parameters.AddWithValue("@val19", ChildTestList.PatientPreparation);
                    cmd.Parameters.AddWithValue("@val20", ChildTestList.ExpectedResultDate);
                    cmd.Parameters.AddWithValue("@val21", ChildTestList.Amount);
                    cmd.Parameters.AddWithValue("@val22", ChildTestList.Finaloutput);
                    cmd.Parameters.AddWithValue("@val23", ChildTestList.TestbasedDiscount);
                    cmd.Parameters.AddWithValue("@val24", ChildTestList.Outsourced);
                    cmd.Parameters.AddWithValue("@val25", ChildTestList.CreateDate);
                    cmd.Parameters.AddWithValue("@val26", ChildTestList.SampleContainer);
                    cmd.Parameters.AddWithValue("@val27", ChildTestList.Methodology);
                    cmd.Parameters.AddWithValue("@val28", ChildTestList.NumericOrText);
                    cmd.Parameters.AddWithValue("@val29", ChildTestList.Pregnancyrefrange);
                    cmd.Parameters.AddWithValue("@val30", testCode);
                    cmd.Parameters.AddWithValue("@val31", ChildTestList.DepartmentName);
                    cmd.Parameters.AddWithValue("@val32", ChildTestList.Priority);
                    cmd.Parameters.AddWithValue("@val33", ChildTestList.Multiplecomponents);
                    cmd.Parameters.AddWithValue("@val34", ChildTestList.AlternativeSample);
                    cmd.Parameters.AddWithValue("@val35", ChildTestList.DisplayName);
                    cmd.Parameters.AddWithValue("@val36", ChildTestList.ValidDate);
                    cmd.Parameters.AddWithValue("@val37", ChildTestList.TestSchedule);
                    cmd.Parameters.AddWithValue("@val38", ChildTestList.cutOffTime);
                    cmd.Parameters.AddWithValue("@val39", ChildTestList.TestInformation);
                    cmd.Parameters.AddWithValue("@val40", ChildTestList.CalculationPresent);
                    cmd.Parameters.AddWithValue("@val41", ChildTestList.ActiveStatus);
                    cmd.Parameters.AddWithValue("@val42", ChildTestList.InstrumentReagent);
                    cmd.Parameters.AddWithValue("@val43", ChildTestList.commonParagraph);
                    cmd.Parameters.AddWithValue("@val44", ChildTestList.UrineCulture);
                    cmd.Parameters.AddWithValue("@val45", ChildTestList.AgewiseSexReferenceValue);
                    cmd.Parameters.AddWithValue("@val46", ChildTestList.ProfilePriority);
                    cmd.Parameters.AddWithValue("@val47", ChildTestList.TestPriority);


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #endregion

        #region getAllLabOrder

        #region getAllDepartmentDetails
        /// <summary>
        /// Table - Department
        /// </summary>
        /// Listed all values from Department table
        /// <returns></returns>
        [Route("api/Account/getAllDepartmentDetails")]
        [HttpGet]
        public List<Department> getAllDepartmentDetails()
        {
            List<Department> departmentDetails = new List<Department>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Department";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Department department = new Department();

                            department.DepartmentID = (int)dr["DepartmentID"];
                            department.DepartmentName = dr["DepartmentName"].ToString();
                            department.CreateDate = dr["CreateDate"].ToString();
                            departmentDetails.Add(department);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return departmentDetails;
            }
        }
        #endregion

        #region getAllMasterProfileList
        /// <summary>
        ///  Table - MasterProfileList
        /// </summary>
        /// Listed all values from MasterProfileList table
        /// <returns></returns>
        [Route("api/Account/getAllMasterProfileList")]
        [HttpGet]
        public List<MasterProfileList> getAllMasterProfileList()
        {
            List<MasterProfileList> lstMasterProfileDetails = new List<MasterProfileList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.MasterProfileList";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            MasterProfileList lstMasterProfile = new MasterProfileList();

                            lstMasterProfile.ProfileID = (int)dr["ProfileID"];
                            //lstMasterProfile.DepartmentID = (int)dr["DepartmentID"]; ;
                            lstMasterProfile.ProfileName = dr["ProfileName"].ToString();
                            lstMasterProfile.ProfileCode = dr["ProfileCode"].ToString();
                            lstMasterProfile.Amount = (double)dr["Amount"];
                            lstMasterProfile.CreateDate = dr["CreateDate"].ToString();
                            lstMasterProfileDetails.Add(lstMasterProfile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstMasterProfileDetails;
            }
        }
        #endregion

        #endregion


        #region getLabOrderMasterListbyDepID
        /// <summary>
        /// getLabOrderMasterList 
        /// </summary>
        /// To get all records from LabOrderMasterList table.
        /// <returns></returns>

        [Route("api/Account/getLabOrderMasterListbyDepID")]
        [HttpGet]
        public List<LabOrderMasterList> getLabOrderMasterListbyDepID(int depID)
        {
            List<LabOrderMasterList> lstLabOrderMaster = new List<LabOrderMasterList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.masterprofilelist where DepartmentID='" + depID + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            LabOrderMasterList LabOrderMaster = new LabOrderMasterList();
                            LabOrderMaster.ProfileID = Convert.ToInt16(dr["ProfileID"]);
                            LabOrderMaster.ProfileName = dr["ProfileName"].ToString();
                            LabOrderMaster.Amount = Convert.ToDouble(dr["Amount"]);
                            lstLabOrderMaster.Add(LabOrderMaster);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabOrderMaster;
            }
        }
        #endregion

        #region laborder

        #region insertLabOrderDetails
        /// <summary>
        /// Table - LabOrder
        /// Inserted the LabOrder table values. The Reg ID reffered from PatientRegistration table.
        /// </summary>
        /// <param name="labOrder"></param>
        [Route("api/Account/insertLabOrderDetails")]
        [HttpPost]
        public void insertLabOrderDetails(LabOrder labOrder)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DateTime today = DateTime.Now;
                    labOrder.CreateDate = today;

                    string strSQL = "INSERT INTO LabOrder(RegID,PatientName,MrdNo,ReferredBy,ReferenceDiscountAmount,Height,Weight,PaymentTypeCode,PaymentTypeName,InsuranceName,CollectAt,HomeCollectChargeAmount,IsPregnancy,LMP,Trimester,DepartmentID,DepartmentName,Amount,CreateDate,ProviderName,ProviderID,ProviderHostName,LocationName,LocationCode,AccountHolderName,AccountHolderNumber) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16,@val17,@val18,@val19,@val20,@val21,@val22,@val23,@val24,@val25,@val26)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", labOrder.RegID);
                    cmd.Parameters.AddWithValue("@val2", labOrder.PatientName);
                    cmd.Parameters.AddWithValue("@val3", labOrder.MrdNo);
                    cmd.Parameters.AddWithValue("@val4", labOrder.ReferredBy);
                    cmd.Parameters.AddWithValue("@val5", labOrder.ReferenceDiscountAmount);
                    cmd.Parameters.AddWithValue("@val6", labOrder.Height);
                    cmd.Parameters.AddWithValue("@val7", labOrder.Weight);
                    cmd.Parameters.AddWithValue("@val8", labOrder.PaymentTypeCode);
                    cmd.Parameters.AddWithValue("@val9", labOrder.PaymentTypeName);
                    cmd.Parameters.AddWithValue("@val10", labOrder.InsuranceName);
                    cmd.Parameters.AddWithValue("@val11", labOrder.CollectAt);
                    cmd.Parameters.AddWithValue("@val12", labOrder.HomeCollectChargeAmount);
                    cmd.Parameters.AddWithValue("@val13", labOrder.IsPregnancy);
                    cmd.Parameters.AddWithValue("@val14", labOrder.LMP);
                    cmd.Parameters.AddWithValue("@val15", labOrder.Trimester);
                    cmd.Parameters.AddWithValue("@val16", labOrder.DepartmentID);
                    cmd.Parameters.AddWithValue("@val17", labOrder.DepartmentName);
                    cmd.Parameters.AddWithValue("@val18", labOrder.Amount);
                    cmd.Parameters.AddWithValue("@val19", labOrder.CreateDate);
                    cmd.Parameters.AddWithValue("@val20", labOrder.ProviderName);
                    cmd.Parameters.AddWithValue("@val21", labOrder.ProviderID);
                    cmd.Parameters.AddWithValue("@val22", labOrder.ProviderHostName);
                    cmd.Parameters.AddWithValue("@val23", labOrder.LocationName);
                    cmd.Parameters.AddWithValue("@val24", labOrder.LocationCode);

                    cmd.Parameters.AddWithValue("@val25", labOrder.AccountHolderName);
                    cmd.Parameters.AddWithValue("@val26", labOrder.AccountHolderNumber);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region getTestNameSearchDetails
        /// <summary>
        /// getTestNameSearchDetails 
        /// </summary>
        /// To get all records from ChildTestList table.
        /// <param name="testName"></param>
        /// <returns></returns>

        [Route("api/Account/getTestNameSearchDetails")]
        [HttpGet]
        public List<ChildTestList> getTestNameSearchDetails(string testName)
        {
            List<ChildTestList> lstTestNameSearchDetails = new List<ChildTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.ChildTestList Where TestName LIKE'%" + testName + "%' AND ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ChildTestList childTestName = new ChildTestList();

                            childTestName.TestID = Convert.ToInt32(dr["TestID"]);
                            childTestName.ProfileID = Convert.ToInt32(dr["ProfileID"]);
                            childTestName.TestName = dr["TestName"].ToString();
                            childTestName.UnitMeasurementNumeric = dr["UnitMeasurementNumeric"].ToString();
                            childTestName.UnitMesurementFreeText = dr["UnitMesurementFreeText"].ToString();
                            childTestName.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            childTestName.DefaultValues = dr["DefaultValues"].ToString();
                            childTestName.GenderMale = dr["GenderMale"].ToString();
                            childTestName.GenderFemale = dr["GenderFemale"].ToString();
                            childTestName.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            childTestName.LowerCriticalValue = dr["LowerCriticalValue"].ToString();
                            childTestName.UpperCriticalValue = dr["UpperCriticalValue"].ToString();
                            childTestName.OtherCriticalReport = dr["OtherCriticalReport"].ToString();
                            childTestName.AgewiseCriticalValue = dr["AgewiseCriticalValue"].ToString();
                            childTestName.AgewiseReferenceRange = dr["AgewiseReferenceRange"].ToString();
                            childTestName.units = dr["units"].ToString();
                            childTestName.TurnAroundTime = dr["TurnAroundTime"].ToString();
                            childTestName.RequiredBiospyTestNumber = dr["RequiredBiospyTestNumber"].ToString();
                            childTestName.RequiredSamples = dr["TestName"].ToString();
                            childTestName.PatientPreparation = dr["TestName"].ToString();
                            childTestName.ExpectedResultDate = dr["TestName"].ToString();
                            childTestName.Amount = Convert.ToDouble(dr["Amount"]);
                            childTestName.Finaloutput = dr["Finaloutput"].ToString();
                            childTestName.TestbasedDiscount = dr["RequiredBiospyTestNumber"].ToString();
                            childTestName.Outsourced = dr["Outsourced"].ToString();
                            childTestName.CreateDate = dr["CreateDate"].ToString();

                            lstTestNameSearchDetails.Add(childTestName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstTestNameSearchDetails;
            }
        }
        #endregion


        //#region updateStatusInvoice
        ///// <summary>
        ///// Table - Invoice
        ///// </summary>
        ///// <param name="childTestList"></param>
        //[Route("api/Account/Invoice")]
        //[HttpPost]
        //public void updateStatusInvoice(Invoice invoice)
        //{
        //    using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
        //    {
        //        try
        //        {
        //            DateTime today = DateTime.Today;
        //            invoice.InvoiceDate = today.ToShortDateString();
        //            string strSQL = "UPDATE  Invoice SET Status='1',InvoiceDate='" + invoice.InvoiceDate + "' where InvoiceNo='" + invoice.InvoiceNo + "'";
        //            conn.Open();
        //            MySqlCommand cmd = new MySqlCommand(strSQL, conn);
        //            cmd.CommandType = CommandType.Text;
        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception Ex)
        //        {
        //            string logdetails = Ex.InnerException.ToString();
        //        }
        //    }
        //}

        //#endregion


        #region insertInvoiceView
        /// <summary>
        /// Table - Invoice
        /// Inserted the invoiceview table values. The reg ID reffered from patientregistration table.
        /// </summary>
        /// <param name="childTestList"></param>
        [Route("api/Account/insertInvoiceView")]
        [HttpPost]
        public void insertInvoiceView(InvoiceView invoiceview)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DateTime today = DateTime.Today;
                    invoiceview.InvoiceDate = today.ToString("MM/dd/yyyy");
                    string strSQL = "INSERT INTO Invoice(RegID,PatientName,MrdNo,Status,Amount,Discount,NetAmount,PaidAmount,Action,InvoiceDate,InvoiceNo,Token,Description,PaymentType,ProviderHostName,HospitalName) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", invoiceview.RegID);
                    cmd.Parameters.AddWithValue("@val2", invoiceview.PatientName);
                    cmd.Parameters.AddWithValue("@val3", invoiceview.MrdNo);
                    cmd.Parameters.AddWithValue("@val4", invoiceview.Status);
                    cmd.Parameters.AddWithValue("@val5", invoiceview.Amount);
                    cmd.Parameters.AddWithValue("@val6", invoiceview.Discount);
                    cmd.Parameters.AddWithValue("@val7", invoiceview.NetAmount);
                    cmd.Parameters.AddWithValue("@val8", invoiceview.PaidAmount);
                    cmd.Parameters.AddWithValue("@val9", invoiceview.Action);
                    cmd.Parameters.AddWithValue("@val10", invoiceview.InvoiceDate);
                    cmd.Parameters.AddWithValue("@val11", invoiceview.InvoiceNo);
                    cmd.Parameters.AddWithValue("@val12", invoiceview.Token);
                    cmd.Parameters.AddWithValue("@val13", invoiceview.Description);
                    cmd.Parameters.AddWithValue("@val14", invoiceview.PaymentType);
                    cmd.Parameters.AddWithValue("@val15", invoiceview.ProviderHostName);
                    cmd.Parameters.AddWithValue("@val16", invoiceview.HospitalName);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getInvoiceDetailsByRegId
        /// <summary>
        /// Table - Invoice
        /// </summary>
        /// Listed all values from Invoice table
        /// <returns></returns>
        [Route("api/Account/getAllInvoiceDetailsById")]
        [HttpGet]
        public InvoiceView getAllInvoiceDetailsById(int id)
        {
            InvoiceView invoiceview = new InvoiceView();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Invoice where Status = 'In progress' && RegID ='" + id + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            invoiceview.Id = (int)dr["Id"];
                            invoiceview.RegID = (int)dr["RegID"];
                            invoiceview.MrdNo = dr["MrdNo"].ToString();
                            invoiceview.PatientName = dr["PatientName"].ToString();
                            invoiceview.Status = dr["Status"].ToString();
                            invoiceview.Amount = dr["Amount"].ToString();
                            invoiceview.Discount = dr["Discount"].ToString();
                            invoiceview.NetAmount = dr["NetAmount"].ToString();
                            invoiceview.PaidAmount = dr["PaidAmount"].ToString();
                            invoiceview.Action = dr["Action"].ToString();
                            invoiceview.InvoiceDate = dr["InvoiceDate"].ToString();
                            invoiceview.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoiceview.Token = dr["Token"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return invoiceview;
            }
        }
        #endregion


        #region getInvoiceSubmitDetailsByRegId
        /// <summary>
        /// Table - Invoice
        /// </summary>
        /// Listed all values from Invoice table
        /// <returns></returns>
        [Route("api/Account/getInvoiceSubmitDetailsByRegId")]
        [HttpGet]
        public InvoiceView getInvoiceSubmitDetailsByRegId(int id)
        {
            InvoiceView invoiceview = new InvoiceView();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Invoice where Status = 'In progress' && RegID ='" + id + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            invoiceview.Id = (int)dr["Id"];
                            invoiceview.RegID = (int)dr["RegID"];
                            invoiceview.MrdNo = dr["MrdNo"].ToString();
                            invoiceview.PatientName = dr["PatientName"].ToString();
                            invoiceview.Status = dr["Status"].ToString();
                            invoiceview.Amount = dr["Amount"].ToString();
                            invoiceview.Discount = dr["Discount"].ToString();
                            invoiceview.NetAmount = dr["NetAmount"].ToString();
                            invoiceview.PaidAmount = dr["PaidAmount"].ToString();
                            invoiceview.Action = dr["Action"].ToString();
                            invoiceview.InvoiceDate = dr["InvoiceDate"].ToString();
                            invoiceview.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoiceview.Token = dr["Token"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return invoiceview;
            }
        }
        #endregion


        #region getPaymentSubmitDetailsByInvoiceNo
        /// <summary>
        /// Table - Invoice
        /// </summary>
        /// Listed all values from Invoice table
        /// <returns></returns>
        [Route("api/Account/getPaymentSubmitDetailsByInvoiceNo")]
        [HttpGet]
        public Payment getPaymentSubmitDetailsByInvoiceNo(string invoiceNo)
        {
            Payment payment = new Payment();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Payment where Status = 'Completed' && InvoiceNo ='" + invoiceNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            payment.PaymentId = (int)dr["PaymentId"];
                            payment.RegID = (int)dr["RegID"];
                            payment.MrdNo = dr["MrdNo"].ToString();
                            payment.PaymentType = dr["PaymentType"].ToString();
                            payment.PaymentDate = dr["PaymentDate"].ToString();
                            payment.InvoiceNo = dr["InvoiceNo"].ToString();
                            payment.Status = dr["Status"].ToString();
                            payment.NetAmount = Convert.ToDouble(dr["NetAmount"]);
                            payment.PendingAmount = Convert.ToDouble(dr["PendingAmount"]);
                            payment.Status = dr["Status"].ToString();
                            payment.PaidAmount = Convert.ToDouble(dr["PaidAmount"]);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return payment;
            }
        }
        #endregion

        //#region getAllInvoiceDetails
        ///// <summary>
        ///// Table - Invoice
        ///// </summary>
        ///// Listed all values from Invoice table
        ///// <returns></returns>
        //[Route("api/Account/getAllInvoiceDetails")]
        //[HttpGet]
        //public List<InvoiceView> getAllInvoiceDetails()
        //{
        //    List<InvoiceView> InvoiceViewDetails = new List<InvoiceView>();
        //    DataTable dt = new DataTable();
        //    using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
        //    {
        //        try
        //        {
        //            string strSQL = "SELECT * FROM pathoclinic.Invoice where Status = '0'";
        //            conn.Open();
        //            MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
        //            MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
        //            DataSet ds = new DataSet();
        //            mydata.Fill(ds);
        //            dt = ds.Tables[0];
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                if (dr != null)
        //                {
        //                    InvoiceView invoiceview = new InvoiceView();
        //                    invoiceview.Id = (int)dr["Id"];
        //                    invoiceview.RegID = (int)dr["RegID"];
        //                    invoiceview.MrdNo = dr["MrdNo"].ToString();
        //                    invoiceview.PatientName = dr["PatientName"].ToString();
        //                    invoiceview.Status = dr["Status"].ToString();
        //                    invoiceview.Amount = dr["Amount"].ToString();
        //                    invoiceview.Discount = dr["Discount"].ToString();
        //                    invoiceview.NetAmount = dr["NetAmount"].ToString();
        //                    invoiceview.PaidAmount = dr["PaidAmount"].ToString();
        //                    invoiceview.Action = dr["Action"].ToString();
        //                    invoiceview.InvoiceDate = dr["InvoiceDate"].ToString();
        //                    invoiceview.InvoiceNo = dr["InvoiceNo"].ToString();
        //                    invoiceview.Token = dr["Token"].ToString();
        //                    InvoiceViewDetails.Add(invoiceview);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.Write(ex);
        //        }
        //        return InvoiceViewDetails;
        //    }
        //}
        //#endregion

        #region getAllLabOrderDetails

        /// <summary>
        /// getAllLabOrderDetails
        /// </summary>
        /// To get all records from LabOrder table.
        ///  /// <param name=" "></param>
        /// <returns name="getAllOrderDetails"></returns>
        /// 
        [Route("api/Account/getAllLabOrderDetails")]
        [HttpGet]

        public List<LabOrder> getAllLabOrderDetails()
        {
            List<LabOrder> lstOrderDetails = new List<LabOrder>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * from labOrder";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrder labOrderDetails = new LabOrder();
                            labOrderDetails.LabId = Convert.ToInt32(dr["LabId"]);
                            labOrderDetails.RegID = Convert.ToInt32(dr["RegID"]);
                            labOrderDetails.PatientName = dr["PatientName"].ToString();
                            labOrderDetails.MrdNo = dr["MrdNo"].ToString();
                            labOrderDetails.ReferredBy = dr["ReferredBy"].ToString();
                            labOrderDetails.ReferenceDiscountAmount = Convert.ToDouble(dr["ReferenceDiscountAmount"]);
                            labOrderDetails.Height = (float)(dr["Height"]);
                            labOrderDetails.Weight = (float)(dr["Weight"]);
                            //labOrderDetails.PaymentType = dr["PaymentType"].ToString();
                            labOrderDetails.CollectAt = dr["CollectAt"].ToString();
                            labOrderDetails.HomeCollectChargeAmount = Convert.ToDouble(dr["HomeCollectChargeAmount"]);
                            labOrderDetails.IsPregnancy = Convert.ToInt16(dr["IsPregnancy"]);
                            labOrderDetails.LMP = dr["LMP"].ToString();
                            labOrderDetails.Trimester = dr["Trimester"].ToString();
                            labOrderDetails.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                            labOrderDetails.DepartmentName = dr["DepartmentName"].ToString();
                            labOrderDetails.Amount = Convert.ToDouble(dr["Amount"]);
                            labOrderDetails.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                            lstOrderDetails.Add(labOrderDetails);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstOrderDetails;
            }
        }

        #endregion

        #region Doctor

        #region getDoctorListByRegID
        /// <summary>
        ///  Table - Doctor
        /// </summary>
        /// Listed all values from MasterProfileList table
        /// <param name="regId"></param>
        /// <returns></returns>
        [Route("api/Account/getDoctorListByRegID")]
        [HttpGet]
        public List<Doctor> getDoctorListByRegID()
        {
            List<Doctor> lstDoctorDetails = new List<Doctor>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT * FROM pathoclinic.Doctor where RegID='" + regId + "' ";
                    string strSQL = "SELECT * FROM pathoclinic.Doctor";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Doctor doctorDetails = new Doctor();
                            doctorDetails.DoctorId = (int)dr["DoctorId"];
                            doctorDetails.RegID = (int)dr["RegID"];
                            doctorDetails.DoctorName = dr["DoctorName"].ToString();
                            doctorDetails.EmailId = dr["EmailId"].ToString();
                            doctorDetails.PhoneNo = (int)(dr["PhoneNo"]);

                            lstDoctorDetails.Add(doctorDetails);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstDoctorDetails;
            }
        }
        #endregion

        #region insertDoctorDetails
        /// <summary>
        /// Table - Doctor
        /// Insert the Doctor table values.
        /// </summary>
        /// <param name="docdor"></param>
        [Route("api/Account/insertDoctorDetails")]
        [HttpPost]
        public void insertDoctorDetails(Doctor doctor)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "INSERT INTO Doctor(RegID,DoctorName,EmailId,PhoneNo) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", doctor.RegID);
                    cmd.Parameters.AddWithValue("@val2", doctor.DoctorName);
                    cmd.Parameters.AddWithValue("@val3", doctor.EmailId);
                    cmd.Parameters.AddWithValue("@val4", doctor.PhoneNo);


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #endregion


        #region getInvoiceDetailsByInvoiceNo
        /// <summary>
        /// Table - Invoice
        /// </summary>
        /// get invoice details from Invoice table by invoiceNo
        /// <param "invoiceNo"></param>
        /// <returns></returns>
        [Route("api/Account/getInvoiceDetailsByInvoiceNo")]
        [HttpGet]
        public List<InvoiceView> getInvoiceDetailsByInvoiceNo(int invoiceNo)
        {
            List<InvoiceView> InvoiceViewDetails = new List<InvoiceView>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Invoice where InvoiceNo='" + invoiceNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InvoiceView invoiceview = new InvoiceView();
                            invoiceview.Id = (int)dr["Id"];
                            invoiceview.RegID = (int)dr["RegID"];
                            invoiceview.MrdNo = dr["MrdNo"].ToString();
                            invoiceview.PatientName = dr["PatientName"].ToString();
                            invoiceview.Status = dr["Status"].ToString();
                            invoiceview.Amount = dr["Amount"].ToString();
                            invoiceview.Discount = dr["Discount"].ToString();
                            invoiceview.NetAmount = dr["NetAmount"].ToString();
                            invoiceview.PaidAmount = dr["PaidAmount"].ToString();
                            invoiceview.Action = dr["Action"].ToString();
                            invoiceview.InvoiceDate = dr["InvoiceDate"].ToString();
                            invoiceview.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoiceview.Token = dr["Token"].ToString();
                            InvoiceViewDetails.Add(invoiceview);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceViewDetails;
            }
        }
        #endregion



        #region insertInvoiceReturn
        /// <summary>
        /// Table - Invoice
        /// Inserted the values into Invoice Return Table . The Invoice ID reffered from Invoice table.
        /// </summary>
        /// <param name="childTestList"></param>
        [Route("api/Account/insertInvoiceReturn")]
        [HttpPost]
        public void insertInvoiceReturn(InvoiceReturn invoiceReturn)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DateTime today = DateTime.Today;
                    invoiceReturn.ReturnDate = today.ToShortDateString();
                    string strSQL = "INSERT INTO InvoiceReturn(RegID,InvoiceNo,ReturnAmount,ReturnDate,ReturnReason,ReturnStatus) VALUES(@val1,@val2,@val3,@val4,@val5,@val6)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", invoiceReturn.RegID);
                    cmd.Parameters.AddWithValue("@val2", invoiceReturn.InvoiceNo);
                    cmd.Parameters.AddWithValue("@val3", invoiceReturn.ReturnAmount);
                    cmd.Parameters.AddWithValue("@val4", invoiceReturn.ReturnDate);
                    cmd.Parameters.AddWithValue("@val5", invoiceReturn.ReturnReason);
                    cmd.Parameters.AddWithValue("@val6", invoiceReturn.ReturnStatus);


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion




        #region getAllInvoiceDetailsReturn
        /// <summary>
        /// Table - Invoice
        /// </summary>
        /// Listed all values from Invoice table
        /// <returns></returns>
        [Route("api/Account/getAllInvoiceDetailsReturn")]
        [HttpGet]
        public List<InvoiceReturn> getAllInvoiceDetailsReturn()
        {
            List<InvoiceReturn> InvoiceViewDetails = new List<InvoiceReturn>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.invoicereturn";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InvoiceReturn invoiceReturn = new InvoiceReturn();
                            invoiceReturn.Sno = i;
                            invoiceReturn.InvoiceReturnId = (int)dr["InvoiceReturnId"];
                            invoiceReturn.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoiceReturn.RegID = (int)dr["RegID"];
                            invoiceReturn.ReturnAmount = dr["ReturnAmount"].ToString();
                            invoiceReturn.ReturnDate = dr["ReturnDate"].ToString();
                            invoiceReturn.ReturnReason = dr["ReturnReason"].ToString();
                            invoiceReturn.ReturnStatus = dr["ReturnStatus"].ToString();
                            InvoiceViewDetails.Add(invoiceReturn);
                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceViewDetails;
            }
        }
        #endregion



        #region getLabOrderDetailsByLabId

        /// <summary>
        /// getLabOrderDetailsByLabId
        /// </summary>
        /// To get records from LabOrder table by LabID.
        ///  /// <param name=" labID"></param>
        /// <returns name="getAllOrderDetails"></returns>
        /// 
        [Route("api/Account/getLabOrderDetailsByLabId")]
        [HttpGet]

        public LabOrder getLabOrderDetailsByLabId(int labID)
        {
            LabOrder labOrderDetails = new LabOrder();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * from labOrder where LabId='" + labID + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            labOrderDetails.LabId = Convert.ToInt32(dr["LabId"]);
                            labOrderDetails.RegID = Convert.ToInt32(dr["RegID"]);
                            labOrderDetails.PatientName = dr["PatientName"].ToString();
                            labOrderDetails.MrdNo = dr["MrdNo"].ToString();
                            labOrderDetails.ReferredBy = dr["ReferredBy"].ToString();
                            labOrderDetails.ReferenceDiscountAmount = Convert.ToDouble(dr["ReferenceDiscountAmount"]);
                            labOrderDetails.Height = (float)(dr["Height"]);
                            labOrderDetails.Weight = (float)(dr["Weight"]);
                            labOrderDetails.PaymentTypeName = dr["PaymentTypeName"].ToString();
                            labOrderDetails.PaymentTypeCode = dr["PaymentTypeCode"].ToString();
                            labOrderDetails.InsuranceName = dr["InsuranceName"].ToString();
                            labOrderDetails.CollectAt = dr["CollectAt"].ToString();
                            labOrderDetails.HomeCollectChargeAmount = Convert.ToDouble(dr["HomeCollectChargeAmount"]);
                            labOrderDetails.IsPregnancy = Convert.ToInt16(dr["IsPregnancy"]);
                            labOrderDetails.LMP = dr["LMP"].ToString();
                            labOrderDetails.Trimester = dr["Trimester"].ToString();
                            labOrderDetails.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                            labOrderDetails.DepartmentName = dr["DepartmentName"].ToString();
                            labOrderDetails.Amount = Convert.ToDouble(dr["Amount"]);
                            labOrderDetails.CreateDate = Convert.ToDateTime(dr["CreateDate"]);




                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return labOrderDetails;
            }
        }

        #endregion

        #region insertInsurance
        /// <summary>
        /// Table - Insurance
        /// Inserted the Insurance table values.
        /// </summary>
        /// <param name="insurance"></param>
        [Route("api/Account/insertInsurance")]
        [HttpPost]
        public void insertInsurance(Insurance insurance)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO Insurance(RegID,InsuranceProviderName,ContactNo,Amount) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", insurance.RegID);
                    cmd.Parameters.AddWithValue("@val2", insurance.InsuranceProviderName);
                    cmd.Parameters.AddWithValue("@val3", insurance.ContactNo);
                    cmd.Parameters.AddWithValue("@val4", insurance.Amount);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion
        #region insertOutofHospital
        /// <summary>
        /// Table - insertOutofHospital
        /// Inserted the insertOutofHospital table values.
        /// </summary>
        /// <param name="outofHospital"></param>
        [Route("api/Account/insertOutofHospital")]
        [HttpPost]
        public void insertOutofHospital(OutofHospital outofHospital)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO OutofHospital(RegID,ProfileName,o_Ho_Name,Amount) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", outofHospital.RegID);
                    cmd.Parameters.AddWithValue("@val2", outofHospital.ProfileName);
                    cmd.Parameters.AddWithValue("@val3", outofHospital.o_Ho_Name);
                    cmd.Parameters.AddWithValue("@val4", outofHospital.Amount);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region getInsuranceDetailbyRefId
        /// <summary>
        /// Table - Insurance
        /// </summary>
        /// Listed all values from Insurance table by RegID
        /// <param value="regID"></param>
        /// <returns></returns>
        [Route("api/Account/getInsuranceDetailbyRefId")]
        [HttpGet]
        public List<Insurance> getInsuranceDetailbyRefId(int regId)
        {
            List<Insurance> lstInsuranceDetails = new List<Insurance>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Insurance where RegID ='" + regId + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Insurance insurance = new Insurance();

                            insurance.InsuranceId = (int)dr["InsuranceId"];
                            insurance.RegID = (int)dr["RegID"];
                            insurance.InsuranceProviderName = dr["InsuranceProviderName"].ToString();
                            insurance.ContactNo = dr["ContactNo"].ToString();
                            insurance.Amount = (double)dr["Amount"];
                            lstInsuranceDetails.Add(insurance);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstInsuranceDetails;
            }
        }
        #endregion

        #region getOutofHospitalDetailsbyRegId
        /// <summary>
        /// Table -  OutofHospital 
        /// </summary>
        /// Listed all values from OutofHospital table by RegID
        /// <param value="regID"></param>
        /// <returns></returns>
        [Route("api/Account/getOutofHospitalDetailsbyRegId")]
        [HttpGet]
        public List<OutofHospital> getOutofHospitalDetailsbyRegId(int regId)
        {
            List<OutofHospital> lstOutofHospitalDetails = new List<OutofHospital>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.OutofHospital where RegID ='" + regId + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            OutofHospital outofHospital = new OutofHospital();

                            outofHospital.OutofHospitalId = (int)dr["OutofHospitalId"];
                            outofHospital.RegID = (int)dr["RegID"];
                            outofHospital.ProfileName = dr["ProfileName"].ToString();
                            outofHospital.o_Ho_Name = dr["o_Ho_Name"].ToString();
                            outofHospital.Amount = (double)dr["Amount"];
                            lstOutofHospitalDetails.Add(outofHospital);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstOutofHospitalDetails;
            }
        }
        #endregion
        #endregion


        #region getAllChildTestList
        /// <summary>
        ///  Table - ChildTestList
        /// </summary>
        /// Listed all values from ChildTestList table
        /// <returns></returns>
        [Route("api/Account/getAllChildTestList")]
        [HttpGet]
        public List<ChildTestList> getAllChildTestList()
        {
            List<ChildTestList> lstChildTestDetails = new List<ChildTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.ChildTestList where ActiveStatus =1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ChildTestList childTestList = new ChildTestList();

                            childTestList.TestID = (int)dr["TestID"];
                            childTestList.ProfileID = (int)dr["ProfileID"];
                            childTestList.TestName = dr["TestName"].ToString();
                            childTestList.TestCode = dr["TestCode"].ToString();
                            childTestList.DepartmentName = dr["DepartmentName"].ToString();
                            childTestList.Methodology = dr["Methodology"].ToString();
                            childTestList.UnitMeasurementNumeric = dr["UnitMeasurementNumeric"].ToString();
                            childTestList.AgewiseReferenceRange = dr["AgewiseReferenceRange"].ToString();
                            childTestList.UnitMesurementFreeText = dr["UnitMesurementFreeText"].ToString();
                            childTestList.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            childTestList.DefaultValues = dr["DefaultValues"].ToString();
                            childTestList.GenderMale = dr["GenderMale"].ToString();
                            childTestList.GenderFemale = dr["GenderFemale"].ToString();
                            childTestList.Pregnancyrefrange = dr["Pregnancyrefrange"].ToString();
                            childTestList.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            childTestList.LowerCriticalValue = dr["LowerCriticalValue"].ToString();
                            childTestList.UpperCriticalValue = dr["UpperCriticalValue"].ToString();
                            childTestList.OtherCriticalReport = dr["OtherCriticalReport"].ToString();
                            childTestList.AgewiseCriticalValue = dr["AgewiseCriticalValue"].ToString();
                            childTestList.units = dr["units"].ToString();
                            childTestList.TurnAroundTime = dr["TurnAroundTime"].ToString();
                            childTestList.RequiredBiospyTestNumber = dr["RequiredBiospyTestNumber"].ToString();
                            childTestList.RequiredSamples = dr["RequiredSamples"].ToString();
                            childTestList.PatientPreparation = dr["PatientPreparation"].ToString();
                            childTestList.ExpectedResultDate = dr["ExpectedResultDate"].ToString();
                            childTestList.Amount = Convert.ToDouble(dr["Amount"]);
                            childTestList.Finaloutput = dr["Finaloutput"].ToString();
                            childTestList.TestbasedDiscount = dr["TestbasedDiscount"].ToString();
                            childTestList.Outsourced = dr["Outsourced"].ToString();
                            childTestList.CreateDate = dr["CreateDate"].ToString();
                            childTestList.cutOffTime = dr["CutoffTime"].ToString();
                            childTestList.ValidDate = dr["AmountValidDate"].ToString();
                            childTestList.DisplayName = dr["DisplayTestName"].ToString();
                            childTestList.TestInformation = dr["TestInformation"].ToString();
                            childTestList.TestSchedule = dr["TestSchedule"].ToString();
                            childTestList.NumericOrText = Convert.ToBoolean(dr["NumericOrText"]);
                            childTestList.commonParagraph = dr["commonParagraph"].ToString();
                            childTestList.UrineCulture = dr["UrineCulture"].ToString();
                            childTestList.AlternativeSample = dr["AlternativeSampleContainer"].ToString();
                            childTestList.CalculationPresent = dr["CalculationPresent"].ToString();
                            childTestList.Multiplecomponents = dr["multiplecomponents"].ToString();
                            childTestList.SampleContainer = dr["SampleContainer"].ToString();
                            lstChildTestDetails.Add(childTestList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstChildTestDetails;
            }
        }
        #endregion


        #region Payment

        #region insertPayment
        /// <summary>
        /// Table -  Payment
        /// Inserted the  Payment table values.
        /// </summary>
        /// <param name=" Payment"></param>
        [Route("api/Account/insertPayment")]
        [HttpPost]
        public void insertPayment(Payment Payment)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO Payment(RegID,PaymentDate,PaymentType,InvoiceNo,OrderNo,MrdNo,PaidAmount,NetAmount,PendingAmount,Status,PaidBy) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", Payment.RegID);
                    cmd.Parameters.AddWithValue("@val2", Payment.PaymentDate);
                    cmd.Parameters.AddWithValue("@val3", Payment.PaymentType);
                    cmd.Parameters.AddWithValue("@val4", Payment.InvoiceNo);
                    cmd.Parameters.AddWithValue("@val5", Payment.OrderNo);
                    cmd.Parameters.AddWithValue("@val6", Payment.MrdNo);
                    cmd.Parameters.AddWithValue("@val7", Payment.PaidAmount);
                    cmd.Parameters.AddWithValue("@val8", Payment.NetAmount);
                    cmd.Parameters.AddWithValue("@val9", Payment.PendingAmount);
                    cmd.Parameters.AddWithValue("@val10", Payment.Status);
                    cmd.Parameters.AddWithValue("@val11", Payment.PaidBy);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #endregion

        #region insertLabTestList


        /// <summary>
        /// Table -  LabTestList
        /// Inserted the  LabTestList table values.
        /// </summary>
        /// <param name=" LabTestList"></param>
        [Route("api/Account/insertLabTestList")]
        [HttpPost]
        public void insertLabTestList(LabTestList labTestList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //foreach (var labTestDetails in labTestList)
                    //{
                    string strSQL = "INSERT INTO LabTestList(RegID,TestCode,TestName,Amount,MrdNo,PatientName,IndividualStatus) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", labTestList.RegID);
                    cmd.Parameters.AddWithValue("@val2", labTestList.TestCode);
                    cmd.Parameters.AddWithValue("@val3", labTestList.TestName);
                    cmd.Parameters.AddWithValue("@val4", labTestList.Amount);
                    cmd.Parameters.AddWithValue("@val5", labTestList.MrdNo);
                    cmd.Parameters.AddWithValue("@val6", labTestList.PatientName);
                    cmd.Parameters.AddWithValue("@val7", labTestList.IndividualStatus);
                    cmd.ExecuteNonQuery();
                    //}
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getLabTestDatailsByRegIDandDate

        /// <summary>
        /// getPatientAllRegistrationInfo
        /// </summary>
        /// 
        ///  /// <param name=" "></param>
        /// <returns name="patientAllRegistrationInfo"></returns>
        /// 
        [Route("api/Account/getLabTestDatailsByRegIDandDate")]
        [HttpGet]
        public LabTechandTestDetails getLabTestDatailsByRegIDandDate(int regID, string orderDate)
        {
            LabTechandTestDetails labTechandTestDetails = new LabTechandTestDetails();

            labTechandTestDetails.LabOrderDetails = new LabOrder();
            labTechandTestDetails.ChildTestListDetails = new ChildTestList();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "Select c.TestID,c.ProfileID,c.TestName,c.UnitMeasurementNumeric,c.UnitMesurementFreeText,c.TableRequiredPrint,c.DefaultValues,c.GenderMale,c.GenderFemale,c.AdditionalFixedComments,c.LowerCriticalValue from pathoclinic.ChildTestList c inner Join pathoclinic.LabOrder l on c.TestID= l.TestID where l.RegID = '" + regID + "' and l.CreateDate = '" + orderDate + "' Where c.ActiveStatus=1";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            labTechandTestDetails.ChildTestListDetails.TestID = Convert.ToInt32(dr["TestID"]);
                            labTechandTestDetails.ChildTestListDetails.ProfileID = Convert.ToInt32(dr["ProfileID"]);
                            labTechandTestDetails.ChildTestListDetails.TestName = dr["TestName"].ToString();
                            labTechandTestDetails.ChildTestListDetails.UnitMeasurementNumeric = dr["UnitMeasurementNumeric"].ToString();
                            labTechandTestDetails.ChildTestListDetails.UnitMesurementFreeText = dr["UnitMesurementFreeText"].ToString();
                            labTechandTestDetails.ChildTestListDetails.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            labTechandTestDetails.ChildTestListDetails.DefaultValues = dr["DefaultValues"].ToString();
                            labTechandTestDetails.ChildTestListDetails.GenderMale = dr["GenderMale"].ToString();
                            labTechandTestDetails.ChildTestListDetails.GenderFemale = dr["GenderFemale"].ToString();
                            labTechandTestDetails.ChildTestListDetails.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            labTechandTestDetails.ChildTestListDetails.LowerCriticalValue = dr["LowerCriticalValue"].ToString();
                            labTechandTestDetails.ChildTestListDetails.UpperCriticalValue = dr["UpperCriticalValue"].ToString();
                            labTechandTestDetails.ChildTestListDetails.OtherCriticalReport = dr["OtherCriticalReport"].ToString();
                            labTechandTestDetails.ChildTestListDetails.AgewiseCriticalValue = dr["AgewiseCriticalValue"].ToString();
                            labTechandTestDetails.ChildTestListDetails.AgewiseReferenceRange = dr["AgewiseReferenceRange"].ToString();
                            labTechandTestDetails.ChildTestListDetails.units = dr["units"].ToString();
                            labTechandTestDetails.ChildTestListDetails.TurnAroundTime = dr["TurnAroundTime"].ToString();
                            labTechandTestDetails.ChildTestListDetails.RequiredBiospyTestNumber = dr["RequiredBiospyTestNumber"].ToString();
                            labTechandTestDetails.ChildTestListDetails.RequiredSamples = dr["TestName"].ToString();
                            labTechandTestDetails.ChildTestListDetails.PatientPreparation = dr["TestName"].ToString();
                            labTechandTestDetails.ChildTestListDetails.ExpectedResultDate = dr["TestName"].ToString();
                            labTechandTestDetails.ChildTestListDetails.Amount = Convert.ToDouble(dr["Amount"]);
                            labTechandTestDetails.ChildTestListDetails.Finaloutput = dr["Finaloutput"].ToString();
                            labTechandTestDetails.ChildTestListDetails.TestbasedDiscount = dr["RequiredBiospyTestNumber"].ToString();
                            labTechandTestDetails.ChildTestListDetails.Outsourced = dr["Outsourced"].ToString();
                            labTechandTestDetails.ChildTestListDetails.CreateDate = dr["CreateDate"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return labTechandTestDetails;
            }
        }

        #endregion


        #region insertLabProfileList
        /// <summary>
        /// Table -  LabTestList
        /// Inserted the LabProfileList table values.
        /// </summary>
        /// <param name="LabProfileList"></param>
        [Route("api/Account/insertLabProfileList")]
        [HttpPost]
        public void insertLabProfileList(LabProfileList labProfileList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //foreach (var labprofileDetails in labProfileList)
                    //{
                    string strSQL = "INSERT INTO LabProfileList(RegID,ProfileCode,ProfileName,Amount,MrdNo,ProfileID,PatientName,IndividualStatus) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", labProfileList.RegID);
                    cmd.Parameters.AddWithValue("@val2", labProfileList.ProfileCode);
                    cmd.Parameters.AddWithValue("@val3", labProfileList.ProfileName);
                    cmd.Parameters.AddWithValue("@val4", labProfileList.Amount);
                    cmd.Parameters.AddWithValue("@val5", labProfileList.MrdNo);
                    cmd.Parameters.AddWithValue("@val6", labProfileList.ProfileID);
                    cmd.Parameters.AddWithValue("@val7", labProfileList.PatientName);
                    cmd.Parameters.AddWithValue("@val8", labProfileList.IndividualStatus);
                    cmd.ExecuteNonQuery();
                    //}
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getProfileListByMrdNoLab
        /// <summary>
        /// Table - LabProfileList
        /// </summary>
        /// Listed all values from LabProfileList table
        /// <returns></returns>
        [Route("api/Account/getProfileListByMrdNoLab")]
        [HttpGet]
        public List<LabProfileList> getProfileListByMrdNoLab(string mrdNo)
        {
            List<LabProfileList> lisLabProfileDetails = new List<LabProfileList>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    //string strSQL = " SELECT DISTINCT SampleContainer, labprofilelist.ProfileID,labprofilelist.ProfileName,labprofilelist.ProfileCode from childtestlist inner join labprofilelist on labprofilelist.ProfileID = childtestlist.ProfileID where labprofilelist.MrdNo = '" + mrdNo + "'AND ActiveStatus=1";


                    string strSQL = " SELECT * from labprofilelist  where  MrdNo = '" + mrdNo + "'";


                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabProfileList labProfileList = new LabProfileList();
                            labProfileList.Sno = i;
                            labProfileList.ProfileID = (int)dr["ProfileID"];
                            labProfileList.ProfileName = dr["ProfileName"].ToString();
                            labProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            labProfileList.IndividualStatus = (int)dr["IndividualStatus"];
                            labProfileList.MrdNo = mrdNo;
                            lisLabProfileDetails.Add(labProfileList);

                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lisLabProfileDetails;
            }
        }
        #endregion


        #region getProfileListByMrdNo
        /// <summary>
        /// Table - LabProfileList
        /// </summary>
        /// Listed all values from LabProfileList table
        /// <returns></returns>
        [Route("api/Account/getProfileListByMrdNo")]
        [HttpGet]
        public List<LabProfileList> getProfileListByMrdNo(string mrdNo)
        {
            List<LabProfileList> lisLabProfileDetails = new List<LabProfileList>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.LabProfileList where mrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabProfileList labProfileList = new LabProfileList();
                            labProfileList.Sno = i;
                            labProfileList.LabProfileListID = (int)dr["LabProfileListID"];

                            labProfileList.RegID = (int)dr["RegID"];
                            labProfileList.ProfileName = dr["ProfileName"].ToString();
                            labProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            labProfileList.Amount = Convert.ToDouble((dr["Amount"]));
                            labProfileList.PatientName = dr["PatientName"].ToString();


                            lisLabProfileDetails.Add(labProfileList);

                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lisLabProfileDetails;
            }
        }
        #endregion


        #region getTestListByMrdNo
        /// <summary>
        /// Table - LabTestList
        /// </summary>
        /// Listed all values from LabTestList table
        /// <returns></returns>
        [Route("api/Account/getTestListByMrdNo")]
        [HttpGet]
        public List<LabTestList> getTestListByMrdNo(string mrdNo)
        {
            List<LabTestList> lisLabTestDetails = new List<LabTestList>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.LabTestList where mrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabTestList labTestList = new LabTestList();
                            labTestList.Sno = i;
                            labTestList.LabTestListId = (int)dr["LabTestListId"];

                            labTestList.RegID = (int)dr["RegID"];
                            labTestList.TestName = dr["TestName"].ToString();
                            labTestList.TestCode = dr["TestCode"].ToString();
                            labTestList.Amount = Convert.ToDouble(dr["Amount"]);
                            labTestList.PatientName = dr["PatientName"].ToString();
                            labTestList.MrdNo = dr["MrdNo"].ToString();

                            lisLabTestDetails.Add(labTestList);

                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lisLabTestDetails;
            }
        }
        #endregion

        #region getTestByTestCode
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// Listed all values from childtestlist table
        /// <returns></returns>
        [Route("api/Account/getTestByTestCode")]
        [HttpGet]
        public ChildTestList getTestByTestCode(string testCode)
        {
            ChildTestList childTestList = new ChildTestList();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.ChildTestList where TestCode='" + testCode + "' AND ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            childTestList.Sno = i;
                            childTestList.TestID = Convert.ToInt32(dr["TestID"]);
                            childTestList.TestID = Convert.ToInt32(dr["TestID"]);
                            childTestList.ProfilePriority = Convert.ToInt32(dr["ProfilePriority"]);
                            childTestList.ProfileID = Convert.ToInt32(dr["ProfileID"]);
                            childTestList.TestName = dr["TestName"].ToString();
                            childTestList.TestCode = dr["TestCode"].ToString();
                            childTestList.Methodology = dr["Methodology"].ToString();
                            childTestList.UnitMeasurementNumeric = dr["UnitMeasurementNumeric"].ToString();
                            childTestList.UnitMesurementFreeText = dr["UnitMesurementFreeText"].ToString();
                            childTestList.SampleContainer = dr["SampleContainer"].ToString();
                            childTestList.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            childTestList.DefaultValues = dr["DefaultValues"].ToString();
                            childTestList.GenderMale = dr["GenderMale"].ToString();
                            childTestList.GenderFemale = dr["GenderFemale"].ToString();
                            childTestList.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            childTestList.LowerCriticalValue = dr["LowerCriticalValue"].ToString();
                            childTestList.UpperCriticalValue = dr["UpperCriticalValue"].ToString();
                            childTestList.OtherCriticalReport = dr["OtherCriticalReport"].ToString();
                            childTestList.AgewiseCriticalValue = dr["AgewiseCriticalValue"].ToString();
                            childTestList.AgewiseReferenceRange = dr["AgewiseReferenceRange"].ToString();
                            childTestList.Pregnancyrefrange = dr["Pregnancyrefrange"].ToString();
                            childTestList.units = dr["units"].ToString();
                            childTestList.TurnAroundTime = dr["TurnAroundTime"].ToString();
                            childTestList.RequiredBiospyTestNumber = dr["RequiredBiospyTestNumber"].ToString();
                            childTestList.RequiredSamples = dr["RequiredSamples"].ToString();
                            childTestList.PatientPreparation = dr["PatientPreparation"].ToString();
                            childTestList.ExpectedResultDate = dr["ExpectedResultDate"].ToString();
                            childTestList.Amount = Convert.ToDouble(dr["Amount"]);
                            childTestList.Finaloutput = dr["Finaloutput"].ToString();
                            childTestList.TestbasedDiscount = dr["TestbasedDiscount"].ToString();
                            childTestList.Outsourced = dr["Outsourced"].ToString();
                            childTestList.CreateDate = dr["CreateDate"].ToString();
                            childTestList.commonParagraph = dr["commonParagraph"].ToString();
                            childTestList.Multiplecomponents = dr["Multiplecomponents"].ToString();
                            childTestList.UrineCulture = dr["UrineCulture"].ToString();
                            childTestList.CalculationPresent = dr["CalculationPresent"].ToString();
                            childTestList.cutOffTime = dr["CutoffTime"].ToString();
                            childTestList.ValidDate = dr["AmountValidDate"].ToString();
                            childTestList.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return childTestList;
            }
        }
        #endregion



        #region getTestDetailsByProfileID
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// Listed all values from childtestlist table
        /// <returns></returns>
        [Route("api/Account/getTestDetailsByProfileID")]
        [HttpGet]
        public List<ChildTestList> getTestDetailsByProfileID(string profileID)
        {
            List<ChildTestList> lstchildTest = new List<ChildTestList>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.ChildTestList where ProfileID='" + profileID + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            ChildTestList childTestDetails = new ChildTestList();
                            childTestDetails.Sno = i;



                            childTestDetails.TestID = Convert.ToInt32(dr["TestID"]);
                            childTestDetails.ProfileID = Convert.ToInt32(dr["ProfileID"]);
                            childTestDetails.TestName = dr["TestName"].ToString();
                            childTestDetails.TestCode = dr["TestCode"].ToString();
                            childTestDetails.Methodology = dr["Methodology"].ToString();
                            childTestDetails.UnitMeasurementNumeric = dr["UnitMeasurementNumeric"].ToString();
                            childTestDetails.UnitMesurementFreeText = dr["UnitMesurementFreeText"].ToString();
                            childTestDetails.SampleContainer = dr["SampleContainer"].ToString();
                            childTestDetails.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            childTestDetails.DefaultValues = dr["DefaultValues"].ToString();
                            childTestDetails.GenderMale = dr["GenderMale"].ToString();
                            childTestDetails.GenderFemale = dr["GenderFemale"].ToString();
                            childTestDetails.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            childTestDetails.LowerCriticalValue = dr["LowerCriticalValue"].ToString();
                            childTestDetails.UpperCriticalValue = dr["UpperCriticalValue"].ToString();
                            childTestDetails.OtherCriticalReport = dr["OtherCriticalReport"].ToString();
                            childTestDetails.AgewiseCriticalValue = dr["AgewiseCriticalValue"].ToString();
                            childTestDetails.AgewiseReferenceRange = dr["AgewiseReferenceRange"].ToString();
                            childTestDetails.units = dr["units"].ToString();
                            childTestDetails.TurnAroundTime = dr["TurnAroundTime"].ToString();
                            childTestDetails.RequiredBiospyTestNumber = dr["RequiredBiospyTestNumber"].ToString();
                            childTestDetails.RequiredSamples = dr["RequiredSamples"].ToString();
                            childTestDetails.PatientPreparation = dr["PatientPreparation"].ToString();
                            childTestDetails.ExpectedResultDate = dr["ExpectedResultDate"].ToString();
                            childTestDetails.Amount = Convert.ToDouble(dr["Amount"]);
                            childTestDetails.Finaloutput = dr["Finaloutput"].ToString();
                            childTestDetails.TestbasedDiscount = dr["TestbasedDiscount"].ToString();
                            childTestDetails.Outsourced = dr["Outsourced"].ToString();
                            childTestDetails.CreateDate = dr["CreateDate"].ToString();


                            i++;

                            lstchildTest.Add(childTestDetails);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstchildTest;
            }
        }
        #endregion



        #region getProfileByProfileCode
        /// <summary>
        /// Table - Masterprofilelist
        /// </summary>
        /// Listed all values from Masterprofilelist table
        /// <returns></returns>
        [Route("api/Account/getProfileByProfileCode")]
        [HttpGet]
        public MasterProfileList getProfileByProfileCode(string profileCode)
        {
            MasterProfileList masterProfileList = new MasterProfileList();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.MasterProfileList where ProfileCode='" + profileCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            masterProfileList.Sno = i;
                            masterProfileList.ProfileID = (int)dr["ProfileID"];
                            masterProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            masterProfileList.ProfileName = dr["ProfileName"].ToString();
                            masterProfileList.Amount = Convert.ToDouble((dr["Amount"]));

                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return masterProfileList;
            }
        }
        #endregion






        #region getAllInsuranceDetail
        /// <summary>
        /// Table - Insurance
        /// </summary>
        /// Listed all values from Insurance table      
        /// <returns></returns>
        [Route("api/Account/getAllInsuranceDetail")]
        [HttpGet]
        public List<Insurance> getAllInsuranceDetail()
        {
            List<Insurance> lstInsuranceDetails = new List<Insurance>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Insurance";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Insurance insurance = new Insurance();

                            insurance.InsuranceId = (int)dr["InsuranceId"];
                            insurance.RegID = (int)dr["RegID"];
                            insurance.InsuranceProviderName = dr["InsuranceProviderName"].ToString();
                            insurance.ContactNo = dr["ContactNo"].ToString();
                            insurance.Amount = (double)dr["Amount"];
                            lstInsuranceDetails.Add(insurance);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstInsuranceDetails;
            }
        }
        #endregion


        #region insertLaborderStatus


        /// <summary>
        /// Table -  LaborderStatus
        /// Inserted the  LaborderStatus table values.
        /// </summary>
        /// <param name=" laborderStatus"></param>
        [Route("api/Account/insertLaborderStatus")]
        [HttpPost]
        public void insertLaborderStatus(LaborderStatus laborderStatus)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    laborderStatus.LabOrderDate = DateTime.Now;
                    string strSQL = "INSERT INTO laborderstatus(RegID,MrdNo,LabStatus,LabOrderDate,ApproveStatus,DenyStatus,SampleStatus) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", laborderStatus.RegID);
                    cmd.Parameters.AddWithValue("@val2", laborderStatus.MrdNo);
                    cmd.Parameters.AddWithValue("@val3", laborderStatus.LabStatus);
                    cmd.Parameters.AddWithValue("@val4", laborderStatus.LabOrderDate);
                    cmd.Parameters.AddWithValue("@val5", laborderStatus.ApproveStatus);
                    cmd.Parameters.AddWithValue("@val6", laborderStatus.DenyStatus);
                    cmd.Parameters.AddWithValue("@val7", laborderStatus.SampleStatus);

                    cmd.ExecuteNonQuery();

                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        //LabTech login Grd

        #region getTodayLaborderforSampleCollect
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getTodayLaborderforSampleCollect")]
        [HttpGet]
        public List<LabOrderInprogressList> getTodayLaborderforSampleCollect()
        {
            List<LabOrderInprogressList> lstLabInprogressDetails = new List<LabOrderInprogressList>();



            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //  string strSQL = "SELECT lo.PatientName, ls.MrdNo, ls.LabStatus FROM pathoclinic.laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo where ls.LabStatus = 'Inprogress'";

                    string strSQL = "SELECT ls.LabOrderDate, pr.RegID,pr.FirstName,pr.LastName,ls.MrdNo, ls.LabStatus, pr.Age, pr.Sex FROM((laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo) INNER JOIN patientregistration pr ON lo.RegID = pr.RegID) where ls.LabStatus = '1' ORDER BY ls.LabOrderDate ASC";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrderInprogressList labOrderView = new LabOrderInprogressList();



                            labOrderView.FirstName = dr["FirstName"].ToString();
                            labOrderView.LastName = dr["LastName"].ToString();
                            labOrderView.LabStatus = dr["LabStatus"].ToString();
                            labOrderView.MrdNo = dr["MrdNo"].ToString();
                            labOrderView.RegID = (int)dr["RegID"];
                            labOrderView.sex = dr["Sex"].ToString();
                            labOrderView.Age = (int)dr["Age"];
                            //var d = Convert.ToDateTime(dr["CreateDate"]);
                            //var getdate = d.ToShortDateString();
                            //var gettime = d.ToShortTimeString();
                            labOrderView.CreateDate = dr["LabOrderDate"].ToString();

                            lstLabInprogressDetails.Add(labOrderView);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabInprogressDetails;
            }
        }
        #endregion



        #region getTodayLaborderStatus
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getTodayLaborderStatus")]
        [HttpGet]
        public List<LabOrderInprogressList> getTodayLaborderStatus(string labstatus)
        {
            List<LabOrderInprogressList> lstLabInprogressDetails = new List<LabOrderInprogressList>();



            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //  string strSQL = "SELECT lo.PatientName, ls.MrdNo, ls.LabStatus FROM pathoclinic.laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo where ls.LabStatus = 'Inprogress'";

                    string strSQL = "SELECT ls.LabOrderDate, pr.RegID,pr.FirstName,pr.LastName,ls.MrdNo, ls.LabStatus, pr.Age, pr.Sex FROM((laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo) INNER JOIN patientregistration pr ON lo.RegID = pr.RegID) where ls.LabStatus = '" + labstatus + "' ORDER BY ls.LabOrderDate ASC";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrderInprogressList labOrderView = new LabOrderInprogressList();



                            labOrderView.FirstName = dr["FirstName"].ToString();
                            labOrderView.LastName = dr["LastName"].ToString();
                            labOrderView.LabStatus = dr["LabStatus"].ToString();
                            labOrderView.MrdNo = dr["MrdNo"].ToString();
                            labOrderView.RegID = (int)dr["RegID"];
                            labOrderView.sex = dr["Sex"].ToString();
                            labOrderView.Age = (int)dr["Age"];
                            //var d = Convert.ToDateTime(dr["CreateDate"]);
                            //var getdate = d.ToShortDateString();
                            //var gettime = d.ToShortTimeString();
                            labOrderView.CreateDate = dr["LabOrderDate"].ToString();

                            lstLabInprogressDetails.Add(labOrderView);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabInprogressDetails;
            }
        }
        #endregion



        #region getTodayLaborderStatusLocation
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getTodayLaborderStatusLocation")]
        [HttpGet]
        public List<LabOrderInprogressList> getTodayLaborderStatusLocation(string labstatus, string locationCode)
        {
            List<LabOrderInprogressList> lstLabInprogressDetails = new List<LabOrderInprogressList>();



            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //  string strSQL = "SELECT lo.PatientName, ls.MrdNo, ls.LabStatus FROM pathoclinic.laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo where ls.LabStatus = 'Inprogress'";

                    string strSQL = "SELECT ls.LabOrderDate, pr.RegID,pr.FirstName,pr.LastName,ls.MrdNo, ls.LabStatus,lo.LocationName, pr.Age, pr.Sex FROM((laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo) INNER JOIN patientregistration pr ON lo.RegID = pr.RegID) where ls.LabStatus = '" + labstatus + "' and lo.LocationCode='" + locationCode + "' ORDER BY ls.LabOrderDate ASC";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrderInprogressList labOrderView = new LabOrderInprogressList();


                            labOrderView.PatientName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                            labOrderView.FirstName = dr["FirstName"].ToString();
                            labOrderView.LastName = dr["LastName"].ToString();
                            labOrderView.LabStatus = dr["LabStatus"].ToString();
                            labOrderView.MrdNo = dr["MrdNo"].ToString();
                            labOrderView.RegID = (int)dr["RegID"];
                            labOrderView.sex = dr["Sex"].ToString();
                            labOrderView.Age = (int)dr["Age"];
                            //var d = Convert.ToDateTime(dr["CreateDate"]);
                            //var getdate = d.ToShortDateString();
                            //var gettime = d.ToShortTimeString();
                            labOrderView.CreateDate = dr["LabOrderDate"].ToString();
                            labOrderView.LocationName = dr["LocationName"].ToString();

                            lstLabInprogressDetails.Add(labOrderView);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabInprogressDetails;
            }
        }
        #endregion




        #region getTodayLaborderforLabView
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getTodayLaborderforLabView")]
        [HttpGet]
        public List<LabOrderInprogressList> getTodayLaborderforLabView()
        {
            List<LabOrderInprogressList> lstLabInprogressDetails = new List<LabOrderInprogressList>();



            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT lo.CreateDate,pr.RegID,pr.FirstName,pr.LastName,ls.MrdNo, ls.LabStatus, pr.Age, pr.DOB, pr.Sex FROM((laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo) INNER JOIN patientregistration pr ON lo.RegID = pr.RegID) where ls.LabStatus = '2' ORDER BY lo.CreateDate ASC";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrderInprogressList labOrderView = new LabOrderInprogressList();
                            labOrderView.FirstName = dr["FirstName"].ToString();
                            labOrderView.LastName = dr["LastName"].ToString();
                            labOrderView.LabStatus = dr["LabStatus"].ToString();
                            labOrderView.MrdNo = dr["MrdNo"].ToString();
                            labOrderView.RegID = (int)dr["RegID"];
                            labOrderView.sex = dr["Sex"].ToString();
                            labOrderView.Age = (int)dr["Age"];

                            //     var d = Convert.ToDateTime(dr["CreateDate"]);
                            //var getdate = d.ToShortDateString();
                            //var gettime = d.ToShortTimeString();
                            //labOrderView.CreateDate = getdate;
                            labOrderView.CreateDate = dr["CreateDate"].ToString();
                            lstLabInprogressDetails.Add(labOrderView);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabInprogressDetails;
            }
        }
        #endregion


        #region getTodayLaborderforLabViewDrop
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getTodayLaborderforLabViewDrop")]
        [HttpGet]
        public List<LabOrderInprogressList> getTodayLaborderforLabViewDrop()
        {
            List<LabOrderInprogressList> lstLabInprogressDetails = new List<LabOrderInprogressList>();



            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT lo.CreateDate,pr.RegID,pr.FirstName,pr.LastName,ls.MrdNo, ls.LabStatus, pr.Age, pr.DOB, pr.Sex FROM((laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo) INNER JOIN patientregistration pr ON lo.RegID = pr.RegID) where ls.LabStatus = '1' and ls.SampleStatus = '1' ORDER BY lo.CreateDate ASC";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrderInprogressList labOrderView = new LabOrderInprogressList();
                            labOrderView.FirstName = dr["FirstName"].ToString();
                            labOrderView.LastName = dr["LastName"].ToString();
                            labOrderView.LabStatus = dr["LabStatus"].ToString();
                            labOrderView.MrdNo = dr["MrdNo"].ToString();
                            labOrderView.RegID = (int)dr["RegID"];
                            labOrderView.sex = dr["Sex"].ToString();
                            labOrderView.Age = (int)dr["Age"];

                            //     var d = Convert.ToDateTime(dr["CreateDate"]);
                            //var getdate = d.ToShortDateString();
                            //var gettime = d.ToShortTimeString();
                            //labOrderView.CreateDate = getdate;
                            labOrderView.CreateDate = dr["CreateDate"].ToString();
                            lstLabInprogressDetails.Add(labOrderView);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabInprogressDetails;
            }
        }
        #endregion



        #region getAllpaymentDetailsByToday
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllpaymentDetailsByToday")]
        [HttpGet]
        public List<Payment> getAllpaymentDetailsByToday()
        {
            List<Payment> PaymentDetails = new List<Payment>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //DateTime today = new DateTime();
                    string today = DateTime.Now.ToString("MM/dd/yyyy");
                    string strSQL = "SELECT * FROM pathoclinic.Payment Where PaymentDate = '" + today + "' && PaymentType = 'Cash'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Payment payment = new Payment();


                            payment.PaymentId = (int)dr["PaymentId"];
                            payment.PaidAmount = Convert.ToDouble(dr["PaidAmount"]);
                            payment.PendingAmount = Convert.ToDouble(dr["PendingAmount"]);
                            payment.NetAmount = Convert.ToDouble(dr["NetAmount"]);
                            payment.PaymentDate = dr["PaymentDate"].ToString();
                            payment.PaymentType = dr["PaymentType"].ToString();
                            payment.InvoiceNo = dr["InvoiceNo"].ToString();
                            payment.RegID = (int)dr["RegID"];

                            PaymentDetails.Add(payment);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return PaymentDetails;
            }
        }
        #endregion

        #region getAllpaymentDetailsByPaymentMode
        /// <summary>
        /// Get details from payment table passing payment mode parameter like card, cash,credit
        /// </summary>
        /// <param name="paymentMode"></param>
        /// <returns></returns>
        [Route("api/Account/getAllpaymentDetailsByPaymentMode")]
        [HttpGet]
        public List<Invoice> getAllpaymentDetailsByPaymentMode(string paymentMode)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Invoice  where PaymentType LIKE'%" + paymentMode + "' AND Action ='SELF'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            invoice.NetAmount = dr["NetAmount"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PaymentType = dr["PaymentType"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }

        #endregion


        #region getpaymentDetailsByAllPaymentMode
        /// <summary>
        /// Get details from payment table passing payment mode parameter like card, cash,credit
        /// </summary>
        /// <param name="paymentMode"></param>
        /// <returns></returns>
        [Route("api/Account/getpaymentDetailsByAllPaymentMode")]
        [HttpGet]
        public List<Invoice> getpaymentDetailsByAllPaymentMode()
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action ='SELF'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.NetAmount = dr["NetAmount"].ToString();
                            invoice.PaymentType = dr["PaymentType"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }

        #endregion
        #region InsertGrouptRegistrationDetails
        /// <summary>
        /// Table - Registration
        /// </summary>
        /// <param name="Registration"></param>
        [Route("api/Account/InsertGrouptRegistrationDetails")]
        [HttpPost]
        public void InsertGrouptRegistrationDetails(GroupRegistration groupregistration)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DateTime today = DateTime.Today;

                    string strSQL = "INSERT INTO grouppatientregistration(Title,FirstName,MiddleName,LastName,Guardian,Relation,PatientType,Sex,MaritalStatus,DOB,Age,DateOfReg,ProfilePicture,Amount,NoOfPerson,GroupName) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val14,@val15,@val16,@val17)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", groupregistration.Title);
                    cmd.Parameters.AddWithValue("@val2", groupregistration.FirstName);
                    cmd.Parameters.AddWithValue("@val3", groupregistration.MiddleName);
                    cmd.Parameters.AddWithValue("@val4", groupregistration.LastName);
                    cmd.Parameters.AddWithValue("@val5", groupregistration.Guardian);
                    cmd.Parameters.AddWithValue("@val6", groupregistration.Relation);
                    cmd.Parameters.AddWithValue("@val7", groupregistration.PatientType);
                    cmd.Parameters.AddWithValue("@val8", groupregistration.Sex);
                    cmd.Parameters.AddWithValue("@val9", groupregistration.MaritalStatus);
                    cmd.Parameters.AddWithValue("@val10", groupregistration.DOB);
                    cmd.Parameters.AddWithValue("@val11", groupregistration.Age);
                    cmd.Parameters.AddWithValue("@val12", groupregistration.DateOfReg);
                    //cmd.Parameters.AddWithValue("@val13", registration.PhoneNumber);
                    cmd.Parameters.AddWithValue("@val14", groupregistration.ProfilePicture);

                    cmd.Parameters.AddWithValue("@val15", groupregistration.Amount);
                    cmd.Parameters.AddWithValue("@val16", groupregistration.NoOfPerson);
                    cmd.Parameters.AddWithValue("@val17", groupregistration.GroupName);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getAllpaymentDetailsByInvoiceNo
        /// <summary>
        /// Get details from payment table passing payment mode parameter like card, cash,credit
        /// </summary>
        /// <param name="paymentMode"></param>
        /// <returns></returns>
        [Route("api/Account/getAllpaymentDetailsByInvoiceNo")]
        [HttpGet]
        public List<Payment> getAllpaymentDetailsByInvoiceNo(string invoiceNo)
        {
            List<Payment> PaymentDetails = new List<Payment>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string today = DateTime.Now.ToString("MM/dd/yyyy");
                    string strSQL = "SELECT * FROM pathoclinic.Payment Where  InvoiceNo = '" + invoiceNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Payment payment = new Payment();


                            payment.PaymentId = (int)dr["PaymentId"];
                            payment.PaidAmount = Convert.ToDouble(dr["PaidAmount"]);
                            payment.PendingAmount = Convert.ToDouble(dr["PendingAmount"]);
                            payment.NetAmount = Convert.ToDouble(dr["NetAmount"]);
                            payment.PaymentDate = dr["PaymentDate"].ToString();
                            payment.PaymentType = dr["PaymentType"].ToString();
                            payment.InvoiceNo = dr["InvoiceNo"].ToString();
                            payment.RegID = (int)dr["RegID"];

                            PaymentDetails.Add(payment);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return PaymentDetails;
            }
        }

        #endregion
        #region getAllProfiles
        /// <summary>
        /// Table - masterprofilelist
        /// </summary>
        /// Listed all values from masterprofilelist table      
        /// <returns></returns>
        [Route("api/Account/getAllProfiles")]
        [HttpGet]
        public List<MasterProfileList> getAllProfiles()
        {
            List<MasterProfileList> lstProfiles = new List<MasterProfileList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.masterprofilelist";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            MasterProfileList masterProfileList = new MasterProfileList();

                            masterProfileList.ProfileID = (int)dr["ProfileID"];
                            masterProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            masterProfileList.ProfileName = dr["ProfileName"].ToString();

                            lstProfiles.Add(masterProfileList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstProfiles;
            }
        }
        #endregion


        #region getTotalPendingAmount
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>

        [Route("api/Account/getTotalPendingAmount")]
        [HttpGet]
        public int getTotalPendingAmount()
        {
            int pendingamt = 0;
            string today = DateTime.Now.ToString("MM/dd/yyyy");
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString);
            string strSQL = "SELECT SUM(PendingAmount) FROM pathoclinic.payment Where PaymentDate = '" + today + "'  && PaymentType ='Cash'";
            conn.Open();
            MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
            DataSet ds = new DataSet();
            mydata.Fill(ds);
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0] != DBNull.Value)
                {
                    pendingamt = Convert.ToInt32(dr[0]);
                }
                else
                {
                    pendingamt = 0;
                }
            }
            conn.Close();
            return pendingamt;
        }


        #endregion

        #region getTotalpaidAmount
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>

        [Route("api/Account/getTotalpaidAmount")]
        [HttpGet]
        public int getTotalpaidAmount()
        {
            int paidamt = 0;
            string today = DateTime.Now.ToString("MM/dd/yyyy");
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString);
            string strSQL = "SELECT SUM(PaidAmount) FROM pathoclinic.payment  Where PaymentDate = '" + today + "'  && PaymentType ='Cash'";
            conn.Open();
            MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
            DataSet ds = new DataSet();
            mydata.Fill(ds);
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0] != DBNull.Value)
                {
                    paidamt = Convert.ToInt32(dr[0]);
                }
                else
                {
                    paidamt = 0;
                }
            }
            conn.Close();
            return paidamt;
        }


        #endregion

        #region  Group
        #region insertGroupdetails
        /// <summary>
        ///
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/insertGroupdetails")]
        [HttpPost]
        public void insertGroupdetails(Group group)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //DateTime today = DateTime.Today;
                    //group.GroupCreatDate = today.ToShortDateString();
                    string strSQL = "INSERT INTO pathoclinic.group(GroupOrginID,NoOfPerson,GroupCreatDate,Amount,GroupName,HospitalName) VALUES(@val2,@val3,@val4,@val5,@val6,@val7)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    //       cmd.Parameters.AddWithValue("@val1", group.GroupID);
                    cmd.Parameters.AddWithValue("@val2", group.GroupOrginID);
                    cmd.Parameters.AddWithValue("@val3", group.NoOfPerson);
                    cmd.Parameters.AddWithValue("@val4", group.GroupCreatDate);
                    cmd.Parameters.AddWithValue("@val5", group.Amount);
                    cmd.Parameters.AddWithValue("@val6", group.GroupName);
                    cmd.Parameters.AddWithValue("@val7", group.HospitalName);


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region insertGroupTestList


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/insertGroupTestList")]
        [HttpPost]
        public void insertGroupTestList(GroupTestList GroupTestList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //foreach (var labTestDetails in labTestList)
                    //{
                    string strSQL = "INSERT INTO GroupTestList(TestCode,TestName,Amount,MrdNo,GroupName) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    //cmd.Parameters.AddWithValue("@val1", GroupTestList.GroupTestListId);
                    cmd.Parameters.AddWithValue("@val1", GroupTestList.TestCode);
                    cmd.Parameters.AddWithValue("@val2", GroupTestList.TestName);
                    cmd.Parameters.AddWithValue("@val3", GroupTestList.Amount);
                    cmd.Parameters.AddWithValue("@val4", GroupTestList.MrdNo);
                    cmd.Parameters.AddWithValue("@val5", GroupTestList.GroupName);
                    cmd.ExecuteNonQuery();
                    //}
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region insertGroupProfileList
        /// <summary>
        ///
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/insertGroupProfileList")]
        [HttpPost]
        public void insertGroupProfileList(GroupProfileList groupprofile)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //foreach (var labprofileDetails in labProfileList)
                    //{
                    string strSQL = "INSERT INTO GroupProfileList(ProfileCode,ProfileName,Amount,MrdNo,GroupName) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    //cmd.Parameters.AddWithValue("@val1", groupprofile.GroupProfileListID);
                    cmd.Parameters.AddWithValue("@val1", groupprofile.ProfileCode);
                    cmd.Parameters.AddWithValue("@val2", groupprofile.ProfileName);
                    cmd.Parameters.AddWithValue("@val3", groupprofile.Amount);
                    cmd.Parameters.AddWithValue("@val4", groupprofile.MrdNo);
                    cmd.Parameters.AddWithValue("@val5", groupprofile.GroupName);

                    cmd.ExecuteNonQuery();
                    //}
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion
        #endregion



        #region  addgroup related 


        #region getGroupDetails
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getGroupDetails")]
        [HttpGet]
        public List<Group> getGroupDetails()
        {
            List<Group> groupDetailsList = new List<Group>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.group";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Group group = new Group();
                            group.GroupOrginID = dr["GroupOrginID"].ToString();
                            group.GroupName = dr["GroupName"].ToString();
                            group.NoOfPerson = dr["NoOfPerson"].ToString();
                            group.GroupCreatDate = dr["GroupCreatDate"].ToString();
                            group.Amount = Convert.ToDouble(dr["Amount"]);
                            groupDetailsList.Add(group);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return groupDetailsList;
            }
        }
        #endregion

        #region getGroupProfileDetailsByMrdNo
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getGroupProfileDetailsByMrdNo")]
        [HttpGet]
        public List<GroupProfileList> getGroupProfileDetailsByMrdNo(string groupmrdno)
        {
            List<GroupProfileList> groupProfileList = new List<GroupProfileList>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.groupprofilelist where MrdNo='" + groupmrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            GroupProfileList groupProfile = new GroupProfileList();
                            groupProfile.Sno = i;
                            groupProfile.GroupProfileListID = (int)dr["GroupProfileListID"];
                            groupProfile.ProfileCode = dr["ProfileCode"].ToString();
                            groupProfile.ProfileName = dr["ProfileName"].ToString();
                            groupProfile.Amount = Convert.ToDouble(dr["Amount"]);
                            groupProfileList.Add(groupProfile);
                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return groupProfileList;
            }
        }
        #endregion


        #region getGroupTestDetailsByMrdNo
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getGroupTestDetailsByMrdNo")]
        [HttpGet]
        public List<GroupTestList> getGroupTestDetailsByMrdNo(string groupmrdno)
        {
            List<GroupTestList> groupTestList = new List<GroupTestList>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.grouptestlist where MrdNo='" + groupmrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            GroupTestList groupTest = new GroupTestList();
                            groupTest.Sno = i;
                            groupTest.GroupTestListId = (int)dr["GroupTestListId"];
                            groupTest.TestCode = dr["TestCode"].ToString();
                            groupTest.TestName = dr["TestName"].ToString();
                            groupTest.Amount = Convert.ToDouble(dr["Amount"]);

                            i++;
                            groupTestList.Add(groupTest);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return groupTestList;
            }
        }
        #endregion

        #region getGroupDetailsbyGroupName
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getGroupDetailsbyGroupName")]
        [HttpGet]
        public Group getGroupDetailsbyGroupName(string groupname)
        {
            Group groupDetailsList = new Group();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.group  where GroupName = '" + groupname + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {


                            groupDetailsList.GroupOrginID = dr["GroupOrginID"].ToString();
                            groupDetailsList.GroupName = dr["GroupName"].ToString();
                            groupDetailsList.HospitalName = dr["HospitalName"].ToString();
                            groupDetailsList.NoOfPerson = dr["NoOfPerson"].ToString();
                            groupDetailsList.GroupCreatDate = dr["GroupCreatDate"].ToString();
                            groupDetailsList.Amount = Convert.ToDouble(dr["Amount"]);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return groupDetailsList;
            }
        }
        #endregion

        #endregion


        #region insurance related
        #region getInsuranceDetails
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getInsuranceDetails")]
        [HttpGet]
        public List<InsuranceAdminLevel> getInsuranceDetails()
        {
            List<InsuranceAdminLevel> insuranceDetailsList = new List<InsuranceAdminLevel>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.InsuranceAdminLevel";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            InsuranceAdminLevel insurance = new InsuranceAdminLevel();
                            insurance.InsuranceOrginID = dr["InsuranceOrginID"].ToString();
                            insurance.InsuranceName = dr["InsuranceName"].ToString();
                            //   insurance.insu = dr["NoOfPerson"].ToString();
                            insurance.InsuranceCreatDate = dr["InsuranceCreatDate"].ToString();
                            insurance.Amount = Convert.ToDouble(dr["Amount"]);
                            insuranceDetailsList.Add(insurance);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return insuranceDetailsList;
            }
        }
        #endregion


        #region getInsuranceDetailsbyInsuranceName
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getInsuranceDetailsbyInsuranceName")]
        [HttpGet]
        public InsuranceAdminLevel getInsuranceDetailsbyInsuranceName(string insurancename)
        {
            InsuranceAdminLevel insuranceDetailsList = new InsuranceAdminLevel();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.InsuranceAdminLevel  where InsuranceName = '" + insurancename + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {


                            insuranceDetailsList.InsuranceOrginID = dr["InsuranceOrginID"].ToString();
                            insuranceDetailsList.InsuranceName = dr["InsuranceName"].ToString();
                            //insuranceDetailsList.NoOfPerson = dr["NoOfPerson"].ToString();
                            insuranceDetailsList.InsuranceCreatDate = dr["InsuranceCreatDate"].ToString();
                            insuranceDetailsList.Amount = Convert.ToDouble(dr["Amount"]);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return insuranceDetailsList;
            }
        }
        #endregion



        #region getinsuranceProfileDetailsByMrdNo
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getinsuranceProfileDetailsByMrdNo")]
        [HttpGet]
        public List<InsuranceProfileList> getinsuranceProfileDetailsByMrdNo(string insurancemrdno)
        {
            List<InsuranceProfileList> insuranceProfileList = new List<InsuranceProfileList>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.InsuranceProfileList where MrdNoInsurance='" + insurancemrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InsuranceProfileList insuranceProfile = new InsuranceProfileList();
                            //insuranceProfile.Sno = i;
                            insuranceProfile.InsuranceProfileListID = (int)dr["InsuranceProfileListID"];
                            insuranceProfile.ProfileCode = dr["ProfileCode"].ToString();
                            insuranceProfile.ProfileName = dr["ProfileName"].ToString();
                            insuranceProfile.Amount = Convert.ToDouble(dr["Amount"]);
                            insuranceProfileList.Add(insuranceProfile);
                            // i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return insuranceProfileList;
            }
        }
        #endregion


        #region getinsuranceTestDetailsByMrdNo
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getinsuranceTestDetailsByMrdNo")]
        [HttpGet]
        public List<InsuranceTestList> getinsuranceTestDetailsByMrdNo(string insurancemrdno)
        {
            List<InsuranceTestList> insuranceTestList = new List<InsuranceTestList>();
            DataTable dt = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.InsuranceTestList where MrdNoInsurance='" + insurancemrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InsuranceTestList insuranceTest = new InsuranceTestList();
                            //   insuranceTest.Sno = i;
                            insuranceTest.InsuranceTestListId = (int)dr["InsuranceTestListId"];
                            insuranceTest.TestCode = dr["TestCode"].ToString();
                            insuranceTest.TestName = dr["TestName"].ToString();
                            insuranceTest.Amount = Convert.ToDouble(dr["Amount"]);

                            //   i++;
                            insuranceTestList.Add(insuranceTest);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return insuranceTestList;
            }
        }
        #endregion

        #endregion



        #region outhospital related
        #region getOutofhospitalDetails
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getOutofhospitalDetails")]
        [HttpGet]
        public List<OutofHospitalAdminLevel> getOutofhospitalDetails()
        {
            List<OutofHospitalAdminLevel> OuthospitalDetailsList = new List<OutofHospitalAdminLevel>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.OutofHospitalAdminLevel";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            OutofHospitalAdminLevel outofhospital = new OutofHospitalAdminLevel();
                            outofhospital.HospitalOrginID = dr["HospitalOrginID"].ToString();
                            outofhospital.HospitalName = dr["HospitalName"].ToString();
                            //   insurance.insu = dr["NoOfPerson"].ToString();
                            outofhospital.OutOfHospitalCreatDate = dr["OutOfHospitalCreatDate"].ToString();
                            outofhospital.Amount = Convert.ToDouble(dr["Amount"]);
                            OuthospitalDetailsList.Add(outofhospital);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return OuthospitalDetailsList;
            }
        }
        #endregion





        #region getHospitalDetailsbyName
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getHospitalDetailsbyName")]
        [HttpGet]
        public OutofHospitalAdminLevel getHospitalDetailsbyInsuranceName(string outofhospitalname)
        {
            OutofHospitalAdminLevel HospitalDetailsList = new OutofHospitalAdminLevel();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.OutofHospitalAdminLevel  where HospitalName = '" + outofhospitalname + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {


                            HospitalDetailsList.HospitalOrginID = dr["HospitalOrginID"].ToString();
                            HospitalDetailsList.HospitalName = dr["HospitalName"].ToString();
                            //insuranceDetailsList.NoOfPerson = dr["NoOfPerson"].ToString();
                            HospitalDetailsList.OutOfHospitalCreatDate = dr["OutOfHospitalCreatDate"].ToString();
                            HospitalDetailsList.Amount = Convert.ToDouble(dr["Amount"]);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return HospitalDetailsList;
            }
        }
        #endregion



        #region getoutofhospitalProfileDetailsByMrdNo
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getoutofhospitalProfileDetailsByMrdNo")]
        [HttpGet]
        public List<OutofHospitalProfilelist> getoutofhospitalProfileDetailsByMrdNo(string hospitalmrdno)
        {
            List<OutofHospitalProfilelist> outofhospitalProfileList = new List<OutofHospitalProfilelist>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.outofhospitalprofilelist where MrdNoOutOfHospital='" + hospitalmrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            OutofHospitalProfilelist hospiatlanameprofile = new OutofHospitalProfilelist();
                            //insuranceProfile.Sno = i;
                            hospiatlanameprofile.OutOfHospitalProfileListID = (int)dr["OutOfHospitalProfileListID"];
                            hospiatlanameprofile.ProfileCode = dr["ProfileCode"].ToString();
                            hospiatlanameprofile.ProfileName = dr["ProfileName"].ToString();
                            hospiatlanameprofile.Amount = Convert.ToDouble(dr["Amount"]);
                            outofhospitalProfileList.Add(hospiatlanameprofile);
                            // i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return outofhospitalProfileList;
            }
        }
        #endregion



        #region getoutofhospitalProfile
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getoutofhospitalProfile")]
        [HttpGet]
        public List<OutofHospitalProfilelist> getoutofhospitalProfile(string hospitalName)
        {
            List<OutofHospitalProfilelist> outofhospitalProfileList = new List<OutofHospitalProfilelist>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.outofhospitalprofilelist where HospitalName='" + hospitalName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            OutofHospitalProfilelist hospiatlanameprofile = new OutofHospitalProfilelist();
                            //insuranceProfile.Sno = i;
                            hospiatlanameprofile.OutOfHospitalProfileListID = (int)dr["OutOfHospitalProfileListID"];
                            hospiatlanameprofile.ProfileCode = dr["ProfileCode"].ToString();
                            hospiatlanameprofile.ProfileName = dr["ProfileName"].ToString();
                            hospiatlanameprofile.Amount = Convert.ToDouble(dr["Amount"]);
                            outofhospitalProfileList.Add(hospiatlanameprofile);
                            // i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return outofhospitalProfileList;
            }
        }
        #endregion

        #region getHospitalTestDetailsByMrdNo
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getHospitalTestDetailsByMrdNo")]
        [HttpGet]
        public List<OutofHospitalTestlist> getHospitalTestDetailsByMrdNo(string hospitalmrdno)
        {
            List<OutofHospitalTestlist> hospitalTestList = new List<OutofHospitalTestlist>();
            DataTable dt = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.outofhospitaltestlist where MrdNoOutOfHospital='" + hospitalmrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            OutofHospitalTestlist hospitalTest = new OutofHospitalTestlist();
                            //   insuranceTest.Sno = i;
                            hospitalTest.OutOfHospitalTestListId = (int)dr["OutOfHospitalTestListId"];
                            hospitalTest.TestCode = dr["TestCode"].ToString();
                            hospitalTest.TestName = dr["TestName"].ToString();
                            hospitalTest.Amount = Convert.ToDouble(dr["Amount"]);

                            //   i++;
                            hospitalTestList.Add(hospitalTest);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return hospitalTestList;
            }
        }
        #endregion
        #endregion


        #region getHospitalTestDetails
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getHospitalTestDetails")]
        [HttpGet]
        public List<OutofHospitalTestlist> getHospitalTestDetails(string hospitalName)
        {
            List<OutofHospitalTestlist> hospitalTestList = new List<OutofHospitalTestlist>();
            DataTable dt = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.outofhospitaltestlist where HospitalName='" + hospitalName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            OutofHospitalTestlist hospitalTest = new OutofHospitalTestlist();
                            //   insuranceTest.Sno = i;
                            hospitalTest.OutOfHospitalTestListId = (int)dr["OutOfHospitalTestListId"];
                            hospitalTest.TestCode = dr["TestCode"].ToString();
                            hospitalTest.TestName = dr["TestName"].ToString();
                            hospitalTest.Amount = Convert.ToDouble(dr["Amount"]);

                            //   i++;
                            hospitalTestList.Add(hospitalTest);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return hospitalTestList;
            }
        }
        #endregion


        #region InsuranceAdmin
        #region insertInsuranceAdminLevel
        /// <summary>
        /// Table - Insert InsuranceAdminLevel
        /// </summary>
        /// <param name="insuranceAdminLevel"></param>
        [Route("api/Account/insertInsuranceAdminLevel")]
        [HttpPost]
        public void insertInsuranceAdminLevel(InsuranceAdminLevel insuranceAdminLevel)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.InsuranceAdminLevel(InsuranceOrginID,InsuranceName,InsuranceCreatDate,Amount) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", insuranceAdminLevel.InsuranceOrginID);
                    cmd.Parameters.AddWithValue("@val2", insuranceAdminLevel.InsuranceName);
                    cmd.Parameters.AddWithValue("@val3", insuranceAdminLevel.InsuranceCreatDate);
                    cmd.Parameters.AddWithValue("@val4", insuranceAdminLevel.Amount);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region insertInsuranceProfileList
        /// <summary>
        /// Table - insert Insuranceprofilelist
        /// </summary>
        /// <param name="insuranceprofilelist"></param>
        [Route("api/Account/insertInsuranceprofilelist")]
        [HttpPost]
        public void insertInsuranceProfileList(InsuranceProfileList insuranceprofilelist)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.Insuranceprofilelist(ProfileCode,ProfileName,Amount,MrdNoInsurance,InsuranceName) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", insuranceprofilelist.ProfileCode);
                    cmd.Parameters.AddWithValue("@val2", insuranceprofilelist.ProfileName);
                    cmd.Parameters.AddWithValue("@val3", insuranceprofilelist.Amount);
                    cmd.Parameters.AddWithValue("@val4", insuranceprofilelist.MrdNoInsurance);
                    cmd.Parameters.AddWithValue("@val5", insuranceprofilelist.InsuranceName);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region insertInsuranceTestList
        /// <summary>
        /// Table - insert Insurancetestlist
        /// </summary>
        /// <param name="insuranceTestList"></param>
        [Route("api/Account/insertInsuranceTestList")]
        [HttpPost]
        public void insertInsuranceTestList(InsuranceTestList insuranceTestList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.Insurancetestlist(TestCode,TestName,Amount,MrdNoInsurance,InsuranceName) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", insuranceTestList.TestCode);
                    cmd.Parameters.AddWithValue("@val2", insuranceTestList.TestName);
                    cmd.Parameters.AddWithValue("@val3", insuranceTestList.Amount);
                    cmd.Parameters.AddWithValue("@val4", insuranceTestList.MrdNoInsurance);
                    cmd.Parameters.AddWithValue("@val5", insuranceTestList.InsuranceName);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region getAllProfilelistByInsuranceName
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getAllProfilelistByInsuranceName")]
        [HttpGet]
        public List<InsuranceProfileList> getAllProfilelistByInsuranceName(string insurancename)
        {
            List<InsuranceProfileList> lstInsProfileDetails = new List<InsuranceProfileList>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {

                try
                {
                    //string strSQL = "SELECT* FROM((childtestlist INNER JOIN labtestlist ON childtestlist.TestCode = labtestlist.TestCode) INNER JOIN samplecollecter ON labtestlist.TestCode = samplecollecter.TestCode) where labtestlist.MrdNo = '" + mrdNo + "'";
                    string strSQL = "SELECT * FROM pathoclinic.insuranceprofilelist where InsuranceName = '" + insurancename + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InsuranceProfileList InsuranceProfile = new InsuranceProfileList();

                            InsuranceProfile.InsuranceProfileListID = (int)dr["InsuranceProfileListID"];
                            InsuranceProfile.ProfileCode = dr["ProfileCode"].ToString();
                            InsuranceProfile.ProfileName = dr["ProfileName"].ToString();
                            InsuranceProfile.Amount = (double)dr["Amount"];
                            InsuranceProfile.MrdNoInsurance = dr["MrdNoInsurance"].ToString();
                            InsuranceProfile.InsuranceName = dr["InsuranceName"].ToString();
                            lstInsProfileDetails.Add(InsuranceProfile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstInsProfileDetails;
            }
        }
        #endregion


        #region getAllTestlistByInsuranceName
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getAllTestlistByInsuranceName")]
        [HttpGet]
        public List<InsuranceTestList> getAllTestlistByInsuranceName(string insurancename)
        {
            List<InsuranceTestList> lstInsTestDetails = new List<InsuranceTestList>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {

                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.insurancetestlist where InsuranceName = '" + insurancename + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InsuranceTestList insurancelTest = new InsuranceTestList();

                            insurancelTest.InsuranceTestListId = (int)dr["InsuranceTestListId"];
                            insurancelTest.TestCode = dr["TestCode"].ToString();
                            insurancelTest.TestName = dr["TestName"].ToString();
                            insurancelTest.Amount = (double)dr["Amount"];
                            insurancelTest.MrdNoInsurance = dr["MrdNoInsurance"].ToString();
                            insurancelTest.InsuranceName = dr["InsuranceName"].ToString();
                            lstInsTestDetails.Add(insurancelTest);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstInsTestDetails;
            }
        }
        #endregion
        #endregion

        #region OutOfHospitalAdmin
        #region insertOutOfHospitalAdminLevel
        /// <summary>
        /// Table - Insert outofhospitaladminlevel
        /// </summary>
        /// <param name="outofHospitalAdminLevel"></param>
        [Route("api/Account/insertOutOfHospitalAdminLevel")]
        [HttpPost]
        public void insertOutOfHospitalAdminLevel(OutofHospitalAdminLevel outofHospitalAdminLevel)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.outofhospitaladminlevel(HospitalOrginID,HospitalName,OutOfHospitalCreatDate,Amount) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", outofHospitalAdminLevel.HospitalOrginID);
                    cmd.Parameters.AddWithValue("@val2", outofHospitalAdminLevel.HospitalName);
                    cmd.Parameters.AddWithValue("@val3", outofHospitalAdminLevel.OutOfHospitalCreatDate);
                    cmd.Parameters.AddWithValue("@val4", outofHospitalAdminLevel.Amount);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region insertOutofHospitalProfilelist
        /// <summary>
        /// Table - insert outofhospitalprofilelist
        /// </summary>
        /// <param name="outofHospitalProfilelist"></param>
        [Route("api/Account/insertOutofHospitalProfilelist")]
        [HttpPost]
        public void insertOutofHospitalProfilelist(OutofHospitalProfilelist outofHospitalProfilelist)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.outofhospitalprofilelist(ProfileCode,ProfileName,Amount,MrdNoOutOfHospital,HospitalName) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", outofHospitalProfilelist.ProfileCode);
                    cmd.Parameters.AddWithValue("@val2", outofHospitalProfilelist.ProfileName);
                    cmd.Parameters.AddWithValue("@val3", outofHospitalProfilelist.Amount);
                    cmd.Parameters.AddWithValue("@val4", outofHospitalProfilelist.MrdNoOutOfHospital);
                    cmd.Parameters.AddWithValue("@val5", outofHospitalProfilelist.HospitalName);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region insertOutofHospitalTestlist
        /// <summary>
        /// Table - insert outofhospitaltestlist
        /// </summary>
        /// <param name="outofHospitalTestlist"></param>
        [Route("api/Account/insertOutofHospitalTestlist")]
        [HttpPost]
        public void insertOutofHospitalTestlist(OutofHospitalTestlist outofHospitalTestlist)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.outofhospitaltestlist(TestCode,TestName,Amount,MrdNoOutOfHospital,HospitalName) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", outofHospitalTestlist.TestCode);
                    cmd.Parameters.AddWithValue("@val2", outofHospitalTestlist.TestName);
                    cmd.Parameters.AddWithValue("@val3", outofHospitalTestlist.Amount);
                    cmd.Parameters.AddWithValue("@val4", outofHospitalTestlist.MrdNoOutOfHospital);
                    cmd.Parameters.AddWithValue("@val5", outofHospitalTestlist.HospitalName);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getAllProfilelistByHospitalName
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getAllProfilelistByHospitalName")]
        [HttpGet]
        public List<OutofHospitalProfilelist> getAllProfilelistByHospitalName(string gethospitalname)
        {
            List<OutofHospitalProfilelist> lstHospProfileDetails = new List<OutofHospitalProfilelist>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {

                try
                {
                    //string strSQL = "SELECT* FROM((childtestlist INNER JOIN labtestlist ON childtestlist.TestCode = labtestlist.TestCode) INNER JOIN samplecollecter ON labtestlist.TestCode = samplecollecter.TestCode) where labtestlist.MrdNo = '" + mrdNo + "'";
                    string strSQL = "SELECT* FROM outofhospitalprofilelist where HospitalName = '" + gethospitalname + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            OutofHospitalProfilelist hospitalProfile = new OutofHospitalProfilelist();

                            hospitalProfile.OutOfHospitalProfileListID = (int)dr["OutOfHospitalProfileListID"];
                            hospitalProfile.ProfileCode = dr["ProfileCode"].ToString();
                            hospitalProfile.ProfileName = dr["ProfileName"].ToString();
                            hospitalProfile.Amount = (double)dr["Amount"];
                            hospitalProfile.MrdNoOutOfHospital = dr["MrdNoOutOfHospital"].ToString();
                            hospitalProfile.HospitalName = dr["HospitalName"].ToString();
                            lstHospProfileDetails.Add(hospitalProfile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstHospProfileDetails;
            }
        }
        #endregion


        #region getAllTestlistByHospitalName
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getAllTestlistByHospitalName")]
        [HttpGet]
        public List<OutofHospitalTestlist> getAllTestlistByHospitalName(string gethospitalname)
        {
            List<OutofHospitalTestlist> lstHospTestDetails = new List<OutofHospitalTestlist>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {

                try
                {
                    string strSQL = "SELECT* FROM outofhospitaltestlist where HospitalName = '" + gethospitalname + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            OutofHospitalTestlist hospitalTest = new OutofHospitalTestlist();

                            hospitalTest.OutOfHospitalTestListId = (int)dr["OutOfHospitalTestListId"];
                            hospitalTest.TestCode = dr["TestCode"].ToString();
                            hospitalTest.TestName = dr["TestName"].ToString();
                            hospitalTest.Amount = (double)dr["Amount"];
                            hospitalTest.MrdNoOutOfHospital = dr["MrdNoOutOfHospital"].ToString();
                            hospitalTest.HospitalName = dr["HospitalName"].ToString();
                            lstHospTestDetails.Add(hospitalTest);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstHospTestDetails;
            }
        }
        #endregion
        #endregion

        //get Sample 


        #region getSampleListByMrdNo
        /// <summary>
        /// Table - LabTestList,childtestlist
        /// </summary>
        /// Listed all values from LabTestList,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getSampleListByMrdNo")]
        [HttpGet]
        public List<SampleContainersByMrdNo> getSampleListByMrdNo(string mrdNo)
        {
            List<SampleContainersByMrdNo> lstSampleDetails = new List<SampleContainersByMrdNo>();
            DataTable dt = new DataTable();
            //int i = 1;

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT* FROM childtestlist INNER JOIN labtestlist ON childtestlist.TestCode = labtestlist.TestCode where labtestlist.MrdNo = '" + mrdNo + "'AND childtestlist.ActiveStatus=1";



                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            //new
                            //   SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();
                            string mrdNoLocal = dr["MrdNo"].ToString();
                            string testcodeLocal = dr["TestCode"].ToString();
                            DataTable dts = new DataTable();
                            // int i = 1;
                            using (MySqlConnection conns = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                            {

                                //string strSQL = "SELECT * FROM childtestlist INNER JOIN labProfilelist ON childtestlist.ProfileID = labProfilelist.ProfileID where labProfilelist.MrdNo = '"+ mrdNo + "'";

                                string strSQLs = "SELECT * FROM SampleCollecter where MrdNo = '" + mrdNoLocal + "' and TestCode='" + testcodeLocal + "'";



                                conns.Open();
                                MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conns);
                                MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                                DataSet dss = new DataSet();
                                mydatas.Fill(dss);
                                dts = dss.Tables[0];
                                if (dts.Rows.Count > 0)
                                {
                                    foreach (DataRow drs in dts.Rows)
                                    {
                                        if (drs != null)
                                        {


                                            SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();
                                            sampleContainer.MrdNo = mrdNo;
                                            sampleContainer.SampleStatus = (int)drs["SampleStatus"];
                                            sampleContainer.SampleContainer = drs["SampleName"].ToString();
                                            sampleContainer.TestCode = drs["TestCode"].ToString();
                                            sampleContainer.TestID = (int)drs["TestID"];
                                            sampleContainer.ProfileID = (int)drs["ProfileID"];
                                            sampleContainer.ProfileName = drs["ProfileName"].ToString();
                                            sampleContainer.TestName = drs["TestName"].ToString();
                                            sampleContainer.IndividualStatus = (int)dr["IndividualStatus"];
                                            sampleContainer.Dynamic = "";
                                            sampleContainer.Catagory_Test_Profile = "Test";
                                            if (sampleContainer.SampleStatus != 1)
                                            {
                                                DataTable dt2 = new DataTable();
                                                string strSQL2 = "SELECT * FROM pathoclinic.Alternativesamplecontainer where TestCode='" + testcodeLocal + "' ";
                                                // conn.Open();
                                                MySqlDataAdapter mydata2 = new MySqlDataAdapter(strSQL2, conn);
                                                MySqlCommandBuilder cmd2 = new MySqlCommandBuilder(mydata2);
                                                DataSet ds2 = new DataSet();
                                                mydata2.Fill(ds2);
                                                dt2 = ds2.Tables[0];
                                                if (dt2.Rows.Count != 0)
                                                {
                                                    foreach (DataRow dr2 in dt2.Rows)
                                                    {
                                                        if (dr2 != null)
                                                        {

                                                            sampleContainer.AlternativeSampleName = dr2["AlternativeSampleName"].ToString(); sampleContainer.AlternativeSampleAvailable = "Yes";

                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    sampleContainer.AlternativeSampleAvailable = "No";
                                                }
                                            }
                                            else
                                            {
                                                sampleContainer.AlternativeSampleAvailable = "No";
                                            }
                                            lstSampleDetails.Add(sampleContainer);
                                            //  sampleContainer.SampleStatus = (int)drs["SampleStatus"];
                                        }



                                    }
                                }
                                else
                                {



                                    SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();
                                    sampleContainer.MrdNo = mrdNo;
                                    sampleContainer.TestID = (int)dr["TestID"];
                                    sampleContainer.ProfileID = (int)dr["ProfileID"];
                                    sampleContainer.TestCode = dr["TestCode"].ToString();
                                    sampleContainer.TestName = dr["TestName"].ToString();
                                    sampleContainer.SampleContainer = dr["SampleContainer"].ToString();
                                    sampleContainer.AlternativeSampleAvailable = dr["AlternativeSampleContainer"].ToString();
                                    sampleContainer.IndividualStatus = (int)dr["IndividualStatus"];
                                    sampleContainer.Dynamic = "";
                                    sampleContainer.Catagory_Test_Profile = "Test";
                                    if (sampleContainer.AlternativeSampleAvailable == "Yes")
                                    {
                                        DataTable dt2 = new DataTable();
                                        string strSQL2 = "SELECT * FROM pathoclinic.Alternativesamplecontainer where TestCode='" + testcodeLocal + "' ";
                                        // conn.Open();
                                        MySqlDataAdapter mydata2 = new MySqlDataAdapter(strSQL2, conn);
                                        MySqlCommandBuilder cmd2 = new MySqlCommandBuilder(mydata2);
                                        DataSet ds2 = new DataSet();
                                        mydata2.Fill(ds2);
                                        dt2 = ds2.Tables[0];
                                        foreach (DataRow dr2 in dt2.Rows)
                                        {
                                            if (dr2 != null)
                                            {

                                                sampleContainer.AlternativeSampleName = dr2["AlternativeSampleName"].ToString();

                                            }
                                        }
                                    }
                                    sampleContainer.SampleStatus = 0;
                                    lstSampleDetails.Add(sampleContainer);

                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstSampleDetails;
            }
        }
        #endregion

        #region getSampleListProfileByMrdNo
        /// <summary>
        /// Table - labProfilelist,childtestlist
        /// </summary>
        /// Listed all values from labProfilelist,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getSampleListProfileByMrdNo")]
        [HttpGet]
        public List<SampleContainersByMrdNo> getSampleListProfileByMrdNo(string mrdNo)
        {
            List<SampleContainersByMrdNo> lstSampleDetails = new List<SampleContainersByMrdNo>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM childtestlist INNER JOIN labProfilelist ON childtestlist.ProfileID = labProfilelist.ProfileID where labProfilelist.MrdNo = '" + mrdNo + "'AND childtestlist.ActiveStatus=1";



                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            //new
                            SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();
                            string mrdNoLocal = dr["MrdNo"].ToString();
                            string testcodeLocal = dr["TestCode"].ToString();
                            DataTable dts = new DataTable();
                            // int i = 1;
                            using (MySqlConnection conns = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                            {
                                string strSQLs = "SELECT * FROM SampleCollecter where MrdNo = '" + mrdNoLocal + "' and TestCode='" + testcodeLocal + "'";
                                conns.Open();
                                MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conns);
                                MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                                DataSet dss = new DataSet();
                                mydatas.Fill(dss);
                                dts = dss.Tables[0];
                                foreach (DataRow drs in dts.Rows)
                                {
                                    if (drs != null)
                                    {
                                        sampleContainer.SampleStatus = (int)drs["SampleStatus"];
                                    }
                                }
                            }
                            sampleContainer.MrdNo = dr["MrdNo"].ToString();
                            sampleContainer.TestID = (int)dr["TestID"];
                            sampleContainer.TestCode = dr["TestCode"].ToString();
                            sampleContainer.TestName = dr["TestName"].ToString();
                            sampleContainer.SampleContainer = dr["SampleContainer"].ToString();
                            lstSampleDetails.Add(sampleContainer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstSampleDetails;
            }
        }
        #endregion


        //new Code

        #region getSampleListTestByProfileTestCode
        /// <summary>
        /// Table - labProfilelist,childtestlist
        /// </summary>
        /// Listed all values from labProfilelist,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getSampleListTestByProfileTestCode")]
        [HttpGet]
        public List<SampleContainersByMrdNo> getSampleListTestByProfileTestCode(string mrdNo, string testCode)
        {
            List<SampleContainersByMrdNo> lstSampleDetails = new List<SampleContainersByMrdNo>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT * FROM childtestlist INNER JOIN labProfilelist ON childtestlist.ProfileID = labProfilelist.ProfileID where labProfilelist.MrdNo = '"+ mrdNo + "'";

                    //string strSQL = "SELECT * FROM SampleCollecter where MrdNo = '" + mrdNo + "' and TestCode ='"+ testCode + "'";

                    string strSQL = "SELECT* FROM childtestlist Left JOIN samplecollecter ON childtestlist.TestCode = samplecollecter.TestCode where samplecollecter.MrdNo = '" + mrdNo + "'AND childtestlist.ActiveStatus=1";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();

                            sampleContainer.MrdNo = dr["MrdNo"].ToString();
                            sampleContainer.TestID = (int)dr["TestID"];
                            sampleContainer.TestCode = dr["TestCode"].ToString();
                            sampleContainer.SampleContainer = dr["SampleName"].ToString();
                            sampleContainer.SampleStatus = (int)dr["SampleStatus"];
                            lstSampleDetails.Add(sampleContainer);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstSampleDetails;
            }
        }
        #endregion


        #region insertSampleCollectTestDetails
        /// <summary>
        /// Table -  SampleCollecter
        /// Inserted the  SampleCollecter table values.
        /// </summary>
        /// <param name="SampleCollect"></param>
        [Route("api/Account/insertSampleCollectTestDetails")]
        [HttpPost]
        public void insertSampleCollectTestDetails(SampleCollect sampleCollect)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    using (MySqlConnection conns = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                    {


                        DataTable dts = new DataTable();
                        string strSQLs = "SELECT * FROM SampleCollecter where MrdNo = '" + sampleCollect.MrdNo + "' and TestCode='" + sampleCollect.TestCode + "'";



                        conns.Open();
                        conn.Open();
                        MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conns);
                        MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                        DataSet dss = new DataSet();
                        mydatas.Fill(dss);
                        dts = dss.Tables[0];
                        if (dts.Rows.Count > 0)
                        {
                            foreach (DataRow drs in dts.Rows)
                            {
                                if (drs != null)
                                {
                                    string strSQL = "UPDATE  SampleCollecter SET SampleStatus='" + sampleCollect.SampleStatus + "',SampleName='" + sampleCollect.SampleName + "' where MrdNo='" + sampleCollect.MrdNo + "' && TestCode='" + sampleCollect.TestCode + "'";

                                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                                    cmd.ExecuteNonQuery();

                                    if (sampleCollect.SampleStatus == 1 && sampleCollect.ProfileID == 0)
                                    {
                                        string strSQL1 = "UPDATE labtestlist SET IndividualStatus=2 where MrdNo='" + sampleCollect.MrdNo + "' && TestCode='" + sampleCollect.TestCode + "'";

                                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                                        cmd1.ExecuteNonQuery();
                                    }

                                    if (sampleCollect.SampleStatus == 1 && sampleCollect.ProfileID != 0)
                                    {
                                        string strSQL1 = "UPDATE labprofilelist SET IndividualStatus=2 where MrdNo='" + sampleCollect.MrdNo + "' && ProfileID='" + sampleCollect.ProfileID + "'";

                                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                                        cmd1.ExecuteNonQuery();
                                    }

                                }

                            }
                        }

                        else
                        {
                            string strSQL = "INSERT INTO SampleCollecter(TestID,TestCode,TestName,SampleStatus,MrdNo,SampleName,ProfileCode,ProfileID,ProfileName,ProfilePriority) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10)";

                            MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                            cmd.Parameters.AddWithValue("@val1", sampleCollect.TestID);
                            cmd.Parameters.AddWithValue("@val2", sampleCollect.TestCode);
                            cmd.Parameters.AddWithValue("@val3", sampleCollect.TestName);
                            cmd.Parameters.AddWithValue("@val4", sampleCollect.SampleStatus);
                            cmd.Parameters.AddWithValue("@val5", sampleCollect.MrdNo);
                            cmd.Parameters.AddWithValue("@val6", sampleCollect.SampleName);
                            cmd.Parameters.AddWithValue("@val7", sampleCollect.ProfileCode);
                            cmd.Parameters.AddWithValue("@val8", sampleCollect.ProfileID);
                            cmd.Parameters.AddWithValue("@val9", sampleCollect.ProfileName);
                            cmd.Parameters.AddWithValue("@val10", sampleCollect.ProfilePriority);
                            cmd.ExecuteNonQuery();

                            if (sampleCollect.SampleStatus == 1 && sampleCollect.ProfileID == 0)
                            {
                                string strSQL1 = "UPDATE labtestlist SET IndividualStatus=2 where MrdNo='" + sampleCollect.MrdNo + "' && TestCode='" + sampleCollect.TestCode + "'";

                                MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                                cmd1.ExecuteNonQuery();
                            }
                            if (sampleCollect.SampleStatus == 1 && sampleCollect.ProfileID != 0)
                            {
                                string strSQL1 = "UPDATE labprofilelist SET IndividualStatus=2 where MrdNo='" + sampleCollect.MrdNo + "' && ProfileID='" + sampleCollect.ProfileID + "'";

                                MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                                cmd1.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region updateSampleCollectTestDetails
        /// <summary>
        /// Table -  SampleCollecter
        /// Inserted the  SampleCollecter table values.
        /// </summary>
        /// <param name="SampleCollect"></param>
        [Route("api/Account/updateSampleCollectTestDetails")]
        [HttpPost]
        public void updateSampleCollectTestDetails(SampleCollect sampleCollect)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    using (MySqlConnection conns = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                    {


                        DataTable dts = new DataTable();
                        string strSQLs = "SELECT * FROM SampleCollecter where MrdNo = '" + sampleCollect.MrdNo + "' and TestCode='" + sampleCollect.TestCode + "'";



                        conns.Open();
                        conn.Open();
                        MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conns);
                        MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                        DataSet dss = new DataSet();
                        mydatas.Fill(dss);
                        dts = dss.Tables[0];
                        if (dts.Rows.Count > 0)
                        {
                            foreach (DataRow drs in dts.Rows)
                            {
                                if (drs != null)
                                {
                                    string strSQL = "UPDATE  SampleCollecter SET SampleStatus='" + sampleCollect.SampleStatus + "' where MrdNo='" + sampleCollect.MrdNo + "' && TestCode='" + sampleCollect.TestCode + "'";

                                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                                    cmd.ExecuteNonQuery();

                                    string strSQLUpdate = "UPDATE LaborderStatus SET LabStatus='1' where MrdNo='" + sampleCollect.MrdNo + "'";
                               
                                    MySqlCommand cmdUpdate = new MySqlCommand(strSQLUpdate, conn);
                                    cmdUpdate.CommandType = CommandType.Text;
                                    cmdUpdate.ExecuteNonQuery();
                                }

                            }
                        }


                    }
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region getSampleListByMrdNoforLabTech
        /// <summary>
        /// Table - LabTestList,childtestlist
        /// </summary>
        /// Listed all values from LabTestList,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getSampleListByMrdNoforLabTech")]
        [HttpGet]
        public List<LabTechGrid> getSampleListByMrdNoforLabTech(string mrdNo)
        {
            List<LabTechGrid> lstLabTech = new List<LabTechGrid>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT* FROM((childtestlist INNER JOIN labtestlist ON childtestlist.TestCode = labtestlist.TestCode) INNER JOIN samplecollecter ON labtestlist.TestCode = samplecollecter.TestCode) where labtestlist.MrdNo = '" + mrdNo + "'";
                    string strSQL = "SELECT* FROM labtestlist where MrdNo = '" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabTechGrid labTech = new LabTechGrid();

                            labTech.TestID = (int)dr["TestID"];
                            labTech.TestCode = dr["TestCode"].ToString();
                            //  labTech.SampleName = dr["SampleName"].ToString();
                            labTech.TestName = dr["TestName"].ToString();
                            labTech.IndividualStatus = (int)dr["IndividualStatus"];
                            lstLabTech.Add(labTech);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabTech;
            }
        }
        #endregion


        #region getSampleListProfileByMrdNoForLabTech
        /// <summary>
        /// Table - labProfilelist,childtestlist
        /// </summary>
        /// Listed all values from labProfilelist,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getSampleListProfileByMrdNoForLabTech")]
        [HttpGet]
        public List<SampleContainersByMrdNo> getSampleListProfileByMrdNoForLabTech(string mrdNo)
        {
            List<SampleContainersByMrdNo> lstSampleDetails = new List<SampleContainersByMrdNo>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM childtestlist INNER JOIN labProfilelist ON childtestlist.ProfileID = labProfilelist.ProfileID where labProfilelist.MrdNo = '" + mrdNo + "' AND childtestlist.ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();

                            sampleContainer.TestID = (int)dr["TestID"];
                            sampleContainer.TestCode = dr["TestCode"].ToString();
                            sampleContainer.SampleContainer = dr["SampleContainer"].ToString();
                            lstSampleDetails.Add(sampleContainer);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstSampleDetails;
            }
        }
        #endregion



        //Get All Details from Progress Table 

        #region LAB TEST LIST
        #region InsertBoneMarrowDetails
        /// <summary>
        /// Table - boneMarrowAspiration
        /// </summary>
        /// <param name="boneMarrowAspiration"></param>
        [Route("api/Account/InsertBoneMarrowDetails")]
        [HttpPost]
        public void InsertBoneMarrowDetails(BoneMarrowAspiration boneMarrowAspiration)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {

                try
                {
                    int coutDetails = 0;
                    string fetch = "Select count(Id) from boneMarrowAspiration where Id='" + boneMarrowAspiration.Id + "'";
                    MySqlDataAdapter mydata = new MySqlDataAdapter(fetch, conn);
                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            coutDetails = Convert.ToInt32(dr[0]);
                        }
                    }
                    if (coutDetails == 0)
                    {
                        string strSQL = "INSERT INTO boneMarrowAspiration(id_MrdNumber,SampleId_mrdnumber,ClinicalFindings,PeripheralBloodFindings,BoneMarrowNumber,Cellularity,Erythropoiesis,Myelopoiesis,M_E,Eosinophils,Lymphocytes,PlasmaCells,Blasts,Megakaryocytes,Others,Perl_sIronStain,Impression,Advice,Status) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16,@val17,@val18,@val19)";
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                        cmd.Parameters.AddWithValue("@val1", boneMarrowAspiration.id_MrdNumber);
                        cmd.Parameters.AddWithValue("@val2", boneMarrowAspiration.SampleId_mrdnumber);
                        cmd.Parameters.AddWithValue("@val3", boneMarrowAspiration.ClinicalFindings);
                        cmd.Parameters.AddWithValue("@val4", boneMarrowAspiration.PeripheralBloodFindings);
                        cmd.Parameters.AddWithValue("@val5", boneMarrowAspiration.BoneMarrowNumber);
                        cmd.Parameters.AddWithValue("@val6", boneMarrowAspiration.Cellularity);
                        cmd.Parameters.AddWithValue("@val7", boneMarrowAspiration.Erythropoiesis);
                        cmd.Parameters.AddWithValue("@val8", boneMarrowAspiration.Myelopoiesis);
                        cmd.Parameters.AddWithValue("@val9", boneMarrowAspiration.M_E);
                        cmd.Parameters.AddWithValue("@val10", boneMarrowAspiration.Eosinophils);
                        cmd.Parameters.AddWithValue("@val11", boneMarrowAspiration.Lymphocytes);
                        cmd.Parameters.AddWithValue("@val12", boneMarrowAspiration.PlasmaCells);
                        cmd.Parameters.AddWithValue("@val13", boneMarrowAspiration.Blasts);
                        cmd.Parameters.AddWithValue("@val14", boneMarrowAspiration.Megakaryocytes);
                        cmd.Parameters.AddWithValue("@val15", boneMarrowAspiration.Others);
                        cmd.Parameters.AddWithValue("@val16", boneMarrowAspiration.Perl_sIronStain);
                        cmd.Parameters.AddWithValue("@val17", boneMarrowAspiration.Impression);
                        cmd.Parameters.AddWithValue("@val18", boneMarrowAspiration.Advice);
                        cmd.Parameters.AddWithValue("@val19", boneMarrowAspiration.Status);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region getAllfromBoneMarrowAspiration
        /// <summary>
        /// Table - bonemarrowaspiration
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [Route("api/Account/getAllfromBoneMarrowAspiration")]
        [HttpGet]
        public List<BoneMarrowAspiration> getAllfromBoneMarrowAspiration()
        {
            List<BoneMarrowAspiration> lstboneMarrowAspiration = new List<BoneMarrowAspiration>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            BoneMarrowAspiration boneMarrowAspiration = new BoneMarrowAspiration();
                            boneMarrowAspiration.Id = (int)dr["Id"];
                            boneMarrowAspiration.id_MrdNumber = dr["id_MrdNumber"].ToString();
                            boneMarrowAspiration.SampleId_mrdnumber = dr["SampleId_mrdnumber"].ToString();
                            boneMarrowAspiration.ClinicalFindings = dr["ClinicalFindings"].ToString();
                            boneMarrowAspiration.PeripheralBloodFindings = dr["PeripheralBloodFindings"].ToString();
                            boneMarrowAspiration.BoneMarrowNumber = dr["BoneMarrowNumber"].ToString();
                            boneMarrowAspiration.Cellularity = dr["Cellularity"].ToString();
                            boneMarrowAspiration.Erythropoiesis = dr["Erythropoiesis"].ToString();
                            boneMarrowAspiration.Myelopoiesis = dr["Myelopoiesis"].ToString();
                            boneMarrowAspiration.M_E = dr["M_E"].ToString();
                            boneMarrowAspiration.Eosinophils = dr["Eosinophils"].ToString();
                            boneMarrowAspiration.Lymphocytes = dr["Lymphocytes"].ToString();
                            boneMarrowAspiration.PlasmaCells = dr["PlasmaCells"].ToString();
                            boneMarrowAspiration.Blasts = dr["Blasts"].ToString();
                            boneMarrowAspiration.Megakaryocytes = dr["Megakaryocytes"].ToString();
                            boneMarrowAspiration.Others = dr["Others"].ToString();
                            boneMarrowAspiration.Perl_sIronStain = dr["Perl_sIronStain"].ToString();
                            boneMarrowAspiration.Impression = dr["Impression"].ToString();
                            boneMarrowAspiration.Status = dr["Status"].ToString();
                            lstboneMarrowAspiration.Add(boneMarrowAspiration);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstboneMarrowAspiration;
            }
        }
        #endregion


        #region InsertPapSmear
        /// <summary>
        /// table - papsmear
        /// </summary>
        /// <param name="papSmear"></param>
        [Route("api/Account/InsertPapSmear")]
        [HttpPost]
        public void InsertPapSmear(PaPSmear papSmear)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO papsmear(id_MrdNumber,SampleId_mrdnumber,Cytologynumber,Numberofsildes,Reportingsystem,Specimentype,SpecimenAdequacy,NonNeoplasticFindings,withoutCol1,withoutCol2,Organisms,withoutCol3,Others,EpithelialcellAbnormalities,withoutCol4,Interpretation,Educationalnotesandcomments,Status) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16,@val17,@val18)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", papSmear.id_MrdNumber);
                    cmd.Parameters.AddWithValue("@val2", papSmear.SampleId_mrdnumber);
                    cmd.Parameters.AddWithValue("@val3", papSmear.Cytologynumber);
                    cmd.Parameters.AddWithValue("@val4", papSmear.Numberofsildes);
                    cmd.Parameters.AddWithValue("@val5", papSmear.Reportingsystem);
                    cmd.Parameters.AddWithValue("@val6", papSmear.Specimentype);
                    cmd.Parameters.AddWithValue("@val7", papSmear.SpecimenAdequacy);
                    cmd.Parameters.AddWithValue("@val8", papSmear.NonNeoplasticFindings);
                    cmd.Parameters.AddWithValue("@val9", papSmear.withoutCol1);
                    cmd.Parameters.AddWithValue("@val10", papSmear.withoutCol2);
                    cmd.Parameters.AddWithValue("@val11", papSmear.Organisms);
                    cmd.Parameters.AddWithValue("@val12", papSmear.withoutCol3);
                    cmd.Parameters.AddWithValue("@val13", papSmear.Others);
                    cmd.Parameters.AddWithValue("@val14", papSmear.EpithelialcellAbnormalities);
                    cmd.Parameters.AddWithValue("@val15", papSmear.withoutCol4);
                    cmd.Parameters.AddWithValue("@val16", papSmear.Interpretation);
                    cmd.Parameters.AddWithValue("@val17", papSmear.Educationalnotesandcomments);
                    cmd.Parameters.AddWithValue("@val18", papSmear.Status);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion

        #endregion
        #region getAllfromProgressTable
        /// <summary>
        /// Table - progresstable
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [Route("api/Account/getAllfromProgressTable")]
        [HttpGet]
        public List<ProgressTable> getAllfromProgressTable()
        {
            List<ProgressTable> lstProgressTablelist = new List<ProgressTable>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.progresstable";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ProgressTable progresstable = new ProgressTable();
                            progresstable.Id = (int)dr["Id"];
                            progresstable.ProgressName = dr["ProgressName"].ToString();
                            progresstable.ProgressValue = dr["ProgressValue"].ToString();
                            progresstable.MrdNumber = dr["MrdNumber"].ToString();
                            lstProgressTablelist.Add(progresstable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstProgressTablelist;
            }
        }
        #endregion

        #region updateMrdNumberforProgress
        /// <summary>
        /// Table - progresstable
        /// Update MrdNo to progresstable tables
        /// </summary>
        /// <param name="progressTable"></param>
        [Route("api/Account/updateMrdNumberforProgress")]
        [HttpPost]
        public void updateMrdNumberforProgress(ProgressTable progressTable)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  progresstable SET MrdNumber='" + progressTable.MrdNumber + "' where Id='" + progressTable.Id + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion



        #region getGroupDetailsbyGroupNameBySearch
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getGroupDetailsbyGroupNameBySearch")]
        [HttpGet]
        public List<Invoice> getGroupDetailsbyGroupNameBySearch(string groupname)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action LIKE'GRP-%" + groupname + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion



        #region getGroupDetailsbyHospitalNameBySearch
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getGroupDetailsbyHospitalNameBySearch")]
        [HttpGet]
        public List<Invoice> getGroupDetailsbyHospitalNameBySearch(string hospitalname)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Invoice  where HospitalName =  '" + hospitalname + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion




        #region getGroupDetailsbyGroupNameAndDate
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getGroupDetailsbyGroupNameAndDate")]
        [HttpGet]
        public List<Invoice> getGroupDetailsbyGroupNameAndDate(string groupname, string Fromdate, string Todate)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "Select* FROM pathoclinic.invoice where Action = 'GRP-" + groupname + "' && InvoiceDate Between '" + Fromdate + "' AND '" + Todate + "'";

                    //string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action LIKE'GRP-%" + groupname + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion



        #region getHospitalDetailsbyHospitalNameAndDate
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getHospitalDetailsbyHospitalNameAndDate")]
        [HttpGet]
        public List<Invoice> getHospitalDetailsbyHospitalNameAndDate(string Fromdate, string Todate, string hospitalname)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "Select* FROM pathoclinic.invoice  where ProviderHostName= '" + hospitalname + "' AND InvoiceDate Between '" + Fromdate + "' AND '" + Todate + "'";

                    //string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action LIKE'GRP-%" + groupname + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            invoice.NetAmount = dr["NetAmount"].ToString();
                            invoice.ProviderHostName = dr["ProviderHostName"].ToString();
                            invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                            invoice.PaymentType = dr["PaymentType"].ToString();
                            InvoiceDetailsList.Add(invoice);



                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion


        #region getInsuranceDetailsbyInsuranceNameAndDate
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getInsuranceDetailsbyInsuranceNameAndDate")]
        [HttpGet]
        public List<Invoice> getInsuranceDetailsbyInsuranceNameAndDate(string Fromdate, string Todate, string insurancename)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "Select* FROM pathoclinic.invoice  where ProviderHostName = '" + insurancename + "' AND InvoiceDate Between '" + Fromdate + "' AND '" + Todate + "'";

                    //string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action LIKE'GRP-%" + groupname + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.NetAmount = dr["NetAmount"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion

        #region getInsuranceDetailsbyInsunanceNameBySearch
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getInsuranceDetailsbyInsunanceNameBySearch")]
        [HttpGet]
        public List<Invoice> getInsuranceDetailsbyInsunanceNameBySearch(string insurancename)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action LIKE'%INS-" + insurancename + "'";
                    string strSQL = "SELECT * FROM pathoclinic.Invoice  where  ProviderHostName ='" + insurancename + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            invoice.NetAmount = dr["NetAmount"].ToString();

                            invoice.ProviderHostName = dr["ProviderHostName"].ToString();
                            invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                            invoice.PaymentType = dr["PaymentType"].ToString();
                            InvoiceDetailsList.Add(invoice);



                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion

        #region getHospitalDetailsbyHospitalNameBySearch
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getHospitalDetailsbyHospitalNameBySearch")]
        [HttpGet]
        public List<Invoice> getHospitalDetailsbyHospitalNameBySearch(string hospitalname)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action LIKE'%OHP-" + hospitalname + "'";
                    string strSQL = "SELECT * FROM pathoclinic.Invoice  where ProviderHostName ='" + hospitalname + "'";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            invoice.NetAmount = dr["NetAmount"].ToString();
                            invoice.ProviderHostName = dr["ProviderHostName"].ToString();
                            invoice.InvoiceDate = dr["InvoiceDate"].ToString();
                            invoice.PaymentType = dr["PaymentType"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion


        #region getGenerateInvoiceForGroup
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getGenerateInvoiceForGroup")]
        [HttpGet]
        public List<GroupGenerateInvoice> getGenerateInvoiceForGroup(string Mrdno)
        {
            List<GroupGenerateInvoice> lstgenerateInvoiceDetails = new List<GroupGenerateInvoice>();



            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //  string strSQL = "SELECT lo.PatientName, ls.MrdNo, ls.LabStatus FROM pathoclinic.laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo where ls.LabStatus = 'Inprogress'";

                    // string strSQL = "SELECT pr.RegID,pr.FirstName,pr.LastName,ls.MrdNo, ls.LabStatus, pr.Age, pr.Sex FROM((laborderstatus ls INNER JOIN laborder lo ON ls.MrdNo = lo.MrdNo) INNER JOIN patientregistration pr ON lo.RegID = pr.RegID) where ls.LabStatus = 'Inprogress'";
                    string strSQL = "SELECT* FROM((Invoice INNER JOIN labtestlist ON labtestlist.MrdNo = Invoice.MrdNo INNER JOIN labprofilelist ON labprofilelist.MrdNo = Invoice.MrdNo)) where Invoice.MrdNo = '" + Mrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            GroupGenerateInvoice groupdetails = new GroupGenerateInvoice();



                            groupdetails.PatientName = dr["PatientName"].ToString();
                            groupdetails.PaidAmount = dr["PaidAmount"].ToString();
                            groupdetails.ProfileCode = dr["ProfileCode"].ToString();
                            groupdetails.TestCode = dr["TestCode"].ToString();
                            groupdetails.RegID = (int)dr["RegID"];
                            groupdetails.Amount = dr["Amount"].ToString();
                            groupdetails.Action = dr["Action"].ToString();

                            lstgenerateInvoiceDetails.Add(groupdetails);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstgenerateInvoiceDetails;
            }
        }
        #endregion




        //insertCommanSampleContainer
        #region insertCommonSampleContainer
        /// <summary>
        /// Table - CommanSampleContainer
        /// </summary>
        /// Inserted the CommanSampleContainer table values.
        /// <param name="commonSampleContainer"></param>
        [Route("api/Account/insertCommonSampleContainer")]
        [HttpPost]
        public void insertCommanSampleContainer(CommonSampleContainer commonSampleContainer)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO CommonSampleContainer(SampleName,SampleTrunAroundTime) VALUES(@val1,@val2)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", commonSampleContainer.SampleName);
                    cmd.Parameters.AddWithValue("@val2", commonSampleContainer.SampleTrunAroundTime);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        //getAllCommonSampleContainer


        #region getAllCommonSampleContainer
        /// <summary>
        /// Table - CommonSampleContainer
        /// </summary>
        /// Listed all values from CommonSampleContainer table
        /// <returns></returns>
        [Route("api/Account/getAllCommonSampleContainer")]
        [HttpGet]
        public List<CommonSampleContainer> getAllCommonSampleContainer()
        {
            List<CommonSampleContainer> lstCommanSampleContainer = new List<CommonSampleContainer>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.CommonSampleContainer";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            CommonSampleContainer CommonSampleContainers = new CommonSampleContainer();

                            CommonSampleContainers.SampleContainerId = (int)dr["SampleContainerId"];
                            CommonSampleContainers.SampleName = dr["SampleName"].ToString();
                            CommonSampleContainers.SampleTrunAroundTime = dr["SampleTrunAroundTime"].ToString();

                            lstCommanSampleContainer.Add(CommonSampleContainers);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstCommanSampleContainer;
            }
        }
        #endregion



        //insertCommanSampleContainer
        #region insertCommonMethodology
        /// <summary>
        /// Table - CommonMethodology
        /// </summary>
        /// Inserted the CommonMethodology table values.
        /// <param name="commonSampleContainer"></param>
        [Route("api/Account/insertCommonMethodology")]
        [HttpPost]
        public void insertCommonMethodology(CommonMethodology commonMethodology)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO CommonMethodology(CommonMethodologyName) VALUES(@val1)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", commonMethodology.CommonMethodologyName);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        //getAllCommonSampleContainer


        #region getAllCommonMethodology
        /// <summary>
        /// Table - CommonMethodology
        /// </summary>
        /// Listed all values from CommonMethodology table
        /// <returns></returns>
        [Route("api/Account/getAllCommonMethodology")]
        [HttpGet]
        public List<CommonMethodology> getAllCommonMethodology()
        {
            List<CommonMethodology> lstCommonMethodology = new List<CommonMethodology>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.CommonMethodology";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            CommonMethodology commonMethodology = new CommonMethodology();

                            commonMethodology.CommonMethodologyId = (int)dr["CommonMethodologyId"];
                            commonMethodology.CommonMethodologyName = dr["CommonMethodologyName"].ToString();

                            lstCommonMethodology.Add(commonMethodology);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstCommonMethodology;
            }
        }
        #endregion


        //distinct sample for profile

        #region getProfileListByMrdNoDistinct
        /// <summary>
        /// Table - LabProfileList
        /// </summary>
        /// Listed all values from LabProfileList table
        /// <returns></returns>
        [Route("api/Account/getProfileListByMrdNoDistinct")]
        [HttpGet]
        public List<ProfileSampleContainer> getProfileListByMrdNoDistinct(string mrdNo, int profileID, string profileCode, string profileName)
        {
            List<ProfileSampleContainer> lstLabProfileDetailsDistinct = new List<ProfileSampleContainer>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT DISTINCT SampleContainer FROM pathoclinic.labprofilelist inner join childtestlist on childtestlist.ProfileID = '" + profileID + "' where MrdNo = '" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ProfileSampleContainer profileSampleContainer = new ProfileSampleContainer();

                            profileSampleContainer.ProfileName = profileName;
                            profileSampleContainer.ProfileCode = profileCode;
                            profileSampleContainer.ProfileID = profileID;
                            profileSampleContainer.MrdNo = mrdNo;
                            profileSampleContainer.SampleName = dr["SampleContainer"].ToString();
                            lstLabProfileDetailsDistinct.Add(profileSampleContainer);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLabProfileDetailsDistinct;
            }
        }
        #endregion


        #region getProfileByProfileID
        /// <summary>
        /// Table - LabTestList,childtestlist
        /// </summary>
        /// Listed all values from LabTestList,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getProfileByProfileID")]
        [HttpGet]
        public List<MasterProfileList> getProfileByProfileID(string profileID)
        {
            List<MasterProfileList> lstProfileList = new List<MasterProfileList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT* FROM masterprofilelist where profileID = '" + profileID + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            MasterProfileList masterProfileList = new MasterProfileList();

                            masterProfileList.ProfileID = (int)dr["ProfileID"];
                            masterProfileList.ProfileName = dr["ProfileName"].ToString();
                            masterProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            //masterProfileList.TestName = dr["TestName"].ToString();
                            //masterProfileList.SampleStatus = (int)dr["SampleStatus"];
                            lstProfileList.Add(masterProfileList);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstProfileList;
            }
        }
        #endregion



        ///new for profileID wise Test
        ///
           #region getProfileIdWiseTest
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// Listed all values from LabTestList,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getProfileIdWiseTest")]
        [HttpGet]
        public List<ChildTestList> getProfileIdWiseTest(string profileID)
        {
            List<ChildTestList> lstChildTest = new List<ChildTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT* FROM childtestlist where profileID = '" + profileID + "' AND ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ChildTestList childTest = new ChildTestList();

                            childTest.ProfileID = (int)dr["ProfileID"];
                            childTest.TestID = (int)dr["TestID"];
                            childTest.TestCode = dr["TestCode"].ToString();
                            childTest.TestName = dr["TestName"].ToString();
                            childTest.units = dr["units"].ToString();
                            //childTest.Priority = (int)dr["Priority"];
                            lstChildTest.Add(childTest);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstChildTest;
            }
        }
        #endregion

        ///age Reference
        #region getAgeReferenceTestDetails
        /// <summary>
        /// Table - agegenderwisereference
        /// </summary>
        /// Listed all values from agegenderwisereference table by testCode
        /// <returns></returns>
        [Route("api/Account/getAgeReferenceTestDetails")]
        [HttpGet]
        public List<AgewiseReference> getAgeReferenceTestDetails(string testCode, string ageRange)
        {
            List<AgewiseReference> lstChildTest = new List<AgewiseReference>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT " + ageRange + " FROM pathoclinic.agegenderwisereference where TestCode = '" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            AgewiseReference agewiseReference = new AgewiseReference();
                            agewiseReference.RequiredField = dr[0].ToString();
                            lstChildTest.Add(agewiseReference);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstChildTest;
            }
        }
        #endregion






        #region getAllTestList
        /// <summary>
        /// Table - masterprofilelist
        /// </summary>
        /// Listed all values from masterprofilelist table      
        /// <returns></returns>
        [Route("api/Account/getAllTestList")]
        [HttpGet]
        public List<ChildTestList> getAllTestList()
        {
            List<ChildTestList> lstTest = new List<ChildTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    int i = 0;
                    string strSQL = "SELECT * FROM pathoclinic.childtestlist where ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        i = i + 1;
                        if (dr != null)
                        {
                            ChildTestList testList = new ChildTestList();
                            testList.Sno = i;
                            testList.TestID = (int)dr["TestID"];
                            testList.TestCode = dr["TestCode"].ToString();
                            testList.TestName = dr["TestName"].ToString();

                            lstTest.Add(testList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstTest;
            }
        }
        #endregion


        #region insertTestdetailsinMasterProfileList
        /// <summary>
        /// Table - MasterProfileList
        /// </summary>
        ///
        /// <param name="masterProfileList"></param>
        [Route("api/Account/insertTestdetailsinMasterProfileList")]
        [HttpPost]
        public void insertTestdetailsinMasterProfileList(MasterProfileList masterProfileList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //  ChildTestList.ProfileID = 1;
                    string ProfileCode = "";
                    string fetch = "Select Max(ProfileCode) from MasterProfileList";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(fetch, conn);
                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ProfileCode = dr[0].ToString();

                        }
                        if (ProfileCode.Trim() != "")
                        {
                            var tempArray = ProfileCode.Split('P');
                            string tempId1 = tempArray[1];

                            var tempArray2 = tempId1.Split('R');
                            string tempId = tempArray2[1];
                            int id = Convert.ToInt32(tempId);
                            id = id + 1;
                            ProfileCode = "PR" + String.Format("{0:000}", id);
                        }
                        else
                        {
                            ProfileCode = "PR" + String.Format("{0:000}", 1);
                        }
                    }




                    DateTime today = DateTime.Today;
                    masterProfileList.CreateDate = today.ToShortDateString();
                    string strSQL = "INSERT INTO MasterProfileList(ProfileName,Amount,CreateDate,ProfileCode) VALUES(@val1,@val2,@val3,@val4)";
                    //   conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", masterProfileList.ProfileName);
                    cmd.Parameters.AddWithValue("@val2", masterProfileList.Amount);
                    cmd.Parameters.AddWithValue("@val3", masterProfileList.CreateDate);
                    cmd.Parameters.AddWithValue("@val4", ProfileCode);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getAllProfilesIdByMax
        /// <summary>
        /// Table - masterprofilelist
        /// </summary>
        /// Listed all values from masterprofilelist table      
        /// <returns></returns>
        [Route("api/Account/getAllProfilesIdByMax")]
        [HttpGet]
        public MasterProfileList getAllProfilesIdByMax()
        {
            MasterProfileList masterProfileList = new MasterProfileList();
            // MasterProfileList lstProfiles = new MasterProfileList();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT max(ProfileID) FROM pathoclinic.masterprofilelist";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {


                            masterProfileList.ProfileID = (int)dr["max(ProfileID)"];
                            //masterProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            //masterProfileList.ProfileName = dr["ProfileName"].ToString();

                            // lstProfiles.Add(masterProfileList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return masterProfileList;
            }
        }
        #endregion

        #region updateMasterProfileID
        /// <summary>
        /// Table - Invoice
        /// Inserted the invoiceview table values. The reg ID reffered from patientregistration table.
        /// </summary>
        /// <param name="childTestList"></param>
        [Route("api/Account/updateMasterProfileID")]
        [HttpPost]
        public void updateMasterProfileID(ChildTestList updateTest)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DateTime today = DateTime.Today;

                    string strSQL = "UPDATE childtestlist SET ProfileID = '" + updateTest.ProfileID + "',ProfilePriority = '" + updateTest.Sno + "'  WHERE TestID = '" + updateTest.TestID + "' AND ActiveStatus=1";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion




        #region InsertAgewiseReferences
        /// <summary>
        /// table - AgeReferenceRange
        /// </summary>
        /// <param name="AgewiseReferenceRange"></param>
        [Route("api/Account/InsertAgewiseReferences")]
        [HttpPost]
        public void InsertAgewiseReferences(AgewiseReferenceRange agewiseReferenceRange)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO agegenderwisereference(TestCode,Parameter,Birth,3Days,1Week,2Weeks,1Month,2Months,6Months,1year,6years,12years,Men,Women) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", agewiseReferenceRange.TestCode);
                    cmd.Parameters.AddWithValue("@val2", agewiseReferenceRange.Parameter);
                    cmd.Parameters.AddWithValue("@val3", agewiseReferenceRange.Birth);
                    cmd.Parameters.AddWithValue("@val4", agewiseReferenceRange.Days3);
                    cmd.Parameters.AddWithValue("@val5", agewiseReferenceRange.Week1);
                    cmd.Parameters.AddWithValue("@val6", agewiseReferenceRange.Weeks2);
                    cmd.Parameters.AddWithValue("@val7", agewiseReferenceRange.Month1);
                    cmd.Parameters.AddWithValue("@val8", agewiseReferenceRange.Months2);
                    cmd.Parameters.AddWithValue("@val9", agewiseReferenceRange.Months6);
                    cmd.Parameters.AddWithValue("@val10", agewiseReferenceRange.year1);
                    cmd.Parameters.AddWithValue("@val11", agewiseReferenceRange.years6);
                    cmd.Parameters.AddWithValue("@val12", agewiseReferenceRange.years12);
                    cmd.Parameters.AddWithValue("@val13", agewiseReferenceRange.Men);
                    cmd.Parameters.AddWithValue("@val14", agewiseReferenceRange.Women);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion


        #region InsertAgewiseCricticalValue
        /// <summary>
        /// table -  AgewiseCricticalValue
        /// </summary>
        /// <param name=" AgewiseCricticalValue"></param>
        [Route("api/Account/InsertAgewiseCricticalValue")]
        [HttpPost]
        public void InsertAgewiseCricticalValue(AgewiseCricticalValue agewiseCricticalValue)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO agewisecricticalvalue(TestCode,Parameter,Birth,3Days,1Week,2Weeks,1Month,2Months,6Months,1year,6years,12years,Men,Women) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", agewiseCricticalValue.TestCode);
                    cmd.Parameters.AddWithValue("@val2", agewiseCricticalValue.Parameter);
                    cmd.Parameters.AddWithValue("@val3", agewiseCricticalValue.Birth);
                    cmd.Parameters.AddWithValue("@val4", agewiseCricticalValue.Days3);
                    cmd.Parameters.AddWithValue("@val5", agewiseCricticalValue.Week1);
                    cmd.Parameters.AddWithValue("@val6", agewiseCricticalValue.Weeks2);
                    cmd.Parameters.AddWithValue("@val7", agewiseCricticalValue.Month1);
                    cmd.Parameters.AddWithValue("@val8", agewiseCricticalValue.Months2);
                    cmd.Parameters.AddWithValue("@val9", agewiseCricticalValue.Months6);
                    cmd.Parameters.AddWithValue("@val10", agewiseCricticalValue.year1);
                    cmd.Parameters.AddWithValue("@val11", agewiseCricticalValue.years6);
                    cmd.Parameters.AddWithValue("@val12", agewiseCricticalValue.years12);
                    cmd.Parameters.AddWithValue("@val13", agewiseCricticalValue.Men);
                    cmd.Parameters.AddWithValue("@val14", agewiseCricticalValue.Women);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion





        #region InsertPragancyReferancyRange
        /// <summary>
        /// table - PragancyReferancyRange
        /// </summary>
        /// <param name="pragancyReferancyRange"></param>
        [Route("api/Account/InsertPragancyReferancyRange")]
        [HttpPost]
        public void InsertPragancyReferancyRange(PragancyReferancyRange pragancyReferancyRange)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pregnancyreferencerange(TestCode,Parameter,FirstTrimester,SecondTrimester,ThirdTrimester) VALUES(@val1,@val2,@val3,@val4,@val5 )";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", pragancyReferancyRange.TestCode);
                    cmd.Parameters.AddWithValue("@val2", pragancyReferancyRange.Parameter);
                    cmd.Parameters.AddWithValue("@val3", pragancyReferancyRange.FirstTrimester);
                    cmd.Parameters.AddWithValue("@val4", pragancyReferancyRange.SecondTrimester);
                    cmd.Parameters.AddWithValue("@val5", pragancyReferancyRange.ThirdTrimester);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion









        #region getMaxTestCode
        /// <summary>
        /// Table - masterprofilelist
        /// </summary>
        /// Listed all values from masterprofilelist table      
        /// <returns></returns>
        [Route("api/Account/getMaxTestCode")]
        [HttpGet]
        public ChildTestList getMaxTestCode()
        {
            ChildTestList childTest = new ChildTestList();
            // MasterProfileList lstProfiles = new MasterProfileList();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "Select Max(TestCode) from ChildTestList where ActiveStatus = 1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {


                            childTest.TestCode = dr[0].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return childTest;
            }
        }
        #endregion



        //sample Collect 27/11

        #region getSampleStatusLabTest
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getSampleStatusLabTest")]
        [HttpGet]
        public int getSampleStatusLabTest(string mrdNo)
        {
            int count = 0;

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = " SELECT * FROM samplecollecter Inner Join labtestlist on samplecollecter.TestCode = labtestlist.TestCode where samplecollecter.MrdNo = '" + mrdNo + "' and samplecollecter.SampleStatus != 1; ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion




        #region getSampleStatusLabProfile
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getSampleStatusLabProfile")]
        [HttpGet]
        public int getSampleStatusLabProfile(string mrdNo)
        {
            int count = 0;

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = " SELECT * FROM samplecollecter Inner Join labprofilelist on samplecollecter.ProfileCode = labprofilelist.ProfileCode where samplecollecter.MrdNo = '" + mrdNo + "' and samplecollecter.SampleStatus != 1;";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion




        #region updateLabSampleStatus 
        /// <summary>
        /// Table - Invoice
        /// Inserted the invoiceview table values. The reg ID reffered from patientregistration table.
        /// </summary>
        /// <param name="LaborderStatus"></param>
        [Route("api/Account/updateLabSampleStatus")]
        [HttpPost]
        public void updateLabSampleStatus(string mrdNo)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE LaborderStatus SET SampleStatus='1' where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion



        #region getBoneMarrowResultByMrdNo
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getBoneMarrowResultByMrdNo")]
        [HttpGet]
        public List<BoneMarrowAspiration> getBoneMarrowResultByMrdNo(string mrdNo)
        {
            List<BoneMarrowAspiration> lstBoneMarrow = new List<BoneMarrowAspiration>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration Where id_MrdNumber='" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {



                            BoneMarrowAspiration boneMarrowAspiration = new BoneMarrowAspiration();

                            boneMarrowAspiration.ClinicalFindings = dr["ClinicalFindings"].ToString();
                            boneMarrowAspiration.PeripheralBloodFindings = dr["PeripheralBloodFindings"].ToString();
                            boneMarrowAspiration.BoneMarrowNumber = dr["BoneMarrowNumber"].ToString();
                            boneMarrowAspiration.Cellularity = dr["Cellularity"].ToString();
                            boneMarrowAspiration.Erythropoiesis = dr["Erythropoiesis"].ToString();
                            boneMarrowAspiration.Myelopoiesis = dr["Myelopoiesis"].ToString();
                            boneMarrowAspiration.M_E = dr["M_E"].ToString();
                            boneMarrowAspiration.Eosinophils = dr["Eosinophils"].ToString();
                            boneMarrowAspiration.Lymphocytes = dr["Lymphocytes"].ToString();
                            boneMarrowAspiration.PlasmaCells = dr["PlasmaCells"].ToString();
                            boneMarrowAspiration.Blasts = dr["Blasts"].ToString();
                            boneMarrowAspiration.Megakaryocytes = dr["Megakaryocytes"].ToString();
                            boneMarrowAspiration.Others = dr["Others"].ToString();
                            boneMarrowAspiration.Perl_sIronStain = dr["Perl_sIronStain"].ToString();
                            boneMarrowAspiration.Impression = dr["Impression"].ToString();
                            boneMarrowAspiration.Advice = dr["Advice"].ToString();
                            boneMarrowAspiration.Status = dr["Status"].ToString();
                            lstBoneMarrow.Add(boneMarrowAspiration);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstBoneMarrow;
            }
        }
        #endregion






        #region insertLabTechResult
        /// <summary>
        /// table - ResultLabTech
        /// </summary>
        /// <param name="resultLabTech"></param>
        [Route("api/Account/insertLabTechResult")]
        [HttpPost]
        public void insertLabTechResult(ResultLabTech resultLabTech)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest Where MrdNo='" + resultLabTech.MrdNo + "' && TestCode ='" + resultLabTech.TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //string strSQL1 = "UPDATE resultlabtest SET Result = @Result, Comment1 = @Comment , Comment2 = @Notes,CreateDate = @CreateDate,Units = @Units, DefaultValues = @NormalValues, SpecialComments = @SpecialComments, StartRange = @StartRange ,EndRange = @EndRange,Methodology= @Methodology,CriticalValue=@CriticalValue,AdditionalFixedComments= @AdditionalFixedComments,DisplayValueText=@DisplayValueText Where MrdNo='" + resultLabTech.MrdNo + "' && TestCode ='" + resultLabTech.TestCode + "'";
                        ////  conn.Open();
                        string strSQL1 = "UPDATE resultlabtest SET Result = @Result, Comment1 = @Comment , Comment2 = @Notes , CreateDate = @CreateDate Where MrdNo='" + resultLabTech.MrdNo + "' && TestCode ='" + resultLabTech.TestCode + "'";
                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.Parameters.AddWithValue("@Result", resultLabTech.Result);
                        cmd1.Parameters.AddWithValue("@Comment", resultLabTech.Comment);
                        cmd1.Parameters.AddWithValue("@Notes", resultLabTech.Notes);
                        cmd1.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        //cmd1.Parameters.AddWithValue("@Units", resultLabTech.Units);
                        //cmd1.Parameters.AddWithValue("@NormalValues", resultLabTech.NormalValues);
                        //cmd1.Parameters.AddWithValue("@SpecialComments", resultLabTech.SpecialComments);
                        //cmd1.Parameters.AddWithValue("@StartRange", resultLabTech.StartRange);
                        //cmd1.Parameters.AddWithValue("@EndRange", resultLabTech.EndRange);
                        //cmd1.Parameters.AddWithValue("@Methodology", resultLabTech.Methodology);
                        //cmd1.Parameters.AddWithValue("@AdditionalFixedComments", resultLabTech.AdditionalFixedComments);
                        //cmd1.Parameters.AddWithValue("@CriticalValue", resultLabTech.CriticalValue);
                        //cmd1.Parameters.AddWithValue("@DisplayValueText", resultLabTech.DisplayValueText);


                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();

                        if (resultLabTech.SpecialComments != null && resultLabTech.SpecialComments != "")
                        {
                            strSQL1 = "UPDATE laborder SET SpecialComments = '" + resultLabTech.SpecialComments + "' Where MrdNo='" + resultLabTech.MrdNo + "'";
                            cmd1 = new MySqlCommand(strSQL1, conn);
                            cmd1.ExecuteNonQuery();



                            resultLabTech.MrdNo = resultLabTech.MrdNo.Remove(resultLabTech.MrdNo.Length - 3);
                            resultLabTech.MrdNo = resultLabTech.MrdNo.Remove(0, 2);

                            int regIDValue = Convert.ToInt32(resultLabTech.MrdNo);


                            strSQL1 = "UPDATE patientregistration SET SpecialComments = '" + resultLabTech.SpecialComments + "' Where RegID='" + regIDValue + "'";
                            cmd1 = new MySqlCommand(strSQL1, conn);
                            cmd1.ExecuteNonQuery();
                        }

                    }
                    else
                    {

                        string strSQL1 = "INSERT INTO resultlabtest(MrdNo,TestCode,TestName,Result,Comment1,CreateDate,units,DefaultValues,Comment2,SpecialComments,StartRange,EndRange,Methodology,AdditionalFixedComments,CriticalValue,DisplayValueText,RegId,SampleContainer,ProfilePriority,FreeText,NormalValuesFiled) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16,@val17,@val18,@val19,@val20,@val21)";

                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.Parameters.AddWithValue("@val1", resultLabTech.MrdNo);
                        cmd1.Parameters.AddWithValue("@val2", resultLabTech.TestCode);
                        cmd1.Parameters.AddWithValue("@val3", resultLabTech.TestName);
                        cmd1.Parameters.AddWithValue("@val4", resultLabTech.Result);
                        cmd1.Parameters.AddWithValue("@val5", resultLabTech.Comment);
                        cmd1.Parameters.AddWithValue("@val6", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@val7", resultLabTech.Units);
                        cmd1.Parameters.AddWithValue("@val8", resultLabTech.NormalValues);
                        cmd1.Parameters.AddWithValue("@val9", resultLabTech.Notes);
                        cmd1.Parameters.AddWithValue("@val10", resultLabTech.SpecialComments);
                        cmd1.Parameters.AddWithValue("@val11", resultLabTech.StartRange);
                        cmd1.Parameters.AddWithValue("@val12", resultLabTech.EndRange);
                        cmd1.Parameters.AddWithValue("@val13", resultLabTech.Methodology);
                        cmd1.Parameters.AddWithValue("@val14", resultLabTech.AdditionalFixedComments);
                        cmd1.Parameters.AddWithValue("@val15", resultLabTech.CriticalValue);
                        cmd1.Parameters.AddWithValue("@val16", resultLabTech.DisplayValueText);
                        cmd1.Parameters.AddWithValue("@val17", resultLabTech.RegId);
                        cmd1.Parameters.AddWithValue("@val18", resultLabTech.SampleContainer);
                        cmd1.Parameters.AddWithValue("@val19", resultLabTech.ProfilePriority);
                        cmd1.Parameters.AddWithValue("@val20", resultLabTech.FreeText);
                        cmd1.Parameters.AddWithValue("@val21", resultLabTech.NormalValuesFiled);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion


        //add Test Validation

        #region getDuplicationSampleContainer
        /// <summary>
        /// Table - CommonSampleContainer
        /// </summary>
        /// Listed all values from CommonSampleContainer table
        /// <returns></returns>
        [Route("api/Account/getDuplicationSampleContainer")]
        [HttpGet]
        public int getDuplicationSampleContainer(string sampleName)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.CommonSampleContainer where SampleName='" + sampleName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion



        #region getDuplicationCommonMethodology
        /// <summary>
        /// Table - CommonMethodologyName
        /// </summary>
        /// Listed all values from CommonMethodologyName table
        /// <returns></returns>
        [Route("api/Account/getDuplicationCommonMethodology")]
        [HttpGet]
        public int getDuplicationCommonMethodology(string methodology)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.CommonMethodology where CommonMethodologyName='" + methodology + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region getDuplicationTestName
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// Listed all values from childtestlist table
        /// <returns></returns>
        [Route("api/Account/getDuplicationTestName")]
        [HttpGet]
        public int getDuplicationTestName(string testName)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.childtestlist where TestName='" + testName + "' AND ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region updateLabSampleStatusCompleted 
        /// <summary>
        /// Table - Invoice
        /// Inserted the invoiceview table values. The reg ID reffered from patientregistration table.
        /// </summary>
        /// <param name="LaborderStatus"></param>
        [Route("api/Account/updateLabSampleStatusCompleted")]
        [HttpPost]
        public void updateLabSampleStatusCompleted(string mrdNo)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE LaborderStatus SET LabStatus='3' where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion

        #region getallcompletedlaborderstatusforNotify
        /// <summary>
        /// Table - LaborderStatus
        /// </summary>
        ///  get All Completed status details from LaborderStatus table.
        /// <returns></returns>
        [Route("api/Account/getallcompletedlaborderstatusforNotify")]
        [HttpGet]
        public int getallcompletedlaborderstatusforNotify()
        {
            CountApprover countApprover = new CountApprover();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT count(*) FROM pathoclinic.laborderstatus as A  inner join pathoclinic.laborder as B on A.MrdNo=B.MrdNo inner join pathoclinic.resultlabtest as C on C.MrdNo = B.MrdNo  where A.LabStatus='completed'";
                    string strSQL = "SELECT count(*) FROM pathoclinic.laborderstatus as A  inner join pathoclinic.laborder as B on A.MrdNo=B.MrdNo where A.LabStatus=3";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            countApprover.Count = Convert.ToInt32(dr[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return countApprover.Count;
            }
        }
        #endregion

        #region getAllCompletedLaborderStatus
        /// <summary>
        /// Table - LaborderStatus
        /// </summary>
        ///  get All Completed status details from LaborderStatus table.
        /// <returns></returns>
        [Route("api/Account/getAllCompletedLaborderStatus")]
        [HttpGet]
        public List<PatientAllLabStatusInfo> getAllCompletedLaborderStatus()
        {
            List<PatientAllLabStatusInfo> lstpatientAllLabStatusInfo = new List<PatientAllLabStatusInfo>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {

                try
                {
                    string strSQL = "SELECT A.LaborderStatusId, A.RegID,A.MrdNo,A.LabStatus,A.LabOrderDate,A.ApproveStatus,A.DenyStatus,B.PatientName FROM pathoclinic.laborderstatus as A  inner join pathoclinic.laborder as B on A.MrdNo=B.MrdNo where A.LabStatus=3";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            PatientAllLabStatusInfo patientAllLabStatusInfo = new PatientAllLabStatusInfo();
                            //laborderstatus table
                            patientAllLabStatusInfo.LaborderStatusId = (int)dr["LaborderStatusId"];
                            patientAllLabStatusInfo.RegID = (int)dr["RegID"];
                            patientAllLabStatusInfo.MrdNo = dr["MrdNo"].ToString();
                            patientAllLabStatusInfo.LabStatus = dr["LabStatus"].ToString();
                            patientAllLabStatusInfo.LabOrderDate = dr["LabOrderDate"].ToString();
                            patientAllLabStatusInfo.ApproveStatus = dr["ApproveStatus"].ToString();
                            patientAllLabStatusInfo.DenyStatus = dr["DenyStatus"].ToString();

                            //laborder table
                            //patientAllLabStatusInfo.LabId = (int)dr["LabId"];
                            patientAllLabStatusInfo.PatientName = dr["PatientName"].ToString();

                            //resultlabtest Tabel
                            //patientAllLabStatusInfo.ResultLabId = (int)dr["ResultLabId"];
                            // patientAllLabStatusInfo.TestCode = dr["TestCode"].ToString();
                            // patientAllLabStatusInfo.TestName = dr["TestName"].ToString();
                            //patientAllLabStatusInfo.ProfileCode = dr["ProfileCode"].ToString();
                            //patientAllLabStatusInfo.ProfileName = dr["ProfileName"].ToString();

                            //patientAllLabStatusInfo.DepartmentID = dr["DepartmentID"].ToString();
                            //patientAllLabStatusInfo.Methodology = dr["Methodology"].ToString();
                            //patientAllLabStatusInfo.SampleContainer = dr["SampleContainer"].ToString();
                            //patientAllLabStatusInfo.DepartmentName = dr["DepartmentName"].ToString();

                            //patientAllLabStatusInfo.count = (int)dr["count"];

                            lstpatientAllLabStatusInfo.Add(patientAllLabStatusInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return lstpatientAllLabStatusInfo;
        }
        #endregion


        #region getAllLaborderStatusByDropDownSearch
        /// <summary>
        /// Table - LaborderStatus
        /// </summary>
        ///  get All Completed status details from LaborderStatus table.
        /// <returns></returns>
        [Route("api/Account/getAllLaborderStatusByDropDownSearch")]
        [HttpGet]
        public List<PatientAllLabStatusInfo> getAllLaborderStatusByDropDownSearch(string status)
        {
            List<PatientAllLabStatusInfo> lstpatientAllLabStatusInfo = new List<PatientAllLabStatusInfo>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {

                try
                {




                    //string strSQL = "SELECT A.LaborderStatusId, A.RegID,A.MrdNo,A.LabStatus,A.LabOrderDate,A.ApproveStatus,A.DenyStatus,B.PatientName,C.TestCode,C.TestName FROM pathoclinic.laborderstatus as A  inner join pathoclinic.laborder as B on A.MrdNo=B.MrdNo inner join pathoclinic.resultlabtest as C on C.MrdNo = B.MrdNo  where A.LabStatus='completed'";
                    string strSQL = "SELECT A.LaborderStatusId, A.RegID,A.MrdNo,A.LabStatus,A.LabOrderDate,A.ApproveStatus,A.DenyStatus,B.PatientName FROM pathoclinic.laborderstatus as A  inner join pathoclinic.laborder as B on A.MrdNo=B.MrdNo where A.LabStatus='" + status + "'";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            PatientAllLabStatusInfo patientAllLabStatusInfo = new PatientAllLabStatusInfo();
                            //laborderstatus table
                            patientAllLabStatusInfo.LaborderStatusId = (int)dr["LaborderStatusId"];
                            patientAllLabStatusInfo.RegID = (int)dr["RegID"];
                            patientAllLabStatusInfo.MrdNo = dr["MrdNo"].ToString();
                            patientAllLabStatusInfo.LabStatus = dr["LabStatus"].ToString();
                            patientAllLabStatusInfo.LabOrderDate = dr["LabOrderDate"].ToString();
                            patientAllLabStatusInfo.ApproveStatus = dr["ApproveStatus"].ToString();
                            patientAllLabStatusInfo.DenyStatus = dr["DenyStatus"].ToString();

                            //laborder table
                            //patientAllLabStatusInfo.LabId = (int)dr["LabId"];
                            patientAllLabStatusInfo.PatientName = dr["PatientName"].ToString();


                            lstpatientAllLabStatusInfo.Add(patientAllLabStatusInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return lstpatientAllLabStatusInfo;
        }
        #endregion

        #region getAllMaxRegistrationId
        /// <summary>
        /// Table - masterprofilelist
        /// </summary>
        /// Listed all values from masterprofilelist table      
        /// <returns></returns>
        [Route("api/Account/getAllMaxRegistrationId")]
        [HttpGet]
        public PatientRegistration getAllMaxRegistrationId()
        {
            PatientRegistration maxregid = new PatientRegistration();
            // MasterProfileList lstProfiles = new MasterProfileList();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT max(RegID),FirstName,LastName FROM pathoclinic.patientregistration";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            maxregid.RegID = (int)dr["max(RegID)"];


                            //masterProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            //masterProfileList.ProfileName = dr["ProfileName"].ToString();

                            // lstProfiles.Add(masterProfileList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return maxregid;
            }
        }
        #endregion

        // 01-12-2017

        #region getTestIdWiseTest
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// Listed all values from childtestlist table by test ID
        /// <returns></returns>
        [Route("api/Account/getTestIdWiseTest")]
        [HttpGet]
        public List<ChildTestList> getTestIdWiseTest(string testID)
        {
            List<ChildTestList> lstChildTest = new List<ChildTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT* FROM childtestlist where TestID = '" + testID + "' AND ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ChildTestList childTest = new ChildTestList();

                            //     childTest.ProfileID = (int)dr["ProfileID"];
                            childTest.TestID = (int)dr["TestID"];
                            childTest.TestCode = dr["TestCode"].ToString();
                            childTest.TestName = dr["TestName"].ToString();
                            childTest.units = dr["units"].ToString();
                            childTest.CalculationPresent = dr["CalculationPresent"].ToString();
                            childTest.Methodology = dr["Methodology"].ToString();
                            childTest.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            childTest.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            childTest.ProfilePriority = (int)dr["ProfilePriority"];
                            //childTest.Priority = (int)dr["Priority"];
                            lstChildTest.Add(childTest);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstChildTest;
            }
        }
        #endregion


        //02-12-2017





        #region insertAgewiseReferenceValue
        /// <summary>
        /// table - AgeReferenceRange
        /// </summary>
        /// <param name="insertAgewiseReferenceValue"></param>
        [Route("api/Account/insertAgewiseReferenceValue")]
        [HttpPost]
        public void insertAgewiseReferenceValue(Agewisereferencevalue agewisereferencevalue)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "INSERT INTO agewisereferencevalue(TestCode,TestName,StartDay,EndDay,StartMonth,EndMonth,StartYear,EndYear,LowUpperReferenceValue,FreeText,Units,DisplayValue,ElementName,Sex) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", agewisereferencevalue.TestCode);
                    cmd.Parameters.AddWithValue("@val2", agewisereferencevalue.TestName);
                    cmd.Parameters.AddWithValue("@val3", agewisereferencevalue.StartDay);
                    cmd.Parameters.AddWithValue("@val4", agewisereferencevalue.EndDay);
                    cmd.Parameters.AddWithValue("@val5", agewisereferencevalue.StartMonth);
                    cmd.Parameters.AddWithValue("@val6", agewisereferencevalue.EndMonth);
                    cmd.Parameters.AddWithValue("@val7", agewisereferencevalue.StartYear);
                    cmd.Parameters.AddWithValue("@val8", agewisereferencevalue.EndYear);
                    cmd.Parameters.AddWithValue("@val9", agewisereferencevalue.LowUpperReferenceValue);
                    cmd.Parameters.AddWithValue("@val10", agewisereferencevalue.FreeText);
                    cmd.Parameters.AddWithValue("@val11", agewisereferencevalue.Units);
                    cmd.Parameters.AddWithValue("@val12", agewisereferencevalue.DisplayText);
                    cmd.Parameters.AddWithValue("@val13", agewisereferencevalue.ElementName);
                    cmd.Parameters.AddWithValue("@val14", agewisereferencevalue.Sex);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion


        #region insertAgewiseCricticalReferences
        /// <summary>
        /// table - AgeReferenceRange
        /// </summary>
        /// <param name="insertAgewiseCricticalReferences"></param>
        [Route("api/Account/insertAgewiseCricticalReferences")]
        [HttpPost]
        public void insertAgewiseCricticalReferences(AgewiseCricticalReferences agewiseCricticalReferences)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "INSERT INTO agewiseCricticalReference(TestCode,TestName,StartDay,EndDay,StartMonth,EndMonth,StartYear,EndYear,LowUpperCricticalValue,Units,FreeText,DisplayValue,ElementName,Sex) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val2", agewiseCricticalReferences.TestName);
                    cmd.Parameters.AddWithValue("@val1", agewiseCricticalReferences.TestCode);
                    cmd.Parameters.AddWithValue("@val3", agewiseCricticalReferences.StartDayCrictical);
                    cmd.Parameters.AddWithValue("@val4", agewiseCricticalReferences.EndDayCrictical);
                    cmd.Parameters.AddWithValue("@val5", agewiseCricticalReferences.StartMonthCrictical);
                    cmd.Parameters.AddWithValue("@val6", agewiseCricticalReferences.EndMonthCrictical);
                    cmd.Parameters.AddWithValue("@val7", agewiseCricticalReferences.StartYearCrictical);
                    cmd.Parameters.AddWithValue("@val8", agewiseCricticalReferences.EndYearCrictical);
                    cmd.Parameters.AddWithValue("@val9", agewiseCricticalReferences.LowUpperCricticalValue);
                    cmd.Parameters.AddWithValue("@val10", agewiseCricticalReferences.Units);
                    cmd.Parameters.AddWithValue("@val11", agewiseCricticalReferences.FreeText);
                    cmd.Parameters.AddWithValue("@val12", agewiseCricticalReferences.DisplayText);
                    cmd.Parameters.AddWithValue("@val13", agewiseCricticalReferences.ElementName);
                    cmd.Parameters.AddWithValue("@val14", agewiseCricticalReferences.Sex);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion


        //#region getBoneArrowReportByMrdNo
        ///// <summary>
        ///// Table - childtestlist
        ///// </summary>
        ///// Listed all values from childtestlist table
        ///// <returns></returns>
        //[Route("api/Account/getBoneArrowReportByMrdNo")]
        //[HttpGet]
        //public int getBoneArrowReportByMrdNo(string mrdNo)
        //{
        //    int count = 0;
        //    DataTable dt = new DataTable();
        //    using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
        //    {
        //        try
        //        {
        //            string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where MrdNo='" + mrdNo + "'";
        //            conn.Open();
        //            MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
        //            MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
        //            DataSet ds = new DataSet();
        //            mydata.Fill(ds);
        //            dt = ds.Tables[0];
        //            count = dt.Rows.Count;

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.Write(ex);
        //        }
        //        return count;
        //    }
        //}
        //#endregion


        #region getBoneArrowReportByMrdNo
        /// <summary>
        /// Table - bonemarrowaspiration
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [Route("api/Account/getBoneArrowReportByMrdNo")]
        [HttpGet]
        public List<BoneMarrowAspiration> getBoneArrowReportByMrdNo(string mrdNo)
        {
            List<BoneMarrowAspiration> lstboneMarrowAspiration = new List<BoneMarrowAspiration>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where id_MrdNumber='" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            BoneMarrowAspiration boneMarrowAspiration = new BoneMarrowAspiration();
                            boneMarrowAspiration.Id = (int)dr["Id"];
                            boneMarrowAspiration.id_MrdNumber = dr["id_MrdNumber"].ToString();
                            boneMarrowAspiration.SampleId_mrdnumber = dr["SampleId_mrdnumber"].ToString();
                            boneMarrowAspiration.ClinicalFindings = dr["ClinicalFindings"].ToString();
                            boneMarrowAspiration.PeripheralBloodFindings = dr["PeripheralBloodFindings"].ToString();
                            boneMarrowAspiration.BoneMarrowNumber = dr["BoneMarrowNumber"].ToString();
                            boneMarrowAspiration.Cellularity = dr["Cellularity"].ToString();
                            boneMarrowAspiration.Erythropoiesis = dr["Erythropoiesis"].ToString();
                            boneMarrowAspiration.Myelopoiesis = dr["Myelopoiesis"].ToString();
                            boneMarrowAspiration.M_E = dr["M_E"].ToString();
                            boneMarrowAspiration.Eosinophils = dr["Eosinophils"].ToString();
                            boneMarrowAspiration.Lymphocytes = dr["Lymphocytes"].ToString();
                            boneMarrowAspiration.PlasmaCells = dr["PlasmaCells"].ToString();
                            boneMarrowAspiration.Blasts = dr["Blasts"].ToString();
                            boneMarrowAspiration.Megakaryocytes = dr["Megakaryocytes"].ToString();
                            boneMarrowAspiration.Others = dr["Others"].ToString();
                            boneMarrowAspiration.Perl_sIronStain = dr["Perl_sIronStain"].ToString();
                            boneMarrowAspiration.Impression = dr["Impression"].ToString();
                            boneMarrowAspiration.Status = dr["Status"].ToString();
                            lstboneMarrowAspiration.Add(boneMarrowAspiration);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstboneMarrowAspiration;
            }
        }
        #endregion



        // Test Code by Test Name in Add Elements



        #region getTestCodebyTestName
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// get test code from childtestlist table by Test Name
        /// <returns></returns>
        [Route("api/Account/getTestCodebyTestName")]
        [HttpGet]
        public string getTestCodebyTestName(string testName)
        {

            string testCode = "";
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT TestCode FROM childtestlist where TestName = '" + testName + "' AND ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            testCode = dr["TestCode"].ToString();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return testCode;
            }
        }
        #endregion


        #region insertElements
        /// <summary>
        /// table - element 
        /// </summary>
        /// <param name="elementTable"></param>
        [Route("api/Account/insertElements")]
        [HttpPost]
        public void insertElements(Elements element)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DateTime today = DateTime.Today;
                    element.CreatedDate = today.ToShortDateString();
                    string strSQL = "INSERT INTO pathoclinic.elements(ElementName,ElementValue,TestCode,TestName,CreatedDate) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", element.ElementName);
                    cmd.Parameters.AddWithValue("@val2", element.ElementValue);
                    cmd.Parameters.AddWithValue("@val3", element.TestCode);
                    cmd.Parameters.AddWithValue("@val4", element.TestName);
                    cmd.Parameters.AddWithValue("@val5", element.CreatedDate);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion

        #region getAllElementsDetailsByTestCode
        /// <summary>
        /// Table - resultlabtest
        /// </summary>
        /// get pending amount values from resultlabtest table      
        /// <returns></returns>
        [Route("api/Account/getAllElementsDetailsByTestCode")]
        [HttpGet]
        public List<Elements> getAllElementsDetailsByTestCode(string testCode)
        {
            List<Elements> elementDetail = new List<Elements>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM elements where testCode='" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Elements elements = new Elements();
                            elements.TestCode = dr["TestCode"].ToString();
                            elements.TestName = dr["TestName"].ToString();
                            elements.ElementId = (int)dr["ElementId"];
                            elements.ElementName = dr["ElementName"].ToString();
                            elementDetail.Add(elements);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return elementDetail;
            }
        }
        #endregion


        #region getElementDetailbyElementName
        /// <summary>
        /// Table - elements
        /// </summary>
        /// get Elements details from elements table by Element Name
        /// <returns></returns>
        [Route("api/Account/getElementDetailbyElementName")]
        [HttpGet]
        public List<Elements> getElementDetailbyElementName(string elementName, string testCode)
        {



            List<Elements> elementDetail = new List<Elements>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM elements where ElementName = '" + elementName + "' and  TestCode = '" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Elements elements = new Elements();
                            elements.TestCode = dr["TestCode"].ToString();
                            elements.TestName = dr["TestName"].ToString();
                            elements.ElementId = (int)dr["ElementId"];
                            elementDetail.Add(elements);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return elementDetail;
            }
        }
        #endregion


        #region getElementDetailbyTestCode
        /// <summary>
        /// Table - elements
        /// </summary>
        /// get Elements details from elements table by Element Name
        /// <returns></returns>
        [Route("api/Account/getElementDetailbyTestCode")]
        [HttpGet]
        public List<Elements> getElementDetailbyTestCode(string testCode)
        {



            List<Elements> elementDetail = new List<Elements>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM elements where TestCode = '" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Elements elements = new Elements();
                            elements.ElementName = dr["ElementName"].ToString();
                            //    elements.TestName = dr["TestCode"].ToString();
                            elements.TestCode = dr["TestCode"].ToString();
                            elements.TestName = dr["TestName"].ToString();
                            elements.ElementId = (int)dr["ElementId"];
                            elementDetail.Add(elements);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return elementDetail;
            }
        }
        #endregion




        #region getAllElementsDetails 
        /// <summary>
        /// Table - elements
        /// </summary>
        /// get Elements details from elements table by Element Name
        /// <returns></returns>
        [Route("api/Account/getAllElementsDetails")]
        [HttpGet]
        public List<Elements> getAllElementsDetails()
        {

            List<Elements> elementDetail = new List<Elements>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM elements";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Elements elements = new Elements();
                            elements.TestCode = dr["TestCode"].ToString();
                            elements.TestName = dr["TestName"].ToString();
                            elements.ElementId = (int)dr["ElementId"];
                            elements.ElementName = dr["ElementName"].ToString();
                            elementDetail.Add(elements);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return elementDetail;
            }
        }
        #endregion



        #region insertTemplates
        /// <summary>
        /// table - templates
        /// </summary>
        /// <param name="templates"></param>
        [Route("api/Account/insertTemplates")]
        [HttpPost]
        public void insertTemplates(Templates templates)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.templates(TemplateName,TemplateValue,ElementId,ElementName,TestCode,TestName) VALUES(@val1,@val2,@val3,@val4,@val5,@val6)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", templates.TemplateName);
                    cmd.Parameters.AddWithValue("@val2", templates.TemplateValue);
                    cmd.Parameters.AddWithValue("@val3", templates.ElementId);
                    cmd.Parameters.AddWithValue("@val4", templates.ElementName);
                    cmd.Parameters.AddWithValue("@val5", templates.TestCode);
                    cmd.Parameters.AddWithValue("@val6", templates.TestName);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion


        #region getTemplateDetailbyTestCodeElementID
        /// <summary>
        /// Table - elements
        /// </summary>
        /// get Elements details from elements table by Element Name
        /// <returns></returns>
        [Route("api/Account/getTemplateDetailbyTestCodeElementID")]
        [HttpGet]
        public List<Templates> getTemplateDetailbyTestCodeElementID(string TestCode, int ElementId)
        {

            //     testCode = "TST003";

            List<Templates> templatesDetail = new List<Templates>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM templates where TestCode = '" + TestCode + "' && ElementId ='" + ElementId + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Templates templateas = new Templates();
                            templateas.ElementName = dr["ElementName"].ToString();
                            //    elements.TestName = dr["TestCode"].ToString();
                            templateas.TestCode = dr["TestCode"].ToString();
                            templateas.TestName = dr["TestName"].ToString();
                            templateas.ElementId = (int)dr["ElementId"];
                            templateas.TemplateName = dr["TemplateName"].ToString();
                            templateas.TemplateId = (int)dr["TemplateId"];
                            templateas.TemplateValue = dr["TemplateValue"].ToString();

                            templatesDetail.Add(templateas);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return templatesDetail;
            }
        }
        #endregion



        #region insertBoneMarrowNew
        /// <summary>
        /// Table - insert BoneMarrowAspiration
        /// </summary>
        /// <param name="insuranceprofilelist"></param>
        [Route("api/Account/insertBoneMarrowNew")]
        [HttpPost]
        public void insertBoneMarrowNew(BoneMarrowAspirationNew boneMarrowAspirationNew)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where MrdNo='" + boneMarrowAspirationNew.MrdNo + "' and TestCode = '" + boneMarrowAspirationNew.TestCode + "' and ElementId ='" + boneMarrowAspirationNew.ElementId + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        string strSQL1 = "INSERT INTO pathoclinic.bonemarrowaspiration(MrdNo,TestCode,TestName,ElementId,ElementName,TemplateId,TemplateName,TemplateDescription) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8)";
                        // conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.Parameters.AddWithValue("@val1", boneMarrowAspirationNew.MrdNo);
                        cmd1.Parameters.AddWithValue("@val2", boneMarrowAspirationNew.TestCode);
                        cmd1.Parameters.AddWithValue("@val3", boneMarrowAspirationNew.TestName);
                        cmd1.Parameters.AddWithValue("@val4", boneMarrowAspirationNew.ElementId);
                        cmd1.Parameters.AddWithValue("@val5", boneMarrowAspirationNew.ElementName);
                        cmd1.Parameters.AddWithValue("@val6", boneMarrowAspirationNew.TemplateId);
                        cmd1.Parameters.AddWithValue("@val7", boneMarrowAspirationNew.TemplateName);
                        cmd1.Parameters.AddWithValue("@val8", boneMarrowAspirationNew.TemplateDescription);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr != null)
                            {
                                string strSQL1 = "UPDATE bonemarrowaspiration SET TemplateDescription='" + boneMarrowAspirationNew.TemplateDescription + "' where MrdNo='" + boneMarrowAspirationNew.MrdNo + "' and TestCode='" + boneMarrowAspirationNew.TestCode + "' and ElementId='" + boneMarrowAspirationNew.ElementId + "' ";
                                // conn.Open();
                                MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                                cmd1.ExecuteNonQuery();
                            }
                        }
                    }



                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getPatientHistoryByRegId
        /// <summary>
        /// Table - elements
        /// </summary>
        /// get Elements details from elements table by Element Name
        /// <returns></returns>
        [Route("api/Account/getPatientHistoryByRegId")]
        [HttpGet]
        public List<LabOrder> getPatientHistoryByRegId(string regID)
        {
            regID = regID.Substring(2, 3);
            int regIDValue = Convert.ToInt32(regID);


            List<LabOrder> labHistory = new List<LabOrder>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM LabOrder where RegID = '" + regIDValue + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrder labOrder = new LabOrder();
                            labOrder.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                            labOrder.MrdNo = dr["MrdNo"].ToString();
                            labHistory.Add(labOrder);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return labHistory;
            }
        }
        #endregion

        #region insertParentPaymentReceived
        /// <summary>
        /// table - parentpaymentreceived
        /// </summary>
        /// <param name="parentpaymentreceived"></param>
        [Route("api/Account/insertParentPaymentReceived")]
        [HttpPost]
        public void insertParentPaymentReceived(ParentPaymentReceived parentpaymentreceived)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strDate = parentpaymentreceived.strPaymentSchedule;

                    DateTime date = DateTime.ParseExact(strDate, "dd/MM/yyyy", null);
                    parentpaymentreceived.datePaymentSchedule = date;

                    string strSQL = "INSERT INTO pathoclinic.parentpaymentreceived(InvoiceNo,InvoiceDate,CreditInvoice,GroupName,Amount,ReceivedPayment,PendingAmount,Status,PendingNotification,PaymentSchedule) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", parentpaymentreceived.InvoiceNo);
                    cmd.Parameters.AddWithValue("@val2", parentpaymentreceived.InvoiceDate);
                    cmd.Parameters.AddWithValue("@val3", parentpaymentreceived.CreditInvoice);
                    cmd.Parameters.AddWithValue("@val4", parentpaymentreceived.GroupName);
                    cmd.Parameters.AddWithValue("@val5", parentpaymentreceived.Amount);
                    cmd.Parameters.AddWithValue("@val6", parentpaymentreceived.ReceivedPayment);
                    cmd.Parameters.AddWithValue("@val7", parentpaymentreceived.PendingAmount);
                    cmd.Parameters.AddWithValue("@val8", parentpaymentreceived.Status);
                    cmd.Parameters.AddWithValue("@val9", parentpaymentreceived.PendingNotification);
                    cmd.Parameters.AddWithValue("@val10", parentpaymentreceived.datePaymentSchedule);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion

        #region getAllfromParentPaymentReceived
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getAllfromParentPaymentReceived")]
        [HttpGet]
        public ParentPaymentReceived getAllfromParentPaymentReceived(string groupname)
        {
            ParentPaymentReceived paymentReceived = new ParentPaymentReceived();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.parentpaymentreceived where GroupName='" + groupname + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            paymentReceived.InvoiceId = (int)dr["InvoiceId"];
                            paymentReceived.InvoiceNo = dr["InvoiceNo"].ToString();
                            paymentReceived.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                            paymentReceived.CreditInvoice = dr["CreditInvoice"].ToString();
                            paymentReceived.GroupName = dr["GroupName"].ToString();
                            paymentReceived.Amount = (double)dr["Amount"];
                            paymentReceived.ReceivedPayment = (double)dr["ReceivedPayment"];
                            paymentReceived.PendingAmount = (double)dr["PendingAmount"];
                            paymentReceived.Status = dr["Status"].ToString();
                            paymentReceived.PendingNotification = dr["PendingNotification"].ToString();
                            paymentReceived.datePaymentSchedule = Convert.ToDateTime(dr["PaymentSchedule"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return paymentReceived;
            }
        }
        #endregion

        #region insertLabProfileOrTestList
        /// <summary>
        /// Table 
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/insertLabProfileOrTestList")]
        [HttpPost]
        public void insertLabProfileOrTestList(CommonProfileTestDetails labProfileTestList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO commonprofiletestdetails(RegID,Description,Amount,MrdNo,PatientName,Code,NetAmount,Discount) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", labProfileTestList.RegID);
                    cmd.Parameters.AddWithValue("@val2", labProfileTestList.Description);
                    cmd.Parameters.AddWithValue("@val3", labProfileTestList.Amount);
                    cmd.Parameters.AddWithValue("@val4", labProfileTestList.MrdNo);
                    cmd.Parameters.AddWithValue("@val5", labProfileTestList.PatientName);
                    cmd.Parameters.AddWithValue("@val6", labProfileTestList.Code);
                    cmd.Parameters.AddWithValue("@val7", labProfileTestList.NetAmount);
                    cmd.Parameters.AddWithValue("@val8", labProfileTestList.Discount);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getProfileTestListByMrdNoLab
        /// <summary>
        /// Table - CommonProfileTestDetails
        /// </summary>
        /// Listed all values from CommonProfileTestDetails table
        /// <returns></returns>
        [Route("api/Account/getProfileTestListByMrdNoLab")]
        [HttpGet]
        public List<CommonProfileTestDetails> getProfileTestListByMrdNoLab(string mrdNo)
        {
            List<CommonProfileTestDetails> lisLabProfileTestDetails = new List<CommonProfileTestDetails>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "SELECT * FROM  pathoclinic.commonprofiletestdetails where MrdNo = '" + mrdNo + "'";


                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            CommonProfileTestDetails labProfileTestList = new CommonProfileTestDetails();

                            labProfileTestList.RegID = (int)dr["RegID"];
                            labProfileTestList.Description = dr["Description"].ToString();
                            labProfileTestList.Amount = Convert.ToDouble(dr["Amount"]);
                            labProfileTestList.PatientName = dr["PatientName"].ToString();
                            labProfileTestList.Code = dr["Code"].ToString();
                            labProfileTestList.MrdNo = mrdNo;
                            labProfileTestList.Discount = dr["Discount"].ToString();
                            labProfileTestList.NetAmount = dr["NetAmount"].ToString();
                            lisLabProfileTestDetails.Add(labProfileTestList);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lisLabProfileTestDetails;
            }
        }
        #endregion


        #region getProfileNameFromAdmin
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getProfileNameFromAdmin")]
        [HttpGet]
        public List<GroupProfileList> getProfileNameFromAdmin(string groupname)
        {
            List<GroupProfileList> getprofileDetails = new List<GroupProfileList>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "SELECT * FROM pathoclinic.groupprofilelist where GroupName = '" + groupname + "'";


                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            GroupProfileList ProfileList = new GroupProfileList();


                            ProfileList.ProfileName = dr["ProfileName"].ToString();
                            ProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            getprofileDetails.Add(ProfileList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return getprofileDetails;
            }
        }
        #endregion


        #region getTestNameFromAdmin
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getTestNameFromAdmin")]
        [HttpGet]
        public List<GroupTestList> getTestNameFromAdmin(string groupname)
        {
            List<GroupTestList> lisTestDetails = new List<GroupTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "SELECT distinct TestName, TestCode FROM grouptestlist where GroupName = '" + groupname + "' UNION ALL SELECT distinct  ProfileName, ProfileCode FROM groupprofilelist  where GroupName = '" + groupname + "'";


                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            GroupTestList TestList = new GroupTestList();
                            if (dr["TestName"].ToString() != null)
                            {
                                TestList.TestCode = dr["TestCode"].ToString();
                                TestList.TestName = dr["TestName"].ToString();
                            }
                            lisTestDetails.Add(TestList);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lisTestDetails;
            }
        }
        #endregion


        #region getProfileNameFromAdminForInsurance
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getProfileNameFromAdminForInsurance")]
        [HttpGet]
        public List<InsuranceProfileList> getProfileNameFromAdminForInsurance(string insurancename)
        {
            List<InsuranceProfileList> getprofileDetails = new List<InsuranceProfileList>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "SELECT * FROM pathoclinic.insuranceprofilelist where InsuranceName = '" + insurancename + "'";


                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InsuranceProfileList ProfileList = new InsuranceProfileList();


                            ProfileList.ProfileName = dr["ProfileName"].ToString();
                            ProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            getprofileDetails.Add(ProfileList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return getprofileDetails;
            }
        }
        #endregion


        #region getTestNameFromAdminforinsurance
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getTestNameFromAdminforinsurance")]
        [HttpGet]
        public List<InsuranceTestList> getTestNameFromAdminforinsurance(string insurancename)
        {
            List<InsuranceTestList> lisTestDetails = new List<InsuranceTestList>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "SELECT distinct TestName, TestCode FROM insurancetestlist where InsuranceName = '" + insurancename + "' UNION ALL SELECT distinct  ProfileName, ProfileCode FROM insuranceprofilelist where InsuranceName = '" + insurancename + "'";


                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InsuranceTestList TestList = new InsuranceTestList();
                            if (dr["TestName"].ToString() != null)
                            {
                                TestList.TestCode = dr["TestCode"].ToString();
                                TestList.TestName = dr["TestName"].ToString();
                                lisTestDetails.Add(TestList);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lisTestDetails;
            }
        }
        #endregion



        #region getTestNameFromAdminforHospital
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getTestNameFromAdminforHospital")]
        [HttpGet]
        public List<InsuranceTestList> getTestNameFromAdminforHospital(string hospitalname)
        {
            List<InsuranceTestList> lisTestDetails = new List<InsuranceTestList>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "SELECT distinct TestName, TestCode FROM outofhospitaltestlist where HospitalName = '" + hospitalname + "' UNION ALL SELECT distinct  ProfileName, ProfileCode FROM outofhospitalprofilelist  where HospitalName = '" + hospitalname + "'";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            InsuranceTestList TestList = new InsuranceTestList();
                            if (dr["TestName"].ToString() != null)
                            {
                                TestList.TestCode = dr["TestCode"].ToString();
                                TestList.TestName = dr["TestName"].ToString();
                                lisTestDetails.Add(TestList);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lisTestDetails;
            }
        }
        #endregion

        #region insertParentPaymentInsurance
        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/insertParentPaymentInsurance")]
        [HttpPost]
        public void insertParentPaymentInsurance(ParentPaymentInsurance parentpaymentreceived)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strDate = parentpaymentreceived.strPaymentSchedule;
                    DateTime date = DateTime.ParseExact(strDate, "dd/MM/yyyy", null);
                    parentpaymentreceived.datePaymentSchedule = date;
                    string strSQL = "INSERT INTO pathoclinic.parentpaymentinsurance(InvoiceNo,InvoiceDate,CreditInvoice,InsuranceName,Amount,ReceivedPayment,PendingAmount,Status,PendingNotification,PaymentSchedule) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", parentpaymentreceived.InvoiceNo);
                    cmd.Parameters.AddWithValue("@val2", parentpaymentreceived.InvoiceDate);
                    cmd.Parameters.AddWithValue("@val3", parentpaymentreceived.CreditInvoice);
                    cmd.Parameters.AddWithValue("@val4", parentpaymentreceived.InsuranceName);
                    cmd.Parameters.AddWithValue("@val5", parentpaymentreceived.Amount);
                    cmd.Parameters.AddWithValue("@val6", parentpaymentreceived.ReceivedPayment);
                    cmd.Parameters.AddWithValue("@val7", parentpaymentreceived.PendingAmount);
                    cmd.Parameters.AddWithValue("@val8", parentpaymentreceived.Status);
                    cmd.Parameters.AddWithValue("@val9", parentpaymentreceived.PendingNotification);
                    cmd.Parameters.AddWithValue("@val10", parentpaymentreceived.datePaymentSchedule);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion


        #region insertParentPaymentHospital
        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/insertParentPaymentHospital")]
        [HttpPost]
        public void insertParentPaymentHospital(ParentPaymentHospital parentpaymentreceived)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strDate = parentpaymentreceived.strPaymentSchedule;

                    DateTime date = DateTime.ParseExact(strDate, "dd/MM/yyyy", null);
                    parentpaymentreceived.datePaymentSchedule = date;

                    string strSQL = "INSERT INTO pathoclinic.parentpaymentHospital(InvoiceNo,InvoiceDate,CreditInvoice,HospitalName,Amount,ReceivedPayment,PendingAmount,Status,PendingNotification,PaymentSchedule) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", parentpaymentreceived.InvoiceNo);
                    cmd.Parameters.AddWithValue("@val2", parentpaymentreceived.InvoiceDate);
                    cmd.Parameters.AddWithValue("@val3", parentpaymentreceived.CreditInvoice);
                    cmd.Parameters.AddWithValue("@val4", parentpaymentreceived.HospitalName);
                    cmd.Parameters.AddWithValue("@val5", parentpaymentreceived.Amount);
                    cmd.Parameters.AddWithValue("@val6", parentpaymentreceived.ReceivedPayment);
                    cmd.Parameters.AddWithValue("@val7", parentpaymentreceived.PendingAmount);
                    cmd.Parameters.AddWithValue("@val8", parentpaymentreceived.Status);
                    cmd.Parameters.AddWithValue("@val9", parentpaymentreceived.PendingNotification);
                    cmd.Parameters.AddWithValue("@val10", parentpaymentreceived.datePaymentSchedule);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion

        #region insertTestMultipleComponents
        /// <summary>
        /// Table - insert testMultipleComponents
        /// </summary>
        /// <param name="testMultipleComponents"></param>
        [Route("api/Account/insertTestMultipleComponents")]
        [HttpPost]
        public void insertTestMultipleComponents(TestMultipleComponents testMultipleComponents)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.TestMultipleComponents(TestCode,ElementName,Color,Units,Comments,CriticalLow,CriticalHigh,ReferenceLow,ReferenceHigh,CommentsType,Methodology,PriorityStatus) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", testMultipleComponents.TestCode);
                    cmd.Parameters.AddWithValue("@val2", testMultipleComponents.ElementName);
                    cmd.Parameters.AddWithValue("@val3", testMultipleComponents.Color);
                    cmd.Parameters.AddWithValue("@val4", testMultipleComponents.Units);
                    cmd.Parameters.AddWithValue("@val5", testMultipleComponents.Comments);
                    cmd.Parameters.AddWithValue("@val6", testMultipleComponents.CriticalLow);
                    cmd.Parameters.AddWithValue("@val7", testMultipleComponents.CriticalHigh);
                    cmd.Parameters.AddWithValue("@val8", testMultipleComponents.ReferenceLow);
                    cmd.Parameters.AddWithValue("@val9", testMultipleComponents.ReferenceHigh);
                    cmd.Parameters.AddWithValue("@val10", testMultipleComponents.CommentsType);
                    cmd.Parameters.AddWithValue("@val11", testMultipleComponents.Methodology);
                    cmd.Parameters.AddWithValue("@val12", 0);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region getAllAgeRefferencesByTestCode
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// Listed all values from LabTestList,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getAllAgeRefferencesByTestCode")]
        [HttpGet]
        public List<Agewisereferencevalue> getAllAgeRefferencesByTestCode(string testCode, string dob, string age, string sex)
        {
            List<Agewisereferencevalue> allReferenceRanges = new List<Agewisereferencevalue>();
            DataTable dt = new DataTable();
            DataTable dtDefault = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "";

                    strSQL = "SELECT * FROM agewisereferencevalue where StartYear <= 0  and EndYear >= 200 and TestCode = '" + testCode + "'";
                    conn.Open();

                    MySqlDataAdapter mydataDefault = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmdDefault = new MySqlCommandBuilder(mydataDefault);
                    DataSet dsDefault = new DataSet();
                    mydataDefault.Fill(dsDefault);
                    dtDefault = dsDefault.Tables[0];

                    if (dtDefault.Rows.Count == 0)
                    {
                        if (Convert.ToInt32(age) >= 1)
                        {
                            strSQL = "SELECT * FROM agewisereferencevalue where StartYear <= '" + Convert.ToInt32(age) + "'  and EndYear >= '" + Convert.ToInt32(age) + "' and TestCode = '" + testCode + "'";

                        }
                        else
                        {
                            DateTime dateOfBirth = Convert.ToDateTime(dob);
                            DateTime todayDate = DateTime.Now;
                            double daysDifferent = (todayDate - dateOfBirth).TotalDays;
                            if (daysDifferent >= 31)
                            {
                                double monthDifferent = daysDifferent / 31;
                                strSQL = "SELECT * FROM agewisereferencevalue where StartMonth <= '" + Convert.ToInt32(monthDifferent) + "'  and EndMonth >= '" + Convert.ToInt32(monthDifferent) + "' and TestCode = '" + testCode + "'";

                            }

                            else
                            {

                                strSQL = "SELECT * FROM agewisereferencevalue where StartDay <= '" + Convert.ToInt32(daysDifferent) + "'  and EndDay >= '" + Convert.ToInt32(daysDifferent) + "' and TestCode = '" + testCode + "'";
                            }
                        }


                        //conn.Open();
                        MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                        MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                        DataSet ds = new DataSet();
                        mydata.Fill(ds);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count != 1)
                        {

                            if (Convert.ToInt32(age) >= 1)
                            {
                                strSQL = "SELECT * FROM agewisereferencevalue where StartYear <= '" + Convert.ToInt32(age) + "'  and EndYear >= '" + Convert.ToInt32(age) + "' and TestCode = '" + testCode + "' and Sex='" + sex + "'";

                            }
                            else
                            {
                                DateTime dateOfBirth = Convert.ToDateTime(dob);
                                DateTime todayDate = DateTime.Now;
                                double daysDifferent = (todayDate - dateOfBirth).TotalDays;
                                if (daysDifferent >= 30)
                                {
                                    double monthDifferent = daysDifferent / 30;
                                    strSQL = "SELECT * FROM agewisereferencevalue where StartMonth <= '" + Convert.ToInt32(monthDifferent) + "'  and EndMonth >= '" + Convert.ToInt32(monthDifferent) + "' and TestCode = '" + testCode + "' and Sex='" + sex + "'";

                                }

                                else
                                {

                                    strSQL = "SELECT * FROM agewisereferencevalue where StartDay <= '" + Convert.ToInt32(daysDifferent) + "'  and EndDay >= '" + Convert.ToInt32(daysDifferent) + "' and TestCode = '" + testCode + "'and Sex='" + sex + "'";
                                }
                            }
                            DataTable dta = new DataTable();
                            MySqlDataAdapter mydataa = new MySqlDataAdapter(strSQL, conn);
                            MySqlCommandBuilder cmda = new MySqlCommandBuilder(mydataa);
                            DataSet dsa = new DataSet();
                            mydataa.Fill(dsa);
                            dta = dsa.Tables[0];
                            foreach (DataRow dra in dta.Rows)
                            {
                                if (dra != null)
                                {
                                    Agewisereferencevalue agewiseReferenceValue1 = new Agewisereferencevalue();

                                    agewiseReferenceValue1.LowUpperReferenceValue = dra["LowUpperReferenceValue"].ToString();
                                    agewiseReferenceValue1.DisplayText = dra["DisplayValue"].ToString();
                                    agewiseReferenceValue1.DisplayText = dra["DisplayValue"].ToString();
                                    agewiseReferenceValue1.StartYear = dra["StartYear"].ToString();
                                    agewiseReferenceValue1.EndYear = dra["EndYear"].ToString();
                                    agewiseReferenceValue1.StartMonth = dra["StartMonth"].ToString();
                                    agewiseReferenceValue1.EndMonth = dra["EndMonth"].ToString();
                                    agewiseReferenceValue1.StartDay = dra["StartDay"].ToString();
                                    agewiseReferenceValue1.EndDay = dra["EndDay"].ToString();
                                    agewiseReferenceValue1.FreeText = dra["FreeText"].ToString();

                                    if (agewiseReferenceValue1.StartYear != null && agewiseReferenceValue1.EndYear != null)
                                    {

                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartYear + "-" + agewiseReferenceValue1.EndYear;
                                    }
                                    else if (agewiseReferenceValue1.StartDay == null && agewiseReferenceValue1.EndDay == null)
                                    {
                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartDay + "-" + agewiseReferenceValue1.EndDay;
                                    }
                                    else
                                    {
                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartMonth + "-" + agewiseReferenceValue1.EndMonth;
                                    }
                                    allReferenceRanges.Clear();
                                    allReferenceRanges.Add(agewiseReferenceValue1);

                                }
                            }
                        }



                        else if (dt.Rows.Count == 1)
                        {
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Agewisereferencevalue agewiseReferenceValue = new Agewisereferencevalue();

                                    agewiseReferenceValue.LowUpperReferenceValue = dr["LowUpperReferenceValue"].ToString();
                                    agewiseReferenceValue.DisplayText = dr["DisplayValue"].ToString();
                                    agewiseReferenceValue.StartYear = dr["StartYear"].ToString();
                                    agewiseReferenceValue.EndYear = dr["EndYear"].ToString();
                                    agewiseReferenceValue.StartMonth = dr["StartMonth"].ToString();
                                    agewiseReferenceValue.EndMonth = dr["EndMonth"].ToString();
                                    agewiseReferenceValue.StartDay = dr["StartDay"].ToString();
                                    agewiseReferenceValue.EndDay = dr["EndDay"].ToString();
                                    agewiseReferenceValue.FreeText = dr["FreeText"].ToString();

                                    if (agewiseReferenceValue.StartYear != null && agewiseReferenceValue.EndYear != null)
                                    {

                                        agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartYear + "-" + agewiseReferenceValue.EndYear;
                                    }
                                    else if (agewiseReferenceValue.StartDay == null && agewiseReferenceValue.EndDay == null)
                                    {
                                        agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartDay + "-" + agewiseReferenceValue.EndDay;
                                    }
                                    else
                                    {
                                        agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartMonth + "-" + agewiseReferenceValue.EndMonth;
                                    }

                                    allReferenceRanges.Add(agewiseReferenceValue);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (dtDefault.Rows.Count == 1)
                        {

                            foreach (DataRow dr in dtDefault.Rows)
                            {
                                Agewisereferencevalue agewiseReferenceValue = new Agewisereferencevalue();

                                agewiseReferenceValue.LowUpperReferenceValue = dr["LowUpperReferenceValue"].ToString();
                                agewiseReferenceValue.DisplayText = dr["DisplayValue"].ToString();
                                agewiseReferenceValue.StartYear = dr["StartYear"].ToString();
                                agewiseReferenceValue.EndYear = dr["EndYear"].ToString();
                                agewiseReferenceValue.StartMonth = dr["StartMonth"].ToString();
                                agewiseReferenceValue.EndMonth = dr["EndMonth"].ToString();
                                agewiseReferenceValue.StartDay = dr["StartDay"].ToString();
                                agewiseReferenceValue.EndDay = dr["EndDay"].ToString();
                                agewiseReferenceValue.FreeText = dr["FreeText"].ToString();

                                if (agewiseReferenceValue.StartYear != null && agewiseReferenceValue.EndYear != null)
                                {

                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartYear + "-" + agewiseReferenceValue.EndYear;
                                }
                                else if (agewiseReferenceValue.StartDay == null && agewiseReferenceValue.EndDay == null)
                                {
                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartDay + "-" + agewiseReferenceValue.EndDay;
                                }
                                else
                                {
                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartMonth + "-" + agewiseReferenceValue.EndMonth;
                                }

                                allReferenceRanges.Add(agewiseReferenceValue);
                            }

                        }

                        else if (dtDefault.Rows.Count != 1)
                        {

                            strSQL = "SELECT * FROM agewisereferencevalue where StartDay <= 0  and EndDay >= 200 and TestCode = '" + testCode + "'and Sex='" + sex + "'";
                            DataTable dta = new DataTable();
                            MySqlDataAdapter mydataa = new MySqlDataAdapter(strSQL, conn);
                            MySqlCommandBuilder cmda = new MySqlCommandBuilder(mydataa);
                            DataSet dsa = new DataSet();
                            mydataa.Fill(dsa);
                            dta = dsa.Tables[0];
                            foreach (DataRow dra in dta.Rows)
                            {
                                if (dra != null)
                                {
                                    Agewisereferencevalue agewiseReferenceValue1 = new Agewisereferencevalue();

                                    agewiseReferenceValue1.LowUpperReferenceValue = dra["LowUpperReferenceValue"].ToString();
                                    agewiseReferenceValue1.DisplayText = dra["DisplayValue"].ToString();
                                    agewiseReferenceValue1.DisplayText = dra["DisplayValue"].ToString();
                                    agewiseReferenceValue1.StartYear = dra["StartYear"].ToString();
                                    agewiseReferenceValue1.EndYear = dra["EndYear"].ToString();
                                    agewiseReferenceValue1.StartMonth = dra["StartMonth"].ToString();
                                    agewiseReferenceValue1.EndMonth = dra["EndMonth"].ToString();
                                    agewiseReferenceValue1.StartDay = dra["StartDay"].ToString();
                                    agewiseReferenceValue1.EndDay = dra["EndDay"].ToString();
                                    agewiseReferenceValue1.FreeText = dra["FreeText"].ToString();
                                    if (agewiseReferenceValue1.StartYear != null && agewiseReferenceValue1.EndYear != null)
                                    {

                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartYear + "-" + agewiseReferenceValue1.EndYear;
                                    }
                                    else if (agewiseReferenceValue1.StartDay == null && agewiseReferenceValue1.EndDay == null)
                                    {
                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartDay + "-" + agewiseReferenceValue1.EndDay;
                                    }
                                    else
                                    {
                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartMonth + "-" + agewiseReferenceValue1.EndMonth;
                                    }
                                    allReferenceRanges.Clear();
                                    allReferenceRanges.Add(agewiseReferenceValue1);

                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return allReferenceRanges;
            }
        }
        #endregion


        #region getAllfromParentPaymentInsurance
        /// <summary>
        /// Table -
        /// </summary>
        ///    
        /// <returns></returns>
        [Route("api/Account/getAllfromParentPaymentInsurance")]
        [HttpGet]
        public ParentPaymentInsurance getAllfromParentPaymentInsurance(string Insurancename)
        {
            ParentPaymentInsurance paymentReceived = new ParentPaymentInsurance();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.parentpaymentinsurance where InsuranceName='" + Insurancename + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            paymentReceived.InvoiceId = (int)dr["InvoiceId"];
                            paymentReceived.InvoiceNo = dr["InvoiceNo"].ToString();
                            paymentReceived.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                            paymentReceived.CreditInvoice = dr["CreditInvoice"].ToString();
                            paymentReceived.InsuranceName = dr["InsuranceName"].ToString();
                            paymentReceived.Amount = (double)dr["Amount"];
                            paymentReceived.ReceivedPayment = (double)dr["ReceivedPayment"];
                            paymentReceived.PendingAmount = (double)dr["PendingAmount"];
                            paymentReceived.Status = dr["Status"].ToString();
                            paymentReceived.PendingNotification = dr["PendingNotification"].ToString();
                            paymentReceived.datePaymentSchedule = Convert.ToDateTime(dr["PaymentSchedule"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return paymentReceived;
            }
        }
        #endregion

        #region getAllfromParentPaymentHospital
        /// <summary>
        /// Table -
        /// </summary>
        ///    
        /// <returns></returns>
        [Route("api/Account/getAllfromParentPaymentHospital")]
        [HttpGet]
        public ParentPaymentHospital getAllfromParentPaymentHospital(string Hospitalname)
        {
            ParentPaymentHospital paymentReceived = new ParentPaymentHospital();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.parentpaymenthospital where HospitalName='" + Hospitalname + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            paymentReceived.InvoiceId = (int)dr["InvoiceId"];
                            paymentReceived.InvoiceNo = dr["InvoiceNo"].ToString();
                            paymentReceived.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                            paymentReceived.CreditInvoice = dr["CreditInvoice"].ToString();
                            paymentReceived.HospitalName = dr["HospitalName"].ToString();
                            paymentReceived.Amount = (double)dr["Amount"];
                            paymentReceived.ReceivedPayment = (double)dr["ReceivedPayment"];
                            paymentReceived.PendingAmount = (double)dr["PendingAmount"];
                            paymentReceived.Status = dr["Status"].ToString();
                            paymentReceived.PendingNotification = dr["PendingNotification"].ToString();
                            paymentReceived.datePaymentSchedule = Convert.ToDateTime(dr["PaymentSchedule"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return paymentReceived;
            }
        }
        #endregion

        #region updateRegistrationDetails
        /// <summary>
        /// Table - patientregistration
        /// Edit and Update patientregistration details using reg ID from patientregistration table.
        /// </summary>
        /// <param name="patientRegistration"></param>
        [Route("api/Account/updateRegistrationDetails")]
        [HttpPost]
        public void updateRegistrationDetails(PatientRegistration patientRegistration)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  pathoclinic.patientregistration SET FirstName='" + patientRegistration.FirstName + "',LastName='" + patientRegistration.LastName + "',PhoneNumber='" + patientRegistration.PhoneNumber + "',Sex= '" + patientRegistration.Sex + "'where RegID='" + patientRegistration.RegID + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region getReportByMrdNo
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportByMrdNo")]
        [HttpGet]
        public List<BoneMarrowAspirationNew> getReportByMrdNo(string mrdNo)
        {
            List<BoneMarrowAspirationNew> bonemarrowaspiration = new List<BoneMarrowAspirationNew>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where MrdNo='" + mrdNo + "' order by ElementId Asc";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            BoneMarrowAspirationNew bonemarrowaspirations = new BoneMarrowAspirationNew();
                            bonemarrowaspirations.MrdNo = dr["MrdNo"].ToString();
                            bonemarrowaspirations.TestCode = dr["TestCode"].ToString();
                            bonemarrowaspirations.TestName = dr["TestName"].ToString();
                            bonemarrowaspirations.ElementId = (int)dr["ElementId"];
                            bonemarrowaspirations.ElementName = dr["ElementName"].ToString();
                            bonemarrowaspirations.TemplateName = dr["TemplateName"].ToString();
                            bonemarrowaspirations.TemplateDescription = dr["TemplateDescription"].ToString();
                            bonemarrowaspiration.Add(bonemarrowaspirations);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return bonemarrowaspiration;
            }
        }
        #endregion


        #region getReportByMrdNoCommon
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportByMrdNoCommon")]
        [HttpGet]
        public List<BoneMarrowAspirationNew> getReportByMrdNoCommon(string mrdNo)
        {
            List<BoneMarrowAspirationNew> bonemarrowaspiration = new List<BoneMarrowAspirationNew>();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT Distinct(TestCode) FROM pathoclinic.bonemarrowaspiration where MrdNo='" + mrdNo + "'" ;
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            BoneMarrowAspirationNew bonemarrowaspirations = new BoneMarrowAspirationNew();
                          //  bonemarrowaspirations.MrdNo = dr["MrdNo"].ToString();
                            bonemarrowaspirations.TestCode = dr["TestCode"].ToString();
                            
                            //   bonemarrowaspirations.TestName = dr["TestName"].ToString();
                            string strSQL1 = "SELECT TestName FROM pathoclinic.bonemarrowaspiration where TestCode='" + bonemarrowaspirations.TestCode + "'";
                        
                            MySqlDataAdapter mydata1 = new MySqlDataAdapter(strSQL1, conn);
                            MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata1);
                            DataSet ds1 = new DataSet();
                            mydata1.Fill(ds1);
                            dt1 = ds1.Tables[0];
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                if (dr1 != null)
                                {
                                    bonemarrowaspirations.TestName = dr1["TestName"].ToString();
                                    bonemarrowaspirations.select = false;
                                  
                                }
                            }
                                    bonemarrowaspiration.Add(bonemarrowaspirations);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return bonemarrowaspiration;
            }
        }
        #endregion


        #region getReportByMrdNoTestCode
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportByMrdNoTestCode")]
        [HttpGet]
        public List<BoneMarrowAspirationNew> getReportByMrdNoTestCode(string mrdNo, string testCode)
        {
            List<BoneMarrowAspirationNew> bonemarrowaspiration = new List<BoneMarrowAspirationNew>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where MrdNo='" + mrdNo + "' and TestCode ='" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            BoneMarrowAspirationNew bonemarrowaspirations = new BoneMarrowAspirationNew();
                            bonemarrowaspirations.MrdNo = dr["MrdNo"].ToString();
                            bonemarrowaspirations.TestName = dr["TestName"].ToString();
                            bonemarrowaspirations.ElementId = Convert.ToInt32(dr["ElementId"]);
                            bonemarrowaspirations.ElementName = dr["ElementName"].ToString();
                            bonemarrowaspirations.TemplateName = dr["TemplateName"].ToString();
                            bonemarrowaspirations.TemplateDescription = dr["TemplateDescription"].ToString();
                            bonemarrowaspiration.Add(bonemarrowaspirations);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return bonemarrowaspiration;
            }
        }
        #endregion


        #region getReportLabTestResultByMrdNo
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportLabTestResultByMrdNo")]
        [HttpGet]
        public List<ResultLabTech> getReportLabTestResultByMrdNo(string mrdNo)
        {
            List<ResultLabTech> lstResultLabTech = new List<ResultLabTech>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string crictical = "";
                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.MrdNo = dr["MrdNo"].ToString();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.TestCode = dr["TestCode"].ToString();
                            resultLabTech.NormalValues = dr["DefaultValues"].ToString();
                            resultLabTech.CriticalValue = dr["CriticalValue"].ToString();
                            resultLabTech.Units = dr["Units"].ToString();
                            resultLabTech.Comment = dr["Comment1"].ToString();
                            resultLabTech.Comment2 = dr["Comment2"].ToString();
                            resultLabTech.SpecialComments = dr["SpecialComments"].ToString();
                            resultLabTech.EndRange = dr["EndRange"].ToString();
                            resultLabTech.StartRange = dr["StartRange"].ToString();
                            resultLabTech.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            resultLabTech.Methodology = dr["Methodology"].ToString();
                            resultLabTech.CriticalValue = dr["CriticalValue"].ToString();
                            resultLabTech.DisplayValueText = dr["DisplayValueText"].ToString();
                            resultLabTech.SampleContainer = dr["SampleContainer"].ToString();
                            resultLabTech.FreeText = dr["FreeText"].ToString();
                            resultLabTech.NormalValuesFiled = dr["NormalValuesFiled"].ToString();
                            resultLabTech.Status = (int) dr["Status"] ;
                            if (resultLabTech.CriticalValue != null) {
                                string[] criticalValues = resultLabTech.CriticalValue.Split('-');
                                double low = Convert.ToDouble(criticalValues[0]);
                                double high = Convert.ToDouble(criticalValues[1]);
                                double result = Convert.ToDouble(resultLabTech.Result);
                                if (result >= low && result <= high)
                                {
                                    crictical = "true";
                                }
                                else {
                                    crictical = "false";
                                }
                            }

                            resultLabTech.CricticalResult = crictical;

                            lstResultLabTech.Add(resultLabTech);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion


        #region getReportLabTestResultByTestCodeMrdNo
        /// <summary>
        /// Table - resultlabtest
        /// </summary>
        /// get   values from resultlabtest table      
        /// <returns></returns>
        [Route("api/Account/getReportLabTestResultByTestCodeMrdNo")]
        [HttpGet]
        public List<ResultLabTech> getReportLabTestResultByTestCodeMrdNo(string mrdNo, string testCode)
        {
            List<ResultLabTech> lstResultLabTech = new List<ResultLabTech>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest where MrdNo='" + mrdNo + "' and TestCode ='" + testCode + "'";

                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.MrdNo = dr["MrdNo"].ToString();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.TestCode = dr["TestCode"].ToString();
                            resultLabTech.NormalValues = dr["DefaultValues"].ToString();
                            resultLabTech.CriticalValue = dr["CriticalValue"].ToString();
                            resultLabTech.Units = dr["Units"].ToString();
                            resultLabTech.Comment = dr["Comment1"].ToString();
                            resultLabTech.Comment2 = dr["Comment2"].ToString();
                            resultLabTech.SpecialComments = dr["SpecialComments"].ToString();
                            resultLabTech.EndRange = dr["EndRange"].ToString();
                            resultLabTech.StartRange = dr["StartRange"].ToString();
                            resultLabTech.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            resultLabTech.Methodology = dr["Methodology"].ToString();
                            resultLabTech.CriticalValue = dr["CriticalValue"].ToString();
                            resultLabTech.DisplayValueText = dr["DisplayValueText"].ToString();
                            resultLabTech.SampleContainer = dr["SampleContainer"].ToString();
                            lstResultLabTech.Add(resultLabTech);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion

        #region getReportLabTestResultByTestCode
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportLabTestResultByTestCode")]
        [HttpGet]
        public List<ChildTestList> getReportLabTestResultByTestCode(string testcode)
        {
            List<ChildTestList> lstResultLabTech = new List<ChildTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.childtestlist where TestCode='" + testcode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            ChildTestList resultLabTech = new ChildTestList();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.Outsourced = dr["Outsourced"].ToString();
                            resultLabTech.Methodology = dr["Methodology"].ToString();
                            resultLabTech.AgewiseCriticalValue = dr["AgewiseCriticalValue"].ToString();
                            lstResultLabTech.Add(resultLabTech);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion

        #region insertbarCode
        /// <summary>
        /// Table - CommanSampleContainer
        /// </summary>
        /// Inserted the CommanSampleContainer table values.
        /// <param name="commonSampleContainer"></param>
        [Route("api/Account/insertbarCode")]
        [HttpPost]
        public void insertbarCode(BarCode barcode)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO pathoclinic.barcode(RegID,PatientName,MrdNo,BarCodeKey) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", barcode.RegID);
                    cmd.Parameters.AddWithValue("@val2", barcode.PatientName);
                    cmd.Parameters.AddWithValue("@val3", barcode.MrdNo);
                    cmd.Parameters.AddWithValue("@val4", barcode.BarCodeKey);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion
        #region insertRateCard
        /// <summary>
        /// Table -  RateCard
        /// Inserted the RateCard table values.
        /// </summary>
        /// <param name="RateCard"></param>
        [Route("api/Account/insertRateCard")]
        [HttpPost]
        public void insertRateCard(RateCard rateCard)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO RateCard(ProfileCode,ProfileName,TestCode,TestName,Amount,StartDate,EndDate,CreatorEmail,CreatorPhoneNo) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", rateCard.ProfileCode);
                    cmd.Parameters.AddWithValue("@val2", rateCard.ProfileName);
                    cmd.Parameters.AddWithValue("@val3", rateCard.TestCode);
                    cmd.Parameters.AddWithValue("@val4", rateCard.TestName);
                    cmd.Parameters.AddWithValue("@val5", rateCard.Amount);
                    cmd.Parameters.AddWithValue("@val6", rateCard.StartDate);
                    cmd.Parameters.AddWithValue("@val7", rateCard.EndDate);
                    cmd.Parameters.AddWithValue("@val8", rateCard.CreatorEmail);
                    cmd.Parameters.AddWithValue("@val9", rateCard.CreatorPhoneNo);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region updateLabStatusInprogress 
        /// <summary>
        /// Table - LaborderStatus
        ///updated the lab order status as 'Inprogress'
        /// </summary>
        /// <param name="LaborderStatus"></param>
        [Route("api/Account/updateLabStatusInprogress")]
        [HttpPost]
        public void updateLabStatusInprogress(string mrdNo)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE LaborderStatus SET LabStatus='Inprogress' where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region updateLabStatusApproved 
        /// <summary>
        /// Table - LaborderStatus
        ///updated the lab order status as 'Approved'
        /// </summary>
        /// <param name="LaborderStatus"></param>
        [Route("api/Account/updateLabStatusApproved")]
        [HttpPost]
        public void updateLabStatusApproved(string mrdNo)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE LaborderStatus SET LabStatus='Approved' where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion




        #region updateReport 
        /// <summary>
        /// Table - ResultLabTech
        ///updated the lab order status as 'Approved'
        /// </summary>
        /// <param name="Report"></param>
        [Route("api/Account/updateReport")]
        [HttpPost]
        //   public void updateReport(string mrdNo)
        //  public void updateReport(string mrdNo, string testCode, string comments, string result)
        public void updateReport(string test1, string test2, string test3, string test4)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    {
                        string strSQL = "SELECT * FROM pathoclinic.resultlabtest Where MrdNo='" + test1 + "' && TestCode ='" + test2 + "'";
                        conn.Open();
                        MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                        MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                        DataSet ds = new DataSet();
                        mydata.Fill(ds);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            string strSQL1 = "UPDATE resultlabtest SET Result = @Result, Comment1 = @Comment Where MrdNo='" + test1 + "' && TestCode ='" + test2 + "'";

                            MySqlCommand cmdedit = new MySqlCommand(strSQL1, conn);

                            cmdedit.Parameters.AddWithValue("@Result", test3);
                            cmdedit.Parameters.AddWithValue("@Comment", test4);

                            cmdedit.CommandType = CommandType.Text;


                            cmdedit.ExecuteNonQuery();

                        }
                    }
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion

        #region getLabOrderByMrdNo
        /// <summary>
        /// Table - LabOrder
        /// </summary>
        /// get details from LabOrder table      
        /// <returns></returns>
        [Route("api/Account/getLabOrderByMrdNo")]
        [HttpGet]
        public List<LabOrder> getLabOrderByMrdNo(string mrdNo)
        {
            List<LabOrder> lstlabOrder = new List<LabOrder>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT Age,Sex,HomeCollectChargeAmount,CollectAt,ReferredBy,PatientName FROM pathoclinic.LabOrder lo inner join   pathoclinic.patientregistration pr on  pr.RegID= lo.RegID  where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrder labOrder = new LabOrder();
                            //    labOrder.MrdNo = dr["MrdNo"].ToString();
                            labOrder.PatientName = dr["PatientName"].ToString();
                            labOrder.ReferredBy = dr["ReferredBy"].ToString();
                            labOrder.CollectAt = dr["CollectAt"].ToString();
                            labOrder.HomeCollectChargeAmount = (double)dr["HomeCollectChargeAmount"];
                            labOrder.Age = (int)dr["Age"];
                            labOrder.Sex = dr["Sex"].ToString();

                            lstlabOrder.Add(labOrder);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstlabOrder;
            }
        }
        #endregion

        #region updateDisbleStatusChildTestList 
        /// <summary>
        /// Table - childtestlist
        /// updated the childtestlist active status as 0 for disable
        /// </summary>
        /// <param name="TestID"></param>
        /// <param name="activeStatus"></param>

        [Route("api/Account/updateDisbleStatusChildTestList")]
        [HttpPost]
        public void updateDisbleStatusChildTestList(string TestID, int activeStatus)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE pathoclinic.childtestlist SET ActiveStatus='" + activeStatus + "'where TestID='" + TestID + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion

        #region updateChildTestList 
        /// <summary>
        /// Table - childtestlist
        /// updated the childtestlist details 
        /// </summary>       
        /// <param name="activeStatus"></param>

        [Route("api/Account/updateChildTestList")]
        [HttpPost]
        public void updateChildTestList(ChildTestList childTestList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE pathoclinic.childtestlist SET TestName=@TestName,DisplayTestName=@DisplayName,DepartmentName=@DepartmentName,Amount=@Amount,AmountValidDate=@ValidDate,units=@units,TurnAroundTime=@TurnAroundTime,PatientPreparation=@PatientPreparation,ExpectedResultDate=@ExpectedResultDate,TestSchedule=@TestSchedule,CutoffTime=@cutOffTime, AdditionalFixedComments=@AdditionalFixedComments, TestInformation=@TestInformation , commonParagraph=@commonParagraph , NumericOrText=@NumericOrText , UrineCulture=@UrineCulture , AlternativeSampleContainer=@AlternativeSample, RequiredBiospyTestNumber=@RequiredBiospyTestNumber, Outsourced=@Outsourced, SampleContainer=@SampleContainer,Pregnancyrefrange=@Pregnancyrefrange,AgewiseReferenceRange=@AgewiseReferenceRange,AgewiseCriticalValue=@AgewiseCriticalValue,multiplecomponents=@multiplecomponents, CalculationPresent=@CalculationPresent,Methodology=@Methodology where TestID='" + childTestList.TestID + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@TestName", childTestList.TestName);
                    cmd.Parameters.AddWithValue("@DisplayName", childTestList.DisplayName);
                    cmd.Parameters.AddWithValue("@commonParagraph", childTestList.commonParagraph);
                    cmd.Parameters.AddWithValue("@SampleContainer", childTestList.SampleContainer);
                    cmd.Parameters.AddWithValue("@NumericOrText", childTestList.NumericOrText);
                    cmd.Parameters.AddWithValue("@UrineCulture", childTestList.UrineCulture);
                    cmd.Parameters.AddWithValue("@AlternativeSample", childTestList.AlternativeSample);

                    cmd.Parameters.AddWithValue("@DepartmentName", childTestList.DepartmentName);
                    cmd.Parameters.AddWithValue("@Amount", childTestList.Amount);
                    cmd.Parameters.AddWithValue("@ValidDate", childTestList.ValidDate);
                    cmd.Parameters.AddWithValue("@units", childTestList.units);
                    cmd.Parameters.AddWithValue("@TurnAroundTime", childTestList.TurnAroundTime);
                    cmd.Parameters.AddWithValue("@PatientPreparation", childTestList.PatientPreparation);
                    cmd.Parameters.AddWithValue("@ExpectedResultDate", childTestList.ExpectedResultDate);
                    cmd.Parameters.AddWithValue("@TestSchedule", childTestList.TestSchedule);
                    cmd.Parameters.AddWithValue("@cutOffTime", childTestList.cutOffTime);
                    cmd.Parameters.AddWithValue("@AdditionalFixedComments", childTestList.AdditionalFixedComments);
                    cmd.Parameters.AddWithValue("@TestInformation", childTestList.TestInformation);
                    cmd.Parameters.AddWithValue("@RequiredBiospyTestNumber", childTestList.RequiredBiospyTestNumber);
                    cmd.Parameters.AddWithValue("@Outsourced", childTestList.Outsourced);

                    cmd.Parameters.AddWithValue("@Pregnancyrefrange", childTestList.Pregnancyrefrange);
                    cmd.Parameters.AddWithValue("@AgewiseReferenceRange", childTestList.AgewiseReferenceRange);
                    cmd.Parameters.AddWithValue("@AgewiseCriticalValue", childTestList.AgewiseCriticalValue);
                    cmd.Parameters.AddWithValue("@multiplecomponents", childTestList.Multiplecomponents);
                    cmd.Parameters.AddWithValue("@CalculationPresent", childTestList.CalculationPresent);

                    cmd.Parameters.AddWithValue("@Methodology", childTestList.Methodology);
                   
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region getallSampleMrdNoCount
        /// <summary>
        /// Table - 
        /// </summary>
        ///  
        /// <returns></returns>
        [Route("api/Account/getallSampleMrdNoCount")]
        [HttpGet]
        public int getallSampleMrdNoCount(string samplemrdno)
        {
            CountApprover countApprover = new CountApprover();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT Count(*) FROM pathoclinic.resultlabtest where MrdNo = '" + samplemrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            countApprover.Count = Convert.ToInt32(dr[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return countApprover.Count;
            }
        }
        #endregion

        #region getallResult
        /// <summary>
        /// Table - 
        /// </summary>
        ///     
        /// <returns></returns>
        [Route("api/Account/getallResult")]
        [HttpGet]
        public List<ResultLabTech> getallResult(string mrdNo)
        {
            List<ResultLabTech> lstResult = new List<ResultLabTech>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest Where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.resultlabtestID = (int)dr["ResultLabId"];
                            resultLabTech.MrdNo = dr["MrdNo"].ToString();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.TestCode = dr["TestCode"].ToString();
                            resultLabTech.NormalValues = dr["DefaultValues"].ToString();
                            resultLabTech.Units = dr["Units"].ToString();
                            resultLabTech.Comment = dr["Comment1"].ToString();
                            resultLabTech.Comment2 = dr["Comment2"].ToString();
                            lstResult.Add(resultLabTech);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResult;
            }
        }
        #endregion


        #region updateFinalResult
        /// <summary>
        /// Table - childtestlist
        /// updated the childtestlist details 
        /// </summary>       
        /// <param name="activeStatus"></param>
        [Route("api/Account/updateFinalResult")]
        [HttpPost]
        public void updateFinalResult(ResultLabTech result)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE pathoclinic.resultlabtest SET Result=@Result,Comment1=@Comment,Comment2=@Comment2 where ResultLabId='" + result.resultlabtestID + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Result", result.Result);
                    cmd.Parameters.AddWithValue("@Comment", result.Comment);
                    cmd.Parameters.AddWithValue("@Comment2", result.Comment2);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region insertTestCalculation
        /// <summary>
        /// table - TestCalculation
        /// </summary>
        /// <param name="TestCalculation"></param>
        [Route("api/Account/insertTestCalculation")]
        [HttpPost]
        public void insertTestCalculation(TestCalculation testCalculation)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.testcalculation(TestCode,Calculation1,Calculation2,Calculation3,Calculation4,CalculationType) VALUES(@val1,@val2,@val3,@val4,@val5,@val6)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", testCalculation.TestCode);
                    cmd.Parameters.AddWithValue("@val2", testCalculation.Calculation1);
                    cmd.Parameters.AddWithValue("@val3", testCalculation.Calculation2);
                    cmd.Parameters.AddWithValue("@val4", testCalculation.Calculation3);
                    cmd.Parameters.AddWithValue("@val5", testCalculation.Calculation4);
                    cmd.Parameters.AddWithValue("@val6", testCalculation.CalculationType);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion

        #region getCalculationByTestCode
        /// <summary>
        /// Table - TestCalculation
        /// </summary>
        /// get details from TestCalculation table      
        /// <returns></returns>
        [Route("api/Account/getCalculationByTestCode")]
        [HttpGet]
        public List<calculationForTestDetails> getCalculationByTestCode(string testCode)
        {
            List<calculationForTestDetails> lstTestCalculation = new List<calculationForTestDetails>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * from  pathoclinic.calculationfortestdetails where TestCode='" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            calculationForTestDetails testCalculation = new calculationForTestDetails();
                            //    labOrder.MrdNo = dr["MrdNo"].ToString();
                            testCalculation.id = (int)dr["id"];
                            testCalculation.testCode = dr["TestCode"].ToString();
                            testCalculation.CalculationUnits = dr["units"].ToString();
                            testCalculation.FormulaLabel = dr["formulaLabel"].ToString();
                            testCalculation.splcalculation = dr["splcalculation"].ToString();
                            testCalculation.CalculationType = dr["CalculationType"].ToString();
                            testCalculation.CalculationCategory = dr["CalculationCategory"].ToString();
                            testCalculation.TestCodesCalculationPart = dr["TestCodesCalculationPart"].ToString();
                            testCalculation.NormalValues = dr["NormalValues"].ToString();
                            testCalculation.ElementName = dr["Elements"].ToString();
                            testCalculation.ElementsCalculationType = dr["ElementsCalculationType"].ToString();
                            lstTestCalculation.Add(testCalculation);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstTestCalculation;
            }
        }
        #endregion
        //getResultByTestCodeMrdNo

        #region getResultByTestCodeMrdNoCalculation
        /// <summary>
        /// Table - resultlabtest
        /// </summary>
        /// get pending amount values from resultlabtest table      
        /// <returns></returns>
        [Route("api/Account/getResultByTestCodeMrdNoCalculation")]
        [HttpGet]
        public List<ResultLabTech> getResultByTestCodeMrdNoCalculation(string mrdNo, string testCode)
        {
            List<ResultLabTech> lstResultLabTech = new List<ResultLabTech>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest where MrdNo='" + mrdNo + "' && TestCode='" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.MrdNo = dr["MrdNo"].ToString();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.TestCode = dr["TestCode"].ToString();
                            resultLabTech.NormalValues = dr["DefaultValues"].ToString();
                            resultLabTech.Units = dr["Units"].ToString();
                            resultLabTech.Comment = dr["Comment1"].ToString();
                            resultLabTech.Comment2 = dr["Comment2"].ToString();
                            resultLabTech.DisplayValueText = dr["DisplayValueText"].ToString();
                            lstResultLabTech.Add(resultLabTech);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion

        #region insertAlternativesamplecontainer
        /// <summary>
        /// Table - Alternativesamplecontainer
        /// </summary>
        /// Inserted the Alternativesamplecontainer table values.
        /// <param name="Alternativesamplecontainer"></param>
        [Route("api/Account/insertAlternativesamplecontainer")]
        [HttpPost]
        public void insertAlternativesamplecontainer(Alternativesamplecontainer alternativesamplecontainer)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO Alternativesamplecontainer(AlternativeSampleName,TestCode) VALUES(@val1,@val2)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", alternativesamplecontainer.AlternativeSampleName);
                    cmd.Parameters.AddWithValue("@val2", alternativesamplecontainer.TestCode);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region insertCommonInstrumentReagent
        /// <summary>
        /// Table - CommonInstrumentReagent
        /// </summary>
        /// Inserted the CommonInstrumentReagent table values.
        /// <param name="CommonInstrumentReagent"></param>
        [Route("api/Account/insertCommonInstrumentReagent")]
        [HttpPost]
        public void insertCommonInstrumentReagent(CommonInstrumentReagent commonInstrumentReagent)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "INSERT INTO CommonInstrumentReagent(InstrumentReagentName) VALUES(@val1)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", commonInstrumentReagent.InstrumentReagentName);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region getDuplicationCommonInstrumentReagent
        /// <summary>
        /// Table - CommonInstrumentReagent
        /// </summary>
        /// Listed all values from CommonInstrumentReagent table
        /// <returns></returns>
        [Route("api/Account/getDuplicationCommonInstrumentReagent")]
        [HttpGet]
        public int getDuplicationCommonInstrumentReagent(string instrumentReagent)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.CommonInstrumentReagent where InstrumentReagentName='" + instrumentReagent + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region getAllInstrumentReagent
        /// <summary>
        /// Table - 
        /// </summary>
        ///     
        /// <returns></returns>
        [Route("api/Account/getAllInstrumentReagent")]
        [HttpGet]
        public List<CommonInstrumentReagent> getAllInstrumentReagent()
        {
            List<CommonInstrumentReagent> lstCommonInstrumentReagent = new List<CommonInstrumentReagent>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.CommonInstrumentReagent ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            CommonInstrumentReagent commonInstrumentReagent = new CommonInstrumentReagent();
                            commonInstrumentReagent.InstrumentReagentName = dr["InstrumentReagentName"].ToString();

                            lstCommonInstrumentReagent.Add(commonInstrumentReagent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstCommonInstrumentReagent;
            }
        }
        #endregion

        #region getLabOrderDetailsByRegId
        /// <summary>
        ///  Table - LabOrder
        ///  get LabOrder details from LabOrder table by regID
        /// </summary>
        /// <param name="regID"></param>
        /// <returns></returns>
        [Route("api/Account/getLabOrderDetailsByRegId")]
        [HttpGet]
        public List<LabOrder> getLabOrderDetailsByRegId(string regID)
        {

            List<LabOrder> labHistory = new List<LabOrder>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM LabOrder where RegID = '" + regID + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrder labOrder = new LabOrder();
                            labOrder.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                            labOrder.PatientName = dr["PatientName"].ToString();
                            labOrder.MrdNo = dr["MrdNo"].ToString();
                            labOrder.RegID = (int)dr["RegID"];

                            labHistory.Add(labOrder);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return labHistory;
            }
        }
        #endregion

        #region insertProfilelistForGrid
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/insertProfilelistForGrid")]
        [HttpPost]
        public void insertProfilelistForGrid(AddProfileGrid addprofilegrid)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //foreach (var labTestDetails in labTestList)
                    //{
                    string strSQL = "INSERT INTO gridprofilelist(RegID,ProfileCode,ProfileName,Amount,MrdNo,Discount,NetAmount,ProfileID) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", addprofilegrid.RegID);
                    cmd.Parameters.AddWithValue("@val2", addprofilegrid.ProfileCode);
                    cmd.Parameters.AddWithValue("@val3", addprofilegrid.ProfileName);
                    cmd.Parameters.AddWithValue("@val4", addprofilegrid.Amount);
                    cmd.Parameters.AddWithValue("@val5", addprofilegrid.MrdNo);
                    cmd.Parameters.AddWithValue("@val6", addprofilegrid.Discount);
                    cmd.Parameters.AddWithValue("@val7", addprofilegrid.NetAmount);
                    cmd.Parameters.AddWithValue("@val8", addprofilegrid.ProfileID);
                    cmd.ExecuteNonQuery();
                    //}
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion
        #region getProfileListForGrid
        /// <summary>
        /// Table - 
        /// </summary>
        ///     
        /// <returns></returns>
        [Route("api/Account/getProfileListForGrid")]
        [HttpGet]
        public List<AddProfileGrid> getProfileListForGrid(string mrdno)
        {
            List<AddProfileGrid> lstprofile = new List<AddProfileGrid>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.gridprofilelist where MrdNo ='" + mrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            AddProfileGrid profile = new AddProfileGrid();
                            profile.ID = (int)dr["ID"];
                            profile.ProfileID = (int)dr["ProfileID"];
                            profile.ProfileCode = dr["ProfileCode"].ToString();
                            profile.MrdNo = dr["MrdNo"].ToString();
                            profile.Discount = dr["Discount"].ToString();
                            profile.NetAmount = dr["NetAmount"].ToString();
                            profile.ProfileName = dr["ProfileName"].ToString();
                            profile.Amount = Convert.ToDouble((dr["Amount"]));
                            lstprofile.Add(profile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstprofile;
            }
        }
        #endregion
        #region UpdateProfilegrid 
        /// <summary>
        /// Table - childtestlist
        /// updated the childtestlist details 
        /// </summary>       
        /// <param name="activeStatus"></param>

        [Route("api/Account/UpdateProfilegrid")]
        [HttpPost]
        public void UpdateProfilegrid(AddProfileGrid addprofile)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE pathoclinic.gridprofilelist SET Discount=@Discount,NetAmount=@NetAmount where ID = '" + addprofile.ID + "' && MrdNo ='" + addprofile.MrdNo + "' && ProfileCode ='" + addprofile.ProfileCode + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Discount", addprofile.Discount);
                    cmd.Parameters.AddWithValue("@NetAmount", addprofile.NetAmount);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion
        #region getTotalprofileAmount
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>

        [Route("api/Account/getTotalprofileAmount")]
        [HttpGet]
        public int getTotalprofileAmount(string mrdno)
        {
            int amt = 0;
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString);
            string strSQL = "SELECT SUM(NetAmount) FROM pathoclinic.gridprofilelist where MrdNo ='" + mrdno + "'";
            conn.Open();
            MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
            DataSet ds = new DataSet();
            mydata.Fill(ds);
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0] != DBNull.Value)
                {
                    amt = Convert.ToInt32(dr[0]);
                }
                else
                {
                    amt = 0;
                }
            }
            conn.Close();
            return amt;
        }


        #endregion



        #region insertTestlistForGrid
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/insertTestlistForGrid")]
        [HttpPost]
        public void insertTestlistForGrid(AddTestGrid addtestgrid)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //foreach (var labTestDetails in labTestList)
                    //{
                    string strSQL = "INSERT INTO gridtestlist(RegID,TestCode,TestName,Amount,MrdNo,Discount,NetAmount) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", addtestgrid.RegID);
                    cmd.Parameters.AddWithValue("@val2", addtestgrid.TestCode);
                    cmd.Parameters.AddWithValue("@val3", addtestgrid.TestName);
                    cmd.Parameters.AddWithValue("@val4", addtestgrid.Amount);
                    cmd.Parameters.AddWithValue("@val5", addtestgrid.MrdNo);
                    cmd.Parameters.AddWithValue("@val6", addtestgrid.Discount);
                    cmd.Parameters.AddWithValue("@val7", addtestgrid.NetAmount);
                    cmd.ExecuteNonQuery();
                    //}
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion
        #region getTestListForGrid
        /// <summary>
        /// Table - 
        /// </summary>
        ///     
        /// <returns></returns>
        [Route("api/Account/getTestListForGrid")]
        [HttpGet]
        public List<AddTestGrid> getTestListForGrid(string mrdno)
        {
            List<AddTestGrid> lstprofile = new List<AddTestGrid>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.gridtestlist where MrdNo ='" + mrdno + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            AddTestGrid test = new AddTestGrid();
                            test.ID = (int)dr["ID"];
                            test.TestCode = dr["TestCode"].ToString();
                            test.MrdNo = dr["MrdNo"].ToString();
                            test.Discount = dr["Discount"].ToString();
                            test.NetAmount = dr["NetAmount"].ToString();
                            test.TestName = dr["TestName"].ToString();
                            test.Amount = Convert.ToDouble((dr["Amount"]));
                            lstprofile.Add(test);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstprofile;
            }
        }
        #endregion
        #region UpdateTestgrid 
        /// <summary>
        /// Table - childtestlist
        /// updated the childtestlist details 
        /// </summary>       
        /// <param name="activeStatus"></param>

        [Route("api/Account/UpdateTestgrid")]
        [HttpPost]
        public void UpdateTestgrid(AddTestGrid addTest)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE pathoclinic.gridtestlist SET Discount=@Discount,NetAmount=@NetAmount where ID = '" + addTest.ID + "' && MrdNo ='" + addTest.MrdNo + "' && TestCode ='" + addTest.TestCode + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Discount", addTest.Discount);
                    cmd.Parameters.AddWithValue("@NetAmount", addTest.NetAmount);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion
        #region getTotalTestAmount
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>

        [Route("api/Account/getTotalTestAmount")]
        [HttpGet]
        public int getTotalTestAmount(string mrdno)
        {
            int amt = 0;
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString);
            string strSQL = "SELECT SUM(NetAmount) FROM pathoclinic.gridtestlist where MrdNo ='" + mrdno + "'";
            conn.Open();
            MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
            MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
            DataSet ds = new DataSet();
            mydata.Fill(ds);
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0] != DBNull.Value)
                {
                    amt = Convert.ToInt32(dr[0]);
                }
                else
                {
                    amt = 0;
                }
            }
            conn.Close();
            return amt;
        }


        #endregion


        #region getAllRegistrationDetailsforCustomerView
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllRegistrationDetailsforCustomerView")]
        [HttpGet]
        public List<PatientRegistration> getAllRegistrationDetailsforCustomerView()
        {
            List<PatientRegistration> lstregistrationDetails = new List<PatientRegistration>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.patientregistration";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            PatientRegistration patientRegistration = new PatientRegistration();

                            patientRegistration.RegID = Convert.ToInt32(dr["RegID"]);
                            patientRegistration.Title = dr["Title"].ToString();
                            patientRegistration.FirstName = dr["FirstName"].ToString();
                            patientRegistration.MiddleName = dr["MiddleName"].ToString();
                            patientRegistration.LastName = dr["LastName"].ToString();
                            patientRegistration.Guardian = dr["Guardian"].ToString();
                            patientRegistration.Relation = dr["Relation"].ToString();
                            patientRegistration.PatientType = dr["PatientType"].ToString();
                            patientRegistration.Sex = dr["Sex"].ToString();
                            patientRegistration.MaritalStatus = dr["MaritalStatus"].ToString();
                            patientRegistration.DOB = dr["DOB"].ToString();
                            patientRegistration.Age = Convert.ToInt16(dr["age"]);

                            patientRegistration.Age = (int)dr["Age"];
                            if (patientRegistration.Age == 0)
                            {

                                patientRegistration.AgeCategory = dr["AgeCategory"].ToString();
                                patientRegistration.Age = (int)dr["UnknownAge"];
                            }

                            patientRegistration.DateOfReg = dr["DateOfReg"].ToString();
                            patientRegistration.PhoneNumber = dr["PhoneNumber"].ToString();
                            lstregistrationDetails.Add(patientRegistration);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstregistrationDetails;
            }
        }
        #endregion


        #region TruncateGridProfileList
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="childTestList"></param>
        [Route("api/Account/TruncateGridProfileList")]
        [HttpPost]
        public void TruncateGridProfileList()
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "truncate pathoclinic.gridprofilelist";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion

        #region TruncateGridTestList
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/TruncateGridTestList")]
        [HttpPost]
        public void TruncateGridTestList()
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "truncate pathoclinic.gridtestlist";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion

        #region getAlternativesamplecontainerByTestCode
        /// <summary>
        /// Table - 
        /// </summary>
        ///     
        /// <returns></returns>
        [Route("api/Account/getAlternativesamplecontainerByTestCode")]
        [HttpGet]
        public List<Alternativesamplecontainer> getAlternativesamplecontainerByTestCode(string testCode)
        {
            List<Alternativesamplecontainer> lstAlternativesamplecontainer = new List<Alternativesamplecontainer>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.Alternativesamplecontainer where TestCode='" + testCode + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Alternativesamplecontainer alternativesamplecontainer = new Alternativesamplecontainer();
                            alternativesamplecontainer.AlternativeSampleContainerId = (int)dr["AlternativeSampleContainerId"];
                            alternativesamplecontainer.AlternativeSampleName = dr["AlternativeSampleName"].ToString();
                            alternativesamplecontainer.TestCode = dr["TestCode"].ToString();
                            lstAlternativesamplecontainer.Add(alternativesamplecontainer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstAlternativesamplecontainer;
            }
        }
        #endregion


        #region getTestDetailsByProfileIDLabTech
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// Listed all values from childtestlist table
        /// <returns></returns>
        [Route("api/Account/getTestDetailsByProfileIDLabTech")]
        [HttpGet]
        public List<LabTechTestListByProfile> getTestDetailsByProfileIDLabTech(string profileID, string mrdNo)
        {
            List<LabTechTestListByProfile> lstchildTest = new List<LabTechTestListByProfile>();
            DataTable dt = new DataTable();
            int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.ChildTestList where ProfileID='" + profileID + "' order by ProfilePriority asc";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            LabTechTestListByProfile childTestDetails = new LabTechTestListByProfile();
                            childTestDetails.Sno = i;

                            //  SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();
                            string mrdNoLocal = mrdNo;
                            string testcodeLocal = dr["TestCode"].ToString();
                            DataTable dts = new DataTable();
                            // int i = 1;
                            using (MySqlConnection conns = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                            {

                                //string strSQL = "SELECT * FROM childtestlist INNER JOIN labProfilelist ON childtestlist.ProfileID = labProfilelist.ProfileID where labProfilelist.MrdNo = '"+ mrdNo + "'";

                                string strSQLs = "SELECT * FROM SampleCollecter where MrdNo = '" + mrdNoLocal + "' and TestCode='" + testcodeLocal + "'";



                                conns.Open();
                                MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conns);
                                MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                                DataSet dss = new DataSet();
                                mydatas.Fill(dss);
                                dts = dss.Tables[0];
                                foreach (DataRow drs in dts.Rows)
                                {
                                    if (drs != null)
                                    {
                                        childTestDetails.SampleStatus = (int)drs["SampleStatus"];
                                        childTestDetails.SampleName = drs["sampleName"].ToString();

                                    }
                                }
                            }
                            childTestDetails.MrdNo = mrdNo;
                            childTestDetails.TestID = Convert.ToInt32(dr["TestID"]);
                            childTestDetails.ProfileID = Convert.ToInt32(dr["ProfileID"]);
                            childTestDetails.TestName = dr["TestName"].ToString();
                            childTestDetails.TestCode = dr["TestCode"].ToString();
                            childTestDetails.Methodology = dr["Methodology"].ToString();
                            childTestDetails.UnitMeasurementNumeric = dr["UnitMeasurementNumeric"].ToString();
                            childTestDetails.UnitMesurementFreeText = dr["UnitMesurementFreeText"].ToString();
                            childTestDetails.SampleContainer = dr["SampleContainer"].ToString();
                            childTestDetails.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            childTestDetails.DefaultValues = dr["DefaultValues"].ToString();
                            childTestDetails.GenderMale = dr["GenderMale"].ToString();
                            childTestDetails.GenderFemale = dr["GenderFemale"].ToString();
                            childTestDetails.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            childTestDetails.LowerCriticalValue = dr["LowerCriticalValue"].ToString();
                            childTestDetails.UpperCriticalValue = dr["UpperCriticalValue"].ToString();
                            childTestDetails.OtherCriticalReport = dr["OtherCriticalReport"].ToString();
                            childTestDetails.AgewiseCriticalValue = dr["AgewiseCriticalValue"].ToString();
                            childTestDetails.AgewiseReferenceRange = dr["AgewiseReferenceRange"].ToString();
                            childTestDetails.units = dr["units"].ToString();
                            childTestDetails.TurnAroundTime = dr["TurnAroundTime"].ToString();
                            childTestDetails.RequiredBiospyTestNumber = dr["RequiredBiospyTestNumber"].ToString();
                            childTestDetails.RequiredSamples = dr["RequiredSamples"].ToString();
                            childTestDetails.PatientPreparation = dr["PatientPreparation"].ToString();
                            childTestDetails.ExpectedResultDate = dr["ExpectedResultDate"].ToString();
                            childTestDetails.Amount = Convert.ToDouble(dr["Amount"]);
                            childTestDetails.Finaloutput = dr["Finaloutput"].ToString();
                            childTestDetails.TestbasedDiscount = dr["TestbasedDiscount"].ToString();
                            childTestDetails.Outsourced = dr["Outsourced"].ToString();
                            childTestDetails.CreateDate = dr["CreateDate"].ToString();
                            childTestDetails.commonParagraph = dr["commonParagraph"].ToString();
                            childTestDetails.Multiplecomponents = dr["Multiplecomponents"].ToString();
                            childTestDetails.UrineCulture = dr["UrineCulture"].ToString();
                            childTestDetails.CalculationPresent = dr["CalculationPresent"].ToString();

                            childTestDetails.ProfilePriority = Convert.ToInt32(dr["ProfilePriority"]);
                            i++;

                            lstchildTest.Add(childTestDetails);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstchildTest;
            }
        }
        #endregion


        #region getPatientBioDetailsByRegId
        /// <summary>
        /// Table - elements
        /// </summary>
        /// get Elements details from elements table by Element Name
        /// <returns></returns>
        [Route("api/Account/getPatientBioDetailsByRegId")]
        [HttpGet]
        public List<BioInfoLabView> getPatientBioDetailsByRegId(string regID)
        {

            List<BioInfoLabView> bioInfoLabView = new List<BioInfoLabView>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM LabOrder where MrdNo = '" + regID + "'";

                    //  string strSQL = "SELECT * FROM patientregistration where RegID = '" + regIDValue + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            BioInfoLabView lab = new BioInfoLabView();
                            lab.RegID = Convert.ToInt32(dr["RegID"]);
                            lab.IsPregnancy = Convert.ToInt32(dr["IsPregnancy"]);
                            lab.LMP = dr["LMP"].ToString();
                            lab.Trimester = dr["Trimester"].ToString();

                            DataTable dta = new DataTable();
                            string strSQL1 = "SELECT * FROM patientregistration where RegID = '" + lab.RegID + "'";
                            MySqlDataAdapter mydataa = new MySqlDataAdapter(strSQL1, conn);
                            MySqlCommandBuilder cmda = new MySqlCommandBuilder(mydataa);
                            DataSet dsa = new DataSet();
                            mydataa.Fill(dsa);
                            dta = dsa.Tables[0];
                            foreach (DataRow dra in dta.Rows)
                            {
                                if (dra != null)
                                {
                                    lab.FirstName = dra["FirstName"].ToString();
                                    lab.LastName = dra["LastName"].ToString();
                                    lab.Age = Convert.ToInt32(dra["Age"]);
                                    lab.Sex = dra["Sex"].ToString();
                                    lab.DOB = dra["DOB"].ToString();
                                    lab.SpecialComments = dra["SpecialComments"].ToString();
                                }
                            }

                            bioInfoLabView.Add(lab);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return bioInfoLabView;
            }
        }
        #endregion


        #region getBoneMarrowResultByMrdNoandTestCode
        /// <summary>
        /// get all details from bonemarrowaspiration table using mrdNo and TestCode for Customer View Page to generate Report
        /// Table - bonemarrowaspiration
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getBoneMarrowResultByMrdNoandTestCode")]
        [HttpGet]
        public List<BoneMarrowAspirationNew> getBoneMarrowResultByMrdNoandTestCode(string mrdNo, string TestCode)
        {
            List<BoneMarrowAspirationNew> bonemarrowaspiration = new List<BoneMarrowAspirationNew>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where MrdNo='" + mrdNo + "'AND TestCode='" + TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            BoneMarrowAspirationNew bonemarrowaspirations = new BoneMarrowAspirationNew();
                            bonemarrowaspirations.bonemarrowaspirationid = (int)dr["bonemarrowaspirationid"];
                            bonemarrowaspirations.MrdNo = dr["MrdNo"].ToString();
                            bonemarrowaspirations.TestCode = dr["TestCode"].ToString();
                            bonemarrowaspirations.TestName = dr["TestName"].ToString();
                            bonemarrowaspirations.ElementId = (int)dr["ElementId"];
                            bonemarrowaspirations.ElementName = dr["ElementName"].ToString();
                            bonemarrowaspirations.TemplateId = (int)dr["TemplateId"];
                            bonemarrowaspirations.TemplateName = dr["TemplateName"].ToString();
                            bonemarrowaspirations.TemplateDescription = dr["TemplateDescription"].ToString();
                            bonemarrowaspiration.Add(bonemarrowaspirations);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return bonemarrowaspiration;
            }
        }
        #endregion


        #region getReportLabTestResultByMrdNoView
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportLabTestResultByMrdNoView")]
        [HttpGet]
        public List<ResultLabTech> getReportLabTestResultByMrdNoView(string mrdNo, string testCode)
        {
            List<ResultLabTech> lstResultLabTech = new List<ResultLabTech>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest where MrdNo='" + mrdNo + "' AND TestCode='" + testCode + "' order by ProfilePriority asc";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.MrdNo = dr["MrdNo"].ToString();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.TestCode = dr["TestCode"].ToString();
                            resultLabTech.NormalValues = dr["DefaultValues"].ToString();
                            resultLabTech.Units = dr["Units"].ToString();
                            resultLabTech.Comment = dr["Comment1"].ToString();
                            resultLabTech.Comment2 = dr["Comment2"].ToString();
                            resultLabTech.SpecialComments = dr["SpecialComments"].ToString();
                            resultLabTech.EndRange = dr["EndRange"].ToString();
                            resultLabTech.StartRange = dr["StartRange"].ToString();
                            resultLabTech.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            resultLabTech.DisplayValueText = dr["DisplayValueText"].ToString();
                            resultLabTech.FreeText = dr["FreeText"].ToString();
                            resultLabTech.ProfilePriority = (int)dr["ProfilePriority"];
                            resultLabTech.NormalValuesFiled = dr["NormalValuesFiled"].ToString();


                            lstResultLabTech.Add(resultLabTech);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion


        #region getReportBoneMarrowByMrdNoView
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportBoneMarrowByMrdNoView")]
        [HttpGet]
        public int getReportBoneMarrowByMrdNoView(string mrdNo, string testCode)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where MrdNo='" + mrdNo + "' AND TestCode='" + testCode + "' order by ElementId Asc ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (dr != null)
                    //    {
                    //        ResultLabTech resultLabTech = new ResultLabTech();
                    //        resultLabTech.MrdNo = dr["MrdNo"].ToString();
                    //        resultLabTech.Result = dr["Result"].ToString();
                    //        resultLabTech.TestName = dr["TestName"].ToString();
                    //        resultLabTech.TestCode = dr["TestCode"].ToString();
                    //        resultLabTech.NormalValues = dr["DefaultValues"].ToString();
                    //        resultLabTech.Units = dr["Units"].ToString();
                    //        resultLabTech.Comment = dr["Comment1"].ToString();
                    //        resultLabTech.Comment2 = dr["Comment2"].ToString();
                    //        lstResultLabTech.Add(resultLabTech);
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region getReportBoneMarrowByMrdNoTestCodeEditView
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportBoneMarrowByMrdNoTestCodeEditView")]
        [HttpGet]
        public List<BoneMarrowAspirationNew> getReportBoneMarrowByMrdNoTestCodeEditView(string mrdNo, string testCode)
        {
            {
                List<BoneMarrowAspirationNew> bonemarrowaspiration = new List<BoneMarrowAspirationNew>();
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                {
                    try
                    {
                        string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where MrdNo='" + mrdNo + "' AND TestCode='" + testCode + "'";
                        conn.Open();
                        MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                        MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                        DataSet ds = new DataSet();
                        mydata.Fill(ds);
                        dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr != null)
                            {
                                BoneMarrowAspirationNew bonemarrowaspirations = new BoneMarrowAspirationNew();
                                bonemarrowaspirations.MrdNo = dr["MrdNo"].ToString();
                                bonemarrowaspirations.TestCode = dr["TestCode"].ToString();
                                bonemarrowaspirations.ElementId = (int)dr["ElementId"];
                                bonemarrowaspirations.TemplateId = (int)dr["TemplateId"];
                                bonemarrowaspirations.ElementName = dr["ElementName"].ToString();
                                bonemarrowaspirations.TemplateName = dr["TemplateName"].ToString();
                                bonemarrowaspirations.TemplateDescription = dr["TemplateDescription"].ToString();
                                bonemarrowaspiration.Add(bonemarrowaspirations);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                    }
                    return bonemarrowaspiration;
                }
            }
        }
        #endregion


        #region updateBoneMarrowNew
        /// <summary>
        /// Table - insert BoneMarrowAspiration
        /// </summary>
        /// <param name="insuranceprofilelist"></param>
        [Route("api/Account/updateBoneMarrowNew")]
        [HttpPost]
        public void updateBoneMarrowNew(BoneMarrowAspirationNew boneMarrowAspirationNew)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  bonemarrowaspiration SET TemplateDescription='" + boneMarrowAspirationNew.TemplateDescription + "' where MrdNo='" + boneMarrowAspirationNew.MrdNo + "' && TestCode='" + boneMarrowAspirationNew.TestCode + "'&& ElementId='" + boneMarrowAspirationNew.ElementId + "' ";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getSampleListByMrdNoProfile
        /// <summary>
        /// Table - LabTestList,childtestlist
        /// </summary>
        /// Listed all values from LabTestList,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getSampleListByMrdNoProfile")]
        [HttpGet]
        public List<SampleContainersByMrdNo> getSampleListByMrdNoProfile(int profileID, string mrdNo)
        {
            List<SampleContainersByMrdNo> lstSampleDetails = new List<SampleContainersByMrdNo>();
            DataTable dt = new DataTable();
            //int i = 1;

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT* FROM pathoclinic.ChildTestList where ProfileID = '" + profileID + "' order by ProfilePriority asc";



                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            //new
                            //   SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();

                            string testcodeLocal = dr["TestCode"].ToString();
                            DataTable dts = new DataTable();
                            // int i = 1;
                            using (MySqlConnection conns = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                            {

                                //string strSQL = "SELECT * FROM childtestlist INNER JOIN labProfilelist ON childtestlist.ProfileID = labProfilelist.ProfileID where labProfilelist.MrdNo = '"+ mrdNo + "'";

                                string strSQLs = "SELECT * FROM SampleCollecter where MrdNo = '" + mrdNo + "' and TestCode='" + testcodeLocal + "'";



                                conns.Open();
                                MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conns);
                                MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                                DataSet dss = new DataSet();
                                mydatas.Fill(dss);
                                dts = dss.Tables[0];
                                if (dts.Rows.Count > 0)
                                {
                                    foreach (DataRow drs in dts.Rows)
                                    {
                                        if (drs != null)
                                        {
                                            SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();
                                            sampleContainer.SampleStatus = (int)drs["SampleStatus"];
                                            sampleContainer.SampleContainer = drs["SampleName"].ToString();
                                            sampleContainer.TestCode = drs["TestCode"].ToString();
                                            sampleContainer.TestID = (int)drs["TestID"];
                                            sampleContainer.ProfileID = (int)drs["ProfileID"];
                                            sampleContainer.ProfileName = drs["ProfileName"].ToString();
                                            sampleContainer.TestName = drs["TestName"].ToString();
                                            sampleContainer.Dynamic = "";
                                            sampleContainer.ProfilePriority = (int)drs["ProfilePriority"];
                                            //if (sampleContainer.SampleStatus != 1)
                                            //{
                                            DataTable dt2 = new DataTable();
                                            string strSQL2 = "SELECT * FROM pathoclinic.Alternativesamplecontainer where TestCode='" + testcodeLocal + "' ";
                                            // conn.Open();
                                            MySqlDataAdapter mydata2 = new MySqlDataAdapter(strSQL2, conn);
                                            MySqlCommandBuilder cmd2 = new MySqlCommandBuilder(mydata2);
                                            DataSet ds2 = new DataSet();
                                            mydata2.Fill(ds2);
                                            dt2 = ds2.Tables[0];
                                            if (dt2.Rows.Count != 0)
                                            {
                                                foreach (DataRow dr2 in dt2.Rows)
                                                {
                                                    if (dr2 != null)
                                                    {

                                                        sampleContainer.AlternativeSampleName = dr2["AlternativeSampleName"].ToString(); sampleContainer.AlternativeSampleAvailable = "Yes";

                                                    }
                                                }

                                            }
                                            else
                                            {
                                                sampleContainer.AlternativeSampleAvailable = "No";
                                            }
                                            //}
                                            //else
                                            //{
                                            //    sampleContainer.AlternativeSampleAvailable = "No";
                                            //}

                                            //  }
                                            lstSampleDetails.Add(sampleContainer);
                                            //  sampleContainer.SampleStatus = (int)drs["SampleStatus"];
                                        }



                                    }

                                }
                                else
                                {
                                    SampleContainersByMrdNo sampleContainer = new SampleContainersByMrdNo();
                                    sampleContainer.MrdNo = mrdNo;
                                    sampleContainer.TestID = (int)dr["TestID"];
                                    sampleContainer.TestCode = dr["TestCode"].ToString();
                                    sampleContainer.TestName = dr["TestName"].ToString();
                                    sampleContainer.SampleContainer = dr["SampleContainer"].ToString();
                                    sampleContainer.ProfileID = (int)dr["ProfileID"];
                                    sampleContainer.AlternativeSampleAvailable = dr["AlternativeSampleContainer"].ToString();
                                    sampleContainer.ProfilePriority = (int)dr["ProfilePriority"];

                                    if (sampleContainer.AlternativeSampleAvailable == "Yes")
                                    {
                                        DataTable dt2 = new DataTable();
                                        string strSQL2 = "SELECT * FROM pathoclinic.Alternativesamplecontainer where TestCode='" + testcodeLocal + "' ";
                                        // conn.Open();
                                        MySqlDataAdapter mydata2 = new MySqlDataAdapter(strSQL2, conn);
                                        MySqlCommandBuilder cmd2 = new MySqlCommandBuilder(mydata2);
                                        DataSet ds2 = new DataSet();
                                        mydata2.Fill(ds2);
                                        dt2 = ds2.Tables[0];
                                        foreach (DataRow dr2 in dt2.Rows)
                                        {
                                            if (dr2 != null)
                                            {

                                                sampleContainer.AlternativeSampleName = dr2["AlternativeSampleName"].ToString();

                                            }
                                        }
                                    }

                                    sampleContainer.Dynamic = "";
                                    sampleContainer.SampleStatus = 0;
                                    lstSampleDetails.Add(sampleContainer);

                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstSampleDetails;
            }
        }
        #endregion


        #region getTestMultipleComponents
        /// <summary>
        /// Table - testmultiplecomponents
        /// </summary>
        /// Listed all values from testmultiplecomponents table by using TestCode
        /// <returns></returns>
        [Route("api/Account/getTestMultipleComponents")]
        [HttpGet]
        public List<TestMultipleComponents> getTestMultipleComponents(string TestCode)
        {
            List<TestMultipleComponents> lstMultipleComponents = new List<TestMultipleComponents>();
            DataTable dt = new DataTable();
            //int i = 1;

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    using (MySqlConnection conns = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                    {
                        string strSQLs = "SELECT * FROM testmultiplecomponents where TestCode='" + TestCode + "'";
                        conns.Open();
                        MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conns);
                        MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                        DataSet ds = new DataSet();
                        mydatas.Fill(ds);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow drs in dt.Rows)
                            {
                                if (drs != null)
                                {
                                    TestMultipleComponents MultipleComponents = new TestMultipleComponents();
                                    MultipleComponents.TestMultipleComponentsID = (int)drs["TestMultipleComponentsID"];
                                    MultipleComponents.TestCode = drs["TestCode"].ToString();
                                    MultipleComponents.ElementName = drs["ElementName"].ToString();
                                    MultipleComponents.Color = drs["Color"].ToString();
                                    MultipleComponents.Units = drs["Units"].ToString();
                                    MultipleComponents.Comments = drs["Comments"].ToString();
                                    MultipleComponents.CriticalLow = drs["CriticalLow"].ToString();
                                    MultipleComponents.CriticalHigh = drs["CriticalHigh"].ToString();
                                    MultipleComponents.ReferenceHigh = drs["ReferenceLow"].ToString();
                                    MultipleComponents.ReferenceLow = drs["ReferenceHigh"].ToString();
                                    MultipleComponents.Methodology = drs["Methodology"].ToString();
                                    MultipleComponents.CommentsType = drs["CommentsType"].ToString();
                                    lstMultipleComponents.Add(MultipleComponents);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstMultipleComponents;
            }
        }
        #endregion


        #region updateMultipleComponent
        /// <summary>
        ///  Table - Update testmultiplecomponents
        /// </summary>
        /// <param name="testMultipleComponents"></param>
        [Route("api/Account/updateMultipleComponent")]
        [HttpPost]
        public void updateMultipleComponent(TestMultipleComponents testMultipleComponents)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "UPDATE  testmultiplecomponents SET Color='" + testMultipleComponents.Color + "',Units='" + testMultipleComponents.Units + "',ElementName='" + testMultipleComponents.ElementName + "',Comments='" + testMultipleComponents.Comments + "' where TestMultipleComponentsID='" + testMultipleComponents.TestMultipleComponentsID + "' && TestCode='" + testMultipleComponents.TestCode + "'";
                    string strSQL = "UPDATE  testmultiplecomponents SET Methodology='" + testMultipleComponents.Methodology + "',Units='" + testMultipleComponents.Units + "',ElementName='" + testMultipleComponents.ElementName + "',CommentsType='" + testMultipleComponents.CommentsType + "' where TestMultipleComponentsID='" + testMultipleComponents.TestMultipleComponentsID + "' && TestCode='" + testMultipleComponents.TestCode + "'";
 
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.ExecuteNonQuery();

                    if (testMultipleComponents.ElementName != testMultipleComponents.ElementMatch)
                    {
                        strSQL = "";
                        strSQL = "UPDATE  agewisecricticalreference SET  ElementName='" + testMultipleComponents.ElementName + "' where ElementName='" + testMultipleComponents.ElementMatch + "' && TestCode='" + testMultipleComponents.TestCode + "'";
                        cmd = new MySqlCommand(strSQL, conn);
                        cmd.ExecuteNonQuery();
                        strSQL = "";
                        strSQL = "UPDATE  agewisereferencevalue SET  ElementName='" + testMultipleComponents.ElementName + "' where ElementName='" + testMultipleComponents.ElementMatch + "' && TestCode='" + testMultipleComponents.TestCode + "'";
                        cmd = new MySqlCommand(strSQL, conn);
                        cmd.ExecuteNonQuery();
                    }

                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region getAllRole
        /// <summary>
        /// Table - elements
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getAllRole")]
        [HttpPost]
        public List<Role> getAllRole()
        {
            List<Role> lstRole = new List<Role>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM Role";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            Role roledetails = new Role();
                            roledetails.Id = (int)dr["Id"];
                            roledetails.RoleName = dr["RoleName"].ToString();
                            lstRole.Add(roledetails);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstRole;
            }
        }
        #endregion


        #region updateRoleName
        /// <summary>
        /// Table - 
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/updateRoleName")]
        [HttpPost]
        public void updateRoleName(Role roledetials)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  role SET RoleName='" + roledetials.RoleName + "' where Id='" + roledetials.Id + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion

        #region updateLoginDetails
        /// <summary>
        /// Table - 
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/updateLoginDetails")]
        [HttpPost]
        public void updateLoginDetails(Login logindetials)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  login SET Password='" + logindetials.Password + "' where Id='" + logindetials.Id + "'";

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();


                    //  string strSQL1 = "UPDATE  login SET  ConfirmPassword='" + logindetials.ConfirmPassword + "' where Id='" + logindetials.Id + "'";

                    ////  conn.Open();
                    //  MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                    //  cmd1.CommandType = CommandType.Text;
                    //  cmd1.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region getPragancyReferancyRange
        /// <summary>
        /// Table - pregnancyreferencerange
        /// </summary>
        /// Listed all Details from pregnancyreferencerange table by using TestCode        
        [Route("api/Account/getPragancyReferancyRange")]
        [HttpGet]
        public List<PragancyReferancyRange> getPragancyReferancyRange(string TestCode)
        {
            List<PragancyReferancyRange> lstPragancyReferancy = new List<PragancyReferancyRange>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQLs = "SELECT * FROM pregnancyreferencerange where TestCode='" + TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conn);
                    MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                    DataSet ds = new DataSet();
                    mydatas.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow drs in dt.Rows)
                        {
                            if (drs != null)
                            {
                                PragancyReferancyRange PragancyReferancy = new PragancyReferancyRange();
                                PragancyReferancy.PregnancyId = (int)drs["PregnancyId"];
                                PragancyReferancy.TestCode = drs["TestCode"].ToString();
                                PragancyReferancy.Parameter = drs["Parameter"].ToString();
                                PragancyReferancy.FirstTrimester = drs["FirstTrimester"].ToString();
                                PragancyReferancy.SecondTrimester = drs["SecondTrimester"].ToString();
                                PragancyReferancy.ThirdTrimester = drs["ThirdTrimester"].ToString();
                                lstPragancyReferancy.Add(PragancyReferancy);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstPragancyReferancy;
            }
        }
        #endregion

        #region updatePragancyReferancy
        /// <summary>
        ///  Table - Update pregnancyreferencerange
        /// </summary>
        /// <param name="pragancyReferancy"></param>
        [Route("api/Account/updatePragancyReferancy")]
        [HttpPost]
        public void updatePragancyReferancy(PragancyReferancyRange pragancyReferancy)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  pregnancyreferencerange SET FirstTrimester='" + pragancyReferancy.FirstTrimester + "',SecondTrimester='" + pragancyReferancy.SecondTrimester + "',ThirdTrimester='" + pragancyReferancy.ThirdTrimester + "' where PregnancyId='" + pragancyReferancy.PregnancyId + "' && TestCode='" + pragancyReferancy.TestCode + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region getAgewisecricticalreference
        /// <summary>
        /// Table - agewisecricticalreference
        /// </summary>
        /// Listed all Details from agewisecricticalreference table by using TestCode        
        ///<param name="TestCode"></param>       
        [Route("api/Account/getAgewisecricticalreference")]
        [HttpGet]
        public List<AgewiseCricticalReferences> getAgewisecricticalreference(string TestCode)
        {
            List<AgewiseCricticalReferences> lstAgewiseCricticalReferences = new List<AgewiseCricticalReferences>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQLs = "SELECT * FROM agewisecricticalreference where TestCode='" + TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conn);
                    MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                    DataSet ds = new DataSet();
                    mydatas.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow drs in dt.Rows)
                        {
                            if (drs != null)
                            {
                                AgewiseCricticalReferences agewiseCricticalReferences = new AgewiseCricticalReferences();
                                agewiseCricticalReferences.AgewiseCricticalValueID = (int)drs["AgewiseCricticalValueID"];
                                agewiseCricticalReferences.TestCode = drs["TestCode"].ToString();
                                agewiseCricticalReferences.TestName = drs["TestName"].ToString();
                                agewiseCricticalReferences.StartDayCrictical = drs["StartDay"].ToString();
                                agewiseCricticalReferences.EndDayCrictical = drs["EndDay"].ToString();
                                agewiseCricticalReferences.StartMonthCrictical = drs["StartMonth"].ToString();
                                agewiseCricticalReferences.EndMonthCrictical = drs["EndMonth"].ToString();
                                agewiseCricticalReferences.StartYearCrictical = drs["StartYear"].ToString();
                                agewiseCricticalReferences.EndYearCrictical = drs["EndYear"].ToString();
                                agewiseCricticalReferences.LowUpperCricticalValue = drs["LowUpperCricticalValue"].ToString();

                                agewiseCricticalReferences.FreeText = drs["FreeText"].ToString();
                                agewiseCricticalReferences.DisplayText = drs["DisplayValue"].ToString();
                                agewiseCricticalReferences.Units = drs["Units"].ToString();
                                agewiseCricticalReferences.ElementName = drs["ElementName"].ToString();
                                agewiseCricticalReferences.Sex = drs["Sex"].ToString();

                                string[] lowHighValue = agewiseCricticalReferences.LowUpperCricticalValue.Split('-');
                                agewiseCricticalReferences.LowCricticalValue = lowHighValue[0];
                                agewiseCricticalReferences.UpperCricticalValue = lowHighValue[1];
                                lstAgewiseCricticalReferences.Add(agewiseCricticalReferences);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstAgewiseCricticalReferences;
            }
        }
        #endregion



        #region updateAgewisecricticalreference 
        /// <summary>
        /// Table - agewisecricticalreference
        /// Update the details to agewisecricticalreference table by using TestCode and AgewiseCricticalValueID.
        /// </summary>
        ///<param name="agewiseCricticalReferences"
        [Route("api/Account/updateAgewisecricticalreference")]
        [HttpPost]
        public void updateAgewisecricticalreference(AgewiseCricticalReferences agewiseCricticalReferences)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  agewisecricticalreference SET StartDay=" + (string.IsNullOrEmpty(agewiseCricticalReferences.StartDayCrictical) ? "null" : agewiseCricticalReferences.StartDayCrictical) + ",EndDay=" + (string.IsNullOrEmpty(agewiseCricticalReferences.EndDayCrictical) ? "null" : agewiseCricticalReferences.EndDayCrictical) + ",StartMonth=" + (string.IsNullOrEmpty(agewiseCricticalReferences.StartMonthCrictical) ? "null" : agewiseCricticalReferences.StartMonthCrictical) + ",EndMonth=" + (string.IsNullOrEmpty(agewiseCricticalReferences.EndMonthCrictical) ? "null" : agewiseCricticalReferences.EndMonthCrictical) + ",StartYear=" + (string.IsNullOrEmpty(agewiseCricticalReferences.StartYearCrictical) ? "null" : agewiseCricticalReferences.StartYearCrictical) + ",EndYear=" + (string.IsNullOrEmpty(agewiseCricticalReferences.EndYearCrictical) ? "null" : agewiseCricticalReferences.EndYearCrictical) + ",LowUpperCricticalValue='" + agewiseCricticalReferences.LowUpperCricticalValue + "',Sex='" + agewiseCricticalReferences.Sex + "',ElementName='" + agewiseCricticalReferences.ElementName + "',FreeText='" + agewiseCricticalReferences.FreeText + "',DisplayValue='" + agewiseCricticalReferences.DisplayText + "' where AgewiseCricticalValueID='" + agewiseCricticalReferences.AgewiseCricticalValueID + "' AND TestCode='" + agewiseCricticalReferences.TestCode + "'";
                    //string strSQL = "UPDATE  agewisecricticalreference SET StartDay='" + agewiseCricticalReferences.StartDayCrictical+ "',EndDay='" + agewiseCricticalReferences.EndDayCrictical + "',StartMonth='" + agewiseCricticalReferences.StartMonthCrictical + "',EndMonth='" + agewiseCricticalReferences.EndMonthCrictical + "',StartYear='" + agewiseCricticalReferences.StartYearCrictical + "',EndYear='" + agewiseCricticalReferences.EndYearCrictical + "',LowUpperCricticalValue='" + agewiseCricticalReferences.LowUpperCricticalValue + "' where AgewiseCricticalValueID='" + agewiseCricticalReferences.AgewiseCricticalValueID + "' && TestCode='" + agewiseCricticalReferences.TestCode + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getAgewiseReferenceValue
        /// <summary>
        /// Table - agewisereferencevalue
        /// </summary>
        /// Listed all Details from agewisereferencevalue table by using TestCode        
        ///<param name="TestCode"></param>       
        [Route("api/Account/getAgewiseReferenceValue")]
        [HttpGet]
        public List<Agewisereferencevalue> getAgewiseReferenceValue(string TestCode)
        {
            List<Agewisereferencevalue> lstAgewiseReferenceValue = new List<Agewisereferencevalue>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQLs = "SELECT * FROM agewisereferencevalue where TestCode='" + TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conn);
                    MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                    DataSet ds = new DataSet();
                    mydatas.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow drs in dt.Rows)
                        {
                            if (drs != null)
                            {
                                Agewisereferencevalue agewiseReferenceValue = new Agewisereferencevalue();
                                agewiseReferenceValue.AgewiseReferenceValueID = (int)drs["AgewiseReferenceValueID"];
                                agewiseReferenceValue.TestCode = drs["TestCode"].ToString();
                                agewiseReferenceValue.TestName = drs["TestName"].ToString();
                                agewiseReferenceValue.ElementName = drs["ElementName"].ToString();
                                agewiseReferenceValue.StartDay = drs["StartDay"].ToString();
                                agewiseReferenceValue.EndDay = drs["EndDay"].ToString();
                                agewiseReferenceValue.StartMonth = drs["StartMonth"].ToString();
                                agewiseReferenceValue.EndMonth = drs["EndMonth"].ToString();
                                agewiseReferenceValue.StartYear = drs["StartYear"].ToString();
                                agewiseReferenceValue.EndYear = drs["EndYear"].ToString();
                                agewiseReferenceValue.LowUpperReferenceValue = drs["LowUpperReferenceValue"].ToString();
                                agewiseReferenceValue.Sex = drs["Sex"].ToString();
                                agewiseReferenceValue.FreeText = drs["FreeText"].ToString();
                                agewiseReferenceValue.DisplayText = drs["DisplayValue"].ToString();
                                string[] lowHighValue = agewiseReferenceValue.LowUpperReferenceValue.Split('-');
                                agewiseReferenceValue.LowReferenceValue = lowHighValue[0];
                                agewiseReferenceValue.UpperReferenceValue = lowHighValue[1];
                                lstAgewiseReferenceValue.Add(agewiseReferenceValue);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstAgewiseReferenceValue;
            }
        }
        #endregion

        #region updateAgewiseReferenceValue 
        /// <summary>
        /// Table - agewisereferencevalue
        /// Update the details to agewisereferencevalue table by using TestCode and AgewiseReferenceValueID.
        /// </summary>
        ///<param name="agewisereferencevalue"
        [Route("api/Account/updateAgewiseReferenceValue")]
        [HttpPost]
        public void updateAgewiseReferenceValue(Agewisereferencevalue agewisereferencevalue)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "UPDATE  agewisereferencevalue SET StartDay=" + (string.IsNullOrEmpty(agewisereferencevalue.StartDay) ? "null" : agewisereferencevalue.StartDay) + ",EndDay=" + (string.IsNullOrEmpty(agewisereferencevalue.EndDay) ? "null" : agewisereferencevalue.EndDay) + ",StartMonth=" + (string.IsNullOrEmpty(agewisereferencevalue.StartMonth) ? "null" : agewisereferencevalue.StartMonth) + ",EndMonth=" + (string.IsNullOrEmpty(agewisereferencevalue.EndMonth) ? "null" : agewisereferencevalue.EndMonth) + ",StartYear=" + (string.IsNullOrEmpty(agewisereferencevalue.StartYear) ? "null" : agewisereferencevalue.StartYear) + ",EndYear=" + (string.IsNullOrEmpty(agewisereferencevalue.EndYear) ? "null" : agewisereferencevalue.EndYear) + ",LowUpperReferenceValue='" + agewisereferencevalue.LowUpperReferenceValue + "',Sex='" + agewisereferencevalue.Sex + "',FreeText='" + agewisereferencevalue.FreeText + "',DisplayValue='" + agewisereferencevalue.DisplayText + "'where AgewiseReferenceValueID='" + agewisereferencevalue.AgewiseReferenceValueID + "' && TestCode='" + agewisereferencevalue.TestCode + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region insertCalculationForTestDetails
        /// <summary>
        /// Table 
        /// </summary>
        /// 
        /// <param name=""></param>
        [Route("api/Account/insertCalculationForTestDetails")]
        [HttpPost]
        public void insertCalculationForTestDetails(calculationForTestDetails calculationfortestdetails)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.calculationForTestDetails(testCode,splcalculation,units,FormulaLabel,CalculationType,TestCodesCalculationPart,Elements,CalculationCategory,NormalValues,ElementsCalculationType) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10)";
                    conn.Open();
                    
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", calculationfortestdetails.testCode);
                    cmd.Parameters.AddWithValue("@val2", calculationfortestdetails.splcalculation);
                    cmd.Parameters.AddWithValue("@val3", calculationfortestdetails.CalculationUnits);
                    cmd.Parameters.AddWithValue("@val4", calculationfortestdetails.FormulaLabel);
                    cmd.Parameters.AddWithValue("@val5", calculationfortestdetails.CalculationType);
                    cmd.Parameters.AddWithValue("@val6", calculationfortestdetails.TestCodesCalculationPart);
                    cmd.Parameters.AddWithValue("@val7", calculationfortestdetails.ElementName);
                    cmd.Parameters.AddWithValue("@val8", calculationfortestdetails.CalculationCategory);
                    cmd.Parameters.AddWithValue("@val9", calculationfortestdetails.NormalValues);
                    cmd.Parameters.AddWithValue("@val10", calculationfortestdetails.ElementsCalculationType);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region insertAgewiseSexReferenceValue
        /// <summary>
        /// table - agewisesexreferencevalue
        /// </summary>
        /// <param name="agewiseSexReferencevalue"></param>
        [Route("api/Account/insertAgewiseSexReferenceValue")]
        [HttpPost]
        public void insertAgewiseSexReferenceValue(AgewiseSexReferencevalue agewiseSexReferencevalue)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO agewisesexreferencevalue(TestCode,TestName,StartDay,EndDay,StartMonth,EndMonth,StartYear,EndYear,LowUpperSexReferenceValue,FreeText,Units,DisplayValue,ElementName) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", agewiseSexReferencevalue.TestCode);
                    cmd.Parameters.AddWithValue("@val2", agewiseSexReferencevalue.TestName);
                    cmd.Parameters.AddWithValue("@val3", agewiseSexReferencevalue.StartDay);
                    cmd.Parameters.AddWithValue("@val4", agewiseSexReferencevalue.EndDay);
                    cmd.Parameters.AddWithValue("@val5", agewiseSexReferencevalue.StartMonth);
                    cmd.Parameters.AddWithValue("@val6", agewiseSexReferencevalue.EndMonth);
                    cmd.Parameters.AddWithValue("@val7", agewiseSexReferencevalue.StartYear);
                    cmd.Parameters.AddWithValue("@val8", agewiseSexReferencevalue.EndYear);
                    cmd.Parameters.AddWithValue("@val9", agewiseSexReferencevalue.LowUpperSexReferenceValue);
                    cmd.Parameters.AddWithValue("@val10", agewiseSexReferencevalue.FreeText);
                    cmd.Parameters.AddWithValue("@val11", agewiseSexReferencevalue.Units);
                    cmd.Parameters.AddWithValue("@val12", agewiseSexReferencevalue.DisplayText);
                    cmd.Parameters.AddWithValue("@val13", agewiseSexReferencevalue.ElementName);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion

        #region UploadImageFiles
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/UploadImageFiles")]
        [HttpPost]
        public List<ImageforTest> UploadImageFiles()
        {
            List<ImageforTest> lstImageforTest = new List<ImageforTest>();
            int iUploadedCnt = 0;
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.           
            string physicalGrandparentPath = "";
            //Deepika
            //physicalGrandparentPath = @"D:/Program Files/Apache Software FoundationTomcat 7.0/webapps/Jan31/LabTestImage/";

            //Vignesh
            //physicalGrandparentPath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/hospital/1/LabTestImage/";

            // kalaivani    
           // physicalGrandparentPath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/Patho/LabTestImage/";
            physicalGrandparentPath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/New/LabTestImage/";

            //Server
          //  physicalGrandparentPath = @"E:/Apache Software Foundation Tomcat 7.0/webapps/pathoServer/LabTestImage/";

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];
                ImageforTest imageDetails = new ImageforTest();
                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(physicalGrandparentPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(physicalGrandparentPath + Path.GetFileName(hpf.FileName));
                        imageDetails.iUploadedCnt = iUploadedCnt = iUploadedCnt + 1;
                        imageDetails.ImagePath = physicalGrandparentPath + Path.GetFileName(hpf.FileName);
                        imageDetails.ImageName = Path.GetFileName(hpf.FileName);
                        imageDetails.UploadStatus = "1";
                        lstImageforTest.Add(imageDetails);
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return lstImageforTest;
            }
            else
            {
                return lstImageforTest;
            }
        }
        #endregion


        #region insertImageforTest
        /// <summary>
        /// Table - imagefortest
        /// </summary>    
        ///<param name="imageforTestdetails"></param>
        [Route("api/Account/insertImageforTest")]
        [HttpPost]
        public void insertImageforTest(ImageforTest imageforTestdetails)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.imagefortest(TestCode,RegID,MrdNo,ImageByte,ImageName,ImagePath,UploadStatus,TestName) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", imageforTestdetails.TestCode);
                    cmd.Parameters.AddWithValue("@val2", imageforTestdetails.RegID);
                    cmd.Parameters.AddWithValue("@val3", imageforTestdetails.MrdNo);
                    cmd.Parameters.AddWithValue("@val4", imageforTestdetails.ImageByte);
                    cmd.Parameters.AddWithValue("@val5", imageforTestdetails.ImageName);
                    cmd.Parameters.AddWithValue("@val6", imageforTestdetails.ImagePath);
                    cmd.Parameters.AddWithValue("@val7", imageforTestdetails.UploadStatus);
                    cmd.Parameters.AddWithValue("@val8", imageforTestdetails.TestName);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region
        /// <summary>
        /// Table - imagefortest
        /// </summary>
        /// <param name="TestCode"></param>
        /// <returns></returns>
        [Route("api/Account/getAllImagebyTest")]
        [HttpGet]
        public List<ImageforTest> getAllImagebyTest(string TestCode)
        {
            List<ImageforTest> lstImageforTest = new List<ImageforTest>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.imagefortest where TestCode = '" + TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ImageforTest imageTest = new ImageforTest();
                            imageTest.ImageId = (int)dr["ImageId"];
                            imageTest.TestCode = dr["TestCode"].ToString();
                            //imageTest.RegID = (int)dr["RegID"];
                            //imageTest.MrdNo = dr["MrdNo"].ToString();
                            //imageTest.ImageByte = Convert.ToByte(dr["ImageByte"]);
                            imageTest.ImageName = dr["ImageName"].ToString();
                            imageTest.ImagePath = dr["ImagePath"].ToString();
                            //imageTest.UploadStatus = dr["UploadStatus"].ToString();
                            imageTest.TestName = dr["TestName"].ToString();
                            lstImageforTest.Add(imageTest);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstImageforTest;
            }
        }
        #endregion

        #region getAllReferredByDoctorUsingProviderName
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getAllReferredByDoctorUsingProviderName")]
        [HttpGet]
        public List<LabOrder> getAllReferredByDoctorUsingProviderName(string providername)
        {

            List<LabOrder> lstrefDoctor = new List<LabOrder>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.laborder where ProviderName = '" + providername + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrder refDoctor = new LabOrder();
                            refDoctor.ReferredBy = dr["ReferredBy"].ToString();
                            lstrefDoctor.Add(refDoctor);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstrefDoctor;
            }
        }
        #endregion


        #region getTodaypatientDetailsByDoctorName
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getTodaypatientDetailsByDoctorName")]
        [HttpGet]
        public List<LabOrder> getTodaypatientDetailsByDoctorName(string docname)
        {

            string todaydate = DateTime.Now.Date.ToString("yyyy-MM-dd");

            // string test = todaydate.ToShortDateString();
            List<LabOrder> lstrefDoctor = new List<LabOrder>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.laborder where ReferredBy = '" + docname + "' AND date(CreateDate) = '" + todaydate + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            LabOrder refDoctor = new LabOrder();
                            refDoctor.Amount = (double)dr["Amount"];
                            refDoctor.ReferredBy = dr["ReferredBy"].ToString();
                            refDoctor.PatientName = dr["PatientName"].ToString();
                            refDoctor.MrdNo = dr["MrdNo"].ToString();
                            lstrefDoctor.Add(refDoctor);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstrefDoctor;
            }
        }
        #endregion


        #region getReportLabTestResultByMrdNoViewClaculation
        /// <summary>
        /// Table - resultlabtest
        /// </summary>
        /// get pending amount values from resultlabtest table      
        /// <returns></returns>
        [Route("api/Account/getReportLabTestResultByMrdNoViewClaculation")]
        [HttpGet]
        public List<ResultLabTech> getReportLabTestResultByMrdNoViewClaculation(string mrdNo)
        {
            List<ResultLabTech> lstResultLabTech = new List<ResultLabTech>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest where MrdNo='" + mrdNo + "' and TestCode IS NULL";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.MrdNo = dr["MrdNo"].ToString();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.TestCode = dr["TestCode"].ToString();
                            resultLabTech.NormalValues = dr["DefaultValues"].ToString();
                            resultLabTech.Units = dr["Units"].ToString();
                            resultLabTech.Comment = dr["Comment1"].ToString();
                            resultLabTech.Comment2 = dr["Comment2"].ToString();
                            resultLabTech.SpecialComments = dr["SpecialComments"].ToString();
                            resultLabTech.EndRange = dr["EndRange"].ToString();
                            resultLabTech.StartRange = dr["StartRange"].ToString();
                            resultLabTech.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            resultLabTech.ActualValue = dr["ActualValue"].ToString();
                            resultLabTech.CalculationFormula = dr["CalculationFormula"].ToString();
                            resultLabTech.CalculationInformation = dr["CalculationInformation"].ToString();
                            resultLabTech.CalculationUnits = dr["CalculationUnits"].ToString();
                            resultLabTech.DisplayValueText = dr["DisplayValueText"].ToString();
                            lstResultLabTech.Add(resultLabTech);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion

        #region getTestMultipleComponentsforLabView
        /// <summary>
        /// Table - testmultiplecomponents
        /// </summary>
        /// Listed all values from testmultiplecomponents table by using TestCode
        /// <returns></returns>
        [Route("api/Account/getTestMultipleComponentsforLabView")]
        [HttpGet]
        public List<TestMultipleComponents> getTestMultipleComponentsforLabView(string TestCode)
        {
            List<TestMultipleComponents> lstMultipleComponents = new List<TestMultipleComponents>();
            DataTable dt = new DataTable();
            //int i = 1;

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    using (MySqlConnection conns = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
                    {
                        string strSQLs = "SELECT * FROM testmultiplecomponents where TestCode='" + TestCode + "' Order by PriorityStatus asc";
                        conns.Open();
                        MySqlDataAdapter mydatas = new MySqlDataAdapter(strSQLs, conns);
                        MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydatas);
                        DataSet ds = new DataSet();
                        mydatas.Fill(ds);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow drs in dt.Rows)
                            {
                                if (drs != null)
                                {
                                    TestMultipleComponents MultipleComponents = new TestMultipleComponents();
                                    MultipleComponents.TestMultipleComponentsID = (int)drs["TestMultipleComponentsID"];
                                    MultipleComponents.TestCode = drs["TestCode"].ToString();
                                    MultipleComponents.ElementName = drs["ElementName"].ToString();
                                    MultipleComponents.Color = drs["Color"].ToString();
                                    MultipleComponents.Units = drs["Units"].ToString();
                                    MultipleComponents.Comments = drs["Comments"].ToString();
                                    MultipleComponents.CriticalLow = drs["CriticalLow"].ToString();
                                    MultipleComponents.CriticalHigh = drs["CriticalHigh"].ToString();
                                    MultipleComponents.ReferenceHigh = drs["ReferenceLow"].ToString();
                                    MultipleComponents.ReferenceLow = drs["ReferenceHigh"].ToString();
                                    MultipleComponents.PriorityStatus = Convert.ToInt32(drs["PriorityStatus"]);
                                    MultipleComponents.Methodology = drs["Methodology"].ToString();

                                    lstMultipleComponents.Add(MultipleComponents);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstMultipleComponents;
            }
        }
        #endregion


        #region getCalculationByTestCodeandElement
        /// <summary>
        /// Table - calculationfortestdetails
        /// </summary>
        /// get details from calculationfortestdetails table by testCode and ElementName      
        /// <returns></returns>
        [Route("api/Account/getCalculationByTestCodeandElement")]
        [HttpGet]
        public List<calculationForTestDetails> getCalculationByTestCodeandElement(string testCode, string ElementName)
        {
            List<calculationForTestDetails> lstTestCalculation = new List<calculationForTestDetails>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * from  pathoclinic.calculationfortestdetails where testCode='" + testCode + "' AND Elements='" + ElementName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            calculationForTestDetails testCalculation = new calculationForTestDetails();
                            testCalculation.testCode = dr["TestCode"].ToString();
                            testCalculation.CalculationUnits = dr["units"].ToString();
                            testCalculation.FormulaLabel = dr["formulaLabel"].ToString();
                            testCalculation.splcalculation = dr["splcalculation"].ToString();
                            testCalculation.CalculationType = dr["CalculationType"].ToString();
                            testCalculation.TestCodesCalculationPart = dr["TestCodesCalculationPart"].ToString();
                            testCalculation.ElementName = dr["Elements"].ToString();
                            testCalculation.CalculationCategory = dr["CalculationCategory"].ToString();
                            testCalculation.NormalValues = dr["NormalValues"].ToString();
                            testCalculation.ElementsCalculationType = dr["ElementsCalculationType"].ToString();
                            lstTestCalculation.Add(testCalculation);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstTestCalculation;
            }
        }
        #endregion

        #region UploadSignatureImages
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/UploadSignatureImages")]
        [HttpPost]
        public List<Login> UploadSignatureImages()
        {
            List<Login> lstSignatureImageforLogin = new List<Login>();
            int iUploadedCnt = 0;
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.           
            string physicalGrandparentPath = "";
            //Deepika System path 
            //physicalGrandparentPath = @"D:/Program Files/Apache Software FoundationTomcat 7.0/webapps/Jan31/SignatureImages/";

            //Vignesh System path 
            //physicalGrandparentPath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/hospital/1/SignatureImages/";

            //kalaivani
            //  physicalGrandparentPath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/Patho/SignatureImages/";
             // physicalGrandparentPath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/New/SignatureImages/";

            //Server System path
           //  physicalGrandparentPath = @"E:/Apache Software Foundation Tomcat 7.0/webapps/pathoServer/SignatureImages/";

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];
                Login loginSignature = new Login();
                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(physicalGrandparentPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(physicalGrandparentPath + Path.GetFileName(hpf.FileName));
                        loginSignature.SignatureImagePath = physicalGrandparentPath + Path.GetFileName(hpf.FileName);
                        loginSignature.SignatureImageName = Path.GetFileName(hpf.FileName);
                        lstSignatureImageforLogin.Add(loginSignature);
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return lstSignatureImageforLogin;
            }
            else
            {
                return lstSignatureImageforLogin;
            }
        }
        #endregion

        #region getsignUpDetailswithSignature
        /// <summary>
        /// Table - login
        /// to get User Signature
        /// </summary>
        /// <param name="userName"></param>       
        [Route("api/Account/getsignUpDetailswithSignature")]
        [HttpGet]
        public Login getsignUpDetailswithSignature(string userName)
        {
            Login loginDetails = new Login();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * from pathoclinic.login where UserName='" + userName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            loginDetails.SignatureImagePath = dr["SignatureImagePath"].ToString();
                            loginDetails.Role = dr["Role"].ToString();
                            loginDetails.SignatureImageName = dr["SignatureImageName"].ToString();
                            loginDetails.RoleId = (int)dr["RoleId"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return loginDetails;
            }
        }
        #endregion

        #region insertMultipleComponentswithCalculation
        /// <summary>
        /// table - multiplecomponentwithcalculation
        /// </summary>
        /// <param name="multiplecomponentwithcalculation"></param>
        [Route("api/Account/insertMultipleComponentswithCalculation")]
        [HttpPost]
        public void insertMultipleComponentswithCalculation(MultipleComponentswithCalculation multipleComponentsCalculation)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.multiplecomponentwithcalculation Where MrdNo='" + multipleComponentsCalculation.MrdNo + "' && TestCode ='" + multipleComponentsCalculation.TestCode + "' && ElementName='" + multipleComponentsCalculation.ElementName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string strSQL1 = "UPDATE pathoclinic.multiplecomponentwithcalculation SET Result=@Result,Notes=@Notes,Comments=@Comments Where MrdNo='" + multipleComponentsCalculation.MrdNo + "' AND TestCode ='" + multipleComponentsCalculation.TestCode + "' AND ElementName='" + multipleComponentsCalculation.ElementName + "'";
                        //  conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.Parameters.AddWithValue("@Result", multipleComponentsCalculation.Result);
                        cmd1.Parameters.AddWithValue("@Comments", multipleComponentsCalculation.Comments);
                        cmd1.Parameters.AddWithValue("@Notes", multipleComponentsCalculation.Notes);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        string strSQL1 = "INSERT INTO pathoclinic.multiplecomponentwithcalculation(MrdNo,TestCode,TestName,ElementName,Calculation,Result,Notes,Comments,ActualValue,NormalValues,Units,DisplayValue,CreateDate,RegId,PriorityStatus,Methodology,FreeText,NormalValuesFiled) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9,@val10,@val11,@val12,@val13,@val14,@val15,@val16,@val17,@val18)";
                        //conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.Parameters.AddWithValue("@val1", multipleComponentsCalculation.MrdNo);
                        cmd1.Parameters.AddWithValue("@val2", multipleComponentsCalculation.TestCode);
                        cmd1.Parameters.AddWithValue("@val3", multipleComponentsCalculation.TestName);
                        cmd1.Parameters.AddWithValue("@val4", multipleComponentsCalculation.ElementName);
                        cmd1.Parameters.AddWithValue("@val5", multipleComponentsCalculation.Calculation);
                        cmd1.Parameters.AddWithValue("@val6", multipleComponentsCalculation.Result);
                        cmd1.Parameters.AddWithValue("@val7", multipleComponentsCalculation.Notes);
                        cmd1.Parameters.AddWithValue("@val8", multipleComponentsCalculation.Comments);
                        cmd1.Parameters.AddWithValue("@val9", multipleComponentsCalculation.ActualValue);
                        cmd1.Parameters.AddWithValue("@val10", multipleComponentsCalculation.NormalValues);
                        cmd1.Parameters.AddWithValue("@val11", multipleComponentsCalculation.Units);
                        cmd1.Parameters.AddWithValue("@val12", multipleComponentsCalculation.DisplayValue);
                        cmd1.Parameters.AddWithValue("@val13", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@val14", multipleComponentsCalculation.RegId);
                        cmd1.Parameters.AddWithValue("@val15", multipleComponentsCalculation.PriorityStatus);
                        cmd1.Parameters.AddWithValue("@val16", multipleComponentsCalculation.Methodology);
                        cmd1.Parameters.AddWithValue("@val17", multipleComponentsCalculation.FreeText);
                        cmd1.Parameters.AddWithValue("@val18", multipleComponentsCalculation.NormalValuesFiled);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion



        #region getgroupcount
        /// <summary>
        /// Table - LaborderStatus
        /// </summary>
        ///  get All Completed status details from LaborderStatus table.
        /// <returns></returns>
        [Route("api/Account/getgroupcount")]
        [HttpGet]
        public string getgroupcount(string ProviderHostName)
        {
            string result = "";
            int labcount = 0;
            int groupCount = 0;
            //LabOrder labcount = new LabOrder();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT count(*) FROM pathoclinic.laborder where ProviderName = 'Group' AND ProviderHostName = '" + ProviderHostName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            labcount = Convert.ToInt32(dr[0]);
                        }
                    }

                    string strSQL1 = "SELECT * FROM pathoclinic.Group where GroupName ='" + ProviderHostName + "'";
                   // conn.Open();
                    MySqlDataAdapter mydata1 = new MySqlDataAdapter(strSQL1, conn);
                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata1);
                    DataSet ds1= new DataSet();
                    mydata1.Fill(ds1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (dr1 != null)
                        {

                            groupCount = Convert.ToInt32(dr1["NoOfPerson"]);
                        }
                    }

                    if (labcount >= groupCount)
                    {
                        result = "false";

                    }
                    else
                    {
                        result = "true";
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return result;
            }
        }
        #endregion


 
        #region getgroupcountRemain
        /// <summary>
        /// Table - LaborderStatus
        /// </summary>
        ///  get All Completed status details from LaborderStatus table.
        /// <returns></returns>
        [Route("api/Account/getgroupcountRemain")]
        [HttpGet]
        public int getgroupcountRemain(string ProviderHostName)
        {
            int result = 0;
            int labcount = 0;
            int groupCount = 0;
            //LabOrder labcount = new LabOrder();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT count(*) FROM pathoclinic.laborder where ProviderName = 'Group' AND ProviderHostName = '" + ProviderHostName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            labcount = Convert.ToInt32(dr[0]);
                        }
                    }

                    string strSQL1 = "SELECT * FROM pathoclinic.Group where GroupName ='" + ProviderHostName + "'";
                   // conn.Open();
                    MySqlDataAdapter mydata1 = new MySqlDataAdapter(strSQL1, conn);
                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata1);
                    DataSet ds1= new DataSet();
                    mydata1.Fill(ds1);
                    dt1 = ds1.Tables[0];
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (dr1 != null)
                        {

                            groupCount = Convert.ToInt32(dr1["NoOfPerson"]);
                        }
                    }

                    result = groupCount - labcount;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return result;
            }
        }
        #endregion


        #region getgroupcountFilterByRegId
        /// <summary>
        /// Table - LaborderStatus
        /// </summary>
        ///  get All Completed status details from LaborderStatus table.
        /// <returns></returns>
        [Route("api/Account/getgroupcountFilterByRegId")]
        [HttpGet]
        public int getgroupcountFilterByRegId(int RegID, string ProviderHostName)
        {
            int labcount = 0;
            //LabOrder labcount = new LabOrder();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT count(*) FROM pathoclinic.laborder   where  ProviderName = 'Group' AND ProviderHostName = '" + ProviderHostName + "'";
                    string strSQL = "SELECT count(*) FROM pathoclinic.laborder  where ProviderName = 'Group' AND ProviderHostName = '" + ProviderHostName + "' AND RegID = '" + RegID + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            labcount = Convert.ToInt32(dr[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return labcount;
            }
        }
        #endregion


        #region getAllAgeRefferencesByTestCodeMultipleComponents
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// Listed all values from LabTestList,childtestlist table by MrdNo
        /// <returns></returns>
        [Route("api/Account/getAllAgeRefferencesByTestCodeMultipleComponents")]
        [HttpGet]
        public List<Agewisereferencevalue> getAllAgeRefferencesByTestCodeMultipleComponents(string testCode, string elementName, string dob, string age, string sex)
        {
            List<Agewisereferencevalue> allReferenceRanges = new List<Agewisereferencevalue>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "";
                    DataTable dtDefault = new DataTable();
                    strSQL = "SELECT * FROM agewisereferencevalue where StartYear <= 0  and EndYear >= 200 and TestCode = '" + testCode + "' and ElementName ='" + elementName + "'";
                    conn.Open();
                    MySqlDataAdapter mydataDefault = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmdDefault = new MySqlCommandBuilder(mydataDefault);
                    DataSet dsDefault = new DataSet();
                    mydataDefault.Fill(dsDefault);
                    dtDefault = dsDefault.Tables[0];
                    if (dtDefault.Rows.Count == 0)
                    {

                        if (Convert.ToInt32(age) >= 1)
                        {
                            strSQL = "SELECT * FROM agewisereferencevalue where StartYear <= '" + Convert.ToInt32(age) + "'  and EndYear >= '" + Convert.ToInt32(age) + "' and TestCode = '" + testCode + "' and ElementName ='" + elementName + "'";
                        }
                        else
                        {
                            DateTime dateOfBirth = Convert.ToDateTime(dob);
                            DateTime todayDate = DateTime.Now;
                            double daysDifferent = (todayDate - dateOfBirth).TotalDays;
                            if (daysDifferent >= 31)
                            {
                                double monthDifferent = daysDifferent / 31;
                                strSQL = "SELECT * FROM agewisereferencevalue where StartMonth <= '" + Convert.ToInt32(monthDifferent) + "'  and EndMonth >= '" + Convert.ToInt32(monthDifferent) + "' and TestCode = '" + testCode + "' and ElementName ='" + elementName + "'";

                            }

                            else
                            {

                                strSQL = "SELECT * FROM agewisereferencevalue where StartDay <= '" + Convert.ToInt32(daysDifferent) + "'  and EndDay >= '" + Convert.ToInt32(daysDifferent) + "' and TestCode = '" + testCode + "' and ElementName ='" + elementName + "'";
                            }
                        }


                        //conn.Open();
                        MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                        MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                        DataSet ds = new DataSet();
                        mydata.Fill(ds);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count != 1)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr != null)
                                {

                                    if (Convert.ToInt32(age) >= 1)
                                    {
                                        strSQL = "SELECT * FROM agewisereferencevalue where StartYear <= '" + Convert.ToInt32(age) + "'  and EndYear >= '" + Convert.ToInt32(age) + "' and TestCode = '" + testCode + "' and Sex='" + sex + "' and ElementName ='" + elementName + "'";

                                    }
                                    else
                                    {
                                        DateTime dateOfBirth = Convert.ToDateTime(dob);
                                        DateTime todayDate = DateTime.Now;
                                        double daysDifferent = (todayDate - dateOfBirth).TotalDays;
                                        if (daysDifferent >= 31)
                                        {
                                            double monthDifferent = daysDifferent / 31;
                                            strSQL = "SELECT * FROM agewisereferencevalue where StartMonth <= '" + Convert.ToInt32(monthDifferent) + "'  and EndMonth >= '" + Convert.ToInt32(monthDifferent) + "' and TestCode = '" + testCode + "' and Sex='" + sex + " and ElementName ='" + elementName + "'";

                                        }

                                        else
                                        {

                                            strSQL = "SELECT * FROM agewisereferencevalue where StartDay <= '" + Convert.ToInt32(daysDifferent) + "'  and EndDay >= '" + Convert.ToInt32(daysDifferent) + "' and TestCode = '" + testCode + "'and Sex='" + sex + "' and ElementName ='" + elementName + "'";
                                        }
                                    }
                                    DataTable dta = new DataTable();
                                    MySqlDataAdapter mydataa = new MySqlDataAdapter(strSQL, conn);
                                    MySqlCommandBuilder cmda = new MySqlCommandBuilder(mydataa);
                                    DataSet dsa = new DataSet();
                                    mydataa.Fill(dsa);
                                    dta = dsa.Tables[0];
                                    foreach (DataRow dra in dta.Rows)
                                    {
                                        if (dra != null)
                                        {
                                            Agewisereferencevalue agewiseReferenceValue1 = new Agewisereferencevalue();

                                            agewiseReferenceValue1.LowUpperReferenceValue = dra["LowUpperReferenceValue"].ToString();
                                            agewiseReferenceValue1.DisplayText = dra["DisplayValue"].ToString();
                                            agewiseReferenceValue1.FreeText = dra["FreeText"].ToString();
                                            agewiseReferenceValue1.StartYear = dra["StartYear"].ToString();
                                            agewiseReferenceValue1.EndYear = dra["EndYear"].ToString();
                                            agewiseReferenceValue1.StartMonth = dra["StartMonth"].ToString();
                                            agewiseReferenceValue1.EndMonth = dra["EndMonth"].ToString();
                                            agewiseReferenceValue1.StartDay = dra["StartDay"].ToString();
                                            agewiseReferenceValue1.EndDay = dra["EndDay"].ToString();

                                            if (agewiseReferenceValue1.StartYear != null && agewiseReferenceValue1.EndYear != null)
                                            {

                                                agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartYear + "-" + agewiseReferenceValue1.EndYear;
                                            }
                                            else if (agewiseReferenceValue1.StartDay == null && agewiseReferenceValue1.EndDay == null)
                                            {
                                                agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartDay + "-" + agewiseReferenceValue1.EndDay;
                                            }
                                            else
                                            {
                                                agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartMonth + "-" + agewiseReferenceValue1.EndMonth;
                                            }
                                            allReferenceRanges.Clear();
                                            allReferenceRanges.Add(agewiseReferenceValue1);

                                        }
                                    }
                                }

                            }
                        }
                        else if (dt.Rows.Count == 1)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                Agewisereferencevalue agewiseReferenceValue = new Agewisereferencevalue();

                                agewiseReferenceValue.LowUpperReferenceValue = dr["LowUpperReferenceValue"].ToString();
                                agewiseReferenceValue.DisplayText = dr["DisplayValue"].ToString();
                                agewiseReferenceValue.FreeText = dr["FreeText"].ToString();
                                agewiseReferenceValue.StartYear = dr["StartYear"].ToString();
                                agewiseReferenceValue.EndYear = dr["EndYear"].ToString();
                                agewiseReferenceValue.StartMonth = dr["StartMonth"].ToString();
                                agewiseReferenceValue.EndMonth = dr["EndMonth"].ToString();
                                agewiseReferenceValue.StartDay = dr["StartDay"].ToString();
                                agewiseReferenceValue.EndDay = dr["EndDay"].ToString();

                                if (agewiseReferenceValue.StartYear != null && agewiseReferenceValue.EndYear != null)
                                {

                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartYear + "-" + agewiseReferenceValue.EndYear;
                                }
                                else if (agewiseReferenceValue.StartDay == null && agewiseReferenceValue.EndDay == null)
                                {
                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartDay + "-" + agewiseReferenceValue.EndDay;
                                }
                                else
                                {
                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartMonth + "-" + agewiseReferenceValue.EndMonth;
                                }

                                allReferenceRanges.Add(agewiseReferenceValue);
                            }
                        }
                    }
                    else
                    {
                        if (dtDefault.Rows.Count == 1)
                        {
                            foreach (DataRow dr in dtDefault.Rows)
                            {
                                Agewisereferencevalue agewiseReferenceValue = new Agewisereferencevalue();

                                agewiseReferenceValue.LowUpperReferenceValue = dr["LowUpperReferenceValue"].ToString();
                                agewiseReferenceValue.DisplayText = dr["DisplayValue"].ToString();
                                agewiseReferenceValue.StartYear = dr["StartYear"].ToString();
                                agewiseReferenceValue.FreeText = dr["FreeText"].ToString();
                                agewiseReferenceValue.EndYear = dr["EndYear"].ToString();
                                agewiseReferenceValue.StartMonth = dr["StartMonth"].ToString();
                                agewiseReferenceValue.EndMonth = dr["EndMonth"].ToString();
                                agewiseReferenceValue.StartDay = dr["StartDay"].ToString();
                                agewiseReferenceValue.EndDay = dr["EndDay"].ToString();

                                if (agewiseReferenceValue.StartYear != null && agewiseReferenceValue.EndYear != null)
                                {

                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartYear + "-" + agewiseReferenceValue.EndYear;
                                }
                                else if (agewiseReferenceValue.StartDay == null && agewiseReferenceValue.EndDay == null)
                                {
                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartDay + "-" + agewiseReferenceValue.EndDay;
                                }
                                else
                                {
                                    agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartMonth + "-" + agewiseReferenceValue.EndMonth;
                                }

                                allReferenceRanges.Add(agewiseReferenceValue);
                            }

                        }
                        else if (dtDefault.Rows.Count == 2)
                        {
                            strSQL = "SELECT * FROM agewisereferencevalue where StartYear <= 0  and EndYear >= 200 and TestCode = '" + testCode + "' and Sex='" + sex + "' and ElementName ='" + elementName + "'";
                            DataTable dta = new DataTable();
                            MySqlDataAdapter mydataa = new MySqlDataAdapter(strSQL, conn);
                            MySqlCommandBuilder cmda = new MySqlCommandBuilder(mydataa);
                            DataSet dsa = new DataSet();
                            mydataa.Fill(dsa);
                            dta = dsa.Tables[0];
                            foreach (DataRow dra in dta.Rows)
                            {
                                if (dra != null)
                                {
                                    Agewisereferencevalue agewiseReferenceValue1 = new Agewisereferencevalue();

                                    agewiseReferenceValue1.LowUpperReferenceValue = dra["LowUpperReferenceValue"].ToString();
                                    agewiseReferenceValue1.DisplayText = dra["DisplayValue"].ToString();
                                    agewiseReferenceValue1.FreeText = dra["FreeText"].ToString();
                                    agewiseReferenceValue1.StartYear = dra["StartYear"].ToString();
                                    agewiseReferenceValue1.EndYear = dra["EndYear"].ToString();
                                    agewiseReferenceValue1.StartMonth = dra["StartMonth"].ToString();
                                    agewiseReferenceValue1.EndMonth = dra["EndMonth"].ToString();
                                    agewiseReferenceValue1.StartDay = dra["StartDay"].ToString();
                                    agewiseReferenceValue1.EndDay = dra["EndDay"].ToString();

                                    if (agewiseReferenceValue1.StartYear != null && agewiseReferenceValue1.EndYear != null)
                                    {

                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartYear + "-" + agewiseReferenceValue1.EndYear;
                                    }
                                    else if (agewiseReferenceValue1.StartDay == null && agewiseReferenceValue1.EndDay == null)
                                    {
                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartDay + "-" + agewiseReferenceValue1.EndDay;
                                    }
                                    else
                                    {
                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartMonth + "-" + agewiseReferenceValue1.EndMonth;
                                    }
                                    allReferenceRanges.Clear();
                                    allReferenceRanges.Add(agewiseReferenceValue1);

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return allReferenceRanges;
            }
        }
        #endregion


        #region updateParentPaymentReceived
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/updateParentPaymentReceived")]
        [HttpPost]
        public void updateParentPaymentReceived(ParentPaymentReceived parentpayment)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  pathoclinic.parentpaymentreceived SET ReceivedPayment='" + parentpayment.ReceivedPayment + "',PendingAmount='" + parentpayment.PendingAmount + "',PendingNotification='" + parentpayment.PendingNotification + "',PaymentSchedule='" + parentpayment.strPaymentSchedule + "'where InvoiceId='" + parentpayment.InvoiceId + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region getReportMutlipleLabTestResultByMrdNoView
        /// <summary>
        /// Table - multiplecomponentwithcalculation
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportMutlipleLabTestResultByMrdNoView")]
        [HttpGet]
        public List<MultipleComponentswithCalculation> getReportMutlipleLabTestResultByMrdNoView(string mrdNo, string testCode, string elementName)
        {
            List<MultipleComponentswithCalculation> lstResultLabTech = new List<MultipleComponentswithCalculation>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.multiplecomponentwithcalculation where MrdNo='" + mrdNo + "' AND TestCode='" + testCode + "' AND ElementName='" + elementName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            MultipleComponentswithCalculation resultLabTech = new MultipleComponentswithCalculation();
                            resultLabTech.MrdNo = dr["MrdNo"].ToString();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.TestCode = dr["TestCode"].ToString();
                            resultLabTech.NormalValues = dr["NormalValues"].ToString();
                            resultLabTech.ElementName = dr["ElementName"].ToString();
                            resultLabTech.DisplayValue = dr["DisplayValue"].ToString();
                            //    resultLabTech.Comments = dr["Comments"].ToString();
                            //resultLabTech. = dr["Comment2"].ToString();
                            //resultLabTech.SpecialComments = dr["SpecialComments"].ToString();
                            //resultLabTech.EndRange = dr["EndRange"].ToString();
                            //resultLabTech.StartRange = dr["StartRange"].ToString();
                            //resultLabTech.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            lstResultLabTech.Add(resultLabTech);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion


        #region CollectedAt

        #region getAllCollectedAt
        /// <summary>
        ///  Table - collectedAt
        /// </summary>
        ///
        /// <param name="regId"></param>
        /// <returns></returns>
        [Route("api/Account/getAllCollectedAt")]
        [HttpGet]
        public List<CollectedAt> getAllCollectedAt()
        {
            List<CollectedAt> lstCollectedDetails = new List<CollectedAt>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT * FROM pathoclinic.Doctor where RegID='" + regId + "' ";
                    string strSQL = "SELECT * FROM pathoclinic.collectedat";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            CollectedAt collectedatDetails = new CollectedAt();

                            collectedatDetails.RegID = (int)dr["RegID"];
                            collectedatDetails.CollectedName = dr["CollectedName"].ToString();
                            collectedatDetails.Date = dr["Date"].ToString();
                            collectedatDetails.Amount = (int)(dr["Amount"]);

                            lstCollectedDetails.Add(collectedatDetails);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstCollectedDetails;
            }
        }
        #endregion

        #region insertCollectedAtDetails
        /// <summary>
        /// Table - Doctor
        /// Insert the Doctor table values.
        /// </summary>
        /// <param name="docdor"></param>
        [Route("api/Account/insertCollectedAtDetails")]
        [HttpPost]
        public void insertCollectedAtDetails(CollectedAt collectedat)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO collectedat(RegID,CollectedName,Date,Amount) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);

                    cmd.Parameters.AddWithValue("@val1", collectedat.RegID);
                    cmd.Parameters.AddWithValue("@val2", collectedat.CollectedName);
                    cmd.Parameters.AddWithValue("@val3", collectedat.Date);
                    cmd.Parameters.AddWithValue("@val4", collectedat.Amount);


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #endregion



        #region updateSpecialComment
        /// <summary>
        /// table - PatientRegistration
        /// </summary>
        /// <param name="patientRegistration"></param>
        [Route("api/Account/updateSpecialComment")]
        [HttpPost]
        public void updateSpecialComment(PatientRegistration patientRegistration)
        {

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    string strSQL = "UPDATE patientregistration SET SpecialComments = '" + patientRegistration.SpecialComments + "' Where RegID='" + patientRegistration.RegID + "'";
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd = new MySqlCommand(strSQL, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion

        #region insertDisplayTestDetails
        /// <summary>
        /// Table - displaytest
        /// Insert the displaytest table values.
        /// </summary>
        /// <param name="displayTest"></param>
        [Route("api/Account/insertDisplayTestDetails")]
        [HttpPost]
        public void insertDisplayTestDetails(DisplayTest displayTest)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.displaytest(TestName,TestCode,DisplayName,Amount,ExpiryDate) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", displayTest.TestName);
                    cmd.Parameters.AddWithValue("@val2", displayTest.TestCode);
                    cmd.Parameters.AddWithValue("@val3", displayTest.DisplayName);
                    cmd.Parameters.AddWithValue("@val4", displayTest.Amount);
                    cmd.Parameters.AddWithValue("@val5", displayTest.ExpiryDate);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion
        #region getAllDisplayTestDetails
        /// <summary>
        /// Table - displaytest
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllDisplayTestDetails")]
        [HttpGet]
        public List<DisplayTest> getAllDisplayTestDetails()
        {
            List<DisplayTest> lstDisplayTest = new List<DisplayTest>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.displaytest";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            DisplayTest displayTest = new DisplayTest();
                            displayTest.DisplayTestID = (int)dr["DisplayTestID"];
                            displayTest.TestName = dr["TestName"].ToString();
                            displayTest.TestCode = dr["TestCode"].ToString();
                            displayTest.DisplayName = dr["DisplayName"].ToString();
                            displayTest.Amount = Convert.ToDouble(dr["Amount"]);
                            displayTest.ExpiryDate = dr["ExpiryDate"].ToString();
                            lstDisplayTest.Add(displayTest);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstDisplayTest;
            }
        }
        #endregion

        #region updateCalculationforTest 
        /// <summary>
        /// Table - calculationfortestdetails
        /// Update the details to calculationfortestdetails table by using TestCode and ID.
        /// </summary>
        ///<param name="calculationTestDetails"
        [Route("api/Account/updateCalculationforTest")]
        [HttpPost]
        public void updateCalculationforTest(calculationForTestDetails calculationTestDetails)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  pathoclinic.calculationfortestdetails SET splcalculation='" + calculationTestDetails.splcalculation + "',formulaLabel='" + calculationTestDetails.FormulaLabel + "',NormalValues='" + calculationTestDetails.NormalValues + "',units ='"+ calculationTestDetails.CalculationUnits + "' where testCode='" + calculationTestDetails.testCode + "' AND id='" + calculationTestDetails.id + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region updateAlternativeSampleContainer 
        /// <summary>
        /// Table - alternativesamplecontainer
        /// Update the details to alternativesamplecontainer table by using TestCode and ID.
        /// </summary>
        ///<param name="alternativesample"
        [Route("api/Account/updateAlternativeSampleContainer")]
        [HttpPost]
        public void updateAlternativeSampleContainer(Alternativesamplecontainer alternativesample)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  pathoclinic.alternativesamplecontainer SET AlternativeSampleName='" + alternativesample.AlternativeSampleName + "' where TestCode='" + alternativesample.TestCode + "' AND AlternativeSampleContainerId='" + alternativesample.AlternativeSampleContainerId + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region getAllsignUpDetailsSignature
        /// <summary>
        /// Table - login
        /// to get User Signature
        /// </summary>
        /// <param name="userName"></param>       
        [Route("api/Account/getAllsignUpDetailsSignature")]
        [HttpGet]
        public List<Login> getAllsignUpDetailsSignature(string Role)
        {
            List<Login> lstloginDetails = new List<Login>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * from pathoclinic.login where Role='" + Role + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        Login login = new Login();
                        if (dr != null)
                        {
                            login.UserName = dr["UserName"].ToString();
                            login.SignatureImagePath = dr["SignatureImagePath"].ToString();
                            login.Role = dr["Role"].ToString();
                            login.SignatureImageName = dr["SignatureImageName"].ToString();
                            login.RoleId = (int)dr["RoleId"];
                            lstloginDetails.Add(login);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstloginDetails;
            }
        }
        #endregion


        #region getReportMultipleComponentResult
        /// <summary>
        /// Table - multiplecomponentwithcalculation
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getReportMultipleComponentResult")]
        [HttpGet]
        public List<MultipleComponentswithCalculation> getReportMultipleComponentResult(string mrdNo, string testCode)
        {
            List<MultipleComponentswithCalculation> lstMultipleComponentResult = new List<MultipleComponentswithCalculation>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.multiplecomponentwithcalculation where MrdNo='" + mrdNo + "' AND TestCode='" + testCode + "' order by PriorityStatus asc";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            MultipleComponentswithCalculation MultipleComponentResult = new MultipleComponentswithCalculation();
                            MultipleComponentResult.MrdNo = dr["MrdNo"].ToString();
                            MultipleComponentResult.Result = dr["Result"].ToString();
                            MultipleComponentResult.TestName = dr["TestName"].ToString();
                            MultipleComponentResult.TestCode = dr["TestCode"].ToString();
                            MultipleComponentResult.NormalValues = dr["NormalValues"].ToString();
                            MultipleComponentResult.ElementName = dr["ElementName"].ToString();
                            MultipleComponentResult.Units = dr["Units"].ToString();
                            MultipleComponentResult.Comments = dr["Comments"].ToString();
                            MultipleComponentResult.ActualValue = dr["ActualValue"].ToString();
                            MultipleComponentResult.Notes = dr["Notes"].ToString();
                            MultipleComponentResult.DisplayValue = dr["DisplayValue"].ToString();
                            MultipleComponentResult.Calculation = dr["Calculation"].ToString();
                            MultipleComponentResult.Methodology = dr["Methodology"].ToString();
                            MultipleComponentResult.NormalValuesFiled = dr["NormalValuesFiled"].ToString();
                            MultipleComponentResult.PriorityStatus = (int)dr["PriorityStatus"];
                            MultipleComponentResult.Status = (int)dr["Status"];

                            lstMultipleComponentResult.Add(MultipleComponentResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstMultipleComponentResult;
            }
        }
        #endregion


        #region EmailNotification
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        [Route("api/Account/EmailNotification")]
        [HttpPost]
        public bool EmailNotification(MailNotification getmail)
        {
            var emailFrom = "pathogesa@gmail.com";
            var emailPwd = "lidar@123";

            MailAddress from = new MailAddress(emailFrom, "Patho");
            MailAddress to = new MailAddress(getmail.ToMail);
            //MailAddress cc = new MailAddress(getmail.CCMail);
            //MailAddress bcc = new MailAddress(getmail.BCCMail);

            //string fileName = Path.GetFileName(fileUploader.PostedFile.FileName);
            //mail.Attachments.Add(new Attachment(fileUploader.PostedFile.InputStream, fileName));
            using (var mail = new MailMessage(from, to))
            {
                string cc = getmail.CCMail;
                string body = getmail.BodyMail;
                mail.Subject = getmail.MailSubject;

                // mail. = cc;
                mail.IsBodyHtml = true;
                mail.Body = body;
                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailFrom, emailPwd);
                smtp.Port = 587;
                smtp.Send(mail);
                return true;
            }
        }
        #endregion


        #region insertBiospyNumber
        /// <summary>
        /// Table -  BiospyNumberGenerate
        /// Inserted the  LabTestList table values.
        /// </summary>
        /// <param name=" BiospyNumberGenerate"></param>
        [Route("api/Account/insertBiospyNumber")]
        [HttpPost]
        public void insertBiospyNumber(BiospyNumberGenerate biospyNumberGenerate)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO BiospyNumberGenerate(TestCode,MrdNo,Catagory,BiospyNo) VALUES(@val1,@val2,@val3,@val4)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", biospyNumberGenerate.TestCode);
                    cmd.Parameters.AddWithValue("@val2", biospyNumberGenerate.MrdNo);
                    cmd.Parameters.AddWithValue("@val3", biospyNumberGenerate.Catagory);
                    cmd.Parameters.AddWithValue("@val4", biospyNumberGenerate.BiospyNo);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #region getMaxBiospy
        /// <summary>
        /// Table - multiplecomponentwithcalculation
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getMaxBiospy")]
        [HttpGet]
        public List<BiospyNumberGenerate> getMaxBiospy(string category)
        {
            List<BiospyNumberGenerate> lstgetMaxBiospy = new List<BiospyNumberGenerate>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string biospyNoMax = "";
                    string fetch = "Select Max(BiospyNo) from BiospyNumberGenerate where category='" + category + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(fetch, conn);
                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);

                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            biospyNoMax = dr[0].ToString();

                        }
                        if (biospyNoMax.Trim() != "")
                        {
                            var tempArray = biospyNoMax.Trim().Split('/');
                            string tempId = tempArray[0].Substring(2, tempArray[1].Length);
                            int id = Convert.ToInt32(tempId);
                            id = id + 1;
                            biospyNoMax = category + tempId + DateTime.Now.Year.ToString("yy");
                            BiospyNumberGenerate biospyNumberGenerate = new BiospyNumberGenerate();
                            biospyNumberGenerate.BiospyNo = biospyNoMax;
                            lstgetMaxBiospy.Add(biospyNumberGenerate);
                        }

                    }
                    if (dt.Rows.Count == 0)
                    {

                        BiospyNumberGenerate biospyNumberGenerate = new BiospyNumberGenerate();
                        biospyNumberGenerate.BiospyNo = category + 0001 + DateTime.Now.Year.ToString("yy");
                        lstgetMaxBiospy.Add(biospyNumberGenerate);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstgetMaxBiospy;
            }
        }
        #endregion

        #region getAllAgeCriticalByTestCode
        /// <summary>
        /// Table - agewisecricticalreference
        /// </summary>
        /// Listed all values from agewisecricticalreference
        /// <returns></returns>
        [Route("api/Account/getAllAgeCriticalByTestCode")]
        [HttpGet]
        public List<AgewiseCricticalReferences> getAllAgeCriticalByTestCode(string testCode, string dob, string age, string sex)
        {
            List<AgewiseCricticalReferences> allReferenceRanges = new List<AgewiseCricticalReferences>();
            DataTable dt = new DataTable();
            DataTable dtDefault = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "";

                    strSQL = "SELECT * FROM agewisecricticalreference where StartYear <= 0  and EndYear >= 200 and TestCode = '" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydataDefault = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmdDefault = new MySqlCommandBuilder(mydataDefault);
                    DataSet dsDefault = new DataSet();
                    mydataDefault.Fill(dsDefault);
                    dtDefault = dsDefault.Tables[0];

                    if (dtDefault.Rows.Count == 0)
                    {
                        if (Convert.ToInt32(age) >= 1)
                        {
                            strSQL = "SELECT * FROM agewisecricticalreference where StartYear <= '" + Convert.ToInt32(age) + "'  and EndYear >= '" + Convert.ToInt32(age) + "' and TestCode = '" + testCode + "'";

                        }
                        else
                        {
                            DateTime dateOfBirth = Convert.ToDateTime(dob);
                            DateTime todayDate = DateTime.Now;
                            double daysDifferent = (todayDate - dateOfBirth).TotalDays;
                            if (daysDifferent >= 31)
                            {
                                double monthDifferent = daysDifferent / 31;
                                strSQL = "SELECT * FROM agewisecricticalreference where StartMonth <= '" + Convert.ToInt32(monthDifferent) + "'  and EndMonth >= '" + Convert.ToInt32(monthDifferent) + "' and TestCode = '" + testCode + "'";

                            }

                            else
                            {

                                strSQL = "SELECT * FROM agewisecricticalreference where StartDay <= '" + Convert.ToInt32(daysDifferent) + "'  and EndDay >= '" + Convert.ToInt32(daysDifferent) + "' and TestCode = '" + testCode + "'";
                            }
                        }


                        //  conn.Open();
                        MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                        MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                        DataSet ds = new DataSet();
                        mydata.Fill(ds);
                        dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr != null)
                                {
                                    AgewiseCricticalReferences agewiseReferenceValue = new AgewiseCricticalReferences();
                                    agewiseReferenceValue.Sex = dr["Sex"].ToString();
                                    //if (agewiseReferenceValue.Sex == "Both")
                                    //{
                                    agewiseReferenceValue.LowUpperCricticalValue = dr["LowUpperCricticalValue"].ToString();
                                    agewiseReferenceValue.DisplayText = dr["DisplayValue"].ToString();
                                    agewiseReferenceValue.StartYearCrictical = dr["StartYear"].ToString();
                                    agewiseReferenceValue.EndYearCrictical = dr["EndYear"].ToString();
                                    agewiseReferenceValue.StartMonthCrictical = dr["StartMonth"].ToString();
                                    agewiseReferenceValue.EndMonthCrictical = dr["EndMonth"].ToString();
                                    agewiseReferenceValue.StartDayCrictical = dr["StartDay"].ToString();
                                    agewiseReferenceValue.EndDayCrictical = dr["EndDay"].ToString();
                                    agewiseReferenceValue.FreeText = dr["FreeText"].ToString();

                                    if (agewiseReferenceValue.StartYearCrictical != null && agewiseReferenceValue.EndYearCrictical != null)
                                    {

                                        agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartYearCrictical + "-" + agewiseReferenceValue.EndYearCrictical;
                                    }
                                    else if (agewiseReferenceValue.StartDayCrictical == null && agewiseReferenceValue.EndDayCrictical == null)
                                    {
                                        agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartMonthCrictical + "-" + agewiseReferenceValue.EndMonthCrictical;
                                    }
                                    else
                                    {
                                        agewiseReferenceValue.AgeMerge = agewiseReferenceValue.StartMonthCrictical + "-" + agewiseReferenceValue.EndDayCrictical;
                                    }

                                    allReferenceRanges.Add(agewiseReferenceValue);
                                }

                            }
                        }
                        else if (dt.Rows.Count != 1)
                        {
                            if (Convert.ToInt32(age) >= 1)
                            {
                                strSQL = "SELECT * FROM agewisecricticalreference where StartYear <= '" + Convert.ToInt32(age) + "'  and EndYear >= '" + Convert.ToInt32(age) + "' and TestCode = '" + testCode + "'and Sex='" + sex + "'";

                            }
                            else
                            {
                                DateTime dateOfBirth = Convert.ToDateTime(dob);
                                DateTime todayDate = DateTime.Now;
                                double daysDifferent = (todayDate - dateOfBirth).TotalDays;
                                if (daysDifferent >= 31)
                                {
                                    double monthDifferent = daysDifferent / 31;
                                    strSQL = "SELECT * FROM agewisecricticalreference where StartMonth <= '" + Convert.ToInt32(monthDifferent) + "'  and EndMonth >= '" + Convert.ToInt32(monthDifferent) + "' and TestCode = '" + testCode + "' and Sex='" + sex + "'";

                                }

                                else
                                {

                                    strSQL = "SELECT * FROM agewisecricticalreference where StartDay <= '" + Convert.ToInt32(daysDifferent) + "'  and EndDay >= '" + Convert.ToInt32(daysDifferent) + "' and TestCode = '" + testCode + "'and Sex='" + sex + "'";
                                }
                            }
                            DataTable dta = new DataTable();
                            MySqlDataAdapter mydataa = new MySqlDataAdapter(strSQL, conn);
                            MySqlCommandBuilder cmda = new MySqlCommandBuilder(mydataa);
                            DataSet dsa = new DataSet();
                            mydataa.Fill(dsa);
                            dta = dsa.Tables[0];
                            foreach (DataRow dra in dta.Rows)
                            {
                                if (dra != null)
                                {
                                    AgewiseCricticalReferences agewiseReferenceValue1 = new AgewiseCricticalReferences();

                                    agewiseReferenceValue1.LowUpperCricticalValue = dra["LowUpperCricticalValue"].ToString();
                                    agewiseReferenceValue1.DisplayText = dra["DisplayValue"].ToString();
                                    agewiseReferenceValue1.FreeText = dra["FreeText"].ToString();
                                    agewiseReferenceValue1.StartYearCrictical = dra["StartYear"].ToString();
                                    agewiseReferenceValue1.EndYearCrictical = dra["EndYear"].ToString();
                                    agewiseReferenceValue1.StartMonthCrictical = dra["StartMonth"].ToString();
                                    agewiseReferenceValue1.EndYearCrictical = dra["EndMonth"].ToString();
                                    agewiseReferenceValue1.StartDayCrictical = dra["StartDay"].ToString();
                                    agewiseReferenceValue1.EndDayCrictical = dra["EndDay"].ToString();

                                    if (agewiseReferenceValue1.StartYearCrictical != null && agewiseReferenceValue1.EndYearCrictical != null)
                                    {

                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartYearCrictical + "-" + agewiseReferenceValue1.EndYearCrictical;
                                    }
                                    else if (agewiseReferenceValue1.StartDayCrictical == null && agewiseReferenceValue1.EndDayCrictical == null)
                                    {
                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartDayCrictical + "-" + agewiseReferenceValue1.EndDayCrictical;
                                    }
                                    else
                                    {
                                        agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartMonthCrictical + "-" + agewiseReferenceValue1.EndMonthCrictical;
                                    }
                                    allReferenceRanges.Clear();
                                    allReferenceRanges.Add(agewiseReferenceValue1);

                                }
                            }
                        }
                    }
                    else
                        {
                            if (dtDefault.Rows.Count == 1)
                            {
                                foreach (DataRow draa in dtDefault.Rows)
                                {
                                    if (draa != null)
                                    {
                                        AgewiseCricticalReferences agewiseReferenceValue1 = new AgewiseCricticalReferences();

                                        agewiseReferenceValue1.LowUpperCricticalValue = draa["LowUpperCricticalValue"].ToString();
                                        agewiseReferenceValue1.DisplayText = draa["DisplayValue"].ToString();
                                        agewiseReferenceValue1.FreeText = draa["FreeText"].ToString();
                                        agewiseReferenceValue1.StartYearCrictical = draa["StartYear"].ToString();
                                        agewiseReferenceValue1.EndYearCrictical = draa["EndYear"].ToString();
                                        agewiseReferenceValue1.StartMonthCrictical = draa["StartMonth"].ToString();
                                        agewiseReferenceValue1.EndYearCrictical = draa["EndMonth"].ToString();
                                        agewiseReferenceValue1.StartDayCrictical = draa["StartDay"].ToString();
                                        agewiseReferenceValue1.EndDayCrictical = draa["EndDay"].ToString();

                                        if (agewiseReferenceValue1.StartYearCrictical != null && agewiseReferenceValue1.EndYearCrictical != null)
                                        {

                                            agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartYearCrictical + "-" + agewiseReferenceValue1.EndYearCrictical;
                                        }
                                        else if (agewiseReferenceValue1.StartDayCrictical == null && agewiseReferenceValue1.EndDayCrictical == null)
                                        {
                                            agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartDayCrictical + "-" + agewiseReferenceValue1.EndDayCrictical;
                                        }
                                        else
                                        {
                                            agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartMonthCrictical + "-" + agewiseReferenceValue1.EndMonthCrictical;
                                        }
                                        allReferenceRanges.Clear();
                                        allReferenceRanges.Add(agewiseReferenceValue1);

                                    }
                                }
                            }
                            else
                            {
                                DataTable dtb = new DataTable();
                                strSQL = "SELECT * FROM agewisecricticalreference where StartYear <= 0  and EndYear >= 200 and TestCode = '" + testCode + "'";
                                // conn.Open();
                                MySqlDataAdapter mydatab = new MySqlDataAdapter(strSQL, conn);
                                MySqlCommandBuilder cmdb = new MySqlCommandBuilder(mydatab);
                                DataSet dsb = new DataSet();
                                mydatab.Fill(dsb);
                                dtb = dsb.Tables[0];
                                foreach (DataRow drb in dtb.Rows)
                                {
                                    if (drb != null)
                                    {
                                        AgewiseCricticalReferences agewiseReferenceValue1 = new AgewiseCricticalReferences();

                                        agewiseReferenceValue1.LowUpperCricticalValue = drb["LowUpperCricticalValue"].ToString();
                                        agewiseReferenceValue1.DisplayText = drb["DisplayValue"].ToString();
                                        agewiseReferenceValue1.FreeText = drb["FreeText"].ToString();
                                        agewiseReferenceValue1.StartYearCrictical = drb["StartYear"].ToString();
                                        agewiseReferenceValue1.EndYearCrictical = drb["EndYear"].ToString();
                                        agewiseReferenceValue1.StartMonthCrictical = drb["StartMonth"].ToString();
                                        agewiseReferenceValue1.EndYearCrictical = drb["EndMonth"].ToString();
                                        agewiseReferenceValue1.StartDayCrictical = drb["StartDay"].ToString();
                                        agewiseReferenceValue1.EndDayCrictical = drb["EndDay"].ToString();

                                        if (agewiseReferenceValue1.StartYearCrictical != null && agewiseReferenceValue1.EndYearCrictical != null)
                                        {

                                            agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartYearCrictical + "-" + agewiseReferenceValue1.EndYearCrictical;
                                        }
                                        else if (agewiseReferenceValue1.StartDayCrictical == null && agewiseReferenceValue1.EndDayCrictical == null)
                                        {
                                            agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartDayCrictical + "-" + agewiseReferenceValue1.EndDayCrictical;
                                        }
                                        else
                                        {
                                            agewiseReferenceValue1.AgeMerge = agewiseReferenceValue1.StartMonthCrictical + "-" + agewiseReferenceValue1.EndMonthCrictical;
                                        }
                                        allReferenceRanges.Clear();
                                        allReferenceRanges.Add(agewiseReferenceValue1);

                                    }
                                }
                            }

                        }
                   


                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return allReferenceRanges;
            }
        }
        #endregion



        #region getReportLabTestResultByMrdNoTestName
        /// <summary>
        /// Table - resultlabtest
        /// </summary>
        /// get pending amount values from resultlabtest table      
        /// <returns></returns>
        [Route("api/Account/getReportLabTestResultByMrdNoTestName")]
        [HttpGet]
        public List<ResultLabTech> getReportLabTestResultByMrdNoTestName(string mrdNo, string testName)
        {
            List<ResultLabTech> lstResultLabTech = new List<ResultLabTech>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest where MrdNo='" + mrdNo + "' and TestName ='" + testName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.MrdNo = dr["MrdNo"].ToString();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.TestCode = dr["TestCode"].ToString();
                            resultLabTech.NormalValues = dr["DefaultValues"].ToString();
                            resultLabTech.Units = dr["Units"].ToString();
                            resultLabTech.Comment = dr["Comment1"].ToString();
                            resultLabTech.Comment2 = dr["Comment2"].ToString();
                            resultLabTech.SpecialComments = dr["SpecialComments"].ToString();
                            resultLabTech.EndRange = dr["EndRange"].ToString();
                            resultLabTech.StartRange = dr["StartRange"].ToString();
                            resultLabTech.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            resultLabTech.ActualValue = dr["ActualValue"].ToString();
                            resultLabTech.CalculationFormula = dr["CalculationFormula"].ToString();
                            resultLabTech.CalculationInformation = dr["CalculationInformation"].ToString();
                            resultLabTech.CalculationUnits = dr["CalculationUnits"].ToString();
                            resultLabTech.CriticalValue = dr["CriticalValue"].ToString();
                            resultLabTech.DisplayValueText = dr["DisplayValueText"].ToString();
                            resultLabTech.NormalValuesFiled = dr["NormalValuesFiled"].ToString();

                            lstResultLabTech.Add(resultLabTech);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion




        #region AddTitle

        #region getAllTitleName
        /// <summary>
        ///  Table - collectedAt
        /// </summary>
        ///
        /// <param name="regId"></param>
        /// <returns></returns>
        [Route("api/Account/getAllTitleName")]
        [HttpGet]
        public List<AddTitle> getAllTitleName()
        {
            List<AddTitle> lstTitlteDetails = new List<AddTitle>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    //string strSQL = "SELECT * FROM pathoclinic.Doctor where RegID='" + regId + "' ";
                    string strSQL = "SELECT * FROM AddTitle";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            AddTitle titleDetails = new AddTitle();

                            titleDetails.TitleId = (int)dr["TitleId"];
                            titleDetails.TitleName = dr["TitleName"].ToString();
                            lstTitlteDetails.Add(titleDetails);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstTitlteDetails;
            }
        }
        #endregion

        #region insertTitleNameDetails
        /// <summary>
        /// Table - Doctor
        /// Insert the Doctor table values.
        /// </summary>
        /// <param name="docdor"></param>
        [Route("api/Account/insertTitleNameDetails")]
        [HttpPost]
        public void insertTitleNameDetails(AddTitle title)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {


                    string strSQL = "INSERT INTO AddTitle(TitleName) VALUES(@val1)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);


                    cmd.Parameters.AddWithValue("@val1", title.TitleName);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion

        #endregion

        #region updatependingAmount 
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name=""></param>
        [Route("api/Account/updatependingAmount")]
        [HttpPost]
        public void updatependingAmount(Invoice invoice)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE invoice SET NetAmount='" + invoice.NetAmount + "' where MrdNo='" + invoice.MrdNo + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region updateMultiCalculationforTest 
        /// <summary>
        /// Table - calculationfortestdetails
        /// Update the details to calculationfortestdetails table by using TestCode and ID.
        /// </summary>
        ///<param name="calculationTestDetails"
        [Route("api/Account/updateMultiCalculationforTest")]
        [HttpPost]
        public void updateMultiCalculationforTest(calculationForTestDetails calculationTestDetails)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  pathoclinic.calculationfortestdetails SET splcalculation='" + calculationTestDetails.splcalculation + "',Elements='" + calculationTestDetails.ElementName + "',ElementsCalculationType='" + calculationTestDetails.ElementsCalculationType + "'where testCode='" + calculationTestDetails.testCode + "' AND id='" + calculationTestDetails.id + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region updatTableRequiredImageforTest 
        /// <summary>
        /// Table - imagefortest
        /// Update the details to imagefortest table by using TestCode and ID.
        /// </summary>
        ///<param name="imagetest"
        [Route("api/Account/updatTableRequiredImageforTest")]
        [HttpPost]
        public void updatTableRequiredImageforTest(ImageforTest imagetest)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  pathoclinic.imagefortest SET ImageName='" + imagetest.ImageName + "',ImagePath='" + imagetest.ImagePath + "'where TestCode='" + imagetest.TestCode + "' AND ImageId='" + imagetest.ImageId + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region getPragancyReferanceRangeLab
        /// <summary>
        /// Table - pregnancyreferencerange
        /// </summary>
        /// get range values from pregnancyreferencerange table      
        /// <returns></returns>
        [Route("api/Account/getPragancyReferanceRangeLab")]
        [HttpGet]
        public List<PragancyReferancyRange> getPragancyReferanceRangeLab(string testCode)
        {
            List<PragancyReferancyRange> lstPragancyReferancyRange = new List<PragancyReferancyRange>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.pregnancyreferencerange where TestCode='" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            PragancyReferancyRange pragancyReferancyRange = new PragancyReferancyRange();

                            pragancyReferancyRange.TestCode = dr["TestCode"].ToString();
                            pragancyReferancyRange.FirstTrimester = dr["FirstTrimester"].ToString();
                            pragancyReferancyRange.SecondTrimester = dr["SecondTrimester"].ToString();
                            pragancyReferancyRange.ThirdTrimester = dr["ThirdTrimester"].ToString();

                            lstPragancyReferancyRange.Add(pragancyReferancyRange);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstPragancyReferancyRange;
            }
        }
        #endregion

        #region getAllSelfListByDate
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getAllSelfListByDate")]
        [HttpGet]
        public List<Invoice> getAllSelfListByDate(string Fromdate, string Todate, string SelfPaymentType)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "Select* FROM pathoclinic.invoice  where Action ='Self' AND InvoiceDate Between '" + Fromdate + "' AND '" + Todate + "'";

                    //string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action LIKE'GRP-%" + groupname + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.PaymentType = dr["PaymentType"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            invoice.NetAmount = dr["NetAmount"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion

        #region getAllPaymentSelfListByDate
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getAllPaymentSelfListByDate")]
        [HttpGet]
        public List<Invoice> getAllPaymentSelfListByDate(string Fromdate, string Todate, string SelfPaymentType)
        {
            List<Invoice> InvoiceDetailsList = new List<Invoice>();
            DataTable dt = new DataTable();
            // int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "Select* FROM pathoclinic.invoice  where PaymentType='" + SelfPaymentType + "' AND InvoiceDate Between '" + Fromdate + "' AND '" + Todate + "'";

                    //string strSQL = "SELECT * FROM pathoclinic.Invoice  where Action LIKE'GRP-%" + groupname + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Invoice invoice = new Invoice();
                            invoice.Token = dr["Token"].ToString();
                            invoice.MrdNo = dr["MrdNo"].ToString();
                            invoice.InvoiceNo = dr["InvoiceNo"].ToString();
                            invoice.Action = dr["Action"].ToString();
                            invoice.PaidAmount = dr["PaidAmount"].ToString();
                            invoice.PaymentType = dr["PaymentType"].ToString();
                            invoice.PatientName = dr["PatientName"].ToString();
                            invoice.Description = dr["Description"].ToString();
                            invoice.Amount = dr["Amount"].ToString();
                            invoice.NetAmount = dr["NetAmount"].ToString();
                            InvoiceDetailsList.Add(invoice);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return InvoiceDetailsList;
            }
        }
        #endregion


        #region getAllChildTestListwithMultiplecomponents
        /// <summary>
        ///  Table - ChildTestList
        /// </summary>
        /// Listed all values from ChildTestList table
        /// <returns></returns>
        [Route("api/Account/getAllChildTestListwithMultiplecomponents")]
        [HttpGet]
        public List<ChildTestList> getAllChildTestListwithMultiplecomponents()
        {
            List<ChildTestList> lstChildTestDetails = new List<ChildTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.ChildTestList where multiplecomponents ='yes'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ChildTestList childTestList = new ChildTestList();

                            childTestList.TestID = (int)dr["TestID"];
                            childTestList.ProfileID = (int)dr["ProfileID"];
                            childTestList.TestName = dr["TestName"].ToString();
                            childTestList.TestCode = dr["TestCode"].ToString();
                            lstChildTestDetails.Add(childTestList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstChildTestDetails;
            }
        }
        #endregion

        #region getAllElementNameByTestCode
        /// <summary>
        ///  Table - ChildTestList
        /// </summary>
        /// Listed all values from ChildTestList table
        /// <returns></returns>
        [Route("api/Account/getAllElementNameByTestCode")]
        [HttpGet]
        public List<TestMultipleComponents> getAllElementNameByTestCode(string testcode)
        {
            List<TestMultipleComponents> lstmultiplecomponentstDetails = new List<TestMultipleComponents>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.testmultiplecomponents where TestCode = '" + testcode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            TestMultipleComponents multiplecomponents = new TestMultipleComponents();

                            multiplecomponents.TestMultipleComponentsID = (int)dr["TestMultipleComponentsID"];
                            multiplecomponents.ElementName = dr["ElementName"].ToString();
                            multiplecomponents.TestCode = dr["TestCode"].ToString();
                            lstmultiplecomponentstDetails.Add(multiplecomponents);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstmultiplecomponentstDetails;
            }
        }
        #endregion


        #region updateElementPriorityStatus
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="progressTable"></param>
        [Route("api/Account/updateElementPriorityStatus")]
        [HttpPost]
        public void updateElementPriorityStatus(TestMultipleComponents testmultiplecomponents)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE  testmultiplecomponents SET PriorityStatus='" + testmultiplecomponents.PriorityStatus + "' where TestMultipleComponentsID='" + testmultiplecomponents.TestMultipleComponentsID + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion
        #region getAllChildTestListforFreeText
        /// <summary>
        ///  Table - ChildTestList
        /// </summary>
        /// Listed all values from ChildTestList table
        /// <returns></returns>
        [Route("api/Account/getAllChildTestListforFreeText")]
        [HttpGet]
        public List<ChildTestList> getAllChildTestListforFreeText()
        {
            List<ChildTestList> lstChildTestDetails = new List<ChildTestList>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.ChildTestList where ActiveStatus =1 AND NumericOrText=0";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            ChildTestList childTestList = new ChildTestList();

                            childTestList.TestID = (int)dr["TestID"];
                            childTestList.ProfileID = (int)dr["ProfileID"];
                            childTestList.TestName = dr["TestName"].ToString();
                            childTestList.TestCode = dr["TestCode"].ToString();
                            childTestList.DepartmentName = dr["DepartmentName"].ToString();
                            childTestList.Methodology = dr["Methodology"].ToString();
                            childTestList.UnitMeasurementNumeric = dr["UnitMeasurementNumeric"].ToString();
                            childTestList.UnitMesurementFreeText = dr["UnitMesurementFreeText"].ToString();
                            childTestList.TableRequiredPrint = dr["TableRequiredPrint"].ToString();
                            childTestList.DefaultValues = dr["DefaultValues"].ToString();
                            childTestList.GenderMale = dr["GenderMale"].ToString();
                            childTestList.GenderFemale = dr["GenderFemale"].ToString();
                            childTestList.Pregnancyrefrange = dr["Pregnancyrefrange"].ToString();
                            childTestList.AdditionalFixedComments = dr["AdditionalFixedComments"].ToString();
                            childTestList.LowerCriticalValue = dr["LowerCriticalValue"].ToString();
                            childTestList.UpperCriticalValue = dr["UpperCriticalValue"].ToString();
                            childTestList.OtherCriticalReport = dr["OtherCriticalReport"].ToString();
                            childTestList.AgewiseCriticalValue = dr["AgewiseCriticalValue"].ToString();
                            childTestList.units = dr["units"].ToString();
                            childTestList.TurnAroundTime = dr["TurnAroundTime"].ToString();
                            childTestList.RequiredBiospyTestNumber = dr["RequiredBiospyTestNumber"].ToString();
                            childTestList.RequiredSamples = dr["RequiredSamples"].ToString();
                            childTestList.PatientPreparation = dr["PatientPreparation"].ToString();
                            childTestList.ExpectedResultDate = dr["ExpectedResultDate"].ToString();
                            childTestList.Amount = Convert.ToDouble(dr["Amount"]);
                            childTestList.Finaloutput = dr["Finaloutput"].ToString();
                            childTestList.TestbasedDiscount = dr["TestbasedDiscount"].ToString();
                            childTestList.Outsourced = dr["Outsourced"].ToString();
                            childTestList.CreateDate = dr["CreateDate"].ToString();
                            childTestList.cutOffTime = dr["CutoffTime"].ToString();
                            childTestList.ValidDate = dr["AmountValidDate"].ToString();
                            childTestList.DisplayName = dr["DisplayTestName"].ToString();
                            childTestList.TestInformation = dr["TestInformation"].ToString();
                            childTestList.TestSchedule = dr["TestSchedule"].ToString();
                            childTestList.NumericOrText = Convert.ToBoolean(dr["NumericOrText"]);
                            childTestList.commonParagraph = dr["commonParagraph"].ToString();
                            childTestList.UrineCulture = dr["UrineCulture"].ToString();
                            childTestList.AlternativeSample = dr["AlternativeSampleContainer"].ToString();
                            childTestList.CalculationPresent = dr["CalculationPresent"].ToString();
                            childTestList.Multiplecomponents = dr["multiplecomponents"].ToString();
                            lstChildTestDetails.Add(childTestList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstChildTestDetails;
            }
        }
        #endregion

        #region insertFreeTextTemplate
        /// <summary>
        /// Table - freetexttemplate
        /// Insert the freetexttemplate table values.
        /// </summary>
        /// <param name="freetexttemplate"></param>
        [Route("api/Account/insertFreeTextTemplate")]
        [HttpPost]
        public void insertFreeTextTemplate(FreeTextTemplate freetexttemplate)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.freetexttemplate(TestCode,TestName,TestType,ElementName,TestMultipleComponentsID) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", freetexttemplate.TestCode);
                    cmd.Parameters.AddWithValue("@val2", freetexttemplate.TestName);
                    cmd.Parameters.AddWithValue("@val3", freetexttemplate.TestType);
                    cmd.Parameters.AddWithValue("@val4", freetexttemplate.ElementName);
                    cmd.Parameters.AddWithValue("@val5", freetexttemplate.TestMultipleComponentsID);
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getTestByTestCodeForHospital
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getTestByTestCodeForHospital")]
        [HttpGet]
        public OutofHospitalTestlist getTestByTestCodeForHospital(string testCode)
        {
            OutofHospitalTestlist hospitalDetails = new OutofHospitalTestlist();
            DataTable dt = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.outofhospitaltestlist where TestCode='" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            // childTestList.Sno = i;

                            hospitalDetails.OutOfHospitalTestListId = Convert.ToInt32(dr["OutOfHospitalTestListId"]);
                            hospitalDetails.TestName = dr["TestName"].ToString();
                            hospitalDetails.TestCode = dr["TestCode"].ToString();
                            hospitalDetails.Amount = Convert.ToDouble(dr["Amount"]);

                            // i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return hospitalDetails;
            }
        }
        #endregion

        #region getProfileByProfileCodeInHospital
        /// <summary>
        /// Table 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getProfileByProfileCodeInHospital")]
        [HttpGet]
        public OutofHospitalProfilelist getProfileByProfileCodeInHospital(string profileCode)
        {
            OutofHospitalProfilelist ProfileList = new OutofHospitalProfilelist();
            DataTable dt = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.outofhospitalprofilelist where ProfileCode='" + profileCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            // masterProfileList.Sno = i;
                            ProfileList.OutOfHospitalProfileListID = (int)dr["OutOfHospitalProfileListID"];
                            ProfileList.ProfileCode = dr["ProfileCode"].ToString();
                            ProfileList.ProfileName = dr["ProfileName"].ToString();
                            ProfileList.Amount = Convert.ToDouble((dr["Amount"]));

                            // i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return ProfileList;
            }
        }
        #endregion


        #region getPreviousTestHistory
        /// <summary>
        /// Table 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getPreviousTestHistory")]
        [HttpGet]
        public List<ResultLabTech> getPreviousTestHistory(int regID, string TestCode)
        {
            List<ResultLabTech> lstPreviousTest = new List<ResultLabTech>();
            List<LabOrder> lstlabTest = new List<LabOrder>();
            DataTable dt = new DataTable();
            DataTable dta = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest where RegID='" + regID + "' and TestCode='" + TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                            lstPreviousTest.Add(resultLabTech);
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstPreviousTest;
            }
        }
        #endregion

        #region getPreviousTestResult
        /// <summary>
        /// Table 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getPreviousTestResult")]
        [HttpGet]
        public List<ResultLabTech> getPreviousTestResult(int regID, string TestCode)
        {
            List<ResultLabTech> lstPreviousTest = new List<ResultLabTech>();
            List<LabOrder> lstlabTest = new List<LabOrder>();
            DataTable dt = new DataTable();
            DataTable dta = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT Result FROM pathoclinic.resultlabtest where RegID='" + regID + "' and TestCode='" + TestCode + "' ORDER BY CreateDate DESC LIMIT  1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.Result = dr["Result"].ToString();

                            lstPreviousTest.Add(resultLabTech);
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstPreviousTest;
            }
        }
        #endregion


        #region getPreviousTestHistoryElement
        /// <summary>
        /// Table 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getPreviousTestHistoryElement")]
        [HttpGet]
        public List<ResultLabTech> getPreviousTestHistoryElement(int regID, string testCode, string elementName)
        {
            List<ResultLabTech> lstPreviousTest = new List<ResultLabTech>();
            List<LabOrder> lstlabTest = new List<LabOrder>();
            DataTable dt = new DataTable();
            DataTable dta = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.multiplecomponentwithcalculation where RegID='" + regID + "' and TestCode='" + testCode + "' and ElementName= '" + elementName + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.Result = dr["Result"].ToString();
                            resultLabTech.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                            lstPreviousTest.Add(resultLabTech);
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstPreviousTest;
            }
        }
        #endregion


        #region getPreviousTestResultElement
        /// <summary>
        /// Table 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/getPreviousTestResultElement")]
        [HttpGet]
        public List<ResultLabTech> getPreviousTestResultElement(int regID, string testCode, string elementName)
        {
            List<ResultLabTech> lstPreviousTest = new List<ResultLabTech>();
            List<LabOrder> lstlabTest = new List<LabOrder>();
            DataTable dt = new DataTable();
            DataTable dta = new DataTable();
            //  int i = 1;
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT Result FROM pathoclinic.multiplecomponentwithcalculation where RegID='" + regID + "' and TestCode='" + testCode + "' and ElementName= '" + elementName + "' ORDER BY CreateDate DESC LIMIT  1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            ResultLabTech resultLabTech = new ResultLabTech();
                            resultLabTech.Result = dr["Result"].ToString();

                            lstPreviousTest.Add(resultLabTech);
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstPreviousTest;
            }
        }
        #endregion



        #region getReportDetailsFromMultipleComponent
        /// <summary>
        /// 
        /// </summary>
        ///      
        /// <returns></returns>
        [Route("api/Account/getReportDetailsFromMultipleComponent")]
        [HttpGet]
        public List<MultipleComponentswithCalculation> getReportDetailsFromMultipleComponent(string mrdNo)
        {
            List<MultipleComponentswithCalculation> lstResultLabTech = new List<MultipleComponentswithCalculation>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.multiplecomponentwithcalculation where MrdNo='" + mrdNo + "' order by PriorityStatus asc ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            MultipleComponentswithCalculation MultipleComponents = new  MultipleComponentswithCalculation();
                            MultipleComponents.MrdNo = dr["MrdNo"].ToString();
                            MultipleComponents.Result = dr["Result"].ToString();
                            MultipleComponents.TestName = dr["TestName"].ToString();
                            MultipleComponents.TestCode = dr["TestCode"].ToString();
                            MultipleComponents.NormalValues = dr["NormalValues"].ToString();
                            MultipleComponents.ElementName = dr["ElementName"].ToString();
                            MultipleComponents.Units = dr["Units"].ToString();
                            MultipleComponents.Comments = dr["Comments"].ToString();
                            MultipleComponents.ActualValue = dr["ActualValue"].ToString();
                            MultipleComponents.Notes = dr["Notes"].ToString();
                            MultipleComponents.DisplayValue = dr["DisplayValue"].ToString();
                            MultipleComponents.Calculation = dr["Calculation"].ToString();
                            MultipleComponents.Methodology = dr["Methodology"].ToString();
                            MultipleComponents.NormalValuesFiled = dr["NormalValuesFiled"].ToString();
                            MultipleComponents.PriorityStatus = (int)dr["PriorityStatus"];
                            MultipleComponents.Status = (int)dr["Status"];
                            MultipleComponents.select = false;

                            lstResultLabTech.Add(MultipleComponents);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion




        #region getReportDetailsFromMultipleTestcodeMrdNo
        /// <summary>
        /// 
        /// </summary>
        ///      
        /// <returns></returns>
        [Route("api/Account/getReportDetailsFromMultipleTestcodeMrdNo")]
        [HttpGet]
        public List<MultipleComponentswithCalculation> getReportDetailsFromMultipleTestcodeMrdNo(string mrdNo, string testCode)
        {
            List<MultipleComponentswithCalculation> lstResultLabTech = new List<MultipleComponentswithCalculation>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.multiplecomponentwithcalculation where MrdNo='" + mrdNo + "' and TestCode='" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            MultipleComponentswithCalculation MultipleComponents = new MultipleComponentswithCalculation();
                            MultipleComponents.MrdNo = dr["MrdNo"].ToString();
                            MultipleComponents.Result = dr["Result"].ToString();
                            MultipleComponents.TestName = dr["TestName"].ToString();
                            MultipleComponents.TestCode = dr["TestCode"].ToString();
                            MultipleComponents.NormalValues = dr["NormalValues"].ToString();
                            MultipleComponents.ElementName = dr["ElementName"].ToString();
                            MultipleComponents.Units = dr["Units"].ToString();
                            MultipleComponents.Comments = dr["Comments"].ToString();
                            MultipleComponents.ActualValue = dr["ActualValue"].ToString();
                            MultipleComponents.Notes = dr["Notes"].ToString();
                            MultipleComponents.DisplayValue = dr["DisplayValue"].ToString();
                            MultipleComponents.Calculation = dr["Calculation"].ToString();
                            MultipleComponents.Methodology = dr["Methodology"].ToString();
                            lstResultLabTech.Add(MultipleComponents);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion






        #region getCriticalValueFronAgeWiseReference
        /// <summary>
        /// 
        /// </summary>
        ///   
        /// <returns></returns>
        [Route("api/Account/getCriticalValueFronAgeWiseReference")]
        [HttpGet]
        public List<AgewiseCricticalReferences> getCriticalValueFronAgeWiseReference(string testcode)
        {
            List<AgewiseCricticalReferences> lstResultLabTech = new List<AgewiseCricticalReferences>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.agewisecricticalreference where TestCode='" + testcode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            AgewiseCricticalReferences resultLabTech = new AgewiseCricticalReferences();
                            resultLabTech.TestName = dr["TestName"].ToString();
                            resultLabTech.LowUpperCricticalValue = dr["LowUpperCricticalValue"].ToString();
                            lstResultLabTech.Add(resultLabTech);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion



        #region getTestCodeandMultiComponentbyTestName
        /// <summary>
        /// Table - childtestlist
        /// </summary>
        /// get test code from childtestlist table by Test Name
        /// <returns></returns>
        [Route("api/Account/getTestCodeandMultiComponentbyTestName")]
        [HttpGet]
        public ChildTestList getTestCodeandMultiComponentbyTestName(string testName)
        {
            ChildTestList childTestList = new ChildTestList();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    //string strSQL = "SELECT TestName,TestCode,multiplecomponents FROM childtestlist where TestName = '" + testName + "' AND ActiveStatus=1 AND commonParagraph = NO AND UrineCulture = NO";
                    string strSQL = "SELECT TestName,TestCode,multiplecomponents FROM childtestlist where TestName = '" + testName + "' AND ActiveStatus=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr != null)
                        {
                            childTestList.TestName = dr["TestName"].ToString();
                            childTestList.TestCode = dr["TestCode"].ToString();
                            childTestList.Multiplecomponents = dr["multiplecomponents"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return childTestList;
            }
        }
        #endregion


        #region updateLabTestList
        /// <summary>
        /// Table - labTestList
        /// Update status to labTestList tables
        /// </summary>
        /// <param name="labTestList"></param>
        [Route("api/Account/updateLabTestList")]
        [HttpPost]
        public void updateLabTestList(LabTestList labTestList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE labtestlist SET IndividualStatus= '" + labTestList.IndividualStatus + "' where MrdNo='" + labTestList.MrdNo + "' && TestCode='" + labTestList.TestCode + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();


                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region updatelabprofilelist
        /// <summary>
        /// Table - labprofilelist
        /// Update status to labprofilelist tables
        /// </summary>
        /// <param name="labprofilelist"></param>
        [Route("api/Account/updatelabprofilelist")]
        [HttpPost]
        public void updatelabprofilelist(LabProfileList labProfileList)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE labprofilelist SET IndividualStatus= '" + labProfileList.IndividualStatus + "' where MrdNo='" + labProfileList.MrdNo + "' && ProfileID='" + labProfileList.ProfileID + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();


                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region updateTestNameResult
        /// <summary>
        /// Table - labprofilelist
        /// Update status to labprofilelist tables
        /// </summary>
        /// <param name="labprofilelist"></param>
        [Route("api/Account/updateTestNameResult")]
        [HttpPost]
        public void updateTestNameResult(ResultLabTech resultLabTech)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    // MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    //cmd.CommandType = CommandType.Text;
                    //cmd.ExecuteNonQuery();
                    string strSQL1 = "UPDATE resultlabtest SET Result = @Result, Comment1 = @Comment Where MrdNo='" + resultLabTech.MrdNo + "' && TestName ='" + resultLabTech.TestName + "'";
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                    cmd1.Parameters.AddWithValue("@Result", resultLabTech.Result);
                    cmd1.Parameters.AddWithValue("@Comment", resultLabTech.Comment);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region getGroupHositalDistinct
        /// <summary>
        /// 
        /// </summary>
        ///
        /// <returns></returns>
        [Route("api/Account/getGroupHositalDistinct")]
        [HttpGet]
        public List<Group> getGroupHositalDistinct()
        {
            List<Group> OuthospitalDetailsList = new List<Group>();
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT DISTINCT HospitalName FROM pathoclinic.Group";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Group outofhospital = new Group();

                            outofhospital.HospitalName = dr["HospitalName"].ToString();

                            if (outofhospital.HospitalName != null && outofhospital.HospitalName != "")
                            {
                                OuthospitalDetailsList.Add(outofhospital);
                            }





                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return OuthospitalDetailsList;
            }
        }
        #endregion



        #region updateLabOrderStatus 
        /// <summary>
        /// Table - Invoice
        /// Inserted the invoiceview table values. The reg ID reffered from patientregistration table.
        /// </summary>
        /// <param name="LaborderStatus"></param>
        [Route("api/Account/updateLabOrderStatus")]
        [HttpPost]
        public void updateLabOrderStatus(string mrdNo, string labOrderStatus)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE LaborderStatus SET LabStatus='" + labOrderStatus + "' where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion



        #region updateSampleStatus
        /// <summary>
        /// Table 
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("api/Account/updateSampleStatus")]
        [HttpPost]
        public LaborderStatus updateSampleStatus(string mrdNo)
        {
            LaborderStatus labOrderStatus = new LaborderStatus();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();


            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * from pathoclinic.samplecollecter where MrdNo='" + mrdNo + "' and SampleStatus != '1' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        string strSQL1 = "UPDATE LaborderStatus SET LabStatus='2' where MrdNo='" + mrdNo + "'";    MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }


                    string strSQL2 = "SELECT * from pathoclinic.samplecollecter where MrdNo='" + mrdNo + "' and SampleStatus == '1' ";
                  
                    MySqlDataAdapter mydata2 = new MySqlDataAdapter(strSQL2, conn);
                    MySqlCommandBuilder cmd2 = new MySqlCommandBuilder(mydata);
                    DataSet ds2 = new DataSet();
                    mydata.Fill(ds2);
                    dt2 = ds2.Tables[0];
                    if (dt2.Rows.Count != 0)
                    {
                        string strSQL1 = "UPDATE LaborderStatus SET SampleStatus='1' where MrdNo='" + mrdNo + "'"; MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return labOrderStatus;
            }
        }
        #endregion


        #region insertLocationDetails
        /// <summary>
        /// Table - Location
        /// </summary>
        /// Inserted the Location details
        /// <param name="location"></param>
        [Route("api/Account/insertLocationDetails")]
        [HttpPost]
        public void insertLocationDetails(Location location)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    int locationCodeMax = 0;

                    int i;
                    string locationCode = "";
                    string fetch = "Select Max(MaxID) from Location";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(fetch, conn);
                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        string maxVal = dr[0].ToString();

                        if (maxVal != null && maxVal != "")
                        {
                            locationCodeMax = Int32.Parse(maxVal);
                            locationCodeMax = locationCodeMax + 1;
                            locationCode = location.LocationName.Substring(0, 3).ToUpper() + String.Format("{0:000}", locationCodeMax);
                        }
                        else
                        {
                            locationCodeMax = 1;
                            locationCode = location.LocationName.Substring(0, 3).ToUpper() + String.Format("{0:000}", locationCodeMax);
                        }


                        string strSQL = "INSERT INTO pathoclinic.Location(LocationName,LocationCode,MaxID) VALUES(@val1,@val2,@val3)";
                        //  conn.Open();
                        MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                        cmd.Parameters.AddWithValue("@val1", location.LocationName);
                        cmd.Parameters.AddWithValue("@val2", locationCode);
                        cmd.Parameters.AddWithValue("@val3", locationCodeMax);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }

                    //     string locationCode = location.LocationName.Substring(0,2);


                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region getLocationNameSearchDetails
        /// <summary>
        /// getLocationNameSearchDetails 
        /// </summary>
        /// To get all records from ChildTestList table.
        /// <param name="locationName"></param>
        /// <returns></returns>

        [Route("api/Account/getLocationNameSearchDetails")]
        [HttpGet]
        public int getLocationNameSearchDetails(string locationName)
        {
            int locationCount = 0;

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.Location Where LocationName= '" + locationName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    locationCount = dt.Rows.Count;
                }

                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return locationCount;
            }
        }
        #endregion


        #region getLocationList
        /// <summary>
        /// getLocationList 
        /// </summary>
        /// To get all records from Location table.
        /// <returns></returns>

        [Route("api/Account/getLocationList")]
        [HttpGet]
        public List<Location> getLocationList()
        {
            List<Location> lstLocation = new List<Location>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.Location";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            Location location = new Location();
                            location.LocationName = dr["LocationName"].ToString();
                            location.LocationCode = dr["LocationCode"].ToString();

                            lstLocation.Add(location);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstLocation;
            }
        }
        #endregion

        #region getLocationCodeSearchDetails
        /// <summary>
        /// getLocationNameSearchDetails 
        /// </summary>
        /// To get all records from ChildTestList table.
        /// <param name="locationName"></param>
        /// <returns></returns>

        [Route("api/Account/getLocationCodeSearchDetails")]
        [HttpGet]
        public int getLocationCodeSearchDetails(string locationCode)
        {
            int locationCount = 0;

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.Location Where LocationCode= '" + locationCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    locationCount = dt.Rows.Count;
                }

                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return locationCount;
            }
        }
        #endregion

        #region insertCorporate
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name=""></param>
        [Route("api/Account/insertCorporate")]
        [HttpPost]
        public void insertCorporate(corporate corporatedetails)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "INSERT INTO pathoclinic.corporate(corporatename) VALUES(@val1)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", corporatedetails.corporatename);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region getcorporatename
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name=""></param>
        /// <returns></returns>

        [Route("api/Account/getcorporatename")]
        [HttpGet]
  
       public List<corporate> getcorporatename()
        {
            List<corporate> lstCorporate = new List<corporate>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.corporate";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {

                            corporate corporate = new corporate();
                            corporate.corporatename = dr["corporatename"].ToString();


                            lstCorporate.Add(corporate);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstCorporate;
            }
        }
        #endregion


        #region getallDisplayCount
        /// <summary>
        /// Table - 
        /// </summary>
        ///  
        /// <returns></returns>
        [Route("api/Account/getallDisplayCount")]
        [HttpGet]
        public int getallDisplayCount(string displayename)
        {
            CountApprover displaydetails = new CountApprover();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT Count(*) FROM pathoclinic.displaytest where DisplayName = '" + displayename + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            displaydetails.Count = Convert.ToInt32(dr[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return displaydetails.Count;
            }
        }
        #endregion


        #region getAllDisplayTestDetailswithparameter
        /// <summary>
        /// Table - displaytest
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/getAllDisplayTestDetailswithparameter")]
        [HttpGet]
        public List<DisplayTest> getAllDisplayTestDetailswithparameter(string testCode,string displayename)
        {
            List<DisplayTest> lstDisplayTest = new List<DisplayTest>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.displaytest where TestCode ='" + testCode + "' AND DisplayName ='" + displayename + "' ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            DisplayTest displayTest = new DisplayTest();
                            displayTest.DisplayTestID = (int)dr["DisplayTestID"];
                            displayTest.TestName = dr["TestName"].ToString();
                            displayTest.TestCode = dr["TestCode"].ToString();
                            displayTest.DisplayName = dr["DisplayName"].ToString();
                            displayTest.Amount = Convert.ToDouble(dr["Amount"]);
                            displayTest.ExpiryDate = dr["ExpiryDate"].ToString();
                            lstDisplayTest.Add(displayTest);
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstDisplayTest;
            }
        }
        #endregion


        #region getDuplicationTitle
        /// <summary>
        /// Table - addtitle
        /// </summary>
        /// Listed all values from addtitle table
        /// <returns></returns>
        [Route("api/Account/getDuplicationTitle")]
        [HttpGet]
        public int getDuplicationTitle(string title)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.addtitle where TitleName='" + title + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion

        #region getDuplicationElement
        /// <summary>
        /// Table - elements
        /// </summary>
        /// Listed all values from elements table
        /// <returns></returns>
        [Route("api/Account/getDuplicationElement")]
        [HttpGet]
        public int getDuplicationElement(string testCode,string elementName)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.elements where TestCode='" + testCode + "' and ElementName='"+ elementName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region getDuplicationTemplate
        /// <summary>
        /// Table - elements
        /// </summary>
        /// Listed all values from elements table
        /// <returns></returns>
        [Route("api/Account/getDuplicationTemplate")]
        [HttpGet]
        public int getDuplicationTemplate(string testCode, string elementName, string templateName)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.templates where TestCode='" + testCode + "' and ElementName='" + elementName + "' and TemplateName ='"+ templateName +"'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion



        #region getDuplicationDepartment
        /// <summary>
        /// Table - department
        /// </summary>
        /// Listed all values from department table
        /// <returns></returns>
        [Route("api/Account/getDuplicationDepartment")]
        [HttpGet]
        public int getDuplicationDepartment(string department)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.department where DepartmentName='" + department + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region getDuplicationDisplayName
        /// <summary>
        /// Table - displaytest
        /// </summary>
        /// Listed all values from displaytest table
        /// <returns></returns>
        [Route("api/Account/getDuplicationDisplayName")]
        [HttpGet]
        public int getDuplicationDisplayName(string displayName,string testCode)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.displaytest where DisplayName='" + displayName + "' and TestCode='"+ testCode +"'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region getDuplicationCorporate
        /// <summary>
        /// Table - corporate
        /// </summary>
        /// Listed all values from corporate table
        /// <returns></returns>
        [Route("api/Account/getDuplicationCorporate")]
        [HttpGet]
        public int getDuplicationCorporate(string corporateName)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.corporate where corporatename='" + corporateName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion

        #region getProfileStatusCount
        /// <summary>
        /// Table - LabProfileList
        /// </summary>
        /// Listed all values from LabProfileList table
        /// <returns></returns>
        [Route("api/Account/getProfileStatusCount")]
        [HttpGet]
        public int getProfileStatusCount(string mrdNo)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.LabProfileList where MrdNo='" + mrdNo + "' and IndividualStatus !=3 and IndividualStatus != 4 ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region getTestStatusCount
        /// <summary>
        /// Table - addtitle
        /// </summary>
        /// Listed all values from addtitle table
        /// <returns></returns>
        [Route("api/Account/getTestStatusCount")]
        [HttpGet]
        public int getTestStatusCount(string mrdNo)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.labtestlist where MrdNo='" + mrdNo + "' and IndividualStatus !=3 and IndividualStatus != 4 ";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion

        #region updateLabSampleStatusApproved 
        /// <summary>
        /// Table - Invoice
        /// Inserted the invoiceview table values. The reg ID reffered from patientregistration table.
        /// </summary>
        /// <param name="LaborderStatus"></param>
        [Route("api/Account/updateLabSampleStatusApproved")]
        [HttpPost]
        public void updateLabSampleStatusApproved(string mrdNo)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "UPDATE LaborderStatus SET LabStatus='4' where MrdNo='" + mrdNo + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion

        #region deleteCalculationByTestCode 
        /// <summary>
        /// Table - calculationfortestdetails
        /// Delete calculationfortestdetails.
        /// </summary>
        /// <param name="CalculationForTest"></param>
        [Route("api/Account/deleteCalculationByTestCode")]
        [HttpPost]
        public string deleteCalculationByTestCode(string testCode)
        {
            string result = "";
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "delete from calculationfortestdetails where TestCode='" + testCode + "'";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    result = "Success";
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                    result = "Fail";
                }
            }
            return result;
        }

        #endregion


        #region updateResultApproveStatus
        /// <summary>
        /// table - ResultLabTech
        /// </summary>
        /// <param name="resultLabTech"></param>
        [Route("api/Account/updateResultApproveStatus")]
        [HttpPost]
        public void updateResultApproveStatus(ResultLabTech resultLabTech)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.resultlabtest Where MrdNo='" + resultLabTech.MrdNo + "' && TestCode ='" + resultLabTech.TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string strSQL1 = "UPDATE resultlabtest SET Status = @Status  Where MrdNo='" + resultLabTech.MrdNo + "' && TestCode ='" + resultLabTech.TestCode + "'";
                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.Parameters.AddWithValue("@Status", resultLabTech.Status);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            }

        #endregion


        #region updateTestApproveStatus
        /// <summary>
        /// Table - labprofilelist
        /// Update status to labprofilelist tables
        /// </summary>
        /// <param name="labprofilelist"></param>
        [Route("api/Account/updateTestApproveStatus")]
        [HttpPost]
        public void updateTestApproveStatus(ResultLabTech resultLabTech)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL1 = "UPDATE resultlabtest SET Status = @Status Where MrdNo='" + resultLabTech.MrdNo + "' && TestName ='" + resultLabTech.TestName + "'";
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                    cmd1.Parameters.AddWithValue("@Status", resultLabTech.Status);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }

        #endregion


        #region updateMultiApproveStatus
        /// <summary>
        /// table - multiplecomponentwithcalculation
        /// </summary>
        /// <param name="multiplecomponentwithcalculation"></param>
        [Route("api/Account/updateMultiApproveStatus")]
        [HttpPost]
        public void updateMultiApproveStatus(MultipleComponentswithCalculation multipleComponentsCalculation)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.multiplecomponentwithcalculation Where MrdNo='" + multipleComponentsCalculation.MrdNo + "' && TestCode ='" + multipleComponentsCalculation.TestCode + "' && ElementName='" + multipleComponentsCalculation.ElementName + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string strSQL1 = "UPDATE pathoclinic.multiplecomponentwithcalculation SET Status=@Status Where MrdNo='" + multipleComponentsCalculation.MrdNo + "' AND TestCode ='" + multipleComponentsCalculation.TestCode + "' AND ElementName='" + multipleComponentsCalculation.ElementName + "'";
                        //  conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.Parameters.AddWithValue("@Status", multipleComponentsCalculation.Status);                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }
                   
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }
        #endregion

        #region updateCommonStautus
        /// <summary>
        /// Table - insert BoneMarrowAspiration
        /// </summary>
        /// <param name="boneMarrowAspirationNew"></param>
        [Route("api/Account/updateCommonStautus")]
        [HttpPost]
        public void updateCommonStautus(BoneMarrowAspirationNew boneMarrowAspirationNew)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM pathoclinic.bonemarrowaspiration where MrdNo='" + boneMarrowAspirationNew.MrdNo + "' and TestCode = '" + boneMarrowAspirationNew.TestCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    
                       
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr != null)
                            {
                                string strSQL1 = "UPDATE bonemarrowaspiration SET Status='" + boneMarrowAspirationNew.Status + "' where MrdNo='" + boneMarrowAspirationNew.MrdNo + "' and TestCode='" + boneMarrowAspirationNew.TestCode + "' ";
                                // conn.Open();
                                MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                                cmd1.ExecuteNonQuery();
                            }
                        }
                    



                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getCalculationResult
        /// <summary>
        /// Table - parentpaymentreceived
        /// </summary>
        /// get pending amount values from parentpaymentreceived table      
        /// <returns></returns>
        [Route("api/Account/getCalculationResult")]
        [HttpGet]
        public List<ResultLabTech> getCalculationResult(string mrdNo, string testCode)
        {

            string informationValue="" ;
            string formulaValues="" ;
            string testCodesCalculationPart="";
            string calcType = "";
            string[] tests;
            List<ResultLabTech> lstResultLabTech = new List<ResultLabTech>();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM pathoclinic.calculationfortestdetails where TestCode='" + testCode + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);



                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            if (dr["CalculationType"].ToString() == "Used by other Tests")
                            {
                                 informationValue = dr["FormulaLabel"].ToString();
                                formulaValues = dr["splcalculation"].ToString();
                             testCodesCalculationPart = dr["TestCodesCalculationPart"].ToString();
                                tests = testCodesCalculationPart.Split(',');
                                foreach (var test in tests) {


                                    string strSQL1 = "SELECT * FROM pathoclinic.resultlabtest where MrdNo='" + mrdNo + "' AND TestCode='" + test + "' order by ProfilePriority asc";
                                 //   conn.Open();
                                    MySqlDataAdapter mydata1 = new MySqlDataAdapter(strSQL1, conn);
                                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata1);
                                    DataSet ds1 = new DataSet();
                                    mydata1.Fill(ds1);
                                    dt1 = ds1.Tables[0];
                                    foreach (DataRow dr1 in dt1.Rows)
                                    {
                                        if (dr1 != null)
                                        {

                                           
                                            string Result = dr1["Result"].ToString();
                                             
                                            formulaValues = formulaValues.Replace(test, Result);
                                            calcType = dr["CalculationType"].ToString();
                                        }
                                    }
                                   
                                }
                            }
                            if (dr["CalculationType"].ToString() == "Normal")
                            {
                                informationValue = dr["FormulaLabel"].ToString();
                                formulaValues = dr["splcalculation"].ToString();
                                testCodesCalculationPart = dr["TestCodesCalculationPart"].ToString();
                                


                                    string strSQL1 = "SELECT * FROM pathoclinic.resultlabtest where MrdNo='" + mrdNo + "' AND TestCode='" + testCode + "' order by ProfilePriority asc";
                                    //   conn.Open();
                                    MySqlDataAdapter mydata1 = new MySqlDataAdapter(strSQL1, conn);
                                    MySqlCommandBuilder cmd1 = new MySqlCommandBuilder(mydata1);
                                    DataSet ds1 = new DataSet();
                                    mydata1.Fill(ds1);
                                    dt1 = ds1.Tables[0];
                                    foreach (DataRow dr1 in dt1.Rows)
                                    {
                                        if (dr1 != null)
                                        {


                                            string Result = dr1["Result"].ToString();

                                            formulaValues = formulaValues.Replace("a", Result);
                                            calcType = dr["CalculationType"].ToString();
                                          //  subTest =
                                    }
                                    }

                                
                            }

                        }
                        ResultLabTech resultLabTech = new ResultLabTech();

                        resultLabTech.Result = formulaValues;
                        resultLabTech.SpecialComments = calcType;
                        resultLabTech.CalculationInformation = informationValue;
                        lstResultLabTech.Add(resultLabTech);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstResultLabTech;
            }
        }
        #endregion

 


        #region Test

            #region insertDDetails
            /// <summary>
            /// Table - Doctor
            /// Insert the Doctor table values.
            /// </summary>
            /// <param name="docdor"></param>
        [Route("api/Account/insertDDetails")]
        [HttpPost]
        public void insertDDetails(DriverUser driverUser)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string driverId = "";
                    string fetch = "Select Max(DriverId) from outsource.Driver";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(fetch, conn);
                    MySqlCommandBuilder cmdRetrieve = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            driverId = dr[0].ToString();

                        }
                        if (driverId.Trim() != "")
                        {
                            var tempArray = driverId.Split('_');
                            string tempId = tempArray[1];
                            int id = Convert.ToInt32(tempId);
                            id = id + 1;
                            driverId = "DA_" + String.Format("{0:0000}", id);
                        }
                        else
                        {
                            driverId = "DA_" + String.Format("{0:0000}", 1);
                        }
                    }



                    string strSQL = "INSERT INTO outsource.Driver(DriverID,DriverFirstName,DriverLastName,EmailId,MobileNo,City,Country,CreateDate) VALUES(@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8)";
                    // conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    driverUser.CreateDate = DateTime.Now;
                    cmd.Parameters.AddWithValue("@val1", driverId);
                    cmd.Parameters.AddWithValue("@val2", driverUser.DriverFirstName);
                    cmd.Parameters.AddWithValue("@val3", driverUser.DriverLastName);
                    cmd.Parameters.AddWithValue("@val4", driverUser.EmailId);
                    cmd.Parameters.AddWithValue("@val5", driverUser.MobileNo);
                    cmd.Parameters.AddWithValue("@val6", driverUser.City);
                    cmd.Parameters.AddWithValue("@val7", driverUser.Country);
                    cmd.Parameters.AddWithValue("@val8", driverUser.CreateDate);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getEmailCount
        /// <summary>
        /// Table - corporate
        /// </summary>
        /// Listed all values from corporate table
        /// <returns></returns>
        [Route("api/Account/getEmailCount")]
        [HttpGet]
        public int getEmailCount(string email)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM Driver where EmailId='" + email + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion


        #region getMobileCount
        /// <summary>
        /// Table - corporate
        /// </summary>
        /// Listed all values from corporate table
        /// <returns></returns>
        [Route("api/Account/getMobileCount")]
        [HttpGet]
        public int getMobileCount(string mobile)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM Driver where MobileNo='" + mobile + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion



        #region UploadDocumentsFiles
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Account/UploadDocumentsFiles")]
        [HttpPost]
        public List<ImageforTest> UploadDocumentsFiles()
        {
            List<ImageforTest> lstImageforTest = new List<ImageforTest>();
            int iUploadedCnt = 0;
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.           
            string physicalGrandparentPath = "";

            // kalaivani    

            physicalGrandparentPath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/outsource/TempFile/";

            //string subPath = "ImagesPath"; // your code goes here

            //bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            //if (!exists)
            //    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];
                ImageforTest imageDetails = new ImageforTest();
                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(physicalGrandparentPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(physicalGrandparentPath + Path.GetFileName(hpf.FileName));
                        imageDetails.iUploadedCnt = iUploadedCnt = iUploadedCnt + 1;
                        imageDetails.ImagePath = physicalGrandparentPath + Path.GetFileName(hpf.FileName);
                        imageDetails.ImageName = Path.GetFileName(hpf.FileName);
                        imageDetails.UploadStatus = "1";
                        lstImageforTest.Add(imageDetails);
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return lstImageforTest;
            }
            else
            {
                return lstImageforTest;
            }
        }
        #endregion


        #region insertDocuments
        /// <summary>
        /// Table - insertDocuments
        /// </summary>    
        ///<param name="insertDocuments"></param>
        [Route("api/Account/insertDocuments")]
        [HttpPost]
        public void insertDocuments(DocumentsCopy documentsCopy)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    DataTable dt = new DataTable();
                    string sourcePath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/outsource/TempFile/";
                    string targetPath = @"C:/Program Files/Apache Software Foundation/Tomcat 7.0/webapps/outsource/Documents/" + documentsCopy.DriverId;

                    string sourceFile = System.IO.Path.Combine(sourcePath, documentsCopy.DocumentName);
                    string destFile = System.IO.Path.Combine(targetPath, documentsCopy.DocumentName);


                    if (!System.IO.Directory.Exists(targetPath))
                    {
                        System.IO.Directory.CreateDirectory(targetPath);
                    }
                    //File.Move(sourcePath, targetPath);
                    System.IO.File.Copy(sourceFile, destFile, true);
                    string documentName = documentsCopy.DocumentType + ".png";
                    System.IO.File.Move(targetPath + "/" + documentsCopy.DocumentName, targetPath + "/" + documentName);

                    string getDetails = "SELECT * FROM documentscopy where DriverId='" + documentsCopy.DriverId + "' and '" + documentsCopy.DocumentType + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(getDetails, conn);
                    MySqlCommandBuilder cmds = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];

                    if (dt.Rows.Count == 1)
                    {
                        string strSQL1 = "delete  FROM documentscopy where DriverId='" + documentsCopy.DriverId + "' and '" + documentsCopy.DocumentType + "'";
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand(strSQL1, conn);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }

                    string strSQL = "INSERT INTO DocumentsCopy(DriverId,DocumentType,DocumentName,DocumentPath,CreateDate) VALUES(@val1,@val2,@val3,@val4,@val5)";
                    //  conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@val1", documentsCopy.DriverId);
                    cmd.Parameters.AddWithValue("@val2", documentsCopy.DocumentType);
                    cmd.Parameters.AddWithValue("@val3", documentName);
                    cmd.Parameters.AddWithValue("@val4", targetPath);
                    cmd.Parameters.AddWithValue("@val5", DateTime.Now);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    System.IO.File.Delete(sourcePath + documentsCopy.DocumentName);

                }
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getDriverIdByMobile
        /// <summary>
        /// Table - Driver
        /// </summary>
        /// get test code from Driver table by mobileNo
        /// <returns></returns>
        [Route("api/Account/getDriverIdByMobile")]
        [HttpGet]
        public string getDriverIdByMobile(string mobileNo)
        {

            string driverId = "";
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "SELECT * FROM Driver where MobileNo='" + mobileNo + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            driverId = dr["DriverId"].ToString();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return driverId;
            }
        }
        #endregion

        #region getFileCount
        /// <summary>
        /// Table - corporate
        /// </summary>
        /// Listed all values from corporate table
        /// <returns></returns>
        [Route("api/Account/getFileCount")]
        [HttpGet]
        public int getFileCount(string driverId)
        {
            int count = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM documentscopy where DriverId='" + driverId + "'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    count = dt.Rows.Count;

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return count;
            }
        }
        #endregion

        #region updateDriverDetails
        /// <summary>
        /// Table -  Driver
        /// Inserted the  Driver table values.
        /// </summary>
        /// <param name="Driver"></param>
        [Route("api/Account/updateDriverDetails")]
        [HttpPost]
        public void updateDriverDetails(DriverUser driverUser)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "UPDATE  Driver SET ProfileUpload='" + driverUser.ProfileUpload + "',IdentityUpload='" + driverUser.IdentityUpload + "',LicenseUpload='" + driverUser.LicenseUpload + "',IdentityProof='" + driverUser.IdentityProof + "',IdentityNo='" + driverUser.IdentityNo + "',DrivingLicenseNo='" + driverUser.DrivingLicenseNo + "' where DriverId='" + driverUser.DriverID + "'  ";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                }
  
                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region getProfilePhoto
        /// <summary>
        /// Table - documentscopy
        /// </summary>
        /// <param name="driverID"></param>
        /// <returns></returns>
        [Route("api/Account/getProfilePhoto")]
        [HttpGet]
        public List<DocumentsCopy> getProfilePhoto(string driverId)
        {
            List<DocumentsCopy> lstDocumentsCopy = new List<DocumentsCopy>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                     string strSQL = "SELECT * FROM documentscopy where DriverId='" + driverId + "' and DocumentType='Profile'"; 
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            DocumentsCopy documentsCopy = new DocumentsCopy();
                         //   documentsCopy.DocumentId = (int)dr["ImageId"];
                            documentsCopy.DriverId = dr["DriverId"].ToString();
                            documentsCopy.DocumentName = dr["DocumentName"].ToString();
                            documentsCopy.DocumentPath = dr["DocumentPath"].ToString();
                            documentsCopy.DocumentType = dr["DocumentType"].ToString();
                            lstDocumentsCopy.Add(documentsCopy);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstDocumentsCopy;
            }
        }
        #endregion


        #region getIdentityPhoto
        /// <summary>
        /// Table - documentscopy
        /// </summary>
        /// <param name="driverID"></param>
        /// <returns></returns>
        [Route("api/Account/getIdentityPhoto")]
        [HttpGet]
        public List<DocumentsCopy> getIdentityPhoto(string driverId)
        {
            List<DocumentsCopy> lstDocumentsCopy = new List<DocumentsCopy>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM documentscopy where DriverId='" + driverId + "' and DocumentType !='Profile' and DocumentType != 'License'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            DocumentsCopy documentsCopy = new DocumentsCopy();
                            documentsCopy.DocumentId = (int)dr["ImageId"];
                            documentsCopy.DriverId = dr["DriverId"].ToString();
                            documentsCopy.DocumentName = dr["DocumentName"].ToString();
                            documentsCopy.DocumentPath = dr["DocumentPath"].ToString();
                            documentsCopy.DocumentType = dr["DocumentType"].ToString();
                            lstDocumentsCopy.Add(documentsCopy);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstDocumentsCopy;
            }
        }
        #endregion


        #region getLicensePhoto
        /// <summary>
        /// Table - documentscopy
        /// </summary>
        /// <param name="driverID"></param>
        /// <returns></returns>
        [Route("api/Account/getLicensePhoto")]
        [HttpGet]
        public List<DocumentsCopy> getLicensePhoto(string driverId)
        {
            List<DocumentsCopy> lstDocumentsCopy = new List<DocumentsCopy>();

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    
                    string strSQL = "SELECT * FROM documentscopy where DriverId='" + driverId + "' and DocumentType='License'";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            DocumentsCopy documentsCopy = new DocumentsCopy();
                            documentsCopy.DocumentId = (int)dr["ImageId"];
                            documentsCopy.DriverId = dr["DriverId"].ToString();
                            documentsCopy.DocumentName = dr["DocumentName"].ToString();
                            documentsCopy.DocumentPath = dr["DocumentPath"].ToString();
                            documentsCopy.DocumentType = dr["DocumentType"].ToString();
                            lstDocumentsCopy.Add(documentsCopy);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstDocumentsCopy;
            }
        }
        #endregion


        #region updateBankDetails
        /// <summary>
        /// Table -  Driver
        /// Update the  Driver table values.
        /// </summary>
        /// <param name="Driver"></param>
        [Route("api/Account/updateBankDetails")]
        [HttpPost]
        public void updateBankDetails(DriverUser driverUser)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "UPDATE Driver SET AccountNo='" + driverUser.AccountNo + "',BankName='" + driverUser.BankName + "',BankBranch='" + driverUser.BankBranch + "',IfscCode='" + driverUser.IfscCode + "'  where DriverId='" + driverUser.DriverID + "'  ";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                }

                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion


        #region updateCareerDetails
        /// <summary>
        /// Table -  Driver
        /// Update the  Driver table values.
        /// </summary>
        /// <param name="Driver"></param>
        [Route("api/Account/updateCareerDetails")]
        [HttpPost]
        public void updateCareerDetails(DriverUser driverUser)
        {
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {

                    string strSQL = "UPDATE Driver SET Experience='" + driverUser.Experience + "',CarType='" + driverUser.CarType + "',TerrifPackage='" + driverUser.TerrifPackage + "',IfscCode='" + driverUser.IfscCode + "'  where DriverId='" + driverUser.DriverID + "'  ";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                catch (Exception Ex)
                {
                    string logdetails = Ex.InnerException.ToString();
                }
            }
        }
        #endregion



        #region getPendingDrivers
        /// <summary>
        /// Table - Department
        /// </summary>
        /// Listed all values from Department table
        /// <returns></returns>
        [Route("api/Account/getPendingDrivers")]
        [HttpGet]
        public List<DriverUser> getPendingDrivers()
        {
            List<DriverUser> lstDriverDetails = new List<DriverUser>();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MasterMani"].ConnectionString))
            {
                try
                {
                    string strSQL = "SELECT * FROM Driver where Verfication=1";
                    conn.Open();
                    MySqlDataAdapter mydata = new MySqlDataAdapter(strSQL, conn);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(mydata);
                    DataSet ds = new DataSet();
                    mydata.Fill(ds);
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            DriverUser driverUser = new DriverUser();

                           
                            driverUser.DriverID = dr["DriverID"].ToString();
                            driverUser.DriverFirstName = dr["DriverFirstName"].ToString();
                             
                            driverUser.DriverLastName = dr["DriverLastName"].ToString();
                            driverUser.EmailId = dr["EmailId"].ToString();
                            driverUser.MobileNo = dr["MobileNo"].ToString();
                            driverUser.City = dr["City"].ToString();

                            driverUser.Country = dr["Country"].ToString();


                            driverUser.ProfileUpload = (int)dr["ProfileUpload"];
                            driverUser.IdentityUpload = (int)dr["IdentityUpload"];
                            driverUser.LicenseUpload = (int)dr["LicenseUpload"];

                            driverUser.IdentityProof = dr["IdentityProof"].ToString();
                            driverUser.IdentityNo = dr["IdentityNo"].ToString();

                            driverUser.DrivingLicenseNo = dr["DrivingLicenseNo"].ToString();
                            driverUser.DOB = Convert.ToDateTime(dr["DOB"].ToString());
                            driverUser.CarType = dr["CarType"].ToString();
                            driverUser.TerrifPackage = (int)dr["TerrifPackage"];
                            driverUser.Experience = (int)dr["Experience"];
                            driverUser.AccountNo = dr["AccountNo"].ToString();
                            driverUser.BankBranch = dr["BankBranch"].ToString();
                            driverUser.BankName = dr["BankName"].ToString();
                            driverUser.IfscCode = dr["IfscCode"].ToString();
                            driverUser.Verfication = (int)dr["Verfication"]; 
                            driverUser.VerifiedBy = dr["VerifiedBy"].ToString();                            
                            driverUser.DriverStatus = dr["DriverStatus"].ToString();
                            driverUser.CreateDate = Convert.ToDateTime(dr["CreateDate"].ToString());

                            lstDriverDetails.Add(driverUser);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return lstDriverDetails;
            }
        }
        #endregion


        #endregion
        //End



    }
}
