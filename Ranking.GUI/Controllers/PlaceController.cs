﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ranking.Domain.Abstract;
using Ranking.Domain.Models;
using Ranking.GUI.ViewModels.Opinions;
using Ranking.GUI.ViewModels.Places;
using System.IO;
using System.Diagnostics;
using System.Dynamic;

namespace Ranking.GUI.Controllers
{
    public class PlaceController : Controller
    {
        private IRepository<Place> _placeRepository = new Repository<Place>();
        private IRepository<Opinion> _opinionRepository = new Repository<Opinion>();
        private IRepository<Picture> _pictureRepository = new Repository<Picture>();
        private IRepository<User> _userRepository = new Repository<User>();

        public ActionResult Index()
        {
            IList<PlaceListViewModel> model = new List<PlaceListViewModel>();
            if (_placeRepository.GetAll().Any())
            {
                foreach (var item in _placeRepository.GetAll().OrderByDescending(h => h.PlaceId).Take(5))
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
                        Rate = item.Rate
                    });
                }
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            Place place = _placeRepository.Get(id);
            PlaceDetailsViewModel model = new PlaceDetailsViewModel
            {
                Country = place.Country,
                City = place.City,
                Description = place.Description,
                Name = place.Name,
                PlaceId = id,
                Rate = place.Rate,
                Telephone = place.Telephone,
                Address = place.Address,
                Verified = place.Verified
            };
            model.Opinions = new List<KeyValuePair<string, Opinion>>();
            model.Picture = new List<string>();

            var picturesPath = _pictureRepository.GetAll().Where(p => p.PlaceId == id).Select(p => p.Source);

            foreach (var item in picturesPath)
                model.Picture.Add(item);

