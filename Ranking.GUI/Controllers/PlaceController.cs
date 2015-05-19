using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ranking.Domain.Abstract;
using Ranking.Domain.Models;
using Ranking.GUI.ViewModels;
using System.IO;
using System.Diagnostics;

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
                        Description = item.Descritpion,
                        Picture = photoPath
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
                Description = place.Descritpion,
                Name = place.Name,
                PlaceId = id
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

        public ActionResult AddOpinion(AddOpinionViewModel model)
        {
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
                            Descritpion = model.Description
                        });
                        _placeRepository.Commit();

                        int placeId = _placeRepository.GetAll().First(h => h.Name == model.Name).PlaceId;

                        foreach (var item in model.Picture)
                        {
                            string fileName = Guid.NewGuid().ToString();
                            string filePath = fileName + Path.GetExtension(item.FileName);
                            string path = Path.Combine(Server.MapPath(@"~/Content/Photos/"), filePath);
                            item.SaveAs(path);

                            _pictureRepository.Add(new Picture { Source = "Content/Photos/" + filePath, PlaceId = placeId });
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

        public ActionResult Delete(int id)
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
            return RedirectToAction("Index");
        }
    }
}