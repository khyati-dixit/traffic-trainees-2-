using PagedList;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using WebApplication3.Models;
using static WebApplication3.Models.UserViewModel;
using System;
using Microsoft.Ajax.Utilities;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        Entities en = new Entities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ResultViewModel collection)
        {
            var user = new UserDetail()
            {
                FullName = collection.FullName,
                UserEmail = collection.UserEmail,
                PasswordHash = collection.PasswordHash,
                CivilIdNumber = collection.CivilIdNumber,
                DOB = collection.DOB,
                MobileNo = collection.MobileNo,
                Address = collection.Address,
                RoleId = collection.RoleId,
                ProfilePic = collection.ProfilePic,
                CreatedDate = collection.CreatedDate,
                ModifiedDate = collection.ModifiedDate,
                IsNotificationActive = collection.IsNotificationActive,
                IsActive = collection.IsActive,
                DeviceId = collection.DeviceId,
                DeviceType = collection.DeviceType,
                FcmToken = collection.FcmToken,
                verify = collection.verify,
                VerifiedBy = collection.VerifiedBy,
                Area = collection.Area,
                Block = collection.Block,
                Street = collection.Street,
                Housing = collection.Housing,
                Floor = collection.Floor,
                NewPass = collection.NewPass,
                ConPass = collection.ConPass,
                Jadda = collection.Jadda,
                Reason = collection.Reason,
                ActivatedBy = collection.ActivatedBy,
                ActivatedDate = collection.ActivatedDate


            };

            var car = new CarDetail()
            {
                CarLicense = collection.CarLicense

            };
            en.UserDetails.Add(user);
            en.CarDetails.Add(car);
            en.SaveChanges();
            return RedirectToAction("About");
        }

        [HttpGet]
        public ActionResult About(int page = 1, int pageSize = 20)
        {
         
            using (Entities us = new Entities())
            {
                List<UserDetail> userDetails = us.UserDetails.ToList();
                List<CarDetail> carDetails = us.CarDetails.ToList();

                List<ResultViewModel> resultViewModels = new List<ResultViewModel>();

                foreach (var user in userDetails)
                {
                    var data = new ResultViewModel
                    {
                        UserId = user.UserId,
                        FullName = user.FullName,
                        UserEmail = user.UserEmail,
                        CivilIdNumber = user.CivilIdNumber,


                    };

                    var cardetails = string.Join(",", carDetails.Where(x => x.UserId == user.UserId).Select(y => y.CarLicense).ToList());

                    data.CarLicense = cardetails;
                    resultViewModels.Add(data);



                }

                PagedList<ResultViewModel> model = new PagedList<ResultViewModel>(resultViewModels, page, pageSize);
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            List<UserDetail> USER = en.UserDetails.ToList();
            var data = from u in USER
                       where u.UserId == id
                       select u;

            return View(data);
        }



        //Customer c = (from x in dataBase.Customers
        //              where x.Name == "Test"
        //              select x).First();
        //        c.Name = "New Name";
        //dataBase.SaveChanges();
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var viewModel = (from a in en.UserDetails
                             where a.UserId == id
                             select new Models.UserViewModel
                             {
                                 UserId = a.UserId,
                                 FullName = a.FullName,
                                 UserEmail = a.UserEmail,
                                 PasswordHash = a.PasswordHash,
                                 CivilIdNumber = a.CivilIdNumber,
                                 DOB = a.DOB,
                                 MobileNo = a.MobileNo,
                                 Address = a.Address,
                                 RoleId = a.RoleId,
                                 ProfilePic = a.ProfilePic,
                                 CreatedDate = a.CreatedDate,
                                 ModifiedDate = a.ModifiedDate,
                                 IsNotificationActive = a.IsNotificationActive,
                                 IsActive = a.IsActive,
                                 DeviceId = a.DeviceId,
                                 DeviceType = a.DeviceType,
                                 FcmToken = a.FcmToken,
                                 verify = a.verify,
                                 VerifiedBy = a.VerifiedBy,
                                 Area = a.Area,
                                 Block = a.Block,
                                 Street = a.Street,
                                 Housing = a.Housing,
                                 Floor = a.Floor,
                                 NewPass = a.NewPass,
                                 ConPass = a.ConPass,
                                 Jadda = a.Jadda,
                                 Reason = a.Reason,
                                 ActivatedBy = a.ActivatedBy,
                                 ActivatedDate = a.ActivatedDate
                             }).FirstOrDefault();

            var cars = en.CarDetails.Where(x => x.UserId == id).Select(y => new carDetailsUser { CarLicense = y.CarLicense, Id = y.Id }).ToList();
            viewModel.CarLicense.AddRange(cars);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel obj, int id)
        {
            var dtls = en.UserDetails.Where(x => x.UserId == obj.UserId).FirstOrDefault();
            //var dtls1 = en.CarDetails.Where(x => x.UserId == id).Select(y => new carDetailsUser { CarLicense = y.CarLicense, Id = y.Id }).ToList();
            dtls.FullName = obj.FullName;
            dtls.UserEmail = obj.UserEmail;
            dtls.PasswordHash = obj.PasswordHash;
            dtls.CivilIdNumber = obj.CivilIdNumber;
            dtls.DOB = obj.DOB;
            dtls.MobileNo = obj.MobileNo;
            dtls.Address = obj.Address;
            dtls.RoleId = obj.RoleId;
            dtls.ProfilePic = obj.ProfilePic;
            dtls.CreatedDate = obj.CreatedDate;
            dtls.ModifiedDate = obj.ModifiedDate;
            dtls.IsNotificationActive = obj.IsNotificationActive;
            dtls.IsActive = obj.IsActive;
            dtls.DeviceId = obj.DeviceId;
            dtls.DeviceType = obj.DeviceType;
            dtls.FcmToken = obj.FcmToken;
            dtls.verify = obj.verify;
            dtls.VerifiedBy = obj.VerifiedBy;
            dtls.Area = obj.Area;
            dtls.Block = obj.Block;
            dtls.Street = obj.Street;
            dtls.Housing = obj.Housing;
            dtls.Floor = obj.Floor;
            dtls.NewPass = obj.NewPass;
            dtls.ConPass = obj.ConPass;
            dtls.Jadda = obj.Jadda;
            dtls.Reason = obj.Reason;
            dtls.ActivatedBy = obj.ActivatedBy;
            dtls.ActivatedDate = obj.ActivatedDate;

            //if (!dtls1.Equals = (null))
            //{
            //    obj.CarLicense.AddRange(dtls1);
            //    en.Entry(dtls1).State = EntityState.Modified;
            //}

            //dtls1.CarLicense = obj.CarLicense;
            en.Entry(dtls).State = EntityState.Modified;

            en.SaveChanges();
            int output = en.SaveChanges();
            if (output > 0)
            {
                ViewBag.msg = "Updated Added Successfully";
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ResultViewModel collection)
        {
            var user = new UserDetail()
            {
                FullName = collection.FullName,
                UserEmail = collection.UserEmail,
                PasswordHash = collection.PasswordHash,
                CivilIdNumber = collection.CivilIdNumber,
                DOB = collection.DOB,
                MobileNo = collection.MobileNo,
                Address = collection.Address,
                RoleId = collection.RoleId,
                ProfilePic = collection.ProfilePic,
                CreatedDate = collection.CreatedDate,
                ModifiedDate = collection.ModifiedDate,
                IsNotificationActive = collection.IsNotificationActive,
                IsActive = collection.IsActive,
                DeviceId = collection.DeviceId,
                DeviceType = collection.DeviceType,
                FcmToken = collection.FcmToken,
                verify = collection.verify,
                VerifiedBy = collection.VerifiedBy,
                Area = collection.Area,
                Block = collection.Block,
                Street = collection.Street,
                Housing = collection.Housing,
                Floor = collection.Floor,
                NewPass = collection.NewPass,
                ConPass = collection.ConPass,
                Jadda = collection.Jadda,
                Reason = collection.Reason,
                ActivatedBy = collection.ActivatedBy,
                ActivatedDate = collection.ActivatedDate


            };

            var car = new CarDetail()
            {
                CarLicense = collection.CarLicense

            };
            en.UserDetails.Add(user);
            en.CarDetails.Add(car);
            en.SaveChanges();

            return RedirectToAction("About");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var viewModel = (from a in en.UserDetails
                             where a.UserId == id
                             select new Models.ResultViewModel
                             {
                                 UserId = a.UserId,
                                 FullName = a.FullName,
                                 UserEmail = a.UserEmail,
                                 PasswordHash = a.PasswordHash,
                                 CivilIdNumber = a.CivilIdNumber,
                                 DOB = a.DOB,
                                 MobileNo = a.MobileNo,
                                 Address = a.Address,
                                 RoleId = a.RoleId,
                                 ProfilePic = a.ProfilePic,
                                 CreatedDate = a.CreatedDate,
                                 ModifiedDate = a.ModifiedDate,
                                 IsNotificationActive = a.IsNotificationActive,
                                 IsActive = a.IsActive,
                                 DeviceId = a.DeviceId,
                                 DeviceType = a.DeviceType,
                                 FcmToken = a.FcmToken,
                                 verify = a.verify,
                                 VerifiedBy = a.VerifiedBy,
                                 Area = a.Area,
                                 Block = a.Block,
                                 Street = a.Street,
                                 Housing = a.Housing,
                                 Floor = a.Floor,
                                 NewPass = a.NewPass,
                                 ConPass = a.ConPass,
                                 Jadda = a.Jadda,
                                 Reason = a.Reason,
                                 ActivatedBy = a.ActivatedBy,
                                 ActivatedDate = a.ActivatedDate
                             }).FirstOrDefault();
            return View(viewModel);
        }

        //public void DeleteUserGroup(MY_GROUPS ug)
        //{
        //    using (var context = new MYConn())
        //    {
        //        var entry = context.Entry(ug);
        //        if (entry.State == EntityState.Detached)
        //        {
        //            context.MY_GROUPS.Attach(ug);
        //        }

        //        context.MY_GROUPS.Remove(ug);
        //        context.SaveChanges();
        //    }
        //}
        [HttpPost]
        public ActionResult Delete(ResultViewModel OBJ,int id)
        {
            var viewModel = en.UserDetails.Where(x => x.UserId == id).FirstOrDefault();
            var viewModel1 = en.CarDetails.Where(x => x.UserId == id).FirstOrDefault();
            //viewModel.IsActive = false;
            
            if(viewModel1!=null)
            {
                var o = en.Entry(viewModel1);
                if (o.State == EntityState.Detached)
                {
                    en.CarDetails.Attach(viewModel1);
                }
                en.CarDetails.Remove(viewModel1);
            }
            //    en.Entry(viewModel1).State = EntityState.Modified;
            en.UserDetails.Remove(viewModel);
           
            en.SaveChanges();
            return RedirectToAction("About");
        }
        [HttpGet]
        public ActionResult Contact()
        {
            List<UserDetail> USER = en.UserDetails.ToList();
            var data = from u in USER
                       select u;
            return View(data);
        }
    }
}