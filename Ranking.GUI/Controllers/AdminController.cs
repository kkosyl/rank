using Ranking.Domain.Abstract;
using Ranking.Domain.Models;
using Ranking.GUI.ViewModels.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Ranking.GUI.Controllers
{
    public class AdminController : Controller
    {
        private IRepository<Place> _placeRepository = new Repository<Place>();
        private IRepository<Opinion> _opinionRepository = new Repository<Opinion>();
        private IRepository<Picture> _pictureRepository = new Repository<Picture>();
        private IRepository<User> _userRepository = new Repository<User>();
        private IRepository<Admin> _adminRepository = new Repository<Admin>();


        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(ViewModels.Admin.LogInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_adminRepository.GetAll().Any(u => u.Login == model.Login) && Crypto.VerifyHashedPassword(_adminRepository.GetAll().First(u => u.Login == model.Login).Password, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                        return RedirectToAction("Index", "Place");
                }
                else
                    ModelState.AddModelError("", "Login bądź hasło niepoprawne.");
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "Place");
        }

        public ActionResult InitAdmin()
        {
            _adminRepository.Add(new Admin
            {
                Login = "admin",
                Password = Crypto.HashPassword("password")
            });
            _adminRepository.Commit();
            return View();
        }

        [Authorize]
        public ActionResult PlaceManagement()
        {
            IList<PlaceListViewModel> model = new List<PlaceListViewModel>();
            if (_placeRepository.GetAll().Any())
            {
                foreach (var item in _placeRepository.GetAll().OrderByDescending(h => h.Verified))
                {
                    int photoId = _pictureRepository.GetAll().Where(p => p.PlaceId == item.PlaceId).FirstOrDefault().PictureID;
                    string photoPath = _pictureRepository.Get(photoId).Source;
                    model.Add(new PlaceListViewModel
                    {
                        PlaceId = item.PlaceId,
                        Name = item.Name,
                        Country = item.Country,
                        City = item.City,
                        Description = item.Description,
                        Picture = photoPath,
                        Verified = item.Verified
                    });
                }
            }
            return View(model);
        }

        [Authorize]
        public JsonResult Verify(int id)
        {
            var p = _placeRepository.Get(id);
            p.Verified = !p.Verified;
            _placeRepository.Commit();
            return null;
        }

        [Authorize]
        public JsonResult Delete(int id)
        {
            var pictures = _pictureRepository.GetAll().Where(p => p.PlaceId == id).Select(p => p).AsEnumerable();
            foreach (var item in pictures)
            {
                string path = Request.MapPath(@"~/Content/Photos/" + item.Source);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                _pictureRepository.Delete(item);
            }
            var opinions = _opinionRepository.GetAll().Where(p => p.PlaceId == id).Select(p => p).AsEnumerable();
            foreach (var item in opinions)
                _opinionRepository.Delete(item);

            _placeRepository.Delete(_placeRepository.Get(id));
            _opinionRepository.Commit();
            _placeRepository.Commit();
            _pictureRepository.Commit();
            return null;
        }

        [Authorize]
        public JsonResult AllPlaces()
        {
            IList<PlaceListViewModel> model = new List<PlaceListViewModel>();
            foreach (var place in _placeRepository.GetAll())
            {
                model.Add(new PlaceListViewModel
                {
                    City = place.City,
                    Country = place.Country,
                    Description = place.Description,
                    PlaceId = place.PlaceId,
                    Name = place.Name,
                    Picture = _pictureRepository.GetAll().First(p => p.PlaceId == place.PlaceId).Source,
                    Telephone = place.Telephone,
                    Verified = place.Verified,
                    Address = place.Address
                });
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult SaveChanges(Place place)
        {
            _placeRepository.Update(place);
            _placeRepository.Commit();
            return null;
        }
    }
}