            var opinions = _opinionRepository.GetAll().Where(o => o.PlaceId == id).Select(o => o);
            foreach (var item in opinions)
            {
                string user = _userRepository.Get(item.UserID).Nick;
                model.Opinions.Add(new KeyValuePair<string, Opinion>(user, item));
            }
            return View(model);
        }

        public ActionResult Opinions(int id)
        {
            IList<OpinionsViewModel> model = new List<OpinionsViewModel>();
            var opinions = _opinionRepository.GetAll().Where(o => o.PlaceId == id).ToList();
            foreach (var item in opinions)
            {
                model.Add(new OpinionsViewModel
                {
                    Content = item.Content,
                    AddDate = item.AddDate,
                    Rate = item.Rate,
                    UserId = item.UserID,
                    UserNickname = _userRepository.Get(item.UserID).Nick
                });
            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult AddOpinion(int id)
        {
            AddOpinionViewModel model = new AddOpinionViewModel();
            model.PlaceId = id;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult AddOpinion(AddOpinionViewModel model, FormCollection form, int id)
        {
            model.PlaceId = id;
            model.Rate = Double.Parse(form["rate"], System.Globalization.CultureInfo.InvariantCulture);
            //if (ModelState["Rate"].Errors.Count > 0)
            //    ModelState["Rate"].Errors.Clear();

            if (ModelState.IsValid && model.Email.Contains('@'))
            {
                if (!_userRepository.GetAll().Any(u => u.Nick == model.Nick && u.Email == model.Email))
                {
                    _userRepository.Add(new User
                    {
                        Email = model.Email,
                        Nick = model.Nick,
                    });
                    _userRepository.Commit();
                }
                int uid = _userRepository.GetAll().FirstOrDefault(u => u.Nick == model.Nick).UserID;
                _opinionRepository.Add(new Opinion
                {
                    AddDate = DateTime.Now,
                    Content = model.Content,
                    Rate = model.Rate,
                    PlaceId = id,
                    UserID = uid
                });
                _opinionRepository.Commit();
                var placeRate = _opinionRepository.GetAll().Where(o => o.PlaceId == id).Select(o => o.Rate);
                double srednia = Math.Round(placeRate.Sum() / placeRate.Count(), 2);
                _placeRepository.Get(id).Rate = srednia;
                _placeRepository.Commit();

                return RedirectToAction("Details", new { id = model.PlaceId });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddPlace()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPlace(AddPlaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_placeRepository.GetAll().Any(p => p.Name == model.Name))
                {
                    try
                    {
                        _placeRepository.Add(new Place
                        {
                            Country = model.Country,
                            City = model.City,
                            Name = model.Name,
                            Description = model.Description,
                            Verified = false,
                            Telephone = model.Telephone == null ? "" : model.Telephone,
                            Address = model.Address == null ? "" : model.Address,
                            Rate = 0
                        });
                        _placeRepository.Commit();

                        int placeId = _placeRepository.GetAll().First(h => h.Name == model.Name).PlaceId;

                        if (model.Picture.ElementAt(0) != null)
                        {
                            foreach (var item in model.Picture)
                            {
                                string fileName = Guid.NewGuid().ToString();
                                string filePath = fileName + Path.GetExtension(item.FileName);
                                string path = Path.Combine(Server.MapPath(@"~/Content/Photos/"), filePath);
                                item.SaveAs(path);

                                _pictureRepository.Add(new Picture { Source = "Content/Photos/" + filePath, PlaceId = placeId });
                                _pictureRepository.Commit();
                            }
                        }
                        else
                        {
                            _pictureRepository.Add(new Picture { Source = "Content/Images/default-image.png", PlaceId = placeId });
                            _pictureRepository.Commit();
                        }
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return View();
        }

        public ActionResult TopTen()
        {
            var sortedPlaces = _placeRepository.GetAll()/*.Where(p => p.Verified)*/.OrderByDescending(o => o.Rate).Take(10);

            IList<PlaceListViewModel> model = new List<PlaceListViewModel>();

            foreach (var item in sortedPlaces)
            {
                model.Add(new PlaceListViewModel
                {
                    City = item.City,
                    Country = item.Country,
                    Description = item.Description,
                    Rate = item.Rate,
                    Name = item.Name,
                    Picture = _pictureRepository.GetAll().First(p => p.PlaceId == item.PlaceId).Source,
                    PlaceId = item.PlaceId
                });
            }

            return View(model);
        }

        public ActionResult All()
        {
            return View();
        }

        public ActionResult Popular(string name)
        {
            var places = _placeRepository.GetAll().Where(p => p.City == name || p.Country == name).OrderByDescending(p => p.Rate).ToList();

            IList<PlaceListViewModel> model = new List<PlaceListViewModel>();

            foreach (var item in places)
            {
                model.Add(new PlaceListViewModel
                {
                    City = item.City,
                    Country = item.Country,
                    Description = item.Description,
                    Rate = item.Rate,
                    Name = item.Name,
                    Picture = _pictureRepository.GetAll().First(p => p.PlaceId == item.PlaceId).Source,
                    PlaceId = item.PlaceId
                });
            }
            return View(model);
        }

        public JsonResult GetPlaces()
        {
            IList<SearchViewModel> model = new List<SearchViewModel>();
            foreach (var place in _placeRepository.GetAll()/*.Where(p => p.Verified)*/)
            {
                model.Add(new SearchViewModel
                {
                    City = place.City,
                    Country = place.Country,
                    //Description = place.Description,
                    PlaceId = place.PlaceId,
                    Name = place.Name,
                    Picture = _pictureRepository.GetAll().First(p => p.PlaceId == place.PlaceId).Source,
                    Rate = place.Rate,
                    Verified = place.Verified
                });
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PolishCities()
        {
            var cities = _placeRepository.GetAll().Where(p => p.Country == "Polska").Select(p => p.City).GroupBy(p => p)
                .Select(places => new { Name = places.Key, Count = places.Count() });
            var result = cities.OrderBy(p => p.Count).Select(p => p.Name).Take(3);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopularCountries()
        {
            var cities = _placeRepository.GetAll().Where(p => p.Country != "Polska").Select(p => p.Country).GroupBy(p => p)
                .Select(places => new { Name = places.Key, Count = places.Count() });
            var result = cities.OrderBy(p => p.Count).Select(p => p.Name).Take(3);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}