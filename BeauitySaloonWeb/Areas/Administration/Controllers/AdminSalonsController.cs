using AutoMapper;
using BeauitySaloonWeb.Areas.Administration.Controllers.Base;
using BeauitySaloonWeb.CustomsValidations;
using BeauitySaloonWeb.Models;
using BeauitySaloonWeb.Models.ViewModel.Salons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Areas.Administration.Controllers
{
    public class AdminSalonsController : AdministrationController
    {
        private static readonly ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        // GET: Administration/Salons
        public ActionResult Index()
        {
            var viewModel = new SalonsListViewModel();
            IEnumerable<Salon> list = _applicationDbContext.Salons.ToList();
            Mapper.CreateMap<Salon, SalonViewModel>();
            if (list.Any())
            {
                viewModel.Salons = Mapper.Map<IEnumerable<SalonViewModel>>(list); //does not work, "cannot convert from 'System.Collections.Generic.IEnumerable<BloodDonatorsApp.Models.Donation>' to 'BloodDonatorsApp.Models.Donation'
            }
            return this.View(viewModel);
        }

        public ActionResult AddSalon()
        {
            var categories = _applicationDbContext.Categories.ToList();
            var cities = _applicationDbContext.Cities.ToList();

            this.ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            this.ViewData["Cities"] = new SelectList(cities, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public ActionResult AddSalon(SalonInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string imageUrl;
            try
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(input.file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    imageUrl = UploadPicture.WriteFile(input.file, "Salons");
                    // Add Salon
                    _applicationDbContext.Salons.Add(new Salon
                    {
                        Name = input.Name,
                        CategoryId = input.CategoryId,
                        CityId = input.CityId,
                        Address = input.Address,
                        ImageUrl = imageUrl,
                        Rating = 0,
                        RatersCount = 0,
                        CreatedOn = DateTime.Now,
                    });
                    var salonId = _applicationDbContext.SaveChanges();

                    // Add to the Salon all Services from its Category
                    var servicesIds = _applicationDbContext.Services.Where(x => x.CategoryId == input.CategoryId).OrderBy(x => x.Id).Select(x => x.Id).ToList();

                    foreach (var serviceId in servicesIds)
                    {
                        _applicationDbContext.SalonServices.Add(new SalonService
                        {
                            SalonId = salonId.ToString(),
                            ServiceId = serviceId,
                            Available = true,
                            CreatedOn = DateTime.Now
                        });
                        _applicationDbContext.SaveChanges();
                    }
                    return this.RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Please choose only Image file";
                    return View(input);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteSalon(string id)
        {
            try
            {
                if (id.StartsWith("seeded"))
                {
                    return this.RedirectToAction("Index");
                }
                int Id = Convert.ToInt32(id);
                var obj = _applicationDbContext.Salons.Where(x => x.Id == Id).FirstOrDefault();
                if (obj != null)
                {
                    _applicationDbContext.Salons.Remove(obj);
                    _applicationDbContext.SaveChanges();
                }
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message.ToString();
                return View("Error");
            }
        }
    }
}