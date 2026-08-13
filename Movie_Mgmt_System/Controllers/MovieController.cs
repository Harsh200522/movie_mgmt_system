using Movie_Mgmt_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_Mgmt_System.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            List<SelectListItem> list = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("Bind_Category",connection);
            cmd.CommandType = CommandType.Text;
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SelectListItem { Text = reader["Cat_type"].ToString(), Value = reader["Cat_type"].ToString() });
            }
            ViewBag.CategoryList = list;
            return View(new Movie());
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(Movie mov)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("Insert_Movie", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Movie_name", mov.movie_name);
                    cmd.Parameters.AddWithValue("@Release_date", mov.realease_date);
                    cmd.Parameters.AddWithValue("@Cat_id", mov.cat_id);
                    cmd.Parameters.AddWithValue("@Rate", mov.rate);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    ModelState.Clear();
                    ViewBag.Message="Movie Insert Successflly!";
                    return View(new Movie());
                }

                return View(mov);

            }
            catch(Exception ex)
            {
                ViewBag.Message = ex+" :Error while inserting category.";
                return View(mov);
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
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
