using Movie_Mgmt_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_Mgmt_System.Controllers
{
    public class UserController : Controller
    {
        string name,email,password,city,phone = "";
        public void Get_user_data()
        {
            if (Session["Email"] != null) 
            { 
                string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Get_User", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Email_id", Session["Email"]);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    name = reader["User_name"].ToString();
                    email= reader["Email_id"].ToString();
                    password = reader["User_password"].ToString();
                    city = reader["City"].ToString();
                    phone= reader["PhoneNo"].ToString() ;
                }
                connection.Close();
            }
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit()
        {
            Get_user_data();
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User use)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("Update_User", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@User_name", use.user_name);
                    cmd.Parameters.AddWithValue("@Email_id", use.email_id);
                    cmd.Parameters.AddWithValue("@User_password", use.user_password);
                    cmd.Parameters.AddWithValue("@City", use.city);
                    cmd.Parameters.AddWithValue("@PhoneNo", use.phoneno);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
