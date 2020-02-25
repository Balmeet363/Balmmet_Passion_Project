using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BalmeetPassion_Project.Data;
using BalmeetPassion_Project.Models;
using System.Diagnostics;
using System.IO;

namespace BalmeetPassion_Project.Controllers
{
    public class PoetriesController : Controller
    {
        // GET: Poetry
        private passionproject db = new passionproject();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            //can we access the search key?
            //Debug.WriteLine("The search key is " + petsearchkey);

            string query = "Select * from poetries";
            List<poetry> poetries = db.Poetries.SqlQuery(query).ToList();
            return View(poetries);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string PoetryName, string PoetryDate, string PoetryDesc)
        {
            string query = "insert into poetries (poetryName, poetryDate, poetryDescription) values (@PoetryName, @PoetryDate, @PoetryDesc)";
            SqlParameter[] sqlparams = new SqlParameter[3]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@PoetryName", PoetryName);
            sqlparams[1] = new SqlParameter("@PoetryDate", PoetryDate);
            sqlparams[2] = new SqlParameter("@PoetryDesc", PoetryDesc);


            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            string query = "select * from poetries where poetryid = @id";
            var parameter = new SqlParameter("@id", id);
            poetry selectedpoetry = db.Poetries.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedpoetry);
        }

        public ActionResult Update(int id)
        {
            string query = "select * from poetries where poetryid = @id";
            var parameter = new SqlParameter("@id", id);
            poetry selectedpoetry = db.Poetries.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedpoetry);
        }
        [HttpPost]
        public ActionResult Update(int id, string PoetryName, string PoetryDate, string PoetryDesc)
        {
            string query = "update poetries set poetryName = @PoetryName,poetryDate = @PoetryDate,poetryDescription = @PoetryDesc where poetryid = @id";
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@PoetryName", PoetryName);
            sqlparams[1] = new SqlParameter("@PoetryDate", PoetryDate);
            sqlparams[2] = new SqlParameter("@PoetryDesc", PoetryDesc);
            sqlparams[3] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from poetries where poetryid=@id";
            SqlParameter param = new SqlParameter("@id", id);
            poetry selectedpoetry = db.Poetries.SqlQuery(query, param).FirstOrDefault();
            return View(selectedpoetry);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from poetries where poetryid=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


            //for the sake of referential integrity, unset the species for all pets
            //string refquery = "update pets set SpeciesID = '' where SpeciesID=@id";
            //db.Database.ExecuteSqlCommand(refquery, param); //same param as before

            return RedirectToAction("List");
        }

    }
}