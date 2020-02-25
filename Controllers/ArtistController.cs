using System;
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
using BalmeetPassion_Project.Models.ViewModels;

using System.Diagnostics;
using System.IO;

namespace BalmeetPassion_Project.Controllers
{
    public class ArtistController : Controller
    {
        // GET: Artist
        private passionproject db = new passionproject();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            //can we access the search key?
            //Debug.WriteLine("The search key is " + petsearchkey);

            string query = "Select * from artists";
            List<Artist> artists = db.Artists.SqlQuery(query).ToList();
            return View(artists);

        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string ArtistName, string ArtistDOB, string ArtistEmail, string ArtistContact)
        {
            string query = "insert into artists (Name, DOB, Contact, Email) values (@ArtistName,@ArtistDOB,@ArtistContact,@ArtistEmail)";
            SqlParameter[] sqlparams = new SqlParameter[4]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@ArtistName", ArtistName);
            sqlparams[1] = new SqlParameter("@ArtistDOB", ArtistDOB);
            sqlparams[2] = new SqlParameter("@ArtistEmail", ArtistEmail);
            sqlparams[3] = new SqlParameter("@ArtistContact", ArtistContact);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            string query = "select * from artists where artistid = @id";
            var parameter = new SqlParameter("@id", id);
            Artist selectedartist = db.Artists.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedartist);
        }
        [HttpPost]
        public ActionResult Update(int id, string ArtistName, string ArtistDOB, string ArtistEmail, string ArtistContact)
        {
            string query = "update artists set Name = @ArtistName,DOB = @ArtistDOB,Email = @ArtistEmail, Contact = @ArtistContact where artistid = @id";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@ArtistName", ArtistName);
            sqlparams[1] = new SqlParameter("@ArtistDOB", ArtistDOB);
            sqlparams[2] = new SqlParameter("@ArtistEmail", ArtistEmail);
            sqlparams[3] = new SqlParameter("@ArtistContact", ArtistContact);
            sqlparams[4] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from artists where artistID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Artist selectedartist = db.Artists.SqlQuery(query, param).FirstOrDefault();
            return View(selectedartist);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from artists where artistid=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


            //for the sake of referential integrity, unset the species for all pets
            //string refquery = "update pets set SpeciesID = '' where SpeciesID=@id";
            //db.Database.ExecuteSqlCommand(refquery, param); //same param as before

            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            string query = "select * from artists where artistid = @id";
            var parameter = new SqlParameter("@id", id);
            Artist selectedartist = db.Artists.SqlQuery(query, parameter).FirstOrDefault();

            string aside_query = "select * from artists inner join poetryArtists on artists.artistID = poetryArtists.artist_artistID where poetryArtists.poetry_poetryID=@id";
            var parameter1 = new SqlParameter("@id", id);
            List<poetry> poetrieswritten = db.Poetries.SqlQuery(aside_query, parameter1).ToList();

            string all_poetries_query = "select * from poetries";
            List<poetry> AllPoetries = db.Poetries.SqlQuery(all_poetries_query).ToList();

            ShowArtist viewmodel = new ShowArtist();
            viewmodel.artist = selectedartist;
            viewmodel.poetries = poetrieswritten;
            viewmodel.all_poetries = AllPoetries;
            return View(viewmodel);
        }
    }
}